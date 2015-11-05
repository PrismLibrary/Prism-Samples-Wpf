

using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace StockTraderRI.ChartControls
{
    public class LineChart : Chart
    {
        static LineChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineChart), new FrameworkPropertyMetadata(typeof(LineChart)));
        }

        public Pen LinePen
        {
            get { return (Pen)GetValue(LinePenProperty); }
            set { SetValue(LinePenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LinePen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinePenProperty =
            DependencyProperty.Register("LinePen", typeof(Pen), typeof(LineChart), new UIPropertyMetadata(null));


        public Brush AreaBrush
        {
            get { return (Brush)GetValue(AreaBrushProperty); }
            set { SetValue(AreaBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AreaBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaBrushProperty =
            DependencyProperty.Register("AreaBrush", typeof(Brush), typeof(LineChart), new UIPropertyMetadata(null));


        public bool IsSmoothOutline
        {
            get { return (bool)GetValue(IsSmoothOutlineProperty); }
            set { SetValue(IsSmoothOutlineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSmoothOutline.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSmoothOutlineProperty =
            DependencyProperty.Register("IsSmoothOutline", typeof(bool), typeof(LineChart), new UIPropertyMetadata(false));



        public DataTemplate ValueAxisItemTemplate
        {
            get { return (DataTemplate)GetValue(ValueAxisItemTemplateProperty); }
            set { SetValue(ValueAxisItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueAxisItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueAxisItemTemplateProperty =
            DependencyProperty.Register("ValueAxisItemTemplate", typeof(DataTemplate), typeof(LineChart), new UIPropertyMetadata(null));


        public DataTemplateSelector ValueAxisItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ValueAxisItemTemplateSelectorProperty); }
            set { SetValue(ValueAxisItemTemplateSelectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueAxisItemTemplateSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueAxisItemTemplateSelectorProperty =
            DependencyProperty.Register("ValueAxisItemTemplateSelector", typeof(DataTemplateSelector), typeof(LineChart), new UIPropertyMetadata(null));


        public DataTemplate LabelAxisItemTemplate
        {
            get { return (DataTemplate)GetValue(LabelAxisItemTemplateProperty); }
            set { SetValue(LabelAxisItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelAxisItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelAxisItemTemplateProperty =
            DependencyProperty.Register("LabelAxisItemTemplate", typeof(DataTemplate), typeof(LineChart), new UIPropertyMetadata(null));


        public DataTemplateSelector LabelAxisItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(LabelAxisItemTemplateSelectorProperty); }
            set { SetValue(LabelAxisItemTemplateSelectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelAxisItemTemplateSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelAxisItemTemplateSelectorProperty =
            DependencyProperty.Register("LabelAxisItemTemplateSelector", typeof(DataTemplateSelector), typeof(LineChart), new UIPropertyMetadata(null));


        public object ValueAxisTitle
        {
            get { return (object)GetValue(ValueAxisTitleProperty); }
            set { SetValue(ValueAxisTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueAxisTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueAxisTitleProperty =
            DependencyProperty.Register("ValueAxisTitle", typeof(object), typeof(LineChart), new UIPropertyMetadata(null));


        public DataTemplate ValueAxisTitleTemplate
        {
            get { return (DataTemplate)GetValue(ValueAxisTitleTemplateProperty); }
            set { SetValue(ValueAxisTitleTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueAxisTitleTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueAxisTitleTemplateProperty =
            DependencyProperty.Register("ValueAxisTitleTemplate", typeof(DataTemplate), typeof(LineChart), new UIPropertyMetadata(null));


        public DataTemplateSelector ValueAxisTitleTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ValueAxisTitleTemplateSelectorProperty); }
            set { SetValue(ValueAxisTitleTemplateSelectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueAxisTitleTemplateSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueAxisTitleTemplateSelectorProperty =
            DependencyProperty.Register("ValueAxisTitleTemplateSelector", typeof(DataTemplateSelector), typeof(LineChart), new UIPropertyMetadata(null));


        public object LabelAxisTitle
        {
            get { return (object)GetValue(LabelAxisTitleProperty); }
            set { SetValue(LabelAxisTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelAxisTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelAxisTitleProperty =
            DependencyProperty.Register("LabelAxisTitle", typeof(object), typeof(LineChart), new UIPropertyMetadata(null));


        public DataTemplate LabelAxisTitleTemplate
        {
            get { return (DataTemplate)GetValue(LabelAxisTitleTemplateProperty); }
            set { SetValue(LabelAxisTitleTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelAxisTitleTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelAxisTitleTemplateProperty =
            DependencyProperty.Register("LabelAxisTitleTemplate", typeof(DataTemplate), typeof(LineChart), new UIPropertyMetadata(null));


        public DataTemplateSelector LabelAxisTitleTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(LabelAxisTitleTemplateSelectorProperty); }
            set { SetValue(LabelAxisTitleTemplateSelectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelAxisTitleTemplateSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelAxisTitleTemplateSelectorProperty =
            DependencyProperty.Register("LabelAxisTitleTemplateSelector", typeof(DataTemplateSelector), typeof(LineChart), new UIPropertyMetadata(null));


        public PropertyPath ValuePath
        {
            get { return (PropertyPath)GetValue(ValuePathProperty); }
            set { SetValue(ValuePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValuePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValuePathProperty =
            DependencyProperty.Register("ValuePath", typeof(PropertyPath), typeof(LineChart), new UIPropertyMetadata(null));


        public PropertyPath LabelPath
        {
            get { return (PropertyPath)GetValue(LabelPathProperty); }
            set { SetValue(LabelPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelPathProperty =
            DependencyProperty.Register("LabelPath", typeof(PropertyPath), typeof(LineChart), new UIPropertyMetadata(null));




        public bool ShowValueAxisTicks
        {
            get { return (bool)GetValue(ShowValueAxisTicksProperty); }
            set { SetValue(ShowValueAxisTicksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowValueAxisTicks.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowValueAxisTicksProperty =
            DependencyProperty.Register("ShowValueAxisTicks", typeof(bool), typeof(LineChart), new UIPropertyMetadata(null));



        public bool ShowLabelAxisTicks
        {
            get { return (bool)GetValue(ShowLabelAxisTicksProperty); }
            set { SetValue(ShowLabelAxisTicksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowLabelAxisTicks.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowLabelAxisTicksProperty =
            DependencyProperty.Register("ShowLabelAxisTicks", typeof(bool), typeof(LineChart), new UIPropertyMetadata(null));




        public bool ShowValueAxisReferenceLines
        {
            get { return (bool)GetValue(ShowValueAxisReferenceLinesProperty); }
            set { SetValue(ShowValueAxisReferenceLinesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowValueAxisReferenceLines.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowValueAxisReferenceLinesProperty =
            DependencyProperty.Register("ShowValueAxisReferenceLines", typeof(bool), typeof(LineChart), new UIPropertyMetadata(null));



        public bool ShowLabelAxisReferenceLines
        {
            get { return (bool)GetValue(ShowLabelAxisReferenceLinesProperty); }
            set { SetValue(ShowLabelAxisReferenceLinesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowLabelAxisReferenceLines.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowLabelAxisReferenceLinesProperty =
            DependencyProperty.Register("ShowLabelAxisReferenceLines", typeof(bool), typeof(LineChart), new UIPropertyMetadata(null));



        public Pen ReferenceLinePen
        {
            get { return (Pen)GetValue(ReferenceLinePenProperty); }
            set { SetValue(ReferenceLinePenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ReferenceLinePen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReferenceLinePenProperty =
            DependencyProperty.Register("ReferenceLinePen", typeof(Pen), typeof(LineChart), new UIPropertyMetadata(null));



        public double TickLength
        {
            get { return (double)GetValue(TickLengthProperty); }
            set { SetValue(TickLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TickLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TickLengthProperty =
            DependencyProperty.Register("TickLength", typeof(double), typeof(LineChart), new UIPropertyMetadata(null));



     }
}