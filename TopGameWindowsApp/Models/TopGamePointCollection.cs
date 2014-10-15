using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace TopGameWindowsApp.Models
{
    public class TopGamePointCollection
    {
        // Used by entity framework when inserting records for golden master
        public int TopGamePointCollectionId { get; set; }

        //// Used by entity framework to handle the one-to-one relationship
        //[ForeignKey("VitalStatistics")]
        //public int VitalStatisticsId { get; set; }
        //public virtual VitalStatistics VitalStatistics { get; set; }

        public TopGamePointCollection()
        {
            Points = new List<TopGamePoint>();
        }

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