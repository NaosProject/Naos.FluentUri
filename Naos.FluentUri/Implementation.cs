// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Implementation.cs" company="Naos">
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

    /// <inheritdoc />
    public class Implementation : ICallOnUriAll
    {
        private readonly IDictionary<string, bool> decoratorsCalled = new Dictionary<string, bool>(); 

        private readonly Uri uri;

        private readonly HeaderJar headerJar = new HeaderJar();

        private readonly CookieJar cookieJar = new CookieJar();

        private TimeSpan timeout;

        private object body;

        private Action<KeyValuePair<string, string>[]> saveResponseHeadersAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="Implementation"/> class.
        /// </summary>
        /// <param name="uri">Uri of the call.</param>
        public Implementation(Uri uri)
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
                Enums.HttpVerb.Get,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                Enums.ContentType.ApplicationJson,
                Enums.ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public TResult Get<TResult>()
        {
            return Operator.Call<TResult>(
                this.uri,
                Enums.HttpVerb.Get,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                Enums.ContentType.ApplicationJson,
                Enums.ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public void Post()
        {
            Operator.Call<VoidResultType>(
                this.uri,
                Enums.HttpVerb.Post,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                Enums.ContentType.ApplicationJson,
                Enums.ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public TResult Post<TResult>()
        {
            return Operator.Call<TResult>(
                this.uri,
                Enums.HttpVerb.Post,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                Enums.ContentType.ApplicationJson,
                Enums.ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public void Put()
        {
            Operator.Call<VoidResultType>(
                this.uri,
                Enums.HttpVerb.Put,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                Enums.ContentType.ApplicationJson,
                Enums.ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public TResult Put<TResult>()
        {
            return Operator.Call<TResult>(
                this.uri,
                Enums.HttpVerb.Put,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                Enums.ContentType.ApplicationJson,
                Enums.ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public void Delete()
        {
            Operator.Call<VoidResultType>(
                this.uri,
                Enums.HttpVerb.Delete,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                Enums.ContentType.ApplicationJson,
                Enums.ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public TResult Delete<TResult>()
        {
            return Operator.Call<TResult>(
                this.uri,
                Enums.HttpVerb.Delete,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                Enums.ContentType.ApplicationJson,
                Enums.ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public void UsingVerb(string httpVerb)
        {
            Operator.Call<VoidResultType>(
                this.uri,
                httpVerb,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                Enums.ContentType.ApplicationJson,
                Enums.ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public TResult UsingVerb<TResult>(string httpVerb)
        {
            return Operator.Call<TResult>(
                this.uri,
                httpVerb,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                Enums.ContentType.ApplicationJson,
                Enums.ContentType.ApplicationJson,
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
}