using System.Configuration;
using Domain;
using Domain.GraphicModels;
using Domain.GraphicModels.GoldenMaster;
using NUnit.Framework;
using FluentAssertions;

namespace UnitTests
{
    [TestFixture]
    public class JsonSerialisationTests
    {
        private readonly string _jsonFileNameAndPath = ConfigurationManager.AppSettings["json-graphics-test-file"];

        [Test]
        public void Will_serialise_TopGameRectangle_object_to_file_and_read_back_again()
        {
            // Arrange
            TopGameRectangle sourceRectangle = new TopGameRectangle(x: 8, y: 9, width: 10, height: 11);

            // Act
            TopGameJsonWriter.WriteToJsonFile(sourceRectangle, _jsonFileNameAndPath);

            // Assert
            TopGameRectangle result = TopGameJsonWriter.ReadFromJsonFile<TopGameRectangle>(_jsonFileNameAndPath);
            result.ShouldBeEquivalentTo(sourceRectangle);
        }

        [Test]
        public void Will_serialise_GoldenMasterGraphicList_object_to_file_and_read_back_again()
        {
            // Arrange
            GoldenMasterGraphicList goldenMasterGraphicList = GoldenMasterBuilder.BuildSomeGoldenMasters();

            // Act
            TopGameJsonWriter.WriteToJsonFile(goldenMasterGraphicList, _jsonFileNameAndPath);

            // Assert
            GoldenMasterGraphicList result = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterGraphicList>(_jsonFileNameAndPath);
            result.GoldenMasters.ShouldAllBeEquivalentTo(goldenMasterGraphicList.GoldenMasters);
        }

        [Test]
        public void These_tests_can_identify_badly_deserialised_data()
        {
            // Arrange & Act
            GoldenMasterGraphicList goldenMasterGraphicList1 = GoldenMasterBuilderV2.BuildSomeGoldenMastersV2();
            GoldenMasterGraphicList goldenMasterGraphicList2 = GoldenMasterBuilderV3.BuildSomeGoldenMastersV3();

            // Assert
            goldenMasterGraphicList1.GoldenMasters.ShouldAllBeEquivalentTo(goldenMasterGraphicList2.GoldenMasters);
        }
    }
}
