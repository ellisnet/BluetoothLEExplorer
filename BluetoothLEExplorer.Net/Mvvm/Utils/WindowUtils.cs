//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Utils/Template10Utils.cs

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using BluetoothLEExplorer.Mvvm.Common;
using BluetoothLEExplorer.Mvvm.Services.NavigationService;

namespace BluetoothLEExplorer.Mvvm.Utils
{
    public static class WindowUtils
    {
        public static INavigationService GetNavigationService(this Frame frame)
            => NavigationService.GetForFrame(frame);

        public static async Task<bool> NavigateAsyncEx(this Frame frame, Type page, object parameter = null)
            => await frame.GetNavigationService().NavigateAsync(page, parameter);

        public static async Task<bool> NavigateAsyncEx<T>(this Frame frame, T key, object parameter = null) where T : struct, IConvertible
            => await frame.GetNavigationService().NavigateAsync(key, parameter);

        public static WindowWrapper GetWindowWrapper(this INavigationService service)
            => WindowWrapper.ActiveWrappers.FirstOrDefault(x => x.NavigationServices.Contains(service));

        public static IDispatcherWrapper GetDispatcherWrapper(this INavigationService service)
            => service.GetWindowWrapper().Dispatcher;

        public static IDispatcherWrapper GetDispatcherWrapper(this Dispatcher wrapper)
            => new DispatcherWrapper(wrapper);

        /// <summary>
        /// Returns a list of submenu buttons with the same GroupName attribute as the command button upon which this
        /// extension is invoked (which is treated as Parent command button).
        /// </summary>
        /// <returns>Submenu buttons in List&lt;HamburgerButtonInfo&gt;. If no submenu buttons found,  List is still returned with element count of 0. </returns>
        /// <remarks>
        /// For added convenience, the GroupName attribute is detected with string.StartWith(groupName) rather than
        /// the straightforward string.Equals(groupName). That way we can tag submenu buttons as groupName1, groupName2, 
        /// groupName3, etc. With this scheme, the parent command button should be named by subset string, 
        /// which in this case is groupName.
        /// You don't have to use this scheme in which case you just stick to a single groupName for all buttons.
        /// </remarks>

        //public static List<HamburgerButtonInfo> ItemsInGroup(this HamburgerButtonInfo button, bool IncludeSecondaryButtons = false)
        //{
        //    string groupName = button.GroupName?.ToString();

        //    // Return 0 count List rather than null
        //    if (string.IsNullOrWhiteSpace(groupName)) return new List<HamburgerButtonInfo>();

        //    FrameworkElement fe = button.Content as FrameworkElement;
        //    HamburgerMenu hamMenu = fe.FirstAncestor<HamburgerMenu>();

        //    List<HamburgerButtonInfo> NavButtons = hamMenu.PrimaryButtons.ToList();
        //    if (IncludeSecondaryButtons) NavButtons.InsertRange(NavButtons.Count, hamMenu.SecondaryButtons.ToList());

        //    List<HamburgerButtonInfo> groupItems = NavButtons.Where(x => !x.Equals(button) && (x.GroupName?.ToString()?.StartsWith(groupName) ?? false)).ToList();

        //    return groupItems;
        //}
    }
}
