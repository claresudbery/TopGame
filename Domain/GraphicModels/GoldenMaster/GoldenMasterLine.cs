using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    public class GoldenMasterLine
    {
        public GoldenMasterLine()
        {
        }

        public GoldenMasterLine(TopGamePoint start, TopGamePoint end)
        {
            Start = new GoldenMasterPoint(start.X, start.Y);
            End = new GoldenMasterPoint(end.X, end.Y);
        }

        public GoldenMasterPoint Start { get; set; }

        public GoldenMasterPoint End { get; set; }
    }
}