// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Naos.FluentUri.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;

    using Newtonsoft.Json;

    /// <summary>
    /// Verbs that can be used for HTTP calls.
    /// </summary>
    public enum HttpVerb
    {
        /// <summary>
        /// Get verb.
        /// </summary>
        Get,

        /// <summary>
        /// Post verb.
        /// </summary>
        Post,

        /// <summary>
        /// Put verb.
        /// </summary>
        Put,

        /// <summary>
        /// Delete verb.
        /// </summary>
        Delete
    }

    /// <summary>
    /// Content type to use for HTTP calls.
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// Application JSON type.
        /// </summary>
        ApplicationJson
    }

    /// <summary>
    /// Exception object for duplicate calls to 
    /// </summary>
    public class DuplicateCallUsingFluentGrammarException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateCallUsingFluentGrammarException"/> class.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        public DuplicateCallUsingFluentGrammarException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Type to use for generics that indicates the lack of a return.
    /// </summary>
    public class VoidResultType
    {
        private static readonly VoidResultType DefaultField = new VoidResultType();

        /// <summary>
        /// Gets the default object to use for return.
        /// </summary>
        public static VoidResultType Default
        {
            get
            {
                return DefaultField;
            }
        }
    }

    /// <summary>
    /// Extension methods to create a single item array from an object.
    /// </summary>
    public static class ObjectToArrayExtensionMethod
    {
        /// <summary>
        /// Converts the object into a single element array.
        /// </summary>
        /// <typeparam name="T">Type of the object being used.</typeparam>
        /// <param name="objectToEncapsulateInArray">Object that extension method operates on.</param>
        /// <returns>New array containing the single item.</returns>
        public static T[] ToSingleElementArray<T>(this T objectToEncapsulateInArray)
        {
            return new[] { objectToEncapsulateInArray };
        }
    }

    /// <summary>
    /// Extension methods to convert the two types of cookies.
    /// </summary>
    public static class CookieConvertionExtensionMethods
    {
        /// <summary>
        /// Extension method to convert a System.Web.HttpCookie to a System.Net.Cookie object.
        /// </summary>
        /// <param name="cookie">HttpCookie to translate.</param>
        /// <returns>Translated Cookie.</returns>
        public static Cookie ToSystemNetCookie(this HttpCookie cookie)
        {
            var ret = new Cookie
            {
                Domain = cookie.Domain,
                Expires = cookie.Expires,
                Name = cookie.Name,
                Path = cookie.Path,
                Secure = cookie.Secure,
                Value = cookie.Value,
            };

            return ret;
        }

        /// <summary>
        /// Extension method to convert a System.Net.Cookie to a System.Web.HttpCookie object.
        /// </summary>
        /// <param name="cookie">Cookie to translate.</param>
        /// <returns>Translated HttpCookie.</returns>
        public static HttpCookie ToSystemWebHttpCookie(this Cookie cookie)
        {
            var ret = new HttpCookie(cookie.Name)
            {
                Domain = cookie.Domain,
                Expires = cookie.Expires,
                Name = cookie.Name,
                Path = cookie.Path,
                Secure = cookie.Secure,
                Value = cookie.Value,
            };

            return ret;
        }
    }

    /// <summary>
    /// Container to hold different types of cookies.
    /// </summary>
    public class CookieJar
    {
        private readonly List<Cookie> cookies = new List<Cookie>();

        /// <summary>
        /// Gets the cookies.
        /// </summary>
        public Cookie[] Cookies
        {
            get
            {
                return this.cookies.ToArray();
            }
        }

        /// <summary>
        /// Adds the cookie to the set of cookie to use for a request.
        /// </summary>
        /// <param name="cookie">Cookie to add to request.</param>
        public void AddCookie(Cookie cookie)
        {
            this.cookies.Add(cookie);
        }

        /// <summary>
        /// Adds the cookie to the set of cookie to use for a request.
        /// </summary>
        /// <param name="cookie">Cookie to add to request.</param>
        public void AddCookie(HttpCookie cookie)
        {
            this.cookies.Add(cookie.ToSystemNetCookie());
        }
    }

    /// <summary>
    /// Utility methods to convert between different types of header collections.
    /// </summary>
    public static class HeaderConvertionExtensionMethods
    {
        /// <summary>
        /// Gets the header in question if present (requires second level check...).
        /// </summary>
        /// <param name="headers">The headers set to look at.</param>
        /// <param name="name">The header name to find.</param>
        /// <returns>The value of the specified key if present.</returns>
        public static string GetHeaderByName(this NameValueCollection headers, string name)
        {
            var values = headers.GetValues(name);

            if (values == null)
            {
                return null;
            }

            return values.FirstOrDefault();
        }

        /// <summary>
        /// Gets the header in question if present (requires second level check...).
        /// </summary>
        /// <param name="headers">The headers set to look at.</param>
        /// <param name="name">The header name to find.</param>
        /// <returns>The value of the specified key if present.</returns>
        public static string GetHeaderByName(this WebHeaderCollection headers, string name)
        {
            var values = headers.GetValues(name);

            if (values == null)
            {
                return null;
            }

            return values.FirstOrDefault();
        }

        /// <summary>
        /// Gets the collection as a key value pair array.
        /// </summary>
        /// <param name="collection">Collection of items as a name value collection.</param>
        /// <returns>Array of key value pairs of the items name and value.</returns>
        public static KeyValuePair<string, string>[] ToKeyValuePairArray(this NameValueCollection collection)
        {
            var ret = new List<KeyValuePair<string, string>>();
            if (collection != null)
            {
                foreach (var key in collection.AllKeys)
                {
                    var values = collection.GetValues(key);
                    if (values != null)
                    {
                        foreach (var value in values)
                        {
                            ret.Add(new KeyValuePair<string, string>(key, value));
                        }
                    }
                }
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Gets the headers collection as a key value pair array.
        /// </summary>
        /// <param name="headers">Headers as a web header collection.</param>
        /// <returns>Array of key value pairs of the header name and value.</returns>
        public static KeyValuePair<string, string>[] ToKeyValuePairArray(this WebHeaderCollection headers)
        {
            var ret = new List<KeyValuePair<string, string>>();
            if (headers != null)
            {
                foreach (var key in headers.AllKeys)
                {
                    var values = headers.GetValues(key);
                    if (values != null)
                    {
                        foreach (var value in values)
                        {
                            ret.Add(new KeyValuePair<string, string>(key, value));
                        }
                    }
                }
            }

            return ret.ToArray();
        }
    }

    /// <summary>
    /// Container to hold different types of headers.
    /// </summary>
    public class HeaderJar
    {
        private readonly List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Gets the headers.
        /// </summary>
        public KeyValuePair<string, string>[] Headers
        {
            get
            {
                return this.headers.ToArray();
            }
        }

        /// <summary>
        /// Adds the headers to the set of headers to use in the request.
        /// </summary>
        /// <param name="headers">Headers to add to set.</param>
        public void Add(NameValueCollection headers)
        {
            var transformedHeaders = headers.ToKeyValuePairArray();
            this.headers.AddRange(transformedHeaders);
        }

        /// <summary>
        /// Adds the headers to the set of headers to use in the request.
        /// </summary>
        /// <param name="headers">Headers to add to set.</param>
        public void Add(WebHeaderCollection headers)
        {
            var transformedHeaders = headers.ToKeyValuePairArray();
            this.headers.AddRange(transformedHeaders);
        }

        /// <summary>
        /// Adds the headers to the set of headers to use in the request.
        /// </summary>
        /// <param name="headers">Headers to add to set.</param>
        public void Add(KeyValuePair<string, string>[] headers)
        {
            this.headers.AddRange(headers);
        }

        /// <summary>
        /// Adds the headers to the set of headers to use in the request.
        /// </summary>
        /// <param name="name">Name of header to add to set.</param>
        /// <param name="value">Value of header to add to set.</param>
        public void Add(string name, string value)
        {
            this.headers.Add(new KeyValuePair<string, string>(name, value));
        }
    }

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
        /// <param name="cookieJar">Optional cookie to use (use null if not needed).</param>
        /// <param name="headers">Optional headers to use (use null if not needed).</param>
        /// <param name="saveResponseHeadersAction">Optional action to use to save response headers (use null if not needed).</param>
        /// <param name="contentType">Content type to use for request.</param>
        /// <param name="acceptType">Content type to use for response.</param>
        /// <param name="timeout">Timeout to use.</param>
        /// <typeparam name="TResult">Return type to convert response to (if you provide VoidResultType then null will be returned - basically a void call).</typeparam>
        /// <returns>Converted response to the specified type.</returns>
        public static TResult Call<TResult>(
            Uri uri,
            HttpVerb httpVerb,
            object body,
            CookieJar cookieJar,
            KeyValuePair<string, string>[] headers,
            Action<KeyValuePair<string, string>[]> saveResponseHeadersAction,
            ContentType contentType,
            ContentType acceptType,
            TimeSpan timeout)
        {
            var httpVerbAsString = httpVerb.ToString().ToUpper();
            return Call<TResult>(
                uri,
                httpVerbAsString,
                body,
                cookieJar,
                headers,
                saveResponseHeadersAction,
                contentType,
                acceptType,
                timeout);
        }

        /// <summary>
        /// Makes a restful call using supplied information.
        /// </summary>
        /// <param name="uri">Uri to make the request against.</param>
        /// <param name="httpVerb">HTTP verb to use.</param>
        /// <param name="body">Optional body object to send (use null if not needed).</param>
        /// <param name="cookieJar">Optional cookie to use (use null if not needed).</param>
        /// <param name="headers">Optional headers to use (use null if not needed).</param>
        /// <param name="saveResponseHeadersAction">Optional action to use to save response headers (use null if not needed).</param>
        /// <param name="contentType">Content type to use for request.</param>
        /// <param name="acceptType">Content type to use for response.</param>
        /// <param name="timeout">Timeout to use.</param>
        /// <typeparam name="TResult">Return type to convert response to (if you provide VoidResultType then null will be returned - basically a void call).</typeparam>
        /// <returns>Converted response to the specified type.</returns>
        public static TResult Call<TResult>(
            Uri uri,
            string httpVerb,
            object body,
            CookieJar cookieJar,
            KeyValuePair<string, string>[] headers,
            Action<KeyValuePair<string, string>[]> saveResponseHeadersAction,
            ContentType contentType,
            ContentType acceptType,
            TimeSpan timeout)
        {
            if (contentType != ContentType.ApplicationJson)
            {
                throw new ArgumentException("ContentType: " + contentType + " not supported at this time.", "contentType");
            }

            if (acceptType != ContentType.ApplicationJson)
            {
                throw new ArgumentException("AcceptType: " + contentType + " not supported at this time.", "acceptType");
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

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri);
            req.CookieContainer = cookieContainer;
            req.ContentType = contentType.ToStringValue();
            req.Accept = acceptType.ToStringValue();
            req.Method = httpVerb;
            req.Timeout = (int)timeout.TotalMilliseconds;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    req.Headers.Add(header.Key, header.Value);
                }
            }

            string bodyAsString = null;
            if (contentType == ContentType.ApplicationJson && body != null)
            {
                bodyAsString = JsonConvert.SerializeObject(body);
            }

            if (httpVerb != HttpVerb.Get.ToString().ToUpper() && !string.IsNullOrEmpty(bodyAsString))
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

            TResult ret = default(TResult);
            if (typeof(TResult) == typeof(VoidResultType))
            {
                return ret; // this will just be null and should only be used when you don't want a return
            }
            else if (acceptType == ContentType.ApplicationJson)
            {
                ret = JsonConvert.DeserializeObject<TResult>(contents);
            }
            else
            {
                throw new ArgumentException("AcceptType: " + acceptType + " not supported at this time.", "acceptType");
            }

            var responseHeaders = responseHeadersRaw == null
                                      ? new KeyValuePair<string, string>[0]
                                      : responseHeadersRaw.ToKeyValuePairArray();
            if (saveResponseHeadersAction != null)
            {
                saveResponseHeadersAction(responseHeaders);
            }

            return ret;
        }

        /// <summary>
        /// Convert the enumeration value of Content Type to the appropriate string value.
        /// </summary>
        /// <param name="contentType">Enumeration content type.</param>
        /// <returns>Appropriate string value of the enumeration.</returns>
        public static string ToStringValue(this ContentType contentType)
        {
            switch (contentType)
            {
                case ContentType.ApplicationJson:
                    return "application/json";
                default:
                    throw new ArgumentException("Unsupported content type: " + contentType, "contentType");
            }
        }
    }

    /// <summary>
    /// Interface of the call for execute methods.
    /// </summary>
    public interface ICallOnUriVerb
    {
        /// <summary>
        /// Executes the chain as a GET without a response.
        /// </summary>
        void Get();

        /// <summary>
        /// Executes the chain as a GET with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult Get<TResult>();

        /// <summary>
        /// Executes the chain as a POST without a response.
        /// </summary>
        void Post();

        /// <summary>
        /// Executes the chain as a POST with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult Post<TResult>();

        /// <summary>
        /// Executes the chain as a PUT without a response.
        /// </summary>
        void Put();

        /// <summary>
        /// Executes the chain as a PUT with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult Put<TResult>();

        /// <summary>
        /// Executes the chain as a DELETE without a response.
        /// </summary>
        void Delete();

        /// <summary>
        /// Executes the chain as a DELETE with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult Delete<TResult>();

        /// <summary>
        /// Executes the chain using the specified verb without a response.
        /// </summary>
        /// <param name="httpVerb">Specified HTTP verb to use.</param>
        void CallWithVerb(string httpVerb);

        /// <summary>
        /// Executes the chain using the specified verb with a response to the provided type.
        /// </summary>
        /// <param name="httpVerb">Specified HTTP verb to use.</param>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult CallWithVerb<TResult>(string httpVerb);
    }

    /// <summary>
    /// Interface of the call for just the timeout.
    /// </summary>
    public interface ICallOnUriTimeout
    {
        /// <summary>
        /// Updates the timeout of the call.
        /// </summary>
        /// <param name="timeout">Timeout to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithTimeout(TimeSpan timeout);
    }

    /// <summary>
    /// Interface of the call for execute methods.
    /// </summary>
    public interface ICallOnUriResponseHeaderSaveAction
    {
        /// <summary>
        /// Save response headers by sending to an output action on execution.
        /// </summary>
        /// <param name="outputAction">Output parameter of the response headers.</param>
        /// <returns>Response headers as an array of key value pair elements.</returns>
        ICallOnUriAll WithResponseHeaderSaveAction(Action<KeyValuePair<string, string>[]> outputAction);
    }

    /// <summary>
    /// Interface of the call for just the headers.
    /// </summary>
    public interface ICallOnUriHeaders
    {
        /// <summary>
        /// Adds the headers to the set of headers to use in the call.
        /// </summary>
        /// <param name="name">Name of header to add to set.</param>
        /// <param name="value">Value of header to add to set.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithHeader(string name, string value);

        /// <summary>
        /// Adds the headers to the set of headers to use in the call.
        /// </summary>
        /// <param name="headers">Headers to add to call.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithHeaders(NameValueCollection headers);

        /// <summary>
        /// Adds the headers to the set of headers to use in the call.
        /// </summary>
        /// <param name="headers">Headers to add to call.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithHeaders(WebHeaderCollection headers);

        /// <summary>
        /// Adds the headers to the set of headers to use in the call.
        /// </summary>
        /// <param name="headers">Headers to add to call.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithHeaders(KeyValuePair<string, string>[] headers);
    }

    /// <summary>
    /// Interface of the call for just the cookie.
    /// </summary>
    public interface ICallOnUriCookie
    {
        /// <summary>
        /// Add a system net cookie to the call.
        /// </summary>
        /// <param name="cookie">Cookie to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithCookie(Cookie cookie);

        /// <summary>
        /// Add a system web HTTP cookie to the call.
        /// </summary>
        /// <param name="cookie">Cookie to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithCookie(HttpCookie cookie);
    }

    /// <summary>
    /// Interface of the call for just the body.
    /// </summary>
    public interface ICallOnUriBody
    {
        /// <summary>
        /// Updates the body of the call.
        /// </summary>
        /// <param name="body">Body to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithBody(object body);
    }

    /// <summary>
    /// Interface of the call for all methods.
    /// </summary>
    public interface ICallOnUriAll : ICallOnUriHeaders, ICallOnUriCookie, ICallOnUriTimeout, ICallOnUriBody, ICallOnUriResponseHeaderSaveAction, ICallOnUriVerb
    {
    }

    /// <inheritdoc />
    public class ImplementationForICallOnUriAll : ICallOnUriAll
    {
        private readonly IDictionary<string, bool> decoratorsCalled = new Dictionary<string, bool>();

        private readonly Uri uri;

        private readonly HeaderJar headerJar = new HeaderJar();

        private readonly CookieJar cookieJar = new CookieJar();

        private TimeSpan timeout;

        private object body;

        private Action<KeyValuePair<string, string>[]> saveResponseHeadersAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImplementationForICallOnUriAll"/> class.
        /// </summary>
        /// <param name="uri">Uri of the call.</param>
        public ImplementationForICallOnUriAll(Uri uri)
        {
            this.uri = uri;
            this.timeout = TimeSpan.FromSeconds(30);
        }

        /// <inheritdoc />
        public ICallOnUriAll WithBody(object bodyObject)
        {
            this.UpdateCallListThrowIfAlreadyCalled("WithBody");

            this.body = bodyObject;
            return this;
        }

        /// <inheritdoc />
        public ICallOnUriAll WithHeader(string name, string value)
        {
            this.headerJar.Add(name, value);
            return this;
        }

        /// <inheritdoc />
        public ICallOnUriAll WithHeaders(NameValueCollection headers)
        {
            this.headerJar.Add(headers);
            return this;
        }

        /// <inheritdoc />
        public ICallOnUriAll WithHeaders(WebHeaderCollection headers)
        {
            this.headerJar.Add(headers);
            return this;
        }

        /// <inheritdoc />
        public ICallOnUriAll WithHeaders(KeyValuePair<string, string>[] headers)
        {
            this.headerJar.Add(headers);
            return this;
        }

        /// <inheritdoc />
        public ICallOnUriAll WithCookie(Cookie cookie)
        {
            this.cookieJar.AddCookie(cookie);
            return this;
        }

        /// <inheritdoc />
        public ICallOnUriAll WithCookie(HttpCookie cookie)
        {
            this.cookieJar.AddCookie(cookie);
            return this;
        }

        /// <inheritdoc />
        public ICallOnUriAll WithTimeout(TimeSpan callTimeout)
        {
            this.UpdateCallListThrowIfAlreadyCalled("WithTimeout");

            this.timeout = callTimeout;
            return this;
        }

        /// <inheritdoc />
        public ICallOnUriAll WithResponseHeaderSaveAction(Action<KeyValuePair<string, string>[]> saveAction)
        {
            this.UpdateCallListThrowIfAlreadyCalled("WithResponseHeaderSaveAction");

            this.saveResponseHeadersAction = saveAction;
            return this;
        }

        /// <inheritdoc />
        public void Get()
        {
            Operator.Call<VoidResultType>(
                this.uri,
                HttpVerb.Get,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                ContentType.ApplicationJson,
                ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public TResult Get<TResult>()
        {
            return Operator.Call<TResult>(
                this.uri,
                HttpVerb.Get,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                ContentType.ApplicationJson,
                ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public void Post()
        {
            Operator.Call<VoidResultType>(
                this.uri,
                HttpVerb.Post,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                ContentType.ApplicationJson,
                ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public TResult Post<TResult>()
        {
            return Operator.Call<TResult>(
                this.uri,
                HttpVerb.Post,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                ContentType.ApplicationJson,
                ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public void Put()
        {
            Operator.Call<VoidResultType>(
                this.uri,
                HttpVerb.Put,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                ContentType.ApplicationJson,
                ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public TResult Put<TResult>()
        {
            return Operator.Call<TResult>(
                this.uri,
                HttpVerb.Put,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                ContentType.ApplicationJson,
                ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public void Delete()
        {
            Operator.Call<VoidResultType>(
                this.uri,
                HttpVerb.Delete,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                ContentType.ApplicationJson,
                ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public TResult Delete<TResult>()
        {
            return Operator.Call<TResult>(
                this.uri,
                HttpVerb.Delete,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                ContentType.ApplicationJson,
                ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public void CallWithVerb(string httpVerb)
        {
            Operator.Call<VoidResultType>(
                this.uri,
                httpVerb,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                ContentType.ApplicationJson,
                ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public TResult CallWithVerb<TResult>(string httpVerb)
        {
            return Operator.Call<TResult>(
                this.uri,
                httpVerb,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                ContentType.ApplicationJson,
                ContentType.ApplicationJson,
                this.timeout);
        }

        private void UpdateCallListThrowIfAlreadyCalled(string methodName)
        {
            bool keyExists;
            var callMade = this.decoratorsCalled.TryGetValue(methodName, out keyExists);
            if (keyExists && callMade)
            {
                throw new DuplicateCallUsingFluentGrammarException(
                    "Cannot call '" + methodName + "' twice, please update chain to only call once...");
            }
            else
            {
                this.decoratorsCalled.Add(methodName, true);
            }
        }
    }

    /// <summary>
    /// Class with extension methods for building a Uri using a fluent grammar.
    /// </summary>
    public static class UriExtensionMethodsForBuilding
    {
        /// <summary>
        /// Appends a path segment to the uri.
        /// </summary>
        /// <param name="uri">Uri to operate on.</param>
        /// <param name="pathSegment">Path segment to append.</param>
        /// <returns>New Uri with adjustments to the url.</returns>
        public static Uri AppendPathSegment(this Uri uri, string pathSegment)
        {
            var uriBuilder = new UriBuilder(uri);

            if (!uriBuilder.Path.EndsWith("/"))
            {
                uriBuilder.Path += "/";
            }

            uriBuilder.Path += pathSegment;

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Appends a query string parameter to the uri.
        /// </summary>
        /// <param name="uri">Uri to operate on.</param>
        /// <param name="name">Name of the query string parameter.</param>
        /// <param name="value">Value of the query string parameter.</param>
        /// <returns>New Uri with adjustments to the url.</returns>
        public static Uri AppendQueryStringParam(this Uri uri, string name, string value)
        {
            var list = new KeyValuePair<string, string>(name, value).ToSingleElementArray();
            return uri.AppendQueryStringParams(list);
        }

        /// <summary>
        /// Appends a set of query string parameters to the uri.
        /// </summary>
        /// <param name="uri">Uri to operate on.</param>
        /// <param name="queryStringParams">Query string parameters to add.</param>
        /// <returns>New Uri with adjustments to the url.</returns>
        public static Uri AppendQueryStringParams(this Uri uri, IDictionary<string, string> queryStringParams)
        {
            var list = queryStringParams.ToList();
            return uri.AppendQueryStringParams(list);
        }

        /// <summary>
        /// Appends a set of query string parameters to the uri.
        /// </summary>
        /// <param name="uri">Uri to operate on.</param>
        /// <param name="queryStringParams">Query string parameters to add.</param>
        /// <returns>New Uri with adjustments to the url.</returns>
        public static Uri AppendQueryStringParams(this Uri uri, ICollection<KeyValuePair<string, string>> queryStringParams)
        {
            var collection = HttpUtility.ParseQueryString(uri.Query);

            // add or updates key-value pair
            foreach (var item in queryStringParams)
            {
                collection.Set(item.Key, item.Value);
            }

            var builder = new StringBuilder();
            var isFirstItem = true;
            var separator = '&';

            foreach (var item in collection.ToKeyValuePairArray())
            {
                if (!isFirstItem)
                {
                    // don't need to separator on first item...
                    builder.Append(separator);
                }

                var valueToAppend = item.Key == null ? item.Value : item.Key + "=" + item.Value;
                builder.Append(valueToAppend);
                isFirstItem = false;
            }

            var queryString = builder.ToString();

            var ret = new UriBuilder(uri) { Query = queryString };

            return ret.Uri;
        }
    }

    /// <summary>
    /// Class of extension methods on the Uri object.
    /// </summary>
    public static class UriExtensionMethodsForCall
    {
        /// <summary>
        /// Add a system net cookie to the call.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <param name="cookie">Cookie to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        public static ICallOnUriAll WithCookie(this Uri uri, HttpCookie cookie)
        {
            return new ImplementationForICallOnUriAll(uri).WithCookie(cookie);
        }

        /// <summary>
        /// Add a system web HTTP cookie to the call.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <param name="cookie">Cookie to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        public static ICallOnUriAll WithCookie(this Uri uri, Cookie cookie)
        {
            return new ImplementationForICallOnUriAll(uri).WithCookie(cookie);
        }

        /// <summary>
        /// Updates the timeout of the call.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <param name="timeout">Timeout to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        public static ICallOnUriAll WithTimeout(this Uri uri, TimeSpan timeout)
        {
            return new ImplementationForICallOnUriAll(uri).WithTimeout(timeout);
        }

        /// <summary>
        /// Adds the headers to the set of headers to use in the call.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <param name="name">Name of header to add to set.</param>
        /// <param name="value">Value of header to add to set.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        public static ICallOnUriAll WithHeader(this Uri uri, string name, string value)
        {
            return new ImplementationForICallOnUriAll(uri).WithHeader(name, value);
        }

        /// <summary>
        /// Adds the headers to the set of headers to use in the call.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <param name="headers">Headers to add to call.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        public static ICallOnUriAll WithHeaders(this Uri uri, NameValueCollection headers)
        {
            return new ImplementationForICallOnUriAll(uri).WithHeaders(headers);
        }

        /// <summary>
        /// Adds the headers to the set of headers to use in the call.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <param name="headers">Headers to add to call.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        public static ICallOnUriAll WithHeaders(this Uri uri, WebHeaderCollection headers)
        {
            return new ImplementationForICallOnUriAll(uri).WithHeaders(headers);
        }

        /// <summary>
        /// Adds the headers to the set of headers to use in the call.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <param name="headers">Headers to add to call.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        public static ICallOnUriAll WithHeaders(this Uri uri, KeyValuePair<string, string>[] headers)
        {
            return new ImplementationForICallOnUriAll(uri).WithHeaders(headers);
        }

        /// <summary>
        /// Updates the body of the call.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <param name="body">Body to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        public static ICallOnUriAll WithBody(this Uri uri, object body)
        {
            return new ImplementationForICallOnUriAll(uri).WithBody(body);
        }

        /// <summary>
        /// Save response headers by sending to an output action on execution.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <param name="outputAction">Output parameter of the response headers.</param>
        /// <returns>Response headers as an array of key value pair elements.</returns>
        public static ICallOnUriAll WithResponseHeaderSaveAction(this Uri uri, Action<KeyValuePair<string, string>[]> outputAction)
        {
            return new ImplementationForICallOnUriAll(uri).WithResponseHeaderSaveAction(outputAction);
        }

        /// <summary>
        /// Executes the chain as a GET without a response.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        public static void Get(this Uri uri)
        {
            new ImplementationForICallOnUriAll(uri).Get();
        }

        /// <summary>
        /// Executes the chain as a GET with a response to the provided type.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        public static TResult Get<TResult>(this Uri uri)
        {
            return new ImplementationForICallOnUriAll(uri).Get<TResult>();
        }

        /// <summary>
        /// Executes the chain as a POST without a response.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        public static void Post(this Uri uri)
        {
            new ImplementationForICallOnUriAll(uri).Post();
        }

        /// <summary>
        /// Executes the chain as a POST with a response to the provided type.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        public static TResult Post<TResult>(this Uri uri)
        {
            return new ImplementationForICallOnUriAll(uri).Post<TResult>();
        }

        /// <summary>
        /// Executes the chain as a PUT without a response.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        public static void Put(this Uri uri)
        {
            new ImplementationForICallOnUriAll(uri).Put();
        }

        /// <summary>
        /// Executes the chain as a PUT with a response to the provided type.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        public static TResult Put<TResult>(this Uri uri)
        {
            return new ImplementationForICallOnUriAll(uri).Put<TResult>();
        }

        /// <summary>
        /// Executes the chain as a DELETE without a response.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        public static void Delete(this Uri uri)
        {
            new ImplementationForICallOnUriAll(uri).Delete();
        }

        /// <summary>
        /// Executes the chain as a DELETE with a response to the provided type.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        public static TResult Delete<TResult>(this Uri uri)
        {
            return new ImplementationForICallOnUriAll(uri).Delete<TResult>();
        }

        /// <summary>
        /// Executes the chain using the specified HTTP verb without a response.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <param name="httpVerb">Specified HTTP verb to use.</param>
        public static void CallWithVerb(this Uri uri, string httpVerb)
        {
            new ImplementationForICallOnUriAll(uri).CallWithVerb(httpVerb);
        }

        /// <summary>
        /// Executes the chain using the specified HTTP verb with a response to the provided type.
        /// </summary>
        /// <param name="uri">Uri (extension method variable) to use for chain.</param>
        /// <param name="httpVerb">Specified HTTP verb to use.</param>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        public static TResult CallWithVerb<TResult>(this Uri uri, string httpVerb)
        {
            return new ImplementationForICallOnUriAll(uri).CallWithVerb<TResult>(httpVerb);
        }
    }
}
