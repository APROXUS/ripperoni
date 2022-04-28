using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

using NETWORKLIST;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ripper
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

                    Globals.Output = (string)token["Output"];
                    Globals.Format = (string)token["Format"];
                    Globals.Resolution = (int)token["Resolution"];

                    Globals.Temp = (string)token["Temp"];
                    Globals.OnFly = (bool)token["OnFly"];
                    Globals.Bytes = (long)token["Bytes"];
                    Globals.Timeout = (int)token["Timeout"];
                    Globals.Chunks = (int)token["Chunks"];
                    Globals.Tries = (int)token["Tries"];
                    Globals.Buffer = (int)token["Buffer"];
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

                    w.WritePropertyName("Output");
                    w.WriteValue(Globals.Output);

                    w.WritePropertyName("Format");
                    w.WriteValue(Globals.Format);

                    w.WritePropertyName("Resolution");
                    w.WriteValue(Globals.Resolution);



                    w.WritePropertyName("Temp");
                    w.WriteValue(Globals.Temp);

                    w.WritePropertyName("OnFly");
                    w.WriteValue(Globals.OnFly);

                    w.WritePropertyName("Bytes");
                    w.WriteValue(Globals.Bytes);

                    w.WritePropertyName("Timeout");
                    w.WriteValue(Globals.Timeout);

                    w.WritePropertyName("Chunks");
                    w.WriteValue(Globals.Chunks);

                    w.WritePropertyName("Tries");
                    w.WriteValue(Globals.Tries);

                    w.WritePropertyName("Buffer");
                    w.WriteValue(Globals.Buffer);

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
        public static MainWindow Main { get; set; }

        public static string Real = AppDomain.CurrentDomain.BaseDirectory;

        public static string Input { get; set; } = "";
        public static string Output { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        public static string Format { get; set; } = "mp4";
        public static int Resolution { get; set; } = 1080;

        public static string Temp { get; set; } = Path.GetTempPath() + "Ripperoni";
        public static bool OnFly { get; set; } = false;
        public static long Bytes { get; set; } = 0;
        public static int Timeout { get; set; } = 1000;
        public static int Chunks { get; set; } = 8;
        public static int Tries { get; set; } = 10;
        public static int Buffer { get; set; } = 10240;
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

        public static void Error(string m, string t, bool f)
        {
            try
            {
                ErrorWindow er = new ErrorWindow(m, t, f);
                er.ShowDialog();

                if (f) Globals.Main.Close();
            }
            catch
            {
                try
                {
                    MessageBoxIcon i;

                    if (f) i = MessageBoxIcon.Error;
                    else i = MessageBoxIcon.Warning;

                    MessageBox.Show(m, t, MessageBoxButtons.OK, i);

                    if (f) Globals.Main.Close();
                }
                catch
                {
                    //You're seriously fucked!
                }
            }
        }
    }
}
