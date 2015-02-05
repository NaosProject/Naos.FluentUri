// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriCall.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    /// <summary>
    /// Interface of the call for execute methods.
    /// </summary>
    public interface ICallOnUriCall
    {
        /// <summary>
        /// Executes the chain without response.
        /// </summary>
        void Call();

        /// <summary>
        /// Executes the chain with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult Call<TResult>();
    }
}