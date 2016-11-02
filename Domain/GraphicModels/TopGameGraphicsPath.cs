using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace Domain.GraphicModels
{
    public class TopGameGraphicsPath
    {
        public TopGameGraphicsPath()
        {
            Lines = new List<TopGameLine>();
            ArcPaths = new List<TopGameArcPath>();
            ActualPath = new GraphicsPath();
        }

        public void AddLine(TopGamePoint pointA, TopGamePoint pointB)
        {
            Lines.Add(new TopGameLine(pointA, pointB));
            ActualPath.AddLine(pointA.Point, pointB.Point);
        }

        public void AddLine(TopGameLine newLine)
        {
            Lines.Add(newLine);
            ActualPath.AddLine(newLine.Start.Point, newLine.End.Point);
        }

        // The way an arc path is drawn is like this:
        // An arc is basically just a sub-section of an ellipse.
        // First of all, you provide an enclosing rectangle. This defines a whole ellipse (or circle, if it's a square).
        // If you want, you can get the full ellipse by simply giving a sweep angle of 360.
        // The sweep angle defines how much of the ellipse will be drawn.
        // The start angle defines where the ellipse will start:
        // This is interpreted by drawing a horizontal line through the centre of your ellipse - this is your x axis.
        // Then, starting from the left hand side, your angle comes up clockwise from your x axis.
        // Imagine a line which goes from the centre of the ellipse to the edge of the ellipse, at an angle which is measured clockwise
        // from the left hand end of the x axis. Where this line meets the edge of the ellipse, this is where your arc starts.
        // Your arc will then continue clockwise for as far as the sweep angle will take it. So for instance, 180 degrees would give half of the ellipse.
        // See TopGame\Docs\GraphicsPath-Arc.png for explanatory diagram.
        public void AddArcPath(TopGameRectangle rectangle, float startAngle, float sweepAngle)
        {
            ArcPaths.Add(new TopGameArcPath(rectangle, startAngle, sweepAngle));
            ActualPath.AddArc(rectangle.Rectangle, startAngle, sweepAngle);
        }

        public void Reset()
        {
            Lines.Clear();
            ArcPaths.Clear();
            ActualPath.Reset();
        }
        
        public IList<TopGameLine> Lines { get; set; }

        public IList<TopGameArcPath> ArcPaths { get; set; }

        public GraphicsPath ActualPath { get; set; }

        /// <summary>
        /// !! We don't copy the ActualPath, because this is only used by graphics-independent code, which doesn't care about ActualPath.
        /// </summary>
        /// <param name="sourcePath"></param>
        public void Copy(TopGameGraphicsPath sourcePath)
        {
            Lines.Clear();
            foreach (var point in sourcePath.Lines)
            {
                Lines.Add(point);
            }

            ArcPaths.Clear();
            foreach (var arcPath in sourcePath.ArcPaths)
            {
                ArcPaths.Add(arcPath);
            }
        }

        public void AddForwardCircularArc(
            TopGamePoint circleCentre, 
            double circleRadius, 
            double arcStartAngle)
        {
            // !! The y coordinate of the enclosing rectangle represents the TOP of the shape, not the bottom
            var enclosingSquare = new TopGameRectangle(circleCentre, circleRadius);

            // See AddArcPath for explanation of how arcs are drawn.
            AddArcPath(enclosingSquare, (float)arcStartAngle, (float)180);
        }

        public void AddBackwardCircularArc(
            TopGamePoint circleCentre,
            double circleRadius,
            double arcStartAngle)
        {
            // !! The y coordinate of the enclosing rectangle represents the TOP of the shape, not the bottom
            var enclosingSquare = new TopGameRectangle(circleCentre, circleRadius);

            // See AddArcPath for explanation of how arcs are drawn.
            AddArcPath(enclosingSquare, (float)arcStartAngle + 180, (float)-180);
        }
    }
}