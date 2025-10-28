
using Microsoft.Extensions.Hosting;

namespace XamlDS.ITK.WPF;

public class ITKWPFLibrary : ITKLibrary
{
    public override void Register(IHostBuilder hostBuilder)
    {
        var coreLib = new ITKCoreLibrary();
        coreLib.Register(hostBuilder);

        var ITKWPFFoundationLibrary = new ITKWPFFoundationLibrary();
        ITKWPFFoundationLibrary.Register(hostBuilder);
    }
}

