using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterMiniPetalRegion : GoldenMasterRegion
    {
        public GoldenMasterMiniPetalRegion()
        {
            GraphicsPath = new GoldenMasterGraphicsPath();
        }

        [JsonProperty]
        public GoldenMasterGraphicsPath GraphicsPath { get; set; }

        public virtual void Copy(TopGameGraphicsPath sourcePath)
        {
            base.Copy(sourcePath);
            GraphicsPath.Copy(sourcePath);
        }
    }
}