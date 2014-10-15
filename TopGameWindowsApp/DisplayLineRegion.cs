using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using nsCard;

namespace TopGameWindowsApp
{
    public class DisplayLineRegion
    {
        public VerticalParallelogram parallelogram;
        public RegionColour Colour {get; private set;}

        public enum RegionColour
        {
            dummy,
            white,
            lightBlue,
            Red,
            Yellow,
            DarkBlue,
            Green
        }

        public DisplayLineRegion()
        {
            Colour = RegionColour.white;
            parallelogram = new VerticalParallelogram();
        }

        public void ChangeColour(ref Bitmap bmpDisplayLines, Card newCard)
        {
            if (Colour != DeckDisplayLine.ColourMappings.Find(o => o.Key == newCard.myCardValue).Value)
            {
                Colour = DeckDisplayLine.ColourMappings.Find(o => o.Key == newCard.myCardValue).Value;
                ReloadColour(ref bmpDisplayLines);
            }
        }

        public void ChangeColour(ref Bitmap bmpDisplayLines, RegionColour newColour)
        {
            if (Colour != newColour)
            {
                Colour = newColour;
                ReloadColour(ref bmpDisplayLines);
            }
        }

        private void ReloadColour(ref Bitmap bmpDisplayLines)
        {
            Color myColour = DeckDisplayLine.RGBMappings.Find(o => o.Key == Colour).Value;
            parallelogram.ReloadColour(ref bmpDisplayLines, myColour);
        }// end function
    }// end class
}// end namespace
