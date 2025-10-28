using System.Collections.ObjectModel;

namespace XamlDS.ITK.ViewModels.Panels;


public class CanvasPaneVm : ViewModelBase
{
}

public class CanvasPanelVm : ViewModelBase
{
    private ObservableCollection<CanvasPaneVm> _children = [];
    public ObservableCollection<CanvasPaneVm> Children
    {
        get => _children;
        set => SetProperty(ref _children, value);
    }
}
