using System.Collections.Generic;
using Domain.Models;
using Domain.Models.GoldenMaster;

namespace UnitTests
{
    public static class GoldenMasterBuilder
    {
        public static GoldenMasterList BuildSomeGoldenMasters()
        {
            GoldenMasterList goldenMasterList = new GoldenMasterList();
            int randomNumber = 1;

            goldenMasterList.GoldenMasters.Add(BuildGoldenMaster(ref randomNumber));
            goldenMasterList.GoldenMasters.Add(BuildGoldenMaster(ref randomNumber));

            return goldenMasterList;
        }

        private static GoldenMasterSinglePass BuildGoldenMaster(ref int randomNumber)
        {
            return new GoldenMasterSinglePass
            {
                NumCardsInLoop = randomNumber++,
                TopGameRegions = new List<GoldenMasterRegion>
                {
                    new GoldenMasterRegion
                    {
                        TopGamePoints = new List<GoldenMasterPoint>
                        {
                            new GoldenMasterPoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    }
                },
                VitalStatistics = new GoldenMasterVitalStatistics
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
                    origin = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    relativeInnerPetalSource = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    relativeArcCentre = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    relativeInnerArcStart = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    relativeInnerArcEnd = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    relativeOuterArcStart = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    relativeOuterArcEnd = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    actualArcCentre = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    actualInnerArcEnd = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    actualInnerArcStart = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    actualInnerPetalSource = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    actualOuterArcEnd = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    actualOuterArcStart = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    numArmSegments = randomNumber++,
                    numArcSegments = randomNumber++,
                    numTotalSegments = randomNumber++,
                    numTotalCardsInGame = randomNumber++,
                    numCardsInPlay = randomNumber++,
                    outerPath = new GoldenMasterGraphicsPath
                    {
                        PointsOnLine = new List<GoldenMasterPoint>
                        {
                            new GoldenMasterPoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    },
                    innerPath = new GoldenMasterGraphicsPath
                    {
                        PointsOnLine = new List<GoldenMasterPoint>
                        {
                            new GoldenMasterPoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    },
                    startArmDivisionStarts = new GoldenMasterPointCollection
                    {
                        Points = new List<GoldenMasterPoint>
                        {
                            new GoldenMasterPoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    },
                    startArmDivisionEnds = new GoldenMasterPointCollection
                    {
                        Points = new List<GoldenMasterPoint>
                        {
                            new GoldenMasterPoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    },
                    endArmDivisionStarts = new GoldenMasterPointCollection
                    {
                        Points = new List<GoldenMasterPoint>
                        {
                            new GoldenMasterPoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    },
                    endArmDivisionEnds = new GoldenMasterPointCollection
                    {
                        Points = new List<GoldenMasterPoint>
                        {
                            new GoldenMasterPoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    },
                    arcSpokes = new GoldenMasterPointCollection
                    {
                        Points = new List<GoldenMasterPoint>
                        {
                            new GoldenMasterPoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    },
                    innerArcSquare = new GoldenMasterRectangle
                    {
                        X = randomNumber++,
                        Y = randomNumber++,
                        Width = randomNumber++,
                        Height = randomNumber++
                    },
                    outerArcSquare = new GoldenMasterRectangle
                    {
                        X = randomNumber++,
                        Y = randomNumber++,
                        Width = randomNumber++,
                        Height = randomNumber
                    }
                }
            };
        }
    }
}