using System.Collections.Generic;

namespace TopGameWindowsApp.Models
{
    public class GoldenMasterSinglePass
    {
        public int GoldenMasterSinglePassId { get; set; }

        public int NumTotalSegments { get; set; }

        public GoldenMasterSinglePass()
        {
            TopGameRegions = new List<TopGameRegion>();
        }

        public int VitalStatisticsId { get; set; }
        public virtual VitalStatistics VitalStatistics { get; set; }

        public virtual ICollection<TopGameRegion> TopGameRegions { get; set; }
    }
}