// <copyright file="NumericPvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>
using System.Numerics;

namespace XamlDS.ITK.ViewModels.Properties;

/// <summary>
/// Base class for all Numeric Property Entity ViewModel classes.
/// </summary>
public class NumericPvm<T>(string name) : PropertyEvm<T>(name) where T : INumber<T>, IComparable<T>, IEquatable<T>
{
    /// <summary>
    /// Backing field for <see cref="Minimum"/>. Defaults to <c>T.Zero</c>.
    /// </summary>
    private T _minimum = T.Zero; // Default minimum value

    /// <summary>
    /// Gets or sets the minimum allowable value for this numeric property.
    /// </summary>
    public T Minimum
    {
        get => _minimum;
        set => SetProperty(ref _minimum, value);
    }

    /// <summary>
    /// Backing field for <see cref="Maximum"/>. Defaults to <c>T.Zero</c>.
    /// </summary>
    private T _maximum = T.Zero; // Default maximum value

    /// <summary>
    /// Gets or sets the maximum allowable value for this numeric property.
    /// </summary>
    public T Maximum
    {
        get => _maximum;
        set => SetProperty(ref _maximum, value);
    }
}
