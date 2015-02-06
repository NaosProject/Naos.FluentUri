// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderJar.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;

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
}