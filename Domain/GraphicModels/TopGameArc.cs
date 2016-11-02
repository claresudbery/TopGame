using System;
using System.Drawing;
using System.Linq;
using Domain.Extensions;
using Domain.GraphicModels.GoldenMaster;

namespace Domain.GraphicModels
{
    public class TopGameArc
    {
        private readonly OnePlayerGraphicsLoop _onePlayerGraphicsLoop;

        public TopGameArc(OnePlayerGraphicsLoop onePlayerGraphicsLoop)
        {
            _onePlayerGraphicsLoop = onePlayerGraphicsLoop;

            RelativeInnerArcStart = new TopGamePoint();
            RelativeInnerArcEnd = new TopGamePoint();

            RelativeOuterArcStart = new TopGamePoint();
            RelativeOuterArcEnd = new TopGamePoint();
            ActualInnerArcStart = new TopGamePoint();
            ActualInnerArcEnd = new TopGamePoint();

            RelativeArcCentre = new TopGamePoint();
            ActualArcCentre = new TopGamePoint();

            ActualOuterArcStart = new TopGamePoint();
            ActualOuterArcEnd = new TopGamePoint();

            OuterPath = new TopGameGraphicsPath();
            InnerPath = new TopGameGraphicsPath();
            ArcSpokes = new TopGamePointCollection();

            InnerArcSquare = new TopGameRectangle();
            OuterArcSquare = new TopGameRectangle();
        }

        public double InnerArcRadius { get; set; }

        public double OuterArcRadius { get; set; }

        public double ArcSegmentAngle { get; set; }

        public double ArcStartAngle { get; set; }

        public TopGamePoint RelativeArcCentre { get; set; }

        public TopGamePoint RelativeInnerArcStart { get; set; }

        public TopGamePoint RelativeInnerArcEnd { get; set; }

        public TopGamePoint RelativeOuterArcStart { get; set; }

        public TopGamePoint RelativeOuterArcEnd { get; set; }

        public TopGamePoint ActualInnerArcStart { get; set; }

        public TopGamePoint ActualInnerArcEnd { get; set; }

        public TopGamePoint ActualArcCentre { get; set; }

        public TopGamePoint ActualOuterArcStart { get; set; }

        public TopGamePoint ActualOuterArcEnd { get; set; }

        public int NumArcSegments { get; set; }

        public TopGameGraphicsPath OuterPath { get; set; }

        public TopGameGraphicsPath InnerPath { get; set; }

        public TopGamePointCollection ArcSpokes { get; set; }

        public TopGameRectangle InnerArcSquare { get; set; }

        public TopGameRectangle OuterArcSquare { get; set; }

        public double OriginToArcCentre { get; set; }

        public double OuterArmLength { get; set; }

        public double InnerArmLength { get; set; }

        public void Copy(TopGameArc topGameArcSource)
        {
            RelativeArcCentre = topGameArcSource.RelativeArcCentre;

            RelativeInnerArcStart = topGameArcSource.RelativeInnerArcStart;
            RelativeInnerArcEnd = topGameArcSource.RelativeInnerArcEnd;

            RelativeOuterArcStart = topGameArcSource.RelativeOuterArcStart;
            RelativeOuterArcEnd = topGameArcSource.RelativeOuterArcEnd;
            ActualInnerArcStart = topGameArcSource.ActualInnerArcStart;
            ActualInnerArcEnd = topGameArcSource.ActualInnerArcEnd;

            ActualArcCentre = topGameArcSource.ActualArcCentre;
            ActualOuterArcStart = topGameArcSource.ActualOuterArcStart;
            ActualOuterArcEnd = topGameArcSource.ActualOuterArcEnd;

            OuterPath.Copy(topGameArcSource.OuterPath);
            InnerPath.Copy(topGameArcSource.InnerPath);
            ArcSpokes.Copy(topGameArcSource.ArcSpokes);

            InnerArcSquare.Copy(topGameArcSource.InnerArcSquare);
            OuterArcSquare.Copy(topGameArcSource.OuterArcSquare);
            InnerArcRadius = topGameArcSource.InnerArcRadius;
            OuterArcRadius = topGameArcSource.OuterArcRadius;
            ArcStartAngle = topGameArcSource.ArcStartAngle;
            ArcSegmentAngle = topGameArcSource.ArcSegmentAngle;
            NumArcSegments = topGameArcSource.NumArcSegments;
            OriginToArcCentre = topGameArcSource.OriginToArcCentre;

            OuterArmLength = topGameArcSource.OuterArmLength;
            InnerArmLength = topGameArcSource.InnerArmLength;
        }

        public void Dispose()
        {
            OuterPath.ActualPath.Dispose();
            InnerPath.ActualPath.Dispose();
        }

        public void AddArcSegments(GoldenMasterSingleGraphicPass goldenMasterData, TopGameLoop generalLoopData)
        {
            Region petalRegion = MakePetalRegion(generalLoopData);

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
                        LineCalculator.MoveAlongLineByFraction(ActualArcCentre, ActualOuterArcStart, 1.5),
                        LineCalculator.MoveAlongLineByFraction(ActualArcCentre, ArcSpokes.Points.ElementAt(0), 1.5),
                            goldenMasterData);

                    // middle arc divisions
                    for (int iCount = 1; iCount < ArcSpokes.Points.Count(); iCount++)
                    {
                        _onePlayerGraphicsLoop.AddArcRegion(
                            petalRegion,
                            ActualArcCentre,
                            LineCalculator.MoveAlongLineByFraction(ActualArcCentre, ArcSpokes.Points.ElementAt(iCount - 1), 1.5),
                            LineCalculator.MoveAlongLineByFraction(ActualArcCentre, ArcSpokes.Points.ElementAt(iCount), 1.5),
                            goldenMasterData);
                    }

                    // last arc division
                    _onePlayerGraphicsLoop.AddArcRegion(
                        petalRegion,
                        ActualArcCentre,
                        LineCalculator.MoveAlongLineByFraction(ActualArcCentre, ArcSpokes.Points.ElementAt(ArcSpokes.Points.Count() - 1), 1.5),
                        LineCalculator.MoveAlongLineByFraction(ActualArcCentre, ActualOuterArcEnd, 1.5),
                            goldenMasterData);
                }
            }
        }

        private void CalculateArcSpokeCoordinates()
        {
            // Create NumArcSegments divisions of the arc
            ArcSpokes.Points.Clear();
            if (NumArcSegments > 1)
            {
                TopGamePoint currentArcSpoke = LineCalculator.GetEndPointOfRotatedLine(OuterArcRadius, ActualArcCentre, ActualOuterArcStart, ArcSegmentAngle);
                ArcSpokes.Points.Add(currentArcSpoke);
                TopGamePoint previousArcSpoke = currentArcSpoke;
                for (int iCount = 1; iCount < NumArcSegments - 1; iCount++)
                {
                    currentArcSpoke = LineCalculator.GetEndPointOfRotatedLine(OuterArcRadius, ActualArcCentre, previousArcSpoke, ArcSegmentAngle);
                    ArcSpokes.Points.Add(currentArcSpoke);
                    previousArcSpoke = currentArcSpoke;
                }
            }
        }

        public void CalculateArcCoordinates(TopGameLoop generalLoopData)
        {
            OuterArmLength = (generalLoopData.NumArmSegments + 1) * generalLoopData.ArmSegmentLength;
            InnerArmLength = generalLoopData.NumArmSegments * generalLoopData.ArmSegmentLength;
            NumArcSegments = generalLoopData.ArcWillExist() ? generalLoopData.NumSegmentsContainedInArmsAndArc().DividedBy3PlusLeftovers() : 0;
            ArcSegmentAngle = (NumArcSegments > 0) ? 180 / NumArcSegments : 0;
            ArcStartAngle = generalLoopData.TotalAngleShare / 2 + generalLoopData.CentralAngle / 2; // was previously expressed as 180 - (_vitalStatistics.AngleB + _vitalStatistics.AngleC)
            OriginToArcCentre = LineCalculator.GetAdjacentSide(OuterArmLength, generalLoopData.CentralAngle / 2);
            OuterArcRadius = LineCalculator.GetOppositeSide(OuterArmLength, generalLoopData.CentralAngle / 2);
            InnerArcRadius = (InnerArmLength > 0) ? LineCalculator.GetOppositeSide(InnerArmLength, generalLoopData.CentralAngle / 2) : 0;

            ActualArcCentre.PopulateFromLengthTopAngleAndStartPoint(
                OriginToArcCentre,
                generalLoopData.TotalAngleShare / 2 + generalLoopData.CentralAngle / 2,
                generalLoopData.Origin,
                RelativeArcCentre);

            if (InnerArmLength > 0)
            {
                // !! Caution !! The inner arc values are relative to the inner petal source, NOT to the Origin!
                ActualInnerArcStart.PopulateFromLengthTopAngleAndStartPoint(
                    InnerArmLength,
                    generalLoopData.TotalAngleShare / 2 + generalLoopData.CentralAngle,
                    generalLoopData.ActualInnerPetalSource,
                    RelativeInnerArcStart);

                ActualInnerArcEnd.PopulateFromLengthTopAngleAndStartPoint(
                    InnerArmLength,
                    generalLoopData.TotalAngleShare / 2,
                    generalLoopData.ActualInnerPetalSource,
                    RelativeInnerArcEnd);
            }

            ActualOuterArcStart.PopulateFromLengthTopAngleAndStartPoint(
                OuterArmLength,
                generalLoopData.TotalAngleShare / 2 + generalLoopData.CentralAngle,
                generalLoopData.Origin,
                RelativeOuterArcStart);

            ActualOuterArcEnd.PopulateFromLengthTopAngleAndStartPoint(
                OuterArmLength,
                generalLoopData.TotalAngleShare / 2,
                generalLoopData.Origin,
                RelativeOuterArcEnd);

            CalculateArcSpokeCoordinates();
        }

        private Region MakePetalRegion(TopGameLoop generalLoopData)
        {
            Region petalRegion = null;
            TopGameGraphicsPath petalPath = MakePetalPath();

            MakeObsoleteOuterAndInnerPath(generalLoopData);

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

        private void MakeObsoleteOuterAndInnerPath(TopGameLoop generalLoopData)
        {
            OuterPath.Reset();
            // We have to go from end to start instead of from start to end, because the arc gets drawn that way round.
            OuterPath.AddLine(generalLoopData.Origin, ActualOuterArcEnd);
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
            OuterPath.AddLine(ActualOuterArcStart, generalLoopData.Origin);

            if (InnerArmLength > 0)
            {
                InnerPath.Reset();
                // We have to go from end to start instead of from start to end, because the arc gets drawn that way round.
                InnerPath.AddLine(generalLoopData.ActualInnerPetalSource, ActualInnerArcEnd);

                // !! The y coordinate of the enclosing rectangle represents the TOP of the shape, not the bottom
                InnerArcSquare.X = ActualArcCentre.X - (int)Math.Round(InnerArcRadius, 0, MidpointRounding.AwayFromZero);
                InnerArcSquare.Y = ActualArcCentre.Y - (int)Math.Round(InnerArcRadius, 0, MidpointRounding.AwayFromZero);
                InnerArcSquare.Width = (int)Math.Round(InnerArcRadius * 2, 0, MidpointRounding.AwayFromZero);
                InnerArcSquare.Height = (int)Math.Round(InnerArcRadius * 2, 0, MidpointRounding.AwayFromZero);

                // See AddArcPath for explanation of how arcs are drawn.
                InnerPath.AddArcPath(InnerArcSquare, (float)ArcStartAngle, (float)180);
                InnerPath.AddLine(ActualInnerArcStart, generalLoopData.ActualInnerPetalSource);
            }
        }

        public void Clear()
        {
            OuterPath.Reset();
            InnerPath.Reset();
            if (ArcSpokes.Points.Any())
            {
                ArcSpokes.Points.Clear();
            }
        }
    }
}