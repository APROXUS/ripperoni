using System;
using System.IO;
using System.Windows;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media.Animation;

using Ripper.MVVM.View;

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
                    Directory.Delete(Globals.Temp, true);
                }

                Directory.CreateDirectory(Globals.Temp);
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
        private void InputText_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Globals.Input = InputText.Text;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Records.Children.Add(new RecordView());
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

            if (Directory.Exists(Globals.Temp))
            {
                Directory.Delete(Globals.Temp, true);
            }

            var a = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
            a.Completed += (s, _) => Close();
            BeginAnimation(OpacityProperty, a);
        }

        #endregion
    }
}
