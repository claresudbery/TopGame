using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TopGameWindowsApp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class VitalStatistics
    {
        public VitalStatistics()
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
        public TopGamePoint origin { get; set; }

        [JsonProperty]
        public TopGamePoint relativeInnerPetalSource { get; set; }

        [JsonProperty]
        public TopGamePoint relativeArcCentre { get; set; }

        [JsonProperty]
        public TopGamePoint relativeInnerArcStart { get; set; }

        [JsonProperty]
        public TopGamePoint relativeInnerArcEnd { get; set; }

        [JsonProperty]
        public TopGamePoint relativeOuterArcStart { get; set; }

        [JsonProperty]
        public TopGamePoint relativeOuterArcEnd { get; set; }

        [JsonProperty]
        public TopGamePoint actualInnerPetalSource { get; set; }

        [JsonProperty]
        public TopGamePoint actualInnerArcStart { get; set; }

        [JsonProperty]
        public TopGamePoint actualInnerArcEnd { get; set; }

        [JsonProperty]
        public TopGamePoint actualArcCentre { get; set; }

        [JsonProperty]
        public TopGamePoint actualOuterArcStart { get; set; }

        [JsonProperty]
        public TopGamePoint actualOuterArcEnd { get; set; }


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
        public virtual TopGameGraphicsPath outerPath { get; set; }
        
        [JsonProperty]
        public virtual TopGameGraphicsPath innerPath { get; set; }


        // ***********************************************************
        // Divisions
        // ***********************************************************
        
        [JsonProperty]
        public virtual TopGamePointCollection startArmDivisionStarts { get; set; }
        
        [JsonProperty]
        public virtual TopGamePointCollection startArmDivisionEnds { get; set; }

        [JsonProperty]
        public virtual TopGamePointCollection endArmDivisionStarts { get; set; }
        
        [JsonProperty]
        public virtual TopGamePointCollection endArmDivisionEnds { get; set; }
        
        [JsonProperty]
        public virtual TopGamePointCollection arcSpokes { get; set; }


        // ***********************************************************
        // Squares
        // ***********************************************************
        
        [JsonProperty]
        public virtual TopGameRectangle innerArcSquare { get; set; }
        
        [JsonProperty]
        public virtual TopGameRectangle outerArcSquare { get; set; }


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