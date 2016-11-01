using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.GraphicModels.GoldenMaster
{
    public class GoldenMasterRegion
    {
        protected GoldenMasterRegion()
        {
            Corners = new List<GoldenMasterPoint>();
        }

        public string Type { get; set; }

        public IList<GoldenMasterPoint> Corners { get; set; }

        public virtual void Copy(TopGameGraphicsPath sourcePath)
        {
            foreach (var line in sourcePath.Lines)
            {
                Corners.Add(line.Start.ToGoldenMasterPoint());
            }
        }
    }
}