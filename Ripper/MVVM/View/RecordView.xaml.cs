using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

using Downloader;
using WebPWrapper;
using Javi.FFmpeg;
using YoutubeDLSharp;
using YoutubeDLSharp.Metadata;

namespace Ripper.MVVM.View
{
    public partial class RecordView : UserControl
    {
        #region Variables...
        private readonly CancellationTokenSource source;
        private CancellationToken token;

        private readonly string i;
        private readonly string o;
        private readonly string f;
        private readonly string e;
        private readonly bool l;
        private readonly int r;

        private FormatData[] fs;
        private FormatData re;
        private string ti;
        private string up;
        private string th;
        private string fr;
        private string vi;
        private string te;
        private float le;

        private string tm;
        private string tc;
        private string f1;
        private string f2;

        private DownloadService d;
        private bool downloading;
        private bool processing;
        private FFmpeg ffmpeg;
        private string p;
        private string s;
        #endregion

        public RecordView()
        {
            InitializeComponent();

            i = Globals.Input;
            o = Globals.Output;
            f = Globals.Format;
            r = Globals.Resolution;

            if (f != "mp4" && f != "webm" && f != "mov" && f != "avi" && f != "flv")
            {
                l = false;
            }
            else
            {
                l = true;
            }

            e = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            source = new CancellationTokenSource();
            token = source.Token;

            try
            {
                Task.Factory.StartNew(() => Processor(), token);
            }
            catch
            {
                Utilities.Error("Could not start worker process...", "Executable Error", "022", false);
            }
        }

        private void Remove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            source.Cancel();
        }

        #region Processor...
        private async void Processor()
        {
            #pragma warning disable CS4014 // This is the wanted functionality

            try
            {
                token.ThrowIfCancellationRequested();

                Dispatcher.Invoke(delegate ()
                {
                    Status.Text = "Starting...";
                });

                #region Systems and Events...
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
                    RequestConfiguration = {
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

                d = new DownloadService(DownloadOption);

                try
                {
                    d.DownloadProgressChanged += DownloadProgression;
                }
                catch
                {
                    Utilities.Error("Could not start a progress event listener...", "Error", "023", false);
                }

                ffmpeg = new FFmpeg(Globals.Real + @"FFmpeg.exe");

                try
                {
                    ffmpeg.OnProgress += ProcessProgression;
                }
                catch
                {
                    Utilities.Error("Could not start a progress event listener...", "Error", "024", false);
                }
                #endregion

                token.ThrowIfCancellationRequested();

                #region Get Metadata...
                try
                {
                    var y = new YoutubeDL
                    {
                        YoutubeDLPath = Globals.Real + "YTDLP.exe"
                    };

                    var r = await y.RunVideoDataFetch(i);
                    VideoData v = r.Data;
                    ti = v.Title;
                    up = v.Uploader;
                    le = v.Duration ?? default;
                    th = v.Thumbnail;
                    fs = v.Formats;
                }
                catch
                {
                    source.Cancel();

                    Utilities.Error($"Could not fetch data regarding the URL: {i} ...", "Worker Error", "025", false);
                }

                try
                {
                    if (!l)
                    {
                        if (fs.ToList().FindAll(a => a.Extension == f).Count < 1)
                        {
                            fs.ToList().FindAll(a => a.Extension == "m4a").ForEach(a =>
                            {
                                fr = "m4a";
                                re = a;
                            });
                        }
                        else
                        {
                            fs.ToList().FindAll(a => a.Extension == f).ForEach(a =>
                            {
                                fr = f;
                                re = a;
                            });
                        }
                    }
                    else
                    {
                        if (fs.ToList().FindAll(d => d.Extension == f).Count < 1)
                        {
                            List<int> l = new List<int>() { 0 };
                            l = fs.ToList().FindAll(a => a.Extension == "mp4").Select(a => a.Height ?? 0).ToList();
                            int scoped = l.Min(i => (Math.Abs(r - i), i)).i;

                            fs.ToList().FindAll(a => a.Height == scoped).ForEach(a =>
                            {
                                fr = "mp4";
                                re = a;
                            });
                        }
                        else
                        {
                            List<int> l = new List<int>() { 0 };
                            l = fs.ToList().FindAll(a => a.Extension == f).Select(a => a.Height ?? 0).ToList();
                            int scoped = l.Min(i => (Math.Abs(r - i), i)).i;

                            fs.ToList().FindAll(a => a.Height == scoped).ForEach(a =>
                            {
                                fr = f;
                                re = a;
                            });
                        }
                    }
                }
                catch
                {
                    source.Cancel();

                    Utilities.Error("Could not find a viable media record...", "Worker Error", "026", false);
                }

                vi = re.Url;
                #endregion

                token.ThrowIfCancellationRequested();

                #region Post Metadata...
                try
                {
                    BitmapImage bi = new BitmapImage();

                    if (th.Split('.').Last().ToString() == "webp")
                    {
                        Bitmap bm;

                        byte[] i = new WebClient().DownloadData(th);
                        using (WebP w = new WebP())
                        {
                            bm = w.Decode(i);
                        }

                        using (var m = new MemoryStream())
                        {
                            bm.Save(m, System.Drawing.Imaging.ImageFormat.Png);
                            m.Position = 0;

                            bi.BeginInit();
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.StreamSource = m;
                            bi.EndInit();
                        }
                    }
                    else
                    {
                        bi.BeginInit();
                        bi.UriSource = new Uri(th, UriKind.Absolute);
                        bi.EndInit();
                    }

                    Dispatcher.Invoke(delegate ()
                    {
                        Thumbnail.ImageSource = bi;
                    });
                }
                catch
                {
                    //Utilities.Error("Could not display thumbnail image...", "Worker Error", "027", false);
                }

                Dispatcher.Invoke(delegate ()
                {
                    Title.Text = ti;

                    Author.Text = up;

                    Length.Text = TimeSpan.FromSeconds(le).ToString(@"hh\:mm\:ss");
                });
                #endregion

                token.ThrowIfCancellationRequested();

                #region Get Base...
                te = Globals.Temp + "\\" + ti + "." + e + "." + fr;

                s = FileSize(new Uri(vi));

                Json.Read();

                downloading = true;

                Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        await d.DownloadFileTaskAsync(vi, te);
                    }
                    catch
                    {
                        source.Cancel();

                        Utilities.Error("Could not download base media files...", "Worker Error", "028", false);
                    }

                    downloading = false;
                });

                while (downloading)
                {
                    token.ThrowIfCancellationRequested();

                    Thread.Sleep(10);
                }
                #endregion

                token.ThrowIfCancellationRequested();

                if (l)
                {
                    #region Get Additional...
                    try
                    {
                        fs.ToList().FindAll(a => a.Extension == "m4a").ForEach(a =>
                        {
                            re = a;
                        });
                    }
                    catch
                    {
                        source.Cancel();

                        Utilities.Error("Could not find a viable media record...", "Worker Error", "029", false);
                    }

                    vi = re.Url;

                    te = Globals.Temp + "\\" + ti + "." + e + ".m4a";

                    s = FileSize(new Uri(vi));

                    Json.Read();

                    downloading = true;

                    Task.Factory.StartNew(async () =>
                    {
                        try
                        {
                            await d.DownloadFileTaskAsync(vi, te);
                        }
                        catch
                        {
                            source.Cancel();

                            Utilities.Error("Could not download addition media files...", "Worker Error", "030", false);
                        }

                        downloading = false;
                    });

                    while (downloading)
                    {
                        token.ThrowIfCancellationRequested();

                        Thread.Sleep(10);
                    }
                    #endregion

                    token.ThrowIfCancellationRequested();
                }

                if (fr != f || l)
                {
                    #region Get Processing...
                    p = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

                    tm = Globals.Temp + "\\" + ti + "." + p + "." + fr;
                    tc = Globals.Temp + "\\" + ti + "." + e + "." + f;

                    Dispatcher.Invoke(delegate ()
                    {
                        Status.Text = "Processing...";

                        Progress.IsIndeterminate = true;
                    });

                    Json.Read();

                    f1 = Globals.Temp + "\\" + ti + "." + e + "." + fr;

                    if (l)
                    {
                        f2 = Globals.Temp + "\\" + ti + "." + e + ".m4a";

                        Dispatcher.Invoke(delegate ()
                        {
                            Status.Text = "Multiplexing...";
                        });

                        processing = true;

                        Task.Factory.StartNew(() => GetMultiplex());

                        while (processing)
                        {
                            token.ThrowIfCancellationRequested();

                            Thread.Sleep(10);
                        }

                        f1 = tm;

                        token.ThrowIfCancellationRequested();
                    }

                    if (f != fr)
                    {
                        Dispatcher.Invoke(delegate ()
                        {
                            Status.Text = "Converting...";
                        });

                        processing = true;

                        Task.Factory.StartNew(() => GetConvert());

                        while (processing)
                        {
                            token.ThrowIfCancellationRequested();

                            Thread.Sleep(10);
                        }

                        token.ThrowIfCancellationRequested();
                    }
                    #endregion

                    token.ThrowIfCancellationRequested();
                }

                #region Completed...
                string so;
                string de;

                Dispatcher.Invoke(delegate ()
                {
                    Status.Text = "Completing...";
                });

                try
                {
                    if (l || f != fr)
                    {
                        so = tm;
                    }
                    else
                    {
                        so = tc;
                    }

                    de = o + "\\" + ti + "." + f;

                    File.Delete(de);
                    File.Move(so, de);
                }
                catch
                {
                    source.Cancel();

                    Utilities.Error("Could not transfer completed files...", "Storage Error", "031", false);
                }

                try
                {
                    File.Delete(Globals.Temp + "\\" + ti + "." + e + "." + fr);

                    if (l)
                    {
                        File.Delete(Globals.Temp + "\\" + ti + "." + e + ".m4a");
                        File.Delete(Globals.Temp + "\\" + ti + "." + p + "." + fr);
                    }

                    if (f != fr)
                    {
                        File.Delete(Globals.Temp + "\\" + ti + "." + e + "." + f);
                    }
                }
                catch
                {
                    Utilities.Error("Could not delete temperary files...", "Storage Error", "032", false);
                }

                Dispatcher.Invoke(delegate ()
                {
                    Status.Text = "Completed!";

                    Progress.IsIndeterminate = false;
                    Progress.Opacity = 0;
                    Progress.Value = 0;
                });
                #endregion
            }
            catch
            {
                if (downloading)
                {
                    d.CancelAsync();
                }
            }
        }
        #endregion

        #region FFmpeg Action...
        private void GetMultiplex()
        {
            try
            {
                string c;

                switch (f)
                {
                    case "webm":
                        c = string.Format($"-i \"{f1}\"  -i \"{f2}\" -c:v copy -c:a libvorbis \"{tm}\"");
                        break;
                    default:
                        c = string.Format($"-i \"{f1}\" -i \"{f2}\" -c:v copy -c:a aac \"{tm}\"");
                        break;
                }

                ffmpeg.Run(f1, tm, c, token);
            }
            catch
            {
                if (!token.IsCancellationRequested)
                {
                    source.Cancel();

                    Utilities.Error("Could not run multiplexer without error...", "Worker Error", "033", false);
                }
                
            }

            processing = false;
        }

        private void GetConvert()
        {
            try
            {
                string c;

                switch (f)
                {
                    case "webm":
                        c = string.Format($"-i \"{f1}\" -c:v vp9 -c:a libvorbis \"{tc}\"");
                        break;
                    case "flv":
                        c = string.Format($"-i \"{f1}\" -c:v libx264 -ar 22050 -crf 28 \"{tc}\"");
                        break;
                    case "mov":
                        c = string.Format($"-i \"{f1}\" -f mov \"{tc}\"");
                        break;
                    case "mp3":
                        c = string.Format($"-i \"{f1}\" -c:a libmp3lame \"{tc}\"");
                        break;
                    case "wav":
                        c = string.Format($"-i \"{f1}\" -c:a pcm_s16le \"{tc}\"");
                        break;
                    case "ogg":
                        c = string.Format($"-i \"{f1}\" -c:a libvorbis \"{tc}\"");
                        break;
                    case "pcm":
                        c = string.Format($"-i \"{f1}\" -c:a pcm_s16le -f s16le -ac 1 -ar 16000 \"{tc}\"");
                        break;
                    default:
                        c = string.Format($"-i \"{f1}\" -c:v copy -c:a copy \"{tc}\"");
                        break;
                }

                ffmpeg.Run(f1, tc, c, token);
            }
            catch
            {
                if (!token.IsCancellationRequested)
                {
                    source.Cancel();

                    Utilities.Error("Could not run converter without error...", "Worker Error", "034", false);
                }
            }

            processing = false;
        }
        #endregion

        #region Auxiliary...
        protected virtual bool FileLocked(FileInfo f)
        {
            try
            {
                using (FileStream s = f.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    s.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }

        private static string FileSize(Uri u)
        {
            try
            {
                var w = HttpWebRequest.Create(u);
                w.Method = "HEAD";

                using (var r = w.GetResponse())
                {
                    var f = r.Headers.Get("Content-Length");
                    var m = Math.Round(Convert.ToDouble(f) / 1024.0 / 1024.0, 0);
                    return m.ToString();
                }
            }
            catch
            {
                Utilities.Error("Could not make HEAD request for remote files...", "Network Error", "035", false);
            }

            return "0";
        }

        private void DownloadProgression(object sender, Downloader.DownloadProgressChangedEventArgs e)
        {
            try
            {
                Dispatcher.Invoke(delegate () {
                    Status.Text = Math.Round(Convert.ToDouble(e.ReceivedBytesSize) / 1024.0 / 1024.0, 0) + 
                    $"/{s}MB ({Math.Round(Convert.ToDouble(e.BytesPerSecondSpeed) / 1024.0 / 1024.0, 1)}MB/s)";

                    Progress.IsIndeterminate = false;
                    Progress.Value = (Int32)e.ProgressPercentage;
                });
            }
            catch
            {
                Utilities.Error("Could not invoke UI dispatcher on progress event...", "Worker Error", "036", false);
            }
        }

        private void ProcessProgression(object sender, FFmpegProgressEventArgs e)
        {
            try
            {
                Dispatcher.Invoke(delegate () {
                    Progress.IsIndeterminate = false;
                    Progress.Value = e.ProcessedDuration.TotalMilliseconds / e.TotalDuration.TotalMilliseconds * 100;
                });
            }
            catch
            {
                Utilities.Error("Could not invoke UI dispatcher on progress event...", "Worker Error", "037", false);
            }
        }
        #endregion
    }
}
