using System.Collections.Generic;

namespace Domain.GraphicModels
{
    public class TopGamePointCollection
    {
        public TopGamePointCollection()
        {
            Points = new List<TopGamePoint>();
        }
        
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