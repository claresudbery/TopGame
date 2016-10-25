using System.Collections.Generic;
using Domain.GraphicModels.GoldenMaster;

namespace UnitTests
{
    public static class GoldenMasterBuilderV3
    {
        public static GoldenMasterGraphicList BuildSomeGraphicGoldenMastersV3()
        {
            GoldenMasterGraphicList goldenMasterGraphicList = new GoldenMasterGraphicList();
            int randomNumber = 1;

            goldenMasterGraphicList.GoldenMasters.Add(BuildGraphicGoldenMasterV3(ref randomNumber));
            goldenMasterGraphicList.GoldenMasters.Add(BuildGraphicGoldenMasterV3(ref randomNumber));

            return goldenMasterGraphicList;
        }

        private static GoldenMasterSingleGraphicPass BuildGraphicGoldenMasterV3(ref int randomNumber)
        {
            return new GoldenMasterSingleGraphicPass
            {
                NumCardsInLoop = randomNumber++,
                MiniPetalRegions = new List<GoldenMasterGraphicsPath>
                {
                    new GoldenMasterGraphicsPath
                    {
                        Lines = new List<GoldenMasterLine>
                        {
                            new GoldenMasterLine
                            {
                                Start = new GoldenMasterPoint
                                {
                                    X = randomNumber++,
                                    Y = randomNumber++
                                },
                                End = new GoldenMasterPoint
                                {
                                    X = randomNumber++,
                                    Y = randomNumber++
                                }
                            }
                        },
                        ArcPaths = new List<GoldenMasterArcPath>
                        {
                            new GoldenMasterArcPath
                            {
                                Rectangle = new GoldenMasterRectangle
                                {
                                    X = randomNumber++,
                                    Y = randomNumber++,
                                    Width = randomNumber++,
                                    Height = randomNumber++
                                },
                                StartAngle = randomNumber++,
                                SweepAngle = randomNumber++
                            }
                        }
                    }
                },
                ArcRegions = new List<GoldenMasterArcRegion>
                {
                    new GoldenMasterArcRegion
                    {
                        Corners = new List<GoldenMasterPoint>
                        {
                            new GoldenMasterPoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    }
                },
                StraightEdgedRegions = new List<GoldenMasterStraightEdgedRegion>
                {
                    new GoldenMasterStraightEdgedRegion
                    {
                        Corners = new List<GoldenMasterPoint>
                        {
                            new GoldenMasterPoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        }
                    }
                },
                VitalGraphicStatistics = new GoldenMasterVitalGraphicStatistics
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
                        Lines = new List<GoldenMasterLine>
                        {
                            new GoldenMasterLine
                            {
                                Start = new GoldenMasterPoint
                                {
                                    X = randomNumber++,
                                    Y = randomNumber++
                                },
                                End = new GoldenMasterPoint
                                {
                                    X = randomNumber++,
                                    Y = randomNumber++
                                }
                            }
                        },
                        ArcPaths = new List<GoldenMasterArcPath>
                        {
                            new GoldenMasterArcPath
                            {
                                Rectangle = new GoldenMasterRectangle
                                {
                                    X = randomNumber++,
                                    Y = randomNumber++,
                                    Width = randomNumber++,
                                    Height = randomNumber++
                                },
                                StartAngle = randomNumber++,
                                SweepAngle = randomNumber++
                            }
                        }
                    },
                    innerPath = new GoldenMasterGraphicsPath
                    {
                        Lines = new List<GoldenMasterLine>
                        {
                            new GoldenMasterLine
                            {
                                Start = new GoldenMasterPoint
                                {
                                    X = randomNumber++,
                                    Y = randomNumber++
                                },
                                End = new GoldenMasterPoint
                                {
                                    X = randomNumber++,
                                    Y = randomNumber++
                                }
                            }
                        },
                        ArcPaths = new List<GoldenMasterArcPath>
                        {
                            new GoldenMasterArcPath
                            {
                                Rectangle = new GoldenMasterRectangle
                                {
                                    X = randomNumber++,
                                    Y = randomNumber++,
                                    Width = randomNumber++,
                                    Height = randomNumber++
                                },
                                StartAngle = randomNumber++,
                                SweepAngle = randomNumber++
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