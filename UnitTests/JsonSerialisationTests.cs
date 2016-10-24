using System.Collections.Generic;
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
        public void Will_serialise_all_GoldenMasterSinglePass_data_and_write_to_file()
        {
            // Arrange
            GoldenMasterList goldenMasterList = BuildSomeGoldenMasters();

            // Act
            TopGameJsonWriter.WriteToJsonFile(goldenMasterList, JsonFileNameAndPath);

            // Assert
            GoldenMasterList result = TopGameJsonWriter.ReadFromJsonFile<GoldenMasterList>(JsonFileNameAndPath);
            result.GoldenMasters.ShouldAllBeEquivalentTo(goldenMasterList.GoldenMasters);
        }

        private GoldenMasterList BuildSomeGoldenMasters()
        {
            GoldenMasterList goldenMasterList = new GoldenMasterList();
            int randomNumber = 1;
            
            goldenMasterList.GoldenMasters.Add(BuildGoldenMaster(ref randomNumber));
            goldenMasterList.GoldenMasters.Add(BuildGoldenMaster(ref randomNumber));

            return goldenMasterList;
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
