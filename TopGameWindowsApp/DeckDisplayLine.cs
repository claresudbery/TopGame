using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using nsCard;
using System.Windows.Forms;

namespace TopGameWindowsApp
{
    public class DeckDisplayLine
    {
        private List<DisplayLineRegion> regions;
        private int iCurrentNumCards;
        private int iNumAddedCards;
        private int iTopIndex;
        private int iBottomIndex;
        private LoadStyle loadStyle;

        public static readonly KeyValuePair<DisplayLineRegion.RegionColour, Color>[] RGBMappingArray = 
            { new KeyValuePair<DisplayLineRegion.RegionColour, Color>(DisplayLineRegion.RegionColour.dummy, Color.FromArgb(10, 10, 10))
            , new KeyValuePair<DisplayLineRegion.RegionColour, Color>(DisplayLineRegion.RegionColour.white, System.Drawing.Color.Bisque) // background
            , new KeyValuePair<DisplayLineRegion.RegionColour, Color>(DisplayLineRegion.RegionColour.lightBlue, System.Drawing.Color.LightCyan) // spuds
            , new KeyValuePair<DisplayLineRegion.RegionColour, Color>(DisplayLineRegion.RegionColour.Red, System.Drawing.Color.Cyan) // Aces
            , new KeyValuePair<DisplayLineRegion.RegionColour, Color>(DisplayLineRegion.RegionColour.Yellow, System.Drawing.Color.Teal) // Kings
            , new KeyValuePair<DisplayLineRegion.RegionColour, Color>(DisplayLineRegion.RegionColour.DarkBlue, System.Drawing.Color.Navy) // Queens
            , new KeyValuePair<DisplayLineRegion.RegionColour, Color>(DisplayLineRegion.RegionColour.Green, System.Drawing.Color.Fuchsia)}; // Jacks
        // a constant lookup list of key-value pairs, initialised with the constant array created above
        public static readonly List<KeyValuePair<DisplayLineRegion.RegionColour, Color>> RGBMappings = new List<KeyValuePair<DisplayLineRegion.RegionColour, Color>>(RGBMappingArray);

        // a constant array of key-value pairs, only created as an intermediate object to initialise the constant list below
        public static readonly KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>[] colourMappingArray = 
            { new KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>(Card.CardValue.Joker, DisplayLineRegion.RegionColour.white    )
            , new KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>(Card.CardValue.Ace,   DisplayLineRegion.RegionColour.Red        )
            , new KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>(Card.CardValue.Two,   DisplayLineRegion.RegionColour.lightBlue  )
            , new KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>(Card.CardValue.Three, DisplayLineRegion.RegionColour.lightBlue)
            , new KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>(Card.CardValue.Four,  DisplayLineRegion.RegionColour.lightBlue )
            , new KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>(Card.CardValue.Five,  DisplayLineRegion.RegionColour.lightBlue )
            , new KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>(Card.CardValue.Six,   DisplayLineRegion.RegionColour.lightBlue  )
            , new KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>(Card.CardValue.Seven, DisplayLineRegion.RegionColour.lightBlue)
            , new KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>(Card.CardValue.Eight, DisplayLineRegion.RegionColour.lightBlue)
            , new KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>(Card.CardValue.Nine,  DisplayLineRegion.RegionColour.lightBlue )
            , new KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>(Card.CardValue.Ten,   DisplayLineRegion.RegionColour.lightBlue  )
            , new KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>(Card.CardValue.Jack,  DisplayLineRegion.RegionColour.Green     )
            , new KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>(Card.CardValue.Queen, DisplayLineRegion.RegionColour.DarkBlue )
            , new KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>(Card.CardValue.King,  DisplayLineRegion.RegionColour.Yellow    )};
        // a constant lookup list of key-value pairs, initialised with the constant array created above
        public static readonly List<KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>> ColourMappings = new List<KeyValuePair<Card.CardValue, DisplayLineRegion.RegionColour>>(colourMappingArray);

        public struct ShuntInfo
        {
            public bool bShuntFinished;
            public bool bCardsNowLeaving;
            public ShuntInfo(bool shuntFinished, bool cardsNowLeaving)
            {
                bShuntFinished = shuntFinished;
                bCardsNowLeaving = cardsNowLeaving;
            }
        }
        
        public enum LoadStyle
        {
            TopLoader,
            BottomLoader
        }

        public DeckDisplayLine(LoadStyle style)
        {
            loadStyle = style;
            regions = new List<DisplayLineRegion>();
            InitialiseRegions(52);
            iNumAddedCards = 0;
            iCurrentNumCards = 0;
        }

        private void InitialiseRegions(int numRegions)
        {
            for (int iCount = 0; iCount < numRegions; iCount++)
            {
                regions.Add(new DisplayLineRegion());
            }
        }

        //
        // ReloadColours: Called when the cards have changed.
        // Changes the colour of each region, and redisplays accordingly.
        // NB This will also have the effect of changing which regions represent cards at all: some regions will be reset to "white space."
        //
        public void ReloadColours(ref Bitmap bmpDisplayLines, ref DeckOfCards newCards)
        {
            // iIndexDifference is an offset to say whether we start at the beginning or shunt forward a bit
            int iIndexDifference = 0;
            if (loadStyle == LoadStyle.BottomLoader)
            {
                iIndexDifference = regions.Count() - newCards.Count();
            }

            // Tell each region what its new colour is. ChangeColour will call ReloadColour, so will also have the effect of redisplaying the region.
            for (int iCount = 0; iCount < newCards.Count(); iCount++)
            {
                regions.ElementAt(iCount + iIndexDifference).ChangeColour(ref bmpDisplayLines, newCards.GetCard(iCount));
            }

            // fill in with white spaces
            if (iCurrentNumCards > newCards.Count())
            {
                int iStartIndex = newCards.Count();
                int iEndIndex = regions.Count() - 1;
                if (loadStyle == LoadStyle.BottomLoader)
                {
                    iStartIndex = 0;
                    iEndIndex = (regions.Count() - newCards.Count()) - 1;
                }
                for (int iCount = iStartIndex; iCount <= iEndIndex; iCount++)
                {
                    regions.ElementAt(iCount).ChangeColour(ref bmpDisplayLines, DisplayLineRegion.RegionColour.white);
                }
            }
            iCurrentNumCards = newCards.Count();
        }

        public ShuntInfo Shunt(ref Bitmap bmpDisplayLines, DeckOfCards cardsToShunt)
        {
            ShuntInfo shuntInfo = new ShuntInfo(true, true);
            int iIndexDifference = 0;
            int iInitialCount = 0;

            if (iBottomIndex > 0)
            {
                shuntInfo.bShuntFinished = false;
                if (iTopIndex > 0)
                {
                    iTopIndex--;
                    iCurrentNumCards++;
                    shuntInfo.bCardsNowLeaving = false;
                }
                if (iTopIndex <= (regions.Count() - cardsToShunt.Count()) - 1)
                {
                    // The last card in the cardsToShunt deck has entered the line, so we can start incrementing the bottom index now.
                    if (iBottomIndex < regions.Count())
                    {
                        regions.ElementAt(iBottomIndex).ChangeColour(ref bmpDisplayLines, DisplayLineRegion.RegionColour.white);
                    }
                    iBottomIndex--;
                    iCurrentNumCards--;
                }

                iIndexDifference = 0;
                if (iTopIndex > 0)
                {
                    iIndexDifference = iTopIndex;
                }
                else if (iBottomIndex < (regions.Count() - 1))
                {
                    // Add 2, because bottom index will already have been incrememented, and we want the previous value
                    iIndexDifference = (iBottomIndex + 1) - cardsToShunt.Count();
                }

                iInitialCount = cardsToShunt.Count() - 1;
                if ((iInitialCount + iIndexDifference) > (regions.Count() - 1))
                {
                    iInitialCount = (regions.Count() - 1) - iIndexDifference;
                }
                for (int iCount = iInitialCount; (iCount >= 0) && ((iCount + iIndexDifference) >= 0); iCount--)
                {
                    regions.ElementAt(iCount + iIndexDifference).ChangeColour(ref bmpDisplayLines, cardsToShunt.GetCard(iCount));
                }
            }
            else if (iBottomIndex == 0)
            {
                regions.ElementAt(iBottomIndex).ChangeColour(ref bmpDisplayLines, DisplayLineRegion.RegionColour.white);
            }

            return shuntInfo;
        }

        public bool ShuntIntoExisting(ref Bitmap bmpDisplayLines, DeckOfCards cardsToShunt)
        {
            bool bShuntCompleted = true;
            int iIndexDifference = 0;
            int iInitialCount = 0;

            if (iTopIndex > iCurrentNumCards)
            {
                bShuntCompleted = false;
                iTopIndex--;
                iNumAddedCards++;

                if (iTopIndex <= (regions.Count() - cardsToShunt.Count()) - 1)
                {
                    // The last card in the cardsToShunt deck has entered the line, so we can start incrementing the bottom index now.
                    if (iBottomIndex < regions.Count())
                    {
                        regions.ElementAt(iBottomIndex).ChangeColour(ref bmpDisplayLines, DisplayLineRegion.RegionColour.white);
                    }
                    iBottomIndex--;
                    iNumAddedCards--;
                }

                iIndexDifference = iTopIndex;

                iInitialCount = cardsToShunt.Count() - 1;
                if ((iInitialCount + iIndexDifference) > (regions.Count() - 1))
                {
                    iInitialCount = (regions.Count() - 1) - iIndexDifference;
                }
                for (int iCount = iInitialCount; (iCount >= 0) && ((iCount + iIndexDifference) >= 0); iCount--)
                {
                    regions.ElementAt(iCount + iIndexDifference).ChangeColour(ref bmpDisplayLines, cardsToShunt.GetCard(iCount));
                }
            }
            else if (iTopIndex == iCurrentNumCards)
            {
                iCurrentNumCards += iNumAddedCards;
            }

            return bShuntCompleted;
        }

        public string GetColoursAsText()
        {
            String deckColours = "";

            for (int iCardCount = 0; iCardCount < regions.Count(); iCardCount++)
            {
                deckColours = String.Concat(deckColours, regions.ElementAt(iCardCount).Colour.ToString()[0]);
            }

            return deckColours;
        }

        public void PrepareForShunt()
        {
            if (loadStyle == LoadStyle.BottomLoader)
            {
                if (iCurrentNumCards > 0)
                {
                    iTopIndex = regions.Count() - iCurrentNumCards;
                    iBottomIndex = regions.Count() - 1;
                }
                else
                {
                    PrepareForShuntNewCardsEntering();
                }
            }
        }

        public void PrepareForShuntIntoExisting()
        {
            PrepareForShuntNewCardsEntering();
        }

        public void PrepareForShuntNewCardsEntering()
        {
            // The top index gets decremented as soon as we start shunting, so it starts out too high.
            iTopIndex = regions.Count();
            // The bottom index doesn't get decremented until all cards have entered the line, so it
            // is initialised to the last position in the line.
            iBottomIndex = regions.Count() - 1;
        }

        public void InitialiseParallelograms(int iStartIndex
            , int iEndIndex
            , VerticalParallelogram.HorizontalDirection horizontalDirection
            , VerticalParallelogram.VerticalDirection verticalDirection
            , int iWidth
            , int iHeight
            , int iStartX
            , int iStartY)
        {
            //int iTempStartX = iStartX;
            //int iTempStartY = iStartY;

            int iTempWidth = iWidth * 2;
            int iTempHeight = iHeight * 2;
            int iTempStartX = iStartX * 2;
            int iTempStartY = iStartY * 2;

            int iIncrement = (iStartIndex < iEndIndex) ? 1 : -1;

            for (int iCount = iStartIndex; iCount != (iEndIndex + iIncrement); iCount = iCount + iIncrement)
            {
                regions.ElementAt(iCount).parallelogram.Initialise(iTempStartX
                    , iTempStartY
                    , horizontalDirection
                    , verticalDirection
                    , iTempWidth
                    , iTempHeight);

                switch (horizontalDirection)
                {
                    case VerticalParallelogram.HorizontalDirection.Left:
                        {
                            // This probably won't happen if I stick with always moving from left to right.
                            iTempStartX = iTempStartX - iTempWidth;
                        }
                        break;
                    case VerticalParallelogram.HorizontalDirection.Right:
                        {
                            iTempStartX = iTempStartX + iTempWidth;
                        }
                        break;
                    case VerticalParallelogram.HorizontalDirection.Neither:
                        {
                            // no action - X just stays the same.
                        }
                        break;
                }

                // It's a 45-degree angle so we add width to both X and Y (or subtract, if we're moving downwards)
                switch (verticalDirection)
                {
                    case VerticalParallelogram.VerticalDirection.Up:
                        {
                            // In bitmaps, the Y is 0 at the top, so when we go up, we subtract.
                            iTempStartY = iTempStartY - iTempWidth;
                        }
                        break;
                    case VerticalParallelogram.VerticalDirection.Down:
                        {
                            // In bitmaps, the Y is 0 at the top, so when we go down, we add.
                            iTempStartY = iTempStartY + iTempWidth;
                        }
                        break;
                    case VerticalParallelogram.VerticalDirection.Neither:
                        {
                            // no action - Y just stays the same.
                        }
                        break;
                }
            }
        }

        public void LoadManyColours(ref Bitmap bmpDisplayLines, int iStartIndex, int iEndIndex, DisplayLineRegion.RegionColour colour)
        {
            int iIncrement = (iStartIndex < iEndIndex) ? 1 : -1;
            for (int iCount = iStartIndex; iCount != (iEndIndex + iIncrement); iCount = iCount + iIncrement)
            {
                regions.ElementAt(iCount).ChangeColour(ref bmpDisplayLines, colour);
            }
        }// end function
    }// end class
}// end namespace
