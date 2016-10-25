using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Domain.Models;
using Domain.Models.GoldenMaster;
using Newtonsoft.Json;

namespace Domain
{
    public static class GoldenMasterPopulator
    {
        /// <summary>
        /// Use just one of the hands (any will do) to load all possible data, to be stored for golden master purposes
        /// </summary>
        public static void PopulateGoldenMaster()
        {
            var allGoldenMasters = GenerateAllData();

            string fileNameAndPath = ConfigurationManager.AppSettings["golden-master-file"];
            TopGameJsonWriter.WriteToJsonFile(allGoldenMasters, fileNameAndPath);
        }

        public static GoldenMasterList GenerateAllData(int maxPlayers = 12)
        {
            var allGoldenMasters = new GoldenMasterList();

            // Graphics loops have three distinguishing features:
            // The first distinguishing feature is how many segments a graphics loop has.
            //      Each segment represents one card.
            // The second distinguishing feature is the loop's position on the screen, which is determined by its rotation angle.
            //      But the rotation angle is applied at the very end of OnePlayerGraphicsLoop.PrepareActualData (via OnePlayerGraphicsLoop.RotateByAngle)
            //      ...and doesn't really affect the actual calculations, wich are the meat of what we are trying to record.
            // The third distinguishing feature is the loop's angle share, which is 360 divided by the number of players.
            //      The max number of players is 12, and the min is 2.
            // So, in order to have a record of all the possible calculated data (for golden master purposes), 
            //      we call OnePlayerGraphicsLoop.PrepareActualData repeatedly (via OnePlayerGraphicsLoop.PopulateGoldenMaster)
            //      - 52 times, for all the possible numbers of segments...
            //      ...and for each one of those 52, we do 11 versions, for all the possible numbers of players.
            for (int iCardCount = 1; iCardCount <= 52; iCardCount++)
            {
                for (int playerCount = 2; playerCount <= maxPlayers; playerCount++)
                {
                    OnePlayerGraphicsLoop graphicLoop = new OnePlayerGraphicsLoop();
                    graphicLoop.SetNumTotalSegments(iCardCount);
                    double angleShare = 360 / (playerCount + 1);
                    double maxCentralAngle = OnePlayerGraphicsLoop.GetMaxCentralAngle(angleShare, playerCount + 1);

                    // Set all the angles - each hand of cards gets the same proportion of the circle
                    graphicLoop.SetAngles(maxCentralAngle, angleShare);
                    GoldenMasterSinglePass resultsOfThisCall = graphicLoop.GenerateGoldenMasterData(playerCount);

                    allGoldenMasters.GoldenMasters.Add(resultsOfThisCall);
                }
            }

            return allGoldenMasters;
        }

        public static string GenerateAllDataAsJsonString()
        {
            var allGoldenMasters = GenerateAllData();

            return JsonConvert.SerializeObject(allGoldenMasters, Formatting.Indented);
        }
    }
}