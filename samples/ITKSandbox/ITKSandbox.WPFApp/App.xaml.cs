using ITKSandbox.Main.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using XamlDS.ITK.WPF.Views;

namespace ITKSandbox.WPFApp;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var hostBuilder = Host.CreateDefaultBuilder(e.Args);
        var appLib = new ITKSandboxWPFAppLibrary();
        appLib.Register(hostBuilder);

        var host = hostBuilder.Build();
        host.Start();

        MainWindow = host.Services.GetRequiredService<DesktopWindowView>();
        MainWindow.DataContext = host.Services.GetRequiredService<MainWindowVm>();
        MainWindow.Show();

        base.OnStartup(e);

        //var windowVm =

        //var host = Host.CreateDefaultBuilder(e.Args)
        //    .UseContentRoot(AppContext.BaseDirectory)
        //    .ConfigureAppConfiguration((context, config) =>
        //    {
        //        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        //        config.AddEnvironmentVariables();
        //        if (e.Args != null)
        //        {
        //            config.AddCommandLine(e.Args);
        //        }
        //    })
        //    .Build();
    }
}

