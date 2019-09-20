// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriHeaders.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;

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
}