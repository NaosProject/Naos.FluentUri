// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriVerb.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    /// <summary>
    /// Interface of the call for execute methods.
    /// </summary>
    public interface ICallOnUriVerb
    {
        /// <summary>
        /// Executes the chain as a GET without a response.
        /// </summary>
        void Get();

        /// <summary>
        /// Executes the chain as a GET with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult Get<TResult>();

        /// <summary>
        /// Executes the chain as a POST without a response.
        /// </summary>
        void Post();

        /// <summary>
        /// Executes the chain as a POST with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult Post<TResult>();

        /// <summary>
        /// Executes the chain as a PUT without a response.
        /// </summary>
        void Put();

        /// <summary>
        /// Executes the chain as a PUT with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult Put<TResult>();

        /// <summary>
        /// Executes the chain as a DELETE without a response.
        /// </summary>
        void Delete();

        /// <summary>
        /// Executes the chain as a DELETE with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult Delete<TResult>();

        /// <summary>
        /// Executes the chain using the specified verb without a response.
        /// </summary>
        /// <param name="httpVerb">Specified HTTP verb to use.</param>
        void CallWithVerb(string httpVerb);

        /// <summary>
        /// Executes the chain using the specified verb with a response to the provided type.
        /// </summary>
        /// <param name="httpVerb">Specified HTTP verb to use.</param>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult CallWithVerb<TResult>(string httpVerb);
    }
}