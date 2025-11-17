namespace XamlDS.ITK.ViewModels;

public class DesktopWindowVm : ViewModelBase
{
    private readonly AppSettingsVm _appSettings;
    private readonly AppStatesVm _appStates;
    private string _windowTitle = string.Empty;
    private object? _content;

    public DesktopWindowVm(AppSettingsVm appSettings, AppStatesVm appStates)
    {
        _appStates = appStates;
        _appSettings = appSettings;
    }

    public AppStatesVm AppStates => _appStates;

    public AppSettingsVm AppSettings => _appSettings;

    public string WindowTitle
    {
        get => _windowTitle;
        set => SetProperty(ref _windowTitle, value);
    }

    public object? Content
    {
        get => _content;
        set => SetProperty(ref _content, value);
    }
}
