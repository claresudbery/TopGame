using System.Collections.Generic;

namespace Domain.GameModels.GoldenMaster
{
    public class GoldenMasterGameData
    {
        public GoldenMasterGameData()
        {
            Turns = new List<GoldenMasterTurnInfo>();
            PlayerStartHands = new List<string>();
        }

        public string StartDeck { get; set; }

        public int NumPlayers { get; set; }

        public List<string> PlayerStartHands { get; set; }

        public List<GoldenMasterTurnInfo> Turns { get; set; }

        public void Clear()
        {
            Turns.Clear();
        }
    }
}