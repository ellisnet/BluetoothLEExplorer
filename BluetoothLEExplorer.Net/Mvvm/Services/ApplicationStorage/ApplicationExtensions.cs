using System.Windows;

namespace BluetoothLEExplorer.Mvvm.Services.ApplicationStorage
{
    public static class ApplicationExtensions
    {
        private static readonly IApplicationDataContainer _localSettingsContainer = new ApplicationDataContainer();
        private static readonly IApplicationDataContainer _roamingSettingsContainer = new ApplicationDataContainer();

        public static IApplicationDataContainer GetLocalSettingsContainer(this Application application)
            => (application == null) ? null : _localSettingsContainer;

        public static IApplicationDataContainer GetRoamingSettingsContainer(this Application application)
            => (application == null) ? null : _roamingSettingsContainer;
    }
}
