﻿// <copyright file="Cvm.cs" company="Xaml Design Studio">
// Copyright (c) Xaml Design Studio. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.
// </copyright>
// This file is part of the XamlDS.ITK.Core open source project.
// It defines the abstract base class for ViewModels (Vm),
// providing a generic implementation for handling property changes in MVVM applications.
// <author>Shin Jae Seon</author>
// <date>2025-08-05</date>
// <summary>
// Base class for all ViewModel classes, implementing INotifyPropertyChanged.
// Provides methods for property change notification and value setting.
// </summary>

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace XamlDS.ITK.Core.ViewModel;

/// <summary>
/// A base class for ViewModels implementing the MVVM pattern, compatible with Avalonia, WPF, and WinUI3.
/// </summary>
public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    private bool _disposed;

    /// <summary>
    /// Event triggered when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Notifies listeners that a property value has changed.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Sets the property and notifies listeners if the value changes.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="field">The backing field of the property.</param>
    /// <param name="value">The new value to set.</param>
    /// <param name="propertyName">The name of the property (optional).</param>
    /// <returns>True if the value was changed, false otherwise.</returns>
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        this.OnPropertyChanged(propertyName);
        return true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        _disposed = true;
    }

    ~ViewModelBase()
    {
        Dispose(false);
    }
}
