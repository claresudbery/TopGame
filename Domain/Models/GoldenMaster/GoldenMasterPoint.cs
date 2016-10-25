using Newtonsoft.Json;

namespace Domain.Models.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterPoint
    {
        public GoldenMasterPoint()
        {
        }

        public GoldenMasterPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        [JsonProperty]
        public int X { get; set; }

        [JsonProperty]
        public int Y { get; set; }
    }
}