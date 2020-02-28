// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriAll.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    /// <summary>
    /// Interface of the call for all methods.
    /// Implements the <see cref="Naos.FluentUri.ICallOnUriHeaders" />
    /// Implements the <see cref="Naos.FluentUri.ICallOnUriCookie" />
    /// Implements the <see cref="Naos.FluentUri.ICallOnUriTimeout" />
    /// Implements the <see cref="Naos.FluentUri.ICallOnUriBody" />
    /// Implements the <see cref="Naos.FluentUri.ICallOnUriResponseHeaderSaveAction" />
    /// Implements the <see cref="Naos.FluentUri.ICallOnUriVerb" />
    /// Implements the <see cref="Naos.FluentUri.ICallOnUriSerializer" />
    /// Implements the <see cref="Naos.FluentUri.ICallOnUriAcceptType" />
    /// Implements the <see cref="Naos.FluentUri.ICallOnUriContentType" />.
    /// </summary>
    /// <seealso cref="Naos.FluentUri.ICallOnUriHeaders" />
    /// <seealso cref="Naos.FluentUri.ICallOnUriCookie" />
    /// <seealso cref="Naos.FluentUri.ICallOnUriTimeout" />
    /// <seealso cref="Naos.FluentUri.ICallOnUriBody" />
    /// <seealso cref="Naos.FluentUri.ICallOnUriResponseHeaderSaveAction" />
    /// <seealso cref="Naos.FluentUri.ICallOnUriVerb" />
    /// <seealso cref="Naos.FluentUri.ICallOnUriSerializer" />
    /// <seealso cref="Naos.FluentUri.ICallOnUriAcceptType" />
    /// <seealso cref="Naos.FluentUri.ICallOnUriContentType" />
    public interface ICallOnUriAll : ICallOnUriHeaders, ICallOnUriCookie, ICallOnUriTimeout, ICallOnUriBody, ICallOnUriResponseHeaderSaveAction, ICallOnUriVerb, ICallOnUriSerializer, ICallOnUriAcceptType, ICallOnUriContentType
    {
    }
}
