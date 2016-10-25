using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Models.GoldenMaster
{
    // Arc regions are only different from straight-edged regions in the way they are used - they are intersected with a curved petal region.
    // They have their own class so that we can differentiate between these and the straight-edged regions.
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterArcRegion
    {
        public GoldenMasterArcRegion()
        {
            Corners = new List<GoldenMasterPoint>();
        }

        [JsonProperty]
        public IList<GoldenMasterPoint> Corners { get; set; }

        public void Copy(TopGameGraphicsPath sourcePath)
        {
            foreach (var line in sourcePath.Lines)
            {
                Corners.Add(line.Start.ToGoldenMasterPoint());
            }
        }
    }
}