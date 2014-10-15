using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace TopGameWindowsApp.Models
{
    public class TopGameRectangle
    {
        // Used by entity framework when inserting records for golden master
        public int TopGameRectangleId { get; set; }

        //// Used by entity framework to handle the one-to-one relationship
        //[ForeignKey("VitalStatistics")]
        //public int VitalStatisticsId { get; set; }
        //public virtual VitalStatistics VitalStatistics { get; set; }

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
            get { return _actualRectangle.X; }
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