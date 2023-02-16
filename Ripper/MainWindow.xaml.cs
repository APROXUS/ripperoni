using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Windows;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;

using WebPWrapper;
using YoutubeDLSharp;
using YouTubeApiSharp;
using Ripper.MVVM.View;
using YoutubeDLSharp.Metadata;

namespace Ripper
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Globals.Main = this;

            Json.Read();

            #region Create Temp...
            try
            {
                if (Directory.Exists(Globals.Temp))
                {
                    try
                    {
                        Directory.Delete(Globals.Temp, true);
                        Directory.CreateDirectory(Globals.Temp);
                    }
                    catch
                    {

                    }
                }
                else
                {
                    Directory.CreateDirectory(Globals.Temp);
                }
            }
            catch (Exception ex)
            {
                Utilities.Error("Could not create a temporary directory...", "Storage Error", "001", false, ex);
            }
            #endregion

            Spinner.Visibility = Visibility.Hidden;
        }

        #region Handle Bar UI...
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ExitProcess();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MinimizeProcess();
        }

        private void DragBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion

        #region Input UI...
        private void Input_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            e.Handled = true;

            Button_Click_2(sender, e);
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Spinner.Visibility = Visibility.Visible;

            // Input handler...
            if (Utilities.Internet())
            {
                if (Uri.IsWellFormedUriString(Input.Text, UriKind.Absolute))
                {
                    if (Input.Text.Split(':')[0].ToLower() == "http" || Input.Text.Split(':')[0].ToLower() == "https")
                    {
                        if (Globals.Domains.Contains(Input.Text.Split('/')[2].ToLower()))
                        {
                            try
                            {
                                // Setting up YouTube DLP service and getting first result...
                                var y = new YoutubeDL();
                                y.YoutubeDLPath = Globals.Real + @"Includes\yt-dlp.exe";

                                RunResult<VideoData> r = await y.RunVideoDataFetch(Input.Text);
                                VideoData v = r.Data;

                                Request(new VideoSearchComponents(v.Title, v.Uploader, "-", TimeSpan.FromSeconds(v.Duration ?? default).ToString(@"hh\:mm\:ss"), Input.Text, v.Thumbnail, "-"));
                            }
                            catch (Exception ex)
                            {
                                Utilities.Error("Could not get YouTube video through DLP...", "Network Error", "008", false, ex);
                            }
                        }
                        else
                        {
                            Utilities.Error("Could not get YouTube video through API...", "Network Error", "008", false, null);
                        }
                    }
                    else
                    {
                        Utilities.Error("Could not get YouTube video through API...", "Network Error", "008", false, null);
                    }
                }
                else
                {
                    try
                    {
                        // Setting up YouTube API search service and getting results...
                        List<VideoSearchComponents> videos = await new VideoSearch().GetVideos(Input.Text, 1);

                        SearchWindow sw = new SearchWindow(WebUtility.UrlEncode(Input.Text), videos) { Owner = this };
                        sw.ShowDialog();

                        Spinner.Visibility = Visibility.Hidden;
                    }
                    catch (Exception ex)
                    {
                        Utilities.Error("Could not open search...", "Network Error", "009", false, ex);
                    }
                }
            }
            else
            {
                Utilities.Error("You must have an internet connection...", "Internet Connectivity", "006", false, null);
            }

            Spinner.Visibility = Visibility.Hidden;
        }

        public void Request(VideoSearchComponents video)
        {
            // Selected video request handler...
            #region Image Processing...
            // Get online image in WPF compatible form...
            BitmapImage bitmapimage = new BitmapImage();
            try
            {
                if (video.getThumbnail().Split('.').Last().ToString() == "webp")
                {
                    Bitmap bitmap;

                    byte[] image = new WebClient().DownloadData(video.getThumbnail());
                    using (WebP webp = new WebP())
                    {
                        bitmap = webp.Decode(image);
                    }

                    using (MemoryStream stream = new MemoryStream())
                    {
                        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        stream.Position = 0;

                        bitmapimage.BeginInit();
                        bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapimage.StreamSource = stream;
                        bitmapimage.EndInit();
                    }
                }
                else
                {
                    bitmapimage.BeginInit();
                    bitmapimage.UriSource = new Uri(video.getThumbnail(), UriKind.Absolute);
                    bitmapimage.EndInit();
                }
            }
            catch (Exception ex)
            {
                Utilities.Error("Could not display thumbnail image...", "Executable Error", "038", false, ex);
            }
            #endregion

            Spinner.Visibility = Visibility.Hidden;

            if (Path.IsPathRooted(Globals.Output))
            {
                // Create video record handler/UI...
                Directory.CreateDirectory(Globals.Output);

                RecordView RecordView = new RecordView(video, bitmapimage);

                Records.Children.Add(RecordView);

                RecordView.Remove.Click += (o, args) =>
                {
                    Records.Children.Remove(RecordView);
                };
            }
            else
            {
                Utilities.Error("The output path must be rooted...", "Error", "010", false, null);
            }
        }
        #endregion

        #region Footer UI...
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.kpnc.io/");
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.kpnc.io/services/ripper");
        }

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.github.com/kpncio/ripper");
        }

        private void TextBlock_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            Directory.CreateDirectory(Globals.Output);

            Process.Start("explorer", Globals.Output);
        }
        #endregion

        #region Auxiliary...
        private void MinimizeProcess()
        {
            Json.Write();

            WindowState = WindowState.Minimized;
        }

        public void ExitProcess()
        {
            Json.Write();

            try
            {
                // Kill rogue FFmpeg executables...

                foreach (var p in Process.GetProcessesByName(@"Includes\ffmpeg.exe"))
                {
                    p.Kill();
                }
            }
            catch (Exception ex)
            {
                Utilities.Error("Could not terminate loose FFmpeg processors...", "Executable Error", "011", false, ex);
            }
            

            try
            {
                Directory.Delete(Globals.Temp, true);
            }
            catch
            {

            }

            // Fade out animation on close...
            var a = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
            a.Completed += (s, _) => Close();
            BeginAnimation(OpacityProperty, a);
        }
        #endregion
    }
}
