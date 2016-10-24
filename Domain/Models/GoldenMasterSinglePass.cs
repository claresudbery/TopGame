using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterSinglePass
    {
        public GoldenMasterSinglePass()
        {
            TopGameRegions = new List<TopGameRegion>();
        }
        
        [JsonProperty]
        public int NumTotalSegments { get; set; }
        
        [JsonProperty]
        public VitalStatistics VitalStatistics { get; set; }

        [JsonProperty]
        public IList<TopGameRegion> TopGameRegions { get; set; }
    }
}