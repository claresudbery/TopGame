using System.Drawing.Drawing2D;

namespace Domain.GraphicModels
{
    public class ColouredRegion
    {
        public System.Drawing.Color TheColour { get; set; }
        //public Region TheRegion {get; set;}

        public ColouredRegion()
        {
            //TheRegion = new Region(sourcePath);
            TheColour = OnePlayerGraphicsLoop.GetConstantBackgroundColour();
        }

        public ColouredRegion(GraphicsPath sourcePath)
        {
            //TheRegion = new Region(sourcePath);
            TheColour = OnePlayerGraphicsLoop.GetConstantBackgroundColour();
        }

        public ColouredRegion(RegionData regionData)
        {
            //TheRegion = new Region(regionData);
            TheColour = OnePlayerGraphicsLoop.GetConstantBackgroundColour();
        }

        /*public void Dispose()
        {
            TheRegion.Dispose();
        }*/
    }
}