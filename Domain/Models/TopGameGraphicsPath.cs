using System.Collections.Generic;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;

namespace Domain.Models
{
    public class TopGameGraphicsPath
    {
        public TopGameGraphicsPath()
        {
            PointsOnLine = new List<TopGamePoint>();
        }

        public void Initialise()
        {
            // The json deserialisation tests fail for some reason on the ShouldAllBeEquivalentTo assertion if the GraphicsPath objects are newed up on creation.
            // So we only new them up in production code.
            ActualPath = new GraphicsPath();
        }

        public void AddLine(TopGamePoint pointA, TopGamePoint pointB)
        {
            PointsOnLine.Add(pointA);
            ActualPath.AddLine(pointA.Point, pointB.Point);
        }

        public void Reset()
        {
            PointsOnLine.Clear();
            ActualPath.Reset();
        }
        
        public IList<TopGamePoint> PointsOnLine { get; set; }

        public GraphicsPath ActualPath { get; set; }

        /// <summary>
        /// !! We don't copy the ActualPath, because this is only used by graphics-independent code, which doesn't care about ActualPath.
        /// </summary>
        /// <param name="sourcePath"></param>
        public void Copy(TopGameGraphicsPath sourcePath)
        {
            PointsOnLine.Clear();
            foreach (var point in sourcePath.PointsOnLine)
            {
                PointsOnLine.Add(point);
            }
        }
    }
}