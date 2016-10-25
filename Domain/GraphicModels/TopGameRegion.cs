using System.Collections.Generic;
using Domain.GraphicModels.GoldenMaster;

namespace Domain.GraphicModels
{
    public class TopGameRegion
    {
        public TopGameRegion()
        {
            TopGamePoints = new List<TopGamePoint>();
        }
        
        public IList<TopGamePoint> TopGamePoints { get; set; }

        public GoldenMasterRegion ToGoldenMasterRegion()
        {
            var goldenMasterRegion = new GoldenMasterRegion();
            foreach(var point in TopGamePoints)
            {
                goldenMasterRegion.TopGamePoints.Add(point.ToGoldenMasterPoint());
            }
            return goldenMasterRegion;
        }
    }
}