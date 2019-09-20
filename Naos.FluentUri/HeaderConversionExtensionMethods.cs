// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderConversionExtensionMethods.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// Utility methods to convert between different types of header collections.
    /// </summary>
    public static class HeaderConversionExtensionMethods
    {
        /// <summary>
        /// Gets the header in question if present (requires second level check...).
        /// </summary>
        /// <param name="headers">The headers set to look at.</param>
        /// <param name="name">The header name to find.</param>
        /// <returns>The value of the specified key if present.</returns>
        public static string GetHeaderByName(this NameValueCollection headers, string name)
        {
            var values = headers?.GetValues(name);

            return values?.FirstOrDefault();
        }

        /// <summary>
        /// Gets the header in question if present (requires second level check...).
        /// </summary>
        /// <param name="headers">The headers set to look at.</param>
        /// <param name="name">The header name to find.</param>
        /// <returns>The value of the specified key if present.</returns>
        public static string GetHeaderByName(this WebHeaderCollection headers, string name)
        {
            var values = headers?.GetValues(name);

            return values?.FirstOrDefault();
        }

        /// <summary>
        /// Gets the collection as a key value pair array.
        /// </summary>
        /// <param name="collection">Collection of items as a name value collection.</param>
        /// <returns>Array of key value pairs of the items name and value.</returns>
        public static KeyValuePair<string, string>[] ToKeyValuePairArray(this NameValueCollection collection)
        {
            var ret = new List<KeyValuePair<string, string>>();
            if (collection != null)
            {
                foreach (var key in collection.AllKeys)
                {
                    var values = collection.GetValues(key);
                    if (values != null)
                    {
                        foreach (var value in values)
                        {
                            ret.Add(new KeyValuePair<string, string>(key, value));
                        }
                    }
                }
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Gets the headers collection as a key value pair array.
        /// </summary>
        /// <param name="headers">Headers as a web header collection.</param>
        /// <returns>Array of key value pairs of the header name and value.</returns>
        public static KeyValuePair<string, string>[] ToKeyValuePairArray(this WebHeaderCollection headers)
        {
            var ret = new List<KeyValuePair<string, string>>();
            if (headers != null)
            {
                foreach (var key in headers.AllKeys)
                {
                    var values = headers.GetValues(key);
                    if (values != null)
                    {
                        foreach (var value in values)
                        {
                            ret.Add(new KeyValuePair<string, string>(key, value));
                        }
                    }
                }
            }

            return ret.ToArray();
        }
    }
}