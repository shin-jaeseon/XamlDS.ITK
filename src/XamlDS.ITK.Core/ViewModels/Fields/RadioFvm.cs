using System.Collections.ObjectModel;

namespace XamlDS.ITK.ViewModels.Fields;

/// <summary>
/// Represents one radio option with a display name and a typed value.
/// </summary>
/// <typeparam name="T">Type of the option value.</typeparam>
public class RadioItemVm<T>(string displayName, T value) : ViewModelBase
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
public class RadioFvm<T>(string name) : FieldVm<T>(name)
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
            var newCollection = value ?? new ObservableCollection<RadioItemVm<T>>();
            SetProperty(ref _items, newCollection);
        }
    }
}
