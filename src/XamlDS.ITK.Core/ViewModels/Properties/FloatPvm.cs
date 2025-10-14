// <copyright file="FloatPvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>

namespace XamlDS.ITK.ViewModels.Properties;

/// <summary>
/// Represents a single-precision floating-point numeric property ViewModel.
/// </summary>
/// <remarks>
/// This type specializes <see cref="NumericPvm{T}"/> for <see cref="float"/> and
/// initializes <see cref="NumericPvm{T}.Minimum"/> and <see cref="NumericPvm{T}.Maximum"/>
/// to <see cref="float.MinValue"/> and <see cref="float.MaxValue"/>, respectively.
/// </remarks>
public class FloatPvm : NumericPvm<float>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FloatPvm"/> class.
    /// </summary>
    /// <param name="name">The display or identifier name for this property.</param>
    public FloatPvm(string name) : base(name)
    {
        Minimum = float.MinValue;
        Maximum = float.MaxValue;
    }
}
