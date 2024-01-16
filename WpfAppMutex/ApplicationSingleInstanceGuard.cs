using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows;

namespace WpfAppMutex;

internal class ApplicationSingleInstanceGuard
{
    private Mutex? _mutex;

    internal void OnStartUp(string id)
    {
        var mutexName = $"Global\\{id}";
        
        var mutexSecurity = new MutexSecurity();
        mutexSecurity.AddAccessRule(new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
            MutexRights.FullControl, AccessControlType.Allow));

        _mutex = MutexAcl.Create(initiallyOwned: false, mutexName, out var newInstanceCreated, mutexSecurity);

        if (newInstanceCreated) return;

        MessageBox.Show("Another instance of the application is already running.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        _mutex.Close();
        Environment.Exit(0);
    }

    internal void OnExit()
    {
        _mutex?.ReleaseMutex();
        _mutex?.Close();
    }
}