namespace Domain.GameModels
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
}