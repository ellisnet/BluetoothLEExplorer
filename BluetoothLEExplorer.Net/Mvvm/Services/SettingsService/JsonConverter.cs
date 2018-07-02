//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Services/SettingsService/JsonConverter.cs

using System;

using Newtonsoft.Json;

namespace BluetoothLEExplorer.Mvvm.Services.SettingsService
{
    public class JsonConverter : IStoreConverter
    {
        public object FromStore(string value, Type type) => JsonConvert.DeserializeObject(value, type);
        public string ToStore(object value, Type type) => JsonConvert.SerializeObject(value, Formatting.None);
    }
}
