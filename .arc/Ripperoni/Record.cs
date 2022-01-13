using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

using WebPWrapper;

using YoutubeDLSharp;
using YoutubeDLSharp.Options;
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

            //VideoTitle.Text = title;
            //VideoUploader.Text = uploader;
            //VideoLength.Text = TimeSpan.FromSeconds(length).ToString(@"hh\:mm\:ss");
            //VideoDate.Text = date.ToString("MM/dd/yyyy");

            PostMeta(
                videos,
                thumbnail,
                title,
                uploader,
                TimeSpan.FromSeconds(length).ToString(@"hh\:mm\:ss"),
                date.ToString("MM/dd/yyyy"),
                fo, re, el, o
            );
        }

        private void PostMeta(FormatData[] vi, string th, string ti, string au, string le, string da, string fo, string re, string el, string o)
        {
            string epoch = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            FileInfo file = new FileInfo(Path.GetTempPath() + "APROX Ripperoni\\Thumbnail." + epoch + ".webp");

            Directory.CreateDirectory(Path.GetTempPath() + "APROX Ripperoni");

            DownloadFileAsync(th, epoch).GetAwaiter();

            while (FileLocked(file))
            {
                Thread.Sleep(100);
            }

            Thumbnail.Invoke((MethodInvoker)delegate {
                byte[] image = File.ReadAllBytes(Path.GetTempPath() + "APROX Ripperoni\\Thumbnail." + epoch + ".webp");
                using (WebP webp = new WebP())
                    Thumbnail.Image = webp.Decode(image);
            });

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

            Fetch(vi, fo, re, el, o);
        }

        private void Fetch(FormatData[] vi, string fo, string re, string el, string o)
        {
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

        private async Task DownloadFileAsync(string th, string epoch)
        {
            WebClient client = new WebClient();
            await client.DownloadFileTaskAsync(new Uri(th), Path.GetTempPath() + "APROX Ripperoni\\Thumbnail." + epoch + ".webp");
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
