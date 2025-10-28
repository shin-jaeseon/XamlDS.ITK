using XamlDS.ITK.ViewModels;

namespace ITKSandbox.Main.ViewModels;

public sealed class MainWindowVm : DesktopWindowVm
{
    public MainWindowVm(AppSettingsVm appSettings, AppStatesVm appStates, MainPanelVm mainPanel) : base(appSettings, appStates)
    {
        WindowTitle = "ITK Sandbox";
        Child = mainPanel;
    }
}
