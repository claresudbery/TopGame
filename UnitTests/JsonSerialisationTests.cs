using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using TopGameWindowsApp;
using TopGameWindowsApp.Models;
using FluentAssertions;

namespace UnitTests
{
    [TestFixture]
    public class JsonSerialisationTests
    {
        [Test]
        public void Will_serialise_all_GoldenMasterSinglePass_data_and_write_to_file()
        {
            // Arrange
            List<GoldenMasterSinglePass> goldenMasters = BuildSomeGoldenMasters();
            var systemUnderTest = new OnePlayerGraphicsLoop();

            // Act
            systemUnderTest.WriteResultsToJsonFile(goldenMasters[0]);
            systemUnderTest.WriteResultsToJsonFile(goldenMasters[1]);

            // Assert
            List<GoldenMasterSinglePass> results;
            using (StreamReader file = File.OpenText(@"c:\Temp\TopGame-GoldenMaster.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                results = (List<GoldenMasterSinglePass>)serializer.Deserialize(file, typeof(List<GoldenMasterSinglePass>));
            }
            results.ShouldAllBeEquivalentTo(goldenMasters);
        }

        private List<GoldenMasterSinglePass> BuildSomeGoldenMasters()
        {
            int randomNumber = 1;

            List<GoldenMasterSinglePass> goldenMasters = new List<GoldenMasterSinglePass>();

            goldenMasters.Add(BuildGoldenMaster(ref randomNumber));
            goldenMasters.Add(BuildGoldenMaster(ref randomNumber));

            return goldenMasters;
        }

        private GoldenMasterSinglePass BuildGoldenMaster(ref int randomNumber)
        {
            return new GoldenMasterSinglePass
            {
                NumTotalSegments = randomNumber++,
                TopGameRegions = new List<TopGameRegion>
                {
                    new TopGameRegion
                    {
                        TopGamePoints = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    }
                },
                VitalStatistics = new VitalStatistics
                {
                    angleB = randomNumber++,
                    angleC = randomNumber++,
                    arcSegmentAngle = randomNumber++,
                    originToArcCentre = randomNumber++,
                    centralSpokeLength = randomNumber++,
                    innerArcRadius = randomNumber++,
                    outerArcRadius = randomNumber++,
                    outerArmLength = randomNumber++,
                    innerArmLength = randomNumber++,
                    constantSegmentLength = randomNumber++,
                    maxCentralAngle = randomNumber++,
                    arcStartAngle = randomNumber++,
                    constantBottomAngle = randomNumber++,
                    centralAngle = randomNumber++,
                    bMinimumAngleApplied = true,
                    bMaximumAngleApplied = false,
                    origin = new TopGamePoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    relativeInnerPetalSource = new TopGamePoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    relativeArcCentre = new TopGamePoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    relativeInnerArcStart = new TopGamePoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    relativeInnerArcEnd = new TopGamePoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    relativeOuterArcStart = new TopGamePoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    relativeOuterArcEnd = new TopGamePoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    actualArcCentre = new TopGamePoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    actualInnerArcEnd = new TopGamePoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    actualInnerArcStart = new TopGamePoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    actualInnerPetalSource = new TopGamePoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    actualOuterArcEnd = new TopGamePoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    actualOuterArcStart = new TopGamePoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    numArmSegments = randomNumber++,
                    numArcSegments = randomNumber++,
                    numTotalSegments = randomNumber++,
                    numTotalCardsInGame = randomNumber++,
                    numCardsInPlay = randomNumber++,
                    outerPath = new TopGameGraphicsPath
                    {
                        PointsOnLine = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    },
                    innerPath = new TopGameGraphicsPath
                    {
                        PointsOnLine = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    },
                    startArmDivisionStarts = new TopGamePointCollection
                    {
                        Points = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    },
                    startArmDivisionEnds = new TopGamePointCollection
                    {
                        Points = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    },
                    endArmDivisionStarts = new TopGamePointCollection
                    {
                        Points = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    },
                    endArmDivisionEnds = new TopGamePointCollection
                    {
                        Points = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    },
                    arcSpokes = new TopGamePointCollection
                    {
                        Points = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    },
                    innerArcSquare = new TopGameRectangle
                    {
                        X = randomNumber++,
                        Y = randomNumber++,
                        Width = randomNumber++,
                        Height = randomNumber++
                    },
                    outerArcSquare = new TopGameRectangle
                    {
                        X = randomNumber++,
                        Y = randomNumber++,
                        Width = randomNumber++,
                        Height = randomNumber
                    }
                }
            }; ;
        }
    }
}
