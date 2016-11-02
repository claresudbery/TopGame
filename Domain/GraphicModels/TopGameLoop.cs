using TopGameWindowsApp;
using Domain.Extensions;

namespace Domain.GraphicModels
{
    public class TopGameLoop
    {
        public TopGameLoop()
        {
            Origin = new TopGamePoint();
            ActualInnerPetalSource = new TopGamePoint();
            RelativeInnerPetalSource = new TopGamePoint();

            MinimumAngleApplied = false;
            MaximumAngleApplied = false;
            ArmSegmentLength = TopGameConstants.ConstantSegmentLength;
            Origin.X = TopGameConstants.OriginX;
            Origin.Y = TopGameConstants.OriginY;
        }

        public TopGamePoint Origin { get; set; }

        public int NumTotalSegments { get; set; }

        public double TotalAngleShare { get; set; }

        public double CentralAngle { get; set; }

        public int NumArmSegments { get; set; }

        public bool MinimumAngleApplied { get; set; }

        public bool MaximumAngleApplied { get; set; }

        public double MaxCentralAngle { get; set; }

        public double ArmSegmentLength { get; set; }

        public TopGamePoint RelativeInnerPetalSource { get; set; }

        public TopGamePoint ActualInnerPetalSource { get; set; }

        public double CentralSpokeLength { get; set; }

        public bool ArcWillExist()
        {
            return NumTotalSegments > 2;
        }

        public int NumSegmentsContainedInArmsAndArc()
        {
            return NumTotalSegments - 2;
        }

        public void SetTotalAngleShare(double newTotalAngleShare)
        {
            TotalAngleShare = newTotalAngleShare;
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

        public void Clear()
        {
            NumTotalSegments = 0;
            CentralAngle = 0;
        }

        public void Copy(TopGameLoop topGameLoopSource)
        {
            TotalAngleShare = topGameLoopSource.TotalAngleShare;
            NumTotalSegments = topGameLoopSource.NumTotalSegments;
            CentralAngle = topGameLoopSource.CentralAngle;
            NumArmSegments = topGameLoopSource.NumArmSegments;
            MinimumAngleApplied = topGameLoopSource.MinimumAngleApplied;
            MaximumAngleApplied = topGameLoopSource.MaximumAngleApplied;
            MaxCentralAngle = topGameLoopSource.MaxCentralAngle;
            ArmSegmentLength = topGameLoopSource.ArmSegmentLength;
            Origin = topGameLoopSource.Origin;
            RelativeInnerPetalSource = topGameLoopSource.RelativeInnerPetalSource;
            ActualInnerPetalSource = topGameLoopSource.ActualInnerPetalSource;
            CentralSpokeLength = topGameLoopSource.CentralSpokeLength;
        }

        public void CalculateGeneralValues()
        {
            NumArmSegments = ArcWillExist() ? NumSegmentsContainedInArmsAndArc().DividedBy3() : 0;
            CalculateArmSegmentLength();
            CentralSpokeLength = LineCalculator.GetAdjacentSide(ArmSegmentLength, CentralAngle / 2);

            ActualInnerPetalSource.PopulateFromLengthTopAngleAndStartPoint(
                CentralSpokeLength,
                TotalAngleShare / 2 + CentralAngle / 2,
                Origin,
                RelativeInnerPetalSource);
        }

        public void CalculateArmSegmentLength()
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

        private int MaximumArmLengthWhichFitsInFrame()
        {
            return Origin.Y - 70;
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
    }
}