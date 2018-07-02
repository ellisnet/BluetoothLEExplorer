using System;
using System.Collections.Generic;

//TODO: Need to implement a method for storing application data here eventually

namespace BluetoothLEExplorer.Mvvm.Services.ApplicationStorage
{
    public class ApplicationDataContainer : IApplicationDataContainer
    {
        public string Name { get; }
        public ApplicationDataLocality Locality { get; }
        public IPropertySet Values { get; }
        public IReadOnlyDictionary<string, ApplicationDataContainer> Containers { get; }
        public ApplicationDataContainer CreateContainer(string name, ApplicationDataCreateDisposition disposition)
        {
            throw new NotImplementedException();
        }

        public void DeleteContainer(string name)
        {
            throw new NotImplementedException();
        }
    }
}
