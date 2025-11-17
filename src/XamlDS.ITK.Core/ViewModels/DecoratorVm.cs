namespace XamlDS.ITK.ViewModels;

public abstract class DecoratorVm : ViewModelBase
{
}

//public class ReadOnlyVdvm : DecoratorVm
//{
//}

//public class HiddenVdvm : DecoratorVm
//{
//}

//public static class HiddenExtensions
//{
//    /// <summary>
//    /// Adds or removes the <see cref="HiddenVdvm"/> decorator. Returns true if a change was made.
//    /// </summary>
//    public static bool SetHidden(this IDecoratorHost host, bool isHidden)
//    {
//        if (host is null) throw new ArgumentNullException(nameof(host));
//        if (isHidden)
//        {
//            if (host.HasDecorator<HiddenVdvm>()) return false;
//            host.EnsureDecorator<HiddenVdvm>();
//            return true;
//        }
//        else
//        {
//            return host.RemoveDecorator<HiddenVdvm>();
//        }
//    }

//    /// <summary>
//    /// Returns true when a <see cref="HiddenVdvm"/> decorator exists.
//    /// </summary>
//    public static bool IsHidden(this IDecoratorHost host)
//    {
//        if (host is null) throw new ArgumentNullException(nameof(host));
//        return host.HasDecorator<HiddenVdvm>();
//    }
//}

//public static class ReadOnlyExtensions
//{
//    /// <summary>
//    /// Adds or removes the <see cref="ReadOnlyVdvm"/> decorator. Returns true if a change was made.
//    /// </summary>
//    public static bool SetReadOnly(this IDecoratorHost host, bool isReadOnly)
//    {
//        if (host is null) throw new ArgumentNullException(nameof(host));
//        if (isReadOnly)
//        {
//            if (host.HasDecorator<ReadOnlyVdvm>()) return false;
//            host.EnsureDecorator<ReadOnlyVdvm>();
//            return true;
//        }
//        else
//        {
//            return host.RemoveDecorator<ReadOnlyVdvm>();
//        }
//    }

//    /// <summary>
//    /// Returns true when a <see cref="ReadOnlyVdvm"/> decorator exists.
//    /// </summary>
//    public static bool IsReadOnly(this IDecoratorHost host)
//    {
//        if (host is null) throw new ArgumentNullException(nameof(host));
//        return host.HasDecorator<ReadOnlyVdvm>();
//    }
//}








