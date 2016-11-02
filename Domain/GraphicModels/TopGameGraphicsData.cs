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

            Origin = new TopGamePoint();

            RelativeInnerPetalSource = new TopGamePoint();
            RelativeArcCentre = new TopGamePoint();

            RelativeInnerArcStart = new TopGamePoint();
            RelativeInnerArcEnd = new TopGamePoint();

            RelativeOuterArcStart = new TopGamePoint();
            RelativeOuterArcEnd = new TopGamePoint();

            ActualInnerPetalSource = new TopGamePoint();
            ActualInnerArcStart = new TopGamePoint();
            ActualInnerArcEnd = new TopGamePoint();

            ActualArcCentre = new TopGamePoint();
            ActualOuterArcStart = new TopGamePoint();
            ActualOuterArcEnd = new TopGamePoint();

            OuterPath = new TopGameGraphicsPath();
            InnerPath = new TopGameGraphicsPath();

            StartArmDivisionStarts = new TopGamePointCollection();
            StartArmDivisionEnds = new TopGamePointCollection();
            EndArmDivisionStarts = new TopGamePointCollection();
            EndArmDivisionEnds = new TopGamePointCollection();
            ArcSpokes = new TopGamePointCollection();

            InnerArcSquare = new TopGameRectangle();
            OuterArcSquare = new TopGameRectangle();

            InitialiseValues();
        }

        private void InitialiseValues()
        {
            InitialiseValuesFromConstants();

            MinimumAngleApplied = false;
            MaximumAngleApplied = false;

            CalculateCentralAngle(TopGameConstants.AnglesInCircle, NumTotalCardsInGame, false);
        }

        public double CalculateCentralAngle(double numDegreesAvailable, int numCardsBeingShared, bool bSuppressMinAndMax)
        {
            MinimumAngleApplied = false;
            MaximumAngleApplied = false;
            CentralAngle = ((double)NumTotalSegments / (double)numCardsBeingShared) * numDegreesAvailable;
            if ((CentralAngle > 0) && !bSuppressMinAndMax)
            {
                if (CentralAngle < TopGameConstants.MinCentralAngle)
                {
                    CentralAngle = TopGameConstants.MinCentralAngle;
                    MinimumAngleApplied = true;
                }
                if (CentralAngle > MaxCentralAngle)
                {
                    CentralAngle = MaxCentralAngle;
                    MaximumAngleApplied = true;
                }
            }

            return CentralAngle;
        }

        private void InitialiseValuesFromConstants()
        {
            Origin.X = TopGameConstants.OriginX;
            Origin.Y = TopGameConstants.OriginY;
            ArmSegmentLength = TopGameConstants.ConstantSegmentLength;
            NumTotalCardsInGame = TopGameConstants.NumCardsInStandardPack;
            // Defunct:
            ConstantCentralSegmentLength = TopGameConstants.ConstantSegmentLength;
        }


        // ***********************************************************
        // Flags
        // ***********************************************************
        
        public bool MinimumAngleApplied { get; set; }

        public bool MaximumAngleApplied { get; set; }


        // ***********************************************************
        // Lengths
        // ***********************************************************

        public double OriginToArcCentre { get; set; }

        public double CentralSpokeLength { get; set; }

        public double InnerArcRadius { get; set; }

        public double OuterArcRadius { get; set; }

        public double OuterArmLength { get; set; }

        public double InnerArmLength { get; set; }

        public double ArmSegmentLength { get; set; }

        public double ConstantCentralSegmentLength { get; set; }


        // ***********************************************************
        // Angles
        // ***********************************************************

        public double MaxCentralAngle { get; set; }

        public double ArcStartAngle { get; set; }

        public double TotalAngleShare { get; set; }

        public double CentralAngle { get; set; }

        public double ArcSegmentAngle { get; set; }


        // ***********************************************************
        // Points
        // ***********************************************************

        public TopGamePoint Origin { get; set; }

        public TopGamePoint RelativeInnerPetalSource { get; set; }

        public TopGamePoint RelativeArcCentre { get; set; }

        public TopGamePoint RelativeInnerArcStart { get; set; }

        public TopGamePoint RelativeInnerArcEnd { get; set; }

        public TopGamePoint RelativeOuterArcStart { get; set; }

        public TopGamePoint RelativeOuterArcEnd { get; set; }

        public TopGamePoint ActualInnerPetalSource { get; set; }

        public TopGamePoint ActualInnerArcStart { get; set; }

        public TopGamePoint ActualInnerArcEnd { get; set; }

        public TopGamePoint ActualArcCentre { get; set; }

        public TopGamePoint ActualOuterArcStart { get; set; }

        public TopGamePoint ActualOuterArcEnd { get; set; }


        // ***********************************************************
        // Counts
        // ***********************************************************

        public int NumArmSegments { get; set; }

        public int NumArcSegments { get; set; }

        public int NumTotalSegments { get; set; }

        public int NumTotalCardsInGame { get; set; }

        public int NumCardsInPlay { get; set; }


        // ***********************************************************
        // Paths
        // ***********************************************************
        
        public TopGameGraphicsPath OuterPath { get; set; }
        
        public TopGameGraphicsPath InnerPath { get; set; }


        // ***********************************************************
        // Divisions
        // ***********************************************************
        
        public TopGamePointCollection StartArmDivisionStarts { get; set; }
        
        public TopGamePointCollection StartArmDivisionEnds { get; set; }

        public TopGamePointCollection EndArmDivisionStarts { get; set; }
        
        public TopGamePointCollection EndArmDivisionEnds { get; set; }
        
        public TopGamePointCollection ArcSpokes { get; set; }


        // ***********************************************************
        // Squares
        // ***********************************************************
        
        public TopGameRectangle InnerArcSquare { get; set; }

        public TopGameRectangle OuterArcSquare { get; set; }


        // ***********************************************************
        // Obsolete
        // ***********************************************************

        public double AngleB { get; set; }

        public double AngleC { get; set; }


        public void Copy(TopGameGraphicsData topGameGraphicsDataSource)
        {
            Origin = topGameGraphicsDataSource.Origin;

            RelativeInnerPetalSource = topGameGraphicsDataSource.RelativeInnerPetalSource;
            RelativeArcCentre = topGameGraphicsDataSource.RelativeArcCentre;

            RelativeInnerArcStart = topGameGraphicsDataSource.RelativeInnerArcStart;
            RelativeInnerArcEnd = topGameGraphicsDataSource.RelativeInnerArcEnd;

            RelativeOuterArcStart = topGameGraphicsDataSource.RelativeOuterArcStart;
            RelativeOuterArcEnd = topGameGraphicsDataSource.RelativeOuterArcEnd;

            ActualInnerPetalSource = topGameGraphicsDataSource.ActualInnerPetalSource;
            ActualInnerArcStart = topGameGraphicsDataSource.ActualInnerArcStart;
            ActualInnerArcEnd = topGameGraphicsDataSource.ActualInnerArcEnd;

            ActualArcCentre = topGameGraphicsDataSource.ActualArcCentre;
            ActualOuterArcStart = topGameGraphicsDataSource.ActualOuterArcStart;
            ActualOuterArcEnd = topGameGraphicsDataSource.ActualOuterArcEnd;

            OuterPath.Copy(topGameGraphicsDataSource.OuterPath);
            InnerPath.Copy(topGameGraphicsDataSource.InnerPath);

            StartArmDivisionStarts.Copy(topGameGraphicsDataSource.StartArmDivisionStarts);
            StartArmDivisionEnds.Copy(topGameGraphicsDataSource.StartArmDivisionEnds);
            EndArmDivisionStarts.Copy(topGameGraphicsDataSource.EndArmDivisionStarts);
            EndArmDivisionEnds.Copy(topGameGraphicsDataSource.EndArmDivisionEnds);
            ArcSpokes.Copy(topGameGraphicsDataSource.ArcSpokes);

            InnerArcSquare.Copy(topGameGraphicsDataSource.InnerArcSquare);
            OuterArcSquare.Copy(topGameGraphicsDataSource.OuterArcSquare);

            MaxCentralAngle = topGameGraphicsDataSource.MaxCentralAngle;
            MinimumAngleApplied = topGameGraphicsDataSource.MinimumAngleApplied;
            MaximumAngleApplied = topGameGraphicsDataSource.MaximumAngleApplied;
            OriginToArcCentre = topGameGraphicsDataSource.OriginToArcCentre;
            CentralSpokeLength = topGameGraphicsDataSource.CentralSpokeLength;
            InnerArcRadius = topGameGraphicsDataSource.InnerArcRadius;
            OuterArcRadius = topGameGraphicsDataSource.OuterArcRadius;
            OuterArmLength = topGameGraphicsDataSource.OuterArmLength;
            InnerArmLength = topGameGraphicsDataSource.InnerArmLength;
            ArmSegmentLength = topGameGraphicsDataSource.ArmSegmentLength;
            ConstantCentralSegmentLength = topGameGraphicsDataSource.ConstantCentralSegmentLength;
            ArcStartAngle = topGameGraphicsDataSource.ArcStartAngle;
            AngleB = topGameGraphicsDataSource.AngleB;
            AngleC = topGameGraphicsDataSource.AngleC;
            TotalAngleShare = topGameGraphicsDataSource.TotalAngleShare;
            CentralAngle = topGameGraphicsDataSource.CentralAngle;
            ArcSegmentAngle = topGameGraphicsDataSource.ArcSegmentAngle;
            NumArmSegments = topGameGraphicsDataSource.NumArmSegments;
            NumArcSegments = topGameGraphicsDataSource.NumArcSegments;
            NumTotalSegments = topGameGraphicsDataSource.NumTotalSegments;
            NumTotalCardsInGame = topGameGraphicsDataSource.NumTotalCardsInGame;
            NumCardsInPlay = topGameGraphicsDataSource.NumCardsInPlay;
        }

        public void Dispose()
        {
            OuterPath.ActualPath.Dispose();
            InnerPath.ActualPath.Dispose();
        }

        // See TopGame\Docs\Arc-and-angles.jpg and TopGame\Docs\GraphicsPath-Arc.png for explanatory diagrams.
        public void PrepareActualData(
            double rotationAngle,
            GoldenMasterSingleGraphicPass goldenMasterData)
        {
            CalculateObsoleteAngleAAndAngleB();

            CalculateGlobalValues();
            CalculateArcCoordinates();
            CalculateArmDivisions();
            AddSubRegions(goldenMasterData);
        }

        private void CalculateGlobalValues()
        {
            // Arms
            NumArmSegments = ArcWillExist() ? NumSegmentsContainedInArmsAndArc().DividedBy3() : 0;
            CalculateArmSegmentLength();
            OuterArmLength = (NumArmSegments + 1) * ArmSegmentLength;
            InnerArmLength = NumArmSegments * ArmSegmentLength;

            // Central triangle(s)
            CentralSpokeLength = GetAdjacentSide(ArmSegmentLength, CentralAngle / 2);

            // Inner petal source
            ActualInnerPetalSource.PopulateFromLengthTopAngleAndStartPoint(
                CentralSpokeLength,
                TotalAngleShare / 2 + CentralAngle / 2,
                Origin,
                RelativeInnerPetalSource);
        }

        private void CalculateObsoleteAngleAAndAngleB()
        {
            // AngleB and AngleC are now not used - only here so as not to break golden master
            AngleB = 90 - TotalAngleShare / 2; // ConstantBottomAngle = angleShare, ie 360 / num hands
            AngleC = 90 - CentralAngle / 2; // was previously expressed as (180 - _vitalStatistics.CentralAngle) / 2
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
            AddArcSegments(goldenMasterData);
            AddEndArmSegments(goldenMasterData);
            AddEndArmCentralRegion(goldenMasterData);
        }

        private void AddEndArmCentralRegion(GoldenMasterSingleGraphicPass goldenMasterData)
        {
            // End with the central region of the end-arm - this is the triangular bit that goes from the centre out to the end of the parallel-lines part of the arm
            // (apart from the central triangle, all the regions in the end-arm are parallelograms)
            if (NumTotalSegments > 1)
            {
                // end-arm central region
                _onePlayerGraphicsLoop.AddTriangularRegion(
                    Origin,
                    EndArmDivisionStarts.Points.ElementAt(0),
                    ActualInnerPetalSource,
                            goldenMasterData
                    );
            }
        }

        private void AddEndArmSegments(GoldenMasterSingleGraphicPass goldenMasterData)
        {
            if (NumArmSegments > 0)
            {
                // all the divisions of the end arm (in reverse)
                _onePlayerGraphicsLoop.AddParallelogramRegion(
                    EndArmDivisionStarts.Points.ElementAt(NumArmSegments - 1),
                    EndArmDivisionEnds.Points.ElementAt(NumArmSegments - 1),
                    ActualInnerArcEnd,
                    ActualOuterArcEnd,
                            goldenMasterData
                    );

                if (NumArmSegments > 1)
                {
                    for (int iCount = NumArmSegments - 1; iCount > 0; iCount--)
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

        private void AddArcSegments(GoldenMasterSingleGraphicPass goldenMasterData)
        {
            Region petalRegion = MakePetalRegion();

            // the divisions of the arc (sounds biblical!)
            if (NumArcSegments > 0)
            {
                if (ArcSpokes.Points.Count() == 0)
                {
                    // the division is the arc itself.
                    TopGameGraphicsPath tempRegionPath = new TopGameGraphicsPath();
                    // See AddArcPath for explanation of how arcs are drawn.
                    tempRegionPath.AddArcPath(OuterArcSquare, (float)ArcStartAngle, (float)180);
                    tempRegionPath.AddLine(ActualOuterArcStart, ActualOuterArcEnd);
                    _onePlayerGraphicsLoop.AddSubRegion(tempRegionPath); 

                    if (goldenMasterData != null)
                    {
                        var miniPetalRegion = new GoldenMasterMiniPetalRegion();
                        miniPetalRegion.Copy(tempRegionPath);
                        goldenMasterData.Regions.Add(miniPetalRegion);
                    }
                }
                else
                {
                    // 1st arc division
                    _onePlayerGraphicsLoop.AddArcRegion(
                        petalRegion,
                        ActualArcCentre,
                        MoveAlongLineByFraction(ActualArcCentre, ActualOuterArcStart, 1.5),
                        MoveAlongLineByFraction(ActualArcCentre, ArcSpokes.Points.ElementAt(0), 1.5),
                            goldenMasterData);

                    // middle arc divisions
                    for (int iCount = 1; iCount < ArcSpokes.Points.Count(); iCount++)
                    {
                        _onePlayerGraphicsLoop.AddArcRegion(
                            petalRegion,
                            ActualArcCentre,
                            MoveAlongLineByFraction(ActualArcCentre, ArcSpokes.Points.ElementAt(iCount - 1), 1.5),
                            MoveAlongLineByFraction(ActualArcCentre, ArcSpokes.Points.ElementAt(iCount), 1.5),
                            goldenMasterData);
                    }

                    // last arc division
                    _onePlayerGraphicsLoop.AddArcRegion(
                        petalRegion,
                        ActualArcCentre,
                        MoveAlongLineByFraction(ActualArcCentre, ArcSpokes.Points.ElementAt(ArcSpokes.Points.Count() - 1), 1.5),
                        MoveAlongLineByFraction(ActualArcCentre, ActualOuterArcEnd, 1.5),
                            goldenMasterData);
                }
            }
        }

        private void AddStartArmSegments(GoldenMasterSingleGraphicPass goldenMasterData)
        {
            if (NumArmSegments > 0)
            {
                // all the divisions of the start arm
                if (NumArmSegments > 1)
                {
                    for (int iCount = 0; iCount < NumArmSegments - 1; iCount++)
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
                    StartArmDivisionStarts.Points.ElementAt(NumArmSegments - 1),
                    StartArmDivisionEnds.Points.ElementAt(NumArmSegments - 1),
                    ActualInnerArcStart,
                    ActualOuterArcStart,
                    goldenMasterData
                    );
            }
        }

        private void AddStartArmCentralRegion(GoldenMasterSingleGraphicPass goldenMasterData)
        {
            // Start with the central region of the start-arm - this is the triangular bit that goes from the centre out to the start of the parallel-lines part of the arm
            // (after the central triangle, all the regions in the start-arm are parallelograms)
            if (NumTotalSegments > 1)
            {
                // start-arm central region
                _onePlayerGraphicsLoop.AddTriangularRegion(
                    Origin,
                    StartArmDivisionStarts.Points.ElementAt(0),
                    ActualInnerPetalSource,
                            goldenMasterData);
            }
            else
            {
                // just one central region
                _onePlayerGraphicsLoop.AddTriangularRegion(
                    Origin,
                    StartArmDivisionStarts.Points.ElementAt(0),
                    EndArmDivisionStarts.Points.ElementAt(0),
                            goldenMasterData);
            }
        }

        private void CalculateArcSpokeCoordinates()
        {
            // Create NumArcSegments divisions of the arc
            ArcSpokes.Points.Clear();
            if (NumArcSegments > 1)
            {
                TopGamePoint currentArcSpoke = GetEndPointOfRotatedLine(OuterArcRadius, ActualArcCentre, ActualOuterArcStart, ArcSegmentAngle);
                ArcSpokes.Points.Add(currentArcSpoke);
                TopGamePoint previousArcSpoke = currentArcSpoke;
                for (int iCount = 1; iCount < NumArcSegments - 1; iCount++)
                {
                    currentArcSpoke = GetEndPointOfRotatedLine(OuterArcRadius, ActualArcCentre, previousArcSpoke, ArcSegmentAngle);
                    ArcSpokes.Points.Add(currentArcSpoke);
                    previousArcSpoke = currentArcSpoke;
                }
            }
        }

        private void CalculateArmDivisions()
        {
            // Create NumArmSegments + 1 divisions of the start arm and end arm (including the centre).
            ClearAllArmDivisions();

            // Always create the first division, even if the arms have no segments.
            double fractionalMultiplier = 1.0 / ((double)NumArmSegments + 1.0);

            StartArmDivisionStarts.Points.Add(MoveAlongLineByFraction(Origin, ActualOuterArcStart, fractionalMultiplier));
            EndArmDivisionStarts.Points.Add(MoveAlongLineByFraction(Origin, ActualOuterArcEnd, fractionalMultiplier));

            StartArmDivisionEnds.Points.Add(ActualInnerPetalSource);
            EndArmDivisionEnds.Points.Add(ActualInnerPetalSource);

            // now create the rest, if necessary
            if (NumArmSegments > 1) // If there's only one, there's no dividers necessary
            {
                for (int iCount = 2; iCount <= NumArmSegments; iCount++)
                {
                    double outerFractionalMultiplier = (double)(iCount) / ((double)NumArmSegments + 1.0); // eg 4 segments: 2/5, 3/5, 4/5
                    StartArmDivisionStarts.Points.Add(MoveAlongLineByFraction(Origin, ActualOuterArcStart, outerFractionalMultiplier));
                    EndArmDivisionStarts.Points.Add(MoveAlongLineByFraction(Origin, ActualOuterArcEnd, outerFractionalMultiplier));

                    double innerFractionalMultiplier = ((double)(iCount) - 1.0) / (double)(NumArmSegments); // eg 4 segments: 1/4, 2/4, 3/4
                    StartArmDivisionEnds.Points.Add(MoveAlongLineByFraction(ActualInnerPetalSource, ActualInnerArcStart, innerFractionalMultiplier));
                    EndArmDivisionEnds.Points.Add(MoveAlongLineByFraction(ActualInnerPetalSource, ActualInnerArcEnd, innerFractionalMultiplier));
                }
            }
        }

        private void CalculateArcCoordinates()
        {
            NumArcSegments = ArcWillExist() ? NumSegmentsContainedInArmsAndArc().DividedBy3PlusLeftovers() : 0;
            ArcSegmentAngle = (NumArcSegments > 0) ? 180 / NumArcSegments : 0;
            ArcStartAngle = TotalAngleShare / 2 + CentralAngle / 2; // was previously expressed as 180 - (_vitalStatistics.AngleB + _vitalStatistics.AngleC)
            OriginToArcCentre = GetAdjacentSide(OuterArmLength, CentralAngle / 2);
            OuterArcRadius = GetOppositeSide(OuterArmLength, CentralAngle / 2);
            InnerArcRadius = (InnerArmLength > 0) ? GetOppositeSide(InnerArmLength, CentralAngle / 2) : 0;

            ActualArcCentre.PopulateFromLengthTopAngleAndStartPoint(
                OriginToArcCentre,
                TotalAngleShare / 2 + CentralAngle / 2,
                Origin,
                RelativeArcCentre);

            if (InnerArmLength > 0)
            {
                // !! Caution !! The inner arc values are relative to the inner petal source, NOT to the Origin!
                ActualInnerArcStart.PopulateFromLengthTopAngleAndStartPoint(
                    InnerArmLength,
                    TotalAngleShare / 2 + CentralAngle,
                    ActualInnerPetalSource,
                    RelativeInnerArcStart);

                ActualInnerArcEnd.PopulateFromLengthTopAngleAndStartPoint(
                    InnerArmLength,
                    TotalAngleShare / 2,
                    ActualInnerPetalSource,
                    RelativeInnerArcEnd);
            }

            ActualOuterArcStart.PopulateFromLengthTopAngleAndStartPoint(
                OuterArmLength,
                TotalAngleShare / 2 + CentralAngle,
                Origin,
                RelativeOuterArcStart);

            ActualOuterArcEnd.PopulateFromLengthTopAngleAndStartPoint(
                OuterArmLength,
                TotalAngleShare / 2,
                Origin,
                RelativeOuterArcEnd);

            CalculateArcSpokeCoordinates();
        }

        private Region MakePetalRegion()
        {
            Region petalRegion = null;
            TopGameGraphicsPath petalPath = MakePetalPath();

            MakeObsoleteOuterAndInnerPath();

            // Create petal region 
            petalRegion = new Region(petalPath.ActualPath);
            //storedPetalRegion = new Region(petalPath.ActualPath);

            return petalRegion;
        }

        private TopGameGraphicsPath MakePetalPath()
        {
            TopGameGraphicsPath petalPath = new TopGameGraphicsPath();

            if (NumArcSegments > 0)
            {
                petalPath.AddForwardCircularArc(
                    ActualArcCentre,
                    OuterArcRadius,
                    ArcStartAngle);
            }

            if (InnerArmLength <= 0)
            {
                // We have to go from start to end instead of from end to start, because the outer arc gets drawn from end to start!
                petalPath.AddLine(ActualOuterArcStart, ActualOuterArcEnd);
            }
            else
            {
                petalPath.AddLine(ActualOuterArcStart, ActualInnerArcStart);

                petalPath.AddBackwardCircularArc(
                    ActualArcCentre,
                    InnerArcRadius,
                    ArcStartAngle);

                petalPath.AddLine(ActualInnerArcEnd, ActualOuterArcEnd);
            }

            return petalPath;
        }

        private void MakeObsoleteOuterAndInnerPath()
        {
            OuterPath.Reset();
            // We have to go from end to start instead of from start to end, because the arc gets drawn that way round.
            OuterPath.AddLine(Origin, ActualOuterArcEnd);
            if (NumArcSegments > 0)
            {
                // !! The y coordinate of the enclosing rectangle represents the TOP of the shape, not the bottom
                OuterArcSquare.X = ActualArcCentre.X - (int)Math.Round(OuterArcRadius, 0, MidpointRounding.AwayFromZero);
                OuterArcSquare.Y = ActualArcCentre.Y - (int)Math.Round(OuterArcRadius, 0, MidpointRounding.AwayFromZero);
                OuterArcSquare.Width = (int)Math.Round(OuterArcRadius * 2, 0, MidpointRounding.AwayFromZero);
                OuterArcSquare.Height = (int)Math.Round(OuterArcRadius * 2, 0, MidpointRounding.AwayFromZero);

                // See AddArcPath for explanation of how arcs are drawn.
                OuterPath.AddArcPath(OuterArcSquare, (float)ArcStartAngle, (float)180);
            }
            else
            {
                OuterPath.AddLine(ActualOuterArcStart, ActualOuterArcEnd);
            }
            OuterPath.AddLine(ActualOuterArcStart, Origin);

            if (InnerArmLength > 0)
            {
                InnerPath.Reset();
                // We have to go from end to start instead of from start to end, because the arc gets drawn that way round.
                InnerPath.AddLine(ActualInnerPetalSource, ActualInnerArcEnd);

                // !! The y coordinate of the enclosing rectangle represents the TOP of the shape, not the bottom
                InnerArcSquare.X = ActualArcCentre.X - (int)Math.Round(InnerArcRadius, 0, MidpointRounding.AwayFromZero);
                InnerArcSquare.Y = ActualArcCentre.Y - (int)Math.Round(InnerArcRadius, 0, MidpointRounding.AwayFromZero);
                InnerArcSquare.Width = (int)Math.Round(InnerArcRadius * 2, 0, MidpointRounding.AwayFromZero);
                InnerArcSquare.Height = (int)Math.Round(InnerArcRadius * 2, 0, MidpointRounding.AwayFromZero);

                // See AddArcPath for explanation of how arcs are drawn.
                InnerPath.AddArcPath(InnerArcSquare, (float)ArcStartAngle, (float)180);
                InnerPath.AddLine(ActualInnerArcStart, ActualInnerPetalSource);
            }
        }

        private void CalculateArmSegmentLength()
        {
            ArmSegmentLength = TopGameConstants.ConstantSegmentLength;
            GrowSegmentLengthByALittleToAccountForExtraArcSegments();

            double potentialOuterArmLength = (NumArmSegments + 1) * ArmSegmentLength;
            if (potentialOuterArmLength > MaximumArmLengthWhichFitsInFrame())
            {
                ArmSegmentLength = MaximumArmLengthWhichFitsInFrame() / NumArmSegments;
                ShrinkSegmentLengthByALittleToAccountForExtraArcSegments();
            }
        }

        private void GrowSegmentLengthByALittleToAccountForExtraArcSegments()
        {
            double segmentAddition = ArcWillExist() ? NumSegmentsContainedInArmsAndArc().LeftoverAfterDividedBy3() : 0;

            ArmSegmentLength = ArmSegmentLength + (TopGameConstants.SegmentGrowthRatio * segmentAddition);
        }

        private void ShrinkSegmentLengthByALittleToAccountForExtraArcSegments()
        {
            double segmentAddition = ArcWillExist() ? NumSegmentsContainedInArmsAndArc().LeftoverAfterDividedBy3() : 0;

            ArmSegmentLength = ArmSegmentLength
                - (TopGameConstants.SegmentGrowthRatio * (2 - segmentAddition));
        }

        private int MaximumArmLengthWhichFitsInFrame()
        {
            return Origin.Y - 70;
        }

        private bool ArcWillExist()
        {
            return NumTotalSegments > 2;
        }

        private int NumSegmentsContainedInArmsAndArc()
        {
            return NumTotalSegments - 2;
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
            NumTotalSegments = 0;
            CentralAngle = 0;
            OuterPath.Reset();
            InnerPath.Reset();
            ClearAllArmDivisions();
            if (ArcSpokes.Points.Any())
            {
                ArcSpokes.Points.Clear();
            }
        }

        public double SafeTan(double angleInDegrees)
        {
            return Math.Tan(DegreeToRadian(angleInDegrees));
        }

        public double SafeCos(double angleInDegrees)
        {
            return Math.Cos(DegreeToRadian(angleInDegrees));
        }

        public double SafeSin(double angleInDegrees)
        {
            return Math.Sin(DegreeToRadian(angleInDegrees));
        }

        public double SafeAtan(double tanValue)
        {
            return RadianToDegree(Math.Atan(tanValue));
        }

        public double SafeAcos(double cosValue)
        {
            return RadianToDegree(Math.Acos(cosValue));
        }

        public double SafeAsin(double sinValue)
        {
            return RadianToDegree(Math.Asin(sinValue));
        }

        public double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        Point MoveAlongLineByLength(Point startPoint, Point endPoint, double length)
        {
            Point newPoint = new Point();
            Point travelVector = new Point();
            Point lineVector = new Point();
            double angle = 0;

            if (length == 0)
            {
                newPoint = startPoint;
            }
            else
            {
                // find the vector for the line itself
                lineVector.X = endPoint.X - startPoint.X;
                lineVector.Y = endPoint.Y - startPoint.Y;

                // calculate the angle 
                angle = SafeAtan(lineVector.Y / lineVector.X);

                // create the vector that moves in the correct direction by the specified amount
                travelVector.X = (int)Math.Round(GetAdjacentSide(length, angle), 0, MidpointRounding.AwayFromZero);
                travelVector.Y = (int)Math.Round(GetOppositeSide(length, angle), 0, MidpointRounding.AwayFromZero);

                // add the vector to the start point.
                newPoint.X = startPoint.X + travelVector.X;
                newPoint.Y = startPoint.Y + travelVector.Y;
            }

            return newPoint;
        }

        TopGamePoint MoveAlongLineByFraction(TopGamePoint startPoint, TopGamePoint endPoint, double fraction)
        {
            TopGamePoint newPoint = new TopGamePoint();
            TopGamePoint travelVector = new TopGamePoint();

            if (fraction == 0)
            {
                fraction = 1;
            }

            // create the vector that moves in the correct direction by the specified amount
            travelVector.X = (int)Math.Round(fraction * (endPoint.X - startPoint.X), 0, MidpointRounding.AwayFromZero);
            travelVector.Y = (int)Math.Round(fraction * (endPoint.Y - startPoint.Y), 0, MidpointRounding.AwayFromZero);

            // add the vector to the start point.
            newPoint.X = startPoint.X + travelVector.X;
            newPoint.Y = startPoint.Y + travelVector.Y;

            return newPoint;
        }

        TopGamePoint GetEndPointOfRotatedLine(
                            double sourceLineLength,
                            TopGamePoint sourceLineStartPoint,
                            TopGamePoint sourceLineEndPoint,
                            double antiClockRotationAngle)
        {
            TopGamePoint rotatedEndPoint = new TopGamePoint();

            double relativeSourceX = sourceLineStartPoint.X - sourceLineEndPoint.X;
            double relativeSourceY = sourceLineStartPoint.Y - sourceLineEndPoint.Y;
            bool relativeXPositive = (relativeSourceX >= 0);
            bool relativeYPositive = (relativeSourceY >= 0);
            int finalXMultiplier = relativeXPositive ? -1 : 1;
            int finalYMultiplier = relativeYPositive ? -1 : 1;

            double acosTarget = Math.Abs(relativeSourceX) / sourceLineLength;
            if (acosTarget > 1)
            {
                // This can happen because X values have been rounded.
                acosTarget = 1;
            }
            double sourceVerticalAngle = SafeAcos(acosTarget);

            double newVerticalAngle = sourceVerticalAngle - antiClockRotationAngle;
            if (relativeXPositive != relativeYPositive)
            {
                // If the X and the Y don't have the same sign, the source line was in one of the opposite 2 quadrants
                // This changes the relationship between the three angles.
                newVerticalAngle = sourceVerticalAngle + antiClockRotationAngle;
            }

            rotatedEndPoint.X = (int)Math.Round(sourceLineLength * SafeCos(newVerticalAngle), 0, MidpointRounding.AwayFromZero);
            rotatedEndPoint.Y = (int)Math.Round(sourceLineLength * SafeSin(newVerticalAngle), 0, MidpointRounding.AwayFromZero);

            rotatedEndPoint.X = finalXMultiplier * rotatedEndPoint.X;
            rotatedEndPoint.Y = finalYMultiplier * rotatedEndPoint.Y;

            // What we have now is actually an abstract vector in space - not a line related to the original start point.
            // Need to adjust it relative to the original start point.
            rotatedEndPoint.X = sourceLineStartPoint.X + rotatedEndPoint.X;
            rotatedEndPoint.Y = sourceLineStartPoint.Y + rotatedEndPoint.Y;

            return rotatedEndPoint;
        }

        double GetAdjacentSide(double hypotenuse, double angle)
        {
            return SafeCos(angle) * hypotenuse;
        }

        double GetOppositeSide(double hypotenuse, double angle)
        {
            return SafeSin(angle) * hypotenuse;
        }

        /// <summary>
        /// Sort out what proportion of the circle we are getting
        /// </summary>
        /// <param name="maxCentralAngle"></param>
        /// <param name="angleShare"></param>
        public void SetAngles(double maxCentralAngle, double angleShare)
        {
            SetMaxCentralAngle(maxCentralAngle);
            SetTotalAngleShare(angleShare);

            // Note that CalculateCentralAngle will return different results depending on how many segments there are.
            CalculateCentralAngle(360, 52, false);
        }

        private void SetMaxCentralAngle(double newMax)
        {
            MaxCentralAngle = newMax;
        }

        private void SetTotalAngleShare(double newTotalAngleShare)
        {
            TotalAngleShare = newTotalAngleShare;
        }
    }
}