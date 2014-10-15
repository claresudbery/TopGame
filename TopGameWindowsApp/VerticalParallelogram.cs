using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TopGameWindowsApp
{
    public class VerticalParallelogram
    {
        private int iLeftTop;
        private int iLeftBottom;
        private int iRightTop;
        private int iRightBottom;
        private int iLeft;
        private int iRight;
        private VerticalDirection leftToRightDirection;

        public enum VerticalDirection
        {
            Up,
            Down,
            Neither
        }

        public enum HorizontalDirection
        {
            Left,
            Right,
            Neither
        }

        public VerticalParallelogram()
        {
            iLeftTop = 0;
            iLeftBottom = 0;
            iRightTop = 0;
            iRightBottom = 0;
            iLeft = 0;
            iRight = 0;
            leftToRightDirection = VerticalDirection.Up;
        }

        public void Initialise(int iStartX
                                , int iStartY
                                , HorizontalDirection horizontalDirection
                                , VerticalDirection verticalDirection
                                , int iWidth
                                , int iHeight)
        {
            iLeft = iStartX;
            iRight = iStartX + (iWidth - 1);
            iLeftBottom = iStartY;
            // In bitmaps, the Y is 0 at the top, so when we go up, we subtract.
            iLeftTop = iStartY - (iHeight - 1);
            if (horizontalDirection == HorizontalDirection.Neither)
            {
                // We're going straight up, which means the parallelogram itself is not slanting upwards
                verticalDirection = VerticalDirection.Neither;
            }
            leftToRightDirection = verticalDirection;
            switch (verticalDirection)
            {
                case VerticalDirection.Up:
                    {
                        // In bitmaps, the Y is 0 at the top, so when we go up, we subtract.
                        iRightBottom = iLeftBottom - (iWidth - 1);
                        iRightTop = iLeftTop - (iWidth - 1);
                    }
                    break;
                case VerticalDirection.Down:
                    {
                        // In bitmaps, the Y is 0 at the top, so when we go down, we add.
                        iRightBottom = iLeftBottom + (iWidth - 1);
                        iRightTop = iLeftTop + (iWidth - 1);
                    }
                    break;
                case VerticalDirection.Neither:
                    {
                        iRightBottom = iLeftBottom;
                        iRightTop = iLeftTop;
                    }
                    break;
            }
        }

        public void ReloadColour(ref Bitmap bmpDisplayLines, Color myColour)
        {
            Color myTempColour = myColour;
            Color blackColor = DeckDisplayLine.RGBMappings.Find(o => o.Key == DisplayLineRegion.RegionColour.white).Value;
            int iTempBottom = iLeftBottom;
            int iTempX = iLeft;
            int iTempY = iTempBottom;
            for (int iStripeCount = 1; iStripeCount <= (iRight - iLeft + 1); iStripeCount++)
            {
                iTempY = iTempBottom;
                // It's iLeftBottom - iLeftTop because of the Y coordinates running from top to bottom, rather than bottom to top.
                for (int iHeightCount = 1; iHeightCount <= ((iLeftBottom - iLeftTop) + 1); iHeightCount++)
                {
                    if (iStripeCount == 1)
                    {
                        myTempColour = blackColor;
                    }
                    else
                    {
                        if (iHeightCount == 1)
                        {
                            myTempColour = blackColor;
                        }
                        else
                        {
                            myTempColour = myColour;
                        }
                    }
                    bmpDisplayLines.SetPixel(iTempX, iTempY, myTempColour);
                    // In bitmaps, the Y is 0 at the top, so when we go up, we subtract.
                    iTempY--;
                }
                switch (leftToRightDirection)
                {
                    case VerticalDirection.Up:
                        {
                            // In bitmaps, the Y is 0 at the top, so when we go up, we subtract.
                            iTempBottom--;
                        }
                        break;
                    case VerticalDirection.Down:
                        {
                            // In bitmaps, the Y is 0 at the top, so when we go down, we add.
                            iTempBottom++;
                        }
                        break;
                    case VerticalDirection.Neither:
                        {
                            // Do nothing. Y will stay the same.
                        }
                        break;
                }
                iTempX++;
            }
        }// end function
    }// end class
}// end namespace
