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
                    Globals.Quality = (int)token["Quality"];
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

                    w.WritePropertyName("Quality");
                    w.WriteValue(Globals.Quality);

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

        public static string Temp { get; set; } = Path.GetTempPath() + "Ripperoni";
        public static bool OnFly { get; set; } = false;
        public static long Bytes { get; set; } = 0;
        public static int Timeout { get; set; } = 1000;
        public static int Chunks { get; set; } = 8;
        public static int Tries { get; set; } = 10;
        public static int Buffer { get; set; } = 10240;
        public static int Quality { get; set; } = 20;

        public static string Domains { get; } = "googlevideo.com www.gvt1.com www.video.google.com www.youtu.be www.youtube.ae www.youtube.al www.youtube.am www.youtube.at www.youtube.az www.youtube.ba www.youtube.be www.youtube.bg www.youtube.bh www.youtube.bo www.youtube.by www.youtube.ca www.youtube.cat www.youtube.ch www.youtube.cl www.youtube.co www.youtube.co.ae www.youtube.co.at www.youtube.co.cr www.youtube.co.hu www.youtube.co.id www.youtube.co.il www.youtube.co.in www.youtube.co.jp www.youtube.co.ke www.youtube.co.kr www.youtube.com www.youtube.co.ma www.youtube.com.ar www.youtube.com.au www.youtube.com.az www.youtube.com.bd www.youtube.com.bh www.youtube.com.bo www.youtube.com.br www.youtube.com.by www.youtube.com.co www.youtube.com.do www.youtube.com.ec www.youtube.com.ee www.youtube.com.eg www.youtube.com.es www.youtube.com.gh www.youtube.com.gr www.youtube.com.gt www.youtube.com.hk www.youtube.com.hn www.youtube.com.hr www.youtube.com.jm www.youtube.com.jo www.youtube.com.kw www.youtube.com.lb www.youtube.com.lv www.youtube.com.ly www.youtube.com.mk www.youtube.com.mt www.youtube.com.mx www.youtube.com.my www.youtube.com.ng www.youtube.com.ni www.youtube.com.om www.youtube.com.pa www.youtube.com.pe www.youtube.com.ph www.youtube.com.pk www.youtube.com.pt www.youtube.com.py www.youtube.com.qa www.youtube.com.ro www.youtube.com.sa www.youtube.com.sg www.youtube.com.sv www.youtube.com.tn www.youtube.com.tr www.youtube.com.tw www.youtube.com.ua www.youtube.com.uy www.youtube.com.ve www.youtube.co.nz www.youtube.co.th www.youtube.co.tz www.youtube.co.ug www.youtube.co.uk www.youtube.co.ve www.youtube.co.za www.youtube.co.zw www.youtube.cr www.youtube.cz www.youtube.de www.youtube.dk www.youtubeeducation.com www.youtube.ee www.youtubeembeddedplayer.googleapis.com www.youtube.es www.youtube.fi www.youtube.fr www.youtube.ge www.youtube.googleapis.com www.youtube.gr www.youtube.gt www.youtube.hk www.youtube.hr www.youtube.hu www.youtube.ie www.youtubei.googleapis.com www.youtube.in www.youtube.iq www.youtube.is www.youtube.it www.youtube.jo www.youtube.jp www.youtubekids.com www.youtube.kr www.youtube.kz www.youtube.la www.youtube.lk www.youtube.lt www.youtube.lu www.youtube.lv www.youtube.ly www.youtube.ma www.youtube.md www.youtube.me www.youtube.mk www.youtube.mn www.youtube.mx www.youtube.my www.youtube.ng www.youtube.ni www.youtube.nl www.youtube.no www.youtube-nocookie.com www.youtube.pa www.youtube.pe www.youtube.ph www.youtube.pk www.youtube.pl www.youtube.pr www.youtube.pt www.youtube.qa www.youtube.ro www.youtube.rs www.youtube.ru www.youtube.sa www.youtube.se www.youtube.sg www.youtube.si www.youtube.sk www.youtube.sn www.youtube.soy www.youtube.sv www.youtube.tn www.youtube.tv www.youtube.ua www.youtube.ug www.youtube-ui.l.google.com www.youtube.uy www.youtube.vn www.yt3.ggpht.com www.yt.be www.ytimg.com";
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
