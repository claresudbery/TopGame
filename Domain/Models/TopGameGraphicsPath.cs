using System.Collections.Generic;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;

namespace Domain.Models
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
    }
}