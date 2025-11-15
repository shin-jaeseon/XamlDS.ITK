using XamlDS.ITK.ViewModels.Commands;

namespace XamlDS.ITK.ViewModels.Panes;

public class ApplicationBarPvm : PaneVm
{
    private SystemCommandsVm _systemCommands;
    public ApplicationBarPvm(SystemCommandsVm systemCommands)
    {
        _systemCommands = systemCommands;
    }

    public SystemCommandsVm SystemCommands => _systemCommands;
}
