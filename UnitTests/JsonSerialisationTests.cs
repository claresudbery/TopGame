using System.Collections.Generic;
using System.Configuration;
using Domain;
using Domain.GameModels.GoldenMaster;
using Domain.GraphicModels;
using Domain.GraphicModels.GoldenMaster;
using NUnit.Framework;
using FluentAssertions;

namespace UnitTests
{
    [TestFixture]
    public class JsonSerialisationTests
    {
        private readonly string _jsonGraphicFileNameAndPath = ConfigurationManager.AppSettings["json-graphics-test-file"];
        private readonly string _jsonGameDataFileNameAndPath = ConfigurationManager.AppSettings["json-game-test-file"];

        [Test]
        public void Will_serialise_TopGameRectangle_object_to_file_and_read_back_again()
        {
            // Arrange
            TopGameRectangle sourceRectangle = new TopGameRectangle(x: 8, y: 9, width: 10, height: 11);

            // Act
            TopGameJsonWriter.WriteToJsonFile(sourceRectangle, _jsonGraphicFileNameAndPath);

            // Assert
            TopGameRectangle result = TopGameJsonWriter.ReadFromJsonFile<TopGameRectangle>(_jsonGraphicFileNameAndPath);
            result.ShouldBeEquivalentTo(sourceRectangle);
        }

        [Test]
        public void Will_serialise_GoldenMasterGraphicList_object_to_file_and_read_back_again()
        {
            // Arrange
            GoldenMasterGraphicList goldenMasterGraphicList = GoldenMasterBuilder.BuildSomeGraphicGoldenMasters();

            // Act
            TopGameJsonWriter.WriteToJsonFile(goldenMasterGraphicList, _jsonGraphicFileNameAndPath);

            // Assert
            GoldenMasterGraphicList result = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterGraphicList>(_jsonGraphicFileNameAndPath);
            result.GoldenMasters.ShouldAllBeEquivalentTo(goldenMasterGraphicList.GoldenMasters);
        }

        [Test]
        public void Will_serialise_GoldenMasterGameDataList_object_to_file_and_read_back_again()
        {
            // Arrange
            GoldenMasterGameDataList goldenMasterGameDataList = GoldenMasterBuilder.BuildSomeGameDataGoldenMasters();

            // Act
            TopGameJsonWriter.WriteToJsonFile(goldenMasterGameDataList, _jsonGameDataFileNameAndPath);

            // Assert
            GoldenMasterGameDataList result = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterGameDataList>(_jsonGameDataFileNameAndPath);
            result.GoldenMasters.ShouldAllBeEquivalentTo(goldenMasterGameDataList.GoldenMasters);
        }

        [Test]
        public void These_tests_can_identify_badly_deserialised_data()
        {
            // Arrange & Act
            GoldenMasterGraphicList goldenMasterGraphicList1 = GoldenMasterBuilderV2.BuildSomeGraphicGoldenMastersV2();
            GoldenMasterGraphicList goldenMasterGraphicList2 = GoldenMasterBuilderV3.BuildSomeGraphicGoldenMastersV3();

            // Assert
            goldenMasterGraphicList1.GoldenMasters.ShouldAllBeEquivalentTo(goldenMasterGraphicList2.GoldenMasters);
        }
    }
}
