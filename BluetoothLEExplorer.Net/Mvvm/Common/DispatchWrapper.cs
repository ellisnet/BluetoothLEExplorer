//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Common/DispatcherWrapper.cs

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BluetoothLEExplorer.Mvvm.Common
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-DispatcherWrapper
    public class DispatcherWrapper : IDispatcherWrapper
    {
        #region Debug

        static void DebugWrite(string text = null, Services.LoggingService.Severities severity = Services.LoggingService.Severities.Template10, [CallerMemberName]string caller = null) =>
            Services.LoggingService.LoggingService.WriteLine(text, severity, caller: $"DispatcherWrapper.{caller}");

        #endregion

        public static IDispatcherWrapper Current() => WindowWrapper.Current().Dispatcher;

        internal DispatcherWrapper(Dispatcher dispatcher)
        {
            DebugWrite(caller: "Constructor");
            this.dispatcher = dispatcher;
        }

        public bool HasThreadAccess() => dispatcher.CheckAccess();

        private readonly Dispatcher dispatcher;

        public async Task DispatchAsync(Action action, int delayms = 0, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (delayms > 0)
                await Task.Delay(delayms).ConfigureAwait(false);

            if (dispatcher.CheckAccess() && priority == DispatcherPriority.Normal)
            {
                action();
            }
            else
            {
                var tcs = new TaskCompletionSource<object>();
                await dispatcher.InvokeAsync(async () =>
                {
                    await Task.Run(() =>
                    {
                        try
                        {
                            action();
                            tcs.TrySetResult(null);
                        }
                        catch (Exception ex)
                        {
                            tcs.TrySetException(ex);
                        }
                    }).ConfigureAwait(false);
                }, priority);
                await tcs.Task.ConfigureAwait(false);
            }
        }

        public async Task DispatchAsync(Func<Task> func, int delayms = 0, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (delayms > 0)
                await Task.Delay(delayms).ConfigureAwait(false);

            if (dispatcher.CheckAccess() && priority == DispatcherPriority.Normal)
            {
                await func().ConfigureAwait(false);
            }
            else
            {
                var tcs = new TaskCompletionSource<object>();
                await dispatcher.InvokeAsync(async () =>
                {
                    await Task.Run(async () =>
                    {
                        try
                        {
                            await func().ConfigureAwait(false);
                            tcs.TrySetResult(null);
                        }
                        catch (Exception ex)
                        {
                            tcs.TrySetException(ex);
                        }
                    }).ConfigureAwait(false);
                }, priority);
                await tcs.Task.ConfigureAwait(false);
            }
        }

        public async Task<T> DispatchAsync<T>(Func<T> func, int delayms = 0, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (delayms > 0)
                await Task.Delay(delayms).ConfigureAwait(false);

            if (dispatcher.CheckAccess() && priority == DispatcherPriority.Normal)
            {
                return func();
            }
            else
            {
                var tcs = new TaskCompletionSource<T>();
                await dispatcher.InvokeAsync(async () =>
                {
                    await Task.Run(() =>
                    {
                        try
                        {
                            tcs.TrySetResult(func());
                        }
                        catch (Exception ex)
                        {
                            tcs.TrySetException(ex);
                        }
                    }).ConfigureAwait(false);
                }, priority);
                return await tcs.Task.ConfigureAwait(false);
            }
        }

        public async void Dispatch(Action action, int delayms = 0, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (delayms > 0)
                await Task.Delay(delayms).ConfigureAwait(false);

            if (dispatcher.CheckAccess() && priority == DispatcherPriority.Normal)
            {
                action();
            }
            else
            {
                await dispatcher.InvokeAsync(async () =>
                {
                    await Task.Run(() =>
                    {
                        action.Invoke();
                    }).ConfigureAwait(false);
                }, priority);
            }
        }

        public T Dispatch<T>(Func<T> action, int delayms = 0, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (delayms > 0)
                Task.Delay(delayms).ConfigureAwait(false).GetAwaiter().GetResult();

            if (dispatcher.CheckAccess() && priority == DispatcherPriority.Normal)
            {
                return action();
            }
            else
            {
                var tcs = new TaskCompletionSource<T>();
                dispatcher.InvokeAsync(async () =>
                {
                    await Task.Run(() =>
                    {
                        try
                        {
                            tcs.TrySetResult(action());
                        }
                        catch (Exception ex)
                        {
                            tcs.TrySetException(ex);
                        }
                    }).ConfigureAwait(false);
                }, priority);
                return tcs.Task.ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        public async Task DispatchIdleAsync(Action action, int delayms = 0)
        {
            if (delayms > 0)
                await Task.Delay(delayms).ConfigureAwait(false);

            var tcs = new TaskCompletionSource<object>();
            await dispatcher.InvokeAsync(async () =>
            {
                await Task.Run(() =>
                {
                    try
                    {
                        action();
                        tcs.TrySetResult(null);
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }).ConfigureAwait(false);
            }, DispatcherPriority.ApplicationIdle);
            await tcs.Task.ConfigureAwait(false);
        }

        public async Task DispatchIdleAsync(Func<Task> func, int delayms = 0)
        {
            if (delayms > 0)
                await Task.Delay(delayms).ConfigureAwait(false);

            var tcs = new TaskCompletionSource<object>();
            await dispatcher.InvokeAsync(async () =>
            {
                await Task.Run(async () =>
                {
                    try
                    {
                        await func().ConfigureAwait(false);
                        tcs.TrySetResult(null);
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }).ConfigureAwait(false);
            }, DispatcherPriority.ApplicationIdle);
            await tcs.Task.ConfigureAwait(false);
        }

        public async Task<T> DispatchIdleAsync<T>(Func<T> func, int delayms = 0)
        {
            if (delayms > 0)
                await Task.Delay(delayms).ConfigureAwait(false);

            var tcs = new TaskCompletionSource<T>();
            await dispatcher.InvokeAsync(async () =>
            {
                await Task.Run(() =>
                {
                    try
                    {
                        tcs.TrySetResult(func());
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }).ConfigureAwait(false);
            }, DispatcherPriority.ApplicationIdle);
            return await tcs.Task.ConfigureAwait(false);
        }

        public async void DispatchIdle(Action action, int delayms = 0)
        {
            if (delayms > 0)
                await Task.Delay(delayms).ConfigureAwait(false);

            await dispatcher.InvokeAsync(async () =>
            {
                await Task.Run(() =>
                {
                    action.Invoke();
                }).ConfigureAwait(false);
            }, DispatcherPriority.ApplicationIdle);
        }

        public T DispatchIdle<T>(Func<T> action, int delayms = 0) where T : class
        {
            if (delayms > 0)
                Task.Delay(delayms).ConfigureAwait(false).GetAwaiter().GetResult();

            var tcs = new TaskCompletionSource<T>();
            dispatcher.InvokeAsync(async () =>
            {
                await Task.Run(() =>
                {
                    try
                    {
                        tcs.TrySetResult(action());
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }).ConfigureAwait(false);
            }, DispatcherPriority.ApplicationIdle);
            return tcs.Task.ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
