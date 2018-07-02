//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Services/SettingsService/IStoreConverter.cs

using System;

namespace BluetoothLEExplorer.Mvvm.Services.SettingsService
{
    public interface IStoreConverter
    {
        string ToStore(object value, Type type);
        object FromStore(string value, Type type);
    }
}
