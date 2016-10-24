using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TopGameWindowsApp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    [DataContract]
    public class GoldenMasterSinglePass
    {
        [DataMember]
        [JsonProperty("GoldenMasterSinglePassId")]
        public int GoldenMasterSinglePassId { get; set; }

        public int NumTotalSegments { get; set; }

        public GoldenMasterSinglePass()
        {
            TopGameRegions = new List<TopGameRegion>();
        }

        public int VitalStatisticsId { get; set; }
        public virtual VitalStatistics VitalStatistics { get; set; }

        public virtual ICollection<TopGameRegion> TopGameRegions { get; set; }
    }
}