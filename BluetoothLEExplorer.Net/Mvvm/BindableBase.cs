//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Mvvm/BindableBase.cs

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;
using BluetoothLEExplorer.Mvvm.Utils;

namespace BluetoothLEExplorer.Mvvm
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-MVVM
    public abstract class BindableBase : IBindable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual bool Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (object.Equals(storage, value))
                return false;

            storage = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        //This shouldn't be needed, but the code below complains if you try to pass a lambda action directly to BeginInvoke
        private Action GetAction(Action action) => action;

        public virtual void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            //if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            //    return;

            var handler = PropertyChanged;
            if (!object.Equals(handler, null))
            {
                var args = new PropertyChangedEventArgs(propertyName);
                var dispatcher = Application.Current?.Dispatcher;
                if (dispatcher != null)
                {
                    if (dispatcher.CheckAccess())
                    {
                        try
                        {
                            handler.Invoke(this, args);
                        }
                        catch
                        {
                            dispatcher.BeginInvoke(DispatcherPriority.Normal, GetAction(() => handler.Invoke(this, args)));
                        }
                    }
                    else
                    {
                        dispatcher.BeginInvoke(DispatcherPriority.Normal, GetAction(() => handler.Invoke(this, args)));
                    }
                }
            }
        }

        public virtual bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            if (object.Equals(field, newValue))
                return false;

            field = newValue;
            RaisePropertyChanged(propertyExpression);
            return true;
        }

        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            //if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            //    return;

            var handler = PropertyChanged;
            if (!object.Equals(handler, null))
            {
                var propertyName = ExpressionUtils.GetPropertyName(propertyExpression);
                if (!object.Equals(propertyName, null))
                {
                    var args = new PropertyChangedEventArgs(propertyName);
                    var dispatcher = Application.Current?.Dispatcher;
                    if (dispatcher != null)
                    {
                        if (dispatcher.CheckAccess())
                        {
                            try
                            {
                                handler.Invoke(this, args);
                            }
                            catch
                            {
                                dispatcher.BeginInvoke(DispatcherPriority.Normal, GetAction(() => handler.Invoke(this, args)));
                            }
                        }
                        else
                        {
                            dispatcher.BeginInvoke(DispatcherPriority.Normal, GetAction(() => handler.Invoke(this, args)));
                        }
                    }
                }
            }
        }
    }
}
