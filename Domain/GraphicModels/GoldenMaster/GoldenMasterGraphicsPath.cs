using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    public class GoldenMasterGraphicsPath
    {
        public GoldenMasterGraphicsPath()
        {
            Lines = new List<GoldenMasterLine>();
            ArcPaths = new List<GoldenMasterArcPath>();
        }

        public IList<GoldenMasterLine> Lines { get; set; }

        public IList<GoldenMasterArcPath> ArcPaths { get; set; }

        public void Copy(TopGameGraphicsPath sourcePath)
        {
            Lines.Clear();
            foreach (var line in sourcePath.Lines)
            {
                Lines.Add(line.ToGoldenMasterLine());
            }

            ArcPaths.Clear();
            foreach (var arcPath in sourcePath.ArcPaths)
            {
                ArcPaths.Add(arcPath.ToGoldenMasterArcPath());
            }
        }
    }
}