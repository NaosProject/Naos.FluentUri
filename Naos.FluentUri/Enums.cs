// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enums.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System;

    /// <summary>
    /// Class to hold enumerations.
    /// </summary>
    public static class Enums
    {
        /// <summary>
        /// Verbs that can be used for HTTP calls.
        /// </summary>
        public enum HttpVerb
        {
            /// <summary>
            /// Get verb.
            /// </summary>
            Get, 

            /// <summary>
            /// Post verb.
            /// </summary>
            Post, 

            /// <summary>
            /// Put verb.
            /// </summary>
            Put, 

            /// <summary>
            /// Delete verb.
            /// </summary>
            Delete
        }

        /// <summary>
        /// Content type to use for HTTP calls.
        /// </summary>
        public enum ContentType
        {
            /// <summary>
            /// Application JSON type.
            /// </summary>
            ApplicationJson
        }
    }
}
