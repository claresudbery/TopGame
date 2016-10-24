using Domain;
using Domain.Models;
using NUnit.Framework;
using FluentAssertions;

namespace UnitTests
{
    [TestFixture]
    public class JsonSerialisationTests
    {
        private const string JsonFileNameAndPath = @"c:\Temp\TopGame-GoldenMaster-testing.json";

        [Test]
        public void Will_serialise_TopGameRectangle_object_to_file_and_read_back_again()
        {
            // Arrange
            TopGameRectangle sourceRectangle = new TopGameRectangle(x: 8, y: 9, width: 10, height: 11);

            // Act
            TopGameJsonWriter.WriteToJsonFile(sourceRectangle, JsonFileNameAndPath);

            // Assert
            TopGameRectangle result = TopGameJsonWriter.ReadFromJsonFile<TopGameRectangle>(JsonFileNameAndPath);
            result.ShouldBeEquivalentTo(sourceRectangle);
        }

        [Test]
        public void Will_serialise_GoldenMasterSinglePass_object_to_file_and_read_back_again()
        {
            // Arrange
            GoldenMasterList goldenMasterList = GoldenMasterBuilder.BuildSomeGoldenMasters();

            // Act
            TopGameJsonWriter.WriteToJsonFile(goldenMasterList, JsonFileNameAndPath);

            // Assert
            GoldenMasterList result = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterList>(JsonFileNameAndPath);
            result.GoldenMasters.ShouldAllBeEquivalentTo(goldenMasterList.GoldenMasters);
        }

        [Test]
        public void These_tests_can_identify_badly_deserialised_data()
        {
            // Arrange & Act
            GoldenMasterList goldenMasterList1 = GoldenMasterBuilderV2.BuildSomeGoldenMastersV2();
            GoldenMasterList goldenMasterList2 = GoldenMasterBuilderV3.BuildSomeGoldenMastersV3();

            // Assert
            goldenMasterList1.GoldenMasters.ShouldAllBeEquivalentTo(goldenMasterList2.GoldenMasters);
        }
    }
}
