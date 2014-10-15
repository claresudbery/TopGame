﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Resources;

namespace TopGameWindowsApp
{
    public class InterlockingCardImages
    {
        public PileOfCardImages leftPile;
        public PileOfCardImages rightPile;
        private Side lastSidePlayed;

        public enum Side
        {
            Left
            , Right
            , Neither
        }

        public InterlockingCardImages()
        {
            leftPile = new PileOfCardImages();
            rightPile = new PileOfCardImages();
            lastSidePlayed = Side.Neither;
        }

        public void Reset()
        {
            leftPile.Reset();
            rightPile.Reset();
        }

        public void PlayCard(string newImage, Side sidePlayed)
        {
            PileOfCardImages imagesToPlay = leftPile;
            bool bReset = false;
            if (lastSidePlayed != sidePlayed)
            {
                lastSidePlayed = sidePlayed;
                bReset = true;
            }
            if (sidePlayed == Side.Right)
            {
                imagesToPlay = rightPile;
            }
            imagesToPlay.PlayCard(newImage, bReset);
        }

        public void UnPlayCard(Side sidePlayed)
        {
            PileOfCardImages imagesToPlay = leftPile;
            bool bReset = false;
            if (sidePlayed == Side.Right)
            {
                imagesToPlay = rightPile;
            }
            imagesToPlay.UnPlayCard();
        }
    }

    public class PileOfCardImages
    {
        private List<PictureBox> cardImages;

        private int iLastCardShown;

        public int Count()
        {
            return cardImages.Count();
        }

        public PileOfCardImages()
        {
            iLastCardShown = -1;
            cardImages = new List<PictureBox>();
        }

        public void AddImage(PictureBox newImage)
        {
            cardImages.Add(newImage);
        }

        public void Reset()
        {
            iLastCardShown = -1;
            for (int iCount = 0; iCount < cardImages.Count(); iCount++)
            {
                cardImages.ElementAt(iCount).Hide();
            }
        }

        public void HideCard()
        {
            if (iLastCardShown >= 0)
            {
                cardImages.ElementAt(iLastCardShown).Hide();
                iLastCardShown--;
            }
        }

        public void PlayCard(string strNewImage = "", bool bReset = false)
        {
            iLastCardShown++;
            if ((iLastCardShown == cardImages.Count()) || bReset)
            {
                iLastCardShown = 0;
            }
            if (strNewImage != "")
            {
                //cardImages.ElementAt(iLastCardShown).ImageLocation = strNewImage; 
                
                ResourceManager resourceManager = Resource1.ResourceManager;
                cardImages.ElementAt(iLastCardShown).Image = (Bitmap)resourceManager.GetObject(strNewImage);
                cardImages.ElementAt(iLastCardShown).Image.Tag = strNewImage;
            }
            cardImages.ElementAt(iLastCardShown).Show();
            cardImages.ElementAt(iLastCardShown).BringToFront();
        }

        public void UnPlayCard()
        {
            cardImages.ElementAt(iLastCardShown).SendToBack();
            cardImages.ElementAt(iLastCardShown).Hide();
            iLastCardShown--;
        }// end function
    }// end class
}// end namespace
