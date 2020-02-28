// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriSerializer.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using OBeautifulCode.Serialization;

    /// <summary>
    /// Interface of the call for setting the serializer.
    /// </summary>
    public interface ICallOnUriSerializer
    {
        /// <summary>
        /// Updates the serializer of the call.
        /// </summary>
        /// <param name="serializer">Serializer to use.</param>
        /// <returns>Updated fluent grammar chain.</returns>
        ICallOnUriAll WithSerializer(IStringSerializeAndDeserialize serializer);
    }
}
