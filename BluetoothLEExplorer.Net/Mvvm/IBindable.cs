//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Mvvm/IBindable.cs

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BluetoothLEExplorer.Mvvm
{
    // this exists for the future implementation of the INPC property attribute
    public interface IBindable : INotifyPropertyChanged
    {
        void RaisePropertyChanged([CallerMemberName]string propertyName = null);
    }
}
