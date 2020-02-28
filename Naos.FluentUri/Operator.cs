// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Operator.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Serialization;

    /// <summary>
    /// Methods to wrap WebRequest usage.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Operator", Justification = "Spelling/name is correct.")]
    public static class Operator
    {
        /// <summary>
        /// Makes a restful call using supplied information.
        /// </summary>
        /// <typeparam name="TResult">Return type to convert response to (if you provide VoidResultType then null will be returned - basically a void call).</typeparam>
        /// <param name="uri">Uri to make the request against.</param>
        /// <param name="httpVerb">HTTP verb to use.</param>
        /// <param name="body">Optional body object to send (use null if not needed).</param>
        /// <param name="cookieJar">Optional cookie to use (use null if not needed).</param>
        /// <param name="headerJar">Optional headers to use (use null if not needed).</param>
        /// <param name="saveResponseHeadersAction">Optional action to use to save response headers (use null if not needed).</param>
        /// <param name="contentType">Content type to use for request.</param>
        /// <param name="acceptType">Content type to use for response.</param>
        /// <param name="timeout">Timeout to use.</param>
        /// <param name="serializer">Serializer to use.</param>
        /// <returns>Converted response to the specified type.</returns>
        public static TResult Call<TResult>(
            Uri                                    uri,
            HttpVerb                               httpVerb,
            object                                 body,
            CookieJar                              cookieJar,
            HeaderJar                              headerJar,
            Action<KeyValuePair<string, string>[]> saveResponseHeadersAction,
            ContentType                            contentType,
            ContentType                            acceptType,
            TimeSpan                               timeout,
            IStringSerializeAndDeserialize         serializer)
            where TResult : class
        {
            var httpVerbAsString = httpVerb.ToString().ToUpperInvariant();
            return Call<TResult>(
                uri,
                httpVerbAsString,
                body,
                cookieJar,
                headerJar,
                saveResponseHeadersAction,
                contentType,
                acceptType,
                timeout,
                serializer);
        }

        /// <summary>
        /// Makes a restful call using supplied information.
        /// </summary>
        /// <typeparam name="TResult">Return type to convert response to (if you provide VoidResultType then null will be returned - basically a void call).</typeparam>
        /// <param name="uri">Uri to make the request against.</param>
        /// <param name="httpVerb">HTTP verb to use.</param>
        /// <param name="body">Optional body object to send (use null if not needed).</param>
        /// <param name="cookieJar">Optional cookie to use (use null if not needed).</param>
        /// <param name="headerJar">Optional headers to use (use null if not needed).</param>
        /// <param name="saveResponseHeadersAction">Optional action to use to save response headers (use null if not needed).</param>
        /// <param name="contentType">Content type to use for request.</param>
        /// <param name="acceptType">Content type to use for response.</param>
        /// <param name="timeout">Timeout to use.</param>
        /// <param name="serializer">Serializer to use.</param>
        /// <returns>Converted response to the specified type.</returns>
        /// <exception cref="ArgumentException">
        /// Must have return of string when accepting text type.
        /// or
        /// ContentType: " + contentType + " not supported at this time. - contentType
        /// or
        /// AcceptType: " + contentType + " not supported at this time. - acceptType
        /// or
        /// AcceptType: " + acceptType + " not supported at this time. - acceptType.
        /// </exception>
        public static TResult Call<TResult>(
            Uri                                    uri,
            string                                 httpVerb,
            object                                 body,
            CookieJar                              cookieJar,
            HeaderJar                              headerJar,
            Action<KeyValuePair<string, string>[]> saveResponseHeadersAction,
            ContentType                            contentType,
            ContentType                            acceptType,
            TimeSpan                               timeout,
            IStringSerializeAndDeserialize         serializer)
            where TResult : class
        {
            new { uri }.AsArg().Must().NotBeNull();
            new { httpVerb }.AsArg().Must().NotBeNullNorWhiteSpace();
            new { serializer }.AsArg().Must().NotBeNull();

            if (acceptType == ContentType.TextPlain && typeof(TResult) != typeof(string))
            {
                throw new ArgumentException("Must have return of string when accepting text type.");
            }

            if (contentType != ContentType.ApplicationJson)
            {
                throw new ArgumentException("ContentType: " + contentType + " not supported at this time.", nameof(contentType));
            }

            if (acceptType != ContentType.ApplicationJson && acceptType != ContentType.TextPlain)
            {
                throw new ArgumentException("AcceptType: " + contentType + " not supported at this time.", nameof(acceptType));
            }

            if (timeout == default(TimeSpan))
            {
                timeout = TimeSpan.FromSeconds(100);
            }

            var cookieContainer = new CookieContainer();
            foreach (var cookie in cookieJar.Cookies)
            {
                cookieContainer.Add(cookie);
            }

            // ReSharper disable once AccessToStaticMemberViaDerivedType - want to call this method...
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.CookieContainer = cookieContainer;
            request.ContentType     = contentType.ToStringValue();
            request.Accept          = acceptType.ToStringValue();
            request.Method          = httpVerb;
            request.Timeout         = (int)timeout.TotalMilliseconds;

            LoadRequestHeaders(request, headerJar);

            string bodyAsString = null;
            if (contentType == ContentType.ApplicationJson && body != null)
            {
                bodyAsString = serializer.SerializeToString(body);
            }

            if (httpVerb != HttpVerb.Get.ToString().ToUpperInvariant() && !string.IsNullOrWhiteSpace(bodyAsString))
            {
                request.ContentLength = bodyAsString.Length;
                using (var requestStream = request.GetRequestStream())
                {
                    using (var requestWriter = new StreamWriter(requestStream, Encoding.ASCII))
                    {
                        requestWriter.Write(bodyAsString);
                        requestWriter.Close();
                    }
                }
            }

            string              contents = null;
            WebHeaderCollection responseHeadersRaw;
            using (var resp = request.GetResponse())
            {
                responseHeadersRaw = resp.Headers;

                var responseStream = resp.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        contents = reader.ReadToEnd();
                    }
                }
            }

            TResult ret = default(TResult);
            if (typeof(TResult) == typeof(VoidResultType))
            {
                return ret; // this will just be null and should only be used when you don't want a return
            }
            else if (acceptType == ContentType.ApplicationJson)
            {
                ret = serializer.Deserialize<TResult>(contents);
            }
            else if (acceptType == ContentType.TextPlain)
            {
                ret = contents as TResult;
            }
            else
            {
                throw new ArgumentException("AcceptType: " + acceptType + " not supported at this time.", nameof(acceptType));
            }

            var responseHeaders = responseHeadersRaw == null
                ? new KeyValuePair<string, string>[0]
                : responseHeadersRaw.ToKeyValuePairArray();

            saveResponseHeadersAction?.Invoke(responseHeaders);

            return ret;
        }

        private static void LoadRequestHeaders(HttpWebRequest request, HeaderJar headerJar)
        {
            new { request }.AsArg().Must().NotBeNull();

            if (headerJar?.Headers != null)
            {
                foreach (var header in headerJar.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
        }

        /// <summary>
        /// Convert the enumeration value of Content Type to the appropriate string value.
        /// </summary>
        /// <param name="contentType">Enumeration content type.</param>
        /// <returns>Appropriate string value of the enumeration.</returns>
        /// <exception cref="ArgumentException">Unsupported content type: " + contentType - contentType.</exception>
        public static string ToStringValue(this ContentType contentType)
        {
            switch (contentType)
            {
                case ContentType.ApplicationJson:
                    return "application/json";
                case ContentType.TextPlain:
                    return "text/plain";
                default:
                    throw new ArgumentException("Unsupported content type: " + contentType, nameof(contentType));
            }
        }
    }
}
