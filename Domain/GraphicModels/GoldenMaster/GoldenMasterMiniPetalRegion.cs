using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterMiniPetalRegion : GoldenMasterRegion
    {
        public GoldenMasterMiniPetalRegion()
        {
            GraphicsPath = new GoldenMasterGraphicsPath();
            Type = "MiniPetal";
        }

        [JsonProperty]
        public GoldenMasterGraphicsPath GraphicsPath { get; set; }

        public virtual void Copy(TopGameGraphicsPath sourcePath)
        {
            Corners.Add(sourcePath.Lines[0].Start.ToGoldenMasterPoint());
            Corners.Add(sourcePath.Lines[0].End.ToGoldenMasterPoint());

            GraphicsPath.Copy(sourcePath);
        }
    }
}