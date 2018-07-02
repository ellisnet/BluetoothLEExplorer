//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Services/SerializationService/SerializationService.cs

namespace BluetoothLEExplorer.Mvvm.Services.SerializationService
{
    public static class SerializationService
    {
        private static ISerializationService _json;
        public static ISerializationService Json => _json ?? (_json = new JsonSerializationService());
    }
}
