using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nsCard;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Domain.Models;

namespace TopGameWindowsApp
{
    public class DeckOfCards
    {
        private List<Card> cards;

        // The display line is the blocky line implemented using a bitmap
        public DeckDisplayLine displayLine2;

        // The graphic loop is a bit neater and cleverer and supercedes the display line: It's more of a proper curve and uses GDI+ rather than a bitmap.
        public OnePlayerGraphicsLoop OnePlayerGraphicLoop;

        public DeckOfCards(DeckDisplayLine.LoadStyle loadStyle)
        {
            cards = new List<Card>();
            //displayLine = new DeckDisplayLine(loadStyle);
            OnePlayerGraphicLoop = new OnePlayerGraphicsLoop();
        }

        public void Dispose()
        {
            OnePlayerGraphicLoop.Dispose();
        }

        //
        // LoadSpuds: loads a pack with 52 non-face-cards. These are just dummies so they can all be identical.
        //
        public void LoadSpuds(int iNumCards)
        {
            Card currentCard;

            // Build up 52 cards
            for (int iCardCount = 1; iCardCount <= iNumCards; iCardCount++)
            {
                currentCard = new Card();
                currentCard.InitialiseAsSpud();
                cards.Add(currentCard);
            }
        }

        public void LoadFullPack()
        {
            Card currentCard;

            // Build up 52 cards
            for (int iValueCount = 1; iValueCount <= 13; iValueCount++)
            {
                for (int iSuitCount = 1; iSuitCount <= 4; iSuitCount++)
                {
                    currentCard = new Card();
                    currentCard.myCardValue = (Card.CardValue)iValueCount;
                    currentCard.mySuit = (Card.Suit)iSuitCount;
                    currentCard.imageUrl = currentCard.GetImageUrl();
                    currentCard.imageName = currentCard.GetImageName();
                    cards.Add(currentCard);
                }
            }

            // No jokers in this game
            /*// Add a couple of jokers
            currentCard = new Card();
            currentCard.myCardValue = CardValue.Joker;
            currentCard.mySuit = Suit.None;
            currentCard.imageUrl = currentCard.GetImageUrl(CardColour.Red);
            currentCard.imageName = currentCard.GetImageName(CardColour.Red);
            cards.Add(currentCard);

            currentCard = new Card();
            currentCard.myCardValue = CardValue.Joker;
            currentCard.mySuit = Suit.None;
            currentCard.imageUrl = currentCard.GetImageUrl(CardColour.Black);
            currentCard.imageName = currentCard.GetImageName(CardColour.Black);
            cards.Add(currentCard);*/
        }

        public int Count()
        {
            return cards.Count();
        }

        public void Clear()
        {
            cards.Clear();
            OnePlayerGraphicLoop.Clear();
        }

        public void AddCard(Card cardToAdd, bool bReloadGraphics = true)
        {
            cards.Add(cardToAdd);
            if (bReloadGraphics)
            {
                OnePlayerGraphicLoop.AddSegment();
                ReloadGraphicColours();
            }
        }

        public void InsertCard(int iCardIndex, Card cardToAdd)
        {
            cards.Insert(iCardIndex, cardToAdd);
            OnePlayerGraphicLoop.AddSegment();
            ReloadGraphicColours();
        }

        public void ReplaceCard(int iCardIndex, Card newCard, bool bReloadGraphics)
        {
            cards.ElementAt(iCardIndex).CopyCard(newCard);
            if (bReloadGraphics)
            {
                OnePlayerGraphicLoop.AddSegment();
                ReloadGraphicColours();
            }
        }

        public void ReloadGraphicColours()
        {
            Debug.Assert(OnePlayerGraphicLoop.GetNumTotalSegments() == cards.Count(), "Wrong number of cards passed to ReloadColours");

            for (int iCount = 0; iCount < cards.Count(); iCount++)
            {
                DisplayLineRegion.RegionColour colour = DeckDisplayLine.ColourMappings.Find(o => o.Key == cards.ElementAt(iCount).myCardValue).Value;
                OnePlayerGraphicLoop.ReloadColour(iCount, DeckDisplayLine.RGBMappings.Find(o => o.Key == colour).Value);
            }
        }

        public Card GetCard(int iCardIndex)
        {
            return cards.ElementAt(iCardIndex);
        }

        public void RemoveTopCard()
        {
            cards.Remove(cards.ElementAt(0));
            OnePlayerGraphicLoop.RemoveTopSegment();
            ReloadGraphicColours();
        }

        public void RemoveBottomCard()
        {
            cards.Remove(cards.ElementAt(cards.Count() - 1));
            OnePlayerGraphicLoop.RemoveBottomSegment();
            ReloadGraphicColours();
        }

        public string GetDeckContents()
        {
            String deckContents = "";

            for (int iCardCount = 0; iCardCount < cards.Count(); iCardCount++)
            {
                deckContents = String.Concat(deckContents, Card.CardValueChars[(int)(cards.ElementAt(iCardCount).myCardValue)]);
            }

            return deckContents;
        }

        public string GetColoursAsText()
        {
            return "Not currently implemented.";
            //return displayLine.GetColoursAsText();
        }

        public string GetTopCardImageUrl()
        {
            return cards.ElementAt(0).imageUrl;
        }

        public string GetTopCardImageName()
        {
            return cards.ElementAt(0).imageName;
        }

        public string GetCardImageName(int iCardIndex)
        {
            return cards.ElementAt(iCardIndex).imageName;
        }

        public void Shuffle()
        {
            List<Card> newDeck = new List<Card>();

            int randomIndex = 0;

            for (int iCount = cards.Count(); iCount >= 1; iCount--)
            {
                Random randomiser = new Random();
                randomIndex = randomiser.Next(iCount);
                newDeck.Add(cards.ElementAt(randomIndex));
                cards.RemoveAt(randomIndex);
            }

            for (int iCount = 0; iCount < newDeck.Count(); iCount++)
            {
                cards.Add(newDeck.ElementAt(iCount));
            }
        }

        public Card FindCard(string strImageName)
        {
            Card foundCard = cards.Find(o => o.imageName == strImageName);
            return foundCard;
        }

        public void CopyCards(ref DeckOfCards source)
        {
            Clear();
            for (int iCount = 0; iCount < source.Count(); iCount++)
            {
                Card newCard = new Card();
                newCard = source.GetCard(iCount);
                AddCard(newCard);
            }
        }// end function
    }// end class
}// end namespace
