// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UriExtensionMethods.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System;

    /// <summary>
    /// Class of extension methods on the Uri object.
    /// </summary>
    public static class UriExtensionMethods
    {
        /// <summary>
        /// Main entry point for FluentUri to make a Get REST call.
        /// </summary>
        /// <param name="uri">Uri of the resource to call.</param>
        /// <returns>Fluent grammar chain to work with.</returns>
        public static ICallOnUriAll Get(this Uri uri)
        {
            return new Implementation(uri, Enums.HttpVerb.Get);
        }

        /// <summary>
        /// Main entry point for FluentUri to make a Post REST call.
        /// </summary>
        /// <param name="uri">Uri of the resource to call.</param>
        /// <returns>Fluent grammar chain to work with.</returns>
        public static ICallOnUriAll Post(this Uri uri)
        {
            return new Implementation(uri, Enums.HttpVerb.Post);
        }

        /// <summary>
        /// Main entry point for FluentUri to make a Put REST call.
        /// </summary>
        /// <param name="uri">Uri of the resource to call.</param>
        /// <returns>Fluent grammar chain to work with.</returns>
        public static ICallOnUriAll Put(this Uri uri)
        {
            return new Implementation(uri, Enums.HttpVerb.Put);
        }

        /// <summary>
        /// Main entry point for FluentUri to make a Delete REST call.
        /// </summary>
        /// <param name="uri">Uri of the resource to call.</param>
        /// <returns>Fluent grammar chain to work with.</returns>
        public static ICallOnUriAll Delete(this Uri uri)
        {
            return new Implementation(uri, Enums.HttpVerb.Delete);
        }
    }
}
