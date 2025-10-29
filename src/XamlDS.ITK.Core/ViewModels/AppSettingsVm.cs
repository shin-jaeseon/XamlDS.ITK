using XamlDS.ITK.ViewModels.Fields;

namespace XamlDS.ITK.ViewModels;

public class AppSettingsVm : ViewModelBase
{
    private BoolFvm _topmostWindow = new("TopmostWindow")
    {
        DisplayName = "Topmost Window",
        Description = "Determines whether the application window stays on top of all other windows.",
        Value = false,
        IsHidden = true
    };

    public BoolFvm TopmostWindow
    {
        get => _topmostWindow;
        set => SetProperty(ref _topmostWindow, value);
    }
}
