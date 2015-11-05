

using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System;
using System.Collections;
using System.Collections.ObjectModel;


namespace StockTraderRI.ChartControls
{
    public class ContinuousAxis : ItemsControl
    {
        static ContinuousAxis()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContinuousAxis), new FrameworkPropertyMetadata(typeof(ContinuousAxis)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ContentControl();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ContentControl;
        }

      
        public ObservableCollection<double> SourceValues
        {
            get { return (ObservableCollection<double>)GetValue(SourceValuesProperty); }
            set { SetValue(SourceValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SourceValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceValuesProperty =
            DependencyProperty.Register("SourceValues", typeof(ObservableCollection<double>), typeof(ContinuousAxis), new UIPropertyMetadata(null));


        public ObservableCollection<double> Values
        {
            get { return (ObservableCollection<double>)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Values.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register("Values", typeof(ObservableCollection<double>), typeof(ContinuousAxis), new UIPropertyMetadata(new ObservableCollection<double>()));


        public ObservableCollection<double> TickPositions
        {
            get { return (ObservableCollection<double>)GetValue(TickPositionsProperty); }
            set { SetValue(TickPositionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TickPositions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TickPositionsProperty =
            DependencyProperty.Register("TickPositions", typeof(ObservableCollection<double>), typeof(ContinuousAxis), new UIPropertyMetadata(null));


        public double Origin
        {
            get { return (double)GetValue(OriginProperty); }
            set { SetValue(OriginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ZeroReferenceLinePosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OriginProperty =
            DependencyProperty.Register("Origin", typeof(double), typeof(ContinuousAxis), new UIPropertyMetadata(0.0));



        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ContinuousAxis), new UIPropertyMetadata(Orientation.Vertical));



        public double ReferenceLineSeperation
        {
            get { return (double)GetValue(ReferenceLineSeperationProperty); }
            set { SetValue(ReferenceLineSeperationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ReferenceLineSeperation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReferenceLineSeperationProperty =
            DependencyProperty.Register("ReferenceLineSeperation", typeof(double), typeof(ContinuousAxis), new UIPropertyMetadata(null));



    }
}