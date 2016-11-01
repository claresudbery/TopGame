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