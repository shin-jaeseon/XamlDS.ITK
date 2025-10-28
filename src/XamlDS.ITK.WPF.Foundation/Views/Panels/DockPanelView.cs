using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XamlDS.ITK.ViewModels.Panels;

namespace XamlDS.ITK.WPF.Views.Panels;

public class DockPanelView : DockPanel
{
    private DockPanelVm? _vm;
    private readonly Dictionary<DockPaneVm, ContentControl> _paneToControl = new();
    private readonly Dictionary<DockPaneVm, PropertyChangedEventHandler> _paneChangeHandlers = new();
    private static readonly IValueConverter DockConverter = new DockPositionToDockConverter();

    public DockPanelView()
    {
        LastChildFill = true; // make the last child fill the remaining space
        DataContextChanged += OnDataContextChanged;
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        DetachFromViewModel(e.OldValue as DockPanelVm);
        AttachToViewModel(e.NewValue as DockPanelVm);
    }

    private void AttachToViewModel(DockPanelVm? vm)
    {
        _vm = vm;
        if (_vm is null)
        {
            ClearVisuals();
            return;
        }

        if (_vm.Children is INotifyCollectionChanged incc)
            incc.CollectionChanged += OnChildrenCollectionChanged;

        // Create ContentControls for existing children
        foreach (var pane in _vm.Children)
            CreateAndTrackChild(pane);

        // Ensure correct visual ordering (non-center first, center last)
        RebuildVisualTree();
    }

    private void DetachFromViewModel(DockPanelVm? oldVm)
    {
        if (oldVm?.Children is INotifyCollectionChanged incc)
            incc.CollectionChanged -= OnChildrenCollectionChanged;

        foreach (var (pane, handler) in _paneChangeHandlers.ToArray())
        {
            pane.PropertyChanged -= handler;
        }

        _paneChangeHandlers.Clear();
        _paneToControl.Clear();
        Children.Clear();
        _vm = null;
    }

    private void OnChildrenCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (_vm is null) return;

        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                foreach (DockPaneVm pane in e.NewItems!)
                    CreateAndTrackChild(pane);
                break;

            case NotifyCollectionChangedAction.Remove:
                foreach (DockPaneVm pane in e.OldItems!)
                    RemoveAndUntrackChild(pane);
                break;

            case NotifyCollectionChangedAction.Replace:
                foreach (DockPaneVm pane in e.OldItems!)
                    RemoveAndUntrackChild(pane);
                foreach (DockPaneVm pane in e.NewItems!)
                    CreateAndTrackChild(pane);
                break;

            case NotifyCollectionChangedAction.Move:
                // Only ordering changed; rebuild to respect center-last rule
                break;

            case NotifyCollectionChangedAction.Reset:
                ClearVisuals();
                foreach (var pane in _vm.Children)
                    CreateAndTrackChild(pane);
                break;
        }

        RebuildVisualTree();
    }

    private void CreateAndTrackChild(DockPaneVm pane)
    {
        ContentControl cc;
        if (pane.Content is null)
        {
            cc = new DummyPaneView();
        }
        else
        {
            cc = new ContentControl
            {
                DataContext = null
            };

            // Bind DataContext <- pane.Content (OneWay)
            BindingOperations.SetBinding(
                cc,
                ContentControl.ContentProperty,
                new Binding(nameof(DockPaneVm.Content)) { Source = pane, Mode = BindingMode.OneWay });
        }

        // Bind DockPanel.Dock <- pane.Position (OneWay, via converter)
        BindingOperations.SetBinding(
            cc,
            DockPanel.DockProperty,
            new Binding(nameof(DockPaneVm.Position))
            {
                Source = pane,
                Mode = BindingMode.OneWay,
                Converter = DockConverter
            });

        _paneToControl[pane] = cc;

        // Track property changes to handle re-ordering if Position switches to/from Center
        PropertyChangedEventHandler handler = (_, args) =>
        {
            if (args.PropertyName is nameof(DockPaneVm.Position))
            {
                // Rebuild to ensure "Center" is placed last, others before
                RebuildVisualTree();
            }
        };
        pane.PropertyChanged += handler;
        _paneChangeHandlers[pane] = handler;
    }

    private void RemoveAndUntrackChild(DockPaneVm pane)
    {
        if (_paneToControl.TryGetValue(pane, out var cc))
        {
            Children.Remove(cc);
            _paneToControl.Remove(pane);
        }

        if (_paneChangeHandlers.TryGetValue(pane, out var handler))
        {
            pane.PropertyChanged -= handler;
            _paneChangeHandlers.Remove(pane);
        }
    }

    private void ClearVisuals()
    {
        foreach (var (pane, handler) in _paneChangeHandlers.ToArray())
        {
            pane.PropertyChanged -= handler;
        }

        _paneChangeHandlers.Clear();
        _paneToControl.Clear();
        Children.Clear();
    }

    private void RebuildVisualTree()
    {
        if (_vm is null) return;

        // Remove all existing visuals; re-add in desired order:
        // 1) non-center panes in collection order
        // 2) center panes in collection order (LastChildFill=true -> center fills remaining)
        Children.Clear();

        var nonCenters = _vm.Children.Where(p => p.Position != DockPosition.Center);
        var centers = _vm.Children.Where(p => p.Position == DockPosition.Center);

        foreach (var pane in nonCenters)
        {
            if (_paneToControl.TryGetValue(pane, out var cc))
                Children.Add(cc);
        }

        foreach (var pane in centers)
        {
            if (_paneToControl.TryGetValue(pane, out var cc))
                Children.Add(cc);
        }
    }

    private sealed class DockPositionToDockConverter : IValueConverter
    {
        public object? Convert(object? value, System.Type targetType, object? parameter, System.Globalization.CultureInfo? culture)
        {
            if (value is DockPosition pos)
            {
                return pos switch
                {
                    DockPosition.Left => Dock.Left,
                    DockPosition.Right => Dock.Right,
                    DockPosition.Top => Dock.Top,
                    DockPosition.Bottom => Dock.Bottom,
                    DockPosition.Center => Dock.Left, // Value ignored for last child when LastChildFill=true
                    _ => Dock.Left
                };
            }

            return Dock.Left;
        }

        public object? ConvertBack(object? value, System.Type targetType, object? parameter, System.Globalization.CultureInfo? culture)
            => throw new System.NotSupportedException();
    }
}
