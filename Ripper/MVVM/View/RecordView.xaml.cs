﻿using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Controls;
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
        private readonly string i;
        private readonly string o;
        private readonly string f;
        private readonly string e;
        private readonly bool l;
        private readonly int r;

        private string ti;
        private string up;
        private float le;
        private string th;
        private FormatData[] fs;
        private FormatData re;
        private string fr;
        private string vi;
        private string te;

        private string tm;
        private string tc;
        private string f1;
        private string f2;

        private DownloadService d;
        private bool done;
        private string s;

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

            Processor();
        }

        #region Processor...
        private void Processor()
        {
            Status.Text = "Starting...";

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

            d = new DownloadService(DownloadOption);

            try
            {
                d.DownloadProgressChanged += DownloadProgression;
            }
            catch
            {
                Utilities.Error("Could not create progress event listener...", "Error", false);
            }

            GetMetadata();
            WaitForAsync();

            PostMetadata();

            GetBase();
            WaitForAsync();

            if (l)
            {
                GetAdditional();
                WaitForAsync();
            }

            if (fr != f || l)
            {
                GetProcessing();
            }

            GetComplete();
        }
        #endregion

        #region Base Action...
        private async void GetMetadata()
        {
            try
            {
                var y = new YoutubeDL
                {
                    YoutubeDLPath = Globals.Real + "DownloaderP.exe"
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
                Utilities.Error("Could not fetch data regarding URL: " + i + " ...", "Error", true);
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
                Utilities.Error("Could not find a viable or valid media record...", "Error", true);
            }

            vi = re.Url;

            done = true;
        }

        private void PostMetadata()
        {
            try
            {
                Bitmap bm;

                if (th.Split('.').Last().ToString() == "webp")
                {
                    byte[] i = new WebClient().DownloadData(th);
                    using (WebP w = new WebP())
                    {
                        bm = w.Decode(i);
                    }
                }
                else
                {
                    byte[] i = new WebClient().DownloadData(th);

                    MemoryStream s = new MemoryStream();
                    byte[] d = i;
                    s.Write(d, 0, Convert.ToInt32(d.Length));
                    Bitmap j = new Bitmap(s, false);
                    s.Dispose();

                    bm = j;
                }

                Thumbnail.ImageSource = Imaging.CreateBitmapSourceFromHBitmap(
                    bm.GetHbitmap(),
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty,
                    BitmapSizeOptions.FromHeight(61)
                );
            }
            catch
            {
                Utilities.Error("Could not fetch and convert thumbnail image...", "Error", false);
            }

            try
            {
                Title.Text = ti.Truncate(89);

                Author.Text = up.Truncate(15);

                Length.Text = TimeSpan.FromSeconds(le).ToString(@"hh\:mm\:ss");
            }
            catch
            {
                Utilities.Error("Could not invoke UI controls...", "Error", false);
            }
        }

        private async void GetBase()
        {
            te = Globals.Temp + "\\" + ti + "." + e + "." + fr;

            s = FileSize(new Uri(vi));

            try
            {
                Title.Text = ti + " (" + s + ")";
            }
            catch
            {
                Utilities.Error("Could not invoke UI controls...", "Error", false);
            }

            Json.Read();

            try
            {
                await d.DownloadFileTaskAsync(vi, te);
            }
            catch
            {
                Utilities.Error("Could not download required files (primary)...", "Error", true);
            }

            done = true;
        }
        #endregion

        #region Additional Action...
        private async void GetAdditional()
        {
            try
            {
                fs.ToList().FindAll(a => a.Extension == "m4a").ForEach(a =>
                {
                    re = a;
                });
            }
            catch
            {
                Utilities.Error("Could not find a viable or valid media record...", "Error", true);
            }

            vi = re.Url;

            te = Globals.Temp + "\\" + ti + "." + e + ".m4a";

            s = FileSize(new Uri(vi));

            Title.Text = "(Audio) " + ti.Truncate(42);

            Json.Read();

            try
            {
                await d.DownloadFileTaskAsync(vi, te);
            }
            catch
            {
                Utilities.Error("Could not download required files (primary)...", "Error", true);
            }

            done = true;
        }
        #endregion

        #region Processing Action...
        private void GetProcessing()
        {
            tm = Globals.Temp + "\\" + ti + "." + e + "." + fr;
            tc = Globals.Temp + "\\" + ti + "." + e + "." + f;

            Title.Text = "(Processing) " + ti.Truncate(38);

            Json.Read();

            f1 = Globals.Temp + "\\" + ti + "." + e + "." + fr;

            if (l)
            {
                f2 = Globals.Temp + "\\" + ti + "." + e + ".m4a";

                GetMultiplex();

                f1 = tm;
            }

            if (f != fr)
            {
                GetConvert();
            }
        }

        private void GetMultiplex()
        {
            try
            {
                using (var ffmpeg = new FFmpeg(Globals.Real + @"FFmpeg.exe"))
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

                    ffmpeg.Run(f1, tm, c);
                }
            }
            catch
            {
                Utilities.Error("Could not run FFmpeg process without error (Multiplexer)...", "Error", true);
            }
        }

        private void GetConvert()
        {
            try
            {
                using (var ffmpeg = new FFmpeg(Globals.Real + @"FFmpeg.exe"))
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

                    ffmpeg.Run(f1, tc, c);
                }
            }
            catch
            {
                Utilities.Error("Could not run FFmpeg process without error (Converter)...", "Error", true);
            }
        }
        #endregion

        #region Completing Action...
        private void GetComplete()
        {
            string so;
            string de;

            try
            {
                if (l || f != fr)
                {
                    so = Globals.Temp + "\\" + ti + "." + e + "." + f;
                }
                else
                {
                    so = Globals.Temp + "\\" + ti + "." + e + "." + f;
                }

                de = o + "\\" + ti + "." + f;

                File.Delete(de);
                File.Move(so, de);
            }
            catch
            {
                Utilities.Error("Could not transfer completed files...", "Error", false);
            }

            try
            {
                if (l)
                {
                    File.Delete(Globals.Temp + "\\" + ti + "." + e + ".m4a");
                    File.Delete(Globals.Temp + "\\" + ti + "." + e + "." + f);
                }

                if (f != fr)
                {
                    File.Delete(Globals.Temp + "\\" + ti + "." + e + "." + f);
                }

                File.Delete(Globals.Temp + "\\" + ti + "." + e + "." + fr);
            }
            catch
            {
                Utilities.Error("Could not delete temperary files...", "Error", false);
            }
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
            catch (IOException) { return true; }

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
                Status.Text = Math.Round(Convert.ToDouble(e.ReceivedBytesSize) / 1024.0 / 1024.0, 2) + " / " + s;

                Progress.IsIndeterminate = false;
                Progress.Value = (Int32)e.ProgressPercentage;
            }
            catch
            {
                Utilities.Error("Could not invoke UI controls on progress event...", "Error", false);
            }
        }

        private void WaitForAsync()
        {
            while (!done)
            {
                Thread.Sleep(100);
            }

            done = false;
        }
        #endregion
    }
}