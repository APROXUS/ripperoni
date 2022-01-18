using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

using Downloader;
using WebPWrapper;
using YoutubeDLSharp;
using YoutubeDLSharp.Metadata;

namespace Ripperoni
{
    public partial class Record : UserControl
    {
        private readonly DownloadService download;

        private readonly string input;
        private readonly string format;
        private readonly string resolution;
        private readonly string elements;
        private readonly string epoch;
        private readonly Processor process;

        public string title;
        private string uploader;
        private float length;
        private string thumbnail;
        public FormatData[] formats;

        private FormatData real_record = default;
        public string real_format = "mp4";
        private int real_resolution = 1920;
        public string down_format = "mp4";

        private string video;
        private string temp;
        private string size;

        public Record(Processor p, string f, string r, string e, string i, string m)
        {
            input = i;
            format = f;
            resolution = r;
            elements = e;
            epoch = m;
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

            download.DownloadProgressChanged += DownloadProgression;

            GetMetadata();
        }

        #region Main Thread
        private async void GetMetadata()
        {
            var y = new YoutubeDL();
            y.YoutubeDLPath = "DownloaderP.exe";

            var r = await y.RunVideoDataFetch(input);
            VideoData v = r.Data;
            title = v.Title;
            uploader = v.Uploader;
            length = v.Duration ?? default;
            thumbnail = v.Thumbnail;
            formats = v.Formats;

            switch (format)
            {
                case ".MP4":
                    real_format = "mp4";
                    break;
                case ".WebM":
                    real_format = "webm";
                    break;
                case ".FLV":
                    real_format = "flv";
                    break;
                case ".3GP":
                    real_format = "3gp";
                    break;
                case ".MOV":
                    real_format = "mov";
                    break;
                case ".AVI":
                    real_format = "avi";
                    break;
                case ".MP3":
                    real_format = "mp3";
                    break;
                case ".WAV":
                    real_format = "wav";
                    break;
                case ".AAC":
                    real_format = "aac";
                    break;
                case ".OGG":
                    real_format = "ogg";
                    break;
                case ".M4A":
                    real_format = "m4a";
                    break;
                case ".PCM":
                    real_format = "pcm";
                    break;
                default:
                    real_format = "mp4";
                    break;
            }

            switch (resolution)
            {
                case "4320p (8K)":
                    real_resolution = 4320;
                    break;
                case "2160p (4K)":
                    real_resolution = 2160;
                    break;
                case "1440p (QHD)":
                    real_resolution = 1440;
                    break;
                case "1080p (FHD)":
                    real_resolution = 1080;
                    break;
                case "720p (HD)":
                    real_resolution = 720;
                    break;
                case "480p (SD)":
                    real_resolution = 480;
                    break;
                case "360p":
                    real_resolution = 360;
                    break;
                case "240p":
                    real_resolution = 240;
                    break;
                case "144p":
                    real_resolution = 144;
                    break;
                default:
                    real_resolution = 1080;
                    break;
            }

            if (elements == "Audio Only")
            {
                if (formats.ToList().FindAll(d => d.Extension == real_format).Count < 1)
                {
                    formats.ToList().FindAll(d => d.Extension == "m4a").ForEach(d =>
                    {
                        down_format = "m4a";
                        real_record = d;
                    });
                }
                else
                {
                    formats.ToList().FindAll(d => d.Extension == real_format).ForEach(d =>
                    {
                        down_format = real_format;
                        real_record = d;
                    });
                }
            }
            else
            {
                if (formats.ToList().FindAll(d => d.Extension == real_format).Count < 1)
                {
                    List<int> l = new List<int>() { 0 };
                    l = formats.ToList().FindAll(d => d.Extension == "mp4").Select(d => d.Height ?? 0).ToList();
                    int scoped = l.Min(i => (Math.Abs(real_resolution - i), i)).i;

                    formats.ToList().FindAll(d => d.Height == scoped).ForEach(d =>
                    {
                        down_format = "mp4";
                        real_record = d;
                    });
                }
                else
                {
                    List<int> l = new List<int>() { 0 };
                    l = formats.ToList().FindAll(d => d.Extension == real_format).Select(d => d.Height ?? 0).ToList();
                    int scoped = l.Min(i => (Math.Abs(real_resolution - i), i)).i;

                    formats.ToList().FindAll(d => d.Height == scoped).ForEach(d =>
                    {
                        down_format = real_format;
                        real_record = d;
                    });
                }
            }

            video = real_record.Url;

            PostMetadata();
        }

        private void PostMetadata()
        {
            if (thumbnail.Split('.').Last().ToString() == "webp")
            {
                Thumbnail.Invoke((MethodInvoker)delegate {
                    byte[] i = new WebClient().DownloadData(thumbnail);
                    using (WebP w = new WebP())
                        Thumbnail.Image = w.Decode(i);
                });
            }
            else
            {
                byte[] i = new WebClient().DownloadData(thumbnail);

                MemoryStream s = new MemoryStream();
                byte[] d = i;
                s.Write(d, 0, Convert.ToInt32(d.Length));
                Bitmap j = new Bitmap(s, false);
                s.Dispose();

                Thumbnail.Image = j;
            }

            Title.Invoke((MethodInvoker)delegate {
                 Title.Text = title.Truncate(89);
            });

            Author.Invoke((MethodInvoker)delegate {
                 Author.Text = uploader.Truncate(15);
            });

            Length.Invoke((MethodInvoker)delegate {
                 Length.Text = TimeSpan.FromSeconds(length).ToString(@"hh\:mm\:ss");
            });

            GetMedia();
        }

        private async void GetMedia()
        {
            temp = Globals.Temp + "\\" + title + "." + epoch + "." + down_format;

            size = FileSize(new Uri(video));

            Title.Invoke((MethodInvoker)delegate {
                Title.Text = title + " (" + size + ")";
            });

            Json.Read();

            await download.DownloadFileTaskAsync(video, temp);

            process.title = title;
            process.formats = formats;
            process.real_format = real_format;
            process.down_format = down_format;

            process.done_primary = true;
        }
        #endregion

        #region Auxiliary
        protected virtual bool FileLocked(FileInfo f)
        {
            try
            {
                using (FileStream s = f.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    s.Close();
                }
            }
            catch (IOException) { return true; }

            return false;
        }

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
            Download.Invoke((MethodInvoker)delegate
            {
                Download.Text = Math.Round(Convert.ToDouble(e.ReceivedBytesSize) / 1024.0 / 1024.0, 2) + " / " + size;
            });

            Progress.Invoke((MethodInvoker)delegate
            {
                Progress.Style = ProgressBarStyle.Blocks;
                Progress.Value = (Int32)e.ProgressPercentage;
            });
        }
        #endregion
    }
}
