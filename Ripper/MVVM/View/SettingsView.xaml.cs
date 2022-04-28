using System.Diagnostics;
using System.Windows.Controls;

namespace Ripper.MVVM.View
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();

            Json.Write();

            TempText.Text = Globals.Temp;
            OnflyCheck.IsChecked = Globals.OnFly;
            SpeedText.Text = Globals.Bytes.ToString();
            DelayText.Text = Globals.Timeout.ToString();
            ChunkText.Text = Globals.Chunks.ToString();
            TriesText.Text = Globals.Tries.ToString();
            BufferText.Text = Globals.Buffer.ToString();
        }

        #region Firewall Fix UI...
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "Firewall.bat",
                    Verb = "runas"
                };
                Process.Start(psi);
            }
            catch
            {
                Utilities.Error("Could not run batch script as administrator...", "Executable Error", "021", true);
            }
        }
        #endregion

        #region Temperary Folder UI...
        private void TempText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.Temp = TempText.Text;

            Json.Write();
        }
        #endregion

        #region Downloader Settings UI...
        private void OnflyCheck_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try { Globals.OnFly = OnflyCheck.IsChecked ?? false; } catch { }
            Json.Write();
        }

        private void SpeedText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { Globals.Bytes = int.Parse(SpeedText.Text); } catch { }
            Json.Write();
        }

        private void DelayText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { Globals.Timeout = int.Parse(DelayText.Text); } catch { }
            Json.Write();
        }

        private void ChunkText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { Globals.Chunks = int.Parse(ChunkText.Text); } catch { }
            Json.Write();
        }

        private void TriesText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { Globals.Tries = int.Parse(TriesText.Text); } catch { }
            Json.Write();
        }

        private void BufferText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { Globals.Buffer = int.Parse(BufferText.Text); } catch { }
            Json.Write();
        }
        #endregion
    }
}
