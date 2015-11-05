

using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System;

namespace StockTraderRI.ChartControls
{
    public class GeometryOperation
    {

        public static Point ComputeIntersectionPoint(FrameworkElement thisV, FrameworkElement toV)
        {
            if (thisV == null)
            {
                throw new ArgumentNullException("thisV");
            }

            PathGeometry fromGeometryFlat;
            //using the cached geometry or building one up
            fromGeometryFlat = BuildVertexGeometry(thisV);
            if (toV != null)
            {
                //transforming the argument vertex's center to the coordinate space of this vertex
                Panel panel = (Panel)VisualTreeHelper.GetParent(thisV);
                Transform transformToPanel = (Transform)toV.TransformToAncestor(panel);
                Transform transformToFromV = (Transform)panel.TransformToDescendant(thisV);
                Point transformedToVCenter = new Point(toV.RenderSize.Width / 2, toV.RenderSize.Height / 2);
                if (transformToPanel != null)
                    transformedToVCenter = transformToPanel.Transform(transformedToVCenter);
                if (transformToFromV != null)
                    transformedToVCenter = transformToFromV.Transform(transformedToVCenter);

                Point thisCenter = new Point(thisV.RenderSize.Width / 2, thisV.RenderSize.Height / 2);
                Vector centerVector = thisCenter - transformedToVCenter;
                //getting intersection point in the coordinate space of this vertex
                Point p1 = FindOppositePoint(fromGeometryFlat, transformedToVCenter, centerVector, null);
                //transforming back to the panel coordinate space
                if (transformToFromV != null)
                    p1 = transformToFromV.Inverse.Transform(p1);
                return p1;
            }
            return new Point(thisV.RenderSize.Width/2, thisV.RenderSize.Height/2);
        }

        /// <summary>
        /// Returns the flattened geometry for the argument vertex. It does not take the transforms on the vertex into account.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private static PathGeometry BuildVertexGeometry(Visual v)
        {
            GeometryGroup fromGeometry = new GeometryGroup();
            fromGeometry.FillRule = FillRule.Nonzero;
            WalkChildren((Visual)v, fromGeometry);
            PathGeometry fromGeometryFlat = fromGeometry.GetFlattenedPathGeometry();
            return fromGeometryFlat;
        }

        /// <summary>
        /// Walks the children of a visual to build a geometry tree. Ignores the transform and clip on the visual.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="g"></param>
        private static void WalkChildren(Visual v, GeometryGroup g)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(v); i++)
            {
                Visual visualChild = (Visual)VisualTreeHelper.GetChild(v, i);
                GeometryGroup child = new GeometryGroup();
                child.FillRule = FillRule.Nonzero;
                g.Children.Add(child);
                EnumVisual(visualChild, child);
            }
        }

        /// <summary>
        /// Builds a geometry group for the given visual taking into account the transform, clip and enumerating the drawings.
        /// </summary>
        /// <param name="myVisual"></param>
        /// <param name="g"></param>
        private static void EnumVisual(Visual myVisual, GeometryGroup g)
        {
            GeometryGroup currentParent = g;
            Matrix m = GetVisualTransform(myVisual);
            MatrixTransform mt = new MatrixTransform();
            mt.Matrix = m;
            currentParent.Transform = mt;

            Geometry clip = VisualTreeHelper.GetClip(myVisual);
            if (clip != null)
            {
                CombinedGeometry combinedGeometry = new CombinedGeometry();
                combinedGeometry.GeometryCombineMode = GeometryCombineMode.Intersect;
                combinedGeometry.Geometry1 = clip;
                GeometryGroup child = new GeometryGroup();
                child.FillRule = FillRule.Nonzero;
                combinedGeometry.Geometry2 = child;
                currentParent.Children.Add(combinedGeometry);
                currentParent = (GeometryGroup)combinedGeometry.Geometry2;
            }

            DrawingGroup dg = VisualTreeHelper.GetDrawing(myVisual);
            if (dg != null)
            {
                if (dg.Transform != null)
                {
                    if (currentParent.Transform != null)
                    {
                        Matrix compositeTransform = new Matrix();
                        compositeTransform = Matrix.Multiply(currentParent.Transform.Value, dg.Transform.Value);
                        MatrixTransform matrixtransform = new MatrixTransform(compositeTransform);
                        currentParent.Transform = matrixtransform;
                    }
                    else
                    {
                        currentParent.Transform = dg.Transform;
                    }
                }

                if (dg.ClipGeometry != null)
                {
                    CombinedGeometry combinedGeometry = new CombinedGeometry();
                    combinedGeometry.GeometryCombineMode = GeometryCombineMode.Intersect;
                    combinedGeometry.Geometry1 = dg.ClipGeometry;
                    GeometryGroup child = new GeometryGroup();
                    child.FillRule = FillRule.Nonzero;
                    combinedGeometry.Geometry2 = child;
                    currentParent.Children.Add(combinedGeometry);
                    currentParent = (GeometryGroup)combinedGeometry.Geometry2;
                }
                EnumerateDrawingGroup(dg, currentParent);
            }
            WalkChildren(myVisual, currentParent);
        }

        /// <summary>
        /// Enumerates through the drawing group of a visual to extract the geometries.
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="currentParent"></param>
        private static void EnumerateDrawingGroup(DrawingGroup dg, GeometryGroup currentParent)
        {
            for (int i = 0; i < dg.Children.Count; i++)
            {
                if (dg.Children[i] is GeometryDrawing)
                {
                    GeometryDrawing geometryDrawing = dg.Children[i] as GeometryDrawing;
                    if (geometryDrawing != null)
                    {
                        Geometry geometry = geometryDrawing.Geometry;
                        currentParent.Children.Add(geometry);
                    }
                }
                else if (dg.Children[i] is DrawingGroup)
                {
                    EnumerateDrawingGroup((DrawingGroup)dg.Children[i], currentParent);
                }
                else if (dg.Children[i] is GlyphRunDrawing)
                {
                    GlyphRunDrawing glyphrunDrawing = dg.Children[i] as GlyphRunDrawing;
                    GlyphRun glyphrun = glyphrunDrawing.GlyphRun;
                    if (glyphrun != null)
                    {
                        Geometry geometry = glyphrun.BuildGeometry();
                        currentParent.Children.Add(geometry);
                    }
                }
                else if (dg.Children[i] is ImageDrawing)
                {
                    ImageDrawing imagedrawing = dg.Children[i] as ImageDrawing;
                    RectangleGeometry rg = new RectangleGeometry(imagedrawing.Rect);
                    currentParent.Children.Add(rg);
                }
                else if (dg.Children[i] is VideoDrawing)
                {
                    VideoDrawing videodrawing = dg.Children[i] as VideoDrawing;
                    RectangleGeometry rg = new RectangleGeometry(videodrawing.Rect);
                    currentParent.Children.Add(rg);
                }
            }
        }

        /// <summary>
        /// returns a matrix corresponding to the combination of the transform and the offset on the visual.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private static Matrix GetVisualTransform(Visual v)
        {
            if (v != null)
            {
                Matrix m = Matrix.Identity;
                Transform transform = VisualTreeHelper.GetTransform(v);
                if (transform != null)
                {
                    Matrix cm = transform.Value;
                    m = Matrix.Multiply(m, cm);
                }
                Vector offset = VisualTreeHelper.GetOffset(v);
                m.Translate(offset.X, offset.Y);
                return m;
            }
            return Matrix.Identity;
        }

        /// <summary>
        /// Intersects a line with a geometry to return the point of intersection
        /// </summary>
        /// <param name="node"></param>
        /// <param name="p"></param>
        /// <param name="centerVector"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static Point FindOppositePoint(PathGeometry node, Point p, Vector centerVector, Transform t)
        {
            Vector xaxis = new Point(1, 0) - new Point(0, 0);
            double angleBetween = Vector.AngleBetween(centerVector, xaxis);
            RotateTransform r = new RotateTransform(angleBetween, p.X, p.Y);
            SortedList pointList = new SortedList();

            for (int i = 0; i < node.Figures.Count; i++)
            {
                PathFigure pf = node.Figures[i];
                Point lastPathFigurePoint = r.Transform(pf.StartPoint);
                for (int j = 0; j < pf.Segments.Count; j++)
                {
                    if (pf.Segments[j] is PolyLineSegment)
                    {
                        PolyLineSegment pls = (PolyLineSegment)pf.Segments[j];
                        for (int k = 0; k < pls.Points.Count; k++)
                        {
                            Point point1 = r.Transform(pls.Points[k]);
                            CheckLineSegmentForIntersection(p, point1, lastPathFigurePoint, pointList);
                            lastPathFigurePoint = point1;
                        }
                    }
                    else if (pf.Segments[j] is LineSegment)
                    {
                        LineSegment ls = (LineSegment)pf.Segments[j];
                        Point p1 = r.Transform(ls.Point);
                        Point p2 = lastPathFigurePoint;
                        lastPathFigurePoint = p1;
                        CheckLineSegmentForIntersection(p, p1, p2, pointList);
                    }
                }
            }
            if (pointList.Count < 1)
            {
                return centerVector + p;
            }
            Point returnPt = new Point((double)pointList.GetByIndex(0), p.Y);
            r.Angle = -1 * angleBetween;
            returnPt = r.Transform(returnPt);
            Vector finalVector = returnPt - p;
            if (finalVector.Length > centerVector.Length)
            {
                return centerVector + p;
            }
            return returnPt;
        }

        private static void CheckLineSegmentForIntersection(Point p, Point p1, Point p2, SortedList pointList)
        {
            double bound1, bound2;
            if (p1.Y > p2.Y)
            {
                bound1 = p2.Y - 0.1;
                bound2 = p1.Y + 0.1;
            }
            else
            {
                bound1 = p1.Y - 0.1;
                bound2 = p2.Y + 0.1;
            }

            if (p.Y > bound1 && p.Y < bound2)
            {
                //find point of intersection
                double x;
                double slope = (p2.Y - p1.Y) / (p2.X - p1.X);
                x = p1.X + (p.Y - p1.Y) / slope;
                pointList[x] = x;
            }
        }

        public static Point[] CatmullRom(Point[] input)
        {
            if (input == null || input.GetLength(0) < 3)
            {
                throw new InvalidOperationException();
            }
            int inputLength = input.GetLength(0);
            Point[] ans = new Point[3 * (inputLength - 2) + 4];
            ans[0] = input[0];
            ans[1] = input[0];
            for (int i = 0; i < input.GetLength(0) - 2; i++)
            {
                Point p1 = input[i];
                Point p2 = input[i + 1];
                Point p3 = input[i + 2];
                Vector v1 = p2 - p3;
                Vector v2 = p1 - p3;
                double theta = Math.Abs(Vector.AngleBetween(v1, v2));
                double h = v1.Length * Math.Sin(theta * Math.PI / 180.0);
                double deltai = Math.Sqrt(Math.Pow(v1.Length, 2) - h * h);
                Vector v3 = p2 - p1;
                double deltai_1 = Math.Sqrt(Math.Pow(v3.Length, 2) - h * h);
                Point b3i_1 = p2 - (deltai_1 / (3 * (deltai + deltai_1))) * (p3 - p1);
                Point b3i_2 = p2 + (deltai / (3 * (deltai + deltai_1))) * (p3 - p1);

                ans[2 + 3 * i] = b3i_1;
                ans[2 + 3 * i + 1] = input[i + 1];
                ans[2 + 3 * i + 2] = b3i_2;
            }
            ans[ans.GetLength(0) - 2] = input[inputLength - 1];
            ans[ans.GetLength(0) - 1] = input[inputLength - 1];
            return ans;
        }

        public static PathGeometry DrawPolyBezier(Point[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (input.Length == 0)
            {
                throw new InvalidOperationException();
            }

            PathGeometry pg = new PathGeometry();
            PathFigure pf = new PathFigure();
            pf.StartPoint = input[0];
            PolyBezierSegment pbs = new PolyBezierSegment();
            for (int i = 1; i < input.GetLength(0); i++)
                pbs.Points.Add(input[i]);
            pf.Segments.Add(pbs);
            pg.Figures.Add(pf);
            return pg;
        }
    }
}
