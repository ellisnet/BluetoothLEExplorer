//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Services/NavigationService/NavigatedEventArgs.cs

using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BluetoothLEExplorer.Mvvm.Services.NavigationService
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-NavigationService
    public class NavigatedEventArgs : EventArgs
    {
        public NavigatedEventArgs() { }
        public NavigatedEventArgs(NavigationEventArgs e, Page page)
        {
            Debugger.Break();  //TODO: Need to check and see if the properties below are being set correctly
            Page = page;
            PageType = page.GetType(); //e.SourcePageType;
            Parameter = e.ExtraData; //e.Parameter;
            //NavigationMode = e.NavigationMode; //TODO: Need to figure out where to get this from - maybe from Navigator?
        }

        public NavigationMode NavigationMode { get; set; }
        public Type PageType { get; set; }
        public object Parameter { get; set; }
        public Page Page { get; set; }
    }
}
