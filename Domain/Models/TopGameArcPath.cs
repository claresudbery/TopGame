using Domain.Models.GoldenMaster;

namespace Domain.Models
{
    public class TopGameArcPath
    {
        public TopGameArcPath()
        {
            // Do nothing
        }

        public TopGameArcPath(TopGameRectangle rectangle, float startAngle, float sweepAngle)
        {
            Rectangle = rectangle;
            StartAngle = startAngle;
            SweepAngle = sweepAngle;
        }

        public TopGameRectangle Rectangle { get; set; }

        public float StartAngle { get; set; }

        public float SweepAngle { get; set; }
        
        public GoldenMasterArcPath ToGoldenMasterArcPath()
        {
            return new GoldenMasterArcPath(Rectangle, StartAngle, SweepAngle);
        }
    }
}