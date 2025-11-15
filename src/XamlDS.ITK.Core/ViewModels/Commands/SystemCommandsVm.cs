namespace XamlDS.ITK.ViewModels.Commands;

public class SystemCommandsVm : ViewModelBase
{
    private ExitApplicationCvm _exitApplication;
    private TestCvm _test;
    public SystemCommandsVm(ExitApplicationCvm exitApplicationCvm, TestCvm testCommand)
    {
        _exitApplication = exitApplicationCvm;
        _test = testCommand;
    }

    public ExitApplicationCvm ExitApplication => _exitApplication;

    public TestCvm Test => _test;
}
