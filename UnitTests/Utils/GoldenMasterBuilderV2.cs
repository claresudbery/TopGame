using System.Collections.Generic;
using Domain.GraphicModels.GoldenMaster;

namespace UnitTests.Utils
{
    public static class GoldenMasterBuilderV2
    {
        public static GoldenMasterGraphicList BuildSomeGraphicGoldenMastersV2()
        {
            GoldenMasterGraphicList goldenMasterGraphicList = new GoldenMasterGraphicList();
            int randomNumber = 1;

            goldenMasterGraphicList.GoldenMasters.Add(BuildGraphicGoldenMasterV2(ref randomNumber));
            goldenMasterGraphicList.GoldenMasters.Add(BuildGraphicGoldenMasterV2(ref randomNumber));

            return goldenMasterGraphicList;
        }

        private static GoldenMasterSingleGraphicPass BuildGraphicGoldenMasterV2(ref int randomNumber)
        {
            return new GoldenMasterSingleGraphicPass
            {
                NumCardsInLoop = randomNumber++,
                Regions = new List<GoldenMasterRegion>
                {
                    new GoldenMasterMiniPetalRegion
                    {
                        Corners = new List<GoldenMasterPoint>
                        {
                            new GoldenMasterPoint
                            {
                                X = randomNumber++,
                                Y = randomNumber++
                            }
                        },
                        GraphicsPath = new GoldenMasterGraphicsPath
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
                    },
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
                    AngleB = randomNumber++,
                    AngleC = randomNumber++,
                    ArcSegmentAngle = randomNumber++,
                    OriginToArcCentre = randomNumber++,
                    CentralSpokeLength = randomNumber++,
                    InnerArcRadius = randomNumber++,
                    OuterArcRadius = randomNumber++,
                    OuterArmLength = randomNumber++,
                    InnerArmLength = randomNumber++,
                    ConstantSegmentLength = randomNumber++,
                    MaxCentralAngle = randomNumber++,
                    ArcStartAngle = randomNumber++,
                    ConstantBottomAngle = randomNumber++,
                    CentralAngle = randomNumber++,
                    MinimumAngleApplied = true,
                    MaximumAngleApplied = false,
                    Origin = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    RelativeInnerPetalSource = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    RelativeArcCentre = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    RelativeInnerArcStart = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    RelativeInnerArcEnd = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    RelativeOuterArcStart = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    RelativeOuterArcEnd = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    ActualArcCentre = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    ActualInnerArcEnd = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    ActualInnerArcStart = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    ActualInnerPetalSource = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    ActualOuterArcEnd = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    ActualOuterArcStart = new GoldenMasterPoint
                    {
                        X = randomNumber++,
                        Y = randomNumber++
                    },
                    NumArmSegments = randomNumber++,
                    NumArcSegments = randomNumber++,
                    NumTotalSegments = randomNumber++,
                    NumTotalCardsInGame = randomNumber++,
                    NumCardsInPlay = randomNumber++,
                    OuterPath = new GoldenMasterGraphicsPath
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
                    InnerPath = new GoldenMasterGraphicsPath
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
                    StartArmDivisionStarts = new GoldenMasterPointCollection
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
                    StartArmDivisionEnds = new GoldenMasterPointCollection
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
                    EndArmDivisionStarts = new GoldenMasterPointCollection
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
                    EndArmDivisionEnds = new GoldenMasterPointCollection
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
                    ArcSpokes = new GoldenMasterPointCollection
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
                    InnerArcSquare = new GoldenMasterRectangle
                    {
                        X = randomNumber++,
                        Y = randomNumber++,
                        Width = randomNumber++,
                        Height = randomNumber++
                    },
                    OuterArcSquare = new GoldenMasterRectangle
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