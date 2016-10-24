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
            string storedGoldenMasterAsJsonString;
            using (StreamReader file = File.OpenText(topGameAppPath + _currentGoldenMasterFileName))
            {
                storedGoldenMasterAsJsonString = file.ReadToEnd();
            }
            var graphicLoops = new List<OnePlayerGraphicsLoop> { new OnePlayerGraphicsLoop() };

            // Act
            var latestCalculatedData = GoldenMasterPopulator.GenerateAllData(graphicLoops);
            var latestCalculatedDataAsJsonString = GoldenMasterPopulator.GenerateAllDataAsJsonString(graphicLoops);

            // Assert
            latestCalculatedData.GoldenMasters.Count.Equals(storedGoldenMaster.GoldenMasters.Count);
            latestCalculatedData.GoldenMasters[0].ShouldBeEquivalentTo(storedGoldenMaster.GoldenMasters[0]);
            latestCalculatedData.GoldenMasters[300].ShouldBeEquivalentTo(storedGoldenMaster.GoldenMasters[300]);
            latestCalculatedData.GoldenMasters[latestCalculatedData.GoldenMasters.Count - 1].ShouldBeEquivalentTo(storedGoldenMaster.GoldenMasters[latestCalculatedData.GoldenMasters.Count - 1]);
            Assert.That(latestCalculatedDataAsJsonString, Is.EqualTo(storedGoldenMasterAsJsonString));
        }

        [Test]
        public void GoldenMaster_data_is_same_as_version_002_when_empty_graphics_loops_are_used()
        {
            // Arrange
            var topGameAppPath = TestContext.CurrentContext.TestDirectory + @"..\..\..\GoldenMasters\";
            GoldenMasterList goldenMaster002 = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterList>(topGameAppPath + _currentGoldenMasterFileName);
            string goldenMaster002AsJsonString;
            using (StreamReader file = File.OpenText(topGameAppPath + _currentGoldenMasterFileName))
            {
                goldenMaster002AsJsonString = file.ReadToEnd();
            }
            var graphicLoops = new List<OnePlayerGraphicsLoop> {new OnePlayerGraphicsLoop()};

            // Act
            var latestGoldenMaster = GoldenMasterPopulator.GenerateAllData(graphicLoops);
            var latestGoldenMasterAsJsonString = GoldenMasterPopulator.GenerateAllDataAsJsonString(graphicLoops);

            // Assert
            latestGoldenMaster.GoldenMasters.Count.Equals(goldenMaster002.GoldenMasters.Count);
            latestGoldenMaster.GoldenMasters[0].ShouldBeEquivalentTo(goldenMaster002.GoldenMasters[0]);
            latestGoldenMaster.GoldenMasters[300].ShouldBeEquivalentTo(goldenMaster002.GoldenMasters[300]);
            latestGoldenMaster.GoldenMasters[latestGoldenMaster.GoldenMasters.Count - 1].ShouldBeEquivalentTo(goldenMaster002.GoldenMasters[latestGoldenMaster.GoldenMasters.Count - 1]);
            Assert.That(latestGoldenMasterAsJsonString, Is.EqualTo(goldenMaster002AsJsonString));
        }
    }
}
