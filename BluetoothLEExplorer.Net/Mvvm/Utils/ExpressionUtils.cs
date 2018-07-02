//Code from here: https://github.com/Windows-XAML/Template10/blob/version_1.1.12_vs2017/Template10%20(Library)/Utils/ExpressionUtils.cs

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BluetoothLEExplorer.Mvvm.Utils
{
    public static class ExpressionUtils
    {
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (object.Equals(propertyExpression, null))
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            var body = propertyExpression.Body as MemberExpression;

            if (object.Equals(body, null))
            {
                throw new ArgumentException("Invalid argument", nameof(propertyExpression));
            }

            var property = body.Member as PropertyInfo;

            if (object.Equals(property, null))
            {
                throw new ArgumentException("Argument is not a property", nameof(propertyExpression));
            }

            return property.Name;
        }
    }
}
