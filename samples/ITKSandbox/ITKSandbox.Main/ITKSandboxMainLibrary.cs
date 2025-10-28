using ITKSandbox.Main.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XamlDS.ITK;

namespace ITKSandbox.Main;

public class ITKSandboxMainLibrary : ITKLibrary
{
    protected override void AddServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddTransient<MainWindowVm>();
        services.AddTransient<MainPanelVm>();
    }
}
