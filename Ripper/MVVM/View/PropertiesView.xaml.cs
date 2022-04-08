using System.IO;
using System.Windows;

using Microsoft.WindowsAPICodePack.Dialogs;

namespace Ripper.MVVM.View
{
    public partial class PropertiesView : System.Windows.Controls.UserControl
    {
        public PropertiesView()
        {
            InitializeComponent();

            Json.Write();

            OutputText.Text = Globals.Output;

            switch (Globals.Format)
            {
                case "mp4":
                    mp4.IsChecked = true;
                    break;
                case "webm":
                    webm.IsChecked = true;
                    break;
                case "mov":
                    mov.IsChecked = true;
                    break;
                case "avi":
                    avi.IsChecked = true;
                    break;
                case "flv":
                    flv.IsChecked = true;
                    break;
                case "mp3":
                    mp3.IsChecked = true;
                    break;
                case "m4a":
                    m4a.IsChecked = true;
                    break;
                case "wav":
                    wav.IsChecked = true;
                    break;
                case "ogg":
                    ogg.IsChecked = true;
                    break;
                case "pcm":
                    pcm.IsChecked = true;
                    break;

            }

            switch (Globals.Resolution)
            {
                case 4320:
                    p4320.IsChecked = true;
                    break;
                case 2160:
                    p2160.IsChecked = true;
                    break;
                case 1440:
                    p1440.IsChecked = true;
                    break;
                case 1080:
                    p1080.IsChecked = true;
                    break;
                case 720:
                    p720.IsChecked = true;
                    break;
                case 480:
                    p480.IsChecked = true;
                    break;
                case 360:
                    p360.IsChecked = true;
                    break;
                case 240:
                    p240.IsChecked = true;
                    break;

            }
        }

        #region Output Path UI...
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(Globals.Output);

            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                InitialDirectory = Globals.Output,
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                OutputText.Text = dialog.FileName;
            }
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Globals.Output = OutputText.Text;
        }
        #endregion

        #region Format Switch UI...
        private void mp4_Checked(object sender, RoutedEventArgs e) { mp4.IsChecked = true; Globals.Format = "mp4"; }

        private void webm_Checked(object sender, RoutedEventArgs e) { webm.IsChecked = true; Globals.Format = "webm"; }

        private void mov_Checked(object sender, RoutedEventArgs e) { mov.IsChecked = true; Globals.Format = "mov"; }

        private void avi_Checked(object sender, RoutedEventArgs e) { avi.IsChecked = true; Globals.Format = "avi"; }

        private void flv_Checked(object sender, RoutedEventArgs e) { flv.IsChecked = true; Globals.Format = "flv"; }

        private void mp3_Checked(object sender, RoutedEventArgs e) { mp3.IsChecked = true; Globals.Format = "mp3"; }

        private void m4a_Checked(object sender, RoutedEventArgs e) { m4a.IsChecked = true; Globals.Format = "m4a"; }

        private void wav_Checked(object sender, RoutedEventArgs e) { wav.IsChecked = true; Globals.Format = "wav"; }

        private void ogg_Checked(object sender, RoutedEventArgs e) { ogg.IsChecked = true; Globals.Format = "ogg"; }

        private void pcm_Checked(object sender, RoutedEventArgs e) { pcm.IsChecked = true; Globals.Format = "pcm"; }
        #endregion

        #region Resolution Switch UI...
        private void p4320_Checked(object sender, RoutedEventArgs e) { p4320.IsChecked = true; Globals.Resolution = 4320; }

        private void p2160_Checked(object sender, RoutedEventArgs e) { p2160.IsChecked = true; Globals.Resolution = 2160; }

        private void p1440_Checked(object sender, RoutedEventArgs e) { p1440.IsChecked = true; Globals.Resolution = 1440; }

        private void p1080_Checked(object sender, RoutedEventArgs e) { p1080.IsChecked = true; Globals.Resolution = 1080; }

        private void p720_Checked(object sender, RoutedEventArgs e) { p720.IsChecked = true; Globals.Resolution = 720; }

        private void p480_Checked(object sender, RoutedEventArgs e) { p480.IsChecked = true; Globals.Resolution = 480; }

        private void p360_Checked(object sender, RoutedEventArgs e) { p360.IsChecked = true; Globals.Resolution = 360; }

        private void p240_Checked(object sender, RoutedEventArgs e) { p240.IsChecked = true; Globals.Resolution = 240; }
        #endregion
    }
}
