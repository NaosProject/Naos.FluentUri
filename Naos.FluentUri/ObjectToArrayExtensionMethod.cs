// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectToArrayExtensionMethod.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    /// <summary>
    /// Extension methods to create a single item array from an object.
    /// </summary>
    public static class ObjectToArrayExtensionMethod
    {
        /// <summary>
        /// Converts the object into a single element array.
        /// </summary>
        /// <typeparam name="T">Type of the object being used.</typeparam>
        /// <param name="objectToEncapsulateInArray">Object that extension method operates on.</param>
        /// <returns>New array containing the single item.</returns>
        public static T[] ToSingleElementArray<T>(this T objectToEncapsulateInArray)
        {
            return new[] { objectToEncapsulateInArray };
        }
    }
}