using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Newtonsoft.Json;

namespace TopGameWindowsApp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TopGamePointCollection
    {
        public TopGamePointCollection()
        {
            Points = new List<TopGamePoint>();
        }

        [JsonProperty]
        public ICollection<TopGamePoint> Points { get; set; }

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