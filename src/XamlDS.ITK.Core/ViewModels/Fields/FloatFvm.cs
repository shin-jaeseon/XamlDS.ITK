// <copyright file="FloatFvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>

namespace XamlDS.ITK.ViewModels.Fields;

/// <summary>
/// Represents a single-precision floating-point numeric field ViewModel.
/// </summary>
/// <remarks>
/// Inherits <see cref="FieldVm{T}.Value"/>, <see cref="FieldVm{T}.IsHidden"/>, and <see cref="FieldVm{T}.IsReadOnly"/>.
/// Specializes <see cref="NumericFvm{T}"/> for <see cref="float"/> and initializes
/// <see cref="NumericFvm{T}.Minimum"/> and <see cref="NumericFvm{T}.Maximum"/> to
/// <see cref="float.MinValue"/> and <see cref="float.MaxValue"/>, respectively.
/// </remarks>
public class FloatFvm : NumericFvm<float>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FloatFvm"/> class.
    /// </summary>
    /// <param name="name">The display or identifier name for this property.</param>
    public FloatFvm(string name) : base(name)
    {
        Minimum = float.MinValue;
        Maximum = float.MaxValue;
    }
}
