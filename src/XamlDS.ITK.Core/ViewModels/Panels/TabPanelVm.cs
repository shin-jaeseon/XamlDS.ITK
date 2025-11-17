using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace XamlDS.ITK.ViewModels.Panels;

public class TabPaneVm : ViewModelBase
{
    private ViewModelBase? _content;
    private string _header = string.Empty;
    private string _icon = string.Empty;

    public ViewModelBase? Content
    {
        get => _content;
        set => SetProperty(ref _content, value);
    }

    public string Header
    {
        get => _header;
        set => SetProperty(ref _header, value);
    }

    public string Icon
    {
        get => _icon;
        set => SetProperty(ref _icon, value);
    }
}

public enum TabHeaderDisplayMode
{
    IconOnly,
    TextOnly,
    IconAndText
}

public class TabPanelVm : DecoratorHostVm
{
    private ObservableCollection<TabPaneVm> _children = [];
    private int _selectedIndex = -1;
    private TabPaneVm? _selectedTab;
    private bool _isSyncing;
    private TabHeaderDisplayMode _headerDisplayMode = TabHeaderDisplayMode.IconAndText;

    public TabPanelVm()
    {
        _children.CollectionChanged += OnChildrenChanged;
    }

    public ObservableCollection<TabPaneVm> Children
    {
        get => _children;
        set
        {
            if (ReferenceEquals(_children, value)) return;

            var old = _children;
            if (SetProperty(ref _children, value))
            {
                if (old is not null)
                    old.CollectionChanged -= OnChildrenChanged;

                if (_children is not null)
                    _children.CollectionChanged += OnChildrenChanged;

                // Re-sync selection against the new collection
                SyncSelectedIndexFromTab();
            }
        }
    }

    // Canonical selection
    public TabPaneVm? SelectedTab
    {
        get => _selectedTab;
        set
        {
            if (SetProperty(ref _selectedTab, value))
            {
                SyncSelectedIndexFromTab();
            }
        }
    }

    // For binding convenience (e.g., XAML SelectedIndex)
    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            if (SetProperty(ref _selectedIndex, value))
            {
                SyncSelectedTabFromIndex();
            }
        }
    }

    private void OnChildrenChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // If the selected tab was removed, choose a sensible neighbor; otherwise just resync.
        if (_selectedTab is not null && !_children.Contains(_selectedTab))
        {
            // Prefer the same index if possible, otherwise previous item, otherwise none.
            var candidateIndex = _children.Count == 0
                ? -1
                : Math.Clamp(e.OldStartingIndex >= 0 ? e.OldStartingIndex : _children.Count - 1, 0, _children.Count - 1);

            SelectedIndex = candidateIndex; // will update SelectedTab via sync
        }
        else
        {
            // Keep index aligned with current tab reference.
            SyncSelectedIndexFromTab();
        }
    }

    private void SyncSelectedTabFromIndex()
    {
        if (_isSyncing) return;
        _isSyncing = true;

        TabPaneVm? newTab = null;
        if (_selectedIndex >= 0 && _selectedIndex < _children.Count)
        {
            newTab = _children[_selectedIndex];
        }

        if (!ReferenceEquals(_selectedTab, newTab))
        {
            _selectedTab = newTab;
            OnPropertyChanged(nameof(SelectedTab));
        }

        _isSyncing = false;
    }

    private void SyncSelectedIndexFromTab()
    {
        if (_isSyncing) return;
        _isSyncing = true;

        var newIndex = _selectedTab is null ? -1 : _children.IndexOf(_selectedTab);

        // If the referenced tab is not in the collection, clear selection
        if (_selectedTab is not null && newIndex == -1)
        {
            _selectedTab = null;
            OnPropertyChanged(nameof(SelectedTab));
        }

        if (_selectedIndex != newIndex)
        {
            _selectedIndex = newIndex;
            OnPropertyChanged(nameof(SelectedIndex));
        }

        _isSyncing = false;
    }

    public TabHeaderDisplayMode HeaderDisplayMode
    {
        get => _headerDisplayMode;
        set => SetProperty(ref _headerDisplayMode, value);
    }
}


