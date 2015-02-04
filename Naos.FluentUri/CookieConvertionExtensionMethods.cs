// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CookieConvertionExtensionMethods.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System.Net;
    using System.Web;

    /// <summary>
    /// Extension methods to convert the two types of cookies.
    /// </summary>
    public static class CookieConvertionExtensionMethods
    {
        /// <summary>
        /// Extension method to convert a System.Web.HttpCookie to a System.Net.Cookie object.
        /// </summary>
        /// <param name="cookie">HttpCookie to translate.</param>
        /// <returns>Translated Cookie.</returns>
        public static Cookie ToSystemNetCookie(this HttpCookie cookie)
        {
            var ret = new Cookie
            {
                Domain = cookie.Domain,
                Expires = cookie.Expires,
                Name = cookie.Name,
                Path = cookie.Path,
                Secure = cookie.Secure,
                Value = cookie.Value,
            };

            return ret;
        }

        /// <summary>
        /// Extension method to convert a System.Net.Cookie to a System.Web.HttpCookie object.
        /// </summary>
        /// <param name="cookie">Cookie to translate.</param>
        /// <returns>Translated HttpCookie.</returns>
        public static HttpCookie ToSystemWebHttpCookie(this Cookie cookie)
        {
            var ret = new HttpCookie(cookie.Name)
            {
                Domain = cookie.Domain,
                Expires = cookie.Expires,
                Name = cookie.Name,
                Path = cookie.Path,
                Secure = cookie.Secure,
                Value = cookie.Value,
            };

            return ret;
        }
    }
}
