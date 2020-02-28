// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImplementationForICallOnUriAll.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Web;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.Json;

    /// <summary>
    /// Class ImplementationForICallOnUriAll.
    /// Implements the <see cref="Naos.FluentUri.ICallOnUriAll" />.
    /// </summary>
    /// <seealso cref="Naos.FluentUri.ICallOnUriAll" />
    /// <inheritdoc />
    public class ImplementationForICallOnUriAll : ICallOnUriAll
    {
        private readonly IDictionary<string, bool> decoratorsCalled = new Dictionary<string, bool>();

        private readonly Uri uri;

        private readonly HeaderJar headerJar = new HeaderJar();

        private readonly CookieJar cookieJar = new CookieJar();

        private TimeSpan internalTimeout;

        private object internalBody;

        private Action<KeyValuePair<string, string>[]> saveResponseHeadersAction;

        private IStringSerializeAndDeserialize serializerForPostBodyAndResponse = new ObcJsonSerializer();

        private ContentType acceptTypeForCall = ContentType.ApplicationJson;

        private ContentType contentTypeForCall = ContentType.ApplicationJson;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImplementationForICallOnUriAll" /> class.
        /// </summary>
        /// <param name="uri">Uri of the call.</param>
        public ImplementationForICallOnUriAll(Uri uri)
        {
            new { uri }.AsArg().Must().NotBeNull();

            this.uri     = uri;
            this.internalTimeout = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// Withes the type of the accept.
        /// </summary>
        /// <param name="acceptType">Type of the accept.</param>
        /// <returns>ICallOnUriAll.</returns>
        /// <inheritdoc />
        public ICallOnUriAll WithAcceptType(ContentType acceptType)
        {
            this.acceptTypeForCall = acceptType;
            return this;
        }

        /// <summary>
        /// Withes the type of the content.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>ICallOnUriAll.</returns>
        /// <inheritdoc />
        public ICallOnUriAll WithContentType(ContentType contentType)
        {
            this.contentTypeForCall = contentType;
            return this;
        }

        /// <summary>
        /// Withes the serializer.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        /// <returns>ICallOnUriAll.</returns>
        /// <inheritdoc />
        public ICallOnUriAll WithSerializer(IStringSerializeAndDeserialize serializer)
        {
            new { serializer }.AsArg().Must().NotBeNull();

            this.UpdateCallListThrowIfAlreadyCalled(nameof(this.WithSerializer));

            this.serializerForPostBodyAndResponse = serializer;
            return this;
        }

        /// <summary>
        /// Withes the body.
        /// </summary>
        /// <param name="body">The body object.</param>
        /// <returns>ICallOnUriAll.</returns>
        /// <inheritdoc />
        public ICallOnUriAll WithBody(object body)
        {
            this.UpdateCallListThrowIfAlreadyCalled(nameof(this.WithBody));

            this.internalBody = body;
            return this;
        }

        /// <summary>
        /// Withes the header.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>ICallOnUriAll.</returns>
        /// <inheritdoc />
        public ICallOnUriAll WithHeader(string name, string value)
        {
            new { name }.AsArg().Must().NotBeNullNorWhiteSpace();
            new { value }.AsArg().Must().NotBeNullNorWhiteSpace();

            this.headerJar.Add(name, value);
            return this;
        }

        /// <summary>
        /// Withes the headers.
        /// </summary>
        /// <param name="headers">The headers.</param>
        /// <returns>ICallOnUriAll.</returns>
        /// <inheritdoc />
        public ICallOnUriAll WithHeaders(NameValueCollection headers)
        {
            new { headers }.AsArg().Must().NotBeNull();

            this.headerJar.Add(headers);
            return this;
        }

        /// <summary>
        /// Withes the headers.
        /// </summary>
        /// <param name="headers">The headers.</param>
        /// <returns>ICallOnUriAll.</returns>
        /// <inheritdoc />
        public ICallOnUriAll WithHeaders(WebHeaderCollection headers)
        {
            new { headers }.AsArg().Must().NotBeNull();

            this.headerJar.Add(headers);
            return this;
        }

        /// <summary>
        /// Withes the headers.
        /// </summary>
        /// <param name="headers">The headers.</param>
        /// <returns>ICallOnUriAll.</returns>
        /// <inheritdoc />
        public ICallOnUriAll WithHeaders(KeyValuePair<string, string>[] headers)
        {
            new { headers }.AsArg().Must().NotBeNull();

            this.headerJar.Add(headers);
            return this;
        }

        /// <summary>
        /// Withes the cookie.
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        /// <returns>ICallOnUriAll.</returns>
        /// <inheritdoc />
        public ICallOnUriAll WithCookie(Cookie cookie)
        {
            new { cookie }.AsArg().Must().NotBeNull();

            this.cookieJar.AddCookie(cookie);
            return this;
        }

        /// <summary>
        /// Withes the cookie.
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        /// <returns>ICallOnUriAll.</returns>
        /// <inheritdoc />
        public ICallOnUriAll WithCookie(HttpCookie cookie)
        {
            new { cookie }.AsArg().Must().NotBeNull();

            this.cookieJar.AddCookie(cookie);
            return this;
        }

        /// <summary>
        /// Withes the timeout.
        /// </summary>
        /// <param name="timeout">The call timeout.</param>
        /// <returns>ICallOnUriAll.</returns>
        /// <inheritdoc />
        public ICallOnUriAll WithTimeout(TimeSpan timeout)
        {
            this.UpdateCallListThrowIfAlreadyCalled(nameof(this.WithTimeout));

            this.internalTimeout = timeout;
            return this;
        }

        /// <summary>
        /// Withes the response header save action.
        /// </summary>
        /// <param name="saveAction">The save action.</param>
        /// <returns>ICallOnUriAll.</returns>
        /// <inheritdoc />
        public ICallOnUriAll WithResponseHeaderSaveAction(Action<KeyValuePair<string, string>[]> saveAction)
        {
            new { saveAction }.AsArg().Must().NotBeNull();

            this.UpdateCallListThrowIfAlreadyCalled(nameof(this.WithResponseHeaderSaveAction));

            this.saveResponseHeadersAction = saveAction;
            return this;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <inheritdoc />
        public void Get()
        {
            Operator.Call<VoidResultType>(
                this.uri,
                HttpVerb.Get,
                this.internalBody,
                this.cookieJar,
                this.headerJar,
                this.saveResponseHeadersAction,
                this.contentTypeForCall,
                this.acceptTypeForCall,
                this.internalTimeout,
                this.serializerForPostBodyAndResponse);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <returns>TResult.</returns>
        /// <inheritdoc />
        public TResult Get<TResult>()
            where TResult : class
        {
            return Operator.Call<TResult>(
                this.uri,
                HttpVerb.Get,
                this.internalBody,
                this.cookieJar,
                this.headerJar,
                this.saveResponseHeadersAction,
                this.contentTypeForCall,
                this.acceptTypeForCall,
                this.internalTimeout,
                this.serializerForPostBodyAndResponse);
        }

        /// <summary>
        /// Posts this instance.
        /// </summary>
        /// <inheritdoc />
        public void Post()
        {
            Operator.Call<VoidResultType>(
                this.uri,
                HttpVerb.Post,
                this.internalBody,
                this.cookieJar,
                this.headerJar,
                this.saveResponseHeadersAction,
                this.contentTypeForCall,
                this.acceptTypeForCall,
                this.internalTimeout,
                this.serializerForPostBodyAndResponse);
        }

        /// <summary>
        /// Posts this instance.
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <returns>TResult.</returns>
        /// <inheritdoc />
        public TResult Post<TResult>()
            where TResult : class
        {
            return Operator.Call<TResult>(
                this.uri,
                HttpVerb.Post,
                this.internalBody,
                this.cookieJar,
                this.headerJar,
                this.saveResponseHeadersAction,
                this.contentTypeForCall,
                this.acceptTypeForCall,
                this.internalTimeout,
                this.serializerForPostBodyAndResponse);
        }

        /// <summary>
        /// Puts this instance.
        /// </summary>
        /// <inheritdoc />
        public void Put()
        {
            Operator.Call<VoidResultType>(
                this.uri,
                HttpVerb.Put,
                this.internalBody,
                this.cookieJar,
                this.headerJar,
                this.saveResponseHeadersAction,
                this.contentTypeForCall,
                this.acceptTypeForCall,
                this.internalTimeout,
                this.serializerForPostBodyAndResponse);
        }

        /// <summary>
        /// Puts this instance.
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <returns>TResult.</returns>
        /// <inheritdoc />
        public TResult Put<TResult>()
            where TResult : class
        {
            return Operator.Call<TResult>(
                this.uri,
                HttpVerb.Put,
                this.internalBody,
                this.cookieJar,
                this.headerJar,
                this.saveResponseHeadersAction,
                this.contentTypeForCall,
                this.acceptTypeForCall,
                this.internalTimeout,
                this.serializerForPostBodyAndResponse);
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <inheritdoc />
        public void Delete()
        {
            Operator.Call<VoidResultType>(
                this.uri,
                HttpVerb.Delete,
                this.internalBody,
                this.cookieJar,
                this.headerJar,
                this.saveResponseHeadersAction,
                this.contentTypeForCall,
                this.acceptTypeForCall,
                this.internalTimeout,
                this.serializerForPostBodyAndResponse);
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <returns>TResult.</returns>
        /// <inheritdoc />
        public TResult Delete<TResult>()
            where TResult : class
        {
            return Operator.Call<TResult>(
                this.uri,
                HttpVerb.Delete,
                this.internalBody,
                this.cookieJar,
                this.headerJar,
                this.saveResponseHeadersAction,
                this.contentTypeForCall,
                this.acceptTypeForCall,
                this.internalTimeout,
                this.serializerForPostBodyAndResponse);
        }

        /// <summary>
        /// Calls the with verb.
        /// </summary>
        /// <param name="httpVerb">The HTTP verb.</param>
        /// <inheritdoc />
        public void CallWithVerb(string httpVerb)
        {
            new { httpVerb }.AsArg().Must().NotBeNullNorWhiteSpace();

            Operator.Call<VoidResultType>(
                this.uri,
                httpVerb,
                this.internalBody,
                this.cookieJar,
                this.headerJar,
                this.saveResponseHeadersAction,
                this.contentTypeForCall,
                this.acceptTypeForCall,
                this.internalTimeout,
                this.serializerForPostBodyAndResponse);
        }

        /// <summary>
        /// Calls the with verb.
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <param name="httpVerb">The HTTP verb.</param>
        /// <returns>TResult.</returns>
        /// <inheritdoc />
        public TResult CallWithVerb<TResult>(string httpVerb)
            where TResult : class
        {
            new { httpVerb }.AsArg().Must().NotBeNullNorWhiteSpace();

            return Operator.Call<TResult>(
                this.uri,
                httpVerb,
                this.internalBody,
                this.cookieJar,
                this.headerJar,
                this.saveResponseHeadersAction,
                this.contentTypeForCall,
                this.acceptTypeForCall,
                this.internalTimeout,
                this.serializerForPostBodyAndResponse);
        }

        private void UpdateCallListThrowIfAlreadyCalled(string methodName)
        {
            var callMade = this.decoratorsCalled.TryGetValue(methodName, out var keyExists);
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
}
