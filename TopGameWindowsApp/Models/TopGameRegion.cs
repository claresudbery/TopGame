using System.Collections.Generic;
using Newtonsoft.Json;

namespace TopGameWindowsApp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TopGameRegion
    {
        public TopGameRegion()
        {
            TopGamePoints = new List<TopGamePoint>();
        }

        [JsonProperty]
        public virtual ICollection<TopGamePoint> TopGamePoints { get; set; }
    }
}