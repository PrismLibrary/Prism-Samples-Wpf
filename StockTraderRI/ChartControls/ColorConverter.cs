

using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows;

namespace StockTraderRI.ChartControls
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Random r;
            int index = 0;

            UIElement c = value as UIElement;
            Panel p = (Panel)VisualTreeHelper.GetParent(c);
            if(p == null)
            {
                r = new Random((int)DateTime.Now.Ticks);
            }
            else
            {
                ItemsControl _parent = ((ItemsControl)((FrameworkElement)VisualTreeHelper.GetParent(p)).TemplatedParent);
                index = _parent.ItemContainerGenerator.IndexFromContainer(c);
                r = new Random((int)index);
            }
            Color color = Color.FromScRgb(1.0f, (float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble());
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
