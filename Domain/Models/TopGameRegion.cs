using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TopGameRegion
    {
        public TopGameRegion()
        {
            TopGamePoints = new List<TopGamePoint>();
        }

        [JsonProperty]
        public IList<TopGamePoint> TopGamePoints { get; set; }

        public GoldenMasterRegion ToGoldenMasterRegion()
        {
            var goldenMasterRegion = new GoldenMasterRegion();
            foreach(var point in TopGamePoints)
            {
                goldenMasterRegion.TopGamePoints.Add(new GoldenMasterPoint(point.X, point.Y));
            }
            return goldenMasterRegion;
        }
    }
}