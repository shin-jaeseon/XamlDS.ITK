using System.Collections.ObjectModel;

namespace XamlDS.ITK.ViewModels.Panels;


public class StackPaneVm : ViewModelBase
{
    public ViewModelBase? Content { get; set; }
}

public class StackPanelVm : DecoratorHostVm
{
    private ObservableCollection<StackPaneVm> _children = [];
    public ObservableCollection<StackPaneVm> Children
    {
        get => _children;
        set => SetProperty(ref _children, value);
    }
}
