// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VoidResultType.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    /// <summary>
    /// Type to use for generics that indicates the lack of a return.
    /// </summary>
    public class VoidResultType
    {
        private static readonly VoidResultType DefaultField = new VoidResultType();

        /// <summary>
        /// Gets the default object to use for return.
        /// </summary>
        public static VoidResultType Default
        {
            get
            {
                return DefaultField;
            }
        }
    }
}