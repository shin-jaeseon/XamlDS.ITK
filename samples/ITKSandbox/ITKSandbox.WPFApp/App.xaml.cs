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

        LoadTheme("Dark", "S06");

        var host = hostBuilder.Build();
        host.Start();

        MainWindow = host.Services.GetRequiredService<DesktopWindowView>();
        MainWindow.DataContext = host.Services.GetRequiredService<MainWindowVm>();
        MainWindow.Show();

        base.OnStartup(e);

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

    private void LoadTheme(string color, string sizePreset)
    {
        Resources.MergedDictionaries.Clear();

        Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri($"pack://application:,,,/XamlDS.ITK.WPF.Foundation;component/Themes/Color.{color}.xaml", UriKind.Absolute)
        });

        Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri($"pack://application:,,,/XamlDS.ITK.WPF.Foundation;component/Themes/SizePreset.{sizePreset}.xaml", UriKind.Absolute)
        });

        Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/XamlDS.ITK.WPF.Foundation;component/Themes/Styles.xaml", UriKind.Absolute)
        });
    }
}

