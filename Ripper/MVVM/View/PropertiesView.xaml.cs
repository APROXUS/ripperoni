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

            try
            {
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
            catch
            {
                Utilities.Error("Could not open an explorer instance...", "Executable Error", "020", false);
            }

            Json.Write();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Globals.Output = OutputText.Text;
        }
        #endregion

        #region Format Switch UI...
        private void MP4_Checked(object sender, RoutedEventArgs e) { mp4.IsChecked = true; Globals.Format = "mp4"; Json.Write(); }

        private void WebM_Checked(object sender, RoutedEventArgs e) { webm.IsChecked = true; Globals.Format = "webm"; Json.Write(); }

        private void MOV_Checked(object sender, RoutedEventArgs e) { mov.IsChecked = true; Globals.Format = "mov"; Json.Write(); }

        private void AVI_Checked(object sender, RoutedEventArgs e) { avi.IsChecked = true; Globals.Format = "avi"; Json.Write(); }

        private void FLV_Checked(object sender, RoutedEventArgs e) { flv.IsChecked = true; Globals.Format = "flv"; Json.Write(); }

        private void MP3_Checked(object sender, RoutedEventArgs e) { mp3.IsChecked = true; Globals.Format = "mp3"; Json.Write(); }

        private void M4A_Checked(object sender, RoutedEventArgs e) { m4a.IsChecked = true; Globals.Format = "m4a"; Json.Write(); }

        private void WAV_Checked(object sender, RoutedEventArgs e) { wav.IsChecked = true; Globals.Format = "wav"; Json.Write(); }

        private void OGG_Checked(object sender, RoutedEventArgs e) { ogg.IsChecked = true; Globals.Format = "ogg"; Json.Write(); }

        private void PCM_Checked(object sender, RoutedEventArgs e) { pcm.IsChecked = true; Globals.Format = "pcm"; Json.Write(); }
        #endregion

        #region Resolution Switch UI...
        private void P4320_Checked(object sender, RoutedEventArgs e) { p4320.IsChecked = true; Globals.Resolution = 4320; Json.Write(); }

        private void P2160_Checked(object sender, RoutedEventArgs e) { p2160.IsChecked = true; Globals.Resolution = 2160; Json.Write(); }

        private void P1440_Checked(object sender, RoutedEventArgs e) { p1440.IsChecked = true; Globals.Resolution = 1440; Json.Write(); }

        private void P1080_Checked(object sender, RoutedEventArgs e) { p1080.IsChecked = true; Globals.Resolution = 1080; Json.Write(); }

        private void P720_Checked(object sender, RoutedEventArgs e) { p720.IsChecked = true; Globals.Resolution = 720; Json.Write(); }

        private void P480_Checked(object sender, RoutedEventArgs e) { p480.IsChecked = true; Globals.Resolution = 480; Json.Write(); }

        private void P360_Checked(object sender, RoutedEventArgs e) { p360.IsChecked = true; Globals.Resolution = 360; Json.Write(); }

        private void P240_Checked(object sender, RoutedEventArgs e) { p240.IsChecked = true; Globals.Resolution = 240; Json.Write(); }
        #endregion
    }
}
