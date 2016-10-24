using System.IO;
using Domain.Models;
using Newtonsoft.Json;

namespace Domain
{
    public static class TopGameJsonWriter
    {
        public static void WriteAllGoldenMastersToJsonFile(GoldenMasterList goldenMasterList)
        {
            var formattedJson = JsonConvert.SerializeObject(goldenMasterList, Formatting.Indented);

            //File.AppendAllText(@"c:\Temp\TopGame-GoldenMaster.json", formattedJson);
            File.WriteAllText(@"c:\Temp\TopGame-GoldenMaster.json", formattedJson);

            //foreach (var goldenMaster in goldenMasterList.GoldenMasters)
            //{
            //    WriteGoldenMasterToJsonFile(goldenMaster);
            //}
        }

        private static void WriteGoldenMasterToJsonFile(GoldenMasterSinglePass goldenMaster)
        {
            var formattedJson = JsonConvert.SerializeObject(goldenMaster, Formatting.Indented);

            //File.AppendAllText(@"c:\Temp\TopGame-GoldenMaster.json", formattedJson);
            File.WriteAllText(@"c:\Temp\TopGame-GoldenMaster.json", formattedJson);
        }
    }
}