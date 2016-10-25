using System.IO;
using Newtonsoft.Json;

namespace Domain
{
    public static class TopGameJsonWriter
    {
        public static void WriteToJsonFile(object objectToWrite, string fileNameAndPath)
        {
            var formattedJson = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented);

            //File.AppendAllText(@"c:\Temp\TopGame-GoldenMaster.json", formattedJson);
            File.WriteAllText(fileNameAndPath, formattedJson);
        }

        public static TObjectType ReadFromJsonFile<TObjectType>(string fileNameAndPath)
        {
            TObjectType result;

            using (StreamReader file = File.OpenText(fileNameAndPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                result = (TObjectType)serializer.Deserialize(file, typeof(TObjectType));
            }

            return result;
        }
    }
}