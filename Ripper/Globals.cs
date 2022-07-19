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
            // Logic to read Config.json with Newtonsoft.JSON

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
            catch (Exception ex)
            {
                Utilities.Error("Could not read local configuration...", "Configuration Error", "013", false, ex);
            }
        }

        public static void Write()
        {
            // Logic to write Config.json with Newtonsoft.JSON

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
            catch (Exception ex)
            {
                Utilities.Error("Could not write local configuration...", "Configuration Error", "014", false, ex);
            }
        }
    }

    public static class Globals
    {
        // Global variables that may be volatile...

        public static MainWindow Main { get; set; }

        public static string Real = AppDomain.CurrentDomain.BaseDirectory;
        
        public static string Output { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        public static string Format { get; set; } = "mp4";
        public static int Resolution { get; set; } = 1080;

        public static string Temp { get; set; } = Path.GetTempPath() + "Ripper";
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
            // Only allow users to continue with a valid internet connection...

            while (!Internet())
            {
                Error("You must have an internet connection to continue. Dismiss this modal when you are ready to proceed...", "Internet Connectivity", "015", false, null);
            }
        }

        public static bool Internet()
        {
            // Get internet connectivity status from Windows NETWORKLIST...

            return new NetworkListManager().IsConnectedToInternet;
        }

        public static bool Exists(string u)
        {
            // Check if an internet file exists...

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

        public static void Error(string m, string t, string n, bool f, Exception e)
        {
            string msg;

            if (e != null) msg = $"{m} \n\n {e}";
            else msg = m;

            try
            {
                // Error handler will try to show custom message box...

                ErrorWindow er = new ErrorWindow(msg, t, n, f);
                er.ShowDialog();

                if (f) Globals.Main.Close();
            }
            catch (Exception ex)
            {
                msg = $"{msg} \n\n {ex}";

                try
                {
                    // Error handler will then try to show a winforms message box...

                    MessageBoxIcon i;

                    if (f) i = MessageBoxIcon.Error;
                    else i = MessageBoxIcon.Warning;

                    MessageBox.Show(msg, $"{n}: {t}", MessageBoxButtons.OK, i);

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
