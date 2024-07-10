using System;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Controls;

namespace Ripperoni.MVVM.View
{
    public partial class AboutView : UserControl
    {
        public AboutView()
        {
            InitializeComponent();

            // Show current version and generated revision number (in hexidecimal)...

            Version v = Assembly.GetExecutingAssembly().GetName().Version;

            Handle.Text = $"KPNC Technology: Ripperoni: {v.Major}.{v.Minor}.{v.Build}.{v.Revision:X}";

            Json.Write();
        }

        #region Packages, Repositories, Licenses...
        // Link handlers...

        private void Y_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/Bluegrams/YoutubeDLSharp");
        }

        private void W_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/JosePineiro/WebP-wrapper");
        }

        private void D_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/bezzad/Downloader");
        }

        private void J_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/jacovis/Javi.FFmpeg");
        }

        private void N_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/JamesNK/Newtonsoft.Json");
        }

        private void E_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/tonerdo/dotnet-env");
        }

        private void G_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/googleapis/google-api-dotnet-client");
        }

        private void F_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://git.ffmpeg.org/gitweb/ffmpeg.git");
        }

        private void P_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/yt-dlp/yt-dlp");
        }
        #endregion
    }
}
