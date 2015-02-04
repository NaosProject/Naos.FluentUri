// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriExecute.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    /// <summary>
    /// Interface of the call for execute methods.
    /// </summary>
    public interface ICallOnUriExecute
    {
        /// <summary>
        /// Executes the chain without response.
        /// </summary>
        void Execute();

        /// <summary>
        /// Executes the chain with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult ExecuteForResult<TResult>() where TResult : class;
    }
}