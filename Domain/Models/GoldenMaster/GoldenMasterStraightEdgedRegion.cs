using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Models.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterStraightEdgedRegion
    {
        public GoldenMasterStraightEdgedRegion()
        {
            Corners = new List<GoldenMasterPoint>();
        }

        [JsonProperty]
        public IList<GoldenMasterPoint> Corners { get; set; }

        public void Copy(TopGameGraphicsPath sourcePath)
        {
            foreach (var line in sourcePath.Lines)
            {
                Corners.Add(line.Start.ToGoldenMasterPoint());
            }
        }
    }
}