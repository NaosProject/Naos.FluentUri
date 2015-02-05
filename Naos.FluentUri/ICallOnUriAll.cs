// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICallOnUriAll.cs" company="Naos">
//   Copyright 2015 Naos
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.FluentUri
{
    /// <summary>
    /// Interface of the call for all methods.
    /// </summary>
    public interface ICallOnUriAll : ICallOnUriHeaders, ICallOnUriCookie, ICallOnUriTimeout, ICallOnUriBody, ICallOnUriResponseHeaderSaveAction, ICallOnUriCall
    {
    }
}