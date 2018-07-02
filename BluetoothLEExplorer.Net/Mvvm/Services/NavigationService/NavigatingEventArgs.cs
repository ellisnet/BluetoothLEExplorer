//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Services/NavigationService/NavigatingEventArgs.cs

using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;
using BluetoothLEExplorer.Mvvm.Common;

namespace BluetoothLEExplorer.Mvvm.Services.NavigationService
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-NavigationService
    public class NavigatingEventArgs : NavigatedEventArgs
    {
        DeferralManager Manager;
        public Deferral GetDeferral() => Manager.GetDeferral();

        public NavigatingEventArgs(DeferralManager manager) : base()
        {
            Manager = manager;
        }

        public NavigatingEventArgs(DeferralManager manager, NavigatingCancelEventArgs e, Page page, Type targetPageType, object parameter, object targetPageParameter) : this(manager)
        {
            Debugger.Break();  //TODO: Need to check and see if the properties below are being set correctly
            NavigationMode = e.NavigationMode;
            PageType = page.GetType(); //e.SourcePageType;
            Page = page;
            Parameter = parameter;
            TargetPageType = targetPageType;
            TargetPageParameter = targetPageParameter;
        }

        public bool Cancel { get; set; } = false;
        public bool Suspending { get; set; } = false;
        public Type TargetPageType { get; set; }
        public object TargetPageParameter { get; set; }
    }
}
