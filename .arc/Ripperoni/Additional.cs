using System;
using System.Net;
using System.Linq;
using System.Threading;
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

        private readonly DownloadService download;

        private FormatData record;
        private string video;
        private string size;
        private string temp;

        private bool downloading;
        private bool swapping;

        public Additional(FormatData[] f, string t, string e)
        {
            downloading = true;

            formats = f;
            title = t;
            epoch = e;

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

            download.DownloadProgressChanged += DownloadProgression;

            Task.Factory.StartNew(() => Swapping());

            GetMedia();
        }

        #region Main Thread
        private async void GetMedia() {

            formats.ToList().FindAll(d => d.Extension == "m4a").ForEach(d =>
            {
                record = d;
            });

            video = record.Url;

            temp = Globals.Temp + "\\" + title + "." + epoch + ".m4a";

            size = FileSize(new Uri(video));

            Title.Invoke((MethodInvoker)delegate {
                Title.Text = title + " (Audio)";
            });

            Json.Read();

            await download.DownloadFileTaskAsync(video, temp);

            downloading = false;
        }
        #endregion

        #region Auxiliary
        private static string FileSize(Uri u)
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

        private void DownloadProgression(object sender, Downloader.DownloadProgressChangedEventArgs e)
        {
            if (!swapping)
            {
                Status.Invoke((MethodInvoker)delegate
                {
                    Status.Text = "[" + Math.Round(Convert.ToDouble(e.ReceivedBytesSize) / 1024.0 / 1024.0, 2) + " / " + size + "]:";
                });
            }

            Progress.Invoke((MethodInvoker)delegate
            {
                Progress.Style = ProgressBarStyle.Blocks;
                Progress.Value = (Int32)e.ProgressPercentage;
            });
        }

        private void Swapping ()
        {
            while (downloading)
            {
                swapping = !swapping;

                if (swapping)
                {
                    Status.Invoke((MethodInvoker)delegate
                    {
                        Status.Text = "[Audio]:";
                    });
                }

                Thread.Sleep(1000);
            }

            Status.Invoke((MethodInvoker)delegate
            {
                Status.Text = "[Audio]:";
            });
        }
        #endregion
    }
}
