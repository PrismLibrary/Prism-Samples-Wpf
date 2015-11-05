

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System;
using System.Windows.Controls;

namespace StockTraderRI.ChartControls
{
    public class ClipWedge : ContentControl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ClipWedge()
        {
            r = new Random();
        }

        public static void OnWedgeShapeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ClipWedge c = sender as ClipWedge;
            if(c!=null)
                c.InvalidateArrange();
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Clip = GetClipGeometry(arrangeBounds);
            return base.ArrangeOverride(arrangeBounds);
        }

        public StreamGeometry GetClipGeometry(Size arrangeBounds)
        {
            StreamGeometry clip = new StreamGeometry();
            StreamGeometryContext clipGC = clip.Open();
            clipGC.BeginFigure(BeginFigurePoint, true, true);
            clipGC.LineTo(LineToPoint, false, true);
            Vector v = LineToPoint - BeginFigurePoint;
            RotateTransform rt = new RotateTransform(WedgeAngle, BeginFigurePoint.X, BeginFigurePoint.Y);
            bool isLargeArc = WedgeAngle >180.0;
            clipGC.ArcTo(rt.Transform(LineToPoint), new Size(v.Length, v.Length), WedgeAngle, isLargeArc, SweepDirection.Clockwise, false, true);
            clipGC.Close();
            return clip;
        }

        public double WedgeAngle
        {
            get { return (double)GetValue(WedgeAngleProperty); }
            set { SetValue(WedgeAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WedgeAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WedgeAngleProperty =
            DependencyProperty.Register("WedgeAngle", typeof(double), typeof(ClipWedge), new UIPropertyMetadata(0.0, new PropertyChangedCallback(OnWedgeShapeChanged)));


        public Point BeginFigurePoint
        {
            get { return (Point)GetValue(BeginFigurePointProperty); }
            set { SetValue(BeginFigurePointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BeginFigurePoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BeginFigurePointProperty =
            DependencyProperty.Register("BeginFigurePoint", typeof(Point), typeof(ClipWedge), new UIPropertyMetadata(new Point(), new PropertyChangedCallback(OnWedgeShapeChanged)));


        public Point LineToPoint
        {
            get { return (Point)GetValue(LineToPointProperty); }
            set { SetValue(LineToPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineTo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineToPointProperty =
            DependencyProperty.Register("LineToPoint", typeof(Point), typeof(ClipWedge), new UIPropertyMetadata(new Point(), new PropertyChangedCallback(OnWedgeShapeChanged)));

        private static Random r;
    }
}