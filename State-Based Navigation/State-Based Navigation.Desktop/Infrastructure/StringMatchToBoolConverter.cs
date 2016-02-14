// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Windows.Data;

namespace StateBasedNavigation.Desktop.Infrastructure
{
    public class StringMatchToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolean = value as bool?;

            if (boolean.HasValue && boolean.HasValue)
            {
                return parameter;
            }

            return null;
        }
    }
}
