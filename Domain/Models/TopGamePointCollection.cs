using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TopGamePointCollection
    {
        public TopGamePointCollection()
        {
            Points = new List<TopGamePoint>();
        }

        [JsonProperty]
        public IList<TopGamePoint> Points { get; set; }

        public void Copy(TopGamePointCollection sourcePointCollection)
        {
            Points.Clear();
            foreach (var point in sourcePointCollection.Points)
            {
                Points.Add(point);
            }
        }
    }
}