using System.Windows;
using XamlDS.ITK.ViewModels.Commands;
using XamlDS.ITK.ViewModels.Panels;
using XamlDS.ITK.ViewModels.Panes;
using XamlDS.ITK.WPF.ViewModels.Commands;
using XamlDS.ITK.WPF.Views.Panels;
using XamlDS.ITK.WPF.Views.Panes;
using XamlDS.ITK.WPF.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using XamlDS.ITK.WPF.Controls.Adorners;

namespace XamlDS.ITK.WPF.Views;
/// <summary>
/// Interaction logic for DesktopWindowView.xaml
/// </summary>
public partial class DesktopWindowView : Window
{
    private GridLineAdorner? _gridAdorner;

    public DesktopWindowView()
    {
        InitializeComponent();

        AddDataTemplate(typeof(DockPanelVm), typeof(DockPanelView));
        AddDataTemplate(typeof(ApplicationBarPvm), typeof(ApplicationBarPv));
        AddDataTemplate(typeof(TestCvm), typeof(CommandView));
        AddDataTemplate(typeof(WPFExitApplicationCvm), typeof(CommandView));

#if DEBUG
        Loaded += OnLoadedAttachDebugGrid;
        Closed += OnClosedDetachDebugGrid;
#endif
    }

    private void AddDataTemplate(Type viewModelType, Type viewType)
    {
        DataTemplate template = new DataTemplate(viewModelType)
        {
            VisualTree = new FrameworkElementFactory(viewType)
        };
        this.Resources.Add(new DataTemplateKey(viewModelType), template);
    }

#if DEBUG
    private void OnLoadedAttachDebugGrid(object? sender, RoutedEventArgs e)
    {
        // Attach adorner to the Window's content to draw an overlay grid for layout debugging
        if (this.Content is UIElement uiElement)
        {
            var layer = AdornerLayer.GetAdornerLayer(uiElement);
            if (layer == null) return;

            _gridAdorner = new GridLineAdorner(uiElement)
            {
                Spacing = 48,
                Thickness = 1,
                LineBrush = Brushes.White,
                LineOpacity = 0.2,
                Origin = GridOrigin.LeftTop,
                SnapToDevicePixels = true
            };

            layer.Add(_gridAdorner);
        }
    }

    private void OnClosedDetachDebugGrid(object? sender, System.EventArgs e)
    {
        if (_gridAdorner != null)
        {
            if (this.Content is UIElement uiElement)
            {
                var layer = AdornerLayer.GetAdornerLayer(uiElement);
                if (layer != null)
                {
                    layer.Remove(_gridAdorner);
                }
            }

            _gridAdorner.Detach();
            _gridAdorner = null;
        }
    }
#endif
}
