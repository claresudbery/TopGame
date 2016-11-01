using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    public class GoldenMasterPointCollection
    {
        public GoldenMasterPointCollection()
        {
            Points = new List<GoldenMasterPoint>();
        }

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