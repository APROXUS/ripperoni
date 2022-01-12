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
        string epoch;

        public Record(string th, string ti, string au, string le, string da, string fo, string re, string el, string i, string o)
        {
            InitializeComponent();

            epoch = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            FileInfo file = new FileInfo(Path.GetTempPath() + "APROX Ripperoni\\Thumbnail." + epoch + ".webp");

            Directory.CreateDirectory(Path.GetTempPath() + "APROX Ripperoni");

            DownloadFileAsync(th).GetAwaiter();

            while (FileLocked(file))
            {
                Thread.Sleep(100);
            }
            
            byte[] image = File.ReadAllBytes(Path.GetTempPath() + "APROX Ripperoni\\Thumbnail." + epoch + ".webp");
            using (WebP webp = new WebP())
                Thumbnail.Image = webp.Decode(image);

            Title.Text = ti;
            Author.Text = au;
            Length.Text = le;
            Date.Text = da;

            Fetch(fo, re, el, i, o);
        }

        private async Task DownloadFileAsync(string th)
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

        private async void Fetch(string fo, string re, string el, string i, string o)
        {
            var youtube = new YoutubeDL
            {
                YoutubeDLPath = "ytdlp.exe",
                FFmpegPath = "ffmpeg.exe",
                OutputFolder = o
            };

            if (el == "Video Only")
            {
                await youtube.RunVideoDownload(i, recodeFormat: VideoRecodeFormat.Mp4);
            }
            else if (el == "Audio Only")
            {
                await youtube.RunAudioDownload(i, AudioConversionFormat.Mp3);
            }
            else
            {
                await youtube.RunVideoDownload(i, "bestvideo+bestaudio/best", DownloadMergeFormat.Unspecified, VideoRecodeFormat.Mp4);
            }

            // // a progress handler with a callback that updates a progress bar
            // var progress = new Progress<DownloadProgress>(p => progressBar.Value = p.Progress);
            // // a cancellation token source used for cancelling the download
            // // use `cts.Cancel();` to perform cancellation
            // var cts = new CancellationTokenSource();
            // // ...
            // await ytdl.RunVideoDownload(Input.Text, progress: progress, ct: cts.Token);
        }
    }
}
