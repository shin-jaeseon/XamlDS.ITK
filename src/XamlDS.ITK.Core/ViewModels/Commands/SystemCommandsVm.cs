namespace XamlDS.ITK.ViewModels.Commands;

public class SystemCommandsVm : ViewModelBase
{
    private ExitApplicationCvm _exitApplication;
    public SystemCommandsVm(ExitApplicationCvm exitApplicationCvm)
    {
        _exitApplication = exitApplicationCvm;
    }

    public ExitApplicationCvm ExitApplication => _exitApplication;
}
