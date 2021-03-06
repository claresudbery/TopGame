﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Domain.GameModels
{
    public class HandOfCards
    {
        public DeckOfCards cardDeck;
        private PileOfCardImages cardImages;
        public DeckDisplayLine transitionLine2;
        public bool PlayerIsOut { get; set; }

        public void Dispose()
        {
            cardDeck.Dispose();
        }

        public HandOfCards()
        {
            cardDeck = new DeckOfCards(DeckDisplayLine.LoadStyle.TopLoader);
            cardImages = new PileOfCardImages();
            //transitionLine = new DeckDisplayLine(DeckDisplayLine.LoadStyle.BottomLoader);
        }

        public void ClearDeck()
        {
            cardDeck.Clear();
        }

        public void AddCard(Card cardToAdd)
        {
            cardDeck.AddCard(cardToAdd);
        }

        public void InitialiseImages()
        {
            cardImages.Reset();
            for (int i = 1; i <= cardImages.Count(); i++)
            {
                cardImages.PlayCard();
            }
        }

        public void AddImage(PictureBox image)
        {
            cardImages.AddImage(image);
        }

        public string GetDeckContents()
        {
            return cardDeck.GetDeckContents();
        }

        public string GetColoursAsText()
        {
            return cardDeck.GetColoursAsText();
        }

        public int NumCards()
        {
            return cardDeck.Count();
        }

        public int NumImages()
        {
            return cardImages.Count();
        }

        public string GetTopCardImageUrl()
        {
            return cardDeck.GetTopCardImageUrl();
        }

        public string GetTopCardImageName()
        {
            return cardDeck.GetTopCardImageName();
        }

        public Card GetCard(int iCardIndex)
        {
            return cardDeck.GetCard(iCardIndex);
        }

        public Card GetTopCard()
        {
            return cardDeck.GetCard(0);
        }

        public void RemoveTopCard()
        {
            cardDeck.RemoveTopCard();
        }

        public void PlayCard()
        {
            // No need to remove the graphic segment or reload the colours: this is done as part of RemoveTopCard.
            // The loop will be redisplayed as a result of the ManyHands calls to DisplayAllDeckContents and ReloadGraphicLoops, in the calling routine.
            RemoveTopCard();
            if (cardDeck.Count() < cardImages.Count())
            {
                cardImages.HideCard();
            }
        }

        public void RedisplayImages(int iPreviousCardCount)
        {
            int iNewCardCount = cardDeck.Count();

            // If we were down to the last few cards in the hand, we need to increase the number displayed in the pile.
            if (iPreviousCardCount < cardImages.Count())
            {
                int iNumCardIncrease = iNewCardCount - iPreviousCardCount;
                int iNumCardsHidden = cardImages.Count() - iPreviousCardCount;
                int iNumCardsToShow = Math.Min(iNumCardIncrease, iNumCardsHidden);
                for (int iCardCount = 1; iCardCount <= iNumCardsToShow; iCardCount++)
                {
                    cardImages.PlayCard();
                }
            }
        }

        public void PlayerWins(ref DeckOfCards cardsWon, ref IGamePlayer mainForm)
        {
            int iPreviousCardCount = cardDeck.Count();

            for (int iCardCount = 0; iCardCount < cardsWon.Count(); iCardCount++)
            {
                cardDeck.AddCard(cardsWon.GetCard(iCardCount));
            }

            // If we were down to the last few cards in the hand, we need to increase the number displayed in the pile.
            RedisplayImages(iPreviousCardCount);
        }

        public void AfterPlayerWins(int iPreviousCardCount)
        {
            // If we were down to the last few cards in the hand, we need to increase the number displayed in the pile.
            RedisplayImages(iPreviousCardCount);
        }

        public void UndoPlay(DeckOfCards cardsInPlay)
        {
            if (cardDeck.Count() < cardImages.Count())
            {
                cardImages.PlayCard();
            }

            cardDeck.InsertCard(0, cardsInPlay.GetCard(cardsInPlay.Count() - 1));
        }

        public void CopyCards(ref DeckOfCards source)
        {
            cardDeck.CopyCards(ref source);
        }// end function
    }// end class
}// end namespace
