// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CookieJar.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System.Net;
    using System.Web;

    /// <summary>
    /// Container to hold different types of cookies.
    /// </summary>
    public class CookieJar
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CookieJar"/> class.
        /// </summary>
        /// <param name="cookie">Cookie to hold.</param>
        public CookieJar(HttpCookie cookie)
        {
            this.SystemWebHttpCookie = cookie;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CookieJar"/> class.
        /// </summary>
        /// <param name="cookie">Cookie to hold.</param>
        public CookieJar(Cookie cookie)
        {
            this.SystemNetCookie = cookie;
        }

        /// <summary>
        /// Gets the System Web Http Cookie.
        /// </summary>
        public HttpCookie SystemWebHttpCookie { get; private set; }

        /// <summary>
        /// Gets the System Web Http Cookie.
        /// </summary>
        public Cookie SystemNetCookie { get; private set; }
    }
}