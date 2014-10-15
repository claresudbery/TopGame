using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsCard
{
    public class Card
    {
        public Suit mySuit {get;set;}
        public CardValue myCardValue {get;set;}
        public string imageUrl { get; set; }
        public string imageName { get; set; }
        public bool marked { get; set; }

        public enum Suit
        {
            None
            , Hearts
            , Clubs
            , Diamonds
            , Spades
        }

        public enum CardValue
        {
            Joker
            , Ace
            , Two
            , Three
            , Four
            , Five
            , Six
            , Seven
            , Eight
            , Nine
            , Ten
            , Jack
            , Queen
            , King
        }

        public enum CardColour
        {
            Black
            , Red
        }

        // I'm nobbling this on purpose to make sure I'm not using urls any more. The correct version is the commented-out one.
        public const string IMAGE_ROOT_PATH = "sploo";
        //public const string IMAGE_ROOT_PATH = "http://localhost/GeniusCardImages/cards_gif/";

        public const char HEARTS_PREFIX = 'h';
        public const char CLUBS_PREFIX = 'c';
        public const char DIAMONDS_PREFIX = 'd';
        public const char SPADES_PREFIX = 's';
        public const char JOKER_PREFIX = 'j';
        public const char RED_PREFIX = 'r';
        public const char BLACK_PREFIX = 'b';
        public const char JACK_PREFIX = 'j';
        public const char QUEEN_PREFIX = 'q';
        public const char KING_PREFIX = 'k';
        public const string IMAGE_FILE_EXTENSION = ".gif";

        public static readonly char[] CardValueChars = { '_', 'A', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'J', 'Q', 'K' };

        public Card()
        {
            mySuit = Suit.None;
            myCardValue = CardValue.Joker;
            imageUrl = "";
            imageName = "";
            marked = false;
        }

        // Any old random spud. So, eight of clubs will do.
        public void InitialiseAsSpud()
        {
            myCardValue = CardValue.Eight;
            mySuit = Suit.Clubs;
            imageUrl = GetImageUrl();
            imageName = GetImageName();
        }

        public void CopyCard(Card cardToCopy)
        {
            mySuit = cardToCopy.mySuit;
            myCardValue = cardToCopy.myCardValue;
            imageUrl = cardToCopy.imageUrl;
            imageName = cardToCopy.imageName;
            marked = cardToCopy.marked;
        }

        public bool FaceCard()        
        {
            bool bFaceCard = false;
            switch (myCardValue)
            {
                case CardValue.Jack: 
                case CardValue.Queen: 
                case CardValue.King: 
                case CardValue.Ace:
                    {
                        bFaceCard = true;
                    }
                    break;
            }
            return bFaceCard;
        }

        public int NumCardsToPay()
        {
            int numCardsToPay = 0;
            switch (myCardValue)
            {
                case CardValue.Jack:
                    {
                        numCardsToPay = 1;
                    }
                    break;
                case CardValue.Queen:
                    {
                        numCardsToPay = 2;
                    }
                    break;
                case CardValue.King:
                    {
                        numCardsToPay = 3;
                    }
                    break;
                case CardValue.Ace:
                    {
                        numCardsToPay = 4;
                    }
                    break;
            }
            return numCardsToPay;
        }

        public string GetImageUrl(CardColour? thisColour = null)
        {
            String newImageUrl = IMAGE_ROOT_PATH;

            newImageUrl = String.Concat(newImageUrl, GetImageName(thisColour));

            // The file extension
            newImageUrl = String.Concat(newImageUrl, IMAGE_FILE_EXTENSION);

            return newImageUrl;
        }

        public string GetImageName(CardColour? thisColour = null)
        {
            String newImageName = "";

            // The suit
            switch (mySuit)
            {
                case Suit.Hearts:
                    {
                        newImageName = String.Concat(newImageName, HEARTS_PREFIX);
                    }
                    break;

                case Suit.Clubs:
                    {
                        newImageName = String.Concat(newImageName, CLUBS_PREFIX);
                    }
                    break;

                case Suit.Diamonds:
                    {
                        newImageName = String.Concat(newImageName, DIAMONDS_PREFIX);
                    }
                    break;

                case Suit.Spades:
                    {
                        newImageName = String.Concat(newImageName, SPADES_PREFIX);
                    }
                    break;

                case Suit.None:
                    {
                        newImageName = String.Concat(newImageName, JOKER_PREFIX);
                        if (thisColour == CardColour.Red)
                        {
                            newImageName = String.Concat(newImageName, RED_PREFIX);
                        }
                        else
                        {
                            newImageName = String.Concat(newImageName, BLACK_PREFIX);
                        }
                    }
                    break;
            }

            // The number
            if (mySuit != Suit.None)
            {
                if ((int)myCardValue <= 10)
                {
                    newImageName = String.Concat(newImageName, ((int)myCardValue).ToString());
                }
                else
                {
                    switch (myCardValue)
                    {
                        case CardValue.Jack:
                            {
                                newImageName = String.Concat(newImageName, JACK_PREFIX);
                            }
                            break;

                        case CardValue.Queen:
                            {
                                newImageName = String.Concat(newImageName, QUEEN_PREFIX);
                            }
                            break;

                        case CardValue.King:
                            {
                                newImageName = String.Concat(newImageName, KING_PREFIX);
                            }
                            break;
                    }
                }
            }

            return newImageName;
        }// end function
    }// end class
}// end namespace
