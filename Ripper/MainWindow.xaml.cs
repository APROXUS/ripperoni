using System;
using System.Windows;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Ripper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        #region Footer UI...
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var p = new ProcessStartInfo
            {
                FileName = "https://www.aprox.us/",
                UseShellExecute = true
            };

            Process.Start(p);
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            var p = new ProcessStartInfo
            {
                FileName = "https://www.aprox.us/services/ripper",
                UseShellExecute = true
            };

            Process.Start(p);
        }

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            var p = new ProcessStartInfo
            {
                FileName = "https://www.github.com/aproxus/ripper",
                UseShellExecute = true
            };

            Process.Start(p);
        }

        private void TextBlock_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            //Directory.CreateDirectory(Output.Text);

            var p = new ProcessStartInfo
            {
                //FileName = Output.Text,
                FileName = "explorer",
                Arguments = @"C:\"
            };

            Process.Start(p);
        }
        #endregion

        #region Auxiliary...
        private void MinimizeProcess()
        {
            WindowState = WindowState.Minimized;
        }

        private void ExitProcess()
        {
            var a = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
            a.Completed += (s, _) => Close();
            BeginAnimation(OpacityProperty, a);
        }
        #endregion
    }
}
