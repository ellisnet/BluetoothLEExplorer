//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Common/IDispatcherWrapper.cs

using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BluetoothLEExplorer.Mvvm.Common
{
    public interface IDispatcherWrapper
    {
        void Dispatch(Action action, int delayms = 0, DispatcherPriority priority = DispatcherPriority.Normal);
        Task DispatchAsync(Func<Task> func, int delayms = 0, DispatcherPriority priority = DispatcherPriority.Normal);
        Task DispatchAsync(Action action, int delayms = 0, DispatcherPriority priority = DispatcherPriority.Normal);
        Task<T> DispatchAsync<T>(Func<T> func, int delayms = 0, DispatcherPriority priority = DispatcherPriority.Normal);

        void DispatchIdle(Action action, int delayms = 0);
        Task DispatchIdleAsync(Func<Task> func, int delayms = 0);
        Task DispatchIdleAsync(Action action, int delayms = 0);
        Task<T> DispatchIdleAsync<T>(Func<T> func, int delayms = 0);

        bool HasThreadAccess();
    }
}
