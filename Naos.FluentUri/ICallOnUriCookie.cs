// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriCookie.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System.Net;
    using System.Web;

    /// <summary>
    /// Interface of the call for just the cookie.
    /// </summary>
    public interface ICallOnUriCookie
    {
        /// <summary>
        /// Add a system net cookie to the call.
        /// </summary>
        /// <param name="cookie">Cookie to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithCookie(Cookie cookie);

        /// <summary>
        /// Add a system web HTTP cookie to the call.
        /// </summary>
        /// <param name="cookie">Cookie to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithCookie(HttpCookie cookie);
    }
}