using System.Collections.Generic;
using Domain.GameModels.GoldenMaster;
using Domain.GraphicModels.GoldenMaster;

namespace UnitTests.Utils
{
    public static class GoldenMasterBuilder
    {
        public static GoldenMasterGraphicList BuildSomeGraphicGoldenMasters()
        {
            GoldenMasterGraphicList goldenMasterGraphicList = new GoldenMasterGraphicList();
            int randomNumber = 1;

            goldenMasterGraphicList.GoldenMasters.Add(BuildGraphicGoldenMaster(ref randomNumber));
            goldenMasterGraphicList.GoldenMasters.Add(BuildGraphicGoldenMaster(ref randomNumber));

            return goldenMasterGraphicList;
        }

        private static GoldenMasterSingleGraphicPass BuildGraphicGoldenMaster(ref int randomNumber)
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

        public static GoldenMasterGameDataList BuildSomeGameDataGoldenMasters()
        {
            return new GoldenMasterGameDataList
            {
                GoldenMasters = new List<GoldenMasterGameData>
                {
                    new GoldenMasterGameData
                    {
                        StartDeck = "StartDeck.01",
                        NumPlayers = 1,
                        PlayerStartHands = new List<string>
                        {
                            "Player01.01",
                            "Player02.01"
                        },
                        Turns = new List<GoldenMasterTurnInfo>
                        {
                            new GoldenMasterTurnInfo
                            {
                                CardsInPlay = "CardsInPlay.01.01",
                                NewPlayerHand = "NewPlayerHand.01.01",
                                PlayerIndex = 11
                            },
                            new GoldenMasterTurnInfo
                            {
                                CardsInPlay = "CardsInPlay.01.02",
                                NewPlayerHand = "NewPlayerHand.01.02",
                                PlayerIndex = 12
                            }
                        }
                    },
                    new GoldenMasterGameData
                    {
                        StartDeck = "StartDeck.02",
                        NumPlayers = 2,
                        PlayerStartHands = new List<string>
                        {
                            "Player01.02",
                            "Player02.02"
                        },
                        Turns = new List<GoldenMasterTurnInfo>
                        {
                            new GoldenMasterTurnInfo
                            {
                                CardsInPlay = "CardsInPlay.02.01",
                                NewPlayerHand = "NewPlayerHand.02.01",
                                PlayerIndex = 21
                            },
                            new GoldenMasterTurnInfo
                            {
                                CardsInPlay = "CardsInPlay.02.02",
                                NewPlayerHand = "NewPlayerHand.02.02",
                                PlayerIndex = 22
                            }
                        }
                    }
                }
            };
        }
    }
}