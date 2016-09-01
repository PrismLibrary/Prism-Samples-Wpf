// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Windows.Data;

namespace Commanding.Modules.Order.Converter
{
    /// <summary>
    /// Converts between a DateTime and a String.
    /// </summary>
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return date.ToString("d", culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, culture, DateTimeStyles.None, out resultDateTime))
            {
                return resultDateTime;
            }

            return value;
        }
    }
}
