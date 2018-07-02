//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Services/ViewService/ViewService.cs

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using BluetoothLEExplorer.Mvvm.Common;

using static BluetoothLEExplorer.Mvvm.Services.LoggingService.LoggingService;

namespace BluetoothLEExplorer.Mvvm.Services.ViewService
{
    public sealed class ViewService : IViewService
    {
        internal static void OnWindowCreated()
        {
            var view = CoreApplication.GetCurrentView();
            if (!view.IsMain && !view.IsHosted)
            {
                var control = ViewLifetimeControl.GetForCurrentView();
                //This one time it should be made manually, as after Consolidate event fires the inner reference number should become zero
                control.StartViewInUse();
                //This is necessary to not make control.StartViewInUse()/control.StopViewInUse() manually on each and every async call. Facade will do it for you
                SynchronizationContext.SetSynchronizationContext(new SecondaryViewSynchronizationContextDecorator(control,
                    SynchronizationContext.Current));
            }
        }

        public async Task<ViewLifetimeControl> OpenAsync(Type page, object parameter = null, string title = null,
            ViewSizePreference size = ViewSizePreference.UseHalf)
        {
            WriteLine($"Page: {page}, Parameter: {parameter}, Title: {title}, Size: {size}");

            var currentView = ApplicationView.GetForCurrentView();
            title = title ?? currentView.Title;

            var newView = CoreApplication.CreateNewView();
            var dispatcher = new DispatcherWrapper(newView.Dispatcher);
            var newControl = await dispatcher.Dispatch(async () =>
            {
                var control = ViewLifetimeControl.GetForCurrentView();
                var newWindow = Window.Current;
                var newAppView = ApplicationView.GetForCurrentView();
                newAppView.Title = title;
                var nav = BootStrapper.Current.NavigationServiceFactory(BootStrapper.BackButton.Ignore, BootStrapper.ExistingContent.Exclude);
                control.NavigationService = nav;
                nav.Navigate(page, parameter);
                newWindow.Content = nav.Frame;
                newWindow.Activate();

                await ApplicationViewSwitcher
                    .TryShowAsStandaloneAsync(newAppView.Id, ViewSizePreference.Default, currentView.Id, size);
                return control;
            }).ConfigureAwait(false);
            return newControl;
        }
    }
}
