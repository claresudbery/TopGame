using System.Drawing;
using Domain.Models.GoldenMaster;
using Newtonsoft.Json;

namespace Domain.Models
{
    public class TopGamePoint
    {
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

        public GoldenMasterPoint ToGoldenMasterPoint()
        {
            return new GoldenMasterPoint(X, Y);
        }
    }
}