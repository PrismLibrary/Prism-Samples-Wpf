

using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System;
using System.Windows.Data;
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;

namespace StockTraderRI.ChartControls
{
    public class ContinuousAxisPanel : Panel
    {
        public ContinuousAxisPanel()
        {
            _largestLabelSize = new Size();
            SetValue(ItemsSourceKey, new ObservableCollection<String>());
            YValues = new ObservableCollection<double>();
            SetValue(TickPositionsKey, new ObservableCollection<double>());
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            _parentControl = ((ContinuousAxis)((FrameworkElement)VisualTreeHelper.GetParent(this)).TemplatedParent);

            if (_parentControl != null)
            {
                Binding valueBinding = new Binding();
                valueBinding.Source = _parentControl;
                valueBinding.Path = new PropertyPath(ContinuousAxis.SourceValuesProperty);
                this.SetBinding(ContinuousAxisPanel.DataValuesProperty, valueBinding);

                Binding itemsBinding = new Binding();
                itemsBinding.Source = this;
                itemsBinding.Path = new PropertyPath(ContinuousAxisPanel.ItemsSourceProperty);
                _parentControl.SetBinding(ContinuousAxis.ItemsSourceProperty, itemsBinding);

                Binding refLineBinding = new Binding();
                refLineBinding.Source = _parentControl;
                refLineBinding.Path = new PropertyPath(ContinuousAxis.ReferenceLineSeperationProperty);
                this.SetBinding(ContinuousAxisPanel.ReferenceLineSeperationProperty, refLineBinding);

                Binding outputBinding = new Binding();
                outputBinding.Source = this;
                outputBinding.Path = new PropertyPath(ContinuousAxisPanel.YValuesProperty);
                _parentControl.SetBinding(ContinuousAxis.ValuesProperty, outputBinding);

                Binding tickPositionBinding = new Binding();
                tickPositionBinding.Source = this;
                tickPositionBinding.Path = new PropertyPath(ContinuousAxisPanel.TickPositionsProperty);
                _parentControl.SetBinding(ContinuousAxis.TickPositionsProperty, tickPositionBinding);

                Binding zerobinding = new Binding();
                zerobinding.Source = this;
                zerobinding.Path = new PropertyPath(ContinuousAxisPanel.OriginProperty);
                _parentControl.SetBinding(ContinuousAxis.OriginProperty, zerobinding);
            }
        }

        public static void OnDataValuesChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ContinuousAxisPanel p = sender as ContinuousAxisPanel;
            if (p != null && p.DataValues != null)
            {
                ((INotifyCollectionChanged)p.DataValues).CollectionChanged += new NotifyCollectionChangedEventHandler(p.Axis2Panel_CollectionChanged);
                p.GenerateItemsSource();
            }
        }

        public void Axis2Panel_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            GenerateItemsSource();
        }

        private void GenerateItemsSource()
        {
            if (DataValues==null || DataValues.Count==0)
            {
                return;
            }
            CalculateValueIncrement(_arrangeSize);
            
            ObservableCollection<String> tempItemsSource = ItemsSource;
            tempItemsSource.Clear();
            int referenceLinesCreated = 0;
            while (referenceLinesCreated != _numReferenceLines)
            {
                if (Orientation.Equals(Orientation.Vertical))
                    tempItemsSource.Add(((double)(_startingIncrement + referenceLinesCreated * _valueIncrement)).ToString());
                else
                    tempItemsSource.Add(((double)(_startingIncrement + (_numReferenceLines - 1 - referenceLinesCreated) * _valueIncrement)).ToString());
                referenceLinesCreated++;
            }
            _highValue = _startingIncrement + (_numReferenceLines - 1) * _valueIncrement;
            _lowValue = _startingIncrement;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            _largestLabelSize.Height = 0.0;
            _largestLabelSize.Width = 0.0;
            UIElementCollection tempInternalChildren = InternalChildren;
            for (int i = 0; i < tempInternalChildren.Count; i++)
            {
                tempInternalChildren[i].Measure(availableSize);
                _largestLabelSize.Height = _largestLabelSize.Height > tempInternalChildren[i].DesiredSize.Height
                    ? _largestLabelSize.Height : tempInternalChildren[i].DesiredSize.Height;
                _largestLabelSize.Width = _largestLabelSize.Width > tempInternalChildren[i].DesiredSize.Width
                    ? _largestLabelSize.Width : tempInternalChildren[i].DesiredSize.Width;
            }
            if (Orientation.Equals(Orientation.Vertical))
            {
                double fitAllLabelSize = _largestLabelSize.Height * InternalChildren.Count;
                availableSize.Height = fitAllLabelSize < availableSize.Height ? fitAllLabelSize : availableSize.Height;
                availableSize.Width = _largestLabelSize.Width;
            }
            else
            {
                double fitAllLabelsSize = _largestLabelSize.Width * InternalChildren.Count;
                availableSize.Width = fitAllLabelsSize < availableSize.Width ? fitAllLabelsSize : availableSize.Width;
                availableSize.Height = _largestLabelSize.Height;
            }
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (!_arrangeSize.Equals(finalSize))
            {
                _arrangeSize = finalSize;
                GenerateItemsSource();
            }
            _arrangeSize = finalSize;
            if (InternalChildren.Count > 0)
            {
                if (Orientation.Equals(Orientation.Vertical))
                {
                    ArrangeVerticalLabels(finalSize);
                    CalculateYOutputValues(finalSize);
                }
                else
                {
                    ArrangeHorizontalLabels(finalSize);
                    CalculateXOutputValues(finalSize);
                }

            }
            return base.ArrangeOverride(finalSize);
        }

        private void ArrangeHorizontalLabels(Size constraint)
        {
            double rectWidth = _largestLabelSize.Width;
            double rectHeight = _largestLabelSize.Height;
            double increments = CalculatePixelIncrements(constraint, _largestLabelSize);
            double start_width = constraint.Width - _largestLabelSize.Width / 2;
            double end_width = start_width - (InternalChildren.Count - 1) * increments;
            ObservableCollection<double> tempTickPositions = TickPositions;

            if (start_width > end_width)
            {
                tempTickPositions.Clear();
                Rect r = new Rect(start_width - rectWidth / 2, 0, rectWidth, rectHeight);
                InternalChildren[0].Arrange(r);
                tempTickPositions.Add(start_width);
                int count = InternalChildren.Count - 1;
                r = new Rect(start_width - count * increments - rectWidth / 2, 0, rectWidth, rectHeight);
                InternalChildren[count].Arrange(r);
                tempTickPositions.Add(start_width - count * increments);

                if (constraint.Width > 3 * rectWidth)
                {
                    _skipFactor = (int)Math.Ceiling((InternalChildren.Count - 2) / Math.Floor((constraint.Width - 2 * rectWidth) / rectWidth));
                    if ((InternalChildren.Count - 2) != 2.0)
                        _skipFactor = Math.Min(_skipFactor, (int)Math.Ceiling((double)(InternalChildren.Count - 2.0) / 2.0));
                    _canDisplayAllLabels = true;
                    if (_skipFactor > 1)
                    {
                        _canDisplayAllLabels = false;
                    }

                    for (int i = 2; i <= InternalChildren.Count - 1; i++)
                    {
                        tempTickPositions.Add(start_width - (i - 1) * increments);
                        if (_canDisplayAllLabels || (i + 1) % _skipFactor == 0)
                        {
                            r = new Rect(start_width - (i - 1) * increments - rectWidth / 2, 0, rectWidth, rectHeight);
                            InternalChildren[i-1].Arrange(r);
                        }
                        else
                        {
                            InternalChildren[i-1].Arrange(new Rect(0, 0, 0, 0));
                        }
                    }
                }
            }
        }

        private void ArrangeVerticalLabels(Size constraint)
        {
            double rectWidth = _largestLabelSize.Width;
            double rectHeight = _largestLabelSize.Height;
            double increments = CalculatePixelIncrements(constraint, _largestLabelSize);
            double start_height = constraint.Height - _largestLabelSize.Height / 2;
            double end_height = start_height - (InternalChildren.Count - 1) * increments;
            ObservableCollection<double> tempTickPositions = TickPositions;

            if(start_height > end_height)
            {
                tempTickPositions.Clear();
                Rect r = new Rect(constraint.Width - rectWidth, (start_height - rectHeight / 2), rectWidth, rectHeight);
                InternalChildren[0].Arrange(r);
                tempTickPositions.Add(start_height);
                int count = InternalChildren.Count-1;
                r = new Rect(constraint.Width - rectWidth, (start_height - count*increments - rectHeight / 2), rectWidth, rectHeight);
                InternalChildren[count].Arrange(r);
                tempTickPositions.Add(start_height - count * increments);

                if (constraint.Height > 3 * rectHeight)
                {
                    _skipFactor = (int)Math.Ceiling((InternalChildren.Count - 2) / Math.Floor((constraint.Height - 2 * rectHeight) / rectHeight));
                    if ((InternalChildren.Count - 2) != 2.0)
                        _skipFactor = Math.Min(_skipFactor, (int)Math.Ceiling((double)(InternalChildren.Count - 2.0) / 2.0));
                    _canDisplayAllLabels = true;
                    if (_skipFactor > 1)
                    {
                        _canDisplayAllLabels = false;
                    }
                    
                    for (int i = 2; i <= InternalChildren.Count-1; i++)
                    {
                        tempTickPositions.Add(start_height - (i - 1) * increments);
                        if (_canDisplayAllLabels || (i + 1) % _skipFactor == 0 )
                        {
                            r = new Rect(constraint.Width - rectWidth, (start_height - (i - 1) * increments - rectHeight / 2), rectWidth, rectHeight);
                            InternalChildren[i - 1].Arrange(r);
                        }
                        else
                        {
                            InternalChildren[i - 1].Arrange(new Rect(0, 0, 0, 0));
                        }
                    }
                }
            }
        }

        private void CalculateYOutputValues(Size constraint)
        {
            YValues.Clear();
            double start_val, lowPixel, highPixel;
            double pixelIncrement = CalculatePixelIncrements(constraint, _largestLabelSize);
            if (Orientation.Equals(Orientation.Vertical))
            {
                start_val = constraint.Height - _largestLabelSize.Height / 2;
                lowPixel = start_val - (InternalChildren.Count - 1) * pixelIncrement;
                highPixel = start_val;
            }
            else
            {
                start_val = constraint.Width - _largestLabelSize.Width / 2;
                lowPixel = start_val - (InternalChildren.Count - 1) * pixelIncrement;
                highPixel = start_val;
            }
            if (highPixel < lowPixel)
                return;
            for (int i = 0; i < DataValues.Count; i++)
            {
                double outVal = highPixel - ((highPixel - lowPixel) / (_highValue - _lowValue)) * (DataValues[i] - _lowValue);
                YValues.Add(outVal);
            }
            if (_startsAtZero || (!_allNegativeValues && !_allPositiveValues))
                Origin = highPixel - ((highPixel - lowPixel) / (_highValue - _lowValue)) * (0.0 - _lowValue);
            else if (!_startsAtZero && _allPositiveValues)
                Origin = highPixel;
            else
                Origin = lowPixel;
        }

        private void CalculateXOutputValues(Size constraint)
        {
            YValues.Clear();
            double start_width = constraint.Width - _largestLabelSize.Width / 2;
            double pixelIncrement = CalculatePixelIncrements(constraint, _largestLabelSize);
            double lowPixel = start_width - (InternalChildren.Count - 1) * pixelIncrement;
            double highPixel = start_width;
            if (highPixel < lowPixel)
                return;
            for (int i = 0; i < DataValues.Count; i++)
            {
                double output = lowPixel + ((highPixel - lowPixel) / (_highValue - _lowValue)) * (DataValues[i] - _lowValue);
                YValues.Add(output);
            }
            if (_startsAtZero || (!_allNegativeValues && !_allPositiveValues))
                Origin = lowPixel + ((highPixel - lowPixel) / (_highValue - _lowValue)) * (0.0 - _lowValue);
            else if (!_startsAtZero && _allPositiveValues)
                Origin = lowPixel;
            else
                Origin = highPixel;
        }

        /// <summary>
        /// Calculate the pixel distance between each tick mark on the vertical axis
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        private double CalculatePixelIncrements(Size constraint, Size labelSize)
        {
            if(Orientation.Equals(Orientation.Vertical))
                return (constraint.Height - _largestLabelSize.Height) / (_numReferenceLines - 1);
            else
                return (constraint.Width - _largestLabelSize.Width) / (_numReferenceLines - 1);
        }

        private double CalculateValueIncrement(Size size)
        {
            // Determine if the starting value is 0 or not
            bool startsAtZero = false;
            bool allPositiveValues = true;
            bool allNegativeValues = true;
            double increment_value = 0;
            int multiplier = 1;
            if (DataValues.Count == 0)
                return 0.0;
            //double low = ((DoubleHolder)DataValues[0]).DoubleValue;
            //double high = ((DoubleHolder)DataValues[0]).DoubleValue;

            double low = DataValues[0];
            double high = DataValues[0];

            for (int i = 0; i < DataValues.Count; i++)
            {
                //double temp = ((DoubleHolder)DataValues[i]).DoubleValue;
                double temp = DataValues[i];

                // Check for positive and negative values
                if (temp > 0)
                {
                    allNegativeValues = false;
                }
                else if (temp < 0)
                {
                    allPositiveValues = false;
                }

                // Reset low and high if necessary
                if (temp < low)
                {
                    low = temp;
                }
                else if (temp > high)
                {
                    high = temp;
                }
            }

            // Determine whether or not the increments will start at zero
            if (allPositiveValues && (low < (high / 2)) ||
                (allNegativeValues && high > (low / 2)))
            {
                _startsAtZero = true;
                startsAtZero = true;
            }

            // If all values in dataset are 0, draw one reference line and label it 0
            if (high == 0 && low == 0)
            {
                _valueIncrement = 0;
                _startingIncrement = 0;
                _numReferenceLines = 1;
                _startsAtZero = startsAtZero;
                return increment_value;
            }

            // Find an increment value that is in the set {1*10^x, 2*10^x, 5*10^x, where x is an integer 
            //  (positive, negative, or zero)}

            if (!allNegativeValues)
            {
                if (startsAtZero)
                {
                    int exp = 0;
                    while (true)
                    {
                        multiplier = IsWithinRange(high, exp, size);
                        if (multiplier != -1)
                        {
                            break;
                        }
                        multiplier = IsWithinRange(high, (-1 * exp), size);
                        if (multiplier != -1)
                        {
                            exp = -1 * exp;
                            break;
                        }
                        exp++;
                    }
                    increment_value = multiplier * Math.Pow(10, exp);
                }
                else
                {
                    int exp = 0;
                    while (true)
                    {
                        multiplier = IsWithinRange((high - low), exp, size);
                        if (multiplier != -1)
                        {
                            break;
                        }
                        multiplier = IsWithinRange((high - low), (-1 * exp), size);
                        if (multiplier != -1)
                        {
                            exp = -1 * exp;
                            break;
                        }
                        if (high == low)
                        {
                            increment_value = high;
                            _valueIncrement = increment_value;
                            _numReferenceLines = 1;
                            break;
                        }

                        exp++;
                    }
                    if (increment_value == 0)
                    {
                        increment_value = multiplier * Math.Pow(10, exp);
                    }
                }
            }
            else
            {
                if (startsAtZero)
                {
                    int exp = 0;
                    while (true)
                    {
                        multiplier = IsWithinRange(low, exp, size);
                        if (multiplier != -1)
                        {
                            break;
                        }
                        multiplier = IsWithinRange(low, (-1 * exp), size);
                        if (multiplier != -1)
                        {
                            exp = -1 * exp;
                            break;
                        }
                        exp++;
                    }
                    increment_value = multiplier * Math.Pow(10, exp);
                }
                else
                {
                    int exp = 0;
                    if (low - high == 0.0)
                        increment_value = 1.0;
                    else
                    {
                        while (true)
                        {
                            multiplier = IsWithinRange((low - high), exp, size);
                            if (multiplier != -1)
                            {
                                break;
                            }
                            multiplier = IsWithinRange((low - high), (-1 * exp), size);
                            if (multiplier != -1)
                            {
                                exp = -1 * exp;
                                break;
                            }
                            exp++;
                        }
                        increment_value = multiplier * Math.Pow(10, exp);
                    }
                }
            }



            double starting_value = 0;

            // Determine starting value if it is nonzero
            if (!startsAtZero)
            {
                if (allPositiveValues)
                {
                    if (low % increment_value == 0)
                    {
                        starting_value = low;
                    }
                    else
                    {
                        starting_value = (int)(low / increment_value) * increment_value;
                    }
                }
                else
                {
                    if (low % increment_value == 0)
                    {
                        starting_value = low;
                    }
                    else
                    {
                        starting_value = (int)((low - increment_value) / increment_value) * increment_value;
                    }
                }
            }
            else if (startsAtZero && allNegativeValues)
            {
                if (low % increment_value == 0)
                {
                    starting_value = low;
                }
                else
                {
                    starting_value = (int)((low - increment_value) / increment_value) * increment_value;
                }
            }

            // Determine the number of reference lines
            //int numRefLines = 0;
            int numRefLines = (int)Math.Ceiling((high - starting_value) / increment_value) + 1;

            _valueIncrement = increment_value;
            _startingIncrement = starting_value;
            _numReferenceLines = numRefLines;
            _startsAtZero = startsAtZero;
            _allPositiveValues = allPositiveValues;
            _allNegativeValues = allNegativeValues;
            return increment_value;
        }

        /// <summary>
        /// Checks to see if the calculated increment value is between the low and high passed in, 
        /// then returns the multiplier used
        /// </summary>
        /// <param name="numerator"></param>
        /// <param name="exponent"></param>
        /// <param name="lowRange"></param>
        /// <param name="highRange"></param>
        /// <returns></returns>
        private int IsWithinRange(double numerator, int exponent, Size size)
        {
            int highRange, lowRange;
           // highRange = (int)Math.Min(10, (int)(size.Height / labelSize.Height)) -2;
            if(Orientation.Equals(Orientation.Vertical))
                highRange = (int)(size.Height / ReferenceLineSeperation);
            else
                highRange = (int)(size.Width / ReferenceLineSeperation);

            lowRange = 1;
            highRange = (int)Math.Max(highRange, 3);
            
            if ((Math.Abs(numerator) / (1 * Math.Pow(10, exponent))) >= lowRange && (Math.Abs(numerator) / (1 * Math.Pow(10, exponent))) <= highRange)
            {
                return 1;
            }
            if ((Math.Abs(numerator) / (2 * Math.Pow(10, exponent))) >= lowRange && (Math.Abs(numerator) / (2 * Math.Pow(10, exponent))) <= highRange)
            {
                return 2;
            }
            if ((Math.Abs(numerator) / (5 * Math.Pow(10, exponent))) >= lowRange && (Math.Abs(numerator) / (5 * Math.Pow(10, exponent))) <= highRange)
            {
                return 5;
            }
            return -1;
        }


        public ObservableCollection<double> YValues
        {
            get { return (ObservableCollection<double>)GetValue(YValuesProperty); }
            set { SetValue(YValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for YValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YValuesProperty =
            DependencyProperty.Register("YValues", typeof(ObservableCollection<double>), typeof(ContinuousAxisPanel), new UIPropertyMetadata(null));



        public ObservableCollection<String> ItemsSource
        {
            get { return (ObservableCollection<String>)GetValue(ItemsSourceProperty); }
        }

        // Using a DependencyProperty as the backing store for Axis2Panel.  This enables animation, styling, binding, etc...
        private static readonly DependencyPropertyKey ItemsSourceKey =
            DependencyProperty.RegisterReadOnly("ItemsSource", typeof(ObservableCollection<String>), typeof(ContinuousAxisPanel), new UIPropertyMetadata());

        public static readonly DependencyProperty ItemsSourceProperty = ItemsSourceKey.DependencyProperty;
        

        private ObservableCollection<double> DataValues
        {
            get { return (ObservableCollection<double>)GetValue(DataValuesProperty); }
            set { SetValue(DataValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataValues.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty DataValuesProperty =
            DependencyProperty.Register("DataValues", typeof(ObservableCollection<double>), typeof(ContinuousAxisPanel), new FrameworkPropertyMetadata(OnDataValuesChanged));


        public ObservableCollection<double> TickPositions
        {
            get { return (ObservableCollection<double>)GetValue(TickPositionsProperty); }
            //set { SetValue(TickPositionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TickPositions.  This enables animation, styling, binding, etc...
        private static readonly DependencyPropertyKey TickPositionsKey =
            DependencyProperty.RegisterReadOnly("TickPositions", typeof(ObservableCollection<double>), typeof(ContinuousAxisPanel), new UIPropertyMetadata(null));

        public static readonly DependencyProperty TickPositionsProperty = TickPositionsKey.DependencyProperty;

        public double Origin
        {
            get { return (double)GetValue(OriginProperty); }
            set { SetValue(OriginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ZeroReferenceLinePosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OriginProperty =
            DependencyProperty.Register("Origin", typeof(double), typeof(ContinuousAxisPanel), new UIPropertyMetadata(0.0));


        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ContinuousAxisPanel), new UIPropertyMetadata(Orientation.Vertical));




        public double ReferenceLineSeperation
        {
            get { return (double)GetValue(ReferenceLineSeperationProperty); }
            set { SetValue(ReferenceLineSeperationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ReferenceLineSeperation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReferenceLineSeperationProperty =
            DependencyProperty.Register("ReferenceLineSeperation", typeof(double), typeof(ContinuousAxisPanel), new UIPropertyMetadata(null));



        private Size _largestLabelSize;
        private bool _canDisplayAllLabels;
        private int _skipFactor;
        private double _lowValue, _highValue;
        private Size _arrangeSize;
        public ItemsControl _parentControl;
        private bool _startsAtZero;
        private double _startingIncrement;
        private double _valueIncrement;
        private int _numReferenceLines;
        private bool _allPositiveValues;
        private bool _allNegativeValues;
    }
}