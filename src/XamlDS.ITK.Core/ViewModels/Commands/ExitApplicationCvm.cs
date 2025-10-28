namespace XamlDS.ITK.ViewModels.Commands;

public abstract class ExitApplicationCvm : CommandVm
{
    protected ExitApplicationCvm() : base("ExitApplication")
    {
        DisplayName = "Exit Application";
        Description = "Exits the application.";
    }
}
