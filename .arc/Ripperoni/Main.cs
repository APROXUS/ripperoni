using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Dialogs;

using Downloader;
using Newtonsoft.Json;
using YoutubeDLSharp.Metadata;

namespace Ripperoni
{
    public partial class Main : Form
    {
        private DownloadService download;
        private Point mouselocation;
        private string updater;
        private bool update;

        public Main()
        {
            InitializeComponent();

            Json.Read();

            try
            {
                if (Directory.Exists(Globals.Temp))
                {
                    Directory.Delete(Globals.Temp, true);
                }

                Directory.CreateDirectory(Globals.Temp);
            }
            catch
            {
                Utilities.Error("Could not create a temperary directory...", "Error", true);
            }
        }
        
        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                string assembly =  $"{Assembly.GetExecutingAssembly().GetName().Version.Major}." +
                    $"{Assembly.GetExecutingAssembly().GetName().Version.Minor}." +
                    $"{Assembly.GetExecutingAssembly().GetName().Version.Build}." +
                    $"{Assembly.GetExecutingAssembly().GetName().Version.Revision:X}";
                Title.Text = $"APROX Ripperoni ({assembly})";

                Size = new Size(400, 670);

                Format.SelectedItem = ".MP4";
                Format_SelectedIndexChanged(sender, e);

                Input.Text = "https://youtu.be/dQw4w9WgXcQ";
                //Input.Text = "https://www.youtube.com/watch?v=tPEE9ZwTmy0";
                //Input.Text = "https://vimeo.com/660521773";
                //Input.Text = "https://vimeo.com/666109154";

                Output.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            }
            catch
            {
                Utilities.Error("Could not apply default properties...", "Error", true);
            }

            Task.Factory.StartNew(() => CheckForUpdates());
        }

        #region Update...
        private class Current
        {
            public int Major { get; set; }
            public int Minor { get; set; }
            public int Build { get; set; }
            public string Url { get; set; }
        }

        private void CheckForUpdates()
        {
            try
            {
                if (Utilities.Internet())
                {
                    string manifest = "https://cdn.aprox.us/app/ripperoni/current.json";

                    if (Utilities.Exists(manifest))
                    {
                        string json;
                        using (StreamReader sr = new StreamReader(WebRequest.Create(manifest).GetResponse().GetResponseStream()))
                        {
                            json = sr.ReadToEnd();
                        }

                        Current current = JsonConvert.DeserializeObject<Current>(json);

                        if (current.Build > Assembly.GetExecutingAssembly().GetName().Version.Build)
                        {
                            Title.Text = $"{Title.Text} > ({current.Major}.{current.Minor}.{current.Build})";

                            Update(current);
                        }
                    }
                }
            }
            catch
            {
                Utilities.Error("Could not check for updates...", "Error", false);
            }
        }

        private async void Update(Current current)
        {
            DownloadConfiguration DownloadOption = new DownloadConfiguration()
            {
                BufferBlockSize = Globals.Buffer,
                ChunkCount = Globals.Chunks,
                MaximumBytesPerSecond = Globals.Bytes,
                MaxTryAgainOnFailover = Globals.Tries,
                OnTheFlyDownload = Globals.OnFly,
                ParallelDownload = true,
                TempDirectory = Globals.Temp,
                Timeout = Globals.Timeout,
                RequestConfiguration =
                {
                    Accept = "*/*",
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    CookieContainer =  new CookieContainer(),
                    Headers = new WebHeaderCollection(),
                    KeepAlive = false,
                    ProtocolVersion = HttpVersion.Version11,
                    UseDefaultCredentials = false,
                    UserAgent = $"DownloaderSample/{Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}"
                }
            };

            download = new DownloadService(DownloadOption);

            download.DownloadProgressChanged += Progression;

            Directory.CreateDirectory(Path.GetTempPath() + "APROX TEMP");

            updater = Path.GetTempPath() + "APROX TEMP/Ripperoni.exe";

            try
            {
                await download.DownloadFileTaskAsync(current.Url, updater);
            }
            catch
            {
                Utilities.Error("Could not download required files (primary)...", "Error", true);
            }

            update = true;
        }

        private void Progression(object sender, Downloader.DownloadProgressChangedEventArgs e)
        {
            try
            {
                Progress.Invoke((MethodInvoker)delegate
                {
                    Progress.Style = ProgressBarStyle.Blocks;
                    Progress.Value = (Int32)e.ProgressPercentage;
                });
            }
            catch
            {
                Utilities.Error("Could not invoke UI controls on progress event...", "Error", false);
            }
        }
        #endregion

        #region Handle Bar...
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(Globals.Temp))
                {
                    Directory.Delete(Globals.Temp, true);
                }

                Directory.CreateDirectory(Globals.Temp);
            }
            catch
            {
                Utilities.Error("Could not create a temperary directory...", "Error", false);
            }

            if (update)
            {
                Process.Start(updater, "-silent");
            }

            ExitProcess();
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            MinimizeProcess();
        }

        private void HandleLayout_MouseDown(object sender, MouseEventArgs e)
        {
            mouselocation = e.Location;
        }

        private void HandleLayout_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - mouselocation.X;
                int dy = e.Location.Y - mouselocation.Y;
                Location = new Point(Location.X + dx, Location.Y + dy);
            }
        }

        private void Icon_MouseDown(object sender, MouseEventArgs e)
        {
            mouselocation = e.Location;
        }

        private void Icon_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - mouselocation.X;
                int dy = e.Location.Y - mouselocation.Y;
                Location = new Point(Location.X + dx, Location.Y + dy);
            }
        }

        private void Title_MouseDown(object sender, MouseEventArgs e)
        {
            mouselocation = e.Location;
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - mouselocation.X;
                int dy = e.Location.Y - mouselocation.Y;
                Location = new Point(Location.X + dx, Location.Y + dy);
            }
        }
        #endregion

        #region Upper UI...
        private void Format_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (Format.SelectedItem)
                {
                    case ".MP4":
                    case ".WebM":
                    case ".FLV":
                    case ".3GP":
                    case ".MOV":
                    case ".AVI":
                        Elements.Items.Clear();
                        Elements.Items.Add("Audio/Video");
                        Elements.Items.Add("Video Only");
                        Elements.SelectedItem = "Audio/Video";

                        Resolution.Items.Clear();
                        Resolution.Items.Add("4320p (8K)");
                        Resolution.Items.Add("2160p (4K)");
                        Resolution.Items.Add("1440p (QHD)");
                        Resolution.Items.Add("1080p (FHD)");
                        Resolution.Items.Add("720p (HD)");
                        Resolution.Items.Add("480p (SD)");
                        Resolution.Items.Add("360p");
                        Resolution.Items.Add("240p");
                        Resolution.Items.Add("144p");
                        Resolution.SelectedItem = "1080p (FHD)";
                        break;
                    default:
                        Elements.Items.Clear();
                        Elements.Items.Add("Audio Only");
                        Elements.SelectedItem = "Audio Only";

                        Resolution.Items.Clear();
                        Resolution.Items.Add("Highest");
                        Resolution.SelectedItem = "Highest";
                        break;
                }

                switch (Format.SelectedItem)
                {
                    case "-- Video --":
                        Format.SelectedItem = ".MP4";
                        break;
                    case "-- Audio --":
                        Format.SelectedItem = ".MP3";
                        break;
                }
            }
            catch
            {
                Utilities.Error("Could not update combo boxes...", "Error", false);
            }
        }
        #endregion

        #region Lower UI...
        private void Support_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://ytdl-org.github.io/youtube-dl/supportedsites.html");
            }
            catch
            {
                Utilities.Error("Could not open external website in browser...", "Error", false);
            }
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            try
            {
                Settings settings = new Settings();
                settings.ShowDialog();
            }
            catch
            {
                Utilities.Error("Could not open settings window...", "Error", false);
            }
        }

        private void Metadata_Click(object sender, EventArgs e)
        {
            try
            {
                Metadata metadata = new Metadata(Input.Text);
                metadata.ShowDialog();
            }
            catch
            {
                Utilities.Error("Could not open metadata window...", "Error", false);
            }
        }

        private void OpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog
                {
                    InitialDirectory = Output.Text,
                    IsFolderPicker = true
                };
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    Output.Text = dialog.FileName;
                }
            }
            catch
            {
                Utilities.Error("Could not open file explorer selector...", "Error", false);
            }
        }

        private void Convert_Click(object sender, EventArgs e)
        {
            if (Utilities.Internet())
            {
                if (Uri.IsWellFormedUriString(Input.Text, UriKind.Absolute))
                {
                    if (Input.Text.Split(':')[0].ToLower() == "http" || Input.Text.Split(':')[0].ToLower() == "https")
                    {
                        if (Path.IsPathRooted(Output.Text))
                        {
                            Directory.CreateDirectory(Output.Text);

                            Records.VerticalScroll.Visible = true;
                            Records.HorizontalScroll.Maximum = 0;
                            Records.AutoScroll = true;
                            Records.ResumeLayout();

                            Main m = this;

                            string input = Input.Text;
                            string output = Output.Text;
                            string format = (string)Format.SelectedItem;
                            string resolution = (string)Resolution.SelectedItem;
                            string elements = (string)Elements.SelectedItem;

                            Processor p = new Processor();
                            Task.Factory.StartNew(() => p.Process(m, input, output, format, resolution, elements));
                        }
                        else
                        {
                            Utilities.Error("[Output] Not a valid output path and must be rooted...", "Error", false);
                        }
                    }
                    else
                    {
                        Utilities.Error("[Input] Not a valid HTTP/HTTPS url...", "Error", false);
                    }
                }
                else
                {
                    Utilities.Error("[Input] Not a valid URL...", "Error", false);
                }
            }
            else
            {
                Utilities.Error("You are not currently connected to the internet...", "Internet Connectivity", false);
            }
        }
        #endregion

        #region Footer UI...
        private void FooterIcon_Click(object sender, EventArgs e)
        {
            try 
            {
                Process.Start("https://www.aprox.us/");
            }
            catch
            {
                Utilities.Error("Could not open external website in browser...", "Error", false);
            }
        }

        private void Website_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://www.aprox.us/service/ripperoni");
            }
            catch
            {
                Utilities.Error("Could not open external website in browser...", "Error", false);
            }
        }

        private void Repository_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://www.github.com/aproxus/ripperoni");
            }
            catch
            {
                Utilities.Error("Could not open external website in browser...", "Error", false);
            }
        }

        private void Folder_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(Output.Text);

                ProcessStartInfo info = new ProcessStartInfo
                {
                    Arguments = Output.Text,
                    FileName = "explorer.exe"
                };

                Process.Start(info);
            }
            catch
            {
                Utilities.Error("Could not open file explorer...", "Error", false);
            }
        }

        #region Hover Effect...
        private void Website_MouseHover(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f, FontStyle.Underline);
            Website.Font = font;
        }

        private void Website_MouseLeave(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f);
            Website.Font = font;
        }

        private void Repository_MouseHover(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f, FontStyle.Underline);
            Repository.Font = font;
        }

        private void Repository_MouseLeave(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f);
            Repository.Font = font;
        }

        private void Settings_MouseHover(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f, FontStyle.Underline);
            Folder.Font = font;
        }

        private void Settings_MouseLeave(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f);
            Folder.Font = font;
        }
        #endregion
        #endregion

        #region Auxiliary...
        private void MinimizeProcess()
        {
            try
            {
                WindowState = FormWindowState.Minimized;
            }
            catch
            {
                Utilities.Error("Could not minimize the window...", "Error", false);
            }
        }

        private void ExitProcess()
        {
            Close();
        }
        #endregion
    }

    public class Processor
    {
        private Main m;

        private string epoch_primary;
        private string epoch_secondary;
        private string epoch_tertiary;

        private string input;
        private string output;
        private string format;
        private string resolution;
        private string elements;

        public string title;
        public FormatData[] formats;
        public string real_format = "mp4";
        public string down_format = "mp4";

        public bool done_primary = false;
        public bool done_secondary = false;
        public bool done_tertiary = false;

        public void Process(Main t, string i, string o, string f, string r, string e)
        {
            try
            {
                m = t;

                input = i;
                output = o;
                format = f;
                resolution = r;
                elements = e;

                epoch_primary = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
                UseRecord();

                if (elements == "Audio/Video")
                {
                    epoch_secondary = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
                    UseAdditional();
                }

                if (elements == "Audio/Video" || real_format != down_format)
                {
                    epoch_tertiary = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
                    UseProcessing();
                }
            }
            catch
            {
                Utilities.Error("Could not set Processor variables...", "Error", true);
            }

            PostMedia();
        }

        private void UseRecord()
        {
            try
            {
                m.Records.Invoke((MethodInvoker)delegate {
                    m.Records.Controls.Add(new Record(this, format, resolution, elements, input, epoch_primary));
                });

                while (!done_primary)
                {
                    Thread.Sleep(100);
                }
            }
            catch
            {
                Utilities.Error("Could not invoke Record Controller...", "Error", true);
            }
        }

        private void UseAdditional()
        {
            try
            {
                m.Records.Invoke((MethodInvoker)delegate {
                    m.Records.Controls.Add(new Additional(this, formats, title, epoch_secondary));
                });

                while (!done_secondary)
                {
                    Thread.Sleep(100);
                }
            }
            catch
            {
                Utilities.Error("Could not invoke Additional Controller...", "Error", true);
            }
        }

        private void UseProcessing()
        {
            try
            {
                m.Records.Invoke((MethodInvoker)delegate
                {
                    m.Records.Controls.Add(new Processing(this, epoch_primary, epoch_secondary, real_format, down_format, title, epoch_tertiary));
                });

                while (!done_tertiary)
                {
                    Thread.Sleep(100);
                }
            }
            catch
            {
                Utilities.Error("Could not invoke Processing Controller...", "Error", true);
            }
        }

        private void PostMedia()
        {
            string source;
            string destination;

            try
            {
                if (elements == "Audio/Video" || real_format != down_format)
                {
                    source = Globals.Temp + "\\" + title + "." + epoch_tertiary + "." + real_format;
                }
                else
                {
                    source = Globals.Temp + "\\" + title + "." + epoch_primary + "." + real_format;
                }

                destination = output + "\\" + title + "." + real_format;

                File.Delete(destination);
                File.Move(source, destination);
            }
            catch
            {
                Utilities.Error("Could not transfer completed files...", "Error", false);
            }

            try
            {
                if (elements == "Audio/Video")
                {
                    File.Delete(Globals.Temp + "\\" + title + "." + epoch_secondary + ".m4a");
                    File.Delete(Globals.Temp + "\\" + title + "." + epoch_tertiary + "." + real_format);
                }

                if (real_format != down_format)
                {
                    File.Delete(Globals.Temp + "\\" + title + "." + epoch_tertiary + "." + real_format);
                }

                File.Delete(Globals.Temp + "\\" + title + "." + epoch_primary + "." + down_format);
            }
            catch
            {
                Utilities.Error("Could not delete temperary files...", "Error", false);
            }
        }
    }
}
