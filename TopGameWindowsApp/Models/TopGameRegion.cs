using System.Collections.Generic;

namespace TopGameWindowsApp.Models
{
    public class TopGameRegion
    {
        // Used by entity framework when inserting records for golden master
        public int TopGameRegionId { get; set; }
        //public virtual GoldenMasterSinglePass GoldenMasterSinglePass { get; set; }

        public TopGameRegion()
        {
            TopGamePoints = new List<TopGamePoint>();
        }

        public int GoldenMasterSinglePassId { get; set; }
        public virtual GoldenMasterSinglePass GoldenMasterSinglePass { get; set; }

        public virtual ICollection<TopGamePoint> TopGamePoints { get; set; }
    }
}