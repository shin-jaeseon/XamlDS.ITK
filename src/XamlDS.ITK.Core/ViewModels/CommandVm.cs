using System.Windows.Input;
using XamlDS.ITK.Input;

namespace XamlDS.ITK.ViewModels;

/// <summary>
/// Command ViewModel base class
/// </summary>
public abstract class CommandVm(string name) : ViewModelBase
{
    private readonly string _name = name;

    /// <summary>
    /// The programmatic identifier for this object.
    /// </summary>
    /// <remarks>
    /// It's used as an identifier in logs and for debugging purposes.
    /// </remarks>
    public string Name { get => _name; }

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

    private string _description = string.Empty;

    /// <summary>
    /// Detailed explanation of this object's purpose or functionality.
    /// </summary>
    /// <remarks>
    /// This property provides a more detailed description of the object's role or function.
    /// For Command ViewModels (Cvm), this is typically used as tooltip text for buttons.
    /// </remarks>
    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    private ICommand? _command = null;

    public virtual ICommand Command
    {
        get
        {
            return _command ??= new CommandHandler(Execute, CanExecute);
        }
        set => SetProperty(ref _command, value);
    }
    public abstract void Execute(object? parameter);
    public virtual bool CanExecute(object? parameter)
    {
        return true;
    }
}
