using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    // Arc regions are only different from straight-edged regions in the way they are used - they are intersected with a curved petal region.
    // They have their own class so that we can differentiate between these and the straight-edged regions.
    public class GoldenMasterArcRegion : GoldenMasterRegion
    {
        public GoldenMasterArcRegion()
        {
            Type = "Arc";
        }
    }
}