// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderJar.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// Container to hold different types of headers.
    /// </summary>
    public class HeaderJar
    {
        private readonly List<KeyValuePair<string, string>> headerCollection = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>The headers.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Want this to be an array.")]
        public KeyValuePair<string, string>[] Headers => this.headerCollection.ToArray();

        /// <summary>
        /// Adds the headers to the set of headers to use in the request.
        /// </summary>
        /// <param name="headers">Headers to add to set.</param>
        public void Add(NameValueCollection headers)
        {
            new { headers }.Must().NotBeNull();

            var transformedHeaders = headers.ToKeyValuePairArray();
            this.headerCollection.AddRange(transformedHeaders);
        }

        /// <summary>
        /// Adds the headers to the set of headers to use in the request.
        /// </summary>
        /// <param name="headers">Headers to add to set.</param>
        public void Add(WebHeaderCollection headers)
        {
            new { headers }.Must().NotBeNull();

            var transformedHeaders = headers.ToKeyValuePairArray();
            this.headerCollection.AddRange(transformedHeaders);
        }

        /// <summary>
        /// Adds the headers to the set of headers to use in the request.
        /// </summary>
        /// <param name="headers">Headers to add to set.</param>
        public void Add(KeyValuePair<string, string>[] headers)
        {
            new { headers }.Must().NotBeNull();

            this.headerCollection.AddRange(headers);
        }

        /// <summary>
        /// Adds the headers to the set of headers to use in the request.
        /// </summary>
        /// <param name="name">Name of header to add to set.</param>
        /// <param name="value">Value of header to add to set.</param>
        public void Add(string name, string value)
        {
            new { name }.Must().NotBeNullNorWhiteSpace();
            new { value }.Must().NotBeNullNorWhiteSpace();

            this.headerCollection.Add(new KeyValuePair<string, string>(name, value));
        }
    }
}