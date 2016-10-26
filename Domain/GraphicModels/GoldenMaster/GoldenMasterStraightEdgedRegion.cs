using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    // Straight-edged regions are only different from arc regions in the way they are used.
    // They have their own class so that we can differentiate between these and the arc regions.
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterStraightEdgedRegion : GoldenMasterRegion
    {
    }
}