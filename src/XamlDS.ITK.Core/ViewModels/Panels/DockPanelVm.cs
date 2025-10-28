using System.Collections.ObjectModel;

namespace XamlDS.ITK.ViewModels.Panels;

public enum DockPosition
{
    Center,
    Left,
    Right,
    Top,
    Bottom,
}

public class DockPaneVm : ViewModelBase
{
    private object? _content;
    private DockPosition _position = DockPosition.Center;

    public object? Content
    {
        get => _content;
        set => SetProperty(ref _content, value);
    }

    public DockPosition Position
    {
        get => _position;
        set => SetProperty(ref _position, value);
    }
}

public class DockPanelVm : ViewModelBase
{
    private ObservableCollection<DockPaneVm> _children = [];
    public ObservableCollection<DockPaneVm> Children
    {
        get => _children;
        set => SetProperty(ref _children, value);
    }
}
