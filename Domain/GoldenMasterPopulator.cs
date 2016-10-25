using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using Domain.GameModels;
using Domain.GameModels.GoldenMaster;
using Domain.GraphicModels;
using Domain.GraphicModels.GoldenMaster;
using Newtonsoft.Json;
using TopGameWindowsApp;

namespace Domain
{
    public static class GoldenMasterPopulator
    {
        public static void PopulateGoldenMasterGame()
        {
            var allGoldenMasters = GenerateAllGameData();

            string fileNameAndPath = ConfigurationManager.AppSettings["golden-master-game-file"];
            TopGameJsonWriter.WriteToJsonFile(allGoldenMasters, fileNameAndPath);
        }

        public static GoldenMasterGameDataList GenerateAllGameData(int maxPlayers = 12)
        {
            var allGoldenMasters = new GoldenMasterGameDataList();
            
            for (int playerCount = 2; playerCount <= maxPlayers; playerCount++)
            {
                var bmpDisplayLines = new Bitmap(750, 750, System.Drawing.Imaging.PixelFormat.Format16bppRgb565);
    
                ManyHands allHands = new ManyHands(playerCount, null, ref bmpDisplayLines);

                GoldenMasterGameData resultsOfThisCall = allHands.GenerateGoldenMasterGameData();

                allGoldenMasters.GoldenMasters.Add(resultsOfThisCall);
            }

            return allGoldenMasters;
        }

        public static void PopulateGraphicGoldenMaster()
        {
            var allGoldenMasters = GenerateAllGraphicData();

            string fileNameAndPath = ConfigurationManager.AppSettings["golden-master-graphics-file"];
            TopGameJsonWriter.WriteToJsonFile(allGoldenMasters, fileNameAndPath);
        }

        public static GoldenMasterGraphicList GenerateAllGraphicData(int maxPlayers = 12)
        {
            var allGoldenMasters = new GoldenMasterGraphicList();

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
                    GoldenMasterSingleGraphicPass resultsOfThisCall = graphicLoop.GenerateGoldenMasterData(playerCount);

                    allGoldenMasters.GoldenMasters.Add(resultsOfThisCall);
                }
            }

            return allGoldenMasters;
        }

        public static string GenerateAllGraphicDataAsJsonString()
        {
            var allGoldenMasters = GenerateAllGraphicData();

            return JsonConvert.SerializeObject(allGoldenMasters, Formatting.Indented);
        }
    }
}