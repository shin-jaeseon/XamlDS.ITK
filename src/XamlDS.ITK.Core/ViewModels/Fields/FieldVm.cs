// <copyright file="PropertyEvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>


// <copyright file="PropertyEvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>

namespace XamlDS.ITK.ViewModels.Fields;

/// <summary>
/// Represents a generic property Entity ViewModel that exposes a typed <see cref="Value"/>.
/// </summary>
/// <typeparam name="T">The type of the property value.</typeparam>
/// <remarks>
/// Inherits from <see cref="EntityVm"/> to leverage common ViewModel infrastructure
/// such as property change notification via <c>SetProperty</c>.
/// </remarks>
public class FieldVm<T>(string name) : ViewModelBase()
{
    private readonly string _name = name;

    /// <summary>
    /// The programmatic identifier for this object.
    /// </summary>
    /// <remarks>
    /// It's used as an identifier in logs and for debugging purposes.
    /// </remarks>
    public string Name { get => _name; }

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
