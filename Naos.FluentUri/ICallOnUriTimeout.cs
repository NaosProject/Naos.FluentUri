// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriTimeout.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System;

    /// <summary>
    /// Interface of the call for just the timeout.
    /// </summary>
    public interface ICallOnUriTimeout
    {
        /// <summary>
        /// Updates the timeout of the call.
        /// </summary>
        /// <param name="timeout">Timeout to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithTimeout(TimeSpan timeout);
    }
}
