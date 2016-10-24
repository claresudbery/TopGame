using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TopGameWindowsApp.Models
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
        public virtual VitalStatistics VitalStatistics { get; set; }

        [JsonProperty]
        public virtual ICollection<TopGameRegion> TopGameRegions { get; set; }
    }
}