using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterList
    {
        public GoldenMasterList()
        {
            GoldenMasters = new List<GoldenMasterSinglePass>();
        }

        [JsonProperty]
        public List<GoldenMasterSinglePass> GoldenMasters { get; set; }
    }
}