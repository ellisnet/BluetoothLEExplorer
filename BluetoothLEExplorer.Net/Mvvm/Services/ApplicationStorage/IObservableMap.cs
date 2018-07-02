using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BluetoothLEExplorer.Mvvm.Services.ApplicationStorage
{
    public delegate void MapChangedEventHandler<K, V>(IObservableMap<K, V> sender, IMapChangedEventArgs<K> @event);

    public enum CollectionChange
    {
        Reset,
        ItemInserted,
        ItemRemoved,
        ItemChanged,
    }

    public interface IMapChangedEventArgs<K>
    {
        CollectionChange CollectionChange { [MethodImpl] get; }
        K Key { [MethodImpl] get; }
    }

    public interface IObservableMap<K, V> : IDictionary<K, V>
    {
        event MapChangedEventHandler<K, V> MapChanged;
    }
}
