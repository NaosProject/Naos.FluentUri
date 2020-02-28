// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VoidResultType.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    /// <summary>
    /// Type to use for generics that indicates the lack of a return.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors", Justification = "Need to be able to new up the type for serialization.")]
    public class VoidResultType
    {
        /// <summary>
        /// Gets the default object to use for return.
        /// </summary>
        /// <value>The default.</value>
        public static VoidResultType Default { get; } = new VoidResultType();
    }
}
