//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Services/NavigationService/INavigationService.cs

using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using BluetoothLEExplorer.Mvvm.Common;
using BluetoothLEExplorer.Mvvm.Services.ViewService;

namespace BluetoothLEExplorer.Mvvm.Services.NavigationService
{
    public interface INavigationService
    {
        void GoBack();
        void GoForward();

        object Content { get; }

        void Navigate(Type page, object parameter = null);
        void Navigate<T>(T key, object parameter = null) where T : struct, IConvertible;

        Task<bool> NavigateAsync(Type page, object parameter = null);
        Task<bool> NavigateAsync<T>(T key, object parameter = null) where T : struct, IConvertible;

        bool CanGoBack { get; }
        bool CanGoForward { get; }

        string NavigationState { get; set; }

        void Refresh();

        void Refresh(object param);



        Task<ViewLifetimeControl> OpenAsync(Type page, object parameter = null, string title = null, ViewSizePreference size = ViewSizePreference.UseHalf);

        object CurrentPageParam { get; }
        Type CurrentPageType { get; }

        DispatcherWrapper Dispatcher { get; }

        [Obsolete]
        Task SaveNavigationAsync();

        [Obsolete]
        Task<bool> RestoreSavedNavigationAsync();

        Task SaveAsync();

        Task<bool> LoadAsync();

        event TypedEventHandler<Type> AfterRestoreSavedNavigation;

        void ClearHistory();
        void ClearCache(bool removeCachedPagesInBackStack = false);

        Task SuspendingAsync();
        void Resuming();

        Frame Frame { get; }
        FrameFacade FrameFacade { get; }

        /// <summary>
        /// Specifies if this instance of INavigationService associated with <see cref="CoreApplication.MainView"/> or any other secondary view.
        /// </summary>
        /// <returns><value>true</value> if associated with MainView, <value>false</value> otherwise</returns>
        bool IsInMainView { get; }
    }
}
