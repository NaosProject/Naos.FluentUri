// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderConvertionExtensionMethods.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;

    /// <summary>
    /// Utility methods to convert between different types of header collections.
    /// </summary>
    public static class HeaderConvertionExtensionMethods
    {
        /// <summary>
        /// Gets the headers collection as a key value pair array.
        /// </summary>
        /// <param name="headers">Headers as a name value collection.</param>
        /// <returns>Array of key value pairs of the header name and value.</returns>
        public static KeyValuePair<string, string>[] ToKeyValuePairArray(this NameValueCollection headers)
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
