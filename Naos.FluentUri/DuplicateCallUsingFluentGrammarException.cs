// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DuplicateCallUsingFluentGrammarException.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System;

    /// <summary>
    /// Exception object for duplicate calls to 
    /// </summary>
    public class DuplicateCallUsingFluentGrammarException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateCallUsingFluentGrammarException"/> class.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        public DuplicateCallUsingFluentGrammarException(string message) : base(message)
        {
        }
    }
}
