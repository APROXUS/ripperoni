using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Threading.Tasks;

using Downloader;
using WebPWrapper;
using YoutubeDLSharp;
using YoutubeDLSharp.Metadata;

namespace Ripperoni
{
    public partial class Record : UserControl
    {
        private readonly string input;
        private readonly string output;

        private readonly string format;
        private readonly string resolution;
        private readonly string elements;

        private string title;
        private string uploader;
        private float length;
        private string thumbnail;
        private FormatData[] formats;

        private FormatData real_record = default;
        private string real_format = "mp4";
        private string real_resolution = "1080p";
        private bool has_resolution = false;

        private string video;
        private string temp;
        private string size;

        public Record(string f, string r, string e, string i, string o)
        {
            input = i;
            output = o;

            format = f;
            resolution = r;
            elements = e;

            InitializeComponent();

            Task.Factory.StartNew(() => GetMetadata());
        }

        #region Main Thread
        private async void GetMetadata()
        {
            var y = new YoutubeDL();
            y.YoutubeDLPath = "ytdlp.exe";

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
                    real_resolution = "4320p";
                    break;
                case "2160p (4K)":
                    real_resolution = "2160p";
                    break;
                case "1440p (QHD)":
                    real_resolution = "1440p";
                    break;
                case "1080p (FHD)":
                    real_resolution = "1080p";
                    break;
                case "720p (HD)":
                    real_resolution = "720p";
                    break;
                case "480p (SD)":
                    real_resolution = "480p";
                    break;
                case "360p":
                    real_resolution = "360p";
                    break;
                case "240p":
                    real_resolution = "240p";
                    break;
                case "144p":
                    real_resolution = "144p";
                    break;
                default:
                    real_resolution = "1080p";
                    break;
            }

            if (formats.ToList().FindAll(d => d.Extension == real_format).Count < 1)
            {
                if (elements == "Audio Only")
                {
                    formats.ToList().FindAll(d => d.Extension == "m4a").ForEach(d =>
                    {
                        if (!has_resolution)
                        {
                            real_record = d;

                            if (d.FormatNote == real_resolution)
                            {
                                has_resolution = true;
                            }
                        }
                    });
                }
                else
                {
                    formats.ToList().FindAll(d => d.Extension == "mp4").ForEach(d =>
                    {
                        if (!has_resolution)
                        {
                            real_record = d;

                            if (d.FormatNote == real_resolution)
                            {
                                has_resolution = true;
                            }
                        }
                    });
                }
            } 
            else
            {
                formats.ToList().FindAll(d => d.Extension == real_format).ForEach(d =>
                {
                    if (!has_resolution)
                    {
                        real_record = d;

                        if (d.FormatNote == real_resolution)
                        {
                            has_resolution = true;
                        }
                    }
                });
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
                 Title.Text = title;
            });

            Author.Invoke((MethodInvoker)delegate {
                 Author.Text = uploader;
            });

            Length.Invoke((MethodInvoker)delegate {
                 Length.Text = TimeSpan.FromSeconds(length).ToString(@"hh\:mm\:ss");
            });

            GetVideo();
        }

        private async void GetVideo()
        {
            temp = Globals.Temp + "\\" + title + "." + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() + "." + real_format;

            size = FileSize(new Uri(video));

            Title.Invoke((MethodInvoker)delegate {
                Title.Text = title + " (" + size + ")";
            });

            Json.Read();

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

            DownloadService Downloader = new DownloadService(DownloadOption);

            Downloader.DownloadProgressChanged += DownloadProgression;

            await Downloader.DownloadFileTaskAsync(video, temp);

            PostVideo();
        }

        private void PostVideo()
        {
            // Second, convert the downloaded file to the requested format:

            //7 Great Open Source Converter Software on Windows & Mac – Free Download
            //Rank  Software Name               Supported OS    Offline Version
            //1.    Handbrake                   Windows / Mac   Full Version
            //2.    TEncoder Video Converter    Windows         Free Version
            //3.    FFmpeg                      Windows / Mac   Free Version
            //4     TalkHelper Video Converter  Windows/ Mac    Full Version
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
