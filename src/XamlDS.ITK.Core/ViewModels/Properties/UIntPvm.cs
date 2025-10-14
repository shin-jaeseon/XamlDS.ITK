// <copyright file="UIntPvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>

namespace XamlDS.ITK.ViewModels.Properties;

/// <summary>
/// Represents an unsigned 32-bit integer numeric property ViewModel.
/// </summary>
/// <remarks>
/// This type specializes <see cref="NumericPvm{T}"/> for <see cref="uint"/> and
/// initializes <see cref="NumericPvm{T}.Minimum"/> and <see cref="NumericPvm{T}.Maximum"/>
/// to <see cref="uint.MinValue"/> and <see cref="uint.MaxValue"/>, respectively.
/// </remarks>
public class UIntPvm : NumericPvm<uint>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UIntPvm"/> class.
    /// </summary>
    /// <param name="name">The display or identifier name for this property.</param>
    public UIntPvm(string name) : base(name)
    {
        Minimum = uint.MinValue;
        Maximum = uint.MaxValue;
    }
}
