// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriResponseHeaderSaveAction.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface of the call for execute methods.
    /// </summary>
    public interface ICallOnUriResponseHeaderSaveAction
    {
        /// <summary>
        /// Save response headers by sending to an output action on execution.
        /// </summary>
        /// <param name="outputAction">Output parameter of the response headers.</param>
        /// <returns>Response headers as an array of key value pair elements.</returns>
        ICallOnUriAll WithResponseHeaderSaveAction(Action<KeyValuePair<string, string>[]> outputAction);
    }
}