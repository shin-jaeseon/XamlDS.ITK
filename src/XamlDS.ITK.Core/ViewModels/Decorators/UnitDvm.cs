namespace XamlDS.ITK.ViewModels.Decorators;

public class UnitDvm : DecoratorVm
{
    private string _unit = string.Empty;
    public string Unit
    {
        get => _unit;
        set => SetProperty(ref _unit, value);
    }
}
