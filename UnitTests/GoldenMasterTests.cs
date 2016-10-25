using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Domain;
using Domain.GraphicModels.GoldenMaster;
using NUnit.Framework;
using FluentAssertions;
using Newtonsoft.Json;

namespace UnitTests
{
    [TestFixture]
    public class GoldenMasterTests
    {
        private readonly string _currentGoldenMasterFileName = ConfigurationManager.AppSettings["current-golden-master-file"];

        [Test]
        public void Latest_calculated_data_should_be_the_same_as_stored_golden_master_data()
        {
            // Arrange
            var topGameAppPath = TestContext.CurrentContext.TestDirectory + @"..\..\..\GoldenMasters\";
            GoldenMasterGraphicList storedGoldenMasterGraphic = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterGraphicList>(topGameAppPath + _currentGoldenMasterFileName);

            // Act
            var latestCalculatedData = GoldenMasterPopulator.GenerateAllGraphicData();

            // Assert
            latestCalculatedData.ShouldBeEquivalentTo(storedGoldenMasterGraphic);
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
