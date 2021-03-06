// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "member", Target = "Naos.FluentUri.Operator.#Call`1(System.Uri,System.String,System.Object,Naos.FluentUri.CookieJar,Naos.FluentUri.HeaderJar,System.Action`1<System.Collections.Generic.KeyValuePair`2<System.String,System.String>[]>,Naos.FluentUri.ContentType,Naos.FluentUri.ContentType,System.TimeSpan,OBeautifulCode.Serialization.IStringSerializeAndDeserialize)", Justification = "Inherently coupled top level call, by design.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Scope = "member", Target = "Naos.FluentUri.Operator.#Call`1(System.Uri,System.String,System.Object,Naos.FluentUri.CookieJar,Naos.FluentUri.HeaderJar,System.Action`1<System.Collections.Generic.KeyValuePair`2<System.String,System.String>[]>,Naos.FluentUri.ContentType,Naos.FluentUri.ContentType,System.TimeSpan,OBeautifulCode.Serialization.IStringSerializeAndDeserialize)", Justification = "Disposal is as expected.")]

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the
// Code Analysis results, point to "Suppress Message", and click
// "In Suppression File".
// You do not need to add suppressions to this file manually.