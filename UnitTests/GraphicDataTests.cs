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
        public void All_adjacent_regions_should_share_two_points()
        {
            // Arrange & Act
            var goldenMasters = GoldenMasterPopulator.GenerateAllGraphicData().GoldenMasters;

            // Assert
            List<TestCheckResult> results = CheckRegionsForAdjacentSides(goldenMasters);
            bool allAssertionsAreTrue = results.All(assertion => assertion.Result == true);
            string errorMessage = results.Any(assertion => assertion.Result == false)
                ? AllErrorMessagesCommaSeparated(results)
                : "";

            Assert.IsTrue(allAssertionsAreTrue, errorMessage);
        }

        private List<TestCheckResult> CheckRegionsForAdjacentSides(List<GoldenMasterSingleGraphicPass> goldenMasters)
        {
            var results = new List<TestCheckResult>();

            foreach (var goldenMaster in goldenMasters)
            {
                var regions = goldenMaster.Regions;

                for (int index = 0; index < (regions.Count - 1); index++)
                {
                    results = LookForAdjacentSides(
                        results,
                        regions[index].Corners,
                        regions[index + 1].Corners,
                        goldenMaster, 
                        index);
                }
            }

            return results;
        }

        private List<TestCheckResult> LookForAdjacentSides(
            List<TestCheckResult> results,
            IList<GoldenMasterPoint> currentCorners,
            IList<GoldenMasterPoint> nextCorners,
            GoldenMasterSingleGraphicPass goldenMaster,
            int regionIndex)
        {
            List<TestCheckResult> latestResults = results;
            string errorMessage = string.Empty;
            var sharedPoints = currentCorners.Intersect(nextCorners);
            bool adjacentSides = sharedPoints.Count() == 2;

            if (!adjacentSides)
            {
                errorMessage =
                string.Format("{0} players {1} cards: Regions {2} and {3} not adjacent",
                    goldenMaster.NumPlayersInGame,
                    goldenMaster.NumCardsInLoop,
                    regionIndex,
                    regionIndex + 1);
            }
            latestResults.Add(new TestCheckResult { Result = adjacentSides, ErrorMessage = errorMessage });

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
