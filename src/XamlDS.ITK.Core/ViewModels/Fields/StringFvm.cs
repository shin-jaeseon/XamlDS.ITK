// <copyright file="StringFvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>

namespace XamlDS.ITK.ViewModels.Fields;

/// <summary>
/// Represents a string field ViewModel.
/// </summary>
/// <remarks>
/// Inherits <see cref="FieldVm{T}.Value"/>, <see cref="FieldVm{T}.IsHidden"/>, and <see cref="FieldVm{T}.IsReadOnly"/>. Initializes
/// <see cref="FieldVm{T}.Value"/> to an empty string by default.
/// </remarks>
public class StringFvm : FieldVm<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringFvm"/> class.
    /// </summary>
    /// <param name="name">The display or identifier name for this property.</param>
    public StringFvm(string name) : base(name)
    {
        Value = string.Empty;
    }
}
