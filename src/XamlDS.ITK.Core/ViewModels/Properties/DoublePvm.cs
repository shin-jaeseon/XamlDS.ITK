// <copyright file="DoublePvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>

namespace XamlDS.ITK.ViewModels.Properties;

/// <summary>
/// Represents a double-precision floating-point numeric property ViewModel.
/// </summary>
/// <remarks>
/// This type specializes <see cref="NumericPvm{T}"/> for <see cref="double"/> and
/// initializes <see cref="NumericPvm{T}.Minimum"/> and <see cref="NumericPvm{T}.Maximum"/>
/// to <see cref="double.MinValue"/> and <see cref="double.MaxValue"/>, respectively.
/// </remarks>
public class DoublePvm : NumericPvm<double>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DoublePvm"/> class.
    /// </summary>
    /// <param name="name">The display or identifier name for this property.</param>
    public DoublePvm(string name) : base(name)
    {
        Minimum = double.MinValue;
        Maximum = double.MaxValue;
    }
}
