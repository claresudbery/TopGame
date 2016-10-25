using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Models.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterRegion
    {
        public GoldenMasterRegion()
        {
            TopGamePoints = new List<GoldenMasterPoint>();
        }

        [JsonProperty]
        public IList<GoldenMasterPoint> TopGamePoints { get; set; }
    }
}