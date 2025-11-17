using XamlDS.ITK.ViewModels.Fields;

namespace XamlDS.ITK.ViewModels;

public class AppSettingsVm : ViewModelBase
{
    private BoolFvm _topmostWindow = new("TopmostWindow")
    {
        DisplayName = "Topmost Window",
        Value = false,
        IsHidden = true
    };

    private BoolFvm _touchFirst = new("TouchFirst")
    {
        DisplayName = "Touch First",
        Value = false,
        IsHidden = true
    };

    private RadioFvm<string> _themeColor = new("ThemeColor")
    {
        DisplayName = "Theme Color",
        Items =
        {
            new RadioItemVm<string>("Light", "Light"),
            new RadioItemVm<string>("Dark", "Dark"),
            new RadioItemVm<string>("System", "System")
        },
        Value = "Light",
    };

    private RadioFvm<string> _themeSizePreset = new("ThemeSizePreset")
    {
        DisplayName = "Theme Size Preset",
        Items =
        {
            new RadioItemVm<string>("Preset4", "Preset4"),
            new RadioItemVm<string>("Preset6", "Preset6"),
            new RadioItemVm<string>("Preset8", "Preset8")
        },
        Value = "Preset6",
    };

    public BoolFvm TopmostWindow
    {
        get => _topmostWindow;
        set => SetProperty(ref _topmostWindow, value);
    }

    public BoolFvm TouchFirst
    {
        get => _touchFirst;
        set => SetProperty(ref _touchFirst, value);
    }

    public RadioFvm<string> ThemeColor
    {
        get => _themeColor;
    }

    public RadioFvm<string> ThemeSizePreset
    {
        get => _themeSizePreset;
    }
}
