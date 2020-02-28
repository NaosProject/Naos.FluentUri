// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DuplicateCallUsingFluentGrammarException.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    using System;

    /// <summary>
    /// Exception object for duplicate calls in chain.
    /// Implements the <see cref="Exception" />.
    /// </summary>
    /// <seealso cref="Exception" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "CallUsing", Justification = "Spelling/name is correct.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "Skipping for now.")]
    [Serializable]
    public class DuplicateCallUsingFluentGrammarException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateCallUsingFluentGrammarException" /> class.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        public DuplicateCallUsingFluentGrammarException(
            string message)
            : base(message)
        {
        }
    }
}
