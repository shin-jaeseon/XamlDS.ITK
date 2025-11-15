namespace XamlDS.ITK.ViewModels;

public abstract class ViewDecoratorVm : ViewModelBase
{
}

public class ReadOnlyVdvm : ViewDecoratorVm
{
}

public class HiddenVdvm : ViewDecoratorVm
{
}

public static class HiddenExtensions
{
    /// <summary>
    /// Adds or removes the <see cref="HiddenVdvm"/> decorator. Returns true if a change was made.
    /// </summary>
    public static bool SetHidden(this IDecoratorHost host, bool isHidden)
    {
        if (host is null) throw new ArgumentNullException(nameof(host));
        if (isHidden)
        {
            if (host.HasDecorator<HiddenVdvm>()) return false;
            host.EnsureDecorator<HiddenVdvm>();
            return true;
        }
        else
        {
            return host.RemoveDecorator<HiddenVdvm>();
        }
    }

    /// <summary>
    /// Returns true when a <see cref="HiddenVdvm"/> decorator exists.
    /// </summary>
    public static bool IsHidden(this IDecoratorHost host)
    {
        if (host is null) throw new ArgumentNullException(nameof(host));
        return host.HasDecorator<HiddenVdvm>();
    }
}

public static class ReadOnlyExtensions
{
    /// <summary>
    /// Adds or removes the <see cref="ReadOnlyVdvm"/> decorator. Returns true if a change was made.
    /// </summary>
    public static bool SetReadOnly(this IDecoratorHost host, bool isReadOnly)
    {
        if (host is null) throw new ArgumentNullException(nameof(host));
        if (isReadOnly)
        {
            if (host.HasDecorator<ReadOnlyVdvm>()) return false;
            host.EnsureDecorator<ReadOnlyVdvm>();
            return true;
        }
        else
        {
            return host.RemoveDecorator<ReadOnlyVdvm>();
        }
    }

    /// <summary>
    /// Returns true when a <see cref="ReadOnlyVdvm"/> decorator exists.
    /// </summary>
    public static bool IsReadOnly(this IDecoratorHost host)
    {
        if (host is null) throw new ArgumentNullException(nameof(host));
        return host.HasDecorator<ReadOnlyVdvm>();
    }
}

public class UnitVdvm : ViewDecoratorVm
{
    public string Unit { get; set; } = string.Empty;
}

public abstract class RangeVdvm : ViewDecoratorVm
{
    public double Minimum { get; set; }
    public double Maximum { get; set; }
}

public class LimitRangeVdvm : RangeVdvm
{
}

public class WarningRangeVdvm : RangeVdvm
{
}

public class PrecisionVdvm : ViewDecoratorVm
{
    public int Precision { get; set; }
}

public class StepVdvm : ViewDecoratorVm
{
    public double Step { get; set; }
}
