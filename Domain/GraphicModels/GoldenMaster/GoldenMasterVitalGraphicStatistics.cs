using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    public class GoldenMasterVitalGraphicStatistics
    {
        public GoldenMasterVitalGraphicStatistics(bool initialise = false)
        {
            origin = new GoldenMasterPoint();

            relativeInnerPetalSource = new GoldenMasterPoint();
            relativeArcCentre = new GoldenMasterPoint();

            relativeInnerArcStart = new GoldenMasterPoint();
            relativeInnerArcEnd = new GoldenMasterPoint();

            relativeOuterArcStart = new GoldenMasterPoint();
            relativeOuterArcEnd = new GoldenMasterPoint();

            actualInnerPetalSource = new GoldenMasterPoint();
            actualInnerArcStart = new GoldenMasterPoint();
            actualInnerArcEnd = new GoldenMasterPoint();

            actualArcCentre = new GoldenMasterPoint();
            actualOuterArcStart = new GoldenMasterPoint();
            actualOuterArcEnd = new GoldenMasterPoint();

            outerPath = new GoldenMasterGraphicsPath();
            innerPath = new GoldenMasterGraphicsPath();

            startArmDivisionStarts = new GoldenMasterPointCollection();
            startArmDivisionEnds = new GoldenMasterPointCollection();
            endArmDivisionStarts = new GoldenMasterPointCollection();
            endArmDivisionEnds = new GoldenMasterPointCollection();
            arcSpokes = new GoldenMasterPointCollection();

            innerArcSquare = new GoldenMasterRectangle();
            outerArcSquare = new GoldenMasterRectangle();
        }


        // ***********************************************************
        // Flags
        // ***********************************************************

        public bool bMinimumAngleApplied { get; set; }

        public bool bMaximumAngleApplied { get; set; }


        // ***********************************************************
        // Lengths
        // ***********************************************************

        public double originToArcCentre { get; set; }

        public double centralSpokeLength { get; set; }

        public double innerArcRadius { get; set; }

        public double outerArcRadius { get; set; }

        public double outerArmLength { get; set; }

        public double innerArmLength { get; set; }

        public double constantSegmentLength { get; set; }

        public double constantCentralSegmentLength { get; set; }


        // ***********************************************************
        // Angles
        // ***********************************************************

        public double maxCentralAngle { get; set; }

        public double arcStartAngle { get; set; }

        public double angleB { get; set; }

        public double angleC { get; set; }

        public double constantBottomAngle { get; set; }

        public double centralAngle { get; set; }

        public double arcSegmentAngle { get; set; }


        // ***********************************************************
        // Points
        // ***********************************************************

        public GoldenMasterPoint origin { get; set; }

        public GoldenMasterPoint relativeInnerPetalSource { get; set; }

        public GoldenMasterPoint relativeArcCentre { get; set; }

        public GoldenMasterPoint relativeInnerArcStart { get; set; }

        public GoldenMasterPoint relativeInnerArcEnd { get; set; }

        public GoldenMasterPoint relativeOuterArcStart { get; set; }

        public GoldenMasterPoint relativeOuterArcEnd { get; set; }

        public GoldenMasterPoint actualInnerPetalSource { get; set; }

        public GoldenMasterPoint actualInnerArcStart { get; set; }

        public GoldenMasterPoint actualInnerArcEnd { get; set; }

        public GoldenMasterPoint actualArcCentre { get; set; }

        public GoldenMasterPoint actualOuterArcStart { get; set; }

        public GoldenMasterPoint actualOuterArcEnd { get; set; }


        // ***********************************************************
        // Counts
        // ***********************************************************

        public int numArmSegments { get; set; }

        public int numArcSegments { get; set; }

        public int numTotalSegments { get; set; }

        public int numTotalCardsInGame { get; set; }

        public int numCardsInPlay { get; set; }


        // ***********************************************************
        // Paths
        // ***********************************************************

        public GoldenMasterGraphicsPath outerPath { get; set; }

        public GoldenMasterGraphicsPath innerPath { get; set; }


        // ***********************************************************
        // Divisions
        // ***********************************************************

        public GoldenMasterPointCollection startArmDivisionStarts { get; set; }

        public GoldenMasterPointCollection startArmDivisionEnds { get; set; }

        public GoldenMasterPointCollection endArmDivisionStarts { get; set; }

        public GoldenMasterPointCollection endArmDivisionEnds { get; set; }

        public GoldenMasterPointCollection arcSpokes { get; set; }


        // ***********************************************************
        // Squares
        // ***********************************************************

        public GoldenMasterRectangle innerArcSquare { get; set; }

        public GoldenMasterRectangle outerArcSquare { get; set; }


        public void Copy(VitalStatistics vitalStatisticsSource)
        {
            origin = vitalStatisticsSource.origin.ToGoldenMasterPoint();

            relativeInnerPetalSource = vitalStatisticsSource.relativeInnerPetalSource.ToGoldenMasterPoint();
            relativeArcCentre = vitalStatisticsSource.relativeArcCentre.ToGoldenMasterPoint();

            relativeInnerArcStart = vitalStatisticsSource.relativeInnerArcStart.ToGoldenMasterPoint();
            relativeInnerArcEnd = vitalStatisticsSource.relativeInnerArcEnd.ToGoldenMasterPoint();

            relativeOuterArcStart = vitalStatisticsSource.relativeOuterArcStart.ToGoldenMasterPoint();
            relativeOuterArcEnd = vitalStatisticsSource.relativeOuterArcEnd.ToGoldenMasterPoint();

            actualInnerPetalSource = vitalStatisticsSource.actualInnerPetalSource.ToGoldenMasterPoint();
            actualInnerArcStart = vitalStatisticsSource.actualInnerArcStart.ToGoldenMasterPoint();
            actualInnerArcEnd = vitalStatisticsSource.actualInnerArcEnd.ToGoldenMasterPoint();

            actualArcCentre = vitalStatisticsSource.actualArcCentre.ToGoldenMasterPoint();
            actualOuterArcStart = vitalStatisticsSource.actualOuterArcStart.ToGoldenMasterPoint();
            actualOuterArcEnd = vitalStatisticsSource.actualOuterArcEnd.ToGoldenMasterPoint();

            outerPath.Copy(vitalStatisticsSource.outerPath);
            innerPath.Copy(vitalStatisticsSource.innerPath);

            startArmDivisionStarts.Copy(vitalStatisticsSource.startArmDivisionStarts);
            startArmDivisionEnds.Copy(vitalStatisticsSource.startArmDivisionEnds);
            endArmDivisionStarts.Copy(vitalStatisticsSource.endArmDivisionStarts);
            endArmDivisionEnds.Copy(vitalStatisticsSource.endArmDivisionEnds);
            arcSpokes.Copy(vitalStatisticsSource.arcSpokes);

            innerArcSquare.Copy(vitalStatisticsSource.innerArcSquare);
            outerArcSquare.Copy(vitalStatisticsSource.outerArcSquare);

            maxCentralAngle = vitalStatisticsSource.maxCentralAngle;
            bMinimumAngleApplied = vitalStatisticsSource.bMinimumAngleApplied;
            bMaximumAngleApplied = vitalStatisticsSource.bMaximumAngleApplied;
            originToArcCentre = vitalStatisticsSource.originToArcCentre;
            centralSpokeLength = vitalStatisticsSource.centralSpokeLength;
            innerArcRadius = vitalStatisticsSource.innerArcRadius;
            outerArcRadius = vitalStatisticsSource.outerArcRadius;
            outerArmLength = vitalStatisticsSource.outerArmLength;
            innerArmLength = vitalStatisticsSource.innerArmLength;
            constantSegmentLength = vitalStatisticsSource.constantSegmentLength;
            constantCentralSegmentLength = vitalStatisticsSource.constantCentralSegmentLength;
            arcStartAngle = vitalStatisticsSource.arcStartAngle;
            angleB = vitalStatisticsSource.angleB;
            angleC = vitalStatisticsSource.angleC;
            constantBottomAngle = vitalStatisticsSource.constantBottomAngle;
            centralAngle = vitalStatisticsSource.centralAngle;
            arcSegmentAngle = vitalStatisticsSource.arcSegmentAngle;
            numArmSegments = vitalStatisticsSource.numArmSegments;
            numArcSegments = vitalStatisticsSource.numArcSegments;
            numTotalSegments = vitalStatisticsSource.numTotalSegments;
            numTotalCardsInGame = vitalStatisticsSource.numTotalCardsInGame;
            numCardsInPlay = vitalStatisticsSource.numCardsInPlay;
        }
    }
}