

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StockTraderRI
{
    public class VisibilityToStarHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Collapsed)
            {
                return new GridLength(0, GridUnitType.Star);
            }
            else
            {
                if (parameter == null)
                {
                    throw new ArgumentNullException("parameter");
                }

                return new GridLength(double.Parse(parameter.ToString(), culture), GridUnitType.Star);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
