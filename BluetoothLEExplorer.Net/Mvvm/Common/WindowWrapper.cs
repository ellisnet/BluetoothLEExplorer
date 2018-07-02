﻿//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Common/WindowWrapper.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using BluetoothLEExplorer.Mvvm.Services.NavigationService;

namespace BluetoothLEExplorer.Mvvm.Common
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-WindowWrapper
    public class WindowWrapper
    {
        #region Debug

        static void DebugWrite(string text = null, Services.LoggingService.Severities severity = Services.LoggingService.Severities.Template10, [CallerMemberName]string caller = null) =>
            Services.LoggingService.LoggingService.WriteLine(text, severity, caller: $"WindowWrapper.{caller}");

        #endregion

        static WindowWrapper()
        {
            DebugWrite(caller: "Static Constructor");
        }

        public WindowWrapper()
        {
            DebugWrite(caller: "Constructor");
        }

        public static WindowWrapper Default()
        {
            try
            {
                var mainDispatcher = Application.Current.Dispatcher;
                return ActiveWrappers.FirstOrDefault(x => x.Window.Dispatcher == mainDispatcher) ??
                        ActiveWrappers.FirstOrDefault();
            }
            catch (COMException)
            {
                //MainView might exist but still be not accessible
                return ActiveWrappers.FirstOrDefault();
            }
        }

        public object Content => Dispatcher.Dispatch(() => Window.Content);

        public static readonly List<WindowWrapper> ActiveWrappers = new List<WindowWrapper>();

        //public static WindowWrapper Current() => ActiveWrappers.FirstOrDefault(x => x.Window ==  Window.Current) ?? Default();
        public static WindowWrapper Current() => ActiveWrappers.FirstOrDefault(x => x.Window == Application.Current.MainWindow) ?? Default();

        public static WindowWrapper Current(Window window) => ActiveWrappers.FirstOrDefault(x => x.Window == window);

        public static WindowWrapper Current(INavigationService nav) => ActiveWrappers.FirstOrDefault(x => x.NavigationServices.Contains(nav));

        //public DisplayInformation DisplayInformation() => Dispatcher.Dispatch(() => Windows.Graphics.Display.DisplayInformation.GetForCurrentView());

        //public ApplicationView ApplicationView() => Dispatcher.Dispatch(() => Windows.UI.ViewManagement.ApplicationView.GetForCurrentView());

        //public UIViewSettings UIViewSettings() => Dispatcher.Dispatch(() => Windows.UI.ViewManagement.UIViewSettings.GetForCurrentView());

        internal WindowWrapper(Window window)
        {
            if (Current(window) != null)
            {
                throw new Exception("Windows already has a wrapper; use Current(window) to fetch.");
            }
            Window = window;
            ActiveWrappers.Add(this);
            Dispatcher = new DispatcherWrapper(window.Dispatcher);
            window.Owner.Closed += (s, e) =>
            {
                ActiveWrappers.Remove(this);
            };
            window.Closed += (s, e) =>
            {
                ActiveWrappers.Remove(this);
            };
        }

        public void Close() { Window.Close(); }
        public Window Window { get; }
        public DispatcherWrapper Dispatcher { get; }
        public NavigationServiceList NavigationServices { get; } = new NavigationServiceList();
    }
}
