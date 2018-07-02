//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Common/Deferral.cs

using System;
using System.Threading.Tasks;

namespace BluetoothLEExplorer.Mvvm.Common
{
    public sealed class DeferralManager
    {
        int _count = 0;
        TaskCompletionSource<object> _completed = new TaskCompletionSource<object>();
        public Deferral GetDeferral()
        {
            System.Threading.Interlocked.Increment(ref _count);
            return new Deferral(() =>
            {
                var count = System.Threading.Interlocked.Decrement(ref _count);
                if (count == 0) _completed.SetResult(null);
            });
        }
        public bool IsComplete()
        {
            return WaitForDeferralsAsync().IsCompleted;
        }
        public Task WaitForDeferralsAsync()
        {
            if (_count == 0) return Task.CompletedTask;
            return _completed.Task;
        }
    }

    public sealed class Deferral
    {
        private Action _callback;
        public Deferral(Action callback)
        {
            _callback = callback;
        }
        public void Complete()
        {
            _callback.Invoke();
        }
    }
}
