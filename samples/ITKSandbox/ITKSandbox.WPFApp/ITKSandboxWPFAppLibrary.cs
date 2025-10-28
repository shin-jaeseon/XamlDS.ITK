using ITKSandbox.Main;
using Microsoft.Extensions.Hosting;
using XamlDS.ITK;
using XamlDS.ITK.WPF;

namespace ITKSandbox.WPFApp;

public class ITKSandboxWPFAppLibrary : ITKLibrary
{
    public override void Register(IHostBuilder hostBuilder)
    {
        var wpfLib = new ITKWPFLibrary();
        wpfLib.Register(hostBuilder);
        var mainLib = new ITKSandboxMainLibrary();
        mainLib.Register(hostBuilder);
    }
}
