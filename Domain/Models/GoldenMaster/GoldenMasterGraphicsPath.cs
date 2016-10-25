using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Models.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterGraphicsPath
    {
        public GoldenMasterGraphicsPath()
        {
            PointsOnLine = new List<GoldenMasterPoint>();
        }

        [JsonProperty]
        public IList<GoldenMasterPoint> PointsOnLine { get; set; }

        public void Copy(TopGameGraphicsPath sourcePath)
        {
            PointsOnLine.Clear();
            foreach (var point in sourcePath.PointsOnLine)
            {
                PointsOnLine.Add(point.ToGoldenMasterPoint());
            }
        }
    }
}