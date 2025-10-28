using System.Windows;
using XamlDS.ITK.ViewModels.Panels;
using XamlDS.ITK.ViewModels.Panes;
using XamlDS.ITK.WPF.ViewModels.Commands;
using XamlDS.ITK.WPF.Views.Commands;
using XamlDS.ITK.WPF.Views.Panels;
using XamlDS.ITK.WPF.Views.Panes;

namespace XamlDS.ITK.WPF.Views;
/// <summary>
/// Interaction logic for DesktopWindowView.xaml
/// </summary>
public partial class DesktopWindowView : Window
{
    public DesktopWindowView()
    {
        InitializeComponent();

        AddDataTemplate(typeof(DockPanelVm), typeof(DockPanelView));
        AddDataTemplate(typeof(HorizontalBarPvm), typeof(HorizontalBarPv));
        AddDataTemplate(typeof(ApplicationBarPvm), typeof(ApplicationBarPv));

        AddDataTemplate(typeof(WPFExitApplicationCvm), typeof(CommandView));
    }

    private void AddDataTemplate(Type viewModelType, Type viewType)
    {
        DataTemplate template = new DataTemplate(viewModelType)
        {
            VisualTree = new FrameworkElementFactory(viewType)
        };
        this.Resources.Add(new DataTemplateKey(viewModelType), template);
    }
}
