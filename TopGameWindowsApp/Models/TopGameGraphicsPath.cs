using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Drawing2D;

namespace TopGameWindowsApp.Models
{
    public class TopGameGraphicsPath
    {
        // Used by entity framework when inserting records for golden master
        public int TopGameGraphicsPathId { get; set; }

        //// Used by entity framework to handle the one-to-one relationship
        //public int VitalStatisticsId { get; set; }
        //public virtual VitalStatistics VitalStatistics { get; set; }

        public TopGameGraphicsPath()
        {
            PointsOnLine = new List<TopGamePoint>();
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

        public ICollection<TopGamePoint> PointsOnLine { get; set; }

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