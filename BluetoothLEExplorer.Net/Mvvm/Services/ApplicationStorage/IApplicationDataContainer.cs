using System.Collections.Generic;

namespace BluetoothLEExplorer.Mvvm.Services.ApplicationStorage
{
    public enum ApplicationDataLocality
    {
        Local,
        Roaming,
        Temporary,
        LocalCache,
    }

    public enum ApplicationDataCreateDisposition
    {
        Always,
        Existing,
    }

    public interface IApplicationDataContainer
    {
        string Name { get; }
        ApplicationDataLocality Locality { get; }
        IPropertySet Values { get; }
        IReadOnlyDictionary<string, ApplicationDataContainer> Containers { get; }
        ApplicationDataContainer CreateContainer(string name, ApplicationDataCreateDisposition disposition);
        void DeleteContainer(string name);
    }
}
