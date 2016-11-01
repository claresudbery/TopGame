using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
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

        private VitalStatistics _vitalStatistics = new VitalStatistics();

        Region storedPetalRegion; // just for debug purposes: see what the petal region looks like.
        List<Region> subRegions;
        List<ColouredRegion> regionColours;

        public void Dispose()
        {
            _vitalStatistics.OuterPath.ActualPath.Dispose();
            _vitalStatistics.InnerPath.ActualPath.Dispose();
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
            _vitalStatistics.ConstantBottomAngle = 90;
            _vitalStatistics.Origin.X = 215; // 430; // 500, 360
            _vitalStatistics.Origin.Y = 215; // 430; // 400, 360
            _vitalStatistics.ConstantSegmentLength = TopGameConstants.CONSTANT_SEGMENT_LENGTH;
            _vitalStatistics.ConstantCentralSegmentLength = TopGameConstants.CONSTANT_SEGMENT_LENGTH;
            _vitalStatistics.NumTotalCardsInGame = 52;
            _vitalStatistics.NumTotalSegments = 0;
            _vitalStatistics.NumCardsInPlay = 0;
            _vitalStatistics.MaxCentralAngle = 170;
            _vitalStatistics.MinimumAngleApplied = false;
            _vitalStatistics.MaximumAngleApplied = false;
            CalculateCentralAngle(360, _vitalStatistics.NumTotalCardsInGame, false);
            
            subRegions = new List<Region>();
            regionColours = new List<ColouredRegion>();
        }

        public void RotateByAngle(double rotationAngle)
        {
            using (Matrix rotateMatrix = new Matrix())
            {
                rotateMatrix.RotateAt((float)rotationAngle, _vitalStatistics.Origin.Point);
                for (int iCount = 0; iCount < subRegions.Count(); iCount++)
                {
                    subRegions.ElementAt(iCount).Transform(rotateMatrix);
                }
            }
        }

        public void LoadConstants(Point newOrigin, double constantStartAngle, double segmentLength, double centralSegmentLength, int numTotalCards, int numCardsPlayed)
        {
            _vitalStatistics.ConstantBottomAngle = constantStartAngle;
            _vitalStatistics.Origin.X = newOrigin.X;
            _vitalStatistics.Origin.Y = newOrigin.Y;
            _vitalStatistics.ConstantSegmentLength = segmentLength;
            _vitalStatistics.ConstantCentralSegmentLength = centralSegmentLength;
            _vitalStatistics.NumTotalCardsInGame = numTotalCards;
        }

        public void Clear()
        {
            _vitalStatistics.NumTotalSegments = 0;
            _vitalStatistics.CentralAngle = 0;
            DisposeRegions();
            _vitalStatistics.OuterPath.Reset();
            _vitalStatistics.InnerPath.Reset();
            ClearAllArmDivisions();
            if (_vitalStatistics.ArcSpokes.Points.Any())
            {
                _vitalStatistics.ArcSpokes.Points.Clear(); 
            }
            regionColours.Clear();
        }

        public void RemoveTopSegment()
        {
            _vitalStatistics.NumTotalSegments--;
            regionColours.RemoveAt(0);
        }

        public void RemoveBottomSegment()
        {
            _vitalStatistics.NumTotalSegments--;
            regionColours.RemoveAt(regionColours.Count() - 1);
        }

        public void AddSegment()
        {
            _vitalStatistics.NumTotalSegments++;
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
            return _vitalStatistics.NumTotalSegments;
        }

        public void SetMaxCentralAngle(double newMax)
        {
            _vitalStatistics.MaxCentralAngle = newMax;
        }

        public void SetConstantBottomAngle(double newBottom)
        {
            _vitalStatistics.ConstantBottomAngle = newBottom;
        }

        public void SetCentralAngle(double newAngle)
        {
            _vitalStatistics.CentralAngle = newAngle;
        }

        public double GetCentralAngle()
        {
            return _vitalStatistics.CentralAngle;
        }

        public int NumRegions()
        {
            return subRegions.Count();
        }

        public bool IsMinimumAngleApplied()
        {
            return _vitalStatistics.MinimumAngleApplied;
        }

        public bool IsMaximumAngleApplied()
        {
            return _vitalStatistics.MaximumAngleApplied;
        }

        // This one displays rainbow colours
        // If iRegionIndex is -1, all regions are displayed - otherwise just the region indicated by iRegionIndex.
        public void Display(int iRegionIndex, int iColourCycler, PaintEventArgs e, bool bClearGraphics)
        {
            if (_vitalStatistics.NumTotalSegments > 0)
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
                e.Graphics.FillRegion(myBrush, storedPetalRegion);
            }
        }

        // This one displays stored colours
        public void Display(int iRegionIndex, PaintEventArgs e)
        {
            if (_vitalStatistics.NumTotalSegments > 0)
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
                tempRegionPath.AddLine(_vitalStatistics.Origin.Point, _vitalStatistics.EndArmDivisionStarts.Points.ElementAt(0).Point);
                tempRegionPath.AddLine(_vitalStatistics.EndArmDivisionStarts.Points.ElementAt(0).Point, _vitalStatistics.ActualInnerPetalSource.Point);
                tempRegionPath.AddLine(_vitalStatistics.ActualInnerPetalSource.Point, _vitalStatistics.Origin.Point);
                e.Graphics.FillRegion(myBrush, subRegions.ElementAt(subRegions.Count() - 1));
            }
        }

        public double CalculateCentralAngle(double numDegreesAvailable, int numCardsBeingShared, bool bSuppressMinAndMax)
        {
            _vitalStatistics.MinimumAngleApplied = false;
            _vitalStatistics.MaximumAngleApplied = false;
            _vitalStatistics.CentralAngle = ((double)_vitalStatistics.NumTotalSegments / (double)numCardsBeingShared) * numDegreesAvailable;
            if ((_vitalStatistics.CentralAngle > 0) && !bSuppressMinAndMax)
            {
                if (_vitalStatistics.CentralAngle < TopGameConstants.MIN_CENTRAL_ANGLE)
                {
                    _vitalStatistics.CentralAngle = TopGameConstants.MIN_CENTRAL_ANGLE;
                    _vitalStatistics.MinimumAngleApplied = true;
                }
                if (_vitalStatistics.CentralAngle > _vitalStatistics.MaxCentralAngle)
                {
                    _vitalStatistics.CentralAngle = _vitalStatistics.MaxCentralAngle;
                    _vitalStatistics.MaximumAngleApplied = true;
                }
            }

            return _vitalStatistics.CentralAngle;
        }

        public void LoadNewData(double rotationAngle)
        {
            if (_vitalStatistics.NumTotalSegments > 0)
            {
                PrepareActualData(rotationAngle);
            }
        }

        public void PrepareActualData(
            double rotationAngle,
            GoldenMasterSingleGraphicPass goldenMasterData = null)
        {            
            // Need to reinitialise ConstantSegmentLength, in case it was reset in a previous call.
            double segmentAddition = (_vitalStatistics.NumTotalSegments > 2) ? (_vitalStatistics.NumTotalSegments - 2) % 3 : 0;
            _vitalStatistics.ConstantSegmentLength = TopGameConstants.CONSTANT_SEGMENT_LENGTH + (0.7 * segmentAddition);

            _vitalStatistics.NumArmSegments = (_vitalStatistics.NumTotalSegments > 1) ? (_vitalStatistics.NumTotalSegments - 2) / 3 : 0;
            _vitalStatistics.NumArcSegments = (_vitalStatistics.NumTotalSegments > 2) ? _vitalStatistics.NumArmSegments + (_vitalStatistics.NumTotalSegments - 2) % 3 : 0;

            // STARTS ****central segment length change STARTS
            _vitalStatistics.CentralSpokeLength = GetAdjacentSide(_vitalStatistics.ConstantSegmentLength, _vitalStatistics.CentralAngle / 2);
            _vitalStatistics.OuterArmLength = (_vitalStatistics.NumArmSegments + 1) * _vitalStatistics.ConstantSegmentLength;
            // ENDS ****central segment length change ENDS

            if (_vitalStatistics.OuterArmLength > (_vitalStatistics.Origin.Y - 70))
            {
                // Arms are getting too big - won't fit in frame. 
                // So just stop at this max value and change ConstantSegmentLength proportionately.
                _vitalStatistics.ConstantSegmentLength = ((_vitalStatistics.Origin.Y - 70) / _vitalStatistics.NumArmSegments) - (0.7 * 2) + (0.7 * segmentAddition);

                // So now we'll get a new outer arm length, etc
                _vitalStatistics.CentralSpokeLength = GetAdjacentSide(_vitalStatistics.ConstantSegmentLength, _vitalStatistics.CentralAngle / 2);
                _vitalStatistics.OuterArmLength = (_vitalStatistics.NumArmSegments + 1) * _vitalStatistics.ConstantSegmentLength;
            }

            // (inner arm not affected by central segment length)
            _vitalStatistics.InnerArmLength = _vitalStatistics.NumArmSegments * _vitalStatistics.ConstantSegmentLength;

            _vitalStatistics.InnerArcRadius = (_vitalStatistics.InnerArmLength > 0) ? GetOppositeSide(_vitalStatistics.InnerArmLength, _vitalStatistics.CentralAngle / 2) : 0;
            _vitalStatistics.ArcSegmentAngle = (_vitalStatistics.NumArcSegments > 0) ? 180 / _vitalStatistics.NumArcSegments : 0;

            _vitalStatistics.AngleB = 90 - _vitalStatistics.ConstantBottomAngle / 2;
            _vitalStatistics.AngleC = (180 - _vitalStatistics.CentralAngle) / 2;
            _vitalStatistics.ArcStartAngle = 180 - (_vitalStatistics.AngleB + _vitalStatistics.AngleC);

            _vitalStatistics.OriginToArcCentre = GetAdjacentSide(_vitalStatistics.OuterArmLength, _vitalStatistics.CentralAngle / 2);
            _vitalStatistics.RelativeArcCentre.X = GetXFromLineLengthAndTopAngle(_vitalStatistics.OriginToArcCentre, _vitalStatistics.ConstantBottomAngle / 2 + _vitalStatistics.CentralAngle / 2);
            _vitalStatistics.RelativeArcCentre.Y = GetYFromLineLengthAndTopAngle(_vitalStatistics.OriginToArcCentre, _vitalStatistics.ConstantBottomAngle / 2 + _vitalStatistics.CentralAngle / 2);

            _vitalStatistics.OuterArcRadius = GetOppositeSide(_vitalStatistics.OuterArmLength, _vitalStatistics.CentralAngle / 2);
            _vitalStatistics.RelativeInnerPetalSource.X = GetXFromLineLengthAndTopAngle(_vitalStatistics.CentralSpokeLength, _vitalStatistics.ConstantBottomAngle / 2 + _vitalStatistics.CentralAngle / 2);
            _vitalStatistics.RelativeInnerPetalSource.Y = GetYFromLineLengthAndTopAngle(_vitalStatistics.CentralSpokeLength, _vitalStatistics.ConstantBottomAngle / 2 + _vitalStatistics.CentralAngle / 2);
            
            if (_vitalStatistics.InnerArmLength > 0)
            {
                _vitalStatistics.RelativeInnerArcEnd.X = GetXFromLineLengthAndBottomAngle(_vitalStatistics.InnerArmLength, _vitalStatistics.AngleB);
                _vitalStatistics.RelativeInnerArcEnd.Y = GetYFromLineLengthAndBottomAngle(_vitalStatistics.InnerArmLength, _vitalStatistics.AngleB);
                _vitalStatistics.RelativeInnerArcStart.X = GetXFromLineLengthAndTopAngle(_vitalStatistics.InnerArmLength, _vitalStatistics.ConstantBottomAngle / 2 + _vitalStatistics.CentralAngle);
                _vitalStatistics.RelativeInnerArcStart.Y = GetYFromLineLengthAndTopAngle(_vitalStatistics.InnerArmLength, _vitalStatistics.ConstantBottomAngle / 2 + _vitalStatistics.CentralAngle);
            }

            _vitalStatistics.RelativeOuterArcEnd.X = GetXFromLineLengthAndBottomAngle(_vitalStatistics.OuterArmLength, _vitalStatistics.AngleB);
            _vitalStatistics.RelativeOuterArcEnd.Y = GetYFromLineLengthAndBottomAngle(_vitalStatistics.OuterArmLength, _vitalStatistics.AngleB);
            _vitalStatistics.RelativeOuterArcStart.X = GetXFromLineLengthAndTopAngle(_vitalStatistics.OuterArmLength, _vitalStatistics.ConstantBottomAngle / 2 + _vitalStatistics.CentralAngle);
            _vitalStatistics.RelativeOuterArcStart.Y = GetYFromLineLengthAndTopAngle(_vitalStatistics.OuterArmLength, _vitalStatistics.ConstantBottomAngle / 2 + _vitalStatistics.CentralAngle);

            _vitalStatistics.ActualArcCentre.X = _vitalStatistics.Origin.X + _vitalStatistics.RelativeArcCentre.X;
            _vitalStatistics.ActualArcCentre.Y = _vitalStatistics.Origin.Y + _vitalStatistics.RelativeArcCentre.Y;
            _vitalStatistics.ActualInnerPetalSource.X = _vitalStatistics.Origin.X + _vitalStatistics.RelativeInnerPetalSource.X;
            _vitalStatistics.ActualInnerPetalSource.Y = _vitalStatistics.Origin.Y + _vitalStatistics.RelativeInnerPetalSource.Y;
            _vitalStatistics.ActualOuterArcStart.X = _vitalStatistics.Origin.X + _vitalStatistics.RelativeOuterArcStart.X;
            _vitalStatistics.ActualOuterArcStart.Y = _vitalStatistics.Origin.Y + _vitalStatistics.RelativeOuterArcStart.Y;
            _vitalStatistics.ActualOuterArcEnd.X = _vitalStatistics.Origin.X + _vitalStatistics.RelativeOuterArcEnd.X;
            _vitalStatistics.ActualOuterArcEnd.Y = _vitalStatistics.Origin.Y + _vitalStatistics.RelativeOuterArcEnd.Y;

            if (_vitalStatistics.InnerArmLength > 0)
            {
                // !! Caution !! The inner arc values are relative to the inner petal source, NOT to the Origin!
                _vitalStatistics.ActualInnerArcStart.X = _vitalStatistics.ActualInnerPetalSource.X + _vitalStatistics.RelativeInnerArcStart.X;
                _vitalStatistics.ActualInnerArcStart.Y = _vitalStatistics.ActualInnerPetalSource.Y + _vitalStatistics.RelativeInnerArcStart.Y;
                _vitalStatistics.ActualInnerArcEnd.X = _vitalStatistics.ActualInnerPetalSource.X + _vitalStatistics.RelativeInnerArcEnd.X;
                _vitalStatistics.ActualInnerArcEnd.Y = _vitalStatistics.ActualInnerPetalSource.Y + _vitalStatistics.RelativeInnerArcEnd.Y;
            }

            _vitalStatistics.OuterPath.Reset();
            _vitalStatistics.OuterPath.AddLine(_vitalStatistics.Origin, _vitalStatistics.ActualOuterArcStart);
            if (_vitalStatistics.NumArcSegments > 0)
            {
                // !! The y coordinate of the enclosing rectangle represents the TOP of the shape, not the bottom
                _vitalStatistics.OuterArcSquare.X = _vitalStatistics.ActualArcCentre.X - (int)Math.Round(_vitalStatistics.OuterArcRadius, 0, MidpointRounding.AwayFromZero);
                _vitalStatistics.OuterArcSquare.Y = _vitalStatistics.ActualArcCentre.Y - (int)Math.Round(_vitalStatistics.OuterArcRadius, 0, MidpointRounding.AwayFromZero);
                _vitalStatistics.OuterArcSquare.Width = (int)Math.Round(_vitalStatistics.OuterArcRadius * 2, 0, MidpointRounding.AwayFromZero);
                _vitalStatistics.OuterArcSquare.Height = (int)Math.Round(_vitalStatistics.OuterArcRadius * 2, 0, MidpointRounding.AwayFromZero);

                _vitalStatistics.OuterPath.AddArcPath(_vitalStatistics.OuterArcSquare, (float)_vitalStatistics.ArcStartAngle, (float)180);
            }
            else
            {
                _vitalStatistics.OuterPath.AddLine(_vitalStatistics.ActualOuterArcStart, _vitalStatistics.ActualOuterArcEnd);
            }
            _vitalStatistics.OuterPath.AddLine(_vitalStatistics.ActualOuterArcEnd, _vitalStatistics.Origin);

            if (_vitalStatistics.InnerArmLength > 0)
            {
                _vitalStatistics.InnerPath.Reset();
                _vitalStatistics.InnerPath.AddLine(_vitalStatistics.ActualInnerPetalSource, _vitalStatistics.ActualInnerArcStart);

                // !! The y coordinate of the enclosing rectangle represents the TOP of the shape, not the bottom
                _vitalStatistics.InnerArcSquare.X = _vitalStatistics.ActualArcCentre.X - (int)Math.Round(_vitalStatistics.InnerArcRadius, 0, MidpointRounding.AwayFromZero);
                _vitalStatistics.InnerArcSquare.Y = _vitalStatistics.ActualArcCentre.Y - (int)Math.Round(_vitalStatistics.InnerArcRadius, 0, MidpointRounding.AwayFromZero);
                _vitalStatistics.InnerArcSquare.Width = (int)Math.Round(_vitalStatistics.InnerArcRadius * 2, 0, MidpointRounding.AwayFromZero);
                _vitalStatistics.InnerArcSquare.Height = (int)Math.Round(_vitalStatistics.InnerArcRadius * 2, 0, MidpointRounding.AwayFromZero);

                _vitalStatistics.InnerPath.AddArcPath(_vitalStatistics.InnerArcSquare, (float)_vitalStatistics.ArcStartAngle, (float)180);
                _vitalStatistics.InnerPath.AddLine(_vitalStatistics.ActualInnerArcEnd, _vitalStatistics.ActualInnerPetalSource);
            }

            // Create petal region 
            using (Region petalRegion = new Region(_vitalStatistics.OuterPath.ActualPath))
            {
                storedPetalRegion = new Region(_vitalStatistics.OuterPath.ActualPath);
                if (_vitalStatistics.InnerArmLength > 0)
                {
                    storedPetalRegion.Exclude(_vitalStatistics.InnerPath.ActualPath);
                    petalRegion.Exclude(_vitalStatistics.InnerPath.ActualPath);
                }

                // Create NumArmSegments + 1 divisions of the start arm and end arm (including the centre).
                ClearAllArmDivisions();
                // always create the first division, even if the arms have no segments.
                double fractionalMultiplier = 1.0 / ((double)_vitalStatistics.NumArmSegments + 1.0);

                // STARTS ****central segment length change STARTS
                _vitalStatistics.StartArmDivisionStarts.Points.Add(MoveAlongLineByFraction(_vitalStatistics.Origin, _vitalStatistics.ActualOuterArcStart, fractionalMultiplier));
                _vitalStatistics.EndArmDivisionStarts.Points.Add(MoveAlongLineByFraction(_vitalStatistics.Origin, _vitalStatistics.ActualOuterArcEnd, fractionalMultiplier));
                // ENDS ****central segment length change ENDS

                _vitalStatistics.StartArmDivisionEnds.Points.Add(_vitalStatistics.ActualInnerPetalSource);
                _vitalStatistics.EndArmDivisionEnds.Points.Add(_vitalStatistics.ActualInnerPetalSource);

                // now create the rest, if necessary
                if (_vitalStatistics.NumArmSegments > 1) // If there's only one, there's no dividers necessary
                {
                    for (int iCount = 2; iCount <= _vitalStatistics.NumArmSegments; iCount++)
                    {
                        // STARTS ****central segment length change STARTS
                        double outerFractionalMultiplier = (double)(iCount) / ((double)_vitalStatistics.NumArmSegments + 1.0); // eg 4 segments: 2/5, 3/5, 4/5
                        _vitalStatistics.StartArmDivisionStarts.Points.Add(MoveAlongLineByFraction(_vitalStatistics.Origin, _vitalStatistics.ActualOuterArcStart, outerFractionalMultiplier));
                        _vitalStatistics.EndArmDivisionStarts.Points.Add(MoveAlongLineByFraction(_vitalStatistics.Origin, _vitalStatistics.ActualOuterArcEnd, outerFractionalMultiplier));
                        // ENDS ****central segment length change ENDS

                        double innerFractionalMultiplier = ((double)(iCount) - 1.0) / (double)(_vitalStatistics.NumArmSegments); // eg 4 segments: 1/4, 2/4, 3/4
                        _vitalStatistics.StartArmDivisionEnds.Points.Add(MoveAlongLineByFraction(_vitalStatistics.ActualInnerPetalSource, _vitalStatistics.ActualInnerArcStart, innerFractionalMultiplier));
                        _vitalStatistics.EndArmDivisionEnds.Points.Add(MoveAlongLineByFraction(_vitalStatistics.ActualInnerPetalSource, _vitalStatistics.ActualInnerArcEnd, innerFractionalMultiplier));
                    }
                }

                // Create NumArcSegments divisions of the arc
                _vitalStatistics.ArcSpokes.Points.Clear();
                if (_vitalStatistics.NumArcSegments > 1)
                {
                    TopGamePoint currentArcSpoke = GetEndPointOfRotatedLine(_vitalStatistics.OuterArcRadius, _vitalStatistics.ActualArcCentre, _vitalStatistics.ActualOuterArcStart, _vitalStatistics.ArcSegmentAngle);
                    currentArcSpoke.X = _vitalStatistics.ActualArcCentre.X + currentArcSpoke.X;
                    currentArcSpoke.Y = _vitalStatistics.ActualArcCentre.Y + currentArcSpoke.Y;
                    _vitalStatistics.ArcSpokes.Points.Add(currentArcSpoke);
                    TopGamePoint previousArcSpoke = currentArcSpoke;
                    for (int iCount = 1; iCount < _vitalStatistics.NumArcSegments - 1; iCount++)
                    {
                        currentArcSpoke = GetEndPointOfRotatedLine(_vitalStatistics.OuterArcRadius, _vitalStatistics.ActualArcCentre, previousArcSpoke, _vitalStatistics.ArcSegmentAngle);
                        currentArcSpoke.X = _vitalStatistics.ActualArcCentre.X + currentArcSpoke.X;
                        currentArcSpoke.Y = _vitalStatistics.ActualArcCentre.Y + currentArcSpoke.Y;
                        _vitalStatistics.ArcSpokes.Points.Add(currentArcSpoke);
                        previousArcSpoke = currentArcSpoke;
                    }
                }

                // Create sub-regions
                DisposeRegions(); 
                Debug.Assert(subRegions.Count() == 0, "There are some subregions left after disposing of them!");

                // The sub-regions are all the areas that get coloured in: Each region represents an individual card.
                // They are displayed in three sections, all of which added together look a bit like a petal:
                // 1) The straight "start-arm", basically made out of two parallel lines (but tapering to a triangular point at the centre)
                // 2) The curved "arc" which joins the two arms together
                // 3) The straight "end-arm", basically made out of two parallel lines (but tapering to a triangular point at the centre)

                // Start with the central region of the start-arm - this is the triangular bit that goes from the centre out to the start of the parallel-lines part of the arm
                // (after the central triangle, all the regions in the start-arm are parallelograms)
                if (_vitalStatistics.NumTotalSegments > 1)
                {
                    // start-arm central region
                    AddTriangularRegion(
                        _vitalStatistics.Origin,
                        _vitalStatistics.StartArmDivisionStarts.Points.ElementAt(0),
                        _vitalStatistics.ActualInnerPetalSource,
                                goldenMasterData);
                }
                else
                {
                    // just one central region
                    AddTriangularRegion(
                        _vitalStatistics.Origin,
                        _vitalStatistics.StartArmDivisionStarts.Points.ElementAt(0),
                        _vitalStatistics.EndArmDivisionStarts.Points.ElementAt(0),
                                goldenMasterData);
                }

                if (_vitalStatistics.NumArmSegments > 0)
                {
                    // all the divisions of the start arm
                    if (_vitalStatistics.NumArmSegments > 1)
                    {
                        for (int iCount = 0; iCount < _vitalStatistics.NumArmSegments - 1; iCount++)
                        {
                            AddParallelogramRegion(
                                _vitalStatistics.StartArmDivisionStarts.Points.ElementAt(iCount),
                                _vitalStatistics.StartArmDivisionEnds.Points.ElementAt(iCount),
                                _vitalStatistics.StartArmDivisionEnds.Points.ElementAt(iCount + 1),
                                _vitalStatistics.StartArmDivisionStarts.Points.ElementAt(iCount + 1),
                                goldenMasterData
                                );
                        }
                    }
                    // the last one hooks up to the arc.
                    AddParallelogramRegion(
                        _vitalStatistics.StartArmDivisionStarts.Points.ElementAt(_vitalStatistics.NumArmSegments - 1),
                        _vitalStatistics.StartArmDivisionEnds.Points.ElementAt(_vitalStatistics.NumArmSegments - 1),
                        _vitalStatistics.ActualInnerArcStart,
                        _vitalStatistics.ActualOuterArcStart,
                                goldenMasterData
                        );
                }

                // the divisions of the arc (sounds biblical!)
                if (_vitalStatistics.NumArcSegments > 0)
                {
                    if (_vitalStatistics.ArcSpokes.Points.Count() == 0)
                    {
                        // the division is the arc itself.
                        TopGameGraphicsPath tempRegionPath = new TopGameGraphicsPath();
                        tempRegionPath.AddArcPath(_vitalStatistics.OuterArcSquare, (float)_vitalStatistics.ArcStartAngle, (float)180);
                        tempRegionPath.AddLine(_vitalStatistics.ActualOuterArcStart, _vitalStatistics.ActualOuterArcEnd);
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
                            _vitalStatistics.ActualArcCentre,
                            MoveAlongLineByFraction(_vitalStatistics.ActualArcCentre, _vitalStatistics.ActualOuterArcStart, 1.5),
                            MoveAlongLineByFraction(_vitalStatistics.ActualArcCentre, _vitalStatistics.ArcSpokes.Points.ElementAt(0), 1.5),
                                goldenMasterData);

                        // middle arc divisions
                        for (int iCount = 1; iCount < _vitalStatistics.ArcSpokes.Points.Count(); iCount++)
                        {
                            AddArcRegion(
                                petalRegion,
                                _vitalStatistics.ActualArcCentre,
                                MoveAlongLineByFraction(_vitalStatistics.ActualArcCentre, _vitalStatistics.ArcSpokes.Points.ElementAt(iCount - 1), 1.5),
                                MoveAlongLineByFraction(_vitalStatistics.ActualArcCentre, _vitalStatistics.ArcSpokes.Points.ElementAt(iCount), 1.5),
                                goldenMasterData);
                        }

                        // last arc division
                        AddArcRegion(
                            petalRegion,
                            _vitalStatistics.ActualArcCentre,
                            MoveAlongLineByFraction(_vitalStatistics.ActualArcCentre, _vitalStatistics.ArcSpokes.Points.ElementAt(_vitalStatistics.ArcSpokes.Points.Count() - 1), 1.5),
                            MoveAlongLineByFraction(_vitalStatistics.ActualArcCentre, _vitalStatistics.ActualOuterArcEnd, 1.5),
                                goldenMasterData);
                    }
                }

                if (_vitalStatistics.NumArmSegments > 0)
                {
                    // all the divisions of the end arm (in reverse)
                    AddParallelogramRegion(
                        _vitalStatistics.EndArmDivisionStarts.Points.ElementAt(_vitalStatistics.NumArmSegments - 1),
                        _vitalStatistics.EndArmDivisionEnds.Points.ElementAt(_vitalStatistics.NumArmSegments - 1),
                        _vitalStatistics.ActualInnerArcEnd,
                        _vitalStatistics.ActualOuterArcEnd,
                                goldenMasterData
                        );

                    if (_vitalStatistics.NumArmSegments > 1)
                    {
                        for (int iCount = _vitalStatistics.NumArmSegments - 1; iCount > 0; iCount--)
                        {
                            AddParallelogramRegion(
                                _vitalStatistics.EndArmDivisionStarts.Points.ElementAt(iCount),
                                _vitalStatistics.EndArmDivisionEnds.Points.ElementAt(iCount),
                                _vitalStatistics.EndArmDivisionEnds.Points.ElementAt(iCount - 1),
                                _vitalStatistics.EndArmDivisionStarts.Points.ElementAt(iCount - 1),
                                goldenMasterData
                                );
                        }
                    }
                }

                // End with the central region of the end-arm - this is the triangular bit that goes from the centre out to the end of the parallel-lines part of the arm
                // (apart from the central triangle, all the regions in the end-arm are parallelograms)
                if (_vitalStatistics.NumTotalSegments > 1)
                {
                    // end-arm central region
                    AddTriangularRegion(
                        _vitalStatistics.Origin,
                        _vitalStatistics.EndArmDivisionStarts.Points.ElementAt(0),
                        _vitalStatistics.ActualInnerPetalSource,
                                goldenMasterData
                        );
                }
            }

            Debug.Assert(_vitalStatistics.NumTotalSegments == subRegions.Count(), "Region count",
                                                "Number of regions is not num total segments");

            if (rotationAngle > 0)
            {
                RotateByAngle(rotationAngle);
            }
        }

        private void ClearAllArmDivisions()
        {
            if (_vitalStatistics.StartArmDivisionStarts.Points.Any())
            {
                _vitalStatistics.StartArmDivisionStarts.Points.Clear();
            }
            if (_vitalStatistics.StartArmDivisionEnds.Points.Any())
            {
                _vitalStatistics.StartArmDivisionEnds.Points.Clear();
            }
            if (_vitalStatistics.EndArmDivisionStarts.Points.Any())
            {
                _vitalStatistics.EndArmDivisionStarts.Points.Clear();
            }
            if (_vitalStatistics.EndArmDivisionEnds.Points.Any())
            {
                _vitalStatistics.EndArmDivisionEnds.Points.Clear();
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
                    e.Graphics.DrawPath(myPen, _vitalStatistics.OuterPath.ActualPath);

                    // Draw inner path
                    e.Graphics.DrawPath(myPen, _vitalStatistics.InnerPath.ActualPath);

                    // Draw arc spokes
                    for (int iCount = 0; iCount < _vitalStatistics.ArcSpokes.Points.Count(); iCount++)
                    {
                        e.Graphics.DrawLine(myPen, _vitalStatistics.ActualArcCentre.Point, _vitalStatistics.ArcSpokes.Points.ElementAt(iCount).Point);
                    }

                    // Draw the divisions of the start arm.
                    for (int iCount = 0; iCount < _vitalStatistics.ArcSpokes.Points.Count(); iCount++)
                    {
                        e.Graphics.DrawLine(myPen, _vitalStatistics.StartArmDivisionStarts.Points.ElementAt(iCount).Point, _vitalStatistics.StartArmDivisionEnds.Points.ElementAt(iCount).Point);
                    }

                    // Draw the divisions of the end arm.
                    for (int iCount = 0; iCount < _vitalStatistics.ArcSpokes.Points.Count(); iCount++)
                    {
                        e.Graphics.DrawLine(myPen, _vitalStatistics.EndArmDivisionStarts.Points.ElementAt(iCount).Point, _vitalStatistics.EndArmDivisionEnds.Points.ElementAt(iCount).Point);
                    }

                    // Draw outer path first line
                    e.Graphics.DrawLine(myPen, _vitalStatistics.Origin.Point, _vitalStatistics.ActualOuterArcStart.Point);
                    // Draw outer path arc square
                    e.Graphics.DrawRectangle(myPen, _vitalStatistics.OuterArcSquare.Rectangle);
                    // Draw outer path arc 
                    e.Graphics.DrawArc(myPen, _vitalStatistics.OuterArcSquare.Rectangle, (float)_vitalStatistics.ArcStartAngle, (float)180);
                    // Draw outer path last line
                    e.Graphics.DrawLine(myPen, _vitalStatistics.ActualOuterArcEnd.Point, _vitalStatistics.Origin.Point);

                    // Draw inner path first line
                    e.Graphics.DrawLine(myPen, _vitalStatistics.ActualInnerPetalSource.Point, _vitalStatistics.ActualInnerArcStart.Point);
                    // Draw inner path arc square
                    e.Graphics.DrawRectangle(myPen, _vitalStatistics.InnerArcSquare.Rectangle);
                    // Draw inner path arc 
                    e.Graphics.DrawArc(myPen, _vitalStatistics.InnerArcSquare.Rectangle, (float)_vitalStatistics.ArcStartAngle, (float)180);
                    // Draw inner path last line
                    e.Graphics.DrawLine(myPen, _vitalStatistics.ActualInnerArcEnd.Point, _vitalStatistics.Origin.Point);
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

        double GetHypotenuseFromAdjacent(double adjacentSide, double adjacentAngle)
        {
            return adjacentSide / SafeCos(adjacentAngle);
        }

        int GetXFromLineLengthAndTopAngle(double lineLength, double topAngle)
        {
            return -(int)Math.Round(lineLength * SafeSin(topAngle), 0, MidpointRounding.AwayFromZero);
        }

        int GetYFromLineLengthAndTopAngle(double lineLength, double topAngle)
        {
            return (int)Math.Round(lineLength * SafeCos(topAngle), 0, MidpointRounding.AwayFromZero);
        }

        int GetXFromLineLengthAndBottomAngle(double lineLength, double bottomAngle)
        {
            return -(int)Math.Round(lineLength * SafeCos(bottomAngle), 0, MidpointRounding.AwayFromZero);
        }

        int GetYFromLineLengthAndBottomAngle(double lineLength, double bottomAngle)
        {
            return (int)Math.Round(lineLength * SafeSin(bottomAngle), 0, MidpointRounding.AwayFromZero);
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
            calculatedGraphicStatistics.Copy(_vitalStatistics);

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