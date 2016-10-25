using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Domain.GameModels.GoldenMaster;
using Domain.GraphicModels;
using TopGameWindowsApp;

namespace Domain.GameModels
{
    public class ManyHands
    {
        private List<HandOfCards> theHands;
        private DeckOfCards cardsInPlay;
        private InterlockingCardImages playedImages;
        private DeckOfCards mainDeck;
        private IGamePlayer mainForm;
        private Bitmap bmpDisplayLines;
        private int iNumCardsToPay = 0;
        private int iNumCardsPlayedSinceFaceCard = 0;
        private bool bFaceCardPlayed = false;
        private int iCurrentAutoPlayer = 0;
        private RelevancyCriteria currentRelevancyCriteria;
        private int iAngleCalculationBounceCount;
        private int iRecursionCount;
        private List<OnePlayerGraphicsLoop> allGraphicLoops;
        private bool _recordingGoldenMaster = false;
        private GoldenMasterGameData _goldenMasterData = new GoldenMasterGameData();

        public void Dispose()
        {
            cardsInPlay.Dispose();
            mainDeck.Dispose();
            for (int iCount = theHands.Count() - 1; iCount >= 0; iCount--)
            {
                theHands.ElementAt(iCount).Dispose();
            }
        }

        public ManyHands(int numHands, IGamePlayer form, ref Bitmap bmpDisplay)
        {
            int maxNumPlayers = 360 / TopGameConstants.MIN_CENTRAL_ANGLE;
            if (numHands > maxNumPlayers)
            {
                MessageBox.Show(string.Format("Too many players - maximum number of players is {0}", maxNumPlayers));
            }
            iAngleCalculationBounceCount = 0;
            iRecursionCount = 0;
            currentRelevancyCriteria = RelevancyCriteria.LookingForNeither;
            allGraphicLoops = new List<OnePlayerGraphicsLoop>();

            bmpDisplayLines = bmpDisplay;

            mainDeck = new DeckOfCards(DeckDisplayLine.LoadStyle.TopLoader);
            mainDeck.LoadFullPack();

            mainForm = form;

            theHands = new List<HandOfCards>();
            for (int iCount = 0; iCount < numHands; iCount++)
            {
                theHands.Add(new HandOfCards());
            }
            cardsInPlay = new DeckOfCards(DeckDisplayLine.LoadStyle.BottomLoader);
            playedImages = new InterlockingCardImages();

            // Set up the parallelograms on all the various display lines
            InitialiseParallelograms();

            InitialiseFaceCardTrackers();

            // load all the graphic loops
            for (int iCount = 0; iCount < theHands.Count(); iCount++)
            {
                allGraphicLoops.Add(theHands.ElementAt(iCount).cardDeck.OnePlayerGraphicLoop);
            }
            allGraphicLoops.Add(cardsInPlay.OnePlayerGraphicLoop);
        }

        public void InitialiseParallelograms()
        {
            // NB: In bitmaps, the Y is 0 at the top, so when we go up, we subtract - and when we go down, we add.

            // The left hand deck line 
            // straight up for 26 cards
            /*theHands.ElementAt(0).cardDeck.displayLine.InitialiseParallelograms(51
                            , 26
                            , VerticalParallelogram.HorizontalDirection.Neither
                            , VerticalParallelogram.VerticalDirection.Up
                            , 5
                            , 5
                            , 5
                            , 175);
            // slant up to the right for 9 cards
            theHands.ElementAt(0).cardDeck.displayLine.InitialiseParallelograms(25
                            , 17
                            , VerticalParallelogram.HorizontalDirection.Right
                            , VerticalParallelogram.VerticalDirection.Up
                            , 5
                            , 5
                            , 10
                            , 50);
            // across to the right for 8 cards
            theHands.ElementAt(0).cardDeck.displayLine.InitialiseParallelograms(16
                            , 9
                            , VerticalParallelogram.HorizontalDirection.Right
                            , VerticalParallelogram.VerticalDirection.Neither
                            , 5
                            , 5
                            , 55
                            , 5);
            // slant down to the right for 9 cards
            theHands.ElementAt(0).cardDeck.displayLine.InitialiseParallelograms(8
                            , 0
                            , VerticalParallelogram.HorizontalDirection.Right
                            , VerticalParallelogram.VerticalDirection.Down
                            , 5
                            , 5
                            , 95
                            , 5);

            // The left hand transition line 
            // straight down for 26 cards
            theHands.ElementAt(0).transitionLine.InitialiseParallelograms(0
                            , 25
                            , VerticalParallelogram.HorizontalDirection.Neither
                            , VerticalParallelogram.VerticalDirection.Down
                            , 5
                            , 5
                            , 5
                            , 180);
            // slant down to the right for 9 cards
            theHands.ElementAt(0).transitionLine.InitialiseParallelograms(26
                            , 34
                            , VerticalParallelogram.HorizontalDirection.Right
                            , VerticalParallelogram.VerticalDirection.Down
                            , 5
                            , 5
                            , 10
                            , 305);
            // straight across to the right for 8 cards
            theHands.ElementAt(0).transitionLine.InitialiseParallelograms(35
                            , 42
                            , VerticalParallelogram.HorizontalDirection.Right
                            , VerticalParallelogram.VerticalDirection.Neither
                            , 5
                            , 5
                            , 55
                            , 350);
            // slant up to the right for 9 cards
            theHands.ElementAt(0).transitionLine.InitialiseParallelograms(43
                            , 51
                            , VerticalParallelogram.HorizontalDirection.Right
                            , VerticalParallelogram.VerticalDirection.Up
                            , 5
                            , 5
                            , 95
                            , 350);

            // The right hand deck line 
            // slant up to the right for 9 cards
            theHands.ElementAt(1).cardDeck.displayLine.InitialiseParallelograms(0
                            , 8
                            , VerticalParallelogram.HorizontalDirection.Right
                            , VerticalParallelogram.VerticalDirection.Up
                            , 5
                            , 5
                            , 145
                            , 50);
            // straight across for 8 cards
            theHands.ElementAt(1).cardDeck.displayLine.InitialiseParallelograms(9
                            , 16
                            , VerticalParallelogram.HorizontalDirection.Right
                            , VerticalParallelogram.VerticalDirection.Neither
                            , 5
                            , 5
                            , 190
                            , 5);
            // slant down to the right for 9 cards
            theHands.ElementAt(1).cardDeck.displayLine.InitialiseParallelograms(17
                            , 25
                            , VerticalParallelogram.HorizontalDirection.Right
                            , VerticalParallelogram.VerticalDirection.Down
                            , 5
                            , 5
                            , 230
                            , 5);
            // straight down for 26 cards
            theHands.ElementAt(1).cardDeck.displayLine.InitialiseParallelograms(26
                            , 51
                            , VerticalParallelogram.HorizontalDirection.Neither
                            , VerticalParallelogram.VerticalDirection.Down
                            , 5
                            , 5
                            , 275
                            , 50);

            // The right hand transition line 
            // slant down to the right for 9 cards
            theHands.ElementAt(1).transitionLine.InitialiseParallelograms(51
                            , 43
                            , VerticalParallelogram.HorizontalDirection.Right
                            , VerticalParallelogram.VerticalDirection.Down
                            , 5
                            , 5
                            , 145
                            , 305);
            // straight across for 8 cards
            theHands.ElementAt(1).transitionLine.InitialiseParallelograms(42
                            , 35
                            , VerticalParallelogram.HorizontalDirection.Right
                            , VerticalParallelogram.VerticalDirection.Neither
                            , 5
                            , 5
                            , 190
                            , 350);
            // slant up to the right for 9 cards
            theHands.ElementAt(1).transitionLine.InitialiseParallelograms(34
                            , 26
                            , VerticalParallelogram.HorizontalDirection.Right
                            , VerticalParallelogram.VerticalDirection.Up
                            , 5
                            , 5
                            , 230
                            , 350);
            // straight up for 26 cards
            theHands.ElementAt(1).transitionLine.InitialiseParallelograms(25
                            , 0
                            , VerticalParallelogram.HorizontalDirection.Neither
                            , VerticalParallelogram.VerticalDirection.Up
                            , 5
                            , 5
                            , 275
                            , 305);

            // the cards in play
            // straight up for 52 cards
            cardsInPlay.displayLine.InitialiseParallelograms(0
                            , 51
                            , VerticalParallelogram.HorizontalDirection.Neither
                            , VerticalParallelogram.VerticalDirection.Up
                            , 5
                            , 5
                            , 140
                            , 305);*/
        }

        public void DealCards()
        {
            // NB Don't need to worry about displaying graphic loops etc - that's done by calling routine.
            //**To Do - check numPlayers is <= num of cards in pack
            bool bCardsLeft = true;
            int iCardCount = 0;

            for (int iPlayerCount = 0; iPlayerCount < theHands.Count(); iPlayerCount++)
            {
                theHands.ElementAt(iPlayerCount).ClearDeck();
            }

            while (bCardsLeft)
            {
                for (int iPlayerCount = 0; (iPlayerCount < theHands.Count()) && bCardsLeft; iPlayerCount++)
                {
                    theHands.ElementAt(iPlayerCount).AddCard(mainDeck.GetCard(iCardCount));
                    iCardCount++;
                    bCardsLeft = (iCardCount < mainDeck.Count());
                }
            }

            for (int iPlayerCount = 0; iPlayerCount < theHands.Count(); iPlayerCount++)
            {
                theHands.ElementAt(iPlayerCount).LoadDisplayLine(ref bmpDisplayLines);
            }
        }

        public void InitialiseAllImages()
        {
            for (int iCount = 0; iCount < theHands.Count(); iCount++)
            {
                theHands.ElementAt(iCount).InitialiseImages();
            }
        }

        public void AddImages(int iHandIndex, List<PictureBox> images)
        {
            for (int iCount = 0; iCount < images.Count(); iCount++)
            {
                theHands.ElementAt(iHandIndex).AddImage(images.ElementAt(iCount));
            }
        }

        public string GetDeckContents(int iHandIndex)
        {
            string deckContents = "";

            if (iHandIndex < theHands.Count())
            {
                deckContents = theHands.ElementAt(iHandIndex).GetDeckContents();
            }
            else
            {
                deckContents = cardsInPlay.GetDeckContents();
            }

            return deckContents;
        }

        public string GetColoursAsText(int iHandIndex)
        {
            string deckColours = "";

            if (iHandIndex < theHands.Count())
            {
                deckColours = theHands.ElementAt(iHandIndex).GetColoursAsText();
            }
            else
            {
                deckColours = cardsInPlay.GetColoursAsText();
            }

            return deckColours;
        }
        
        public void AddPlayedImages(InterlockingCardImages.Side side, List<PictureBox> images)
        {
            switch(side)
            {
                case InterlockingCardImages.Side.Left:
                    {
                        for (int iCount = 0; iCount < images.Count(); iCount++)
                        {
                            playedImages.leftPile.AddImage(images.ElementAt(iCount));
                        }
                        playedImages.leftPile.Reset();
                    }
                    break;

                case InterlockingCardImages.Side.Right:
                    {
                        for (int iCount = 0; iCount < images.Count(); iCount++)
                        {
                            playedImages.rightPile.AddImage(images.ElementAt(iCount));
                        }
                        playedImages.rightPile.Reset();
                    }
                    break;
            }
        }

        public Card PlayCard(int iHandIndex)
        {
            Card cardPlayed = new Card();

            if (theHands.ElementAt(iHandIndex).NumCards() > 0)
            {
                InterlockingCardImages.Side correctSide = (iHandIndex == 0) ? InterlockingCardImages.Side.Left : InterlockingCardImages.Side.Right;
                playedImages.PlayCard(theHands.ElementAt(iHandIndex).GetTopCardImageName(), correctSide);
                cardPlayed = theHands.ElementAt(iHandIndex).GetTopCard();

                // AddCard will add a new region to represent the new card, and change the colour of all the regions to match their corresponding cards.
                cardsInPlay.AddCard(cardPlayed);

                // displayLine.ReloadColours changes the colour of each region, and redisplays accordingly. It has the side-effect of adding / removing cards, by creating / replacing empty regions.
                //cardsInPlay.displayLine.ReloadColours(ref bmpDisplayLines, cardsInPlay);

                // PlayCard will take charge of changing the region colours for the hand
                theHands.ElementAt(iHandIndex).PlayCard(ref bmpDisplayLines);

                NoteGoldenMasterTurnInfo(iHandIndex);

                // ReloadGraphicLoops calculates all the new data for the graphic loops.
                ReloadGraphicLoops();

                // DisplayAllDeckContents refreshes the display, which will redisplay the bitmap AND the graphic loops.
                mainForm.DisplayAllDeckContents();
            }

            return cardPlayed;
        }

        public GoldenMasterGameData GenerateGoldenMasterGameData()
        {
            _recordingGoldenMaster = true;

            _goldenMasterData.Clear();

            mainDeck.Clear();
            mainDeck.LoadFullPack();
            _goldenMasterData.StartDeck = mainDeck.GetDeckContents();

            DealCards();
            cardsInPlay.Clear();
            AutoPlay();

            _recordingGoldenMaster = false;

            return _goldenMasterData;
        }

        private void NoteGoldenMasterTurnInfo(int iPlayerIndex)
        {
            if (_recordingGoldenMaster)
            {
                var turnInfo = new GoldenMasterTurnInfo();

                turnInfo.CardsInPlay = cardsInPlay.GetDeckContents();
                turnInfo.NewPlayerHand = theHands[iPlayerIndex].cardDeck.GetDeckContents();
                turnInfo.PlayerIndex = iPlayerIndex;

                _goldenMasterData.Turns.Add(turnInfo);
            }
        }

        private bool IsTotalNumSegmentsCorrect(int correctNumSegments)
        {
            bool bCorrectNumSegments = true;

            int iTotalNumSegments = 0;
            for (int iCount = 0; iCount < allGraphicLoops.Count(); iCount++)
            {
                iTotalNumSegments += allGraphicLoops.ElementAt(iCount).GetNumTotalSegments();
            }

            if (iTotalNumSegments != correctNumSegments)
            {
                MessageBox.Show("Doesn't add up to correct number of cards.");
                bCorrectNumSegments = false;
            }

            return bCorrectNumSegments;
        }

        public void ReloadGraphicLoops()
        {
            ReloadGraphicLoopsWithFixedShareSizes();
        }

        public void ReloadGraphicLoopsWhenAnglesAreShared(ref List<OnePlayerGraphicsLoop> theLoops)
        {
            if (IsTotalNumSegmentsCorrect(52))
            {
                // Temporarily get rid of any empty hands (not permanently, because any temporarily cardless player may be about to win the hand)
                List<OnePlayerGraphicsLoop> filteredGraphicList = new List<OnePlayerGraphicsLoop>();
                for (int iCount = 0; iCount < theLoops.Count(); iCount++)
                {
                    if (theLoops.ElementAt(iCount).GetNumTotalSegments() > 0)
                    {
                        filteredGraphicList.Add(theLoops.ElementAt(iCount));
                    }
                    else
                    {
                        theLoops.ElementAt(iCount).CalculateCentralAngle(360, 52, false);
                    }
                }

                iRecursionCount = 0;
                currentRelevancyCriteria = RelevancyCriteria.LookingForNeither;
                iAngleCalculationBounceCount = 0;
                CalculateAnglesWhenEveryoneShares(360, 52, ref filteredGraphicList, ref filteredGraphicList, false);

                double nextRotationAngle = 0;
                for (int iCount = 0; iCount < theLoops.Count(); iCount++)
                {
                    theLoops.ElementAt(iCount).LoadNewData(nextRotationAngle);
                    nextRotationAngle += theLoops.ElementAt(iCount).GetCentralAngle();
                }
            }
        }

        public void ReloadGraphicLoopsWithFixedShareSizes()
        {
            // NB the graphic loops will already have had their numTotalSegments reset,
            // because they are all members of an associated HandOfCards object,
            // which will have changed numTotalSegments in routines like PlayCard.
            if (IsTotalNumSegmentsCorrect(52))
            {
                double angleShare = 360 / (allGraphicLoops.Count());
                double maxCentralAngle = OnePlayerGraphicsLoop.GetMaxCentralAngle(angleShare, allGraphicLoops.Count);

                for (int iCount = 0; iCount < allGraphicLoops.Count(); iCount++)
                {
                    // Set all the angles - each hand of cards gets the same proportion of the circle
                    allGraphicLoops.ElementAt(iCount).SetAngles(maxCentralAngle, angleShare);
                }

                // Calculate rotation angles, then load new data
                double nextRotationAngle = 0;
                double thisCentralAngle = 0;
                for (int iCount = 0; iCount < allGraphicLoops.Count(); iCount++)
                {
                    thisCentralAngle = allGraphicLoops.ElementAt(iCount).GetCentralAngle();
                    nextRotationAngle = (iCount * angleShare) + ((angleShare - thisCentralAngle) / 2);
                    allGraphicLoops.ElementAt(iCount).LoadNewData(nextRotationAngle);
                }
            }
        }

        private void CalculateAnglesWhenEveryoneShares(double numDegreesAvailable,
            int numCardsBeingShared,
            ref List<OnePlayerGraphicsLoop> allPlayerGraphics,
            ref List<OnePlayerGraphicsLoop> currentPlayerGraphics,
            bool bSuppressMinAndMax)
        {
            iRecursionCount++;
            if (iRecursionCount == 15)
            {
                // If the code is working as I intend, particularly with the bounce-counting an' all, this should really really never happen. 
                // But hey, this is head-bending stuff (for me), and we all make mistakes.
                MessageBox.Show("Suspicious recursion!");
            }

            // Leave room for an extra 5 recursive iterations (before we give up and walk away with our heads hung in shame), just so we can debug and see what the hell's going on.
            if (iRecursionCount <= 20)
            {
                int maxNumPlayers = 360 / TopGameConstants.MIN_CENTRAL_ANGLE;
                double runningAngleTotal = 0;
                double finalPlayerAngle = 0;
                bool bSuppressMinAndMaxOnNextCall = false;

                if ((allPlayerGraphics.Count() > maxNumPlayers) || (currentPlayerGraphics.Count() > maxNumPlayers))
                {
                    MessageBox.Show("Too many players");
                }
                else
                {
                    if (currentPlayerGraphics.Count() > 0)
                    {
                        // Calculate all the angles
                        for (int iCount = 0; iCount < currentPlayerGraphics.Count() - 1; iCount++)
                        {
                            runningAngleTotal += currentPlayerGraphics.ElementAt(iCount).CalculateCentralAngle(numDegreesAvailable, numCardsBeingShared, bSuppressMinAndMax);
                        }

                        // Do the last one separately, so we can adjust to take account of rounding.
                        finalPlayerAngle = currentPlayerGraphics.ElementAt(currentPlayerGraphics.Count() - 1).CalculateCentralAngle(numDegreesAvailable, numCardsBeingShared, bSuppressMinAndMax);
                        runningAngleTotal += finalPlayerAngle;

                        // This is the crucial bit: we need everything to add up to numDegreesAvailable, and there are various reasons why it might not.
                        if (runningAngleTotal != numDegreesAvailable)
                        {
                            if (Math.Abs(runningAngleTotal - numDegreesAvailable) < 1)
                            {
                                // It's only a tiny bit out. Just adjust to make up the difference.
                                // If runningAngleTotal is bigger than numDegreesAvailable, then runningAngleTotal - numDegreesAvailable is +ve, so we subtract.
                                // If numDegreesAvailable is bigger than runningAngleTotal, then runningAngleTotal - numDegreesAvailable is -ve, so we still subtract.
                                finalPlayerAngle -= runningAngleTotal - numDegreesAvailable;
                                currentPlayerGraphics.ElementAt(currentPlayerGraphics.Count() - 1).SetCentralAngle(finalPlayerAngle);
                            }
                            else
                            {
                                RelevancyCriteria newRelevancyCriteria = RelevancyCriteria.LookingForMinimums;
                                if (runningAngleTotal > numDegreesAvailable)
                                {
                                    // The minimum angle values must have taken us over numDegreesAvailable. We'll need to recalculate the other angles.
                                    newRelevancyCriteria = RelevancyCriteria.LookingForMinimums;
                                }
                                else if (runningAngleTotal < numDegreesAvailable)
                                {
                                    // The maximum angle values must have taken us under numDegreesAvailable. We'll need to recalculate the other angles.
                                    newRelevancyCriteria = RelevancyCriteria.LookingForMaximums;
                                }

                                if ((newRelevancyCriteria != currentRelevancyCriteria) && (currentRelevancyCriteria != RelevancyCriteria.LookingForNeither))
                                {
                                    // We're bouncing back and forth between minimums and maximums. This can lead to an infinite loop, so we need to keep an eye on it.
                                    iAngleCalculationBounceCount++;

                                    // We'll allow one bounce, because the resulting calculations might sort things out. But after that we'll intervene.
                                    if (iAngleCalculationBounceCount == 2)
                                    {
                                        // To stop the bouncing, we remove both minimums and maximums from the angle-recalculation list,
                                        // and also set bSuppressMinAndMaxOnNextCall to make sure that no new mins or maxes are created.
                                        newRelevancyCriteria = RelevancyCriteria.LookingForBoth;
                                        bSuppressMinAndMaxOnNextCall = true;
                                    }
                                }

                                if (iAngleCalculationBounceCount > 2)
                                {
                                    // If the code is working as I intend, this should never happen. But hey, this is head-bending stuff (for me), and we all make mistakes.
                                    MessageBox.Show("Too many recursion bounces in CalculateAngles!");
                                }
                                else
                                {
                                    currentRelevancyCriteria = newRelevancyCriteria;

                                    List<OnePlayerGraphicsLoop> filteredGraphicList = new List<OnePlayerGraphicsLoop>();
                                    double degreesToDiscountTotal = 0;
                                    int cardsToDiscountTotal = 0;
                                    int numAvailableCards = 0;
                                    double numAvailableDegrees = 0;
                                    // We'll have to recalculate the angles with the min / max ones removed (depending on what the problem is).
                                    // First remove all the relevant ones.
                                    for (int iCount = 0; iCount < allPlayerGraphics.Count(); iCount++)
                                    {
                                        if (IsGraphicRelevant(currentRelevancyCriteria, ref allPlayerGraphics, iCount))
                                        {
                                            // It's a min/max, so we add its figures to the running total, so we know how many degrees and how many cards are being discounted from the new totals.
                                            degreesToDiscountTotal += allPlayerGraphics.ElementAt(iCount).GetCentralAngle();
                                            cardsToDiscountTotal += allPlayerGraphics.ElementAt(iCount).GetNumTotalSegments();
                                        }
                                        else
                                        {
                                            // It's NOT a min/max, so we DO want it in our new list
                                            filteredGraphicList.Add(allPlayerGraphics.ElementAt(iCount));
                                        }
                                    }

                                    // Check there ARE other graphics besides the min/max ones. If not, we just leave it as it was.
                                    // (it shouldn't be possible where minimums are concerned, but it is possible for all the cards 
                                    // to be distributed between only two players, in which case there will only be two max hands and then
                                    // a bit of a gap, and the total angle will not be 360 degrees, but that's fine)
                                    if (filteredGraphicList.Count() > 0)
                                    {
                                        // Now redistribute the angles amongst the remaining players.
                                        numAvailableCards = 52 - cardsToDiscountTotal;
                                        numAvailableDegrees = 360 - degreesToDiscountTotal;
                                        CalculateAnglesWhenEveryoneShares(numAvailableDegrees,
                                                        numAvailableCards,
                                                        ref allPlayerGraphics,
                                                        ref filteredGraphicList,
                                                        bSuppressMinAndMaxOnNextCall);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool IsGraphicRelevant(RelevancyCriteria relevancyCriteria, ref List<OnePlayerGraphicsLoop> graphics, int iGraphicIndex)
        {
            bool bRelevant = false;

            switch (relevancyCriteria)
            {
                case RelevancyCriteria.LookingForMinimums:
                    {
                        bRelevant = graphics.ElementAt(iGraphicIndex).IsMinimumAngleApplied();
                    }
                    break;
                case RelevancyCriteria.LookingForMaximums:
                    {
                        bRelevant = graphics.ElementAt(iGraphicIndex).IsMaximumAngleApplied();
                    }
                    break;
                case RelevancyCriteria.LookingForBoth:
                    {
                        bRelevant = graphics.ElementAt(iGraphicIndex).IsMinimumAngleApplied()
                                    || graphics.ElementAt(iGraphicIndex).IsMaximumAngleApplied();
                    }
                    break;
            }

            return bRelevant;
        }

        public void NewGame()
        {
            playedImages.leftPile.Reset();
            playedImages.rightPile.Reset();
            mainDeck.Shuffle();
            DealCards();
            cardsInPlay.Clear();

            for (int iCount = 0; iCount < theHands.Count(); iCount++)
            {
                // Redisplay the decks
                theHands.ElementAt(iCount).InitialiseImages();
            }

            // ReloadGraphicLoops calculates all the new data for the graphic loops.
            ReloadGraphicLoops();
        }

        public void PlayerWins(int iHandIndex)
        {
            playedImages.Reset();
            //theHands.ElementAt(iHandIndex).PlayerWins(ref bmpDisplayLines, ref cardsInPlay, ref mainForm);

            // Have to store num cards in local variable, because it will change as we remove cards.
            int iNumCards = cardsInPlay.Count();
            int iPreviousCardCount = theHands.ElementAt(iHandIndex).NumCards();

            for (int iCardCount = 0; iCardCount < iNumCards; iCardCount++)
            {
                if (OKToProceed())
                {
                    theHands.ElementAt(iHandIndex).AddCard(cardsInPlay.GetCard(0));
                    cardsInPlay.RemoveTopCard();

                    // ReloadGraphicLoops calculates all the new data for the graphic loops.
                    ReloadGraphicLoops();

                    mainForm.DisplayAllDeckContents();
                    DoPause();
                }
            }

            theHands.ElementAt(iHandIndex).AfterPlayerWins(iPreviousCardCount);

            // No need to clear cards in play, as it will be gradually emptied by PlayerWins.
            // cardsInPlay.Clear();
        }

        public void UndoPlay(int iHandIndex)
        {
            if (theHands.ElementAt(iHandIndex).NumCards() > 0)
            {
                InterlockingCardImages.Side correctSide = (iHandIndex == 0) ? InterlockingCardImages.Side.Left : InterlockingCardImages.Side.Right;
                playedImages.UnPlayCard(correctSide);
                theHands.ElementAt(iHandIndex).UndoPlay(cardsInPlay);
                cardsInPlay.RemoveBottomCard();
            }
        }

        public void TestDisplayLines(int iWhoseTurn)
        {
            /*theHands.ElementAt(0).cardDeck.LoadFullPack();
            theHands.ElementAt(0).transitionLine.LoadFullPack();
            theHands.ElementAt(1).cardDeck.LoadFullPack();
            theHands.ElementAt(1).transitionLine.LoadFullPack();
            cardsInPlay.LoadFullPack();*/

            /*int iStartIndex = iWhoseTurn;
            int iEndIndex = iWhoseTurn;

            if (iWhoseTurn == 0)
            {
                iStartIndex = 1;
                iEndIndex = 17;
            }

            for (int iCount = iStartIndex; iCount <= iEndIndex; iCount++)
            {
                switch (iCount)
                {
                    // The left hand deck line 
                    case 1:
                        {
                            // straight up for 26 cards (originally used reverse numbering)
                            LoadManyColours(
                            theHands.ElementAt(0).cardDeck.displayLine, 26
                                            , 51
                                            , DisplayLineRegion.RegionColour.DarkBlue);
                        }
                        break;
                    case 2:
                        {
                            // slant up to the right for 9 cards (originally used reverse numbering)
                            LoadManyColours(
                            theHands.ElementAt(0).cardDeck.displayLine, 17
                                            , 25
                                            , DisplayLineRegion.RegionColour.Green);
                        }
                        break;
                    case 3:
                        {
                            // across to the right for 8 cards (originally used reverse numbering)
                            LoadManyColours(
                            theHands.ElementAt(0).cardDeck.displayLine, 9
                                            , 16
                                            , DisplayLineRegion.RegionColour.lightBlue);
                        }
                        break;
                    case 4:
                        {
                            // slant down to the right for 9 cards (originally used reverse numbering)
                            LoadManyColours(
                            theHands.ElementAt(0).cardDeck.displayLine, 0
                                            , 8
                                            , DisplayLineRegion.RegionColour.Red);
                        }
                        break;

                        // The left hand transition line 
                    case 5:
                        {
                            // straight down for 26 cards
                            LoadManyColours(
                            theHands.ElementAt(0).transitionLine, 0
                                            , 25
                                            , DisplayLineRegion.RegionColour.DarkBlue);
                        }
                        break;
                    case 6:
                        {
                            // slant down to the right for 9 cards
                            LoadManyColours(
                            theHands.ElementAt(0).transitionLine, 26
                                            , 34
                                            , DisplayLineRegion.RegionColour.Yellow);
                        }
                        break;
                    case 7:
                        {
                            // straight across to the right for 8 cards
                            LoadManyColours(
                            theHands.ElementAt(0).transitionLine, 35
                                            , 42
                                            , DisplayLineRegion.RegionColour.DarkBlue);
                        }
                        break;
                    case 8:
                        {
                        // slant up to the right for 9 cards
                        LoadManyColours(
                        theHands.ElementAt(0).transitionLine, 43
                                        , 51
                                        , DisplayLineRegion.RegionColour.Green);
                        }
                        break;
                        // The right hand deck line
                    case 9:
                        { 
                        // slant up to the right for 9 cards
                        LoadManyColours(
                        theHands.ElementAt(1).cardDeck.displayLine, 0
                                        , 8
                                        , DisplayLineRegion.RegionColour.lightBlue);
                        }
                        break;
                    case 10:
                        {
                        // straight across for 8 cards
                        LoadManyColours(
                        theHands.ElementAt(1).cardDeck.displayLine, 9
                                        , 16
                                        , DisplayLineRegion.RegionColour.Red);
                        }
                        break;
                    case 11:
                        {
                        // slant down to the right for 9 cards
                        LoadManyColours(
                        theHands.ElementAt(1).cardDeck.displayLine, 17
                                        , 25
                                        , DisplayLineRegion.RegionColour.DarkBlue);
                        }
                        break;
                    case 12:
                        {
                        // straight down for 26 cards
                        LoadManyColours(
                        theHands.ElementAt(1).cardDeck.displayLine, 26
                                        , 51
                                        , DisplayLineRegion.RegionColour.Yellow);
                        }
                        break;
                        // The right hand transition line
                    case 13:
                        {
                            // slant down to the right for 9 cards (originally used reverse numbering)
                        LoadManyColours(
                        theHands.ElementAt(1).transitionLine, 43
                                        , 51
                                        , DisplayLineRegion.RegionColour.DarkBlue);
                        }
                        break;
                    case 14:
                        {
                            // straight across for 8 cards (originally used reverse numbering)
                        LoadManyColours(
                        theHands.ElementAt(1).transitionLine, 35
                                        , 42
                                        , DisplayLineRegion.RegionColour.Green);
                        }
                        break;
                    case 15:
                        {
                            // slant up to the right for 9 cards (originally used reverse numbering)
                        LoadManyColours(
                        theHands.ElementAt(1).transitionLine, 26
                                        , 34
                                        , DisplayLineRegion.RegionColour.lightBlue);
                        }
                        break;
                    case 16:
                        {
                            // straight up for 26 cards (originally used reverse numbering)
                        LoadManyColours(
                        theHands.ElementAt(1).transitionLine, 0
                                        , 25
                                        , DisplayLineRegion.RegionColour.Red);
                        }
                        break;
                    // the cards in play
                    case 17:
                        {
                            // straight up for 52 cards
                            LoadManyColours(
                            cardsInPlay.displayLine, 0
                                            , 51
                                            , DisplayLineRegion.RegionColour.DarkBlue);
                        }
                        break;
                }
            }*/
        }

        private void LoadManyColours(DeckDisplayLine displayLine, int iStartIndex, int iEndIndex, DisplayLineRegion.RegionColour colour)
        {
            displayLine.LoadManyColours(ref bmpDisplayLines, iStartIndex, iEndIndex, colour);
        }

        public void AutoPlay()
        {
            iCurrentAutoPlayer = 0;

            while (SomePlayersStillHaveCards(iCurrentAutoPlayer) && OKToProceed())
            {
                // PlayNextCard is recursive, and will keep going until a player has won.
                InitialiseFaceCardTrackers();
                PlayNextCard();

                if (OKToProceed())
                {
                    // Whoever the current player was, that's the one that has lost. 
                    // So we go back to the previous player, as they were the winner.
                    PreviousAutoPlayer();
                    PlayerWins(iCurrentAutoPlayer);
                }
            }

            mainForm.bStop = false;
            mainForm.iStoredPauseSize = mainForm.iPauseSize;
            mainForm.iPauseSize = 0;
            AllPlayersBackInAgain();
        }

        public void DoPause()
        {
            System.Threading.Thread.Sleep(mainForm.iPauseSize);
        }

        public bool OKToProceed()
        {
            bool bOKToProceed = false;
            
            Application.DoEvents();
            bOKToProceed = !(mainForm.bStop);

            if (!bOKToProceed)
            {
                int i = 1;
            }

            return bOKToProceed;
        }

        public void PlayNextCard()
        {
            if (OKToProceed())
            {
                DoPause();
                Card cardPlayed = PlayCard(iCurrentAutoPlayer);

                if (cardPlayed.FaceCard())
                {
                    bFaceCardPlayed = true;
                    iNumCardsPlayedSinceFaceCard = 0;
                    iNumCardsToPay = cardPlayed.NumCardsToPay();
                    NextAutoPlayer();
                    PlayNextCardIfPossible();
                }
                else
                {
                    if (bFaceCardPlayed)
                    {
                        iNumCardsPlayedSinceFaceCard++;
                        if (iNumCardsPlayedSinceFaceCard < iNumCardsToPay)
                        {
                            PlayNextCardIfPossible();
                        }
                    }
                    else
                    {
                        NextAutoPlayer();
                        PlayNextCardIfPossible();
                    }
                }
            }
        }

        public void PlayNextCardIfPossible()
        {
            if (theHands.ElementAt(iCurrentAutoPlayer).NumCards() > 0)
            {
                PlayNextCard();
            }
            else
            {
                theHands.ElementAt(iCurrentAutoPlayer).PlayerIsOut = true;
            }
        }

        public void InitialiseFaceCardTrackers()
        {
            bFaceCardPlayed = false;
            iNumCardsPlayedSinceFaceCard = 0;
            iNumCardsToPay = 0;
        }

        public void NextAutoPlayer()
        {
            iCurrentAutoPlayer++;
            if (iCurrentAutoPlayer == theHands.Count())
            {
                iCurrentAutoPlayer = 0;
            }
            if (theHands.ElementAt(iCurrentAutoPlayer).PlayerIsOut)
            {
                if (SomePlayersStillHaveCards(-1))
                {
                    // Keep going until we find a player who is still in the game.
                    NextAutoPlayer();
                }
            }
        }

        public void PreviousAutoPlayer()
        {
            iCurrentAutoPlayer--;
            if (iCurrentAutoPlayer == -1)
            {
                iCurrentAutoPlayer = theHands.Count() - 1;
            }
        }

        public void AllPlayersBackInAgain()
        {
            for (int iPlayerCount = 0; iPlayerCount < theHands.Count(); iPlayerCount++)
            {
                theHands.ElementAt(iPlayerCount).PlayerIsOut = false;
            }
        }

        public bool SomePlayersStillHaveCards(int iPlayerToIgnore)
        {
            bool bSomePlayersStillHaveCards = false;

            for (int iPlayerCount = 0; (iPlayerCount < theHands.Count()) && !bSomePlayersStillHaveCards; iPlayerCount++)
            {
                if (iPlayerCount != iPlayerToIgnore)
                {
                    if (!theHands.ElementAt(iPlayerCount).PlayerIsOut)
                    {
                        bSomePlayersStillHaveCards = true;
                    }
                }
            }

            return bSomePlayersStillHaveCards;
        }

        public void DisplayGraphicRegion(PaintEventArgs e, int iRegionIndex, int iGraphicIndex, int iColourCycler)
        {
            if (allGraphicLoops.Count() > 0)
            {
                e.Graphics.Clear(OnePlayerGraphicsLoop.GetConstantBackgroundColour());
                allGraphicLoops.ElementAt(iGraphicIndex).Display(iRegionIndex, iColourCycler, e, false);
            }
        }

        public void DisplayPetalRegion(PaintEventArgs e, int iGraphicIndex)
        {
            if (allGraphicLoops.Count() > 0)
            {
                e.Graphics.Clear(OnePlayerGraphicsLoop.GetConstantBackgroundColour());
                allGraphicLoops.ElementAt(iGraphicIndex).DisplayPetalRegion(e);
            }
        }

        public void DisplayGraphicLoops(PaintEventArgs e)
        {
            int maxNumPlayers = 360 / TopGameConstants.MIN_CENTRAL_ANGLE;
            if ((theHands.Count() + 1) > maxNumPlayers)
            {
                MessageBox.Show("Too many players");
            }
            else
            {
                if (allGraphicLoops.Count() > 0)
                {
                    e.Graphics.Clear(OnePlayerGraphicsLoop.GetConstantBackgroundColour());
                    for (int iCount = 0; iCount < allGraphicLoops.Count(); iCount++)
                    {
                        allGraphicLoops.ElementAt(iCount).Display(-1, e);
                    }
                }
            }
        }

        public void CopyCards(ref DeckOfCards player01Cards, ref DeckOfCards player02Cards)
        {
            theHands.ElementAt(0).CopyCards(ref player01Cards);
            theHands.ElementAt(1).CopyCards(ref player02Cards);
        }

        public int NumHands()
        {
            return theHands.Count();
        }// end function
    }

// end class
}// end namespace
