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

            Json.Read();

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
            catch
            {
                Utilities.Error("Could not create a temperary directory...", "Error", true);
            }
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
                                Utilities.Error("[Output] Not a valid output path and must be rooted...", "Error", false);
                            }
                        }
                        else
                        {
                            Utilities.Error("[Input] Not a valid YouTube url...", "Error", false);
                        }
                    }
                    else
                    {
                        Utilities.Error("[Input] Not a valid HTTP/HTTPS url...", "Error", false);
                    }
                }
                else
                {
                    Utilities.Error("[Input] Not a valid URL...", "Error", false);
                }
            }
            else
            {
                Utilities.Error("You are not currently connected to the internet...", "Internet Connectivity", false);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (Utilities.Internet())
            {
                // Request environment variables (api keys) from albienet

                DotNetEnv.Env.Load();

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
                    string thumbnail = "https://cdn.aprox.us/img/ripperoni/unknown.jpg";

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

                SearchWindow sw = new SearchWindow(this, WebUtility.UrlEncode(Input.Text), videos);
                sw.ShowDialog();
            }
            else
            {
                Utilities.Error("You are not currently connected to the internet...", "Internet Connectivity", false);
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
                Utilities.Error("[Output] Not a valid output path and must be rooted...", "Error", false);
            }
        }
        #endregion

        #region Footer UI...
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.aprox.us/");
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.aprox.us/services/ripper");
        }

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.github.com/aproxus/ripper");
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

        private void ExitProcess()
        {
            Json.Write();

            foreach (var p in Process.GetProcessesByName("FFmpeg.exe"))
            {
                p.Kill();
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
