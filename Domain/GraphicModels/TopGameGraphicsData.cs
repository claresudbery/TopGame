using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Domain.Extensions;
using Domain.GraphicModels.GoldenMaster;
using TopGameWindowsApp;

namespace Domain.GraphicModels
{
    public class TopGameGraphicsData
    {
        private readonly OnePlayerGraphicsLoop _onePlayerGraphicsLoop;

        public TopGameGraphicsData(OnePlayerGraphicsLoop onePlayerGraphicsLoop)
        {
            _onePlayerGraphicsLoop = onePlayerGraphicsLoop;

            TopGameArc = new TopGameArc(onePlayerGraphicsLoop);
            GeneralLoopData = new TopGameLoop();

            StartArmDivisionStarts = new TopGamePointCollection();
            StartArmDivisionEnds = new TopGamePointCollection();
            EndArmDivisionStarts = new TopGamePointCollection();
            EndArmDivisionEnds = new TopGamePointCollection();

            InitialiseValues();
        }

        private void InitialiseValues()
        {
            InitialiseValuesFromConstants();

            CalculateCentralAngle(TopGameConstants.AnglesInCircle, NumTotalCardsInGame, false);
        }

        public double CalculateCentralAngle(double numDegreesAvailable, int numCardsBeingShared, bool bSuppressMinAndMax)
        {
            return GeneralLoopData.CalculateCentralAngle(numDegreesAvailable, numCardsBeingShared, bSuppressMinAndMax);
        }

        private void InitialiseValuesFromConstants()
        {
            NumTotalCardsInGame = TopGameConstants.NumCardsInStandardPack;
            // Defunct:
            ConstantCentralSegmentLength = TopGameConstants.ConstantSegmentLength;
        }


        // ***********************************************************
        // Composite sub-objects
        // ***********************************************************

        public TopGameArc TopGameArc { get; set; }

        public TopGameLoop GeneralLoopData { get; set; }


        // ***********************************************************
        // Lengths
        // ***********************************************************

        public double ConstantCentralSegmentLength { get; set; }


        // ***********************************************************
        // Counts
        // ***********************************************************

        public int NumTotalCardsInGame { get; set; }

        public int NumCardsInPlay { get; set; }


        // ***********************************************************
        // Divisions
        // ***********************************************************
        
        public TopGamePointCollection StartArmDivisionStarts { get; set; }
        
        public TopGamePointCollection StartArmDivisionEnds { get; set; }

        public TopGamePointCollection EndArmDivisionStarts { get; set; }
        
        public TopGamePointCollection EndArmDivisionEnds { get; set; }


        // ***********************************************************
        // Obsolete
        // ***********************************************************

        public double AngleB { get; set; }

        public double AngleC { get; set; }


        public void Copy(TopGameGraphicsData topGameGraphicsDataSource)
        {
            TopGameArc.Copy(topGameGraphicsDataSource.TopGameArc);
            GeneralLoopData.Copy(topGameGraphicsDataSource.GeneralLoopData);
            
            StartArmDivisionStarts.Copy(topGameGraphicsDataSource.StartArmDivisionStarts);
            StartArmDivisionEnds.Copy(topGameGraphicsDataSource.StartArmDivisionEnds);
            EndArmDivisionStarts.Copy(topGameGraphicsDataSource.EndArmDivisionStarts);
            EndArmDivisionEnds.Copy(topGameGraphicsDataSource.EndArmDivisionEnds);

            ConstantCentralSegmentLength = topGameGraphicsDataSource.ConstantCentralSegmentLength;
            AngleB = topGameGraphicsDataSource.AngleB;
            AngleC = topGameGraphicsDataSource.AngleC;
            NumTotalCardsInGame = topGameGraphicsDataSource.NumTotalCardsInGame;
            NumCardsInPlay = topGameGraphicsDataSource.NumCardsInPlay;
        }

        public void Dispose()
        {
            TopGameArc.Dispose();
        }

        // See TopGame\Docs\Arc-and-angles.jpg and TopGame\Docs\GraphicsPath-Arc.png for explanatory diagrams.
        public void PrepareActualData(
            double rotationAngle,
            GoldenMasterSingleGraphicPass goldenMasterData)
        {
            CalculateObsoleteAngleAAndAngleB();

            GeneralLoopData.CalculateGeneralValues();
            TopGameArc.CalculateArcCoordinates(GeneralLoopData);
            CalculateArmDivisions();
            AddSubRegions(goldenMasterData);
        }

        private void CalculateObsoleteAngleAAndAngleB()
        {
            // AngleB and AngleC are now not used - only here so as not to break golden master
            AngleB = 90 - GeneralLoopData.TotalAngleShare / 2; // ConstantBottomAngle = angleShare, ie 360 / num hands
            AngleC = 90 - GeneralLoopData.CentralAngle / 2; // was previously expressed as (180 - _vitalStatistics.CentralAngle) / 2
        }

        // The sub-regions are all the areas that get coloured in: Each region represents an individual card.
        // They are displayed in three sections, all of which added together look a bit like a petal:
        // 1) The straight "start-arm", basically made out of two parallel lines (but tapering to a triangular point at the centre)
        // 2) The curved "arc" which joins the two arms together
        // 3) The straight "end-arm", basically made out of two parallel lines (but tapering to a triangular point at the centre)
        private void AddSubRegions(GoldenMasterSingleGraphicPass goldenMasterData)
        {
            _onePlayerGraphicsLoop.DisposeRegions();

            AddStartArmCentralRegion(goldenMasterData);
            AddStartArmSegments(goldenMasterData);
            TopGameArc.AddArcSegments(goldenMasterData, GeneralLoopData);
            AddEndArmSegments(goldenMasterData);
            AddEndArmCentralRegion(goldenMasterData);
        }

        private void AddEndArmCentralRegion(GoldenMasterSingleGraphicPass goldenMasterData)
        {
            // End with the central region of the end-arm - this is the triangular bit that goes from the centre out to the end of the parallel-lines part of the arm
            // (apart from the central triangle, all the regions in the end-arm are parallelograms)
            if (GeneralLoopData.NumTotalSegments > 1)
            {
                // end-arm central region
                _onePlayerGraphicsLoop.AddTriangularRegion(
                    GeneralLoopData.Origin,
                    EndArmDivisionStarts.Points.ElementAt(0),
                    GeneralLoopData.ActualInnerPetalSource,
                            goldenMasterData
                    );
            }
        }

        private void AddEndArmSegments(GoldenMasterSingleGraphicPass goldenMasterData)
        {
            if (GeneralLoopData.NumArmSegments > 0)
            {
                // all the divisions of the end arm (in reverse)
                _onePlayerGraphicsLoop.AddParallelogramRegion(
                    EndArmDivisionStarts.Points.ElementAt(GeneralLoopData.NumArmSegments - 1),
                    EndArmDivisionEnds.Points.ElementAt(GeneralLoopData.NumArmSegments - 1),
                    TopGameArc.ActualInnerArcEnd,
                    TopGameArc.ActualOuterArcEnd,
                            goldenMasterData
                    );

                if (GeneralLoopData.NumArmSegments > 1)
                {
                    for (int iCount = GeneralLoopData.NumArmSegments - 1; iCount > 0; iCount--)
                    {
                        _onePlayerGraphicsLoop.AddParallelogramRegion(
                            EndArmDivisionStarts.Points.ElementAt(iCount),
                            EndArmDivisionEnds.Points.ElementAt(iCount),
                            EndArmDivisionEnds.Points.ElementAt(iCount - 1),
                            EndArmDivisionStarts.Points.ElementAt(iCount - 1),
                            goldenMasterData
                            );
                    }
                }
            }
        }

        private void AddStartArmSegments(GoldenMasterSingleGraphicPass goldenMasterData)
        {
            if (GeneralLoopData.NumArmSegments > 0)
            {
                // all the divisions of the start arm
                if (GeneralLoopData.NumArmSegments > 1)
                {
                    for (int iCount = 0; iCount < GeneralLoopData.NumArmSegments - 1; iCount++)
                    {
                        _onePlayerGraphicsLoop.AddParallelogramRegion(
                            StartArmDivisionStarts.Points.ElementAt(iCount),
                            StartArmDivisionEnds.Points.ElementAt(iCount),
                            StartArmDivisionEnds.Points.ElementAt(iCount + 1),
                            StartArmDivisionStarts.Points.ElementAt(iCount + 1),
                            goldenMasterData
                            );
                    }
                }

                // the last one hooks up to the arc.
                _onePlayerGraphicsLoop.AddParallelogramRegion(
                    StartArmDivisionStarts.Points.ElementAt(GeneralLoopData.NumArmSegments - 1),
                    StartArmDivisionEnds.Points.ElementAt(GeneralLoopData.NumArmSegments - 1),
                    TopGameArc.ActualInnerArcStart,
                    TopGameArc.ActualOuterArcStart,
                    goldenMasterData
                    );
            }
        }

        private void AddStartArmCentralRegion(GoldenMasterSingleGraphicPass goldenMasterData)
        {
            // Start with the central region of the start-arm - this is the triangular bit that goes from the centre out to the start of the parallel-lines part of the arm
            // (after the central triangle, all the regions in the start-arm are parallelograms)
            if (GeneralLoopData.NumTotalSegments > 1)
            {
                // start-arm central region
                _onePlayerGraphicsLoop.AddTriangularRegion(
                    GeneralLoopData.Origin,
                    StartArmDivisionStarts.Points.ElementAt(0),
                    GeneralLoopData.ActualInnerPetalSource,
                            goldenMasterData);
            }
            else
            {
                // just one central region
                _onePlayerGraphicsLoop.AddTriangularRegion(
                    GeneralLoopData.Origin,
                    StartArmDivisionStarts.Points.ElementAt(0),
                    EndArmDivisionStarts.Points.ElementAt(0),
                            goldenMasterData);
            }
        }

        private void CalculateArmDivisions()
        {
            // Create NumArmSegments + 1 divisions of the start arm and end arm (including the centre).
            ClearAllArmDivisions();

            // Always create the first division, even if the arms have no segments.
            double fractionalMultiplier = 1.0 / ((double)GeneralLoopData.NumArmSegments + 1.0);

            StartArmDivisionStarts.Points.Add(LineCalculator.MoveAlongLineByFraction(GeneralLoopData.Origin, TopGameArc.ActualOuterArcStart, fractionalMultiplier));
            EndArmDivisionStarts.Points.Add(LineCalculator.MoveAlongLineByFraction(GeneralLoopData.Origin, TopGameArc.ActualOuterArcEnd, fractionalMultiplier));

            StartArmDivisionEnds.Points.Add(GeneralLoopData.ActualInnerPetalSource);
            EndArmDivisionEnds.Points.Add(GeneralLoopData.ActualInnerPetalSource);

            // now create the rest, if necessary
            if (GeneralLoopData.NumArmSegments > 1) // If there's only one, there's no dividers necessary
            {
                for (int iCount = 2; iCount <= GeneralLoopData.NumArmSegments; iCount++)
                {
                    double outerFractionalMultiplier = (double)(iCount) / ((double)GeneralLoopData.NumArmSegments + 1.0); // eg 4 segments: 2/5, 3/5, 4/5
                    StartArmDivisionStarts.Points.Add(LineCalculator.MoveAlongLineByFraction(GeneralLoopData.Origin, TopGameArc.ActualOuterArcStart, outerFractionalMultiplier));
                    EndArmDivisionStarts.Points.Add(LineCalculator.MoveAlongLineByFraction(GeneralLoopData.Origin, TopGameArc.ActualOuterArcEnd, outerFractionalMultiplier));

                    double innerFractionalMultiplier = ((double)(iCount) - 1.0) / (double)(GeneralLoopData.NumArmSegments); // eg 4 segments: 1/4, 2/4, 3/4
                    StartArmDivisionEnds.Points.Add(LineCalculator.MoveAlongLineByFraction(GeneralLoopData.ActualInnerPetalSource, TopGameArc.ActualInnerArcStart, innerFractionalMultiplier));
                    EndArmDivisionEnds.Points.Add(LineCalculator.MoveAlongLineByFraction(GeneralLoopData.ActualInnerPetalSource, TopGameArc.ActualInnerArcEnd, innerFractionalMultiplier));
                }
            }
        }

        private void ClearAllArmDivisions()
        {
            if (StartArmDivisionStarts.Points.Any())
            {
                StartArmDivisionStarts.Points.Clear();
            }
            if (StartArmDivisionEnds.Points.Any())
            {
                StartArmDivisionEnds.Points.Clear();
            }
            if (EndArmDivisionStarts.Points.Any())
            {
                EndArmDivisionStarts.Points.Clear();
            }
            if (EndArmDivisionEnds.Points.Any())
            {
                EndArmDivisionEnds.Points.Clear();
            }
        }

        public void Clear()
        {
            TopGameArc.Clear();
            GeneralLoopData.Clear();
            ClearAllArmDivisions();
        }

        /// <summary>
        /// Sort out what proportion of the circle we are getting
        /// </summary>
        /// <param name="maxCentralAngle"></param>
        /// <param name="angleShare"></param>
        public void SetAngles(double maxCentralAngle, double angleShare)
        {
            GeneralLoopData.SetAngles(maxCentralAngle, angleShare);
        }
    }
}