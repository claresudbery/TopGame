using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Domain;
using Domain.GameModels.GoldenMaster;
using Domain.GraphicModels.GoldenMaster;
using NUnit.Framework;
using FluentAssertions;
using Newtonsoft.Json;

namespace UnitTests
{
    [TestFixture]
    public class GoldenMasterTests
    {
        private readonly string _currentGraphicGoldenMasterFileName = ConfigurationManager.AppSettings["current-golden-master-graphics-file"];
        private readonly string _currentGameDataGoldenMasterFileName = ConfigurationManager.AppSettings["current-golden-master-game-file"];
        private const string GoldenMasterFolder = @"..\..\..\GoldenMasters\";

        [Test]
        public void Latest_calculated_graphic_data_should_be_the_same_as_stored_golden_master_graphic_data()
        {
            // Arrange
            var topGameGoldenMasterPath = TestContext.CurrentContext.TestDirectory + GoldenMasterFolder;
            GoldenMasterGraphicList storedGraphicGoldenMaster = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterGraphicList>(topGameGoldenMasterPath + _currentGraphicGoldenMasterFileName);

            // Act
            GoldenMasterGraphicList latestCalculatedData = GoldenMasterPopulator.GenerateAllGraphicData();

            // Assert
            latestCalculatedData.GoldenMasters.Count.Equals(storedGraphicGoldenMaster.GoldenMasters.Count);
            foreach (var storedGoldenMaster in storedGraphicGoldenMaster.GoldenMasters)
            {
                int currentIndex = storedGraphicGoldenMaster.GoldenMasters.IndexOf(storedGoldenMaster);
                latestCalculatedData.GoldenMasters[currentIndex].ShouldBeEquivalentTo(storedGoldenMaster);
            }
        }

        [Test]
        public void Latest_calculated_game_data_should_be_the_same_as_stored_golden_master_game_data()
        {
            // Arrange
            var topGameGoldenMasterPath = TestContext.CurrentContext.TestDirectory + GoldenMasterFolder;
            GoldenMasterGameDataList storedGoldenMasterGameData = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterGameDataList>(topGameGoldenMasterPath + _currentGameDataGoldenMasterFileName);

            // Act
            GoldenMasterGameDataList latestCalculatedData = GoldenMasterPopulator.GenerateAllGameData();

            // Assert
            latestCalculatedData.ShouldBeEquivalentTo(storedGoldenMasterGameData);
        }

        private string GetFileContentsAsJsonString(string fileNameAndPath)
        {
            string fileContentsAsJsonString = "";

            using (StreamReader file = File.OpenText(fileNameAndPath))
            {
                fileContentsAsJsonString = file.ReadToEnd();
                //fileContentsAsJsonString = fileContentsAsJsonString.Replace("\n", "\r\n");
            }

            return fileContentsAsJsonString;
        }
    }
}
