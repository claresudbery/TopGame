using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Domain.Extensions;
using Domain.GraphicModels.GoldenMaster;
using TopGameWindowsApp;

namespace Domain.GraphicModels
{
    /// <summary>
    /// This represents the "loop" visible on screen (looks a bit like a petal) which represents one player's cards (or the cards currently in play)
    /// </summary>
    public class OnePlayerGraphicsLoop
    {
        public static System.Drawing.Color GetConstantBackgroundColour()
        {
            return System.Drawing.Color.CornflowerBlue;
        }

        private readonly TopGameGraphicsData _topGameGraphicsData = new TopGameGraphicsData();

        Region storedPetalRegion; // just for debug purposes: see what the petal region looks like.
        List<Region> subRegions;
        List<ColouredRegion> regionColours;

        public void Dispose()
        {
            _topGameGraphicsData.Dispose();
            DisposeRegions();
        }

        private void DisposeRegions()
        {
            for (int iCount = subRegions.Count() - 1; iCount >= 0; iCount--)
            {
                subRegions.ElementAt(iCount).Dispose();
                subRegions.RemoveAt(iCount);
            }
        }

        public OnePlayerGraphicsLoop()
        {
            subRegions = new List<Region>();
            regionColours = new List<ColouredRegion>();
        }

        public void RotateByAngle(double rotationAngle)
        {
            using (Matrix rotateMatrix = new Matrix())
            {
                rotateMatrix.RotateAt((float)rotationAngle, _topGameGraphicsData.Origin.Point);
                for (int iCount = 0; iCount < subRegions.Count(); iCount++)
                {
                    subRegions.ElementAt(iCount).Transform(rotateMatrix);
                }
            }
        }

        public void Clear()
        {
            _topGameGraphicsData.NumTotalSegments = 0;
            _topGameGraphicsData.CentralAngle = 0;
            DisposeRegions();
            _topGameGraphicsData.OuterPath.Reset();
            _topGameGraphicsData.InnerPath.Reset();
            ClearAllArmDivisions();
            if (_topGameGraphicsData.ArcSpokes.Points.Any())
            {
                _topGameGraphicsData.ArcSpokes.Points.Clear(); 
            }
            regionColours.Clear();
        }

        public void RemoveTopSegment()
        {
            _topGameGraphicsData.NumTotalSegments--;
            regionColours.RemoveAt(0);
        }

        public void RemoveBottomSegment()
        {
            _topGameGraphicsData.NumTotalSegments--;
            regionColours.RemoveAt(regionColours.Count() - 1);
        }

        public void AddSegment()
        {
            _topGameGraphicsData.NumTotalSegments++;
            regionColours.Add(new ColouredRegion());
        }

        public void SetNumTotalSegments(int numSegments)
        {
            Clear();
            for (int iCount = 0; iCount < numSegments; iCount++)
            {
                AddSegment();
            }
        }

        public int GetNumTotalSegments()
        {
            return _topGameGraphicsData.NumTotalSegments;
        }

        public void SetMaxCentralAngle(double newMax)
        {
            _topGameGraphicsData.MaxCentralAngle = newMax;
        }

        public void SetConstantBottomAngle(double newBottom)
        {
            _topGameGraphicsData.TotalAngleShare = newBottom;
        }

        public void SetCentralAngle(double newAngle)
        {
            _topGameGraphicsData.CentralAngle = newAngle;
        }

        public double GetCentralAngle()
        {
            return _topGameGraphicsData.CentralAngle;
        }

        public int NumRegions()
        {
            return subRegions.Count();
        }

        public bool IsMinimumAngleApplied()
        {
            return _topGameGraphicsData.MinimumAngleApplied;
        }

        public bool IsMaximumAngleApplied()
        {
            return _topGameGraphicsData.MaximumAngleApplied;
        }

        // This one displays rainbow colours
        // If iRegionIndex is -1, all regions are displayed - otherwise just the region indicated by iRegionIndex.
        public void Display(int iRegionIndex, int iColourCycler, PaintEventArgs e, bool bClearGraphics)
        {
            if (_topGameGraphicsData.NumTotalSegments > 0)
            {
                int iRegionCount = (iRegionIndex == -1) ? subRegions.Count() : iRegionIndex + 1;
                int iRegionStart = (iRegionIndex == -1) ? 0 : iRegionIndex;

                if (bClearGraphics)
                {
                    e.Graphics.Clear(GetConstantBackgroundColour());
                }

                // Fill and display the sub-regions
                using (SolidBrush myBrush = new SolidBrush(System.Drawing.Color.Black))
                {
                    for (int i = iRegionStart; i < iRegionCount; i++)
                    {
                        switch ((i + iColourCycler) % 7)
                        {
                            case 0:
                                {
                                    myBrush.Color = System.Drawing.Color.Red;
                                }
                                break;
                            case 1:
                                {
                                    myBrush.Color = System.Drawing.Color.Orange;
                                }
                                break;
                            case 2:
                                {
                                    myBrush.Color = System.Drawing.Color.Yellow;
                                }
                                break;
                            case 3:
                                {
                                    myBrush.Color = System.Drawing.Color.Green;
                                }
                                break;
                            case 4:
                                {
                                    myBrush.Color = System.Drawing.Color.Blue;
                                }
                                break;
                            case 5:
                                {
                                    myBrush.Color = System.Drawing.Color.Indigo;
                                }
                                break;
                            case 6:
                                {
                                    myBrush.Color = System.Drawing.Color.Violet;
                                }
                                break;
                        }
                        e.Graphics.FillRegion(myBrush, subRegions.ElementAt(i));
                    }
                }
            }
        }

        // just for debug purposes: see what the petal region looks like.
        public void DisplayPetalRegion(PaintEventArgs e)
        {
            using (SolidBrush myBrush = new SolidBrush(System.Drawing.Color.Black))
            {
                myBrush.Color = regionColours.ElementAt(0).TheColour;
                //e.Graphics.FillRegion(myBrush, storedPetalRegion);

                System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Black, 1);
                e.Graphics.DrawPath(myPen, _topGameGraphicsData.OuterPath.ActualPath);

                //System.Drawing.Pen myOtherPen = new System.Drawing.Pen(System.Drawing.Color.Red, 1);
                //e.Graphics.DrawPath(myOtherPen, _vitalStatistics.InnerPath.ActualPath);
            }
        }

        // This one displays stored colours
        public void Display(int iRegionIndex, PaintEventArgs e)
        {
            if (_topGameGraphicsData.NumTotalSegments > 0)
            {
                int iRegionCount = (iRegionIndex == -1) ? subRegions.Count() : iRegionIndex + 1;
                int iRegionStart = (iRegionIndex == -1) ? 0 : iRegionIndex;

                // Fill and display the sub-regions
                using (System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Black, 1))
                using (SolidBrush myBrush = new SolidBrush(System.Drawing.Color.Black))
                {
                    for (int i = iRegionStart; i < iRegionCount; i++)
                    {
                        myBrush.Color = regionColours.ElementAt(i).TheColour;
                        e.Graphics.FillRegion(myBrush, subRegions.ElementAt(i));
                    }
                }
            }
        }

        public void DisplayFinalRegion(PaintEventArgs e)
        {
            using (SolidBrush myBrush = new SolidBrush(System.Drawing.Color.Black))
            using (System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red, 1))
            using (GraphicsPath tempRegionPath = new GraphicsPath())
            {
                // end-arm central region
                tempRegionPath.AddLine(_topGameGraphicsData.Origin.Point, _topGameGraphicsData.EndArmDivisionStarts.Points.ElementAt(0).Point);
                tempRegionPath.AddLine(_topGameGraphicsData.EndArmDivisionStarts.Points.ElementAt(0).Point, _topGameGraphicsData.ActualInnerPetalSource.Point);
                tempRegionPath.AddLine(_topGameGraphicsData.ActualInnerPetalSource.Point, _topGameGraphicsData.Origin.Point);
                e.Graphics.FillRegion(myBrush, subRegions.ElementAt(subRegions.Count() - 1));
            }
        }

        public double CalculateCentralAngle(double numDegreesAvailable, int numCardsBeingShared, bool bSuppressMinAndMax)
        {
            return _topGameGraphicsData.CalculateCentralAngle(numDegreesAvailable, numCardsBeingShared, bSuppressMinAndMax);
        }

        public void LoadNewData(double rotationAngle)
        {
            if (_topGameGraphicsData.NumTotalSegments > 0)
            {
                PrepareActualData(rotationAngle);
            }
        }

        // See TopGame\Docs\Arc-and-angles.jpg and TopGame\Docs\GraphicsPath-Arc.png for explanatory diagrams.
        public void PrepareActualData(
            double rotationAngle,
            GoldenMasterSingleGraphicPass goldenMasterData = null)
        {
            CalculateObsoleteAngleAAndAngleB();

            CalculateGlobalValues();
            CalculateArcCoordinates();
            CalculateArmDivisions();
            AddSubRegions(goldenMasterData);

            Debug.Assert(_topGameGraphicsData.NumTotalSegments == subRegions.Count(), "Region count", "Number of regions is not num total segments");

            if (rotationAngle > 0)
            {
                RotateByAngle(rotationAngle);
            }
        }

        private void CalculateGlobalValues()
        {
            // Arms
            _topGameGraphicsData.NumArmSegments = ArcWillExist() ? NumSegmentsContainedInArmsAndArc().DividedBy3() : 0;
            CalculateArmSegmentLength();
            _topGameGraphicsData.OuterArmLength = (_topGameGraphicsData.NumArmSegments + 1) * _topGameGraphicsData.ArmSegmentLength;
            _topGameGraphicsData.InnerArmLength = _topGameGraphicsData.NumArmSegments * _topGameGraphicsData.ArmSegmentLength;

            // Central triangle(s)
            _topGameGraphicsData.CentralSpokeLength = GetAdjacentSide(_topGameGraphicsData.ArmSegmentLength, _topGameGraphicsData.CentralAngle / 2);

            // Inner petal source
            _topGameGraphicsData.ActualInnerPetalSource.PopulateFromLengthTopAngleAndStartPoint(
                _topGameGraphicsData.CentralSpokeLength,
                _topGameGraphicsData.TotalAngleShare / 2 + _topGameGraphicsData.CentralAngle / 2,
                _topGameGraphicsData.Origin,
                _topGameGraphicsData.RelativeInnerPetalSource);
        }

        private void CalculateObsoleteAngleAAndAngleB()
        {
            // AngleB and AngleC are now not used - only here so as not to break golden master
            _topGameGraphicsData.AngleB = 90 - _topGameGraphicsData.TotalAngleShare / 2; // ConstantBottomAngle = angleShare, ie 360 / num hands
            _topGameGraphicsData.AngleC = 90 - _topGameGraphicsData.CentralAngle / 2; // was previously expressed as (180 - _vitalStatistics.CentralAngle) / 2
        }

        // The sub-regions are all the areas that get coloured in: Each region represents an individual card.
        // They are displayed in three sections, all of which added together look a bit like a petal:
        // 1) The straight "start-arm", basically made out of two parallel lines (but tapering to a triangular point at the centre)
        // 2) The curved "arc" which joins the two arms together
        // 3) The straight "end-arm", basically made out of two parallel lines (but tapering to a triangular point at the centre)
        private void AddSubRegions(GoldenMasterSingleGraphicPass goldenMasterData)
        {
            DisposeRegions();

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
            if (_topGameGraphicsData.NumTotalSegments > 1)
            {
                // end-arm central region
                AddTriangularRegion(
                    _topGameGraphicsData.Origin,
                    _topGameGraphicsData.EndArmDivisionStarts.Points.ElementAt(0),
                    _topGameGraphicsData.ActualInnerPetalSource,
                            goldenMasterData
                    );
            }
        }

        private void AddEndArmSegments(GoldenMasterSingleGraphicPass goldenMasterData)
        {
            if (_topGameGraphicsData.NumArmSegments > 0)
            {
                // all the divisions of the end arm (in reverse)
                AddParallelogramRegion(
                    _topGameGraphicsData.EndArmDivisionStarts.Points.ElementAt(_topGameGraphicsData.NumArmSegments - 1),
                    _topGameGraphicsData.EndArmDivisionEnds.Points.ElementAt(_topGameGraphicsData.NumArmSegments - 1),
                    _topGameGraphicsData.ActualInnerArcEnd,
                    _topGameGraphicsData.ActualOuterArcEnd,
                            goldenMasterData
                    );

                if (_topGameGraphicsData.NumArmSegments > 1)
                {
                    for (int iCount = _topGameGraphicsData.NumArmSegments - 1; iCount > 0; iCount--)
                    {
                        AddParallelogramRegion(
                            _topGameGraphicsData.EndArmDivisionStarts.Points.ElementAt(iCount),
                            _topGameGraphicsData.EndArmDivisionEnds.Points.ElementAt(iCount),
                            _topGameGraphicsData.EndArmDivisionEnds.Points.ElementAt(iCount - 1),
                            _topGameGraphicsData.EndArmDivisionStarts.Points.ElementAt(iCount - 1),
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
            if (_topGameGraphicsData.NumArcSegments > 0)
            {
                if (_topGameGraphicsData.ArcSpokes.Points.Count() == 0)
                {
                    // the division is the arc itself.
                    TopGameGraphicsPath tempRegionPath = new TopGameGraphicsPath();
                    // See AddArcPath for explanation of how arcs are drawn.
                    tempRegionPath.AddArcPath(_topGameGraphicsData.OuterArcSquare, (float)_topGameGraphicsData.ArcStartAngle, (float)180);
                    tempRegionPath.AddLine(_topGameGraphicsData.ActualOuterArcStart, _topGameGraphicsData.ActualOuterArcEnd);
                    subRegions.Add(new Region(tempRegionPath.ActualPath));

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
                    AddArcRegion(
                        petalRegion,
                        _topGameGraphicsData.ActualArcCentre,
                        MoveAlongLineByFraction(_topGameGraphicsData.ActualArcCentre, _topGameGraphicsData.ActualOuterArcStart, 1.5),
                        MoveAlongLineByFraction(_topGameGraphicsData.ActualArcCentre, _topGameGraphicsData.ArcSpokes.Points.ElementAt(0), 1.5),
                            goldenMasterData);

                    // middle arc divisions
                    for (int iCount = 1; iCount < _topGameGraphicsData.ArcSpokes.Points.Count(); iCount++)
                    {
                        AddArcRegion(
                            petalRegion,
                            _topGameGraphicsData.ActualArcCentre,
                            MoveAlongLineByFraction(_topGameGraphicsData.ActualArcCentre, _topGameGraphicsData.ArcSpokes.Points.ElementAt(iCount - 1), 1.5),
                            MoveAlongLineByFraction(_topGameGraphicsData.ActualArcCentre, _topGameGraphicsData.ArcSpokes.Points.ElementAt(iCount), 1.5),
                            goldenMasterData);
                    }

                    // last arc division
                    AddArcRegion(
                        petalRegion,
                        _topGameGraphicsData.ActualArcCentre,
                        MoveAlongLineByFraction(_topGameGraphicsData.ActualArcCentre, _topGameGraphicsData.ArcSpokes.Points.ElementAt(_topGameGraphicsData.ArcSpokes.Points.Count() - 1), 1.5),
                        MoveAlongLineByFraction(_topGameGraphicsData.ActualArcCentre, _topGameGraphicsData.ActualOuterArcEnd, 1.5),
                            goldenMasterData);
                }
            }
        }

        private void AddStartArmSegments(GoldenMasterSingleGraphicPass goldenMasterData)
        {
            if (_topGameGraphicsData.NumArmSegments > 0)
            {
                // all the divisions of the start arm
                if (_topGameGraphicsData.NumArmSegments > 1)
                {
                    for (int iCount = 0; iCount < _topGameGraphicsData.NumArmSegments - 1; iCount++)
                    {
                        AddParallelogramRegion(
                            _topGameGraphicsData.StartArmDivisionStarts.Points.ElementAt(iCount),
                            _topGameGraphicsData.StartArmDivisionEnds.Points.ElementAt(iCount),
                            _topGameGraphicsData.StartArmDivisionEnds.Points.ElementAt(iCount + 1),
                            _topGameGraphicsData.StartArmDivisionStarts.Points.ElementAt(iCount + 1),
                            goldenMasterData
                            );
                    }
                }

                // the last one hooks up to the arc.
                AddParallelogramRegion(
                    _topGameGraphicsData.StartArmDivisionStarts.Points.ElementAt(_topGameGraphicsData.NumArmSegments - 1),
                    _topGameGraphicsData.StartArmDivisionEnds.Points.ElementAt(_topGameGraphicsData.NumArmSegments - 1),
                    _topGameGraphicsData.ActualInnerArcStart,
                    _topGameGraphicsData.ActualOuterArcStart,
                    goldenMasterData
                    );
            }
        }

        private void AddStartArmCentralRegion(GoldenMasterSingleGraphicPass goldenMasterData)
        {// Start with the central region of the start-arm - this is the triangular bit that goes from the centre out to the start of the parallel-lines part of the arm
            // (after the central triangle, all the regions in the start-arm are parallelograms)
            if (_topGameGraphicsData.NumTotalSegments > 1)
            {
                // start-arm central region
                AddTriangularRegion(
                    _topGameGraphicsData.Origin,
                    _topGameGraphicsData.StartArmDivisionStarts.Points.ElementAt(0),
                    _topGameGraphicsData.ActualInnerPetalSource,
                            goldenMasterData);
            }
            else
            {
                // just one central region
                AddTriangularRegion(
                    _topGameGraphicsData.Origin,
                    _topGameGraphicsData.StartArmDivisionStarts.Points.ElementAt(0),
                    _topGameGraphicsData.EndArmDivisionStarts.Points.ElementAt(0),
                            goldenMasterData);
            }
        }

        private void CalculateArcSpokeCoordinates()
        {
            // Create NumArcSegments divisions of the arc
            _topGameGraphicsData.ArcSpokes.Points.Clear();
            if (_topGameGraphicsData.NumArcSegments > 1)
            {
                TopGamePoint currentArcSpoke = GetEndPointOfRotatedLine(_topGameGraphicsData.OuterArcRadius, _topGameGraphicsData.ActualArcCentre, _topGameGraphicsData.ActualOuterArcStart, _topGameGraphicsData.ArcSegmentAngle);
                _topGameGraphicsData.ArcSpokes.Points.Add(currentArcSpoke);
                TopGamePoint previousArcSpoke = currentArcSpoke;
                for (int iCount = 1; iCount < _topGameGraphicsData.NumArcSegments - 1; iCount++)
                {
                    currentArcSpoke = GetEndPointOfRotatedLine(_topGameGraphicsData.OuterArcRadius, _topGameGraphicsData.ActualArcCentre, previousArcSpoke, _topGameGraphicsData.ArcSegmentAngle);
                    _topGameGraphicsData.ArcSpokes.Points.Add(currentArcSpoke);
                    previousArcSpoke = currentArcSpoke;
                }
            }
        }

        private void CalculateArmDivisions()
        {
            // Create NumArmSegments + 1 divisions of the start arm and end arm (including the centre).
            ClearAllArmDivisions();

            // Always create the first division, even if the arms have no segments.
            double fractionalMultiplier = 1.0 / ((double)_topGameGraphicsData.NumArmSegments + 1.0);

            _topGameGraphicsData.StartArmDivisionStarts.Points.Add(MoveAlongLineByFraction(_topGameGraphicsData.Origin, _topGameGraphicsData.ActualOuterArcStart, fractionalMultiplier));
            _topGameGraphicsData.EndArmDivisionStarts.Points.Add(MoveAlongLineByFraction(_topGameGraphicsData.Origin, _topGameGraphicsData.ActualOuterArcEnd, fractionalMultiplier));

            _topGameGraphicsData.StartArmDivisionEnds.Points.Add(_topGameGraphicsData.ActualInnerPetalSource);
            _topGameGraphicsData.EndArmDivisionEnds.Points.Add(_topGameGraphicsData.ActualInnerPetalSource);

            // now create the rest, if necessary
            if (_topGameGraphicsData.NumArmSegments > 1) // If there's only one, there's no dividers necessary
            {
                for (int iCount = 2; iCount <= _topGameGraphicsData.NumArmSegments; iCount++)
                {
                    double outerFractionalMultiplier = (double)(iCount) / ((double)_topGameGraphicsData.NumArmSegments + 1.0); // eg 4 segments: 2/5, 3/5, 4/5
                    _topGameGraphicsData.StartArmDivisionStarts.Points.Add(MoveAlongLineByFraction(_topGameGraphicsData.Origin, _topGameGraphicsData.ActualOuterArcStart, outerFractionalMultiplier));
                    _topGameGraphicsData.EndArmDivisionStarts.Points.Add(MoveAlongLineByFraction(_topGameGraphicsData.Origin, _topGameGraphicsData.ActualOuterArcEnd, outerFractionalMultiplier));

                    double innerFractionalMultiplier = ((double)(iCount) - 1.0) / (double)(_topGameGraphicsData.NumArmSegments); // eg 4 segments: 1/4, 2/4, 3/4
                    _topGameGraphicsData.StartArmDivisionEnds.Points.Add(MoveAlongLineByFraction(_topGameGraphicsData.ActualInnerPetalSource, _topGameGraphicsData.ActualInnerArcStart, innerFractionalMultiplier));
                    _topGameGraphicsData.EndArmDivisionEnds.Points.Add(MoveAlongLineByFraction(_topGameGraphicsData.ActualInnerPetalSource, _topGameGraphicsData.ActualInnerArcEnd, innerFractionalMultiplier));
                }
            }
        }

        private void CalculateArcCoordinates()
        {
            _topGameGraphicsData.NumArcSegments = ArcWillExist() ? NumSegmentsContainedInArmsAndArc().DividedBy3PlusLeftovers() : 0;
            _topGameGraphicsData.ArcSegmentAngle = (_topGameGraphicsData.NumArcSegments > 0) ? 180 / _topGameGraphicsData.NumArcSegments : 0;
            _topGameGraphicsData.ArcStartAngle = _topGameGraphicsData.TotalAngleShare / 2 + _topGameGraphicsData.CentralAngle / 2; // was previously expressed as 180 - (_vitalStatistics.AngleB + _vitalStatistics.AngleC)
            _topGameGraphicsData.OriginToArcCentre = GetAdjacentSide(_topGameGraphicsData.OuterArmLength, _topGameGraphicsData.CentralAngle / 2);
            _topGameGraphicsData.OuterArcRadius = GetOppositeSide(_topGameGraphicsData.OuterArmLength, _topGameGraphicsData.CentralAngle / 2);
            _topGameGraphicsData.InnerArcRadius = (_topGameGraphicsData.InnerArmLength > 0) ? GetOppositeSide(_topGameGraphicsData.InnerArmLength, _topGameGraphicsData.CentralAngle / 2) : 0;

            _topGameGraphicsData.ActualArcCentre.PopulateFromLengthTopAngleAndStartPoint(
                _topGameGraphicsData.OriginToArcCentre,
                _topGameGraphicsData.TotalAngleShare / 2 + _topGameGraphicsData.CentralAngle / 2,
                _topGameGraphicsData.Origin,
                _topGameGraphicsData.RelativeArcCentre);

            if (_topGameGraphicsData.InnerArmLength > 0)
            {
                // !! Caution !! The inner arc values are relative to the inner petal source, NOT to the Origin!
                _topGameGraphicsData.ActualInnerArcStart.PopulateFromLengthTopAngleAndStartPoint(
                    _topGameGraphicsData.InnerArmLength,
                    _topGameGraphicsData.TotalAngleShare / 2 + _topGameGraphicsData.CentralAngle,
                    _topGameGraphicsData.ActualInnerPetalSource,
                    _topGameGraphicsData.RelativeInnerArcStart);

                _topGameGraphicsData.ActualInnerArcEnd.PopulateFromLengthTopAngleAndStartPoint(
                    _topGameGraphicsData.InnerArmLength,
                    _topGameGraphicsData.TotalAngleShare / 2,
                    _topGameGraphicsData.ActualInnerPetalSource,
                    _topGameGraphicsData.RelativeInnerArcEnd);
            }

            _topGameGraphicsData.ActualOuterArcStart.PopulateFromLengthTopAngleAndStartPoint(
                _topGameGraphicsData.OuterArmLength,
                _topGameGraphicsData.TotalAngleShare / 2 + _topGameGraphicsData.CentralAngle,
                _topGameGraphicsData.Origin,
                _topGameGraphicsData.RelativeOuterArcStart);

            _topGameGraphicsData.ActualOuterArcEnd.PopulateFromLengthTopAngleAndStartPoint(
                _topGameGraphicsData.OuterArmLength,
                _topGameGraphicsData.TotalAngleShare / 2,
                _topGameGraphicsData.Origin,
                _topGameGraphicsData.RelativeOuterArcEnd);

            CalculateArcSpokeCoordinates();
        }

        private Region MakePetalRegion()
        {
            Region petalRegion = null;
            TopGameGraphicsPath petalPath = MakePetalPath();

            MakeObsoleteOuterAndInnerPath();

            // Create petal region 
            petalRegion = new Region(petalPath.ActualPath);
            storedPetalRegion = new Region(petalPath.ActualPath);

            return petalRegion;
        }

        private TopGameGraphicsPath MakePetalPath()
        {
            TopGameGraphicsPath petalPath = new TopGameGraphicsPath();

            if (_topGameGraphicsData.NumArcSegments > 0)
            {
                petalPath.AddForwardCircularArc(
                    _topGameGraphicsData.ActualArcCentre,
                    _topGameGraphicsData.OuterArcRadius,
                    _topGameGraphicsData.ArcStartAngle);
            }

            if (_topGameGraphicsData.InnerArmLength <= 0)
            {
                // We have to go from start to end instead of from end to start, because the outer arc gets drawn from end to start!
                petalPath.AddLine(_topGameGraphicsData.ActualOuterArcStart, _topGameGraphicsData.ActualOuterArcEnd);
            }
            else
            {
                petalPath.AddLine(_topGameGraphicsData.ActualOuterArcStart, _topGameGraphicsData.ActualInnerArcStart);

                petalPath.AddBackwardCircularArc(
                    _topGameGraphicsData.ActualArcCentre,
                    _topGameGraphicsData.InnerArcRadius,
                    _topGameGraphicsData.ArcStartAngle);

                petalPath.AddLine(_topGameGraphicsData.ActualInnerArcEnd, _topGameGraphicsData.ActualOuterArcEnd);
            }

            return petalPath;
        }

        private void MakeObsoleteOuterAndInnerPath()
        {
            _topGameGraphicsData.OuterPath.Reset();
            // We have to go from end to start instead of from start to end, because the arc gets drawn that way round.
            _topGameGraphicsData.OuterPath.AddLine(_topGameGraphicsData.Origin, _topGameGraphicsData.ActualOuterArcEnd);
            if (_topGameGraphicsData.NumArcSegments > 0)
            {
                // !! The y coordinate of the enclosing rectangle represents the TOP of the shape, not the bottom
                _topGameGraphicsData.OuterArcSquare.X = _topGameGraphicsData.ActualArcCentre.X - (int)Math.Round(_topGameGraphicsData.OuterArcRadius, 0, MidpointRounding.AwayFromZero);
                _topGameGraphicsData.OuterArcSquare.Y = _topGameGraphicsData.ActualArcCentre.Y - (int)Math.Round(_topGameGraphicsData.OuterArcRadius, 0, MidpointRounding.AwayFromZero);
                _topGameGraphicsData.OuterArcSquare.Width = (int)Math.Round(_topGameGraphicsData.OuterArcRadius * 2, 0, MidpointRounding.AwayFromZero);
                _topGameGraphicsData.OuterArcSquare.Height = (int)Math.Round(_topGameGraphicsData.OuterArcRadius * 2, 0, MidpointRounding.AwayFromZero);

                // See AddArcPath for explanation of how arcs are drawn.
                _topGameGraphicsData.OuterPath.AddArcPath(_topGameGraphicsData.OuterArcSquare, (float)_topGameGraphicsData.ArcStartAngle, (float)180);
            }
            else
            {
                _topGameGraphicsData.OuterPath.AddLine(_topGameGraphicsData.ActualOuterArcStart, _topGameGraphicsData.ActualOuterArcEnd);
            }
            _topGameGraphicsData.OuterPath.AddLine(_topGameGraphicsData.ActualOuterArcStart, _topGameGraphicsData.Origin);

            if (_topGameGraphicsData.InnerArmLength > 0)
            {
                _topGameGraphicsData.InnerPath.Reset();
                // We have to go from end to start instead of from start to end, because the arc gets drawn that way round.
                _topGameGraphicsData.InnerPath.AddLine(_topGameGraphicsData.ActualInnerPetalSource, _topGameGraphicsData.ActualInnerArcEnd);

                // !! The y coordinate of the enclosing rectangle represents the TOP of the shape, not the bottom
                _topGameGraphicsData.InnerArcSquare.X = _topGameGraphicsData.ActualArcCentre.X - (int)Math.Round(_topGameGraphicsData.InnerArcRadius, 0, MidpointRounding.AwayFromZero);
                _topGameGraphicsData.InnerArcSquare.Y = _topGameGraphicsData.ActualArcCentre.Y - (int)Math.Round(_topGameGraphicsData.InnerArcRadius, 0, MidpointRounding.AwayFromZero);
                _topGameGraphicsData.InnerArcSquare.Width = (int)Math.Round(_topGameGraphicsData.InnerArcRadius * 2, 0, MidpointRounding.AwayFromZero);
                _topGameGraphicsData.InnerArcSquare.Height = (int)Math.Round(_topGameGraphicsData.InnerArcRadius * 2, 0, MidpointRounding.AwayFromZero);

                // See AddArcPath for explanation of how arcs are drawn.
                _topGameGraphicsData.InnerPath.AddArcPath(_topGameGraphicsData.InnerArcSquare, (float)_topGameGraphicsData.ArcStartAngle, (float)180);
                _topGameGraphicsData.InnerPath.AddLine(_topGameGraphicsData.ActualInnerArcStart, _topGameGraphicsData.ActualInnerPetalSource);
            }
        }

        private void CalculateArmSegmentLength()
        {
            _topGameGraphicsData.ArmSegmentLength = TopGameConstants.ConstantSegmentLength;
            GrowSegmentLengthByALittleToAccountForExtraArcSegments();

            double potentialOuterArmLength = (_topGameGraphicsData.NumArmSegments + 1) * _topGameGraphicsData.ArmSegmentLength;
            if (potentialOuterArmLength > MaximumArmLengthWhichFitsInFrame())
            {
                _topGameGraphicsData.ArmSegmentLength = MaximumArmLengthWhichFitsInFrame() / _topGameGraphicsData.NumArmSegments;
                ShrinkSegmentLengthByALittleToAccountForExtraArcSegments();
            }
        }

        private void GrowSegmentLengthByALittleToAccountForExtraArcSegments()
        {
            double segmentAddition = ArcWillExist() ? NumSegmentsContainedInArmsAndArc().LeftoverAfterDividedBy3() : 0;

            _topGameGraphicsData.ArmSegmentLength = _topGameGraphicsData.ArmSegmentLength + (TopGameConstants.SegmentGrowthRatio * segmentAddition);
        }

        private void ShrinkSegmentLengthByALittleToAccountForExtraArcSegments()
        {
            double segmentAddition = ArcWillExist() ? NumSegmentsContainedInArmsAndArc().LeftoverAfterDividedBy3() : 0;

            _topGameGraphicsData.ArmSegmentLength = _topGameGraphicsData.ArmSegmentLength
                - (TopGameConstants.SegmentGrowthRatio * (2 - segmentAddition));
        }

        private int MaximumArmLengthWhichFitsInFrame()
        {
            return _topGameGraphicsData.Origin.Y - 70;
        }

        private bool ArcWillExist()
        {
            return _topGameGraphicsData.NumTotalSegments > 2;
        }

        private int NumSegmentsContainedInArmsAndArc()
        {
            return _topGameGraphicsData.NumTotalSegments - 2;
        }

        private void ClearAllArmDivisions()
        {
            if (_topGameGraphicsData.StartArmDivisionStarts.Points.Any())
            {
                _topGameGraphicsData.StartArmDivisionStarts.Points.Clear();
            }
            if (_topGameGraphicsData.StartArmDivisionEnds.Points.Any())
            {
                _topGameGraphicsData.StartArmDivisionEnds.Points.Clear();
            }
            if (_topGameGraphicsData.EndArmDivisionStarts.Points.Any())
            {
                _topGameGraphicsData.EndArmDivisionStarts.Points.Clear();
            }
            if (_topGameGraphicsData.EndArmDivisionEnds.Points.Any())
            {
                _topGameGraphicsData.EndArmDivisionEnds.Points.Clear();
            }
        }

        private void AddArcRegion(
            Region petalRegion, 
            TopGamePoint pointA, 
            TopGamePoint pointB, 
            TopGamePoint pointC, 
            GoldenMasterSingleGraphicPass goldenMasterData)
        {
            TopGameGraphicsPath tempRegionPath = new TopGameGraphicsPath();
            tempRegionPath.AddLine(pointA, pointB);
            tempRegionPath.AddLine(pointB, pointC);
            tempRegionPath.AddLine(pointC, pointA);

            using (Region tempRegion = new Region(petalRegion.Clone().GetRegionData()))
            {
                tempRegion.Intersect(tempRegionPath.ActualPath);
                subRegions.Add(new Region(tempRegion.GetRegionData()));
            }

            if (goldenMasterData != null)
            {
                var arcRegion = new GoldenMasterArcRegion();
                arcRegion.Copy(tempRegionPath);
                goldenMasterData.Regions.Add(arcRegion);
            }
        }

        private void AddTriangularRegion(
            TopGamePoint pointA, 
            TopGamePoint pointB, 
            TopGamePoint pointC, 
            GoldenMasterSingleGraphicPass goldenMasterData)
        {
            TopGameGraphicsPath tempRegionPath = new TopGameGraphicsPath();
            tempRegionPath.AddLine(pointA, pointB);
            tempRegionPath.AddLine(pointB, pointC);
            tempRegionPath.AddLine(pointC, pointA);

            subRegions.Add(new Region(tempRegionPath.ActualPath));

            if (goldenMasterData != null)
            {
                var straightEdgedRegion = new GoldenMasterStraightEdgedRegion();
                straightEdgedRegion.Copy(tempRegionPath);
                goldenMasterData.Regions.Add(straightEdgedRegion);
            }
        }

        private void AddParallelogramRegion(
            TopGamePoint pointA, 
            TopGamePoint pointB, 
            TopGamePoint pointC, 
            TopGamePoint pointD, 
            GoldenMasterSingleGraphicPass goldenMasterData)
        {
            TopGameGraphicsPath tempRegionPath = new TopGameGraphicsPath();
            tempRegionPath.AddLine(pointA, pointB);
            tempRegionPath.AddLine(pointB, pointC);
            tempRegionPath.AddLine(pointC, pointD);
            tempRegionPath.AddLine(pointD, pointA);

            subRegions.Add(new Region(tempRegionPath.ActualPath));

            if (goldenMasterData != null)
            {
                var straightEdgedRegion = new GoldenMasterStraightEdgedRegion();
                straightEdgedRegion.Copy(tempRegionPath);
                goldenMasterData.Regions.Add(straightEdgedRegion);
            }
        }

        public void DisplayOtherBits(PaintEventArgs e)
        {
            using (System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red, 3))
            {
                bool bGo = false;

                if (bGo)
                {
                    // Draw outer path
                    e.Graphics.DrawPath(myPen, _topGameGraphicsData.OuterPath.ActualPath);

                    // Draw inner path
                    e.Graphics.DrawPath(myPen, _topGameGraphicsData.InnerPath.ActualPath);

                    // Draw arc spokes
                    for (int iCount = 0; iCount < _topGameGraphicsData.ArcSpokes.Points.Count(); iCount++)
                    {
                        e.Graphics.DrawLine(myPen, _topGameGraphicsData.ActualArcCentre.Point, _topGameGraphicsData.ArcSpokes.Points.ElementAt(iCount).Point);
                    }

                    // Draw the divisions of the start arm.
                    for (int iCount = 0; iCount < _topGameGraphicsData.ArcSpokes.Points.Count(); iCount++)
                    {
                        e.Graphics.DrawLine(myPen, _topGameGraphicsData.StartArmDivisionStarts.Points.ElementAt(iCount).Point, _topGameGraphicsData.StartArmDivisionEnds.Points.ElementAt(iCount).Point);
                    }

                    // Draw the divisions of the end arm.
                    for (int iCount = 0; iCount < _topGameGraphicsData.ArcSpokes.Points.Count(); iCount++)
                    {
                        e.Graphics.DrawLine(myPen, _topGameGraphicsData.EndArmDivisionStarts.Points.ElementAt(iCount).Point, _topGameGraphicsData.EndArmDivisionEnds.Points.ElementAt(iCount).Point);
                    }

                    // Draw outer path first line
                    e.Graphics.DrawLine(myPen, _topGameGraphicsData.Origin.Point, _topGameGraphicsData.ActualOuterArcStart.Point);
                    // Draw outer path arc square
                    e.Graphics.DrawRectangle(myPen, _topGameGraphicsData.OuterArcSquare.Rectangle);
                    // Draw outer path arc 
                    e.Graphics.DrawArc(myPen, _topGameGraphicsData.OuterArcSquare.Rectangle, (float)_topGameGraphicsData.ArcStartAngle, (float)180);
                    // Draw outer path last line
                    e.Graphics.DrawLine(myPen, _topGameGraphicsData.ActualOuterArcEnd.Point, _topGameGraphicsData.Origin.Point);

                    // Draw inner path first line
                    e.Graphics.DrawLine(myPen, _topGameGraphicsData.ActualInnerPetalSource.Point, _topGameGraphicsData.ActualInnerArcStart.Point);
                    // Draw inner path arc square
                    e.Graphics.DrawRectangle(myPen, _topGameGraphicsData.InnerArcSquare.Rectangle);
                    // Draw inner path arc 
                    e.Graphics.DrawArc(myPen, _topGameGraphicsData.InnerArcSquare.Rectangle, (float)_topGameGraphicsData.ArcStartAngle, (float)180);
                    // Draw inner path last line
                    e.Graphics.DrawLine(myPen, _topGameGraphicsData.ActualInnerArcEnd.Point, _topGameGraphicsData.Origin.Point);
                }
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

        public void ReloadColour(int iRegionIndex, System.Drawing.Color newColour)
        {
            regionColours.ElementAt(iRegionIndex).TheColour = newColour;
        }// end function

        /// <summary>
        /// Sort out what proportion of the circle we are getting
        /// </summary>
        /// <param name="maxCentralAngle"></param>
        /// <param name="angleShare"></param>
        public void SetAngles(double maxCentralAngle, double angleShare)
        {
            SetMaxCentralAngle(maxCentralAngle);
            SetConstantBottomAngle(angleShare);

            // Note that CalculateCentralAngle will return different results depending on how many segments there are.
            CalculateCentralAngle(360, 52, false);
        }

        public GoldenMasterSingleGraphicPass GenerateGoldenMasterData(int numPlayersInGame)
        {
            var resultsOfThisCall = new GoldenMasterSingleGraphicPass();
            PrepareActualData(0, resultsOfThisCall);

            // Don't copy vital statistics until after the call to PrepareActualData
            GoldenMasterVitalGraphicStatistics calculatedGraphicStatistics = new GoldenMasterVitalGraphicStatistics();
            calculatedGraphicStatistics.Copy(_topGameGraphicsData);

            // All the collections of regions will be populated during the call to PrepareActualData above
            resultsOfThisCall.VitalGraphicStatistics = calculatedGraphicStatistics;
            resultsOfThisCall.NumCardsInLoop = calculatedGraphicStatistics.NumTotalSegments;
            resultsOfThisCall.NumPlayersInGame = numPlayersInGame;

            return resultsOfThisCall;
        }

        public static double GetMaxCentralAngle(double angleShare, int numHands)
        {
            double maxCentralAngle = angleShare;
            if (numHands == 3)
            {
                // If 2 players (so 3 hands, because of cards played), then 120 degrees is too big, so we reduce the max
                // central angle a bit.
                maxCentralAngle = maxCentralAngle - 20;
            }

            return maxCentralAngle;
        }
    }

// end class
}// end namespace