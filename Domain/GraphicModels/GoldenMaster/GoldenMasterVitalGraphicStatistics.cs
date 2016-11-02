using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    public class GoldenMasterVitalGraphicStatistics
    {
        public GoldenMasterVitalGraphicStatistics(bool initialise = false)
        {
            Origin = new GoldenMasterPoint();

            RelativeInnerPetalSource = new GoldenMasterPoint();
            RelativeArcCentre = new GoldenMasterPoint();

            RelativeInnerArcStart = new GoldenMasterPoint();
            RelativeInnerArcEnd = new GoldenMasterPoint();

            RelativeOuterArcStart = new GoldenMasterPoint();
            RelativeOuterArcEnd = new GoldenMasterPoint();

            ActualInnerPetalSource = new GoldenMasterPoint();
            ActualInnerArcStart = new GoldenMasterPoint();
            ActualInnerArcEnd = new GoldenMasterPoint();

            ActualArcCentre = new GoldenMasterPoint();
            ActualOuterArcStart = new GoldenMasterPoint();
            ActualOuterArcEnd = new GoldenMasterPoint();

            OuterPath = new GoldenMasterGraphicsPath();
            InnerPath = new GoldenMasterGraphicsPath();

            StartArmDivisionStarts = new GoldenMasterPointCollection();
            StartArmDivisionEnds = new GoldenMasterPointCollection();
            EndArmDivisionStarts = new GoldenMasterPointCollection();
            EndArmDivisionEnds = new GoldenMasterPointCollection();
            ArcSpokes = new GoldenMasterPointCollection();

            InnerArcSquare = new GoldenMasterRectangle();
            OuterArcSquare = new GoldenMasterRectangle();
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

        public double ConstantSegmentLength { get; set; }

        public double ConstantCentralSegmentLength { get; set; }


        // ***********************************************************
        // Angles
        // ***********************************************************

        public double MaxCentralAngle { get; set; }

        public double ArcStartAngle { get; set; }

        public double AngleB { get; set; }

        public double AngleC { get; set; }

        public double ConstantBottomAngle { get; set; }

        public double CentralAngle { get; set; }

        public double ArcSegmentAngle { get; set; }


        // ***********************************************************
        // Points
        // ***********************************************************

        public GoldenMasterPoint Origin { get; set; }

        public GoldenMasterPoint RelativeInnerPetalSource { get; set; }

        public GoldenMasterPoint RelativeArcCentre { get; set; }

        public GoldenMasterPoint RelativeInnerArcStart { get; set; }

        public GoldenMasterPoint RelativeInnerArcEnd { get; set; }

        public GoldenMasterPoint RelativeOuterArcStart { get; set; }

        public GoldenMasterPoint RelativeOuterArcEnd { get; set; }

        public GoldenMasterPoint ActualInnerPetalSource { get; set; }

        public GoldenMasterPoint ActualInnerArcStart { get; set; }

        public GoldenMasterPoint ActualInnerArcEnd { get; set; }

        public GoldenMasterPoint ActualArcCentre { get; set; }

        public GoldenMasterPoint ActualOuterArcStart { get; set; }

        public GoldenMasterPoint ActualOuterArcEnd { get; set; }


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

        public GoldenMasterGraphicsPath OuterPath { get; set; }

        public GoldenMasterGraphicsPath InnerPath { get; set; }


        // ***********************************************************
        // Divisions
        // ***********************************************************

        public GoldenMasterPointCollection StartArmDivisionStarts { get; set; }

        public GoldenMasterPointCollection StartArmDivisionEnds { get; set; }

        public GoldenMasterPointCollection EndArmDivisionStarts { get; set; }

        public GoldenMasterPointCollection EndArmDivisionEnds { get; set; }

        public GoldenMasterPointCollection ArcSpokes { get; set; }


        // ***********************************************************
        // Squares
        // ***********************************************************

        public GoldenMasterRectangle InnerArcSquare { get; set; }

        public GoldenMasterRectangle OuterArcSquare { get; set; }


        public void Copy(VitalStatistics vitalStatisticsSource)
        {
            Origin = vitalStatisticsSource.Origin.ToGoldenMasterPoint();

            RelativeInnerPetalSource = vitalStatisticsSource.RelativeInnerPetalSource.ToGoldenMasterPoint();
            RelativeArcCentre = vitalStatisticsSource.RelativeArcCentre.ToGoldenMasterPoint();

            RelativeInnerArcStart = vitalStatisticsSource.RelativeInnerArcStart.ToGoldenMasterPoint();
            RelativeInnerArcEnd = vitalStatisticsSource.RelativeInnerArcEnd.ToGoldenMasterPoint();

            RelativeOuterArcStart = vitalStatisticsSource.RelativeOuterArcStart.ToGoldenMasterPoint();
            RelativeOuterArcEnd = vitalStatisticsSource.RelativeOuterArcEnd.ToGoldenMasterPoint();

            ActualInnerPetalSource = vitalStatisticsSource.ActualInnerPetalSource.ToGoldenMasterPoint();
            ActualInnerArcStart = vitalStatisticsSource.ActualInnerArcStart.ToGoldenMasterPoint();
            ActualInnerArcEnd = vitalStatisticsSource.ActualInnerArcEnd.ToGoldenMasterPoint();

            ActualArcCentre = vitalStatisticsSource.ActualArcCentre.ToGoldenMasterPoint();
            ActualOuterArcStart = vitalStatisticsSource.ActualOuterArcStart.ToGoldenMasterPoint();
            ActualOuterArcEnd = vitalStatisticsSource.ActualOuterArcEnd.ToGoldenMasterPoint();

            OuterPath.Copy(vitalStatisticsSource.OuterPath);
            InnerPath.Copy(vitalStatisticsSource.InnerPath);

            StartArmDivisionStarts.Copy(vitalStatisticsSource.StartArmDivisionStarts);
            StartArmDivisionEnds.Copy(vitalStatisticsSource.StartArmDivisionEnds);
            EndArmDivisionStarts.Copy(vitalStatisticsSource.EndArmDivisionStarts);
            EndArmDivisionEnds.Copy(vitalStatisticsSource.EndArmDivisionEnds);
            ArcSpokes.Copy(vitalStatisticsSource.ArcSpokes);

            InnerArcSquare.Copy(vitalStatisticsSource.InnerArcSquare);
            OuterArcSquare.Copy(vitalStatisticsSource.OuterArcSquare);

            MaxCentralAngle = vitalStatisticsSource.MaxCentralAngle;
            MinimumAngleApplied = vitalStatisticsSource.MinimumAngleApplied;
            MaximumAngleApplied = vitalStatisticsSource.MaximumAngleApplied;
            OriginToArcCentre = vitalStatisticsSource.OriginToArcCentre;
            CentralSpokeLength = vitalStatisticsSource.CentralSpokeLength;
            InnerArcRadius = vitalStatisticsSource.InnerArcRadius;
            OuterArcRadius = vitalStatisticsSource.OuterArcRadius;
            OuterArmLength = vitalStatisticsSource.OuterArmLength;
            InnerArmLength = vitalStatisticsSource.InnerArmLength;
            ConstantSegmentLength = vitalStatisticsSource.ArmSegmentLength;
            ConstantCentralSegmentLength = vitalStatisticsSource.ConstantCentralSegmentLength;
            ArcStartAngle = vitalStatisticsSource.ArcStartAngle;
            AngleB = vitalStatisticsSource.AngleB;
            AngleC = vitalStatisticsSource.AngleC;
            ConstantBottomAngle = vitalStatisticsSource.TotalAngleShare;
            CentralAngle = vitalStatisticsSource.CentralAngle;
            ArcSegmentAngle = vitalStatisticsSource.ArcSegmentAngle;
            NumArmSegments = vitalStatisticsSource.NumArmSegments;
            NumArcSegments = vitalStatisticsSource.NumArcSegments;
            NumTotalSegments = vitalStatisticsSource.NumTotalSegments;
            NumTotalCardsInGame = vitalStatisticsSource.NumTotalCardsInGame;
            NumCardsInPlay = vitalStatisticsSource.NumCardsInPlay;
        }
    }
}