using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterLine
    {
        public GoldenMasterLine()
        {
        }

        public GoldenMasterLine(TopGamePoint start, TopGamePoint end)
        {
            Start = new GoldenMasterPoint(start.X, start.Y);
            End = new GoldenMasterPoint(end.X, end.Y);
        }

        [JsonProperty]
        public GoldenMasterPoint Start { get; set; }

        [JsonProperty]
        public GoldenMasterPoint End { get; set; }
    }
}