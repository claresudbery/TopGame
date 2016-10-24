using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using nsCard;
using System.Resources;
using Domain.Models;

namespace TopGameWindowsApp
{
    public partial class MainGame : Form
    {
        public enum RelevancyCriteria
        {
            LookingForMinimums
            , LookingForMaximums
            , LookingForBoth
            , LookingForNeither
        }
        public ManyHands allHands;
        List<PictureBox> player1HandImages;
        List<PictureBox> player2HandImages;
        List<PictureBox> playedLeftImages;
        List<PictureBox> playedRightImages;
        private Bitmap bmpDisplayLines;
        private int iTestCount;
        public bool bStop;
        public int iPauseSize;
        public int iStoredPauseSize;
        private int iCurrentRegion;
        private int iCurrentPlayerGraphic;
        private int iColourCycler;

        // These are examples of graphic loops, but are only used for the many-player demo. The main graphics loops are held by the ManyHands object.
        OnePlayerGraphicsLoop _testGraphicsLoop;
        private List<OnePlayerGraphicsLoop> playerGraphics;

        RelevancyCriteria currentRelevancyCriteria;
        int iAngleCalculationBounceCount;
        private int iRecursionCount;
        private CardPicker cardPickingForm;

        public void DisposeAll()
        {
            bmpDisplayLines.Dispose();
            _testGraphicsLoop.Dispose();
            for (int iCount = 0; iCount < playerGraphics.Count(); iCount++)
            {
                playerGraphics.ElementAt(iCount).Dispose();
            }
            allHands.Dispose();
            cardPickingForm.Dispose();
        }

        public MainGame()
        {
            iRecursionCount = 0;
            cardPickingForm = new CardPicker();
            playerGraphics = new List<OnePlayerGraphicsLoop>();
            _testGraphicsLoop = new OnePlayerGraphicsLoop();
            _testGraphicsLoop.LoadNewData(0);
            iCurrentRegion = 0;
            iCurrentPlayerGraphic = -1;
            iColourCycler = 0;

            bStop = false;
            iPauseSize = 0;
            iStoredPauseSize = 60;
            iTestCount = 1;
            bmpDisplayLines = new Bitmap(750, 750, System.Drawing.Imaging.PixelFormat.Format16bppRgb565);// ("c:\\_working\\DisplayLines.bmp");
            allHands = new ManyHands(2, this, ref bmpDisplayLines);
            allHands.DealCards();

            InitializeComponent();

            //pictureBox1.AllowDrop = true;

            //Player1 cards in deck (unplayed)
            imgPlayer1Deck01.AllowDrop = true;
            imgPlayer1Deck02.AllowDrop = true;
            imgPlayer1Deck03.AllowDrop = true;
            imgPlayer1Deck04.AllowDrop = true;
            imgPlayer1Deck05.AllowDrop = true;
            player1HandImages = new List<PictureBox>();
            player1HandImages.Add(imgPlayer1Deck01);
            player1HandImages.Add(imgPlayer1Deck02);
            player1HandImages.Add(imgPlayer1Deck03);
            player1HandImages.Add(imgPlayer1Deck04);
            player1HandImages.Add(imgPlayer1Deck05);
            allHands.AddImages(0, player1HandImages);

            //Player2 cards in deck (unplayed)
            imgPlayer2Deck01.AllowDrop = true;
            imgPlayer2Deck02.AllowDrop = true;
            imgPlayer2Deck03.AllowDrop = true;
            imgPlayer2Deck04.AllowDrop = true;
            imgPlayer2Deck05.AllowDrop = true;
            player2HandImages = new List<PictureBox>();
            player2HandImages.Add(imgPlayer2Deck01);
            player2HandImages.Add(imgPlayer2Deck02);
            player2HandImages.Add(imgPlayer2Deck03);
            player2HandImages.Add(imgPlayer2Deck04);
            player2HandImages.Add(imgPlayer2Deck05);
            allHands.AddImages(1, player2HandImages);

            allHands.InitialiseAllImages();

            //Player1 cards played (will be displayed face up, overlapping)
            imgPlayer1Played01.AllowDrop = true;
            imgPlayer1Played02.AllowDrop = true;
            imgPlayer1Played03.AllowDrop = true;
            imgPlayer1Played04.AllowDrop = true;
            playedLeftImages = new List<PictureBox>();
            playedLeftImages.Add(imgPlayer1Played01);
            playedLeftImages.Add(imgPlayer1Played02);
            playedLeftImages.Add(imgPlayer1Played03);
            playedLeftImages.Add(imgPlayer1Played04);
            allHands.AddPlayedImages(InterlockingCardImages.Side.Left, playedLeftImages);

            //Player2 cards played (will be displayed face up, overlapping)
            imgPlayer2Played01.AllowDrop = true;
            imgPlayer2Played02.AllowDrop = true;
            imgPlayer2Played03.AllowDrop = true;
            imgPlayer2Played04.AllowDrop = true;
            playedRightImages = new List<PictureBox>();
            playedRightImages.Add(imgPlayer2Played01);
            playedRightImages.Add(imgPlayer2Played02);
            playedRightImages.Add(imgPlayer2Played03);
            playedRightImages.Add(imgPlayer2Played04);
            allHands.AddPlayedImages(InterlockingCardImages.Side.Right, playedRightImages);

            // Make a note of all the graphic loop results returned for all possible values of numSegments, so we can unit-test against these expected values.
            allHands.LoadGoldenMasterData();
            //CheckGoldenMasterData();

            allHands.ReloadGraphicLoops();
            DisplayAllDeckContents();
            InitialisePlayerDisplay();

            imgPlayer1Deck01.SendToBack();
            imgPlayer1Deck02.SendToBack();
            imgPlayer1Deck03.SendToBack();
            imgPlayer1Deck04.SendToBack();
            imgPlayer1Deck05.SendToBack();
            imgPlayer2Deck01.SendToBack();
            imgPlayer2Deck02.SendToBack();
            imgPlayer2Deck03.SendToBack();
            imgPlayer2Deck04.SendToBack();
            imgPlayer2Deck05.SendToBack();
            imgPlayer1Played01.SendToBack();
            imgPlayer1Played02.SendToBack();
            imgPlayer1Played03.SendToBack();
            imgPlayer1Played04.SendToBack();
            imgPlayer2Played01.SendToBack();
            imgPlayer2Played02.SendToBack();
            imgPlayer2Played03.SendToBack();
            imgPlayer2Played04.SendToBack();
        }

        private void CheckGoldenMasterData()
        {
            //using (var db = new GoldenMasterDbContext())
            //{
            //    var thing = db.CallsToPrepareActualData
            //        .Where(entity => entity.NumTotalSegments == 1)
            //        .Include(e => e.VitalStatistics)
            //        .Include(e => e.VitalStatistics.origin)
            //        .Include(e => e.VitalStatistics.outerPath)
            //        .Include(e => e.VitalStatistics.startArmDivisionStarts)
            //        .Include(e => e.VitalStatistics.innerArcSquare)
            //        .Include(e => e.TopGameRegions)
            //        .ToList();
            //    var thing4 = thing.Last().TopGameRegions;
            //    if (thing4 != null && thing4.Count > 0)
            //    {
            //        int thing10 = thing4.ElementAt(0).TopGamePoints.ElementAt(0).X;
            //    }
            //    var thing7 = thing.Last().VitalStatistics;
            //    double thing13 = thing7.centralAngle;
            //    double thing16 = thing13;

            //    var thing2 = db.CallsToPrepareActualData
            //        .Where(entity => entity.NumTotalSegments == 3)
            //        .Include(e => e.VitalStatistics)
            //        .Include(e => e.VitalStatistics.origin)
            //        .Include(e => e.VitalStatistics.outerPath)
            //        .Include(e => e.VitalStatistics.startArmDivisionStarts)
            //        .Include(e => e.VitalStatistics.innerArcSquare)
            //        .Include(e => e.TopGameRegions)
            //        .ToList();
            //    var thing5 = thing2.Last().TopGameRegions;
            //    if (thing5 != null && thing5.Count > 0)
            //    {
            //        int thing11 = thing5.ElementAt(0).TopGamePoints.ElementAt(0).X;
            //    }
            //    var thing8 = thing2.Last().VitalStatistics;
            //    double thing14 = thing8.centralAngle;
            //    var thing22 = thing8.origin;
            //    var thing23 = thing8.outerPath;
            //    var thing24 = thing8.startArmDivisionStarts;
            //    var thing25 = thing8.innerArcSquare;
            //    double thing17 = thing14;

            //    var thing3 = db.CallsToPrepareActualData
            //        .Where(entity => entity.NumTotalSegments == 5)
            //        .Include(e => e.VitalStatistics)
            //        .Include(e => e.VitalStatistics.origin)
            //        .Include(e => e.VitalStatistics.outerPath.PointsOnLine)
            //        .Include(e => e.VitalStatistics.startArmDivisionStarts.Points)
            //        .Include(e => e.VitalStatistics.innerArcSquare)
            //        .Include(e => e.VitalStatistics.outerArcSquare)
            //        .Include(e => e.TopGameRegions)
            //        .ToList();
            //    var thing6 = thing3.Last().TopGameRegions;
            //    if (thing6 != null && thing6.Count > 0)
            //    {
            //        int thing12 = thing6.ElementAt(0).TopGamePoints.ElementAt(0).X;
            //    }
            //    var thing9 = thing3.Last().VitalStatistics;
            //    double thing15 = thing9.centralAngle;
            //    var thing26 = thing9.origin;
            //    var thing27 = thing9.outerPath;
            //    var thing28 = thing9.startArmDivisionStarts;
            //    var thing29 = thing9.outerArcSquare;
            //    double thing18 = thing15;
            //}
        }

        public void DisplayAllDeckContents()
        {
            lblPlayer1Cards.Text = allHands.GetDeckContents(0);
            lblPlayer2Cards.Text = allHands.GetDeckContents(1);
            lblCardsInPlay.Text = allHands.GetDeckContents(2);

            lblPlayer1CardColours.Text = allHands.GetColoursAsText(0);
            lblPlayer2CardColours.Text = allHands.GetColoursAsText(1);
            lblCardColoursInPlay.Text = allHands.GetColoursAsText(2);

            lblPauseSize.Text = iPauseSize.ToString();

            //pictureBox2.Refresh();
            //imgPlayer1Played01.Refresh();
            //imgPlayer2Played01.Refresh();
            Refresh();
        }

        public void InitialisePlayerDisplay()
        {
            txtPlayer01CardsAsText.Text = lblPlayer1Cards.Text;
            txtPlayer02CardsAsText.Text = lblPlayer2Cards.Text;
        }

        private void btnPlayer1Plays_Click(object sender, EventArgs e)
        {
            allHands.PlayCard(0);
        }

        private void btnPlayer2Plays_Click(object sender, EventArgs e)
        {
            allHands.PlayCard(1);
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            allHands.NewGame();
            DisplayAllDeckContents();
            InitialisePlayerDisplay();
        }

        private void btnPlayer1Wins_Click(object sender, EventArgs e)
        {
            allHands.PlayerWins(0);
        }

        private void btnPlayer2Wins_Click(object sender, EventArgs e)
        {
            allHands.PlayerWins(1);
        }

        private void btnPlayer1Undo_Click(object sender, EventArgs e)
        {
            allHands.UndoPlay(0);
            DisplayAllDeckContents();
        }

        private void btnPlayer2Undo_Click(object sender, EventArgs e)
        {
            allHands.UndoPlay(1);
            DisplayAllDeckContents();
        }

        // Stop button
        private void button1_Click(object sender, EventArgs e)
        {
            //DisplayLineRegion thing = new DisplayLineRegion();
            //thing.GetRedImage();
            //allHands.BitmapTest();
            /*allHands.TestDisplayLines(iTestCount);
            DisplayAllDeckContents();
            iTestCount++;
            if (iTestCount == 18)
            {
                iTestCount = 1;
            }*/
            bStop = true;
        }

        private void MainGame_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            iPauseSize = iStoredPauseSize;
            allHands.AutoPlay();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        // NB this got called MouseClick by accident, but is actually hooked up to MouseDown events.
        private void imgPlayer1Deck05_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox sendingPictureBox = (PictureBox)sender;
            //sendingPictureBox.DoDragDrop((string)sendingPictureBox.ImageLocation, DragDropEffects.Copy);
            //sendingPictureBox.DoDragDrop((Bitmap)sendingPictureBox.Image, DragDropEffects.All);
            //string test1 = sendingPictureBox.Image.Name;
            sendingPictureBox.DoDragDrop(sendingPictureBox.Image.Tag.ToString(), DragDropEffects.Copy);
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            //if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            //if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                /*Bitmap newImage = new Bitmap((Bitmap)e.Data.GetData(DataFormats.Bitmap));
                pictureBox1.Image = newImage;*/
                
                /*string newImageLocation = (string)e.Data.GetData(DataFormats.Text);
                pictureBox1.ImageLocation = newImageLocation;*/

                string newImageName = (string)e.Data.GetData(DataFormats.Text);
                ResourceManager resourceManager = Resource1.ResourceManager;
                imgPlayer2Deck05.Image = (Bitmap)resourceManager.GetObject(newImageName);

                imgPlayer2Deck05.Refresh();
                PictureBox sendingPictureBox = (PictureBox)sender;
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            /*bmpDisplayLines.RotateFlip(RotateFlipType.Rotate270FlipNone);
            bmpDisplayLines.RotateFlip(RotateFlipType.RotateNoneFlipY);
            e.Graphics.DrawImage(bmpDisplayLines, 0, 0);
            bmpDisplayLines.RotateFlip(RotateFlipType.RotateNoneFlipY);
            bmpDisplayLines.RotateFlip(RotateFlipType.Rotate90FlipNone);     */
            e.Graphics.Clear(OnePlayerGraphicsLoop.GetConstantBackgroundColour());
            if (playerGraphics.Count() > 0)
            {
                if (iCurrentPlayerGraphic == -1)
                {
                    // Display all player graphics.
                    e.Graphics.Clear(System.Drawing.Color.White);
                    for (int iCount = 0; iCount < playerGraphics.Count(); iCount++)
                    {
                        playerGraphics.ElementAt(iCount).Display(-1, 0, e, false);
                    }
                }
                else
                {
                    // Just display the current player graphic.
                    playerGraphics.ElementAt(iCurrentPlayerGraphic).Display(-1, 0, e, false);
                }
            }
            else
            {
                allHands.DisplayGraphicLoops(e);
                //allHands.DisplayPetalRegion(e, 1);
                //allHands.DisplayGraphicRegion(e, iCurrentRegion, 1, iColourCycler);
                //graphicsTest.Display(iCurrentRegion, iColourCycler, e, false);
            }
            //graphicsTest.DisplayFinalRegion(e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // This is clicked when cycling through and displaying regions one by one.
            // Display the next region.
            iCurrentRegion++;
            iColourCycler++;
            if (iCurrentRegion == _testGraphicsLoop.NumRegions())
            {
                iCurrentRegion = 0;
            }
            pictureBox2.Refresh();
        }

        private void btnAllRegions_Click(object sender, EventArgs e)
        {
            iCurrentRegion = -1;
            pictureBox2.Refresh();
        }

        private void btnColourCycle_Click(object sender, EventArgs e)
        {
            iCurrentRegion = -1;
            for (int iCount = 0; iCount < 50; iCount++)
            {
                System.Threading.Thread.Sleep(iPauseSize);
                iColourCycler++;
                pictureBox2.Refresh();
            }
        }

        private void btnSetNumSegments_Click(object sender, EventArgs e)
        {
            _testGraphicsLoop.SetNumTotalSegments(int.Parse(textBox1.Text));
            _testGraphicsLoop.LoadNewData(0);
            iCurrentRegion = -1;
            iColourCycler = 0;
            pictureBox2.Refresh();
        }

        private void btnRotateRegion_Click(object sender, EventArgs e)
        {
            _testGraphicsLoop.RotateByAngle((double)int.Parse(textBox2.Text));
            iCurrentRegion = -1;
            iColourCycler = 0;
            pictureBox2.Refresh();
        }

        private void btnCycleNumSegments_Click(object sender, EventArgs e)
        {
            iCurrentRegion = -1;
            iColourCycler = 0;
            for (int iCount = 1; iCount < 10; iCount++)
            {
                for (int jCount = 1; jCount < 30; jCount++)
                {
                    System.Threading.Thread.Sleep(iPauseSize);
                    _testGraphicsLoop.SetNumTotalSegments(jCount);
                    _testGraphicsLoop.LoadNewData(0);
                    pictureBox2.Refresh();
                }
                for (int kCount = 28; kCount > 1; kCount--)
                {
                    System.Threading.Thread.Sleep(iPauseSize);
                    _testGraphicsLoop.SetNumTotalSegments(kCount);
                    _testGraphicsLoop.LoadNewData(0);
                    pictureBox2.Refresh();
                }
            }
        }

        private void btnManyPlayers_Click(object sender, EventArgs e)
        {
            iPauseSize = 200;
            OnePlayerGraphicsLoop currentGraphic01 = new OnePlayerGraphicsLoop();
            currentGraphic01.SetNumTotalSegments(1);
            playerGraphics.Add(currentGraphic01);

            OnePlayerGraphicsLoop currentGraphic02 = new OnePlayerGraphicsLoop();
            currentGraphic02.SetNumTotalSegments(3);
            playerGraphics.Add(currentGraphic02);

            OnePlayerGraphicsLoop currentGraphic03 = new OnePlayerGraphicsLoop();
            currentGraphic03.SetNumTotalSegments(3);
            playerGraphics.Add(currentGraphic03);

            OnePlayerGraphicsLoop currentGraphic04 = new OnePlayerGraphicsLoop();
            currentGraphic04.SetNumTotalSegments(3);
            playerGraphics.Add(currentGraphic04);

            OnePlayerGraphicsLoop currentGraphic05 = new OnePlayerGraphicsLoop();
            currentGraphic05.SetNumTotalSegments(1);
            playerGraphics.Add(currentGraphic05);

            OnePlayerGraphicsLoop currentGraphic06 = new OnePlayerGraphicsLoop();
            currentGraphic06.SetNumTotalSegments(22);
            playerGraphics.Add(currentGraphic06);

            OnePlayerGraphicsLoop currentGraphic07 = new OnePlayerGraphicsLoop();
            currentGraphic07.SetNumTotalSegments(4);
            playerGraphics.Add(currentGraphic07);

            OnePlayerGraphicsLoop currentGraphic08 = new OnePlayerGraphicsLoop();
            currentGraphic08.SetNumTotalSegments(7);
            playerGraphics.Add(currentGraphic08);

            OnePlayerGraphicsLoop currentGraphic09 = new OnePlayerGraphicsLoop();
            currentGraphic09.SetNumTotalSegments(8);
            playerGraphics.Add(currentGraphic09);

            int maxNumPlayers = 360 / TopGameConstants.MIN_CENTRAL_ANGLE;
            if (playerGraphics.Count() > maxNumPlayers)
            {
                MessageBox.Show("Too many players");
            }
            else
            {
                if (IsTotalNumSegmentsCorrect(52))
                {
                    DisplayMultiPlayers();
                }
            }
            PlayDummyGame();
            int iNumGraphics = playerGraphics.Count();
            for (int iCount = 0; iCount < iNumGraphics; iCount++)
            {
                playerGraphics.Remove(playerGraphics.ElementAt(0));
            }
            bStop = false;
            iStoredPauseSize = iPauseSize;
            iPauseSize = 0;
        }

        private bool IsTotalNumSegmentsCorrect(int correctNumSegments)
        {
            bool bCorrectNumSegments = true;

            int iTotalNumSegments = 0;
            for (int iCount = 0; iCount < playerGraphics.Count(); iCount++)
            {
                iTotalNumSegments += playerGraphics.ElementAt(iCount).GetNumTotalSegments();
            }

            if (iTotalNumSegments != correctNumSegments)
            {
                MessageBox.Show("Doesn't add up to correct number of cards.");
                bCorrectNumSegments = false;
            }

            return bCorrectNumSegments;
        }

        private void DisplayMultiPlayers()
        {
            iCurrentPlayerGraphic = -1; 

            iRecursionCount = 0;
            currentRelevancyCriteria = RelevancyCriteria.LookingForNeither;
            iAngleCalculationBounceCount = 0;

            allHands.ReloadGraphicLoopsWhenAnglesAreShared(ref playerGraphics);

            pictureBox2.Refresh();
        }

        private void CalculateAngles(double numDegreesAvailable, 
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
                                        CalculateAngles(numAvailableDegrees, 
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

        private void btnNextPlayer_Click(object sender, EventArgs e)
        {
            iCurrentPlayerGraphic++;
            if (iCurrentPlayerGraphic == playerGraphics.Count())
            {
                iCurrentPlayerGraphic = 0;
            }
            pictureBox2.Refresh();        
        }

        private void btnDummyGame_Click(object sender, EventArgs e)
        {
            PlayDummyGame();
        }

        private void PlayDummyGame()
        {
            //                      0:1, 1:3, 2:3, 3:3, 4:1, 5:22, 6:4, 7:7, 8:8
            PlayDummyCard(4, 0); // 0:2, 1:3, 2:3, 3:3, 4:0, 5:22, 6:4, 7:7, 8:8
            PlayDummyCard(5, 0); // 0:3, 1:3, 2:3, 3:3, 4:0, 5:21, 6:4, 7:7, 8:8
            PlayDummyCard(5, 0); // 0:4, 1:3, 2:3, 3:3, 4:0, 5:20, 6:4, 7:7, 8:8
            PlayDummyCard(5, 0); // 0:5, 1:3, 2:3, 3:3, 4:0, 5:19, 6:4, 7:7, 8:8
            PlayDummyCard(5, 0); // 0:6, 1:3, 2:3, 3:3, 4:0, 5:18, 6:4, 7:7, 8:8
            PlayDummyCard(0, 4); // 0:5, 1:3, 2:3, 3:3, 4:1, 5:18, 6:4, 7:7, 8:8
            PlayDummyCard(0, 4); // 0:4, 1:3, 2:3, 3:3, 4:2, 5:18, 6:4, 7:7, 8:8
            PlayDummyCard(0, 4); // 0:3, 1:3, 2:3, 3:3, 4:3, 5:18, 6:4, 7:7, 8:8
            PlayDummyCard(0, 4); // 0:2, 1:3, 2:3, 3:3, 4:4, 5:18, 6:4, 7:7, 8:8
            PlayDummyCard(0, 4); // 0:1, 1:3, 2:3, 3:3, 4:5, 5:18, 6:4, 7:7, 8:8
            PlayDummyCard(4, 0); // 0:2, 1:3, 2:3, 3:3, 4:4, 5:18, 6:4, 7:7, 8:8
            PlayDummyCard(5, 0); // 0:3, 1:3, 2:3, 3:3, 4:4, 5:17, 6:4, 7:7, 8:8
            PlayDummyCard(6, 0); // 0:4, 1:3, 2:3, 3:3, 4:4, 5:17, 6:3, 7:7, 8:8
            PlayDummyCard(7, 0); // 0:5, 1:3, 2:3, 3:3, 4:4, 5:17, 6:3, 7:6, 8:8
            PlayDummyCard(8, 0); // 0:6, 1:3, 2:3, 3:3, 4:4, 5:17, 6:3, 7:6, 8:7
            PlayDummyCard(1, 0); // 0:7, 1:2, 2:3, 3:3, 4:4, 5:17, 6:3, 7:6, 8:7
            PlayDummyCard(2, 0); // 0:8, 1:2, 2:2, 3:3, 4:4, 5:17, 6:3, 7:6, 8:7
            PlayDummyCard(3, 0); // 0:9, 1:2, 2:2, 3:2, 4:4, 5:17, 6:3, 7:6, 8:7

            PlayDummyCard(4, 0); // 0:10, 1:2, 2:2, 3:2, 4:3, 5:17, 6:3, 7:6, 8:7
            PlayDummyCard(5, 0); // 0:11, 1:2, 2:2, 3:2, 4:3, 5:16, 6:3, 7:6, 8:7
            PlayDummyCard(6, 0); // 0:12, 1:2, 2:2, 3:2, 4:3, 5:16, 6:2, 7:6, 8:7
            PlayDummyCard(7, 0); // 0:13, 1:2, 2:2, 3:2, 4:3, 5:16, 6:2, 7:5, 8:7
            PlayDummyCard(8, 0); // 0:14, 1:2, 2:2, 3:2, 4:3, 5:16, 6:2, 7:5, 8:6
            PlayDummyCard(1, 0); // 0:15, 1:1, 2:2, 3:2, 4:3, 5:16, 6:2, 7:5, 8:6
            PlayDummyCard(2, 0); // 0:16, 1:1, 2:1, 3:2, 4:3, 5:16, 6:2, 7:5, 8:6
            PlayDummyCard(3, 0); // 0:17, 1:1, 2:1, 3:1, 4:3, 5:16, 6:2, 7:5, 8:6

            PlayDummyCard(4, 0); // 0:18, 1:1, 2:1, 3:1, 4:2, 5:16, 6:2, 7:5, 8:6
            PlayDummyCard(5, 0); // 0:19, 1:1, 2:1, 3:1, 4:2, 5:15, 6:2, 7:5, 8:6
            PlayDummyCard(6, 0); // 0:20, 1:1, 2:1, 3:1, 4:2, 5:15, 6:1, 7:5, 8:6
            PlayDummyCard(7, 0); // 0:21, 1:1, 2:1, 3:1, 4:2, 5:15, 6:1, 7:4, 8:6
            PlayDummyCard(8, 0); // 0:22, 1:1, 2:1, 3:1, 4:2, 5:15, 6:1, 7:4, 8:5
            PlayDummyCard(1, 0); // 0:23, 1:0, 2:1, 3:1, 4:2, 5:15, 6:1, 7:4, 8:5
            PlayDummyCard(2, 0); // 0:24, 1:0, 2:0, 3:1, 4:2, 5:15, 6:1, 7:4, 8:5
            PlayDummyCard(3, 0); // 0:25, 1:0, 2:0, 3:0, 4:2, 5:15, 6:1, 7:4, 8:5

            PlayDummyCard(4, 0); // 0:26, 1:0, 2:0, 3:0, 4:1, 5:15, 6:1, 7:4, 8:5
            PlayDummyCard(5, 0); // 0:27, 1:0, 2:0, 3:0, 4:1, 5:14, 6:1, 7:4, 8:5
            PlayDummyCard(6, 0); // 0:28, 1:0, 2:0, 3:0, 4:1, 5:14, 6:0, 7:4, 8:5
            PlayDummyCard(7, 0); // 0:29, 1:0, 2:0, 3:0, 4:1, 5:14, 6:0, 7:3, 8:5
            PlayDummyCard(8, 0); // 0:30, 1:0, 2:0, 3:0, 4:1, 5:14, 6:0, 7:3, 8:4

            // Player 5 pays player 4
            PlayDummyCard(4, 0); // 0:31, 1:0, 2:0, 3:0, 4:0, 5:14, 6:0, 7:3, 8:4
            PlayDummyCard(5, 0); // 0:32, 1:0, 2:0, 3:0, 4:0, 5:13, 6:0, 7:3, 8:4
            PlayDummyCard(5, 0); // 0:33, 1:0, 2:0, 3:0, 4:0, 5:12, 6:0, 7:3, 8:4
            PlayDummyCard(5, 0); // 0:34, 1:0, 2:0, 3:0, 4:0, 5:11, 6:0, 7:3, 8:4
            PlayDummyCard(5, 0); // 0:35, 1:0, 2:0, 3:0, 4:0, 5:10, 6:0, 7:3, 8:4

            // player 4 wins
            PlayDummyCard(0, 4); // 0:34, 1:0, 2:0, 3:0, 4:1, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:33, 1:0, 2:0, 3:0, 4:2, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:32, 1:0, 2:0, 3:0, 4:3, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:31, 1:0, 2:0, 3:0, 4:4, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:30, 1:0, 2:0, 3:0, 4:5, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:29, 1:0, 2:0, 3:0, 4:6, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:28, 1:0, 2:0, 3:0, 4:7, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:27, 1:0, 2:0, 3:0, 4:8, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:26, 1:0, 2:0, 3:0, 4:9, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:25, 1:0, 2:0, 3:0, 4:10, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:24, 1:0, 2:0, 3:0, 4:10, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:23, 1:0, 2:0, 3:0, 4:11, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:22, 1:0, 2:0, 3:0, 4:12, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:21, 1:0, 2:0, 3:0, 4:13, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:20, 1:0, 2:0, 3:0, 4:14, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:19, 1:0, 2:0, 3:0, 4:15, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:18, 1:0, 2:0, 3:0, 4:16, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:17, 1:0, 2:0, 3:0, 4:17, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:16, 1:0, 2:0, 3:0, 4:18, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:15, 1:0, 2:0, 3:0, 4:19, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:14, 1:0, 2:0, 3:0, 4:20, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:13, 1:0, 2:0, 3:0, 4:21, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:12, 1:0, 2:0, 3:0, 4:22, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:11, 1:0, 2:0, 3:0, 4:23, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:10, 1:0, 2:0, 3:0, 4:24, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:9, 1:0, 2:0, 3:0, 4:25, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:8, 1:0, 2:0, 3:0, 4:26 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:7, 1:0, 2:0, 3:0, 4:27, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:6, 1:0, 2:0, 3:0, 4:28, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:5, 1:0, 2:0, 3:0, 4:29, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:4, 1:0, 2:0, 3:0, 4:30, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:3, 1:0, 2:0, 3:0, 4:31, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:2, 1:0, 2:0, 3:0, 4:32, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:1, 1:0, 2:0, 3:0, 4:33, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(0, 4); // 0:0, 1:0, 2:0, 3:0, 4:34, 5:10, 6:0, 7:3, 8:4

            PlayDummyCard(4, 0); // 0:1, 1:0, 2:0, 3:0, 4:33, 5:10, 6:0, 7:3, 8:4
            PlayDummyCard(5, 0); // 0:2, 1:0, 2:0, 3:0, 4:33, 5:9, 6:0, 7:3, 8:4
            PlayDummyCard(7, 0); // 0:3, 1:0, 2:0, 3:0, 4:33, 5:9, 6:0, 7:2, 8:4
            PlayDummyCard(8, 0); // 0:4, 1:0, 2:0, 3:0, 4:33, 5:9, 6:0, 7:2, 8:3

            PlayDummyCard(4, 0); // 0:5, 1:0, 2:0, 3:0, 4:32, 5:9, 6:0, 7:2, 8:3
            PlayDummyCard(5, 0); // 0:6, 1:0, 2:0, 3:0, 4:32, 5:8, 6:0, 7:2, 8:3
            PlayDummyCard(7, 0); // 0:7, 1:0, 2:0, 3:0, 4:32, 5:8, 6:0, 7:1, 8:3
            PlayDummyCard(8, 0); // 0:8, 1:0, 2:0, 3:0, 4:32, 5:8, 6:0, 7:1, 8:2

            PlayDummyCard(4, 0); // 0:9, 1:0, 2:0, 3:0, 4:31, 5:8, 6:0, 7:1, 8:2
            PlayDummyCard(5, 0); // 0:10, 1:0, 2:0, 3:0, 4:31, 5:7, 6:0, 7:1, 8:2
            PlayDummyCard(7, 0); // 0:11, 1:0, 2:0, 3:0, 4:31, 5:7, 6:0, 7:0, 8:2
            PlayDummyCard(8, 0); // 0:12, 1:0, 2:0, 3:0, 4:31, 5:7, 6:0, 7:0, 8:1

            PlayDummyCard(4, 0); // 0:13, 1:0, 2:0, 3:0, 4:30, 5:7, 6:0, 7:0, 8:1
            PlayDummyCard(5, 0); // 0:14, 1:0, 2:0, 3:0, 4:30, 5:6, 6:0, 7:0, 8:1
            PlayDummyCard(8, 0); // 0:15, 1:0, 2:0, 3:0, 4:30, 5:6, 6:0, 7:0, 8:0

            PlayDummyCard(4, 0); // 0:16, 1:0, 2:0, 3:0, 4:30, 5:6, 6:0, 7:0, 8:0
            PlayDummyCard(5, 0); // 0:17, 1:0, 2:0, 3:0, 4:30, 5:5, 6:0, 7:0, 8:0

            PlayDummyCard(4, 0); // 0:18, 1:0, 2:0, 3:0, 4:29, 5:5, 6:0, 7:0, 8:0
            PlayDummyCard(5, 0); // 0:19, 1:0, 2:0, 3:0, 4:29, 5:4, 6:0, 7:0, 8:0

            PlayDummyCard(4, 0); // 0:20, 1:0, 2:0, 3:0, 4:28, 5:4, 6:0, 7:0, 8:0
            PlayDummyCard(5, 0); // 0:21, 1:0, 2:0, 3:0, 4:28, 5:3, 6:0, 7:0, 8:0
            PlayDummyCard(5, 0); // 0:22, 1:0, 2:0, 3:0, 4:28, 5:2, 6:0, 7:0, 8:0
            PlayDummyCard(5, 0); // 0:23, 1:0, 2:0, 3:0, 4:28, 5:1, 6:0, 7:0, 8:0
            PlayDummyCard(5, 0); // 0:24, 1:0, 2:0, 3:0, 4:28, 5:0, 6:0, 7:0, 8:0

            PlayDummyCard(0, 4); // 0:23, 1:0, 2:0, 3:0, 4:29, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:22, 1:0, 2:0, 3:0, 4:30, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:21, 1:0, 2:0, 3:0, 4:31, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:20, 1:0, 2:0, 3:0, 4:32, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:19, 1:0, 2:0, 3:0, 4:33, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:18, 1:0, 2:0, 3:0, 4:34, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:17, 1:0, 2:0, 3:0, 4:35, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:16, 1:0, 2:0, 3:0, 4:36, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:15, 1:0, 2:0, 3:0, 4:37, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:14, 1:0, 2:0, 3:0, 4:38, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:13, 1:0, 2:0, 3:0, 4:39, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:12, 1:0, 2:0, 3:0, 4:40, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:11, 1:0, 2:0, 3:0, 4:41, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:10, 1:0, 2:0, 3:0, 4:42, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:09, 1:0, 2:0, 3:0, 4:43, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:08, 1:0, 2:0, 3:0, 4:44, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:07, 1:0, 2:0, 3:0, 4:45, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:06, 1:0, 2:0, 3:0, 4:46, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:05, 1:0, 2:0, 3:0, 4:47, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:04, 1:0, 2:0, 3:0, 4:48, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:03, 1:0, 2:0, 3:0, 4:49, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:02, 1:0, 2:0, 3:0, 4:50, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:01, 1:0, 2:0, 3:0, 4:51, 5:0, 6:0, 7:0, 8:0
            PlayDummyCard(0, 4); // 0:00, 1:0, 2:0, 3:0, 4:52, 5:0, 6:0, 7:0, 8:0
        }

        private void PlayDummyCard(int iFromPlayer, int iToPlayer)
        {
            if (playerGraphics.ElementAt(iFromPlayer).GetNumTotalSegments() <= 0)
            {
                MessageBox.Show("Not enough cards in hand");
            }
            else
            {
                playerGraphics.ElementAt(iFromPlayer).RemoveTopSegment();
                playerGraphics.ElementAt(iToPlayer).AddSegment();
                if (IsTotalNumSegmentsCorrect(52))
                {
                    Application.DoEvents();
                    if (!bStop)
                    {
                        System.Threading.Thread.Sleep(iPauseSize);
                        DisplayMultiPlayers();
                    }
                }
            }
        }

        public void ReloadGraphicLoops()
        {
            allHands.ReloadGraphicLoops();
        }

        private void btnCardPicker_Click(object sender, EventArgs e)
        {
            if (cardPickingForm.IsDisposed)
            {
                cardPickingForm = new CardPicker();
            }
            if (allHands.NumHands() == 2)
            {
                cardPickingForm.Show(this);
            }
            else
            {
                MessageBox.Show("You can only do this for a 2-player game.");
            }
        }

        private void btnCardPicker_Leave(object sender, EventArgs e)
        {

        }

        private void MainGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisposeAll();
        }
        
        public void CopyCards(ref DeckOfCards player01Cards, ref DeckOfCards player02Cards)
        {
            allHands.CopyCards(ref player01Cards, ref player02Cards);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (iPauseSize >= 30)
            {
                iPauseSize -= 30;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            iPauseSize += 30;
        }

        private void txtPauseSize_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSetPause_Click(object sender, EventArgs e)
        {
            iPauseSize = int.Parse(txtPauseSize.Text);
            iStoredPauseSize = iPauseSize;
            DisplayAllDeckContents();
        }

        private void btnDefaultGame_Click(object sender, EventArgs e)
        {
            allHands.NewGame();
            DisplayAllDeckContents();
            InitialisePlayerDisplay();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("oi");
        }// end function
    }// end class
}// end namespace
