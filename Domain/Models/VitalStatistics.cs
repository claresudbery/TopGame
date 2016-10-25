using Newtonsoft.Json;

namespace Domain.Models
{
    public class VitalStatistics
    {
        public VitalStatistics(bool initialise = false)
        {
            origin = new TopGamePoint();

            relativeInnerPetalSource = new TopGamePoint();
            relativeArcCentre = new TopGamePoint();

            relativeInnerArcStart = new TopGamePoint();
            relativeInnerArcEnd = new TopGamePoint();

            relativeOuterArcStart = new TopGamePoint();
            relativeOuterArcEnd = new TopGamePoint();

            actualInnerPetalSource = new TopGamePoint();
            actualInnerArcStart = new TopGamePoint();
            actualInnerArcEnd = new TopGamePoint();

            actualArcCentre = new TopGamePoint();
            actualOuterArcStart = new TopGamePoint();
            actualOuterArcEnd = new TopGamePoint();

            outerPath = new TopGameGraphicsPath();
            innerPath = new TopGameGraphicsPath();

            startArmDivisionStarts = new TopGamePointCollection();
            startArmDivisionEnds = new TopGamePointCollection();
            endArmDivisionStarts = new TopGamePointCollection();
            endArmDivisionEnds = new TopGamePointCollection();
            arcSpokes = new TopGamePointCollection();

            innerArcSquare = new TopGameRectangle();
            outerArcSquare = new TopGameRectangle();

            if (initialise)
            {
                // The json deserialisation tests fail for some reason on the ShouldAllBeEquivalentTo assertion if the GraphicsPath objects are newed up on creation.
                // So we only new them up in production code.
                outerPath.Initialise();
                innerPath.Initialise();
            }
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

        public TopGamePoint origin { get; set; }

        public TopGamePoint relativeInnerPetalSource { get; set; }

        public TopGamePoint relativeArcCentre { get; set; }

        public TopGamePoint relativeInnerArcStart { get; set; }

        public TopGamePoint relativeInnerArcEnd { get; set; }

        public TopGamePoint relativeOuterArcStart { get; set; }

        public TopGamePoint relativeOuterArcEnd { get; set; }

        public TopGamePoint actualInnerPetalSource { get; set; }

        public TopGamePoint actualInnerArcStart { get; set; }

        public TopGamePoint actualInnerArcEnd { get; set; }

        public TopGamePoint actualArcCentre { get; set; }

        public TopGamePoint actualOuterArcStart { get; set; }

        public TopGamePoint actualOuterArcEnd { get; set; }


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
        
        public TopGameGraphicsPath outerPath { get; set; }
        
        public TopGameGraphicsPath innerPath { get; set; }


        // ***********************************************************
        // Divisions
        // ***********************************************************
        
        public TopGamePointCollection startArmDivisionStarts { get; set; }
        
        public TopGamePointCollection startArmDivisionEnds { get; set; }

        public TopGamePointCollection endArmDivisionStarts { get; set; }
        
        public TopGamePointCollection endArmDivisionEnds { get; set; }
        
        public TopGamePointCollection arcSpokes { get; set; }


        // ***********************************************************
        // Squares
        // ***********************************************************
        
        public TopGameRectangle innerArcSquare { get; set; }
        
        public TopGameRectangle outerArcSquare { get; set; }


        public void Copy(VitalStatistics vitalStatisticsSource)
        {
            origin = vitalStatisticsSource.origin;

            relativeInnerPetalSource = vitalStatisticsSource.relativeInnerPetalSource;
            relativeArcCentre = vitalStatisticsSource.relativeArcCentre;

            relativeInnerArcStart = vitalStatisticsSource.relativeInnerArcStart;
            relativeInnerArcEnd = vitalStatisticsSource.relativeInnerArcEnd;

            relativeOuterArcStart = vitalStatisticsSource.relativeOuterArcStart;
            relativeOuterArcEnd = vitalStatisticsSource.relativeOuterArcEnd;

            actualInnerPetalSource = vitalStatisticsSource.actualInnerPetalSource;
            actualInnerArcStart = vitalStatisticsSource.actualInnerArcStart;
            actualInnerArcEnd = vitalStatisticsSource.actualInnerArcEnd;

            actualArcCentre = vitalStatisticsSource.actualArcCentre;
            actualOuterArcStart = vitalStatisticsSource.actualOuterArcStart;
            actualOuterArcEnd = vitalStatisticsSource.actualOuterArcEnd;

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