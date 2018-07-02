//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Services/NavigationService/INavigable.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using BluetoothLEExplorer.Mvvm.Common;

namespace BluetoothLEExplorer.Mvvm.Services.NavigationService
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-NavigationService
    public interface INavigable
    {
        Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state);
        Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending);
        Task OnNavigatingFromAsync(NavigatingEventArgs args);
        INavigationService NavigationService { get; set; }
        IDispatcherWrapper Dispatcher { get; set; }
        IStateItems SessionState { get; set; }
    }
}
