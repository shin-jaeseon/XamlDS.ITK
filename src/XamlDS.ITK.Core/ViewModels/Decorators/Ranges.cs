namespace XamlDS.ITK.ViewModels.Decorators;

public abstract class RangeDvm : DecoratorVm
{
    public double Minimum { get; set; }
    public double Maximum { get; set; }
}

public class LimitRangeDvm : RangeDvm
{
}

public class WarningRangeDvm : RangeDvm
{
}
