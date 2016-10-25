using System.Collections.Generic;

namespace Domain.GameModels.GoldenMaster
{
    public class GoldenMasterGameData
    {
        public GoldenMasterGameData()
        {
            Turns = new List<GoldenMasterTurnInfo>();
        }

        public string StartDeck { get; set; }

        public List<GoldenMasterTurnInfo> Turns { get; set; }

        public void Clear()
        {
            Turns.Clear();
        }
    }
}