using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ripperoni
{
    public class Json
    {
        public static void Read()
        {
            if (File.Exists("Config.json"))
            {
                string json = File.ReadAllText("config.json");

                JObject token = JObject.Parse(json);
                Globals.Buffer = (int)token["Buffer"];
                Globals.Chunks = (int)token["Chunks"];
                Globals.Bytes = (long)token["Bytes"];
                Globals.Tries = (int)token["Tries"];
                Globals.OnFly = (bool)token["OnFly"];
                Globals.Timeout = (int)token["Timeout"];
                Globals.Temp = (string)token["Temp"];
            }
            else
            {
                Write();
                Read();
            }
        }

        public static void Write()
        {
            StreamWriter sw = new StreamWriter("Config.json");
            using (JsonWriter w = new JsonTextWriter(sw))
            {
                w.Formatting = Formatting.Indented;

                w.WriteStartObject();

                w.WritePropertyName("Buffer");
                w.WriteValue(Globals.Buffer);

                w.WritePropertyName("Chunks");
                w.WriteValue(Globals.Chunks);

                w.WritePropertyName("Bytes");
                w.WriteValue(Globals.Bytes);

                w.WritePropertyName("Tries");
                w.WriteValue(Globals.Tries);

                w.WritePropertyName("OnFly");
                w.WriteValue(Globals.OnFly);

                w.WritePropertyName("Timeout");
                w.WriteValue(Globals.Timeout);

                w.WritePropertyName("Temp");
                w.WriteValue(Globals.Temp);

                w.WriteEndObject();
            }
        }
    }

    public static class Globals
    {
        public static int Buffer { get; set; } = 10240;
        public static int Chunks { get; set; } = 8;
        public static long Bytes { get; set; } = 0;
        public static int Tries { get; set; } = 10;
        public static bool OnFly { get; set; } = false;
        public static int Timeout { get; set; } = 1000;
        public static string Temp { get; set; } = Path.GetTempPath() + "Ripperoni";
    }

    public static class Utilities
    {
        public static string Truncate(this string value, int maxLength, string truncationSuffix = "…")
        {
            return value?.Length > maxLength
                ? value.Substring(0, maxLength) + truncationSuffix
                : value;
        }
    }
}
