using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XamlDS.ITK.Common;
using XamlDS.ITK.ViewModels;
using XamlDS.ITK.ViewModels.Commands;
using XamlDS.ITK.ViewModels.Panes;

namespace XamlDS.ITK;

public sealed class ITKCoreLibrary : ITKLibrary
{
    protected override void AddServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddSingleton<EnvironmentConfig>();
        services.AddSingleton<AppSettingsVm>();
        services.AddSingleton<AppStatesVm>();
        services.AddTransient<DesktopWindowVm>();

        services.AddSingleton<SystemCommandsVm>();
        services.AddTransient<ApplicationBarPvm>();
    }
}
