using System;
using System.Drawing;
using Domain.GraphicModels.GoldenMaster;

namespace Domain.GraphicModels
{
    public class TopGamePoint
    {
        public TopGamePoint()
        {
            // Do nothing
        }

        public TopGamePoint(int x, int y)
        {
            _actualPoint.X = x;
            _actualPoint.Y = y;
        }
        
        public int X
        {
            get { return _actualPoint.X; }
            set { _actualPoint.X = value; }
        }
        
        public int Y
        {
            get { return _actualPoint.Y; }
            set { _actualPoint.Y = value; }
        }

        public Point Point
        {
            get { return _actualPoint; }
        }

        private Point _actualPoint = new Point();

        public GoldenMasterPoint ToGoldenMasterPoint()
        {
            return new GoldenMasterPoint(X, Y);
        }

        public void PopulateFromLengthTopAngleAndStartPoint(
            double length, 
            double topAngle, 
            TopGamePoint startPoint,
            TopGamePoint tempRelativePoint) // Get rid of tempRelativePoint! It's only here for golden master purposes.
        {
            TopGamePoint relativeVector = new TopGamePoint();
            relativeVector.X = GetXFromLineLengthAndTopAngle(length, topAngle);
            relativeVector.Y = GetYFromLineLengthAndTopAngle(length, topAngle);

            X = startPoint.X + relativeVector.X;
            Y = startPoint.Y + relativeVector.Y;

            // Delete this! It's only here for golden master purposes. 
            tempRelativePoint.X = relativeVector.X;
            tempRelativePoint.Y = relativeVector.Y;
        }

        int GetXFromLineLengthAndTopAngle(double lineLength, double topAngle)
        {
            return -(int)Math.Round(lineLength * SafeSin(topAngle), 0, MidpointRounding.AwayFromZero);
        }

        int GetYFromLineLengthAndTopAngle(double lineLength, double topAngle)
        {
            return (int)Math.Round(lineLength * SafeCos(topAngle), 0, MidpointRounding.AwayFromZero);
        }

        public double SafeSin(double angleInDegrees)
        {
            return Math.Sin(DegreeToRadian(angleInDegrees));
        }

        public double SafeCos(double angleInDegrees)
        {
            return Math.Cos(DegreeToRadian(angleInDegrees));
        }

        public double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}