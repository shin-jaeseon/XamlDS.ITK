
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XamlDS.ITK.ViewModels.Commands;
using XamlDS.ITK.WPF.ViewModels.Commands;
using XamlDS.ITK.WPF.Views;

namespace XamlDS.ITK.WPF;

public class ITKWPFFoundationLibrary : ITKLibrary
{
    protected override void AddServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddSingleton<ExitApplicationCvm, WPFExitApplicationCvm>();
        services.AddTransient<DesktopWindowView>();
    }
}

