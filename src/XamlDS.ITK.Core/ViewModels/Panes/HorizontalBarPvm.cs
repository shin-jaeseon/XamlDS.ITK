namespace XamlDS.ITK.ViewModels.Panes;

public class HorizontalBarPvm : PaneVm
{
    private object? _leftContent;
    private object? _rightContent;
    private object? _centerContent;

    public object? LeftContent
    {
        get => _leftContent;
        set => SetProperty(ref _leftContent, value);
    }
    public object? RightContent
    {
        get => _rightContent;
        set => SetProperty(ref _rightContent, value);
    }
    public object? CenterContent
    {
        get => _centerContent;
        set => SetProperty(ref _centerContent, value);
    }
}
