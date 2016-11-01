using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    public class GoldenMasterRectangle
    {
        public GoldenMasterRectangle()
        {
            // Do nothing
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public void Copy(TopGameRectangle sourceRectangle)
        {
            Width = sourceRectangle.Width;
            Height = sourceRectangle.Height;
            X = sourceRectangle.X;
            Y = sourceRectangle.Y;
        }
    }
}