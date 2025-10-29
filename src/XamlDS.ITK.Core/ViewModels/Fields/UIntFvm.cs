// <copyright file="UIntPvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>

namespace XamlDS.ITK.ViewModels.Fields;

/// <summary>
/// Represents an unsigned32-bit integer numeric field ViewModel.
/// </summary>
/// <remarks>
/// Inherits <see cref="FieldVm{T}.Value"/>, <see cref="FieldVm{T}.IsHidden"/>, and <see cref="FieldVm{T}.IsReadOnly"/>.
/// Specializes <see cref="NumericFvm{T}"/> for <see cref="uint"/> and initializes
/// <see cref="NumericFvm{T}.Minimum"/> and <see cref="NumericFvm{T}.Maximum"/> to
/// <see cref="uint.MinValue"/> and <see cref="uint.MaxValue"/>, respectively.
/// </remarks>
public class UIntFvm : NumericFvm<uint>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UIntFvm"/> class.
    /// </summary>
    /// <param name="name">The display or identifier name for this property.</param>
    public UIntFvm(string name) : base(name)
    {
        Minimum = uint.MinValue;
        Maximum = uint.MaxValue;
    }
}
