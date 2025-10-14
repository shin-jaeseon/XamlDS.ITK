// <copyright file="PropertyEvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>

using XamlDS.ITK.ViewModel;

namespace XamlDS.ITK.ViewModels.Properties;

/// <summary>
/// Represents a generic property Entity ViewModel that exposes a typed <see cref="Value"/>.
/// </summary>
/// <typeparam name="T">The type of the property value.</typeparam>
/// <remarks>
/// Inherits from <see cref="EntityVm"/> to leverage common ViewModel infrastructure
/// such as property change notification via <c>SetProperty</c>.
/// </remarks>
public class PropertyEvm<T>(string name) : EntityVm(name)
{
    /// <summary>
    /// Backing field for <see cref="Value"/>.
    /// </summary>
    private T _value = default!;

    /// <summary>
    /// Gets or sets the property value.
    /// </summary>
    public virtual T Value
    {
        get => _value;
        set => SetProperty(ref _value, value);
    }
}
