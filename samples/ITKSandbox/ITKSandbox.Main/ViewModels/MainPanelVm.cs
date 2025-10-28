using XamlDS.ITK.ViewModels.Panels;
using XamlDS.ITK.ViewModels.Panes;

namespace ITKSandbox.Main.ViewModels;

public class MainPanelVm : DockPanelVm
{
    public MainPanelVm(ApplicationBarPvm applicationBar) : base()
    {
        Children = [
            new DockPaneVm()
            {
                Position = DockPosition.Bottom,
                Content = applicationBar,
            },
            new DockPaneVm()
            {
                //Content = new MockPanelVm() {Text = "Dock Center"},
            }
        ];
    }
}
