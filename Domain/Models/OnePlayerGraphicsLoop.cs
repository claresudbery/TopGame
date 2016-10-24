using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using TopGameWindowsApp;

namespace Domain.Models
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

        private VitalStatistics _vitalStatistics = new VitalStatistics(true);

        Region storedPetalRegion;
        //List<GraphicsPath> regionPaths;
        List<Region> subRegions;
        List<TopGameRegion> graphicsIndependentRegions;
        List<ColouredRegion> regionColours;

        public void Dispose()
        {
            _vitalStatistics.outerPath.ActualPath.Dispose();
            _vitalStatistics.innerPath.ActualPath.Dispose();
            DisposeRegions();
            DisposeRegionPaths();
        }

        private void DisposeRegions()
        {
            graphicsIndependentRegions.Clear();
            for (int iCount = subRegions.Count() - 1; iCount >= 0; iCount--)
            {
                subRegions.ElementAt(iCount).Dispose();
                subRegions.RemoveAt(iCount);
                //MessageBox.Show("There are now " + subRegions.Count() + " regions left");
            }
        }

        private void DisposeRegionPaths()
        {
            //for (int iCount = regionPaths.Count() - 1; iCount >= 0; iCount--)
            //{
            //    regionPaths.ElementAt(iCount).Dispose();
            //    regionPaths.RemoveAt(iCount);
            //    //MessageBox.Show("There are now " + regionPaths.Count() + " region paths left");
            //}
        }

        public OnePlayerGraphicsLoop()
        {
            _vitalStatistics.constantBottomAngle = 90;
            _vitalStatistics.origin.X = 215; // 430; // 500, 360
            _vitalStatistics.origin.Y = 215; // 430; // 400, 360
            _vitalStatistics.constantSegmentLength = TopGameConstants.CONSTANT_SEGMENT_LENGTH;
            _vitalStatistics.constantCentralSegmentLength = TopGameConstants.CONSTANT_SEGMENT_LENGTH;
            _vitalStatistics.numTotalCardsInGame = 52;
            _vitalStatistics.numTotalSegments = 0;
            _vitalStatistics.numCardsInPlay = 0;
            _vitalStatistics.maxCentralAngle = 170;
            _vitalStatistics.bMinimumAngleApplied = false;
            _vitalStatistics.bMaximumAngleApplied = false;
            CalculateCentralAngle(360, _vitalStatistics.numTotalCardsInGame, false);

            graphicsIndependentRegions = new List<TopGameRegion>();
            subRegions = new List<Region>();
            //regionPaths = new List<GraphicsPath>();
            regionColours = new List<ColouredRegion>();
        }

        public void RotateByAngle(double rotationAngle)
        {
            using (Matrix rotateMatrix = new Matrix())
            {
                rotateMatrix.RotateAt((float)rotationAngle, _vitalStatistics.origin.Point);
                for (int iCount = 0; iCount < subRegions.Count(); iCount++)
                {
                    subRegions.ElementAt(iCount).Transform(rotateMatrix);
                }
                //for (int iCount = 0; iCount < regionPaths.Count(); iCount++)
                //{
                //    regionPaths.ElementAt(iCount).Transform(rotateMatrix);
                //}
            }
        }

        public void LoadConstants(Point newOrigin, double constantStartAngle, double segmentLength, double centralSegmentLength, int numTotalCards, int numCardsPlayed)
        {
            _vitalStatistics.constantBottomAngle = constantStartAngle;
            _vitalStatistics.origin.X = newOrigin.X;
            _vitalStatistics.origin.Y = newOrigin.Y;
            _vitalStatistics.constantSegmentLength = segmentLength;
            _vitalStatistics.constantCentralSegmentLength = centralSegmentLength;
            _vitalStatistics.numTotalCardsInGame = numTotalCards;
        }

        public void Clear()
        {
            _vitalStatistics.numTotalSegments = 0;
            _vitalStatistics.centralAngle = 0;
            DisposeRegions();
            _vitalStatistics.outerPath.Reset();
            _vitalStatistics.innerPath.Reset();
            ClearAllArmDivisions();
            if (_vitalStatistics.arcSpokes.Points.Any())
            {
                _vitalStatistics.arcSpokes.Points.Clear(); //RemoveRange(_vitalStatistics.arcSpokes.Points);
            }
            regionColours.Clear();
        }

        public void RemoveTopSegment()
        {
            _vitalStatistics.numTotalSegments--;
            regionColours.RemoveAt(0);
        }

        public void RemoveBottomSegment()
        {
            _vitalStatistics.numTotalSegments--;
            regionColours.RemoveAt(regionColours.Count() - 1);
        }

        public void AddSegment()
        {
            _vitalStatistics.numTotalSegments++;
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
            return _vitalStatistics.numTotalSegments;
        }

        public void SetMaxCentralAngle(double newMax)
        {
            _vitalStatistics.maxCentralAngle = newMax;
        }

        public void SetConstantBottomAngle(double newBottom)
        {
            _vitalStatistics.constantBottomAngle = newBottom;
        }

        public void SetCentralAngle(double newAngle)
        {
            _vitalStatistics.centralAngle = newAngle;
        }

        public double GetCentralAngle()
        {
            return _vitalStatistics.centralAngle;
        }

        public int NumRegions()
        {
            return subRegions.Count();
        }

        public bool IsMinimumAngleApplied()
        {
            return _vitalStatistics.bMinimumAngleApplied;
        }

        public bool IsMaximumAngleApplied()
        {
            return _vitalStatistics.bMaximumAngleApplied;
        }

        // This one displays rainbow colours
        // If iRegionIndex is -1, all regions are displayed - otherwise just the region indicated by iRegionIndex.
        public void Display(int iRegionIndex, int iColourCycler, PaintEventArgs e, bool bClearGraphics)
        {
            if (_vitalStatistics.numTotalSegments > 0)
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
            if (_vitalStatistics.numTotalSegments > 0)
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
                        //e.Graphics.DrawPath(myPen, regionPaths.ElementAt(i));
                    }
                    //for (int i = 0; i < regionPaths.Count(); i++)
                    //{
                    //    //e.Graphics.DrawPath(myPen, regionPaths.ElementAt(i));
                    //}
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
                tempRegionPath.AddLine(_vitalStatistics.origin.Point, _vitalStatistics.endArmDivisionStarts.Points.ElementAt(0).Point);
                tempRegionPath.AddLine(_vitalStatistics.endArmDivisionStarts.Points.ElementAt(0).Point, _vitalStatistics.actualInnerPetalSource.Point);
                tempRegionPath.AddLine(_vitalStatistics.actualInnerPetalSource.Point, _vitalStatistics.origin.Point);
                //e.Graphics.DrawPath(myPen, tempRegionPath);
                //e.Graphics.FillRegion(myBrush, new Region(tempRegionPath));
                //GraphicsPath tempRegionPath2 = new GraphicsPath(subRegions.ElementAt(subRegions.Count() - 1).GetRegionData());
                e.Graphics.FillRegion(myBrush, subRegions.ElementAt(subRegions.Count() - 1));
                // subRegions.ElementAt(subRegions.Count() - 1)
            }
        }

        public double CalculateCentralAngle(double numDegreesAvailable, int numCardsBeingShared, bool bSuppressMinAndMax)
        {
            _vitalStatistics.bMinimumAngleApplied = false;
            _vitalStatistics.bMaximumAngleApplied = false;
            _vitalStatistics.centralAngle = ((double)_vitalStatistics.numTotalSegments / (double)numCardsBeingShared) * numDegreesAvailable;
            if ((_vitalStatistics.centralAngle > 0) && !bSuppressMinAndMax)
            {
                if (_vitalStatistics.centralAngle < TopGameConstants.MIN_CENTRAL_ANGLE)
                {
                    _vitalStatistics.centralAngle = TopGameConstants.MIN_CENTRAL_ANGLE;
                    _vitalStatistics.bMinimumAngleApplied = true;
                }
                if (_vitalStatistics.centralAngle > _vitalStatistics.maxCentralAngle)
                {
                    _vitalStatistics.centralAngle = _vitalStatistics.maxCentralAngle;
                    _vitalStatistics.bMaximumAngleApplied = true;
                }
            }

            return _vitalStatistics.centralAngle;
        }

        public void LoadNewData(double rotationAngle)
        {
            if (_vitalStatistics.numTotalSegments > 0)
            {
                PrepareActualData(rotationAngle);
            }
        }

        public void PrepareActualData(double rotationAngle, ICollection<GoldenMasterRegion> goldenMasterRegions = null)
        {            
            // Need to reinitialise constantSegmentLength, in case it was reset in a previous call.
            double segmentAddition = (_vitalStatistics.numTotalSegments > 2) ? (_vitalStatistics.numTotalSegments - 2) % 3 : 0;
            _vitalStatistics.constantSegmentLength = TopGameConstants.CONSTANT_SEGMENT_LENGTH + (0.7 * segmentAddition);

            _vitalStatistics.numArmSegments = (_vitalStatistics.numTotalSegments > 1) ? (_vitalStatistics.numTotalSegments - 2) / 3 : 0;
            _vitalStatistics.numArcSegments = (_vitalStatistics.numTotalSegments > 2) ? _vitalStatistics.numArmSegments + (_vitalStatistics.numTotalSegments - 2) % 3 : 0;

            /*if (numTotalSegments > 36)
            {
                // We can't cope with getting any bigger than this, so we just stay this size, no matter what.
                constantSegmentLength = (CONSTANT_SEGMENT_LENGTH * 36) / numTotalSegments;
            }*/

            // STARTS ****central segment length change STARTS
            _vitalStatistics.centralSpokeLength = GetAdjacentSide(_vitalStatistics.constantSegmentLength, _vitalStatistics.centralAngle / 2);
            _vitalStatistics.outerArmLength = (_vitalStatistics.numArmSegments + 1) * _vitalStatistics.constantSegmentLength;
            //centralSpokeLength = GetAdjacentSide(constantCentralSegmentLength, centralAngle / 2);
            //outerArmLength = (numArmSegments * constantSegmentLength) + constantCentralSegmentLength;
            // ENDS ****central segment length change ENDS

            if (_vitalStatistics.outerArmLength > (_vitalStatistics.origin.Y - 70))
            {
                // Arms are getting too big - won't fit in frame. 
                // So just stop at this max value and change constantSegmentLength proportionately.
                _vitalStatistics.constantSegmentLength = ((_vitalStatistics.origin.Y - 70) / _vitalStatistics.numArmSegments) - (0.7 * 2) + (0.7 * segmentAddition);

                // So now we'll get a new outer arm length, etc
                _vitalStatistics.centralSpokeLength = GetAdjacentSide(_vitalStatistics.constantSegmentLength, _vitalStatistics.centralAngle / 2);
                _vitalStatistics.outerArmLength = (_vitalStatistics.numArmSegments + 1) * _vitalStatistics.constantSegmentLength;
            }

            // (inner arm not affected by central segment length)
            _vitalStatistics.innerArmLength = _vitalStatistics.numArmSegments * _vitalStatistics.constantSegmentLength;

            _vitalStatistics.innerArcRadius = (_vitalStatistics.innerArmLength > 0) ? GetOppositeSide(_vitalStatistics.innerArmLength, _vitalStatistics.centralAngle / 2) : 0;
            _vitalStatistics.arcSegmentAngle = (_vitalStatistics.numArcSegments > 0) ? 180 / _vitalStatistics.numArcSegments : 0;

            _vitalStatistics.angleB = 90 - _vitalStatistics.constantBottomAngle / 2;
            _vitalStatistics.angleC = (180 - _vitalStatistics.centralAngle) / 2;
            _vitalStatistics.arcStartAngle = 180 - (_vitalStatistics.angleB + _vitalStatistics.angleC);

            _vitalStatistics.originToArcCentre = GetAdjacentSide(_vitalStatistics.outerArmLength, _vitalStatistics.centralAngle / 2);
            _vitalStatistics.relativeArcCentre.X = GetXFromLineLengthAndTopAngle(_vitalStatistics.originToArcCentre, _vitalStatistics.constantBottomAngle / 2 + _vitalStatistics.centralAngle / 2);
            _vitalStatistics.relativeArcCentre.Y = GetYFromLineLengthAndTopAngle(_vitalStatistics.originToArcCentre, _vitalStatistics.constantBottomAngle / 2 + _vitalStatistics.centralAngle / 2);

            _vitalStatistics.outerArcRadius = GetOppositeSide(_vitalStatistics.outerArmLength, _vitalStatistics.centralAngle / 2);
            _vitalStatistics.relativeInnerPetalSource.X = GetXFromLineLengthAndTopAngle(_vitalStatistics.centralSpokeLength, _vitalStatistics.constantBottomAngle / 2 + _vitalStatistics.centralAngle / 2);
            _vitalStatistics.relativeInnerPetalSource.Y = GetYFromLineLengthAndTopAngle(_vitalStatistics.centralSpokeLength, _vitalStatistics.constantBottomAngle / 2 + _vitalStatistics.centralAngle / 2);

            //innerArmLength = GetHypotenuseFromAdjacent(originToArcCentre - centralSpokeLength, centralAngle / 2);
            if (_vitalStatistics.innerArmLength > 0)
            {
                _vitalStatistics.relativeInnerArcEnd.X = GetXFromLineLengthAndBottomAngle(_vitalStatistics.innerArmLength, _vitalStatistics.angleB);
                _vitalStatistics.relativeInnerArcEnd.Y = GetYFromLineLengthAndBottomAngle(_vitalStatistics.innerArmLength, _vitalStatistics.angleB);
                _vitalStatistics.relativeInnerArcStart.X = GetXFromLineLengthAndTopAngle(_vitalStatistics.innerArmLength, _vitalStatistics.constantBottomAngle / 2 + _vitalStatistics.centralAngle);
                _vitalStatistics.relativeInnerArcStart.Y = GetYFromLineLengthAndTopAngle(_vitalStatistics.innerArmLength, _vitalStatistics.constantBottomAngle / 2 + _vitalStatistics.centralAngle);
            }

            _vitalStatistics.relativeOuterArcEnd.X = GetXFromLineLengthAndBottomAngle(_vitalStatistics.outerArmLength, _vitalStatistics.angleB);
            _vitalStatistics.relativeOuterArcEnd.Y = GetYFromLineLengthAndBottomAngle(_vitalStatistics.outerArmLength, _vitalStatistics.angleB);
            _vitalStatistics.relativeOuterArcStart.X = GetXFromLineLengthAndTopAngle(_vitalStatistics.outerArmLength, _vitalStatistics.constantBottomAngle / 2 + _vitalStatistics.centralAngle);
            _vitalStatistics.relativeOuterArcStart.Y = GetYFromLineLengthAndTopAngle(_vitalStatistics.outerArmLength, _vitalStatistics.constantBottomAngle / 2 + _vitalStatistics.centralAngle);

            _vitalStatistics.actualArcCentre.X = _vitalStatistics.origin.X + _vitalStatistics.relativeArcCentre.X;
            _vitalStatistics.actualArcCentre.Y = _vitalStatistics.origin.Y + _vitalStatistics.relativeArcCentre.Y;
            _vitalStatistics.actualInnerPetalSource.X = _vitalStatistics.origin.X + _vitalStatistics.relativeInnerPetalSource.X;
            _vitalStatistics.actualInnerPetalSource.Y = _vitalStatistics.origin.Y + _vitalStatistics.relativeInnerPetalSource.Y;
            _vitalStatistics.actualOuterArcStart.X = _vitalStatistics.origin.X + _vitalStatistics.relativeOuterArcStart.X;
            _vitalStatistics.actualOuterArcStart.Y = _vitalStatistics.origin.Y + _vitalStatistics.relativeOuterArcStart.Y;
            _vitalStatistics.actualOuterArcEnd.X = _vitalStatistics.origin.X + _vitalStatistics.relativeOuterArcEnd.X;
            _vitalStatistics.actualOuterArcEnd.Y = _vitalStatistics.origin.Y + _vitalStatistics.relativeOuterArcEnd.Y;

            if (_vitalStatistics.innerArmLength > 0)
            {
                // !! Caution !! The inner arc values are relative to the inner petal source, NOT to the origin!
                _vitalStatistics.actualInnerArcStart.X = _vitalStatistics.actualInnerPetalSource.X + _vitalStatistics.relativeInnerArcStart.X;
                _vitalStatistics.actualInnerArcStart.Y = _vitalStatistics.actualInnerPetalSource.Y + _vitalStatistics.relativeInnerArcStart.Y;
                _vitalStatistics.actualInnerArcEnd.X = _vitalStatistics.actualInnerPetalSource.X + _vitalStatistics.relativeInnerArcEnd.X;
                _vitalStatistics.actualInnerArcEnd.Y = _vitalStatistics.actualInnerPetalSource.Y + _vitalStatistics.relativeInnerArcEnd.Y;
            }

            _vitalStatistics.outerPath.Reset();
            _vitalStatistics.outerPath.AddLine(_vitalStatistics.origin, _vitalStatistics.actualOuterArcStart);
            if (_vitalStatistics.numArcSegments > 0)
            {
                // !! The y coordinate of the enclosing rectangle represents the TOP of the shape, not the bottom
                _vitalStatistics.outerArcSquare.X = _vitalStatistics.actualArcCentre.X - (int)Math.Round(_vitalStatistics.outerArcRadius, 0, MidpointRounding.AwayFromZero);
                _vitalStatistics.outerArcSquare.Y = _vitalStatistics.actualArcCentre.Y - (int)Math.Round(_vitalStatistics.outerArcRadius, 0, MidpointRounding.AwayFromZero);
                _vitalStatistics.outerArcSquare.Width = (int)Math.Round(_vitalStatistics.outerArcRadius * 2, 0, MidpointRounding.AwayFromZero);
                _vitalStatistics.outerArcSquare.Height = (int)Math.Round(_vitalStatistics.outerArcRadius * 2, 0, MidpointRounding.AwayFromZero);

                _vitalStatistics.outerPath.ActualPath.AddArc(_vitalStatistics.outerArcSquare.Rectangle, (float)_vitalStatistics.arcStartAngle, (float)180);
            }
            else
            {
                _vitalStatistics.outerPath.AddLine(_vitalStatistics.actualOuterArcStart, _vitalStatistics.actualOuterArcEnd);
            }
            _vitalStatistics.outerPath.AddLine(_vitalStatistics.actualOuterArcEnd, _vitalStatistics.origin);

            if (_vitalStatistics.innerArmLength > 0)
            {
                _vitalStatistics.innerPath.Reset();
                _vitalStatistics.innerPath.AddLine(_vitalStatistics.actualInnerPetalSource, _vitalStatistics.actualInnerArcStart);

                // !! The y coordinate of the enclosing rectangle represents the TOP of the shape, not the bottom
                _vitalStatistics.innerArcSquare.X = _vitalStatistics.actualArcCentre.X - (int)Math.Round(_vitalStatistics.innerArcRadius, 0, MidpointRounding.AwayFromZero);
                _vitalStatistics.innerArcSquare.Y = _vitalStatistics.actualArcCentre.Y - (int)Math.Round(_vitalStatistics.innerArcRadius, 0, MidpointRounding.AwayFromZero);
                _vitalStatistics.innerArcSquare.Width = (int)Math.Round(_vitalStatistics.innerArcRadius * 2, 0, MidpointRounding.AwayFromZero);
                _vitalStatistics.innerArcSquare.Height = (int)Math.Round(_vitalStatistics.innerArcRadius * 2, 0, MidpointRounding.AwayFromZero);

                _vitalStatistics.innerPath.ActualPath.AddArc(_vitalStatistics.innerArcSquare.Rectangle, (float)_vitalStatistics.arcStartAngle, (float)180);
                _vitalStatistics.innerPath.AddLine(_vitalStatistics.actualInnerArcEnd, _vitalStatistics.actualInnerPetalSource);
            }

            // Create petal region 
            using (Region petalRegion = new Region(_vitalStatistics.outerPath.ActualPath))
            {
                storedPetalRegion = new Region(_vitalStatistics.outerPath.ActualPath);
                if (_vitalStatistics.innerArmLength > 0)
                {
                    storedPetalRegion.Exclude(_vitalStatistics.innerPath.ActualPath);
                    petalRegion.Exclude(_vitalStatistics.innerPath.ActualPath);
                }

                // Create numArmSegments + 1 divisions of the start arm and end arm (including the centre).
                ClearAllArmDivisions();
                // always create the first division, even if the arms have no segments.
                double fractionalMultiplier = 1.0 / ((double)_vitalStatistics.numArmSegments + 1.0);

                // STARTS ****central segment length change STARTS
                _vitalStatistics.startArmDivisionStarts.Points.Add(MoveAlongLineByFraction(_vitalStatistics.origin, _vitalStatistics.actualOuterArcStart, fractionalMultiplier));
                _vitalStatistics.endArmDivisionStarts.Points.Add(MoveAlongLineByFraction(_vitalStatistics.origin, _vitalStatistics.actualOuterArcEnd, fractionalMultiplier));
                //_vitalStatistics.startArmDivisionStarts.Add(MoveAlongLineByLength(_vitalStatistics.origin, _vitalStatistics.actualOuterArcStart, constantCentralSegmentLength));
                //_vitalStatistics.endArmDivisionStarts.Add(MoveAlongLineByLength(_vitalStatistics.origin, _vitalStatistics.actualOuterArcEnd, constantCentralSegmentLength));
                // ENDS ****central segment length change ENDS

                _vitalStatistics.startArmDivisionEnds.Points.Add(_vitalStatistics.actualInnerPetalSource);
                _vitalStatistics.endArmDivisionEnds.Points.Add(_vitalStatistics.actualInnerPetalSource);

                // now create the rest, if necessary
                if (_vitalStatistics.numArmSegments > 1) // If there's only one, there's no dividers necessary
                {
                    for (int iCount = 2; iCount <= _vitalStatistics.numArmSegments; iCount++)
                    {
                        // STARTS ****central segment length change STARTS
                        double outerFractionalMultiplier = (double)(iCount) / ((double)_vitalStatistics.numArmSegments + 1.0); // eg 4 segments: 2/5, 3/5, 4/5
                        _vitalStatistics.startArmDivisionStarts.Points.Add(MoveAlongLineByFraction(_vitalStatistics.origin, _vitalStatistics.actualOuterArcStart, outerFractionalMultiplier));
                        _vitalStatistics.endArmDivisionStarts.Points.Add(MoveAlongLineByFraction(_vitalStatistics.origin, _vitalStatistics.actualOuterArcEnd, outerFractionalMultiplier));
                        //double outerFractionalMultiplier = ((double)(iCount) - 1.0) / (double)(_vitalStatistics.numArmSegments); // eg 4 segments: 1/4, 2/4, 3/4
                        //_vitalStatistics.startArmDivisionStarts.Add(MoveAlongLineByFraction(_vitalStatistics.startArmDivisionStarts.ElementAt(0), _vitalStatistics.actualOuterArcStart, outerFractionalMultiplier));
                        //_vitalStatistics.endArmDivisionStarts.Add(MoveAlongLineByFraction(_vitalStatistics.endArmDivisionStarts.ElementAt(0), _vitalStatistics.actualOuterArcEnd, outerFractionalMultiplier));
                        // ENDS ****central segment length change ENDS

                        double innerFractionalMultiplier = ((double)(iCount) - 1.0) / (double)(_vitalStatistics.numArmSegments); // eg 4 segments: 1/4, 2/4, 3/4
                        _vitalStatistics.startArmDivisionEnds.Points.Add(MoveAlongLineByFraction(_vitalStatistics.actualInnerPetalSource, _vitalStatistics.actualInnerArcStart, innerFractionalMultiplier));
                        _vitalStatistics.endArmDivisionEnds.Points.Add(MoveAlongLineByFraction(_vitalStatistics.actualInnerPetalSource, _vitalStatistics.actualInnerArcEnd, innerFractionalMultiplier));
                    }
                }

                // Create numArcSegments divisions of the arc
                _vitalStatistics.arcSpokes.Points.Clear();
                if (_vitalStatistics.numArcSegments > 1)
                {
                    TopGamePoint currentArcSpoke = GetEndPointOfRotatedLine(_vitalStatistics.outerArcRadius, _vitalStatistics.actualArcCentre, _vitalStatistics.actualOuterArcStart, _vitalStatistics.arcSegmentAngle);
                    currentArcSpoke.X = _vitalStatistics.actualArcCentre.X + currentArcSpoke.X;
                    currentArcSpoke.Y = _vitalStatistics.actualArcCentre.Y + currentArcSpoke.Y;
                    _vitalStatistics.arcSpokes.Points.Add(currentArcSpoke);
                    TopGamePoint previousArcSpoke = currentArcSpoke;
                    for (int iCount = 1; iCount < _vitalStatistics.numArcSegments - 1; iCount++)
                    {
                        currentArcSpoke = GetEndPointOfRotatedLine(_vitalStatistics.outerArcRadius, _vitalStatistics.actualArcCentre, previousArcSpoke, _vitalStatistics.arcSegmentAngle);
                        currentArcSpoke.X = _vitalStatistics.actualArcCentre.X + currentArcSpoke.X;
                        currentArcSpoke.Y = _vitalStatistics.actualArcCentre.Y + currentArcSpoke.Y;
                        _vitalStatistics.arcSpokes.Points.Add(currentArcSpoke);
                        previousArcSpoke = currentArcSpoke;
                    }
                }

                // Create sub-regions
                DisposeRegions(); // Note that this also clears graphicsIndependentRegions
                DisposeRegionPaths();
                Debug.Assert(subRegions.Count() == 0, "There are some subregions left after disposing of them!");

                // The sub-regions are all the areas that get coloured in: Each region represents an individual card.
                // They are displayed in three sections, all of which added together look a bit like a petal:
                // 1) The straight "start-arm", basically made out of two parallel lines (but tapering to a triangular point at the centre)
                // 2) The curved "arc" which joins the two arms together
                // 3) The straight "end-arm", basically made out of two parallel lines (but tapering to a triangular point at the centre)

                // Start with the central region of the start-arm - this is the triangular bit that goes from the centre out to the start of the parallel-lines part of the arm
                // (after the central triangle, all the regions in the start-arm are parallelograms)
                if (_vitalStatistics.numTotalSegments > 1)
                {
                    // start-arm central region
                    AddTriangularRegion(
                        _vitalStatistics.origin,
                        _vitalStatistics.startArmDivisionStarts.Points.ElementAt(0),
                        _vitalStatistics.actualInnerPetalSource,
                                goldenMasterRegions);
                }
                else
                {
                    // just one central region
                    AddTriangularRegion(
                        _vitalStatistics.origin,
                        _vitalStatistics.startArmDivisionStarts.Points.ElementAt(0),
                        _vitalStatistics.endArmDivisionStarts.Points.ElementAt(0),
                                goldenMasterRegions);
                    //tempRegionPath.AddLine(_vitalStatistics.origin, _vitalStatistics.startArmDivisionStarts.ElementAt(0));
                    //tempRegionPath.AddLine(_vitalStatistics.startArmDivisionStarts.ElementAt(0), _vitalStatistics.endArmDivisionStarts.ElementAt(0));
                    //tempRegionPath.AddLine(_vitalStatistics.endArmDivisionStarts.ElementAt(0), _vitalStatistics.origin);
                    //subRegions.Add(new Region(tempRegionPath));
                    ////regionPaths.Add(new GraphicsPath(tempRegionPath.PathPoints, tempRegionPath.PathTypes));
                }

                if (_vitalStatistics.numArmSegments > 0)
                {
                    // all the divisions of the start arm
                    if (_vitalStatistics.numArmSegments > 1)
                    {
                        for (int iCount = 0; iCount < _vitalStatistics.numArmSegments - 1; iCount++)
                        {
                            AddParallelogramRegion(
                                _vitalStatistics.startArmDivisionStarts.Points.ElementAt(iCount),
                                _vitalStatistics.startArmDivisionEnds.Points.ElementAt(iCount),
                                _vitalStatistics.startArmDivisionEnds.Points.ElementAt(iCount + 1),
                                _vitalStatistics.startArmDivisionStarts.Points.ElementAt(iCount + 1),
                                goldenMasterRegions
                                );
                            //tempRegionPath.Reset();
                            //tempRegionPath.AddLine(_vitalStatistics.startArmDivisionStarts.ElementAt(iCount), _vitalStatistics.startArmDivisionEnds.ElementAt(iCount));
                            //tempRegionPath.AddLine(_vitalStatistics.startArmDivisionEnds.ElementAt(iCount), _vitalStatistics.startArmDivisionEnds.ElementAt(iCount + 1));
                            //tempRegionPath.AddLine(_vitalStatistics.startArmDivisionEnds.ElementAt(iCount + 1), _vitalStatistics.startArmDivisionStarts.ElementAt(iCount + 1));
                            //tempRegionPath.AddLine(_vitalStatistics.startArmDivisionStarts.ElementAt(iCount + 1), _vitalStatistics.startArmDivisionStarts.ElementAt(iCount));
                            //subRegions.Add(new Region(tempRegionPath));
                            ////regionPaths.Add(new GraphicsPath(tempRegionPath.PathPoints, tempRegionPath.PathTypes));
                        }
                    }
                    // the last one hooks up to the arc.
                    AddParallelogramRegion(
                        _vitalStatistics.startArmDivisionStarts.Points.ElementAt(_vitalStatistics.numArmSegments - 1),
                        _vitalStatistics.startArmDivisionEnds.Points.ElementAt(_vitalStatistics.numArmSegments - 1),
                        _vitalStatistics.actualInnerArcStart,
                        _vitalStatistics.actualOuterArcStart,
                                goldenMasterRegions
                        );
                    //tempRegionPath.Reset();
                    //tempRegionPath.AddLine(_vitalStatistics.startArmDivisionStarts.ElementAt(_vitalStatistics.numArmSegments - 1), _vitalStatistics.startArmDivisionEnds.ElementAt(_vitalStatistics.numArmSegments - 1));
                    //tempRegionPath.AddLine(_vitalStatistics.startArmDivisionEnds.ElementAt(_vitalStatistics.numArmSegments - 1), _vitalStatistics.actualInnerArcStart);
                    //tempRegionPath.AddLine(_vitalStatistics.actualInnerArcStart, _vitalStatistics.actualOuterArcStart);
                    //tempRegionPath.AddLine(_vitalStatistics.actualOuterArcStart, _vitalStatistics.startArmDivisionStarts.ElementAt(_vitalStatistics.numArmSegments - 1));
                    //subRegions.Add(new Region(tempRegionPath));
                    ////regionPaths.Add(new GraphicsPath(tempRegionPath.PathPoints, tempRegionPath.PathTypes));
                }

                // the divisions of the arc (sounds biblical!)
                if (_vitalStatistics.numArcSegments > 0)
                {
                    if (_vitalStatistics.arcSpokes.Points.Count() == 0)
                    {
                        using (GraphicsPath tempRegionPath = new GraphicsPath())
                        {
                            // the division is the arc itself.
                            tempRegionPath.AddArc(_vitalStatistics.outerArcSquare.Rectangle, (float)_vitalStatistics.arcStartAngle, (float)180);
                            tempRegionPath.AddLine(_vitalStatistics.actualOuterArcStart.Point, _vitalStatistics.actualOuterArcEnd.Point);
                            subRegions.Add(new Region(tempRegionPath));
                            //regionPaths.Add(new GraphicsPath(tempRegionPath.PathPoints, tempRegionPath.PathTypes));

                            var newTopGameRegion = new TopGameRegion();
                            newTopGameRegion.TopGamePoints.Add(_vitalStatistics.actualOuterArcStart);
                            newTopGameRegion.TopGamePoints.Add(_vitalStatistics.actualOuterArcEnd);
                            if (goldenMasterRegions != null)
                            {
                                goldenMasterRegions.Add(newTopGameRegion.ToGoldenMasterRegion());
                            }
                            else
                            {
                                graphicsIndependentRegions.Add(newTopGameRegion);
                            }
                        }
                    }
                    else
                    {
                        // 1st arc division
                        AddArcRegion(
                            petalRegion, 
                            _vitalStatistics.actualArcCentre, 
                            _vitalStatistics.actualOuterArcStart,
                            _vitalStatistics.arcSpokes.Points.ElementAt(0),
                                goldenMasterRegions);
                        //tempRegionPath.Reset();
                        //tempRegionPath.AddLine(_vitalStatistics.actualArcCentre, _vitalStatistics.actualOuterArcStart);
                        //tempRegionPath.AddLine(_vitalStatistics.actualOuterArcStart, _vitalStatistics.arcSpokes.ElementAt(0));
                        //tempRegionPath.AddLine(_vitalStatistics.arcSpokes.ElementAt(0), _vitalStatistics.actualArcCentre);
                        //using (Region tempRegion = new Region(petalRegion.Clone().GetRegionData()))
                        //{
                        //    tempRegion.Intersect(tempRegionPath);
                        //    subRegions.Add(new Region(tempRegion.GetRegionData()));
                        //}

                        // middle arc divisions
                        for (int iCount = 1; iCount < _vitalStatistics.arcSpokes.Points.Count(); iCount++)
                        {
                            AddArcRegion(
                                petalRegion,
                                _vitalStatistics.actualArcCentre,
                                MoveAlongLineByFraction(_vitalStatistics.actualArcCentre, _vitalStatistics.arcSpokes.Points.ElementAt(iCount - 1), 1.5),
                                MoveAlongLineByFraction(_vitalStatistics.actualArcCentre, _vitalStatistics.arcSpokes.Points.ElementAt(iCount), 1.5),
                                goldenMasterRegions);
                            //tempRegionPath.Reset();
                            //tempRegionPath.AddLine(_vitalStatistics.actualArcCentre, MoveAlongLineByFraction(_vitalStatistics.actualArcCentre, _vitalStatistics.arcSpokes.ElementAt(iCount - 1), 1.5));
                            //tempRegionPath.AddLine(MoveAlongLineByFraction(_vitalStatistics.actualArcCentre, _vitalStatistics.arcSpokes.ElementAt(iCount - 1), 1.5), MoveAlongLineByFraction(_vitalStatistics.actualArcCentre, _vitalStatistics.arcSpokes.ElementAt(iCount), 1.5));
                            //tempRegionPath.AddLine(MoveAlongLineByFraction(_vitalStatistics.actualArcCentre, _vitalStatistics.arcSpokes.ElementAt(iCount), 1.5), _vitalStatistics.actualArcCentre);
                            //using (Region tempRegion = new Region(petalRegion.Clone().GetRegionData()))
                            //{
                            //    tempRegion.Intersect(tempRegionPath);
                            //    subRegions.Add(new Region(tempRegion.GetRegionData()));
                            //}
                        }

                        // last arc division
                        AddArcRegion(
                            petalRegion,
                            _vitalStatistics.actualArcCentre,
                            MoveAlongLineByFraction(_vitalStatistics.actualArcCentre, _vitalStatistics.arcSpokes.Points.ElementAt(_vitalStatistics.arcSpokes.Points.Count() - 1), 1.5),
                            MoveAlongLineByFraction(_vitalStatistics.actualArcCentre, _vitalStatistics.actualOuterArcEnd, 1.5),
                                goldenMasterRegions);
                        //tempRegionPath.Reset();
                        //tempRegionPath.AddLine(_vitalStatistics.actualArcCentre, MoveAlongLineByFraction(_vitalStatistics.actualArcCentre, _vitalStatistics.arcSpokes.ElementAt(_vitalStatistics.arcSpokes.Count() - 1), 1.5));
                        //tempRegionPath.AddLine(MoveAlongLineByFraction(_vitalStatistics.actualArcCentre, _vitalStatistics.arcSpokes.ElementAt(_vitalStatistics.arcSpokes.Count() - 1), 1.5), MoveAlongLineByFraction(_vitalStatistics.actualArcCentre, _vitalStatistics.actualOuterArcEnd, 1.5));
                        //tempRegionPath.AddLine(MoveAlongLineByFraction(_vitalStatistics.actualArcCentre, _vitalStatistics.actualOuterArcEnd, 1.5), _vitalStatistics.actualArcCentre);
                        //using (Region tempRegion = new Region(petalRegion.Clone().GetRegionData()))
                        //{
                        //    tempRegion.Intersect(tempRegionPath);
                        //    subRegions.Add(new Region(tempRegion.GetRegionData()));
                        //}
                    }
                }

                if (_vitalStatistics.numArmSegments > 0)
                {
                    // all the divisions of the end arm (in reverse)
                    AddParallelogramRegion(
                        _vitalStatistics.endArmDivisionStarts.Points.ElementAt(_vitalStatistics.numArmSegments - 1),
                        _vitalStatistics.endArmDivisionEnds.Points.ElementAt(_vitalStatistics.numArmSegments - 1),
                        _vitalStatistics.actualInnerArcEnd,
                        _vitalStatistics.actualOuterArcEnd,
                                goldenMasterRegions
                        );
                    //tempRegionPath.Reset();
                    //tempRegionPath.AddLine(_vitalStatistics.endArmDivisionStarts.ElementAt(_vitalStatistics.numArmSegments - 1), _vitalStatistics.endArmDivisionEnds.ElementAt(_vitalStatistics.numArmSegments - 1));
                    //tempRegionPath.AddLine(_vitalStatistics.endArmDivisionEnds.ElementAt(_vitalStatistics.numArmSegments - 1), _vitalStatistics.actualInnerArcEnd);
                    //tempRegionPath.AddLine(_vitalStatistics.actualInnerArcEnd, _vitalStatistics.actualOuterArcEnd);
                    //tempRegionPath.AddLine(_vitalStatistics.actualOuterArcEnd, _vitalStatistics.endArmDivisionStarts.ElementAt(_vitalStatistics.numArmSegments - 1));
                    //subRegions.Add(new Region(tempRegionPath));
                    ////regionPaths.Add(new GraphicsPath(tempRegionPath.PathPoints, tempRegionPath.PathTypes));

                    if (_vitalStatistics.numArmSegments > 1)
                    {
                        for (int iCount = _vitalStatistics.numArmSegments - 1; iCount > 0; iCount--)
                        {
                            AddParallelogramRegion(
                                _vitalStatistics.endArmDivisionStarts.Points.ElementAt(iCount),
                                _vitalStatistics.endArmDivisionEnds.Points.ElementAt(iCount),
                                _vitalStatistics.endArmDivisionEnds.Points.ElementAt(iCount - 1),
                                _vitalStatistics.endArmDivisionStarts.Points.ElementAt(iCount - 1),
                                goldenMasterRegions
                                );
                            //tempRegionPath.Reset();
                            //tempRegionPath.AddLine(_vitalStatistics.endArmDivisionStarts.ElementAt(iCount), _vitalStatistics.endArmDivisionEnds.ElementAt(iCount));
                            //tempRegionPath.AddLine(_vitalStatistics.endArmDivisionEnds.ElementAt(iCount), _vitalStatistics.endArmDivisionEnds.ElementAt(iCount - 1));
                            //tempRegionPath.AddLine(_vitalStatistics.endArmDivisionEnds.ElementAt(iCount - 1), _vitalStatistics.endArmDivisionStarts.ElementAt(iCount - 1));
                            //tempRegionPath.AddLine(_vitalStatistics.endArmDivisionStarts.ElementAt(iCount - 1), _vitalStatistics.endArmDivisionStarts.ElementAt(iCount));
                            //subRegions.Add(new Region(tempRegionPath));
                            ////regionPaths.Add(new GraphicsPath(tempRegionPath.PathPoints, tempRegionPath.PathTypes));
                        }
                    }
                }

                // End with the central region of the end-arm - this is the triangular bit that goes from the centre out to the end of the parallel-lines part of the arm
                // (apart from the central triangle, all the regions in the end-arm are parallelograms)
                if (_vitalStatistics.numTotalSegments > 1)
                {
                    // end-arm central region
                    AddTriangularRegion(
                        _vitalStatistics.origin,
                        _vitalStatistics.endArmDivisionStarts.Points.ElementAt(0),
                        _vitalStatistics.actualInnerPetalSource,
                                goldenMasterRegions
                        );
                    //tempRegionPath.Reset();
                    //tempRegionPath.AddLine(_vitalStatistics.origin, _vitalStatistics.endArmDivisionStarts.ElementAt(0));
                    //tempRegionPath.AddLine(_vitalStatistics.endArmDivisionStarts.ElementAt(0), _vitalStatistics.actualInnerPetalSource);
                    //tempRegionPath.AddLine(_vitalStatistics.actualInnerPetalSource, _vitalStatistics.origin);
                    //subRegions.Add(new Region(tempRegionPath));
                    ////regionPaths.Add(new GraphicsPath(tempRegionPath.PathPoints, tempRegionPath.PathTypes));
                }
            }

            Debug.Assert(_vitalStatistics.numTotalSegments == subRegions.Count(), "Region count",
                                                "Number of regions is not num total segments");

            if (rotationAngle > 0)
            {
                RotateByAngle(rotationAngle);
            }
        }

        private void ClearAllArmDivisions()
        {
            if (_vitalStatistics.startArmDivisionStarts.Points.Any())
            {
                _vitalStatistics.startArmDivisionStarts.Points.Clear();
            }
            if (_vitalStatistics.startArmDivisionEnds.Points.Any())
            {
                _vitalStatistics.startArmDivisionEnds.Points.Clear();
            }
            if (_vitalStatistics.endArmDivisionStarts.Points.Any())
            {
                _vitalStatistics.endArmDivisionStarts.Points.Clear();
            }
            if (_vitalStatistics.endArmDivisionEnds.Points.Any())
            {
                _vitalStatistics.endArmDivisionEnds.Points.Clear();
            }
        }

        private void AddArcRegion(Region petalRegion, TopGamePoint pointA, TopGamePoint pointB, TopGamePoint pointC, ICollection<GoldenMasterRegion> goldenMasterRegions)
        {
            using (GraphicsPath tempRegionPath = new GraphicsPath())
            {
                var newTopGameRegion = new TopGameRegion();
                newTopGameRegion.TopGamePoints.Add(pointA);
                newTopGameRegion.TopGamePoints.Add(pointB);
                newTopGameRegion.TopGamePoints.Add(pointC);

                if (goldenMasterRegions != null)
                {
                    goldenMasterRegions.Add(newTopGameRegion.ToGoldenMasterRegion());
                }
                else
                {
                    graphicsIndependentRegions.Add(newTopGameRegion);
                }

                tempRegionPath.AddLine(pointA.Point, pointB.Point);
                tempRegionPath.AddLine(pointB.Point, pointC.Point);
                tempRegionPath.AddLine(pointC.Point, pointA.Point);

                using (Region tempRegion = new Region(petalRegion.Clone().GetRegionData()))
                {
                    tempRegion.Intersect(tempRegionPath);
                    subRegions.Add(new Region(tempRegion.GetRegionData()));
                }
            }
        }

        private void AddTriangularRegion(TopGamePoint pointA, TopGamePoint pointB, TopGamePoint pointC, ICollection<GoldenMasterRegion> goldenMasterRegions)
        {
            using (GraphicsPath tempRegionPath = new GraphicsPath())
            {
                var newTopGameRegion = new TopGameRegion();
                newTopGameRegion.TopGamePoints.Add(pointA);
                newTopGameRegion.TopGamePoints.Add(pointB);
                newTopGameRegion.TopGamePoints.Add(pointC);

                if (goldenMasterRegions != null)
                {
                    goldenMasterRegions.Add(newTopGameRegion.ToGoldenMasterRegion());
                }
                else
                {
                    graphicsIndependentRegions.Add(newTopGameRegion);
                }

                tempRegionPath.AddLine(pointA.Point, pointB.Point);
                tempRegionPath.AddLine(pointB.Point, pointC.Point);
                tempRegionPath.AddLine(pointC.Point, pointA.Point);

                subRegions.Add(new Region(tempRegionPath));
                //regionPaths.Add(new GraphicsPath(tempRegionPath.PathPoints, tempRegionPath.PathTypes));
            }
        }

        private void AddParallelogramRegion(TopGamePoint pointA, TopGamePoint pointB, TopGamePoint pointC, TopGamePoint pointD, ICollection<GoldenMasterRegion> goldenMasterRegions)
        {
            using (GraphicsPath tempRegionPath = new GraphicsPath())
            {
                var newTopGameRegion = new TopGameRegion();
                newTopGameRegion.TopGamePoints.Add(pointA);
                newTopGameRegion.TopGamePoints.Add(pointB);
                newTopGameRegion.TopGamePoints.Add(pointC);
                newTopGameRegion.TopGamePoints.Add(pointD);

                if (goldenMasterRegions != null)
                {
                    goldenMasterRegions.Add(newTopGameRegion.ToGoldenMasterRegion());
                }
                else
                {
                    graphicsIndependentRegions.Add(newTopGameRegion);
                }

                tempRegionPath.AddLine(pointA.Point, pointB.Point);
                tempRegionPath.AddLine(pointB.Point, pointC.Point);
                tempRegionPath.AddLine(pointC.Point, pointD.Point);
                tempRegionPath.AddLine(pointD.Point, pointA.Point);

                subRegions.Add(new Region(tempRegionPath));
                //regionPaths.Add(new GraphicsPath(tempRegionPath.PathPoints, tempRegionPath.PathTypes));
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
                    e.Graphics.DrawPath(myPen, _vitalStatistics.outerPath.ActualPath);

                    // Draw inner path
                    e.Graphics.DrawPath(myPen, _vitalStatistics.innerPath.ActualPath);

                    // Draw arc spokes
                    for (int iCount = 0; iCount < _vitalStatistics.arcSpokes.Points.Count(); iCount++)
                    {
                        e.Graphics.DrawLine(myPen, _vitalStatistics.actualArcCentre.Point, _vitalStatistics.arcSpokes.Points.ElementAt(iCount).Point);
                    }

                    // Draw the divisions of the start arm.
                    for (int iCount = 0; iCount < _vitalStatistics.arcSpokes.Points.Count(); iCount++)
                    {
                        e.Graphics.DrawLine(myPen, _vitalStatistics.startArmDivisionStarts.Points.ElementAt(iCount).Point, _vitalStatistics.startArmDivisionEnds.Points.ElementAt(iCount).Point);
                    }

                    // Draw the divisions of the end arm.
                    for (int iCount = 0; iCount < _vitalStatistics.arcSpokes.Points.Count(); iCount++)
                    {
                        e.Graphics.DrawLine(myPen, _vitalStatistics.endArmDivisionStarts.Points.ElementAt(iCount).Point, _vitalStatistics.endArmDivisionEnds.Points.ElementAt(iCount).Point);
                    }

                    // Draw outer path first line
                    e.Graphics.DrawLine(myPen, _vitalStatistics.origin.Point, _vitalStatistics.actualOuterArcStart.Point);
                    // Draw outer path arc square
                    e.Graphics.DrawRectangle(myPen, _vitalStatistics.outerArcSquare.Rectangle);
                    // Draw outer path arc 
                    e.Graphics.DrawArc(myPen, _vitalStatistics.outerArcSquare.Rectangle, (float)_vitalStatistics.arcStartAngle, (float)180);
                    // Draw outer path last line
                    e.Graphics.DrawLine(myPen, _vitalStatistics.actualOuterArcEnd.Point, _vitalStatistics.origin.Point);

                    // Draw inner path first line
                    e.Graphics.DrawLine(myPen, _vitalStatistics.actualInnerPetalSource.Point, _vitalStatistics.actualInnerArcStart.Point);
                    // Draw inner path arc square
                    e.Graphics.DrawRectangle(myPen, _vitalStatistics.innerArcSquare.Rectangle);
                    // Draw inner path arc 
                    e.Graphics.DrawArc(myPen, _vitalStatistics.innerArcSquare.Rectangle, (float)_vitalStatistics.arcStartAngle, (float)180);
                    // Draw inner path last line
                    e.Graphics.DrawLine(myPen, _vitalStatistics.actualInnerArcEnd.Point, _vitalStatistics.origin.Point);
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

        public GoldenMasterSinglePass PopulateGoldenMaster(int numPlayersInGame)
        {
            var resultsOfThisCall = new GoldenMasterSinglePass();
            PrepareActualData(0, resultsOfThisCall.TopGameRegions);

            // Don't copy vital statistics until after the call to PrepareActualData
            VitalStatistics calculatedStatistics = new VitalStatistics();
            calculatedStatistics.Copy(_vitalStatistics);

            // resultsOfThisCall.TopGameRegions will be populated during the call to PrepareActualData above
            resultsOfThisCall.VitalStatistics = calculatedStatistics;
            resultsOfThisCall.NumCardsInLoop = calculatedStatistics.numTotalSegments;
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