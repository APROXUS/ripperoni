using System;
using System.IO;
using System.Windows;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media.Animation;

using Ripper.MVVM.View;
using System.Threading;

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
                        if (Path.IsPathRooted(Globals.Output))
                        {
                            Directory.CreateDirectory(Globals.Output);

                            Globals.Input = Input.Text;

                            var RecordView = new RecordView();

                            Records.Children.Add(RecordView);

                            RecordView.Remove.Click += (o, args) =>
                            {
                                Thread.Sleep(1000);
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

            try
            {
                if (Directory.Exists(Globals.Temp))
                {
                    Directory.Delete(Globals.Temp, true);
                }
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
