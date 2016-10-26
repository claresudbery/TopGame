using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterRegion
    {
        public GoldenMasterRegion()
        {
            Corners = new List<GoldenMasterPoint>();
        }

        [JsonProperty]
        public IList<GoldenMasterPoint> Corners { get; set; }

        public virtual void Copy(TopGameGraphicsPath sourcePath)
        {
            foreach (var line in sourcePath.Lines)
            {
                Corners.Add(line.Start.ToGoldenMasterPoint());
            }
        }
    }
}