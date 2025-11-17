using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamlDS.ITK.ViewModels.Panels;

namespace XamlDS.ITK.ViewModels;

public class PanelVm<T> : DecoratorHostVm
{
    private ObservableCollection<T> _children = new();
    public ObservableCollection<T> Children
    {
        get => _children;
        set => SetProperty(ref _children, value);
    }
}
