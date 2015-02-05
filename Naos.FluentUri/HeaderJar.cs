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
        private readonly WebHeaderCollection webHeaderCollectionHeaders;

        private readonly NameValueCollection nameValueCollectionHeaders;

        private readonly KeyValuePair<string, string>[] keyValuePairArrayHeaders;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderJar"/> class.
        /// </summary>
        public HeaderJar()
        {
            // Deliberate no-op
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderJar"/> class.
        /// </summary>
        /// <param name="headers">Headers to hold.</param>
        public HeaderJar(NameValueCollection headers)
        {
            this.nameValueCollectionHeaders = headers;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderJar"/> class.
        /// </summary>
        /// <param name="headers">Headers to hold.</param>
        public HeaderJar(WebHeaderCollection headers)
        {
            this.webHeaderCollectionHeaders = headers;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderJar"/> class.
        /// </summary>
        /// <param name="headers">Headers to hold.</param>
        public HeaderJar(KeyValuePair<string, string>[] headers)
        {
            this.keyValuePairArrayHeaders = headers;
        }

        /// <summary>
        /// Gets the headers.
        /// </summary>
        public KeyValuePair<string, string>[] Headers
        {
            get
            {
                if (this.nameValueCollectionHeaders != null)
                {
                    return this.nameValueCollectionHeaders.ToKeyValuePairArray();
                }
                else if (this.webHeaderCollectionHeaders != null)
                {
                    return this.webHeaderCollectionHeaders.ToKeyValuePairArray();
                }
                else if (this.keyValuePairArrayHeaders != null)
                {
                    return this.keyValuePairArrayHeaders;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}