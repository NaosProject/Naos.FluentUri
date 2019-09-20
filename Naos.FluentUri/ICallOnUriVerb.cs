// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriVerb.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Spelling/name is correct.")]
        void Get();

        /// <summary>
        /// Executes the chain as a GET with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Spelling/name is correct.")]
        TResult Get<TResult>()
            where TResult : class;

        /// <summary>
        /// Executes the chain as a POST without a response.
        /// </summary>
        void Post();

        /// <summary>
        /// Executes the chain as a POST with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult Post<TResult>()
            where TResult : class;

        /// <summary>
        /// Executes the chain as a PUT without a response.
        /// </summary>
        void Put();

        /// <summary>
        /// Executes the chain as a PUT with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult Put<TResult>()
            where TResult : class;

        /// <summary>
        /// Executes the chain as a DELETE without a response.
        /// </summary>
        void Delete();

        /// <summary>
        /// Executes the chain as a DELETE with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <returns>Converted output from the call.</returns>
        TResult Delete<TResult>()
            where TResult : class;

        /// <summary>
        /// Executes the chain using the specified verb without a response.
        /// </summary>
        /// <param name="httpVerb">Specified HTTP verb to use.</param>
        void CallWithVerb(string httpVerb);

        /// <summary>
        /// Executes the chain using the specified verb with a response to the provided type.
        /// </summary>
        /// <typeparam name="TResult">Type to convert the response to.</typeparam>
        /// <param name="httpVerb">Specified HTTP verb to use.</param>
        /// <returns>Converted output from the call.</returns>
        TResult CallWithVerb<TResult>(string httpVerb)
            where TResult : class;
    }
}