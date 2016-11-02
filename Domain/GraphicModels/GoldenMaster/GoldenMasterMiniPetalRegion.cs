using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    public class GoldenMasterMiniPetalRegion : GoldenMasterRegion
    {
        public GoldenMasterMiniPetalRegion()
        {
            GraphicsPath = new GoldenMasterGraphicsPath();
            Type = "MiniPetal";
        }

        public GoldenMasterGraphicsPath GraphicsPath { get; set; }

        public override void Copy(TopGameGraphicsPath sourcePath)
        {
            Corners.Add(sourcePath.Lines[0].Start.ToGoldenMasterPoint());
            Corners.Add(sourcePath.Lines[0].End.ToGoldenMasterPoint());

            GraphicsPath.Copy(sourcePath);
        }
    }
}