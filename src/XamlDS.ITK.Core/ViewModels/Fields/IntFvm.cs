// <copyright file="IntFvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>

namespace XamlDS.ITK.ViewModels.Fields;

/// <summary>
/// Represents a signed32-bit integer numeric field ViewModel.
/// </summary>
/// <remarks>
/// Inherits <see cref="FieldVm{T}.Value"/>, <see cref="FieldVm{T}.IsHidden"/>, and <see cref="FieldVm{T}.IsReadOnly"/>.
/// Specializes <see cref="NumericFvm{T}"/> for <see cref="int"/> and initializes
/// <see cref="NumericFvm{T}.Minimum"/> and <see cref="NumericFvm{T}.Maximum"/> to
/// <see cref="int.MinValue"/> and <see cref="int.MaxValue"/>, respectively.
/// </remarks>
public class IntFvm : NumericFvm<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntFvm"/> class.
    /// </summary>
    /// <param name="name">The display or identifier name for this property.</param>
    public IntFvm(string name) : base(name)
    {
        Minimum = int.MinValue;
        Maximum = int.MaxValue;
    }
}
