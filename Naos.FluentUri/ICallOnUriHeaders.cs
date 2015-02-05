// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriHeaders.cs" company="Naos">
//   Copyright 2015 Naos
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
        /// Adds headers from a NameValueCollection to the call.
        /// </summary>
        /// <param name="headers">Headers to add to call.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithHeaders(NameValueCollection headers);

        /// <summary>
        /// Adds headers from a NameValueCollection to the call.
        /// </summary>
        /// <param name="headers">Headers to add to call.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithHeaders(WebHeaderCollection headers);

        /// <summary>
        /// Adds headers from a KeyValuePair string,string[] to the call.
        /// </summary>
        /// <param name="headers">Headers to add to call.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithHeaders(KeyValuePair<string, string>[] headers);
    }
}