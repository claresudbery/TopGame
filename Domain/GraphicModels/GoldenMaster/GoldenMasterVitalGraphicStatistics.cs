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


        public void Copy(TopGameGraphicsData topGameGraphicsDataSource)
        {
            Origin = topGameGraphicsDataSource.Origin.ToGoldenMasterPoint();

            RelativeInnerPetalSource = topGameGraphicsDataSource.RelativeInnerPetalSource.ToGoldenMasterPoint();
            RelativeArcCentre = topGameGraphicsDataSource.RelativeArcCentre.ToGoldenMasterPoint();

            RelativeInnerArcStart = topGameGraphicsDataSource.RelativeInnerArcStart.ToGoldenMasterPoint();
            RelativeInnerArcEnd = topGameGraphicsDataSource.RelativeInnerArcEnd.ToGoldenMasterPoint();

            RelativeOuterArcStart = topGameGraphicsDataSource.RelativeOuterArcStart.ToGoldenMasterPoint();
            RelativeOuterArcEnd = topGameGraphicsDataSource.RelativeOuterArcEnd.ToGoldenMasterPoint();

            ActualInnerPetalSource = topGameGraphicsDataSource.ActualInnerPetalSource.ToGoldenMasterPoint();
            ActualInnerArcStart = topGameGraphicsDataSource.ActualInnerArcStart.ToGoldenMasterPoint();
            ActualInnerArcEnd = topGameGraphicsDataSource.ActualInnerArcEnd.ToGoldenMasterPoint();

            ActualArcCentre = topGameGraphicsDataSource.ActualArcCentre.ToGoldenMasterPoint();
            ActualOuterArcStart = topGameGraphicsDataSource.ActualOuterArcStart.ToGoldenMasterPoint();
            ActualOuterArcEnd = topGameGraphicsDataSource.ActualOuterArcEnd.ToGoldenMasterPoint();

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
            ConstantSegmentLength = topGameGraphicsDataSource.ArmSegmentLength;
            ConstantCentralSegmentLength = topGameGraphicsDataSource.ConstantCentralSegmentLength;
            ArcStartAngle = topGameGraphicsDataSource.ArcStartAngle;
            AngleB = topGameGraphicsDataSource.AngleB;
            AngleC = topGameGraphicsDataSource.AngleC;
            ConstantBottomAngle = topGameGraphicsDataSource.TotalAngleShare;
            CentralAngle = topGameGraphicsDataSource.CentralAngle;
            ArcSegmentAngle = topGameGraphicsDataSource.ArcSegmentAngle;
            NumArmSegments = topGameGraphicsDataSource.NumArmSegments;
            NumArcSegments = topGameGraphicsDataSource.NumArcSegments;
            NumTotalSegments = topGameGraphicsDataSource.NumTotalSegments;
            NumTotalCardsInGame = topGameGraphicsDataSource.NumTotalCardsInGame;
            NumCardsInPlay = topGameGraphicsDataSource.NumCardsInPlay;
        }
    }
}