// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriBody.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    /// <summary>
    /// Interface of the call for just the body.
    /// </summary>
    public interface ICallOnUriBody
    {
        /// <summary>
        /// Updates the body of the call.
        /// </summary>
        /// <param name="body">Body to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithBody(object body);
    }
}
