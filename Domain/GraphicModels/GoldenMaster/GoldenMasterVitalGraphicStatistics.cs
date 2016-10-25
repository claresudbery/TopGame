using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
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

        [JsonProperty]
        public bool bMinimumAngleApplied { get; set; }

        [JsonProperty]
        public bool bMaximumAngleApplied { get; set; }


        // ***********************************************************
        // Lengths
        // ***********************************************************

        [JsonProperty]
        public double originToArcCentre { get; set; }

        [JsonProperty]
        public double centralSpokeLength { get; set; }

        [JsonProperty]
        public double innerArcRadius { get; set; }

        [JsonProperty]
        public double outerArcRadius { get; set; }

        [JsonProperty]
        public double outerArmLength { get; set; }

        [JsonProperty]
        public double innerArmLength { get; set; }

        [JsonProperty]
        public double constantSegmentLength { get; set; }

        [JsonProperty]
        public double constantCentralSegmentLength { get; set; }


        // ***********************************************************
        // Angles
        // ***********************************************************

        [JsonProperty]
        public double maxCentralAngle { get; set; }

        [JsonProperty]
        public double arcStartAngle { get; set; }

        [JsonProperty]
        public double angleB { get; set; }

        [JsonProperty]
        public double angleC { get; set; }

        [JsonProperty]
        public double constantBottomAngle { get; set; }

        [JsonProperty]
        public double centralAngle { get; set; }

        [JsonProperty]
        public double arcSegmentAngle { get; set; }


        // ***********************************************************
        // Points
        // ***********************************************************

        [JsonProperty]
        public GoldenMasterPoint origin { get; set; }

        [JsonProperty]
        public GoldenMasterPoint relativeInnerPetalSource { get; set; }

        [JsonProperty]
        public GoldenMasterPoint relativeArcCentre { get; set; }

        [JsonProperty]
        public GoldenMasterPoint relativeInnerArcStart { get; set; }

        [JsonProperty]
        public GoldenMasterPoint relativeInnerArcEnd { get; set; }

        [JsonProperty]
        public GoldenMasterPoint relativeOuterArcStart { get; set; }

        [JsonProperty]
        public GoldenMasterPoint relativeOuterArcEnd { get; set; }

        [JsonProperty]
        public GoldenMasterPoint actualInnerPetalSource { get; set; }

        [JsonProperty]
        public GoldenMasterPoint actualInnerArcStart { get; set; }

        [JsonProperty]
        public GoldenMasterPoint actualInnerArcEnd { get; set; }

        [JsonProperty]
        public GoldenMasterPoint actualArcCentre { get; set; }

        [JsonProperty]
        public GoldenMasterPoint actualOuterArcStart { get; set; }

        [JsonProperty]
        public GoldenMasterPoint actualOuterArcEnd { get; set; }


        // ***********************************************************
        // Counts
        // ***********************************************************

        [JsonProperty]
        public int numArmSegments { get; set; }

        [JsonProperty]
        public int numArcSegments { get; set; }

        [JsonProperty]
        public int numTotalSegments { get; set; }

        [JsonProperty]
        public int numTotalCardsInGame { get; set; }

        [JsonProperty]
        public int numCardsInPlay { get; set; }


        // ***********************************************************
        // Paths
        // ***********************************************************

        [JsonProperty]
        public GoldenMasterGraphicsPath outerPath { get; set; }

        [JsonProperty]
        public GoldenMasterGraphicsPath innerPath { get; set; }


        // ***********************************************************
        // Divisions
        // ***********************************************************

        [JsonProperty]
        public GoldenMasterPointCollection startArmDivisionStarts { get; set; }

        [JsonProperty]
        public GoldenMasterPointCollection startArmDivisionEnds { get; set; }

        [JsonProperty]
        public GoldenMasterPointCollection endArmDivisionStarts { get; set; }

        [JsonProperty]
        public GoldenMasterPointCollection endArmDivisionEnds { get; set; }

        [JsonProperty]
        public GoldenMasterPointCollection arcSpokes { get; set; }


        // ***********************************************************
        // Squares
        // ***********************************************************

        [JsonProperty]
        public GoldenMasterRectangle innerArcSquare { get; set; }

        [JsonProperty]
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