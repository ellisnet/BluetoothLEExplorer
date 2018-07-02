using System.Collections.Generic;

namespace BluetoothLEExplorer.Mvvm.Services.ApplicationStorage
{
    public interface IPropertySet : IObservableMap<string, object>, IDictionary<string, object>, IEnumerable<KeyValuePair<string, object>>
    {
    }
}
