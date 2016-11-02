using System;
using System.Drawing;
using Domain.GraphicModels.GoldenMaster;

namespace Domain.GraphicModels
{
    public class TopGameLine
    {
        public TopGameLine()
        {
            // Do nothing
        }

        public TopGameLine(TopGamePoint start, TopGamePoint end)
        {
            Start = start;
            End = end;
        }

        public TopGameLine(PointF start, PointF end)
        {
            Start = new TopGamePoint((int)Math.Floor(start.X), (int)Math.Floor(start.Y));
            End = new TopGamePoint((int)Math.Ceiling(end.X), (int)Math.Ceiling(end.Y));
        }

        public TopGamePoint Start { get; set; }

        public TopGamePoint End { get; set; }

        public GoldenMasterLine ToGoldenMasterLine()
        {
            return new GoldenMasterLine(Start, End);
        }
    }
}