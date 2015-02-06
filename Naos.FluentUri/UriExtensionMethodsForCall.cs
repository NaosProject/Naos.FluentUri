// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UriExtensionMethodsForCall.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Web;

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
