namespace XamlDS.ITK.ViewModels.Commands;

public abstract class TestCvm : CommandVm
{
    public TestCvm() : base("TestCommand")
    {
        DisplayName = "Test";
        Description = "A command for testing purposes.";
    }
}
