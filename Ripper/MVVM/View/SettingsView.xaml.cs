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
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "Firewall.bat";
                psi.Verb = "runas";
                Process.Start(psi);
            }
            catch
            {
                Utilities.Error("Could not run console as administator...", "Error", true);
            }
            
        }
        #endregion

        #region Temperary Folder UI...
        private void TempText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Globals.Temp = TempText.Text;
        }
        #endregion

        #region Downloader Settings UI...
        private void OnflyCheck_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try { Globals.OnFly = OnflyCheck.IsChecked ?? false; } catch { }
        }

        private void SpeedText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { Globals.Bytes = int.Parse(SpeedText.Text); } catch { }
        }

        private void DelayText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { Globals.Timeout = int.Parse(DelayText.Text); } catch { }
        }

        private void ChunkText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { Globals.Chunks = int.Parse(ChunkText.Text); } catch { }
        }

        private void TriesText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { Globals.Tries = int.Parse(TriesText.Text); } catch { }
        }

        private void BufferText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { Globals.Buffer = int.Parse(BufferText.Text); } catch { }
        }
        #endregion
    }
}
