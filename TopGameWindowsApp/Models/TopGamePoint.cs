using System.Collections.Generic;
using System.Drawing;

namespace TopGameWindowsApp.Models
{
    public class TopGamePoint
    {
        // Used by entity framework when inserting records for golden master
        public int TopGamePointId { get; set; }

        // This is needed to signal to Entity Framework that there is a many-to-many relationship between TopGamePoint and TopGameRegion
        public ICollection<TopGameRegion> TopGameRegions { get; set; }

        // This is needed to signal to Entity Framework that there is a many-to-many relationship between TopGamePoint and TopGameGraphicsPath
        public ICollection<TopGameGraphicsPath> TopGameGraphicsPaths { get; set; }

        // This is needed to signal to Entity Framework that there is a many-to-many relationship between TopGamePoint and TopGamePointCollection
        public ICollection<TopGamePointCollection> TopGamePointCollections { get; set; }

        public TopGamePoint()
        {
            // Do nothing
        }

        public TopGamePoint(int x, int y)
        {
            _actualPoint.X = x;
            _actualPoint.Y = y;
        }

        public int X
        {
            get { return _actualPoint.X; }
            set { _actualPoint.X = value; }
        }

        public int Y
        {
            get { return _actualPoint.Y; }
            set { _actualPoint.Y = value; }
        }

        public Point Point
        {
            get { return _actualPoint; }
        }

        private Point _actualPoint = new Point();
    }
}