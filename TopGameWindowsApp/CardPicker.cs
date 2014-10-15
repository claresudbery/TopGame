using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using nsCard;

namespace TopGameWindowsApp
{
    public partial class CardPicker : Form
    {
        #region Declarations
        #region Associated Objects
        public enum CardPickerDeck
        {
            Player01,
            Player02,
            SourceDeck,
            None
        }

        struct CardPictureInfo
        {
            public int iImageIndex;
            public CardPickerDeck whichDeck;
            public string strImageLabelPart1;
            public string strImageLabelPart2;
            public bool bPopulated;
        }
        #endregion 

        private List<System.Windows.Forms.PictureBox> player01CardImages;
        private List<System.Windows.Forms.PictureBox> player02CardImages;
        private List<System.Windows.Forms.PictureBox> deckCardImages;

        private DeckOfCards player01Cards;
        private DeckOfCards player02Cards;
        private DeckOfCards sourceDeck; // This will have cards added / removed as they are placed in either player's hand.
        private DeckOfCards cleanSourceDeck; // This is kept clean as a reference point containing all cards, for lookup purposes.

        private const int startXPos = 5;
        private const int startYPos = 5;
        private const int cardWidth = 71;
        private const int cardHeight = 96;
        private const int numRows = 6;
        private const float scaleFactorVal = 0.72F;

        private string removedImageName;
        private CardPictureInfo emptiedPictureInfo;
        private string emptiedPictureBoxName;

        #endregion 

        #region Initialisation
        public CardPicker()
        {
            cleanSourceDeck = new DeckOfCards(DeckDisplayLine.LoadStyle.TopLoader);
            sourceDeck = new DeckOfCards(DeckDisplayLine.LoadStyle.TopLoader);
            player01Cards = new DeckOfCards(DeckDisplayLine.LoadStyle.TopLoader);
            player02Cards = new DeckOfCards(DeckDisplayLine.LoadStyle.TopLoader);

            removedImageName = "";
            emptiedPictureBoxName = "";
            emptiedPictureInfo = new CardPictureInfo();
            emptiedPictureInfo.strImageLabelPart1 = "";
            emptiedPictureInfo.strImageLabelPart2 = "";
            emptiedPictureInfo.whichDeck = CardPickerDeck.None;
            emptiedPictureInfo.iImageIndex = -1;
            emptiedPictureInfo.bPopulated = false;

            InitializeComponent();
            
            // What we do is load the player decks with spuds at the start, just so we can display the deck contents as text as we go along.
            player01Cards.LoadSpuds(26);
            player02Cards.LoadSpuds(26); 
            
            sourceDeck.LoadFullPack();
            cleanSourceDeck.LoadFullPack();
            
            CreateCardPickerImages();
            
            RedisplayCardText();
            DoScale(this);
        }

        private void DoScale(Control control)
        {
            System.Drawing.SizeF scaleFactor = new System.Drawing.SizeF();
            scaleFactor.Height = scaleFactorVal;
            scaleFactor.Width = scaleFactorVal;
            control.Scale(scaleFactor);
        }

        private void CreateCardPickerImages()
        {
            player01CardImages = new List<System.Windows.Forms.PictureBox>();
            player02CardImages = new List<System.Windows.Forms.PictureBox>();
            deckCardImages = new List<System.Windows.Forms.PictureBox>();

            CreateIndividualImages(ref player01CardImages, 26);
            CreateIndividualImages(ref player02CardImages, 26);
            CreateIndividualImages(ref deckCardImages, 52);
            /*CreateIndividualImages(ref player01CardImages, 2);
            CreateIndividualImages(ref player02CardImages, 2);
            CreateIndividualImages(ref deckCardImages, 2);*/

            BeginInits(ref player01CardImages);
            BeginInits(ref player02CardImages);
            BeginInits(ref deckCardImages);

            ConfigureImages(ref player01CardImages, "Player 1", 1, 0, false, CardPickerDeck.Player01);
            ConfigureImages(ref player02CardImages, "Player 2", player01CardImages.Count() + 1, 14, false, CardPickerDeck.Player02);
            ConfigureImages(ref deckCardImages, "deckCardImages", player01CardImages.Count() + player02CardImages.Count() + 1, 5, true, CardPickerDeck.SourceDeck);
            /*ConfigureImages(ref player01CardImages, "Player 1", 1, 0, false, CardPickerDeck.Player01);
            ConfigureImages(ref player02CardImages, "Player 2", player01CardImages.Count() + 1, 3, false, CardPickerDeck.Player02);
            ConfigureImages(ref deckCardImages, "deckCardImages", player01CardImages.Count() + player02CardImages.Count() + 1, 2, true, CardPickerDeck.SourceDeck);*/
    
            EndInits(ref player01CardImages);
            EndInits(ref player02CardImages);
            EndInits(ref deckCardImages);
        }

        private void CreateIndividualImages(ref List<System.Windows.Forms.PictureBox> cardContainer, int iNumImages)
        {
            for (int iCount = 0; iCount < iNumImages; iCount++)
            {
                cardContainer.Add(new System.Windows.Forms.PictureBox());
            }
        }
        
        private void BeginInits(ref List<System.Windows.Forms.PictureBox> cardContainer)
        {
            for (int iCount = 0; iCount < cardContainer.Count(); iCount++)
            {
                ((System.ComponentModel.ISupportInitialize)(cardContainer.ElementAt(iCount))).BeginInit();
            }
        }
        
        private void ConfigureImages(ref List<System.Windows.Forms.PictureBox> imageContainer, 
                                    string containerName, 
                                    int iStartTabIndex, 
                                    int iStartColumn, 
                                    bool bUseSourceDeck,
                                    CardPickerDeck whichDeck)
        {
            ResourceManager resourceManager = Resource1.ResourceManager;
            for (int iCount = 0; iCount < imageContainer.Count(); iCount++)
            {
                CardPictureInfo pictureBoxInfo = new CardPictureInfo();
                int iPositionNumber = iCount + 1;
                int iColumnNumber = iStartColumn + iCount / numRows;
                int iRowNumber = iCount % numRows;
                if (bUseSourceDeck)
                {
                    imageContainer.ElementAt(iCount).Image = (Bitmap)resourceManager.GetObject(cleanSourceDeck.GetCardImageName(iCount));
                    DoScale(imageContainer.ElementAt(iCount));
                    imageContainer.ElementAt(iCount).Image.Tag = cleanSourceDeck.GetCardImageName(iCount);
                    DoScale(imageContainer.ElementAt(iCount));
                    /*imageContainer.ElementAt(iCount).Image = (Bitmap)resourceManager.GetObject("xx");
                    imageContainer.ElementAt(iCount).Image.Tag = "xx";*/
                    pictureBoxInfo.strImageLabelPart1 = "Source Deck";
                    pictureBoxInfo.strImageLabelPart2 = "";
                    pictureBoxInfo.bPopulated = true;
                }
                else
                {
                    pictureBoxInfo.bPopulated = false;
                    pictureBoxInfo.strImageLabelPart1 = containerName;
                    pictureBoxInfo.strImageLabelPart2 = "Card " + iPositionNumber;
                }
                pictureBoxInfo.whichDeck = whichDeck;
                pictureBoxInfo.iImageIndex = iCount;
                imageContainer.ElementAt(iCount).Tag = pictureBoxInfo;
                imageContainer.ElementAt(iCount).BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                imageContainer.ElementAt(iCount).ImageLocation = "";
                imageContainer.ElementAt(iCount).Location = new System.Drawing.Point(startXPos + iColumnNumber * cardWidth, startYPos + iRowNumber * cardHeight);
                imageContainer.ElementAt(iCount).Name = containerName + " image " + iCount;
                imageContainer.ElementAt(iCount).Size = new System.Drawing.Size(cardWidth, cardHeight);
                imageContainer.ElementAt(iCount).TabIndex = iStartTabIndex + iCount;
                imageContainer.ElementAt(iCount).TabStop = true;
                imageContainer.ElementAt(iCount).AllowDrop = true;
                imageContainer.ElementAt(iCount).DragDrop += new System.Windows.Forms.DragEventHandler(this.OnPictureBoxDragDrop);
                imageContainer.ElementAt(iCount).DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
                imageContainer.ElementAt(iCount).MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
                imageContainer.ElementAt(iCount).Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);

                this.Controls.Add(imageContainer.ElementAt(iCount));
            }
        }
        
        private void EndInits(ref List<System.Windows.Forms.PictureBox> cardContainer)
        {
            for (int iCount = 0; iCount < cardContainer.Count(); iCount++)
            {
                ((System.ComponentModel.ISupportInitialize)(cardContainer.ElementAt(iCount))).EndInit();
            }
        }
        #endregion 

        #region Card Management
        private void SaveCards(ref List<System.Windows.Forms.PictureBox> cardImages, ref DeckOfCards cardDeck, ref DeckOfCards sourceDeck)
        {
            cardDeck.Clear();
            for (int iCount = 0; iCount < cardImages.Count(); iCount++)
            {
                if (cardImages.ElementAt(iCount).Image != null)
                {
                    Card newCard = new Card();
                    newCard = sourceDeck.FindCard(cardImages.ElementAt(iCount).Image.Tag.ToString());
                    cardDeck.AddCard(newCard, false);
                }
            }
        }

        private void SaveCards()
        {
            SaveCards(ref player01CardImages, ref player01Cards, ref cleanSourceDeck);
            SaveCards(ref player02CardImages, ref player02Cards, ref cleanSourceDeck);
        }
        
        private void SaveOneCard(ref List<System.Windows.Forms.PictureBox> cardImages, ref DeckOfCards cardDeck, ref DeckOfCards sourceDeck, int iImageIndex)
        {
            Card newCard = new Card(); 
            if (cardImages.ElementAt(iImageIndex).Image != null)
            {
                newCard = sourceDeck.FindCard(cardImages.ElementAt(iImageIndex).Image.Tag.ToString());
            }
            else
            {
                // No card image: we'll represent this as a random spud.
                newCard.InitialiseAsSpud();
            }
            cardDeck.ReplaceCard(iImageIndex, newCard, false);
        }

        public bool AllCardsAssigned()
        {
            bool bAllCardsAssigned = true;

            for (int iCount = 0; (iCount < player01CardImages.Count()) && bAllCardsAssigned; iCount++)
            {
                // If either of the players has any null images, then all cards are not assigned and we can stop checking.
                //bAllCardsAssigned = ((CardPictureInfo)player01CardImages.ElementAt(iCount).Tag).bPopulated;
                bAllCardsAssigned = (player01CardImages.ElementAt(iCount).Image != null);                
                
            }
            if (bAllCardsAssigned)
            {
                for (int iCount = 0; (iCount < player02CardImages.Count()) && bAllCardsAssigned; iCount++)
                {
                    // If either of the players has any null images, then all cards are not assigned and we can stop checking.
                    //bAllCardsAssigned = ((CardPictureInfo)player02CardImages.ElementAt(iCount).Tag).bPopulated;
                    bAllCardsAssigned = (player02CardImages.ElementAt(iCount).Image != null);                
                }
            }

            return bAllCardsAssigned;
        }

        private void AssignLeftovers()
        {
            int iDeckCardImagesCount = 0;

            // Cycle through each player's cards. For every one that has not yet been assigned, pull the next available one from the unassigned deck.
            AssignLeftovers(ref iDeckCardImagesCount, ref player01CardImages);
            AssignLeftovers(ref iDeckCardImagesCount, ref player02CardImages);
        }

        private void AssignLeftovers(ref int iDeckCardImagesCount, ref List<System.Windows.Forms.PictureBox> cardImages)
        {
            string newImageName = "";
            ResourceManager resourceManager = Resource1.ResourceManager;
            for (int iCount = 0; iCount < cardImages.Count(); iCount++)
            {
                if (cardImages.ElementAt(iCount).Image == null)
                //if (!((CardPictureInfo)cardImages.ElementAt(iCount).Tag).bPopulated)
                {
                    if (iDeckCardImagesCount < deckCardImages.Count())
                    {
                        // No image assigned here yet. Let's find an available one.                        
                        //while (!((CardPictureInfo)deckCardImages.ElementAt(iDeckCardImagesCount).Tag).bPopulated)
                        while (deckCardImages.ElementAt(iDeckCardImagesCount).Image == null)
                        {
                            iDeckCardImagesCount++;
                        }
                        //if (((CardPictureInfo)deckCardImages.ElementAt(iDeckCardImagesCount).Tag).bPopulated)
                        if (deckCardImages.ElementAt(iDeckCardImagesCount).Image != null)
                        {
                            newImageName = deckCardImages.ElementAt(iDeckCardImagesCount).Image.Tag.ToString();
                            cardImages.ElementAt(iCount).Image = (Bitmap)resourceManager.GetObject(newImageName);
                            cardImages.ElementAt(iCount).Image.Tag = newImageName;
                            deckCardImages.ElementAt(iDeckCardImagesCount).Image = null;

                            CardPictureInfo tempInfo = new CardPictureInfo();
                            tempInfo = (CardPictureInfo)(deckCardImages.ElementAt(iDeckCardImagesCount).Tag);
                            tempInfo.bPopulated = false;
                            deckCardImages.ElementAt(iDeckCardImagesCount).Tag = tempInfo;
                            bool bTest = ((CardPictureInfo)(deckCardImages.ElementAt(iDeckCardImagesCount).Tag)).bPopulated;
                        }
                    }
                }
            }

            Refresh();
        }

        void RedisplayCardText()
        {
            lblPlayer01Cards.Text = player01Cards.GetDeckContents();
            lblPlayer01Cards.Refresh();
            lblPlayer02Cards.Text = player02Cards.GetDeckContents();
            lblPlayer02Cards.Refresh();
        }
     
        void AdjustCardsInDeck(CardPictureInfo pictureInfo)
        {
            switch(pictureInfo.whichDeck)
            {
                case CardPickerDeck.Player01:
                    {
                        SaveOneCard(ref player01CardImages, ref player01Cards, ref cleanSourceDeck, pictureInfo.iImageIndex);
                    }
                    break;
                case CardPickerDeck.Player02:
                    {
                        SaveOneCard(ref player02CardImages, ref player02Cards, ref cleanSourceDeck, pictureInfo.iImageIndex);
                    }
                    break;
                case CardPickerDeck.SourceDeck:
                    {
                        SaveOneCard(ref deckCardImages, ref sourceDeck, ref cleanSourceDeck, pictureInfo.iImageIndex);
                    }
                    break;
            }
            RedisplayCardText();
        }
        #endregion 

        #region Event Handlers (mostly button-click)
        private void btnSaveCards_Click(object sender, EventArgs e)
        {
            if (AllCardsAssigned())
            {
                SaveCards();
                ((MainGame)Owner).CopyCards(ref player01Cards, ref player02Cards);
                ((MainGame)Owner).ReloadGraphicLoops();
                ((MainGame)Owner).DisplayAllDeckContents();
                ((MainGame)Owner).InitialisePlayerDisplay();
                Close();
            }
            else
            {
                MessageBox.Show("You must assign every single card to one player or the other.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSpuds_Click(object sender, EventArgs e)
        {
            AssignLeftovers();
            SaveCards();
            RedisplayCardText();
        }
        #endregion 

        #region Drag and Drop Functionality

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            PictureBox sendingPictureBox = (PictureBox)sender;

            if (sendingPictureBox.Name == emptiedPictureBoxName)
            {
                // This picture box was emptied but now needs restoring (which we can tell because otherwise by now emptiedPictureBoxName would be empty again)
                ResourceManager resourceManager = Resource1.ResourceManager;

                sendingPictureBox.Image = (Bitmap)resourceManager.GetObject(removedImageName);
                sendingPictureBox.Image.Tag = removedImageName;
                sendingPictureBox.Refresh();
                CardPictureInfo tempInfo = new CardPictureInfo();
                tempInfo = (CardPictureInfo)(sendingPictureBox.Tag);
                tempInfo.bPopulated = true;
                sendingPictureBox.Tag = tempInfo;
                bool bTest = ((CardPictureInfo)sendingPictureBox.Tag).bPopulated;

                // Redisplay the new deck contents
                AdjustCardsInDeck((CardPictureInfo)sendingPictureBox.Tag);
            }

            //if (!(((CardPictureInfo)sendingPictureBox.Tag).bPopulated))                
            if (sendingPictureBox.Image == null)
            {
                // It hasn't got a card image, so instead we display text indicating either the card's position in the pack or if this is just a source-card placeholder.
                System.Drawing.Font font = new System.Drawing.Font("Arial", 7);
                SolidBrush myBrush = new SolidBrush(System.Drawing.Color.Black);
                //System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red, 1);
                Rectangle rect = new Rectangle(1, 15, 300, 115);
                StringFormat format1 = new StringFormat(StringFormatFlags.NoClip);
                e.Graphics.DrawString(((CardPictureInfo)sendingPictureBox.Tag).strImageLabelPart1, font, myBrush, rect, format1);

                Rectangle rect2 = new Rectangle(1, 40, 300, 115);
                e.Graphics.DrawString(((CardPictureInfo)sendingPictureBox.Tag).strImageLabelPart2, font, myBrush, rect2, format1);
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            PictureBox sendingPictureBox = (PictureBox)sender;

            //if (((CardPictureInfo)sendingPictureBox.Tag).bPopulated)
            if (sendingPictureBox.Image != null)
            {
                // Make a note of the removed image, in case it never gets assigned anywhere and we have to put it back again.
                removedImageName = sendingPictureBox.Image.Tag.ToString();
                emptiedPictureInfo = (CardPictureInfo)sendingPictureBox.Tag;
                emptiedPictureBoxName = sendingPictureBox.Name;

                // The card image must now be moved from this picture box to the drag destination.
                sendingPictureBox.DoDragDrop(removedImageName, DragDropEffects.Copy);
                CardPictureInfo tempInfo = new CardPictureInfo();
                tempInfo = ((CardPictureInfo)sendingPictureBox.Tag);
                tempInfo.bPopulated = false;
                sendingPictureBox.Tag = tempInfo;
                bool bTest = ((CardPictureInfo)sendingPictureBox.Tag).bPopulated;
                sendingPictureBox.Image = null;

                // Redisplay the new deck contents
                AdjustCardsInDeck((CardPictureInfo)sendingPictureBox.Tag);
            }
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            PictureBox sendingPictureBox = (PictureBox)sender;
            e.Effect = DragDropEffects.All;

            // This is only a valid drop location if it hasn't already got a card image.
            //if (!((CardPictureInfo)sendingPictureBox.Tag).bPopulated)
            if (sendingPictureBox.Image == null)
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            if (emptiedPictureInfo.bPopulated == true)
            {
                //RestoreAbandonedImage();
            }
        }

        private void OnPictureBoxDragDrop(object sender, DragEventArgs e)
        {
            PictureBox sendingPictureBox = (PictureBox)sender;

            // This is only a valid drop location if it hasn't already got a card image.
            //if (!((CardPictureInfo)sendingPictureBox.Tag).bPopulated)
            if (sendingPictureBox.Image == null)
            {
                if (e.Data.GetDataPresent(DataFormats.Text))
                {
                    string newImageName = (string)e.Data.GetData(DataFormats.Text);
                    ResourceManager resourceManager = Resource1.ResourceManager;
                    sendingPictureBox.Image = (Bitmap)resourceManager.GetObject(newImageName);
                    sendingPictureBox.Image.Tag = newImageName;
                    sendingPictureBox.Refresh();
                    CardPictureInfo tempInfo = new CardPictureInfo();
                    tempInfo = ((CardPictureInfo)sendingPictureBox.Tag);
                    tempInfo.bPopulated = true;
                    sendingPictureBox.Tag = tempInfo;
                    bool bTest = ((CardPictureInfo)sendingPictureBox.Tag).bPopulated;

                    // The image has found a home, so we don't need to keep hold of it any more.
                    removedImageName = "";
                    emptiedPictureInfo.bPopulated = false;
                    emptiedPictureBoxName = "";

                    // Redisplay the new deck contents
                    AdjustCardsInDeck((CardPictureInfo)sendingPictureBox.Tag);
                }
            }
            else
            {
                if (emptiedPictureInfo.bPopulated == true)
                {
                    //RestoreAbandonedImage();
                }
            }
        }

        private void SetImage(ref List<PictureBox> pictureBoxes, int iPBoxIndex, string imageName)
        {
            ResourceManager resourceManager = Resource1.ResourceManager;

            pictureBoxes.ElementAt(iPBoxIndex).Image = (Bitmap)resourceManager.GetObject(imageName);
            pictureBoxes.ElementAt(iPBoxIndex).Image.Tag = imageName;
            pictureBoxes.ElementAt(iPBoxIndex).Refresh();
            CardPictureInfo tempInfo = new CardPictureInfo();
            tempInfo = (CardPictureInfo)(pictureBoxes.ElementAt(iPBoxIndex).Tag);
            tempInfo.bPopulated = true;
            pictureBoxes.ElementAt(iPBoxIndex).Tag = tempInfo;
            bool bTest = ((CardPictureInfo)pictureBoxes.ElementAt(iPBoxIndex).Tag).bPopulated;
        }

        private void RestoreAbandonedImage()
        {
            switch (emptiedPictureInfo.whichDeck)
            {
                case CardPickerDeck.Player01:
                    {
                        SetImage(ref player01CardImages, emptiedPictureInfo.iImageIndex, removedImageName);
                    }
                    break;
                case CardPickerDeck.Player02:
                    {
                        SetImage(ref player02CardImages, emptiedPictureInfo.iImageIndex, removedImageName);
                    }
                    break;
                case CardPickerDeck.SourceDeck:
                    {
                        SetImage(ref deckCardImages, emptiedPictureInfo.iImageIndex, removedImageName);
                    }
                    break;
            }
            emptiedPictureInfo.bPopulated = false;
            removedImageName = "";
            emptiedPictureBoxName = "";
        }// end function
        #endregion
    }// end class
}// end namespace
