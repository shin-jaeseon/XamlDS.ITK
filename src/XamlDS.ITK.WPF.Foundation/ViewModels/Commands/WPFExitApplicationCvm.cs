using XamlDS.ITK.ViewModels.Commands;

namespace XamlDS.ITK.WPF.ViewModels.Commands;

public class WPFExitApplicationCvm : ExitApplicationCvm
{
    public override void Execute(object? parameter)
    {
        if (CanExecute(parameter) == false)
            return;
        System.Windows.Application.Current.Shutdown();
    }
}
