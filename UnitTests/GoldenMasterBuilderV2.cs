using System.Collections.Generic;
using Domain.Models;

namespace UnitTests
{
    public static class GoldenMasterBuilderV2
    {
        public static GoldenMasterList BuildSomeGoldenMastersV2()
        {
            GoldenMasterList goldenMasterList = new GoldenMasterList();

            goldenMasterList.GoldenMasters.Add(BuildGoldenMasterV2());
            goldenMasterList.GoldenMasters.Add(BuildGoldenMasterV2());

            return goldenMasterList;
        }

        private static GoldenMasterSinglePass BuildGoldenMasterV2()
        {
            return new GoldenMasterSinglePass
            {
                NumTotalSegments = 11,
                TopGameRegions = new List<TopGameRegion>
                {
                    new TopGameRegion
                    {
                        TopGamePoints = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = 11,
                                Y = 11
                            }
                        }
                    }
                },
                VitalStatistics = new VitalStatistics
                {
                    angleB = 11,
                    angleC = 11,
                    arcSegmentAngle = 11,
                    originToArcCentre = 11,
                    centralSpokeLength = 11,
                    innerArcRadius = 11,
                    outerArcRadius = 11,
                    outerArmLength = 11,
                    innerArmLength = 11,
                    constantSegmentLength = 11,
                    maxCentralAngle = 11,
                    arcStartAngle = 11,
                    constantBottomAngle = 11,
                    centralAngle = 11,
                    bMinimumAngleApplied = true,
                    bMaximumAngleApplied = false,
                    origin = new TopGamePoint
                    {
                        X = 11,
                        Y = 11
                    },
                    relativeInnerPetalSource = new TopGamePoint
                    {
                        X = 11,
                        Y = 11
                    },
                    relativeArcCentre = new TopGamePoint
                    {
                        X = 11,
                        Y = 11
                    },
                    relativeInnerArcStart = new TopGamePoint
                    {
                        X = 11,
                        Y = 11
                    },
                    relativeInnerArcEnd = new TopGamePoint
                    {
                        X = 11,
                        Y = 11
                    },
                    relativeOuterArcStart = new TopGamePoint
                    {
                        X = 11,
                        Y = 11
                    },
                    relativeOuterArcEnd = new TopGamePoint
                    {
                        X = 11,
                        Y = 11
                    },
                    actualArcCentre = new TopGamePoint
                    {
                        X = 11,
                        Y = 11
                    },
                    actualInnerArcEnd = new TopGamePoint
                    {
                        X = 11,
                        Y = 11
                    },
                    actualInnerArcStart = new TopGamePoint
                    {
                        X = 11,
                        Y = 11
                    },
                    actualInnerPetalSource = new TopGamePoint
                    {
                        X = 11,
                        Y = 11
                    },
                    actualOuterArcEnd = new TopGamePoint
                    {
                        X = 11,
                        Y = 11
                    },
                    actualOuterArcStart = new TopGamePoint
                    {
                        X = 11,
                        Y = 11
                    },
                    numArmSegments = 11,
                    numArcSegments = 11,
                    numTotalSegments = 11,
                    numTotalCardsInGame = 11,
                    numCardsInPlay = 11,
                    outerPath = new TopGameGraphicsPath
                    {
                        PointsOnLine = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = 11,
                                Y = 11
                            }
                        }
                    },
                    innerPath = new TopGameGraphicsPath
                    {
                        PointsOnLine = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = 11,
                                Y = 11
                            }
                        }
                    },
                    startArmDivisionStarts = new TopGamePointCollection
                    {
                        Points = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = 11,
                                Y = 11
                            }
                        }
                    },
                    startArmDivisionEnds = new TopGamePointCollection
                    {
                        Points = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = 11,
                                Y = 11
                            }
                        }
                    },
                    endArmDivisionStarts = new TopGamePointCollection
                    {
                        Points = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = 11,
                                Y = 11
                            }
                        }
                    },
                    endArmDivisionEnds = new TopGamePointCollection
                    {
                        Points = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = 11,
                                Y = 11
                            }
                        }
                    },
                    arcSpokes = new TopGamePointCollection
                    {
                        Points = new List<TopGamePoint>
                        {
                            new TopGamePoint
                            {
                                X = 11,
                                Y = 11
                            }
                        }
                    },
                    innerArcSquare = new TopGameRectangle
                    (
                        x: 11,
                        y: 11,
                        width: 11,
                        height: 11
                    ),
                    outerArcSquare = new TopGameRectangle
                    (
                        x: 11,
                        y: 11,
                        width: 11,
                        height: 11
                    )
                }
            };
        }
    }
}