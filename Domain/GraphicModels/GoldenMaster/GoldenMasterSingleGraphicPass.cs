using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    public class GoldenMasterSingleGraphicPass
    {
        public GoldenMasterSingleGraphicPass()
        {
            Regions = new List<GoldenMasterRegion>();
        }
        
        public int NumCardsInLoop { get; set; }

        public int NumPlayersInGame { get; set; }

        public GoldenMasterVitalGraphicStatistics VitalGraphicStatistics { get; set; }

        public IList<GoldenMasterRegion> Regions { get; set; }
    }
}