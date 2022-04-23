using System;
using System.Net;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Threading.Tasks;

using Downloader;
using YoutubeDLSharp.Metadata;

namespace Ripperoni
{
    public partial class Additional : UserControl
    {
        private readonly FormatData[] formats;
        private readonly string title;
        private readonly string epoch;
        private readonly Processor process;

        private readonly DownloadService download;

        private FormatData record;
        private string video;
        private string size;
        private string temp;

        public Additional(Processor p, FormatData[] f, string t, string e)
        {
            formats = f;
            title = t;
            epoch = e;
            process = p;

            p.done_primary = false;

            InitializeComponent();

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

            try
            {
                download.DownloadProgressChanged += DownloadProgression;
            }
            catch
            {
                Utilities.Error("Could not create progress event listener...", "Error", false);
            }

            GetMedia();
        }

        private void Additional_Load(object sender, EventArgs e)
        {
            Size = new System.Drawing.Size(400, 25);
        }

        #region Main Thread
        private async void GetMedia()
        {
            try
            {
                formats.ToList().FindAll(d => d.Extension == "m4a").ForEach(d =>
                {
                    record = d;
                });
            }
            catch
            {
                Utilities.Error("Could not find a viable or valid media record...", "Error", true);
            }

            video = record.Url;

            temp = Globals.Temp + "\\" + title + "." + epoch + ".m4a";

            size = FileSize(new Uri(video));

            Title.Text = "(Audio) " + title.Truncate(42);

            Json.Read();

            try
            {
                await download.DownloadFileTaskAsync(video, temp);
            }
            catch
            {
                Utilities.Error("Could not download required files (primary)...", "Error", true);
            }

            process.done_secondary = true;

            Done();
        }

        private void Done()
        {
            Task.Factory.StartNew(() => {
                bool ending = true;
                while (ending)
                {
                    if (process.done)
                    {
                        process.Remove(this);
                        ending = false;
                    }

                    System.Threading.Thread.Sleep(10);
                }
            });
        }
        #endregion

        #region Auxiliary
        private static string FileSize(Uri u)
        {
            try
            {
                var w = HttpWebRequest.Create(u);
                w.Method = "HEAD";

                using (var r = w.GetResponse())
                {
                    var f = r.Headers.Get("Content-Length");
                    var m = Math.Round(Convert.ToDouble(f) / 1024.0 / 1024.0, 2);
                    return m + " MB";
                }
            }
            catch
            {
                Utilities.Error("Could not compute the size of remote files...", "Error", false);
            }

            return 0 + " MB";
        }

        private void DownloadProgression(object sender, Downloader.DownloadProgressChangedEventArgs e)
        {
            try
            {
                Status.Invoke((MethodInvoker)delegate
                {
                    Status.Text = "[" + Math.Round(Convert.ToDouble(e.ReceivedBytesSize) / 1024.0 / 1024.0, 2) + " / " + size + "]:";
                });

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
    }
}
