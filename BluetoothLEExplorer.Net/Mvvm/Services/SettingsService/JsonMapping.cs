//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Services/SettingsService/JsonMapping.cs

using System;

namespace BluetoothLEExplorer.Mvvm.Services.SettingsService
{
    public class JsonMapping : IPropertyMapping
    {
        protected IStoreConverter jsonConverter = new JsonConverter();
        public IStoreConverter GetConverter(Type type) => this.jsonConverter;
    }
}
