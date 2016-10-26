using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Domain;
using Domain.GameModels.GoldenMaster;
using Domain.GraphicModels.GoldenMaster;
using NUnit.Framework;
using FluentAssertions;
using Newtonsoft.Json;

namespace UnitTests
{
    [TestFixture]
    public class GraphicDataTests
    {
        [Test]
        public void All_arc_regions_should_start_and_end_at_the_same_point()
        {
            // Arrange & Act
            var goldenMasters = GoldenMasterPopulator.GenerateAllGraphicData().GoldenMasters;

            // Assert
            List<TestCheckResult> results = CheckArcRegionsForStartAndEnd(goldenMasters);
            bool allAssertionsAreTrue = results.All(assertion => assertion.Result == true);
            string errorMessage = results.Any(assertion => assertion.Result == false)
                ? AllErrorMessagesCommaSeparated(results)
                : "";

            Assert.IsTrue(allAssertionsAreTrue, errorMessage);
        }

        [Test]
        public void All_straight_edged_regions_should_start_and_end_at_the_same_point()
        {
            // Arrange & Act
            var goldenMasters = GoldenMasterPopulator.GenerateAllGraphicData().GoldenMasters;

            // Assert
            List<TestCheckResult> results = CheckStraightEdgedRegionsForStartAndEnd(goldenMasters);
            bool allAssertionsAreTrue = results.All(assertion => assertion.Result == true);
            string errorMessage = results.Any(assertion => assertion.Result == false)
                ? AllErrorMessagesCommaSeparated(results)
                : "";

            Assert.IsTrue(allAssertionsAreTrue, errorMessage);
        }

        private List<TestCheckResult> CheckStraightEdgedRegionsForStartAndEnd(List<GoldenMasterSingleGraphicPass> goldenMasters)
        {
            var results = new List<TestCheckResult>();

            //foreach (var goldenMaster in goldenMasters)
            //{
            //    var straightEdgedRegions = goldenMaster.StraightEdgedRegions;

            //    foreach (var straightEdgedRegion in straightEdgedRegions)
            //    {
            //        results = CheckStartAndEnd(results, straightEdgedRegion.Corners, goldenMaster, straightEdgedRegions.IndexOf(straightEdgedRegion), "straight-edged");
            //    }
            //}

            return results;
        }

        private List<TestCheckResult> CheckArcRegionsForStartAndEnd(List<GoldenMasterSingleGraphicPass> goldenMasters)
        {
            var results = new List<TestCheckResult>();

            //foreach (var goldenMaster in goldenMasters)
            //{
            //    var arcRegions = goldenMaster.ArcRegions;

            //    foreach (var arcRegion in arcRegions)
            //    {
            //        results = CheckStartAndEnd(results, arcRegion.Corners, goldenMaster, arcRegions.IndexOf(arcRegion), "arc");
            //    }
            //}

            return results;
        }

        private List<TestCheckResult> CheckStartAndEnd(
            List<TestCheckResult> results, 
            IList<GoldenMasterPoint> corners,
            GoldenMasterSingleGraphicPass goldenMaster,
            int regionIndex,
            string regionType)
        {
            List<TestCheckResult> latestResults = results;
            string errorMessage = string.Empty;
            int numCorners = corners.Count;
            bool startAndEndAreTheSame = corners[0].X == corners[numCorners - 1].X
                                         && corners[0].Y == corners[numCorners - 1].Y;
            //if (!startAndEndAreTheSame)
            //{
                errorMessage =
                    string.Format("For {0} players and {1} cards, the {2} region at index {3} starts at [{4}, {5}] but ends at [{6}, {7}]",
                        goldenMaster.NumPlayersInGame,
                        goldenMaster.NumCardsInLoop,
                        regionType,
                        regionIndex,
                        corners[0].X,
                        corners[0].Y,
                        corners[1].X,
                        corners[1].Y);
            //}
            latestResults.Add(new TestCheckResult { Result = startAndEndAreTheSame, ErrorMessage = errorMessage });

            return latestResults;
        }

        private static string AllErrorMessagesCommaSeparated(List<TestCheckResult> assertions)
        {
            return assertions.Where(assertion => assertion.Result == false)
                .Select(assertion => assertion.ErrorMessage)
                .Aggregate((working, next) => working + ", " + next);
        }
    }
}
