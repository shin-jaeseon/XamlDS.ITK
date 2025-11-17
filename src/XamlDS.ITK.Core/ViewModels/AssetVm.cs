namespace XamlDS.ITK.ViewModels;

public abstract class AssetVm(string name) : DecoratorHostVm
{
    private readonly string _name = name;

    /// <summary>
    /// The programmatic identifier for this object.
    /// </summary>
    /// <remarks>
    /// It's used as an identifier in logs and for debugging purposes.
    /// </remarks>
    public string Name { get => _name; }
}
