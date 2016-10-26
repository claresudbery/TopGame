using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterSingleGraphicPass
    {
        public GoldenMasterSingleGraphicPass()
        {
            MiniPetalRegions = new List<GoldenMasterMiniPetalRegion>();
            ArcRegions = new List<GoldenMasterArcRegion>();
            StraightEdgedRegions = new List<GoldenMasterStraightEdgedRegion>();
        }
        
        [JsonProperty]
        public int NumCardsInLoop { get; set; }

        [JsonProperty]
        public int NumPlayersInGame { get; set; }

        [JsonProperty]
        public GoldenMasterVitalGraphicStatistics VitalGraphicStatistics { get; set; }

        [JsonProperty]
        public IList<GoldenMasterMiniPetalRegion> MiniPetalRegions { get; set; }

        [JsonProperty]
        public IList<GoldenMasterArcRegion> ArcRegions { get; set; }

        [JsonProperty]
        public IList<GoldenMasterStraightEdgedRegion> StraightEdgedRegions { get; set; }
    }
}