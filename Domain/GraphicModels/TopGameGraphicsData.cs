using TopGameWindowsApp;

namespace Domain.GraphicModels
{
    public class TopGameGraphicsData
    {
        public TopGameGraphicsData()
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

            InitialiseValues();
        }

        private void InitialiseValues()
        {
            InitialiseValuesFromConstants();

            MinimumAngleApplied = false;
            MaximumAngleApplied = false;

            CalculateCentralAngle(TopGameConstants.AnglesInCircle, NumTotalCardsInGame, false);
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

        private void InitialiseValuesFromConstants()
        {
            Origin.X = TopGameConstants.OriginX;
            Origin.Y = TopGameConstants.OriginY;
            ArmSegmentLength = TopGameConstants.ConstantSegmentLength;
            NumTotalCardsInGame = TopGameConstants.NumCardsInStandardPack;
            // Defunct:
            ConstantCentralSegmentLength = TopGameConstants.ConstantSegmentLength;
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

        public double ArmSegmentLength { get; set; }

        public double ConstantCentralSegmentLength { get; set; }


        // ***********************************************************
        // Angles
        // ***********************************************************

        public double MaxCentralAngle { get; set; }

        public double ArcStartAngle { get; set; }

        public double TotalAngleShare { get; set; }

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


        // ***********************************************************
        // Obsolete
        // ***********************************************************

        public double AngleB { get; set; }

        public double AngleC { get; set; }


        public void Copy(TopGameGraphicsData topGameGraphicsDataSource)
        {
            Origin = topGameGraphicsDataSource.Origin;

            RelativeInnerPetalSource = topGameGraphicsDataSource.RelativeInnerPetalSource;
            RelativeArcCentre = topGameGraphicsDataSource.RelativeArcCentre;

            RelativeInnerArcStart = topGameGraphicsDataSource.RelativeInnerArcStart;
            RelativeInnerArcEnd = topGameGraphicsDataSource.RelativeInnerArcEnd;

            RelativeOuterArcStart = topGameGraphicsDataSource.RelativeOuterArcStart;
            RelativeOuterArcEnd = topGameGraphicsDataSource.RelativeOuterArcEnd;

            ActualInnerPetalSource = topGameGraphicsDataSource.ActualInnerPetalSource;
            ActualInnerArcStart = topGameGraphicsDataSource.ActualInnerArcStart;
            ActualInnerArcEnd = topGameGraphicsDataSource.ActualInnerArcEnd;

            ActualArcCentre = topGameGraphicsDataSource.ActualArcCentre;
            ActualOuterArcStart = topGameGraphicsDataSource.ActualOuterArcStart;
            ActualOuterArcEnd = topGameGraphicsDataSource.ActualOuterArcEnd;

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
            ArmSegmentLength = topGameGraphicsDataSource.ArmSegmentLength;
            ConstantCentralSegmentLength = topGameGraphicsDataSource.ConstantCentralSegmentLength;
            ArcStartAngle = topGameGraphicsDataSource.ArcStartAngle;
            AngleB = topGameGraphicsDataSource.AngleB;
            AngleC = topGameGraphicsDataSource.AngleC;
            TotalAngleShare = topGameGraphicsDataSource.TotalAngleShare;
            CentralAngle = topGameGraphicsDataSource.CentralAngle;
            ArcSegmentAngle = topGameGraphicsDataSource.ArcSegmentAngle;
            NumArmSegments = topGameGraphicsDataSource.NumArmSegments;
            NumArcSegments = topGameGraphicsDataSource.NumArcSegments;
            NumTotalSegments = topGameGraphicsDataSource.NumTotalSegments;
            NumTotalCardsInGame = topGameGraphicsDataSource.NumTotalCardsInGame;
            NumCardsInPlay = topGameGraphicsDataSource.NumCardsInPlay;
        }

        public void Dispose()
        {
            OuterPath.ActualPath.Dispose();
            InnerPath.ActualPath.Dispose();
        }
    }
}