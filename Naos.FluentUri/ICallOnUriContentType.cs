// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriContentType.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    /// <summary>
    /// Interface of the call for just the content type.
    /// </summary>
    public interface ICallOnUriContentType
    {
        /// <summary>
        /// Updates the content type of the call.
        /// </summary>
        /// <param name="contentType">Content type to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithContentType(ContentType contentType);
    }
}