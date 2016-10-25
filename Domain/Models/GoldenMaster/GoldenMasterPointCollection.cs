using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Models.GoldenMaster
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GoldenMasterPointCollection
    {
        public GoldenMasterPointCollection()
        {
            Points = new List<GoldenMasterPoint>();
        }

        [JsonProperty]
        public IList<GoldenMasterPoint> Points { get; set; }

        public void Copy(TopGamePointCollection sourcePointCollection)
        {
            Points.Clear();
            foreach (var point in sourcePointCollection.Points)
            {
                Points.Add(point.ToGoldenMasterPoint());
            }
        }
    }
}