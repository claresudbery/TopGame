using System.Collections.Generic;
using Domain.GraphicModels.GoldenMaster;

namespace UnitTests.Utils
{
    class GoldenMasterPointEqualityComparer : IEqualityComparer<GoldenMasterPoint>
    {
        public bool Equals(GoldenMasterPoint firstPoint, GoldenMasterPoint secondPoint)
        {
            return firstPoint.X == secondPoint.X 
                   && firstPoint.Y == secondPoint.Y;
        }

        public int GetHashCode(GoldenMasterPoint obj)
        {
            return 1;
        }
    }
}