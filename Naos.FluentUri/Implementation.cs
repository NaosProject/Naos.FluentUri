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

        private readonly Enums.HttpVerb httpVerb;

        private HeaderJar headerJar = new HeaderJar();

        private CookieJar cookieJar;

        private TimeSpan timeout;

        private object body;

        private Action<KeyValuePair<string, string>[]> saveResponseHeadersAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="Implementation"/> class.
        /// </summary>
        /// <param name="uri">Uri of the call.</param>
        /// <param name="httpVerb">HttpVerb of the call</param>
        public Implementation(Uri uri, Enums.HttpVerb httpVerb)
        {
            this.uri = uri;
            this.httpVerb = httpVerb;
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
        public ICallOnUriAll WithHeaders(NameValueCollection headers)
        {
            this.UpdateCallListThrowIfAlreadyCalled("WithHeaders");

            this.headerJar = new HeaderJar(headers);
            return this;
        }

        /// <inheritdoc />
        public ICallOnUriAll WithHeaders(WebHeaderCollection headers)
        {
            this.UpdateCallListThrowIfAlreadyCalled("WithHeaders");

            this.headerJar = new HeaderJar(headers);
            return this;
        }

        /// <inheritdoc />
        public ICallOnUriAll WithHeaders(KeyValuePair<string, string>[] headers)
        {
            this.UpdateCallListThrowIfAlreadyCalled("WithHeaders");

            this.headerJar = new HeaderJar(headers);
            return this;
        }

        /// <inheritdoc />
        public ICallOnUriAll WithCookie(Cookie cookie)
        {
            this.UpdateCallListThrowIfAlreadyCalled("WithCookie");

            this.cookieJar = new CookieJar(cookie);
            return this;
        }

        /// <inheritdoc />
        public ICallOnUriAll WithCookie(HttpCookie cookie)
        {
            this.UpdateCallListThrowIfAlreadyCalled("WithCookie");

            this.cookieJar = new CookieJar(cookie);
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
        public void Call()
        {
            Operator.Call<VoidResultType>(
                this.uri,
                this.httpVerb,
                this.body,
                this.cookieJar,
                this.headerJar.Headers,
                this.saveResponseHeadersAction,
                Enums.ContentType.ApplicationJson,
                Enums.ContentType.ApplicationJson,
                this.timeout);
        }

        /// <inheritdoc />
        public TResult Call<TResult>()
        {
            return Operator.Call<TResult>(
                this.uri,
                this.httpVerb,
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