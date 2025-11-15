using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace XamlDS.ITK.ViewModels.Fields;

/// <summary>
/// Represents one radio option with a display name and a typed value.
/// </summary>
/// <typeparam name="T">Type of the option value.</typeparam>
public class RadioItemVm<T>(string displayName, T value)
{
    /// <summary>
    /// The human readable label to show for this option.
    /// </summary>
    public string DisplayName { get; } = displayName;

    /// <summary>
    /// The typed value associated with this option.
    /// </summary>
    public T Value { get; } = value;
}

/// <summary>
/// ViewModel representing a set of radio options and a typed selected value.
/// </summary>
/// <typeparam name="T">Type of the selected value.</typeparam>
/// <param name="name">Name of the field (passed to <see cref="FieldVm{T}"/>/base).</param>
public class RadioFvm<T> : FieldVm<T>
{
    private ObservableCollection<RadioItemVm<T>> _items = new();

    /// <summary>
    /// Collection of available radio options. This collection is observable and the view may bind to it.
    /// Setting the collection will attach change handlers to keep <see cref="Value"/> in sync.
    /// </summary>
    public ObservableCollection<RadioItemVm<T>> Items
    {
        get => _items;
        set
        {
            if (ReferenceEquals(_items, value))
                return;

            // detach old handler
            if (_items != null)
                _items.CollectionChanged -= OnItemsChanged;

            var newCollection = value ?? new ObservableCollection<RadioItemVm<T>>();

            // use SetProperty to raise change notification for the property itself
            if (SetProperty(ref _items!, newCollection))
            {
                _items.CollectionChanged += OnItemsChanged;
                // Ensure current value is still valid for the new collection
                EnsureValueIsValid();
            }
        }
    }

    /// <summary>
    /// Computed convenience property that returns the <see cref="RadioItemVm{T}"/> matching current <see cref="Value"/>,
    /// or <see langword="null"/> if no matching item exists. Setting this property will update <see cref="Value"/>
    /// to the provided item's value (or to default(T) when set to <see langword="null"/>).
    /// </summary>
    public RadioItemVm<T>? SelectedItem
    {
        get => Items.FirstOrDefault(i => EqualityComparer<T>.Default.Equals(i.Value, Value));
        set
        {
            // null means clear selection -> set default
            if (value is null)
            {
                Value = default!;
                return;
            }

            // Try to find a matching item in the collection either by reference or by value equality
            var match = Items.FirstOrDefault(i => ReferenceEquals(i, value) || EqualityComparer<T>.Default.Equals(i.Value, value.Value));
            if (match is null)
            {
                throw new ArgumentException("The specified SelectedItem is not part of the Items collection.", nameof(value));
            }

            // Set the typed Value to the matched item's value.
            Value = match.Value!;
        }
    }

    /// <summary>
    /// Initializes a new instance and hooks collection change events.
    /// </summary>
    public RadioFvm(string name)
        : base(name)
    {
        // Ensure _items is not null before subscribing to CollectionChanged
        _items.CollectionChanged += OnItemsChanged;
    }

    private void OnItemsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // If selected value was removed or collection reset, ensure Value stays valid.
        if (e.Action == NotifyCollectionChangedAction.Remove ||
            e.Action == NotifyCollectionChangedAction.Replace ||
            e.Action == NotifyCollectionChangedAction.Reset)
        {
            EnsureValueIsValid();
        }
    }

    private void EnsureValueIsValid()
    {
        // If current Value is present in Items, nothing to do.
        if (Items.Any(i => EqualityComparer<T>.Default.Equals(i.Value, Value)))
            return;

        // If Items has at least one element, set Value to first item's value.
        if (Items.Count > 0)
        {
            Value = Items[0].Value!;
        }
        else
        {
            // no items left -> reset to default(T)
            Value = default!;
        }

        // Notify that SelectedItem changed (Value setter should already raise Value changed).
        OnPropertyChanged(nameof(SelectedItem));
    }

    /// <summary>
    /// Adds a new radio option. Throws <see cref="ArgumentException"/> if an option with the same value already exists.
    /// </summary>
    /// <param name="displayName">Label for the option.</param>
    /// <param name="value">Typed value for the option.</param>
    public void AddItem(string displayName, T value)
    {
        if (Items.Any(i => EqualityComparer<T>.Default.Equals(i.Value, value)))
            throw new ArgumentException($"An item with the value '{value}' already exists.", nameof(value));

        Items.Add(new RadioItemVm<T>(displayName, value));
    }

    /// <summary>
    /// Removes the option with the given value. If the removed value was the current selection,
    /// <see cref="Value"/> will be adjusted (first item or default).
    /// </summary>
    /// <param name="value">Value to remove.</param>
    public void RemoveItem(T value)
    {
        var itemToRemove = Items.FirstOrDefault(i => EqualityComparer<T>.Default.Equals(i.Value, value));
        if (itemToRemove != null)
            Items.Remove(itemToRemove);
    }

    /// <summary>
    /// Override Value setter to raise SelectedItem notifications when selection changes.
    /// </summary>
    public override T Value
    {
        get => base.Value;
        set
        {
            var equal = EqualityComparer<T>.Default.Equals(base.Value, value);
            base.Value = value;
            if (!equal)
            {
                // ensure SelectedItem observers are notified
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
    }
}
