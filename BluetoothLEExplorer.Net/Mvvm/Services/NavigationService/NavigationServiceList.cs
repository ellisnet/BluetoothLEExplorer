//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Services/NavigationService/NavigationServiceList.cs

using System.Collections.Generic;
using System.Linq;

namespace BluetoothLEExplorer.Mvvm.Services.NavigationService
{
    public class NavigationServiceList : List<INavigationService>
    {
        public INavigationService GetByFrameId(string frameId) => this.FirstOrDefault(x => x.FrameFacade.FrameId == frameId);
    }
}
