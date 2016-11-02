namespace Domain.GraphicModels
{
    public class VitalStatistics
    {
        public VitalStatistics()
        {
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

        public double SegmentLength { get; set; }

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


        public void Copy(VitalStatistics vitalStatisticsSource)
        {
            Origin = vitalStatisticsSource.Origin;

            RelativeInnerPetalSource = vitalStatisticsSource.RelativeInnerPetalSource;
            RelativeArcCentre = vitalStatisticsSource.RelativeArcCentre;

            RelativeInnerArcStart = vitalStatisticsSource.RelativeInnerArcStart;
            RelativeInnerArcEnd = vitalStatisticsSource.RelativeInnerArcEnd;

            RelativeOuterArcStart = vitalStatisticsSource.RelativeOuterArcStart;
            RelativeOuterArcEnd = vitalStatisticsSource.RelativeOuterArcEnd;

            ActualInnerPetalSource = vitalStatisticsSource.ActualInnerPetalSource;
            ActualInnerArcStart = vitalStatisticsSource.ActualInnerArcStart;
            ActualInnerArcEnd = vitalStatisticsSource.ActualInnerArcEnd;

            ActualArcCentre = vitalStatisticsSource.ActualArcCentre;
            ActualOuterArcStart = vitalStatisticsSource.ActualOuterArcStart;
            ActualOuterArcEnd = vitalStatisticsSource.ActualOuterArcEnd;

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
            SegmentLength = vitalStatisticsSource.SegmentLength;
            ConstantCentralSegmentLength = vitalStatisticsSource.ConstantCentralSegmentLength;
            ArcStartAngle = vitalStatisticsSource.ArcStartAngle;
            AngleB = vitalStatisticsSource.AngleB;
            AngleC = vitalStatisticsSource.AngleC;
            ConstantBottomAngle = vitalStatisticsSource.ConstantBottomAngle;
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