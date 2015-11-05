

using System;
using System.Globalization;
using System.Windows.Data;

namespace StockTraderRI.Infrastructure.Converters
{
    public class DecimalToColorConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is decimal))
            {
                return null;
            }

            decimal decimalValue = (decimal) value;
            string color;
            if (decimalValue < 0m)
            {
                color = "#ffff0000";
            }
            else
            {
                color = "#ff00cc00";
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
