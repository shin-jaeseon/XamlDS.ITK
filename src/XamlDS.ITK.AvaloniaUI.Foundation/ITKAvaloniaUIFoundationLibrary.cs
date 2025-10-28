using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XamlDS.ITK.AvaloniaUI.Views;

namespace XamlDS.ITK.AvaloniaUI;

public class ITKAvaloniaUIFoundationLibrary : ITKLibrary
{
    protected override void AddServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddTransient<DesktopWindowView>();
    }
}
