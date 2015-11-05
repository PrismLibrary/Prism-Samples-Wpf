

using System.Windows;
using System.Windows.Controls;


namespace StockTraderRI.ChartControls
{
    public class PieChart : Chart
    {
        static PieChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PieChart), new FrameworkPropertyMetadata(typeof(PieChart)));
        }

        public object ValueTitle
        {
            get { return (object)GetValue(ValueTitleProperty); }
            set { SetValue(ValueTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueTitleProperty =
            DependencyProperty.Register("ValueTitle", typeof(object), typeof(PieChart), new UIPropertyMetadata(null));


        public DataTemplate ValueTitleTemplate
        {
            get { return (DataTemplate)GetValue(ValueTitleTemplateProperty); }
            set { SetValue(ValueTitleTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueTitleTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueTitleTemplateProperty =
            DependencyProperty.Register("ValueTitleTemplate", typeof(DataTemplate), typeof(PieChart), new UIPropertyMetadata(null));


        public DataTemplateSelector ValueTitleTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ValueTitleTemplateSelectorProperty); }
            set { SetValue(ValueTitleTemplateSelectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueTitleTemplateSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueTitleTemplateSelectorProperty =
            DependencyProperty.Register("ValueTitleTemplateSelector", typeof(DataTemplateSelector), typeof(PieChart), new UIPropertyMetadata(null));


        public object LabelTitle
        {
            get { return (object)GetValue(LabelTitleProperty); }
            set { SetValue(LabelTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelTitleProperty =
            DependencyProperty.Register("LabelTitle", typeof(object), typeof(PieChart), new UIPropertyMetadata(null));


        public DataTemplate LabelTitleTemplate
        {
            get { return (DataTemplate)GetValue(LabelTitleTemplateProperty); }
            set { SetValue(LabelTitleTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelTitleTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelTitleTemplateProperty =
            DependencyProperty.Register("LabelTitleTemplate", typeof(DataTemplate), typeof(PieChart), new UIPropertyMetadata(null));


        public DataTemplateSelector LabelTitleTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(LabelTitleTemplateSelectorProperty); }
            set { SetValue(LabelTitleTemplateSelectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelTitleTemplateSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelTitleTemplateSelectorProperty =
            DependencyProperty.Register("LabelTitleTemplateSelector", typeof(DataTemplateSelector), typeof(PieChart), new UIPropertyMetadata(null));


        public PropertyPath ValuePath
        {
            get { return (PropertyPath)GetValue(ValuePathProperty); }
            set { SetValue(ValuePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValuePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValuePathProperty =
            DependencyProperty.Register("ValuePath", typeof(PropertyPath), typeof(PieChart), new UIPropertyMetadata(null));


        public PropertyPath LabelPath
        {
            get { return (PropertyPath)GetValue(LabelPathProperty); }
            set { SetValue(LabelPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelPathProperty =
            DependencyProperty.Register("LabelPath", typeof(PropertyPath), typeof(PieChart), new UIPropertyMetadata(null));


        public DataTemplate LegendItemTemplate
        {
            get { return (DataTemplate)GetValue(LegendItemTemplateProperty); }
            set { SetValue(LegendItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LegendItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LegendItemTemplateProperty =
            DependencyProperty.Register("LegendItemTemplate", typeof(DataTemplate), typeof(PieChart), new UIPropertyMetadata(null));


        public DataTemplateSelector LegendItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(LegendItemTemplateSelectorProperty); }
            set { SetValue(LegendItemTemplateSelectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LegendItemTemplateSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LegendItemTemplateSelectorProperty =
            DependencyProperty.Register("LegendItemTemplateSelector", typeof(DataTemplate), typeof(PieChart), new UIPropertyMetadata(null));


    }
}