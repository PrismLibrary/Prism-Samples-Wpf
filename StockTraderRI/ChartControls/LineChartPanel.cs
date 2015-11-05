

using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace StockTraderRI.ChartControls
{
    public class LineChartPanel : Panel
    {
        protected override void OnInitialized(System.EventArgs e)
        {
            base.OnInitialized(e);
            _childrenPositions = new ObservableCollection<Point>();
        }
        
        private static void OnValuesChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            LineChartPanel v = sender as LineChartPanel;
            if (v == null)
                return;
            ItemCollection oldItems = args.OldValue as ItemCollection;
            ItemCollection newItems = args.NewValue as ItemCollection;
            if (oldItems != null)
                ((INotifyCollectionChanged)oldItems).CollectionChanged -= new NotifyCollectionChangedEventHandler(v.LineChartPanel_CollectionChanged);

            if(args.Property == LineChartPanel.XValuesProperty)
            {
                if (GetXValues(v)!= null)
                    GetXValues(v).CollectionChanged += new NotifyCollectionChangedEventHandler(v.LineChartPanel_CollectionChanged);
            }
            else if(args.Property == LineChartPanel.YValuesProperty)
            {
                if(GetYValues(v)!=null)
                    GetYValues(v).CollectionChanged += new NotifyCollectionChangedEventHandler(v.LineChartPanel_CollectionChanged);
            }
            v.InvalidateArrange();
            v.InvalidateVisual();
        }

        private void LineChartPanel_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InvalidateArrange();
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (dc == null)
            {
                throw new ArgumentNullException("dc");
            }

            base.OnRender(dc);
            if (_childrenPositions.Count == 0)
                return;
            if (!IsSmoothOutline)
            {
                dc.DrawGeometry(AreaBrush, LinePen, CreateLineCurveGeometry());
            }
            else
            {
                dc.DrawGeometry(AreaBrush, LinePen, CreateAreaCurveGeometry());
            }
        }

        private PathGeometry CreateLineCurveGeometry()
        {
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            pathGeometry.Figures.Add(pathFigure);
            pathFigure.StartPoint = (Point)GeometryOperation.ComputeIntersectionPoint((FrameworkElement)InternalChildren[0], (FrameworkElement)InternalChildren[1]);
            PolyLineSegment pls = new PolyLineSegment();
            for(int i=1; i < InternalChildren.Count; i++)
                pls.Points.Add(GeometryOperation.ComputeIntersectionPoint((FrameworkElement)InternalChildren[i], 
                    ((FrameworkElement)InternalChildren[i-1])));
            pathFigure.Segments.Add(pls);
            if(AreaBrush!=null)
            {
                pathFigure.Segments.Add(new LineSegment(new Point(_childrenPositions[_childrenPositions.Count - 1].X, GetHorizontalAxis(this)), false));
                pathFigure.Segments.Add(new LineSegment(new Point(_childrenPositions[0].X, GetHorizontalAxis(this)), false));
            }
            return pathGeometry;
        }

        private PathGeometry CreateAreaCurveGeometry()
        {
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            pathGeometry.Figures.Add(pathFigure);
            
            Point[] catmullRomPoints = new Point[_childrenPositions.Count];
            _childrenPositions.CopyTo(catmullRomPoints, 0);
            Point[] bezierPoints = GeometryOperation.CatmullRom(catmullRomPoints);
            pathFigure.StartPoint = bezierPoints[0];
            PolyBezierSegment pbs = new PolyBezierSegment();
            for (int i = 1; i < bezierPoints.GetLength(0); i++)
                pbs.Points.Add(bezierPoints[i]);
            pathFigure.Segments.Add(pbs);
            if (AreaBrush != null)
            {
                pathFigure.Segments.Add(new LineSegment(new Point(_childrenPositions[_childrenPositions.Count - 1].X, GetHorizontalAxis(this)), false));
                pathFigure.Segments.Add(new LineSegment(new Point(_childrenPositions[0].X, GetHorizontalAxis(this)), false));
            }
            return pathGeometry;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                InternalChildren[i].Measure(availableSize);
            }
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            int count = Math.Min(XValues.Count, YValues.Count);
            _childrenPositions.Clear();
            for (int i = 0; i < count; i++)
            {
                double x = (i + 1 >= XValues.Count) ? XValues[i] : XValues[i] + XValues[i + 1];
                Point position = new Point(x / 2, YValues[i]);
                _childrenPositions.Add(position);
                Rect r = new Rect(position.X - InternalChildren[i].DesiredSize.Width / 2,
                    position.Y - InternalChildren[i].DesiredSize.Height / 2
                    , InternalChildren[i].DesiredSize.Width, InternalChildren[i].DesiredSize.Height);
                InternalChildren[i].Arrange(r);
            }
            return finalSize;
        }

        public bool IsSmoothOutline
        {
            get { return (bool)GetValue(IsSmoothOutlineProperty); }
            set { SetValue(IsSmoothOutlineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSmoothOutline.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSmoothOutlineProperty =
            DependencyProperty.Register("IsSmoothOutline", typeof(bool), typeof(LineChartPanel), new UIPropertyMetadata(false));

        public Pen LinePen
        {
            get { return (Pen)GetValue(LinePenProperty); }
            set { SetValue(LinePenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LinePen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinePenProperty =
            DependencyProperty.Register("LinePen", typeof(Pen), typeof(LineChartPanel), new UIPropertyMetadata(null));


        public Brush AreaBrush
        {
            get { return (Brush)GetValue(AreaBrushProperty); }
            set { SetValue(AreaBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AreaBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaBrushProperty =
            DependencyProperty.Register("AreaBrush", typeof(Brush), typeof(LineChartPanel), new UIPropertyMetadata(null));


        private ObservableCollection<double> XValues
        {
            get { return (ObservableCollection<double>)GetXValues(this); }
            set { SetXValues(this, value); }
        }

        public static ObservableCollection<double> GetXValues(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return (ObservableCollection<double>)obj.GetValue(XValuesProperty);
        }

        public static void SetXValues(DependencyObject obj, ObservableCollection<double> value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            obj.SetValue(XValuesProperty, value);
        }

        // Using a DependencyProperty as the backing store for XValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XValuesProperty =
            DependencyProperty.RegisterAttached("XValues", typeof(ObservableCollection<double>), typeof(LineChartPanel), new UIPropertyMetadata(null, new PropertyChangedCallback(OnValuesChanged)));


        private ObservableCollection<double> YValues
        {
            get { return (ObservableCollection<double>)GetYValues(this); }
            set { SetYValues(this, value); }
        }

        public static ObservableCollection<double> GetYValues(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return (ObservableCollection<double>)obj.GetValue(YValuesProperty);
        }

        public static void SetYValues(DependencyObject obj, ObservableCollection<double> value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            obj.SetValue(YValuesProperty, value);
        }

        // Using a DependencyProperty as the backing store for YValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YValuesProperty =
            DependencyProperty.RegisterAttached("YValues", typeof(ObservableCollection<double>), typeof(LineChartPanel), new UIPropertyMetadata(null, new PropertyChangedCallback(OnValuesChanged)));



        public static double GetHorizontalAxis(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return (double)obj.GetValue(HorizontalAxisProperty);
        }

        public static void SetHorizontalAxis(DependencyObject obj, double value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            obj.SetValue(HorizontalAxisProperty, value);
        }

        // Using a DependencyProperty as the backing store for HorizontalAxis.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalAxisProperty =
            DependencyProperty.RegisterAttached("HorizontalAxis", typeof(double), typeof(LineChartPanel), new UIPropertyMetadata(0.0));


        private ObservableCollection<Point> _childrenPositions;
    }
}