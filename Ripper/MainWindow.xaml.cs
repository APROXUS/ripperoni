using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Diagnostics;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Media.Animation;

using Ripper.MVVM.View;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

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
        private void Input_Changed(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Globals.Input = Input.Text;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Utilities.Internet())
            {
                if (Uri.IsWellFormedUriString(Input.Text, UriKind.Absolute))
                {
                    if (Input.Text.Split(':')[0].ToLower() == "http" || Input.Text.Split(':')[0].ToLower() == "https")
                    {
                        if (File.ReadAllText("Domains.txt").Contains(Input.Text.Split('/')[2].ToLower()))
                        {
                            if (Path.IsPathRooted(Globals.Output))
                            {
                                Directory.CreateDirectory(Globals.Output);

                                Globals.Input = Input.Text;

                                var RecordView = new RecordView();

                                Records.Children.Add(RecordView);

                                RecordView.Remove.Click += (o, args) =>
                                {
                                    Records.Children.Remove(RecordView);
                                };
                            }
                            else
                            {
                                Utilities.Error("The output path must be rooted...", "Configuration Error", "002", false, null);
                            }
                        }
                        else
                        {
                            Utilities.Error("The input must be a valid YouTube URL...", "Configuration Error", "003", false, null);
                        }
                    }
                    else
                    {
                        Utilities.Error("The input must be a valid HTTP(S) URL...", "Configuration Error", "004", false, null);
                    }
                }
                else
                {
                    Utilities.Error("The input must be a valid URL...", "Configuration Error", "005", false, null);
                }
            }
            else
            {
                Utilities.Error("You must have an internet connection...", "Internet Connectivity", "006", false, null);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (Utilities.Internet())
            {
                try
                {
                    DotNetEnv.Env.LoadContents(new WebClient().DownloadString("https://cdn.kpnc.io/app/ripperoni/.env"));
                }
                catch (Exception ex)
                {
                    Utilities.Error("Could not retrieve an environment variable from CDN...", "Network Error", "007", false, ex);
                }

                try
                {
                    var youtube = new YouTubeService(new BaseClientService.Initializer()
                    {
                        ApiKey = Environment.GetEnvironmentVariable("YOUTUBE"),
                        ApplicationName = GetType().ToString()
                    });

                    var request = youtube.Search.List("snippet");
                    request.Q = Input.Text;
                    request.MaxResults = 10;

                    List<string[]> videos = new List<string[]>();

                    foreach (var result in request.Execute().Items)
                    {
                        string thumbnail = "https://cdn.kpnc.io/img/ripperoni/unknown.jpg";

                        if (result.Snippet.Thumbnails.Standard != null)
                            thumbnail = result.Snippet.Thumbnails.Standard.Url;
                        if (result.Snippet.Thumbnails.Medium != null)
                            thumbnail = result.Snippet.Thumbnails.Medium.Url;
                        if (result.Snippet.Thumbnails.High != null)
                            thumbnail = result.Snippet.Thumbnails.High.Url;
                        if (result.Snippet.Thumbnails.Maxres != null)
                            thumbnail = result.Snippet.Thumbnails.Maxres.Url;

                        switch (result.Id.Kind)
                        {
                            case "youtube#video":
                                videos.Add(new string[] { result.Id.VideoId, thumbnail, result.Snippet.Title, result.Snippet.Description, result.Snippet.ChannelTitle });
                                break;

                            default:
                                break;
                        }
                    }

                    SearchWindow sw = new SearchWindow(WebUtility.UrlEncode(Input.Text), videos);
                    sw.ShowDialog();
                }
                catch (Exception ex)
                {
                    Utilities.Error("Could not search YouTube through API...", "Network Error", "008", false, ex);
                }
            }
            else
            {
                Utilities.Error("You must have an internet connection...", "Internet Connectivity", "009", false, null);
            }
        }

        public void Request(string v)
        {
            if (Path.IsPathRooted(Globals.Output))
            {
                Directory.CreateDirectory(Globals.Output);

                Globals.Input = v;

                var RecordView = new RecordView();

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
            Process.Start("https://www.github.com/kpncio/ripperoni");
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
                foreach (var p in Process.GetProcessesByName("FFmpeg.exe"))
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

            var a = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
            a.Completed += (s, _) => Close();
            BeginAnimation(OpacityProperty, a);
        }
        #endregion
    }
}
