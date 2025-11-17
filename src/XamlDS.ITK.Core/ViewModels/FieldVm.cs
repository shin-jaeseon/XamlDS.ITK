// <copyright file="PropertyEvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>


// <copyright file="FieldVm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>

namespace XamlDS.ITK.ViewModels;

/// <summary>
/// Represents a generic field ViewModel that exposes a typed <see cref="Value"/> and common UI state
/// such as <see cref="IsHidden"/> and <see cref="IsReadOnly"/>.
/// </summary>
/// <typeparam name="T">The type of the property value.</typeparam>
/// <remarks>
/// Inherits from <see cref="EntityVm"/> to leverage common ViewModel infrastructure
/// such as property change notification via <c>SetProperty</c>.
/// </remarks>
public class FieldVm<T>(string name) : DecoratorHostVm
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

    private string _displayName = string.Empty;

    /// <summary>
    /// Gets or sets the user-friendly name displayed in the UI.
    /// </summary>
    /// <remarks>
    /// This property is used for displaying the name of the functionality or device
    /// to the end user in the interface, such as in usage history charts or component labels.
    /// </remarks>
    public string DisplayName
    {
        get => _displayName;
        set => SetProperty(ref _displayName, value);
    }

    private bool _isHidden = false;

    /// <summary>
    /// Gets or sets a value indicating whether this field should be hidden from the UI.
    /// </summary>
    /// <remarks>
    /// Set to <see langword="true"/> to keep the field functional in application logic while preventing users from seeing or interacting with it in the UI.
    /// Typically, the view should not render any control when this is <see langword="true"/> (e.g., collapse the control's visibility).
    /// This property raises change notifications via <c>SetProperty</c> so UI bindings can react to runtime changes.
    /// </remarks>
    public bool IsHidden
    {
        get => _isHidden;
        set => SetProperty(ref _isHidden, value);
    }

    private bool _isReadOnly = false;

    /// <summary>
    /// Gets or sets a value indicating whether this field is visible but not editable by the user.
    /// </summary>
    /// <remarks>
    /// Use this when the field should be displayed for context while preventing modification.
    /// This is distinct from <see cref="IsHidden"/>, which removes the field from the UI entirely.
    /// Typical UI behavior is to bind a control's enabled/editable state to the inverse of this value or render the field as read-only.
    /// This property raises change notifications via <c>SetProperty</c> so UI bindings can react to runtime changes.
    /// </remarks>
    public bool IsReadOnly
    {
        get => _isReadOnly;
        set => SetProperty(ref _isReadOnly, value);
    }
}
