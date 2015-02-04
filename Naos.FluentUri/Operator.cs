// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Operator.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
    /// Methods to wrap WebRequest usage
    /// </summary>
    public static class Operator
    {
        /// <summary>
        /// Makes a restful call using supplied information.
        /// </summary>
        /// <param name="uri">Uri to make the request against.</param>
        /// <param name="httpVerb">HTTP verb to use.</param>
        /// <param name="body">Optional body object to send (use null if not needed).</param>
        /// <param name="headers">Optional headers to use (use null if not needed).</param>
        /// <param name="cookieJar">Optional cookie to use (use null if not needed).</param>
        /// <param name="contentType">Content type to use.</param>
        /// <param name="timeout">Timeout to use.</param>
        /// <typeparam name="TResult">Return type to convert response to (if you provide VoidResultType then VoidResultType.Default will be returned - basically a void call).</typeparam>
        /// <returns>Converted response to the specified type.</returns>
        public static TResult Call<TResult>(
            Uri uri,
            Enums.HttpVerb httpVerb,
            object body,
            KeyValuePair<string, string>[] headers,
            CookieJar cookieJar,
            Enums.ContentType contentType,
            TimeSpan timeout) where TResult : class
        {
            if (contentType != Enums.ContentType.ApplicationJson)
            {
                throw new ArgumentException("ContentType: " + contentType + " not supported at this time.", "contentType");
            }

            if (timeout == default(TimeSpan))
            {
                timeout = TimeSpan.FromSeconds(100);
            }

            var cookieContainer = new CookieContainer();
            if (cookieJar != null)
            {
                if (cookieJar.SystemNetCookie != null && cookieJar.SystemWebHttpCookie != null)
                {
                    throw new ArgumentException("CookieJar is not intended to have each cookie set", "cookieJar");
                }
                else if (cookieJar.SystemNetCookie != null)
                {
                    cookieContainer.Add(cookieJar.SystemNetCookie);
                }
                else if (cookieJar.SystemWebHttpCookie != null)
                {
                    cookieContainer.Add(cookieJar.SystemWebHttpCookie.ToSystemNetCookie());
                }
            }

            var httpVerbAsString = httpVerb.ToString().ToUpper();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri);
            req.CookieContainer = cookieContainer;
            req.ContentType = contentType.ToStringValue();
            req.Method = httpVerbAsString;
            req.Timeout = (int)timeout.TotalMilliseconds;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    req.Headers.Add(header.Key, header.Value);
                }
            }

            string bodyAsString = null;
            if (contentType == Enums.ContentType.ApplicationJson && body != null)
            {
                bodyAsString = JsonConvert.SerializeObject(body);
            }

            if (httpVerb != Enums.HttpVerb.Get && !string.IsNullOrEmpty(bodyAsString))
            {
                req.ContentLength = bodyAsString.Length;
                using (var requestWriter = new StreamWriter(req.GetRequestStream(), Encoding.ASCII))
                {
                    requestWriter.Write(bodyAsString);
                    requestWriter.Close();
                }
            }

            string contents = null;
            WebHeaderCollection responseHeadersRaw;
            using (var resp = req.GetResponse())
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

            // TODO: maybe raise these via an event later?
            var responseHeaders = responseHeadersRaw.ToKeyValuePairArray();

            TResult ret = null;
            if (typeof(TResult) == typeof(VoidResultType))
            {
                ret = VoidResultType.Default as TResult;
            }
            else if (contentType == Enums.ContentType.ApplicationJson)
            {
                ret = JsonConvert.DeserializeObject<TResult>(contents);
            }

            return ret;
        }

        /// <summary>
        /// Convert the enumeration value of Content Type to the appropriate string value.
        /// </summary>
        /// <param name="contentType">Enumeration content type.</param>
        /// <returns>Appropriate string value of the enumeration.</returns>
        public static string ToStringValue(this Enums.ContentType contentType)
        {
            switch (contentType)
            {
                case Enums.ContentType.ApplicationJson:
                    return "application/json";
                default:
                    throw new ArgumentException("Unsupported content type: " + contentType, "contentType");
            }
        }
    }
}
