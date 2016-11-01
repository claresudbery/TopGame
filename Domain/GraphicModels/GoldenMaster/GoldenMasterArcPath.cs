using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    public class GoldenMasterArcPath
    {
        public GoldenMasterArcPath()
        {
        }

        public GoldenMasterArcPath(TopGameRectangle rectangle, float startAngle, float sweepAngle)
        {
            Rectangle = new GoldenMasterRectangle();
            Rectangle.Copy(rectangle);
            StartAngle = startAngle;
            SweepAngle = sweepAngle;
        }

        public GoldenMasterRectangle Rectangle { get; set; }

        public float StartAngle { get; set; }

        public float SweepAngle { get; set; }
    }
}