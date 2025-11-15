using System.Collections.ObjectModel;

namespace XamlDS.ITK.ViewModels;

/// <summary>
/// Defines the contract for a ViewModel that can host <see cref="ViewDecoratorVm"/> instances.
/// </summary>
public interface IDecoratorHost
{
    /// <summary>
    /// Gets a read-only view of decorators applied to this host.
    /// </summary>
    ReadOnlyObservableCollection<ViewDecoratorVm> Decorators { get; }

    /// <summary>
    /// Returns true when a decorator of type <typeparamref name="T"/> exists.
    /// </summary>
    bool HasDecorator<T>() where T : ViewDecoratorVm;

    /// <summary>
    /// Gets the existing decorator of type <typeparamref name="T"/> if any; otherwise creates, adds and returns a new instance.
    /// Guarantees at most one instance of <typeparamref name="T"/> in the collection.
    /// </summary>
    T EnsureDecorator<T>() where T : ViewDecoratorVm, new();

    /// <summary>
    /// Attempts to get a decorator of type <typeparamref name="T"/>.
    /// </summary>
    bool TryGetDecorator<T>(out T decorator) where T : ViewDecoratorVm;

    /// <summary>
    /// Removes a decorator of type <typeparamref name="T"/> if present.
    /// </summary>
    bool RemoveDecorator<T>() where T : ViewDecoratorVm;
}

/// <summary>
/// Base ViewModel implementation of <see cref="IDecoratorHost"/>.
/// Use this as a base class for Pane, Command, Field, etc. that need decorators.
/// </summary>
public abstract class DecoratorHostVm : ViewModelBase, IDecoratorHost
{
    private readonly ObservableCollection<ViewDecoratorVm> _decorators = new();
    private readonly ReadOnlyObservableCollection<ViewDecoratorVm> _readOnlyDecorators;

    protected DecoratorHostVm()
    {
        _readOnlyDecorators = new ReadOnlyObservableCollection<ViewDecoratorVm>(_decorators);
    }

    public ReadOnlyObservableCollection<ViewDecoratorVm> Decorators => _readOnlyDecorators;

    public bool HasDecorator<T>() where T : ViewDecoratorVm
    => _decorators.Any(d => d is T);

    public bool TryGetDecorator<T>(out T decorator) where T : ViewDecoratorVm
    {
        var d = _decorators.OfType<T>().FirstOrDefault();
        decorator = d!;
        return d is not null;
    }

    public T EnsureDecorator<T>() where T : ViewDecoratorVm, new()
    {
        var existing = _decorators.OfType<T>().FirstOrDefault();
        if (existing is not null)
            return existing;

        var created = new T();
        _decorators.Add(created);
        return created;
    }

    public bool RemoveDecorator<T>() where T : ViewDecoratorVm
    {
        var existing = _decorators.OfType<T>().FirstOrDefault();
        return existing is not null && _decorators.Remove(existing);
    }
}
