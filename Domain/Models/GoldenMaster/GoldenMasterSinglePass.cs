using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Models.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterSinglePass
    {
        public GoldenMasterSinglePass()
        {
            MiniPetalRegions = new List<GoldenMasterGraphicsPath>();
            ArcRegions = new List<GoldenMasterArcRegion>();
            StraightEdgedRegions = new List<GoldenMasterStraightEdgedRegion>();
        }
        
        [JsonProperty]
        public int NumCardsInLoop { get; set; }

        [JsonProperty]
        public int NumPlayersInGame { get; set; }

        [JsonProperty]
        public GoldenMasterVitalStatistics VitalStatistics { get; set; }

        [JsonProperty]
        public IList<GoldenMasterGraphicsPath> MiniPetalRegions { get; set; }

        [JsonProperty]
        public IList<GoldenMasterArcRegion> ArcRegions { get; set; }

        [JsonProperty]
        public IList<GoldenMasterStraightEdgedRegion> StraightEdgedRegions { get; set; }
    }
}