// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriAcceptType.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    /// <summary>
    /// Interface of the call for just the accept type.
    /// </summary>
    public interface ICallOnUriAcceptType
    {
        /// <summary>
        /// Updates the accept type of the call.
        /// </summary>
        /// <param name="acceptType">Accept type to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithAcceptType(ContentType acceptType);
    }
}
