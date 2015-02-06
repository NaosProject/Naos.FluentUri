// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CookieJar.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;
    using System.Web;

    /// <summary>
    /// Container to hold different types of cookies.
    /// </summary>
    public class CookieJar
    {
        private readonly List<Cookie> cookies = new List<Cookie>();

        /// <summary>
        /// Gets the cookies.
        /// </summary>
        public Cookie[] Cookies
        {
            get
            {
                return this.cookies.ToArray();
            }
        }

        /// <summary>
        /// Adds the cookie to the set of cookie to use for a request.
        /// </summary>
        /// <param name="cookie">Cookie to add to request.</param>
        public void AddCookie(Cookie cookie)
        {
            this.cookies.Add(cookie);
        }
 
        /// <summary>
        /// Adds the cookie to the set of cookie to use for a request.
        /// </summary>
        /// <param name="cookie">Cookie to add to request.</param>
        public void AddCookie(HttpCookie cookie)
        {
            this.cookies.Add(cookie.ToSystemNetCookie());
        }
    }
}