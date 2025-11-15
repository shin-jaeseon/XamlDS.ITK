using XamlDS.ITK.ViewModels.Commands;

namespace XamlDS.ITK.WPF.ViewModels.Commands
{
    public class WPFTestCvm : TestCvm
    {
        public override bool CanExecute(object? parameter)
        {
            return false;
        }
        public override void Execute(object? parameter)
        {
        }
    }
}
