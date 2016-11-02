using System;
using System.Drawing;

namespace Domain.GraphicModels
{
    public static class LineCalculator
    {
        public static double SafeTan(double angleInDegrees)
        {
            return Math.Tan(DegreeToRadian(angleInDegrees));
        }

        public static double SafeCos(double angleInDegrees)
        {
            return Math.Cos(DegreeToRadian(angleInDegrees));
        }

        public static double SafeSin(double angleInDegrees)
        {
            return Math.Sin(DegreeToRadian(angleInDegrees));
        }

        public static double SafeAtan(double tanValue)
        {
            return RadianToDegree(Math.Atan(tanValue));
        }

        public static double SafeAcos(double cosValue)
        {
            return RadianToDegree(Math.Acos(cosValue));
        }

        public static double SafeAsin(double sinValue)
        {
            return RadianToDegree(Math.Asin(sinValue));
        }

        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public static Point MoveAlongLineByLength(Point startPoint, Point endPoint, double length)
        {
            Point newPoint = new Point();
            Point travelVector = new Point();
            Point lineVector = new Point();
            double angle = 0;

            if (length == 0)
            {
                newPoint = startPoint;
            }
            else
            {
                // find the vector for the line itself
                lineVector.X = endPoint.X - startPoint.X;
                lineVector.Y = endPoint.Y - startPoint.Y;

                // calculate the angle 
                angle = SafeAtan(lineVector.Y / lineVector.X);

                // create the vector that moves in the correct direction by the specified amount
                travelVector.X = (int)Math.Round(GetAdjacentSide(length, angle), 0, MidpointRounding.AwayFromZero);
                travelVector.Y = (int)Math.Round(GetOppositeSide(length, angle), 0, MidpointRounding.AwayFromZero);

                // add the vector to the start point.
                newPoint.X = startPoint.X + travelVector.X;
                newPoint.Y = startPoint.Y + travelVector.Y;
            }

            return newPoint;
        }

        public static TopGamePoint MoveAlongLineByFraction(TopGamePoint startPoint, TopGamePoint endPoint, double fraction)
        {
            TopGamePoint newPoint = new TopGamePoint();
            TopGamePoint travelVector = new TopGamePoint();

            if (fraction == 0)
            {
                fraction = 1;
            }

            // create the vector that moves in the correct direction by the specified amount
            travelVector.X = (int)Math.Round(fraction * (endPoint.X - startPoint.X), 0, MidpointRounding.AwayFromZero);
            travelVector.Y = (int)Math.Round(fraction * (endPoint.Y - startPoint.Y), 0, MidpointRounding.AwayFromZero);

            // add the vector to the start point.
            newPoint.X = startPoint.X + travelVector.X;
            newPoint.Y = startPoint.Y + travelVector.Y;

            return newPoint;
        }

        public static TopGamePoint GetEndPointOfRotatedLine(
                            double sourceLineLength,
                            TopGamePoint sourceLineStartPoint,
                            TopGamePoint sourceLineEndPoint,
                            double antiClockRotationAngle)
        {
            TopGamePoint rotatedEndPoint = new TopGamePoint();

            double relativeSourceX = sourceLineStartPoint.X - sourceLineEndPoint.X;
            double relativeSourceY = sourceLineStartPoint.Y - sourceLineEndPoint.Y;
            bool relativeXPositive = (relativeSourceX >= 0);
            bool relativeYPositive = (relativeSourceY >= 0);
            int finalXMultiplier = relativeXPositive ? -1 : 1;
            int finalYMultiplier = relativeYPositive ? -1 : 1;

            double acosTarget = Math.Abs(relativeSourceX) / sourceLineLength;
            if (acosTarget > 1)
            {
                // This can happen because X values have been rounded.
                acosTarget = 1;
            }
            double sourceVerticalAngle = SafeAcos(acosTarget);

            double newVerticalAngle = sourceVerticalAngle - antiClockRotationAngle;
            if (relativeXPositive != relativeYPositive)
            {
                // If the X and the Y don't have the same sign, the source line was in one of the opposite 2 quadrants
                // This changes the relationship between the three angles.
                newVerticalAngle = sourceVerticalAngle + antiClockRotationAngle;
            }

            rotatedEndPoint.X = (int)Math.Round(sourceLineLength * SafeCos(newVerticalAngle), 0, MidpointRounding.AwayFromZero);
            rotatedEndPoint.Y = (int)Math.Round(sourceLineLength * SafeSin(newVerticalAngle), 0, MidpointRounding.AwayFromZero);

            rotatedEndPoint.X = finalXMultiplier * rotatedEndPoint.X;
            rotatedEndPoint.Y = finalYMultiplier * rotatedEndPoint.Y;

            // What we have now is actually an abstract vector in space - not a line related to the original start point.
            // Need to adjust it relative to the original start point.
            rotatedEndPoint.X = sourceLineStartPoint.X + rotatedEndPoint.X;
            rotatedEndPoint.Y = sourceLineStartPoint.Y + rotatedEndPoint.Y;

            return rotatedEndPoint;
        }

        public static double GetAdjacentSide(double hypotenuse, double angle)
        {
            return SafeCos(angle) * hypotenuse;
        }

        public static double GetOppositeSide(double hypotenuse, double angle)
        {
            return SafeSin(angle) * hypotenuse;
        }
    }
}