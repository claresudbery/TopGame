using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterGraphicList
    {
        public GoldenMasterGraphicList()
        {
            GoldenMasters = new List<GoldenMasterSingleGraphicPass>();
        }

        // There is one GoldenMasterSinglePass object for each possible graphics loop.
        // There are eleven possible numbers of players (ranging from 2 to 12).
        // Therefore there are eleven graphics loops for each possible number of segments.
        // There is one segment per card, so there are 52 possible numbers of segments.
        // Therefore there should be (52 x 11) GoldenMasterSinglePass objects in the GoldenMasters collection.
        [JsonProperty]
        public List<GoldenMasterSingleGraphicPass> GoldenMasters { get; set; }
    }
}