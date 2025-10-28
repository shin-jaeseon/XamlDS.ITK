// <copyright file="DoubleFvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>

namespace XamlDS.ITK.ViewModels.Fields;

/// <summary>
/// Represents a double-precision floating-point numeric field ViewModel.
/// </summary>
/// <remarks>
/// This type specializes <see cref="NumericFvm{T}"/> for <see cref="double"/> and
/// initializes <see cref="NumericFvm{T}.Minimum"/> and <see cref="NumericFvm{T}.Maximum"/>
/// to <see cref="double.MinValue"/> and <see cref="double.MaxValue"/>, respectively.
/// </remarks>
public class DoubleFvm : NumericFvm<double>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DoubleFvm"/> class.
    /// </summary>
    /// <param name="name">The display or identifier name for this property.</param>
    public DoubleFvm(string name) : base(name)
    {
        Minimum = double.MinValue;
        Maximum = double.MaxValue;
    }
}
