using Newtonsoft.Json;

namespace Domain.Models.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
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

        [JsonProperty]
        public GoldenMasterRectangle Rectangle { get; set; }

        [JsonProperty]
        public float StartAngle { get; set; }

        [JsonProperty]
        public float SweepAngle { get; set; }
    }
}