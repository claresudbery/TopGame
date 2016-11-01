using System.Collections.Generic;
using Domain.GraphicModels.GoldenMaster;
using Newtonsoft.Json;
using TopGameWindowsApp;

namespace Domain.GameModels.GoldenMaster
{
    public class GoldenMasterGameDataList
    {
        public GoldenMasterGameDataList()
        {
            GoldenMasters = new List<GoldenMasterGameData>();
        }

        // There is one GoldenMasterGameData object for each possible number of players (min players is 2, max is 12).
        // They all use the same deck, which is an unshuffled deck.
        public List<GoldenMasterGameData> GoldenMasters { get; set; }
    }
}