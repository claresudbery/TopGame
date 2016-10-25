using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Domain;
using Domain.Models;
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
            GoldenMasterList storedGoldenMaster = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterList>(topGameAppPath + _currentGoldenMasterFileName);
            string storedGoldenMasterAsJsonString = GetFileContentsAsJsonString(topGameAppPath + _currentGoldenMasterFileName);

            // Act
            var latestCalculatedData = GoldenMasterPopulator.GenerateAllData();
            var latestCalculatedDataAsJsonString = GoldenMasterPopulator.GenerateAllDataAsJsonString();

            // Assert
            latestCalculatedData.GoldenMasters.Count.Equals(storedGoldenMaster.GoldenMasters.Count);
            latestCalculatedData.GoldenMasters[0].ShouldBeEquivalentTo(storedGoldenMaster.GoldenMasters[0]);
            latestCalculatedData.GoldenMasters[300].ShouldBeEquivalentTo(storedGoldenMaster.GoldenMasters[300]);
            latestCalculatedData.GoldenMasters[latestCalculatedData.GoldenMasters.Count - 1].ShouldBeEquivalentTo(storedGoldenMaster.GoldenMasters[latestCalculatedData.GoldenMasters.Count - 1]);
            //Assert.That(latestCalculatedDataAsJsonString, Is.EqualTo(storedGoldenMasterAsJsonString));
        }

        [Test]
        public void Version_002_should_be_the_same_as_version_004()
        {
            // Arrange & Act
            var topGameAppPath = TestContext.CurrentContext.TestDirectory + @"..\..\..\GoldenMasters\";
            GoldenMasterList goldenMaster002 = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterList>(topGameAppPath + "TopGame-GoldenMaster-002.json");
            string goldenMaster002AsJsonString = GetFileContentsAsJsonString(topGameAppPath + "TopGame-GoldenMaster-002.json");
            GoldenMasterList goldenMaster004 = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterList>(topGameAppPath + "TopGame-GoldenMaster-004.json");
            string goldenMaster004AsJsonString = GetFileContentsAsJsonString(topGameAppPath + "TopGame-GoldenMaster-004.json");

            // Assert
            goldenMaster002.GoldenMasters.Count.Equals(goldenMaster004.GoldenMasters.Count);
            goldenMaster002.GoldenMasters[0].ShouldBeEquivalentTo(goldenMaster004.GoldenMasters[0]);
            goldenMaster002.GoldenMasters[300].ShouldBeEquivalentTo(goldenMaster004.GoldenMasters[300]);
            goldenMaster002.GoldenMasters[goldenMaster002.GoldenMasters.Count - 1].ShouldBeEquivalentTo(goldenMaster004.GoldenMasters[goldenMaster004.GoldenMasters.Count - 1]);
            Assert.That(goldenMaster002AsJsonString, Is.EqualTo(goldenMaster004AsJsonString));
        }

        private string GetFileContentsAsJsonString(string fileNameAndPath)
        {
            string fileContentsAsJsonString = "";

            using (StreamReader file = File.OpenText(fileNameAndPath))
            {
                fileContentsAsJsonString = file.ReadToEnd();
                fileContentsAsJsonString = fileContentsAsJsonString.Replace("\n", "\r\n");
            }

            return fileContentsAsJsonString;
        }
    }
}
