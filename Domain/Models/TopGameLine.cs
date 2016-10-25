using Domain.Models.GoldenMaster;

namespace Domain.Models
{
    public class TopGameLine
    {
        public TopGameLine()
        {
            // Do nothing
        }

        public TopGameLine(TopGamePoint start, TopGamePoint end)
        {
            Start = start;
            End = end;
        }

        public TopGamePoint Start { get; set; }

        public TopGamePoint End { get; set; }

        public GoldenMasterLine ToGoldenMasterLine()
        {
            return new GoldenMasterLine(Start, End);
        }
    }
}