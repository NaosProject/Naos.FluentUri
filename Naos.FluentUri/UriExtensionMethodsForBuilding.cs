// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UriExtensionMethodsForBuilding.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using OBeautifulCode.Validation.Recipes;

    /// <summary>
    /// Class with extension methods for building a Uri using a fluent grammar.
    /// </summary>
    public static class UriExtensionMethodsForBuilding
    {
        /// <summary>
        /// Appends a path segment to the uri.
        /// </summary>
        /// <param name="uri">Uri to operate on.</param>
        /// <param name="pathSegment">Path segment to append.</param>
        /// <returns>New Uri with adjustments to the url.</returns>
        public static Uri AppendPathSegment(this Uri uri, string pathSegment)
        {
            new { uri }.Must().NotBeNull();
            new { pathSegment }.Must().NotBeNull();

            var uriBuilder = new UriBuilder(uri);

            if (!uriBuilder.Path.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                uriBuilder.Path += "/";
            }

            uriBuilder.Path += pathSegment;

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Appends a query string parameter to the uri.
        /// </summary>
        /// <param name="uri">Uri to operate on.</param>
        /// <param name="name">Name of the query string parameter.</param>
        /// <param name="value">Value of the query string parameter.</param>
        /// <returns>New Uri with adjustments to the url.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Param", Justification = "Spelling/name is correct.")]
        public static Uri AppendQueryStringParam(this Uri uri, string name, string value)
        {
            new { uri }.Must().NotBeNull();
            new { value }.Must().NotBeNull();

            var list = new KeyValuePair<string, string>(name, value).ToSingleElementArray();
            return uri.AppendQueryStringParams(list);
        }

        /// <summary>
        /// Appends a set of query string parameters to the uri.
        /// </summary>
        /// <param name="uri">Uri to operate on.</param>
        /// <param name="queryStringParams">Query string parameters to add.</param>
        /// <returns>New Uri with adjustments to the url.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Params", Justification = "Spelling/name is correct.")]
        public static Uri AppendQueryStringParams(this Uri uri, IDictionary<string, string> queryStringParams)
        {
            new { uri }.Must().NotBeNull();
            new { queryStringParams }.Must().NotBeNull();

            var list = queryStringParams.ToList();
            return uri.AppendQueryStringParams(list);
        }

        /// <summary>
        /// Appends a set of query string parameters to the uri.
        /// </summary>
        /// <param name="uri">Uri to operate on.</param>
        /// <param name="queryStringParams">Query string parameters to add.</param>
        /// <returns>New Uri with adjustments to the url.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Params", Justification = "Spelling/name is correct.")]
        public static Uri AppendQueryStringParams(this Uri uri, ICollection<KeyValuePair<string, string>> queryStringParams)
        {
            new { uri }.Must().NotBeNull();
            new { queryStringParams }.Must().NotBeNull();

            var collection = HttpUtility.ParseQueryString(uri.Query);

            // add or updates key-value pair
            foreach (var item in queryStringParams)
            {
                collection.Set(item.Key, item.Value);
            }

            var builder         = new StringBuilder();
            var thisIsFirstItem = true;
            var separator       = '&';

            foreach (var item in collection.ToKeyValuePairArray())
            {
                if (!thisIsFirstItem)
                {
                    // don't need to separator on first item...
                    builder.Append(separator);
                }

                var valueToAppend = item.Key == null ? item.Value : item.Key + "=" + item.Value;
                builder.Append(valueToAppend);
                thisIsFirstItem = false;
            }

            var queryString = builder.ToString();

            var ret = new UriBuilder(uri) { Query = queryString };

            return ret.Uri;
        }
    }
}