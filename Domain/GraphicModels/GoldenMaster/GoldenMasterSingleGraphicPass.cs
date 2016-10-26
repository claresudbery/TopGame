using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterSingleGraphicPass
    {
        public GoldenMasterSingleGraphicPass()
        {
            Regions = new List<GoldenMasterRegion>();
        }
        
        [JsonProperty]
        public int NumCardsInLoop { get; set; }

        [JsonProperty]
        public int NumPlayersInGame { get; set; }

        [JsonProperty]
        public GoldenMasterVitalGraphicStatistics VitalGraphicStatistics { get; set; }

        [JsonProperty]
        public IList<GoldenMasterRegion> Regions { get; set; }
    }
}