//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Common/TypedEventHandler.cs

using System;
using System.ComponentModel;

namespace BluetoothLEExplorer.Mvvm.Common
{
    public delegate void TypedEventHandler<T>(object sender, T e);

    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }

    public class CancelEventArgs<T> : CancelEventArgs
    {
        public CancelEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }

    public class DeferredEventArgs : EventArgs
    {
        DeferralManager Manager;
        public DeferredEventArgs(DeferralManager manager)
        {
            Manager = manager;
        }

        public Deferral GetDeferral() => Manager.GetDeferral();
    }
}
