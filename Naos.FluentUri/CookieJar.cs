// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CookieJar.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web;
    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Container to hold different types of cookies.
    /// </summary>
    public class CookieJar
    {
        private readonly List<Cookie> cookies = new List<Cookie>();

        /// <summary>
        /// Gets the cookies.
        /// </summary>
        /// <value>The cookies.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Want this to be an array.")]
        public Cookie[] Cookies => this.cookies.ToArray();

        /// <summary>
        /// Adds the cookie to the set of cookie to use for a request.
        /// </summary>
        /// <param name="cookie">Cookie to add to request.</param>
        public void AddCookie(Cookie cookie)
        {
            new { cookie }.AsArg().Must().NotBeNull();

            this.cookies.Add(cookie);
        }

        /// <summary>
        /// Adds the cookie to the set of cookie to use for a request.
        /// </summary>
        /// <param name="cookie">Cookie to add to request.</param>
        public void AddCookie(HttpCookie cookie)
        {
            new { cookie }.AsArg().Must().NotBeNull();

            this.cookies.Add(cookie.ToSystemNetCookie());
        }
    }
}
