﻿using System.Collections.Generic;
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

            // Act
            var latestCalculatedData = GoldenMasterPopulator.GenerateAllData();
            var latestCalculatedDataAsJsonString = GoldenMasterPopulator.GenerateAllDataAsJsonString();

            // Assert
            latestCalculatedData.GoldenMasters.Count.Equals(storedGoldenMaster.GoldenMasters.Count);
            latestCalculatedData.GoldenMasters[0].ShouldBeEquivalentTo(storedGoldenMaster.GoldenMasters[0]);
            latestCalculatedData.GoldenMasters[300].ShouldBeEquivalentTo(storedGoldenMaster.GoldenMasters[300]);
            latestCalculatedData.GoldenMasters[latestCalculatedData.GoldenMasters.Count - 1].ShouldBeEquivalentTo(storedGoldenMaster.GoldenMasters[latestCalculatedData.GoldenMasters.Count - 1]);
            Assert.That(latestCalculatedDataAsJsonString, Is.EqualTo(storedGoldenMasterAsJsonString));
        }

        [Test]
        public void When_max_players_is_two_then_generated_data_should_be_the_same_as_version_001_with_new_properties()
        {
            // Arrange & Act
            var topGameAppPath = TestContext.CurrentContext.TestDirectory + @"..\..\..\GoldenMasters\";
            GoldenMasterList goldenMaster001 = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterList>(topGameAppPath + "TopGame-GoldenMaster-001-new-props.json");

            // Act
            var latestCalculatedData = GoldenMasterPopulator.GenerateAllData(2);

            // Assert
            latestCalculatedData.GoldenMasters.Count.Equals(goldenMaster001.GoldenMasters.Count);
            latestCalculatedData.ShouldBeEquivalentTo(goldenMaster001);
        }

        [Test]
        public void Version_002_should_be_the_same_as_version_004()
        {
            // Arrange & Act
            var topGameAppPath = TestContext.CurrentContext.TestDirectory + @"..\..\..\GoldenMasters\";
            GoldenMasterList goldenMaster002 = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterList>(topGameAppPath + "TopGame-GoldenMaster-002.json");
            string goldenMaster002AsJsonString;
            using (StreamReader file = File.OpenText(topGameAppPath + "TopGame-GoldenMaster-002.json"))
            {
                goldenMaster002AsJsonString = file.ReadToEnd();
            }
            GoldenMasterList goldenMaster004 = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterList>(topGameAppPath + "TopGame-GoldenMaster-004.json");
            string goldenMaster004AsJsonString;
            using (StreamReader file = File.OpenText(topGameAppPath + "TopGame-GoldenMaster-004.json"))
            {
                goldenMaster004AsJsonString = file.ReadToEnd();
            }

            // Assert
            goldenMaster002.GoldenMasters.Count.Equals(goldenMaster004.GoldenMasters.Count);
            goldenMaster002.GoldenMasters[0].ShouldBeEquivalentTo(goldenMaster004.GoldenMasters[0]);
            goldenMaster002.GoldenMasters[300].ShouldBeEquivalentTo(goldenMaster004.GoldenMasters[300]);
            goldenMaster002.GoldenMasters[goldenMaster002.GoldenMasters.Count - 1].ShouldBeEquivalentTo(goldenMaster004.GoldenMasters[goldenMaster004.GoldenMasters.Count - 1]);
            Assert.That(goldenMaster002AsJsonString, Is.EqualTo(goldenMaster004AsJsonString));
        }
    }
}
