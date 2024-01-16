using System.Reflection;
using System.Windows;

namespace WpfAppMutex;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private readonly ApplicationSingleInstanceGuard _applicationSingleInstanceGuard = new();

    protected override void OnStartup(StartupEventArgs e)
    {
        var appName = Assembly.GetExecutingAssembly().GetName().Name ?? "Moav.App";
        _applicationSingleInstanceGuard.OnStartUp(appName);
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _applicationSingleInstanceGuard.OnExit();
        base.OnExit(e);
    }
}