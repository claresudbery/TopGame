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
            Origin = topGameGraphicsDataSource.GeneralLoopData.Origin.ToGoldenMasterPoint();

            RelativeInnerPetalSource = topGameGraphicsDataSource.GeneralLoopData.RelativeInnerPetalSource.ToGoldenMasterPoint();
            RelativeArcCentre = topGameGraphicsDataSource.TopGameArc.RelativeArcCentre.ToGoldenMasterPoint();

            RelativeInnerArcStart = topGameGraphicsDataSource.TopGameArc.RelativeInnerArcStart.ToGoldenMasterPoint();
            RelativeInnerArcEnd = topGameGraphicsDataSource.TopGameArc.RelativeInnerArcEnd.ToGoldenMasterPoint();

            RelativeOuterArcStart = topGameGraphicsDataSource.TopGameArc.RelativeOuterArcStart.ToGoldenMasterPoint();
            RelativeOuterArcEnd = topGameGraphicsDataSource.TopGameArc.RelativeOuterArcEnd.ToGoldenMasterPoint();

            ActualInnerPetalSource = topGameGraphicsDataSource.GeneralLoopData.ActualInnerPetalSource.ToGoldenMasterPoint();
            ActualInnerArcStart = topGameGraphicsDataSource.TopGameArc.ActualInnerArcStart.ToGoldenMasterPoint();
            ActualInnerArcEnd = topGameGraphicsDataSource.TopGameArc.ActualInnerArcEnd.ToGoldenMasterPoint();

            ActualArcCentre = topGameGraphicsDataSource.TopGameArc.ActualArcCentre.ToGoldenMasterPoint();
            ActualOuterArcStart = topGameGraphicsDataSource.TopGameArc.ActualOuterArcStart.ToGoldenMasterPoint();
            ActualOuterArcEnd = topGameGraphicsDataSource.TopGameArc.ActualOuterArcEnd.ToGoldenMasterPoint();

            OuterPath.Copy(topGameGraphicsDataSource.TopGameArc.OuterPath);
            InnerPath.Copy(topGameGraphicsDataSource.TopGameArc.InnerPath);

            StartArmDivisionStarts.Copy(topGameGraphicsDataSource.StartArmDivisionStarts);
            StartArmDivisionEnds.Copy(topGameGraphicsDataSource.StartArmDivisionEnds);
            EndArmDivisionStarts.Copy(topGameGraphicsDataSource.EndArmDivisionStarts);
            EndArmDivisionEnds.Copy(topGameGraphicsDataSource.EndArmDivisionEnds);
            ArcSpokes.Copy(topGameGraphicsDataSource.TopGameArc.ArcSpokes);

            InnerArcSquare.Copy(topGameGraphicsDataSource.TopGameArc.InnerArcSquare);
            OuterArcSquare.Copy(topGameGraphicsDataSource.TopGameArc.OuterArcSquare);

            MaxCentralAngle = topGameGraphicsDataSource.GeneralLoopData.MaxCentralAngle;
            MinimumAngleApplied = topGameGraphicsDataSource.GeneralLoopData.MinimumAngleApplied;
            MaximumAngleApplied = topGameGraphicsDataSource.GeneralLoopData.MaximumAngleApplied;
            OriginToArcCentre = topGameGraphicsDataSource.TopGameArc.OriginToArcCentre;
            CentralSpokeLength = topGameGraphicsDataSource.GeneralLoopData.CentralSpokeLength;
            InnerArcRadius = topGameGraphicsDataSource.TopGameArc.InnerArcRadius;
            OuterArcRadius = topGameGraphicsDataSource.TopGameArc.OuterArcRadius;
            OuterArmLength = topGameGraphicsDataSource.TopGameArc.OuterArmLength;
            InnerArmLength = topGameGraphicsDataSource.TopGameArc.InnerArmLength;
            ConstantSegmentLength = topGameGraphicsDataSource.GeneralLoopData.ArmSegmentLength;
            ConstantCentralSegmentLength = topGameGraphicsDataSource.ConstantCentralSegmentLength;
            ArcStartAngle = topGameGraphicsDataSource.TopGameArc.ArcStartAngle;
            AngleB = topGameGraphicsDataSource.AngleB;
            AngleC = topGameGraphicsDataSource.AngleC;
            ConstantBottomAngle = topGameGraphicsDataSource.GeneralLoopData.TotalAngleShare;
            CentralAngle = topGameGraphicsDataSource.GeneralLoopData.CentralAngle;
            ArcSegmentAngle = topGameGraphicsDataSource.TopGameArc.ArcSegmentAngle;
            NumArmSegments = topGameGraphicsDataSource.GeneralLoopData.NumArmSegments;
            NumArcSegments = topGameGraphicsDataSource.TopGameArc.NumArcSegments;
            NumTotalSegments = topGameGraphicsDataSource.GeneralLoopData.NumTotalSegments;
            NumTotalCardsInGame = topGameGraphicsDataSource.NumTotalCardsInGame;
            NumCardsInPlay = topGameGraphicsDataSource.NumCardsInPlay;
        }
    }
}