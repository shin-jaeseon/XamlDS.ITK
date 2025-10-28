namespace XamlDS.ITK.ViewModels;

public class DesktopWindowVm : ViewModelBase
{
    private readonly AppSettingsVm _appSettings;
    private readonly AppStatesVm _appStates;
    private string _windowTitle = string.Empty;
    private ViewModelBase? _child;

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


    public ViewModelBase? Child
    {
        get => _child;
        set => SetProperty(ref _child, value);
    }
}
