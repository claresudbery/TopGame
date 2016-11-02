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

        private readonly TopGameGraphicsData _topGameGraphicsData;
        private List<Region> subRegions;

        //Region storedPetalRegion; // just for debug purposes: see what the petal region looks like.
        List<ColouredRegion> regionColours;

        public OnePlayerGraphicsLoop()
        {
            _topGameGraphicsData = new TopGameGraphicsData(this);

            regionColours = new List<ColouredRegion>();
            subRegions = new List<Region>();
        }

        public void Dispose()
        {
            _topGameGraphicsData.Dispose();
            DisposeRegions();
        }

        public void DisposeRegions()
        {
            for (int iCount = subRegions.Count() - 1; iCount >= 0; iCount--)
            {
                subRegions.ElementAt(iCount).Dispose();
                subRegions.RemoveAt(iCount);
            }
        }

        public void Clear()
        {
            _topGameGraphicsData.Clear();
            DisposeRegions();
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

        private void PrepareActualData(double rotationAngle, GoldenMasterSingleGraphicPass goldenMasterData = null)
        {
            _topGameGraphicsData.PrepareActualData(rotationAngle, goldenMasterData);

            if (rotationAngle > 0)
            {
                RotateByAngle(rotationAngle);
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

        public void AddArcRegion(
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

        public void AddTriangularRegion(
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

        public void AddParallelogramRegion(
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

        public void AddSubRegion(TopGameGraphicsPath topGameGraphicsPath)
        {
            subRegions.Add(new Region(topGameGraphicsPath.ActualPath));
        }
    }

// end class
}// end namespace