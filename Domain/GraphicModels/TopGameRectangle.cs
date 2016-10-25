using System.Drawing;

namespace Domain.GraphicModels
{
    public class TopGameRectangle
    {
        public TopGameRectangle()
        {
            // Do nothing
        }

        public TopGameRectangle(int x, int y, int width, int height)
        {
            _actualRectangle.X = x;
            _actualRectangle.Y = y;
            _actualRectangle.Width = width;
            _actualRectangle.Height = height;
        }
        
        public int X
        {
            get { return _actualRectangle.X; }
            set { _actualRectangle.X = value; }
        }
        
        public int Y
        {
            get { return _actualRectangle.Y; }
            set { _actualRectangle.Y = value; }
        }
        
        public int Width
        {
            get { return _actualRectangle.Width; }
            set { _actualRectangle.Width = value; }
        }
        
        public int Height
        {
            get { return _actualRectangle.Height; }
            set { _actualRectangle.Height = value; }
        }
        
        public Rectangle Rectangle
        {
            get { return _actualRectangle; }
        }

        private Rectangle _actualRectangle = new Rectangle();

        public void Copy(TopGameRectangle sourceRectangle)
        {
            Width = sourceRectangle.Width;
            Height = sourceRectangle.Height;
            X = sourceRectangle.X;
            Y = sourceRectangle.Y;
        }
    }
}