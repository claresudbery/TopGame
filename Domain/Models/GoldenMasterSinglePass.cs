using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterSinglePass
    {
        public GoldenMasterSinglePass()
        {
            TopGameRegions = new List<GoldenMasterRegion>();
        }
        
        [JsonProperty]
        public int NumCardsInLoop { get; set; }

        [JsonProperty]
        public int NumPlayersInGame { get; set; }

        [JsonProperty]
        public VitalStatistics VitalStatistics { get; set; }

        [JsonProperty]
        public IList<GoldenMasterRegion> TopGameRegions { get; set; }
    }
}