using Newtonsoft.Json;

namespace Domain.Models.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterRectangle
    {
        public GoldenMasterRectangle()
        {
            // Do nothing
        }

        [JsonProperty]
        public int X { get; set; }

        [JsonProperty]
        public int Y { get; set; }

        [JsonProperty]
        public int Width { get; set; }

        [JsonProperty]
        public int Height { get; set; }

        public void Copy(TopGameRectangle sourceRectangle)
        {
            Width = sourceRectangle.Width;
            Height = sourceRectangle.Height;
            X = sourceRectangle.X;
            Y = sourceRectangle.Y;
        }
    }
}