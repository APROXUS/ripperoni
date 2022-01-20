using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

using NETWORKLIST;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ripperoni
{
    public class Json
    {
        public static void Read()
        {
            try
            {
                if (File.Exists(Globals.Real + "Config.json"))
                {
                    string json = File.ReadAllText(Globals.Real + "Config.json");

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
            catch
            {
                Utilities.Error("Could not read configuration...", "Error", false);
            }
        }

        public static void Write()
        {
            try
            {
                StreamWriter sw = new StreamWriter(Globals.Real + "Config.json");
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
            catch
            {
                Utilities.Error("Could not write configuration...", "Error", false);
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
        public static string Real = AppDomain.CurrentDomain.BaseDirectory;
    }

    public static class Utilities
    {
        public static void ForceInternet()
        {
            while (!Internet())
            {
                Error("You must have an internet connection to continue. Press 'OK' when you are ready to proceed...", "Internet Connectivity", false);
            }
        }

        public static bool Internet()
        {
            return new NetworkListManager().IsConnectedToInternet;
        }

        public static bool Exists(string u)
        {
            bool r = true;

            WebRequest webRequest = WebRequest.Create(u);
            webRequest.Method = "HEAD";
            webRequest.Timeout = 5000;

            try
            {
                webRequest.GetResponse();
            }
            catch
            {
                r = false;
            }

            return r;
        }

        public static string Truncate(this string v, int l, string s = "…")
        {
            try
            {
                return v?.Length > l
                ? v.Substring(0, l) + s
                : v;
            }
            catch
            {
                Error("Could not truncate text...", "Error", false);

                return "Error...";
            }
        }

        public static void Error(string m, string t, bool f)
        {
            try
            {
                MessageBoxIcon i;

                if (f) i = MessageBoxIcon.Error;
                else i = MessageBoxIcon.Warning;

                MessageBox.Show(m, t, MessageBoxButtons.OK, i);

                if (f) Application.Exit();
            }
            catch
            {
                //You're seriously fucked!
            }
        }
    }
}
