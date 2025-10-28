using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace XamlDS.ITK;

public abstract class ITKLibrary
{
    public virtual void Register(IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(AddServices);
    }

    protected virtual void AddServices(HostBuilderContext context, IServiceCollection services)
    {

    }
}
