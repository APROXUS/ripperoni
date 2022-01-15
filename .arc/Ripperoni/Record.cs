using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

using WebPWrapper;

using YoutubeDLSharp;
using YoutubeDLSharp.Metadata;

namespace Ripperoni
{
    public partial class Record : UserControl
    {
        public Record(string fo, string re, string el, string i, string o)
        {
            InitializeComponent();

            Task.Factory.StartNew(() => GetMeta(fo, re, el, i, o));
        }

        private async void GetMeta(string fo, string re, string el, string i, string o)
        {
            var youtube = new YoutubeDL
            {
                YoutubeDLPath = "ytdlp.exe",
                FFmpegPath = "ffmpeg.exe"
            };

            var res = await youtube.RunVideoDataFetch(i);
            VideoData video = res.Data;
            string title = video.Title;
            string uploader = video.Uploader;
            DateTime date = video.UploadDate ?? default;
            float length = video.Duration ?? default;
            string thumbnail = video.Thumbnail;
            FormatData[] videos = video.Formats;

            FormatData vrec = default;
            string cfor = "mp4";
            string cres = "1080p";
            bool hres = false;

            switch (fo)
            {
                case ".MP4":
                    cfor = "mp4";
                    break;
                case ".WebM":
                    cfor = "webm";
                    break;
                case ".FLV":
                    cfor = "flv";
                    break;
                case ".3GP":
                    cfor = "3gp";
                    break;
                case ".MOV":
                    cfor = "mov";
                    break;
                case ".AVI":
                    cfor = "avi";
                    break;
                case ".MP3":
                    cfor = "mp3";
                    break;
                case ".WAV":
                    cfor = "wav";
                    break;
                case ".AAC":
                    cfor = "aac";
                    break;
                case ".OGG":
                    cfor = "ogg";
                    break;
                case ".M4A":
                    cfor = "m4a";
                    break;
                case ".PCM":
                    cfor = "pcm";
                    break;
                default:
                    cfor = "mp4";
                    break;
            }

            switch (re)
            {
                case "4320p (8K)":
                    cres = "4320p";
                    break;
                case "2160p (4K)":
                    cres = "2160p";
                    break;
                case "1440p (QHD)":
                    cres = "1440p";
                    break;
                case "1080p (FHD)":
                    cres = "1080p";
                    break;
                case "720p (HD)":
                    cres = "720p";
                    break;
                case "480p (SD)":
                    cres = "480p";
                    break;
                case "360p":
                    cres = "360p";
                    break;
                case "240p":
                    cres = "240p";
                    break;
                case "144p":
                    cres = "144p";
                    break;
                default:
                    cres = "1080p";
                    break;
            }

            if (videos.ToList().FindAll(v => v.Extension == cfor).Count < 1)
            {
                if (el == "Audio Only")
                {
                    videos.ToList().FindAll(v => v.Extension == "m4a").ForEach(v =>
                    {
                        if (!hres)
                        {
                            vrec = v;

                            if (v.FormatNote == cres)
                            {
                                hres = true;
                            }
                        }
                    });
                }
                else
                {
                    videos.ToList().FindAll(v => v.Extension == "mp4").ForEach(v =>
                    {
                        if (!hres)
                        {
                            vrec = v;

                            if (v.FormatNote == cres)
                            {
                                hres = true;
                            }
                        }
                    });
                }
            } 
            else
            {
                videos.ToList().FindAll(v => v.Extension == cfor).ForEach(v =>
                {
                    if (!hres)
                    {
                        vrec = v;

                        if (v.FormatNote == cres)
                        {
                            hres = true;
                        }
                    }
                });
            }

            PostMeta(
                vrec.Url,
                thumbnail,
                title,
                uploader,
                TimeSpan.FromSeconds(length).ToString(@"hh\:mm\:ss"),
                date.ToString("MM/dd/yyyy"),
                cfor, el, o
            );
        }

        private void PostMeta(string vi, string th, string ti, string au, string le, string da, string fo, string el, string o)
        {
            //string epoch = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            //Directory.CreateDirectory(Path.GetTempPath() + "APROX Ripperoni");

            if (th.Split('.').Last().ToString() == "webp")
            {
                Thumbnail.Invoke((MethodInvoker)delegate {
                    byte[] image = new WebClient().DownloadData(th);
                    using (WebP webp = new WebP())
                        Thumbnail.Image = webp.Decode(image);
                });
            }
            else
            {
                byte[] image = new WebClient().DownloadData(th);

                MemoryStream stream = new MemoryStream();
                byte[] data = image;
                stream.Write(data, 0, Convert.ToInt32(data.Length));
                Bitmap jpeg = new Bitmap(stream, false);
                stream.Dispose();

                Thumbnail.Image = jpeg;
            }

            Title.Invoke((MethodInvoker)delegate {
                 Title.Text = ti;
            });

            Author.Invoke((MethodInvoker)delegate {
                 Author.Text = au;
            });

            Length.Invoke((MethodInvoker)delegate {
                 Length.Text = le;
            });

            Date.Invoke((MethodInvoker)delegate {
                 Date.Text = da;
            });

            Fetch(ti, fo, el, vi, o);
        }

        private async void Fetch(string ti, string fo, string el, string vi, string o)
        {
            Title.Invoke((MethodInvoker)delegate {
                Title.Text = ti + " (" + FileSize(new Uri(vi)) + ")";
            });

            Global.Downloader.DownloadProgressChanged += DownloadProgression;

            await Global.Downloader.DownloadFileTaskAsync(vi, o + "\\" + ti + "." + fo);

            //WebClient web = new WebClient();
            //web.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgression);
            //web.DownloadFileAsync(new Uri(vi), o + "\\" + ti + "." + fo);

            // First, download the video with an incredibly fast utility:

            // The best alternative is JDownloader, which is free.
            // Other great apps like Multithreaded Download Manager
            // are DownThemAll (Free, Open Source), Free Download Manage (Free),
            // Internet Download Manager (Paid) and aria2 (Free, Open Source).

            // Second, convert the downloaded file to the requested format:

            //7 Great Open Source Converter Software on Windows & Mac – Free Download
            //Rank  Software Name               Supported OS     Offline Version
            //1.    Handbrake                   Windows / Mac   Full Version
            //2.    TEncoder Video Converter    Windows         Free Version
            //3.    FFmpeg                      Windows / Mac   Free Version
            //4     TalkHelper Video Converter  Windows/ Mac    Full Version
        }

        private void DownloadProgression(object sender, Downloader.DownloadProgressChangedEventArgs e)
        {

            string asdf = sender.ToString();

            Progress.Invoke((MethodInvoker)delegate
            {
                Progress.Style = ProgressBarStyle.Blocks;
                Progress.Value = (Int32)e.ProgressPercentage;
            });
        }

        protected virtual bool FileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }

        private static string FileSize(Uri uriPath)
        {
            var webRequest = HttpWebRequest.Create(uriPath);
            webRequest.Method = "HEAD";

            using (var webResponse = webRequest.GetResponse())
            {
                var fileSize = webResponse.Headers.Get("Content-Length");
                var fileSizeInMegaByte = Math.Round(Convert.ToDouble(fileSize) / 1024.0 / 1024.0, 2);
                return fileSizeInMegaByte + " MB";
            }
        }

        //private async void Fetch(string fo, string re, string el, string i, string o)
        //{
        //    var youtube = new YoutubeDL
        //    {
        //        YoutubeDLPath = "ytdlp.exe",
        //        FFmpegPath = "ffmpeg.exe",
        //        OutputFolder = o
        //    };

        //    if (el == "Video Only")
        //    {
        //        await youtube.RunVideoDownload(i, recodeFormat: VideoRecodeFormat.Mp4);
        //    }
        //    else if (el == "Audio Only")
        //    {
        //        await youtube.RunAudioDownload(i, AudioConversionFormat.Mp3);
        //    }
        //    else
        //    {
        //        await youtube.RunVideoDownload(i, "bestvideo+bestaudio/best", DownloadMergeFormat.Unspecified, VideoRecodeFormat.Mp4);
        //    }

        //    // // a progress handler with a callback that updates a progress bar
        //    // var progress = new Progress<DownloadProgress>(p => progressBar.Value = p.Progress);
        //    // // a cancellation token source used for cancelling the download
        //    // // use `cts.Cancel();` to perform cancellation
        //    // var cts = new CancellationTokenSource();
        //    // // ...
        //    // await ytdl.RunVideoDownload(Input.Text, progress: progress, ct: cts.Token);
        //}
    }
}
