using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Newtonsoft.Json;

namespace TopGameWindowsApp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
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

        [JsonProperty]
        public int X
        {
            get { return _actualRectangle.X; }
            set { _actualRectangle.X = value; }
        }

        [JsonProperty]
        public int Y
        {
            get { return _actualRectangle.X; }
            set { _actualRectangle.Y = value; }
        }

        [JsonProperty]
        public int Width
        {
            get { return _actualRectangle.Width; }
            set { _actualRectangle.Width = value; }
        }

        [JsonProperty]
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