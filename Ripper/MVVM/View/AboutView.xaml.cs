using System;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Controls;

namespace Ripper.MVVM.View
{
    public partial class AboutView : UserControl
    {
        public AboutView()
        {
            InitializeComponent();

            Version v = Assembly.GetExecutingAssembly().GetName().Version;

            Handle.Text = $"KPNC Technology: Ripperoni: {v.Major}.{v.Minor}.{v.Build}.{v.Revision:X}";

            Json.Write();
        }

        #region Packages, Repositories, Licenses...
        private void RS_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://www.kpnc.io/services/ripperoni");
        }

        private void RG_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/kpncio/ripperoni");
        }

        private void RL_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/kpncio/ripperoni/blob/main/LICENSE");
        }

        private void YN_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://www.nuget.org/packages/YoutubeDLSharp");
        }

        private void YG_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/Bluegrams/YoutubeDLSharp");
        }

        private void YL_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/Bluegrams/YoutubeDLSharp/blob/master/LICENSE.txt");
        }

        private void WG_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/JosePineiro/WebP-wrapper");
        }

        private void WL_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/JosePineiro/WebP-wrapper/blob/master/LICENSE");
        }

        private void DN_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://www.nuget.org/packages/Downloader");
        }

        private void DG_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/bezzad/Downloader");
        }

        private void DL_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/bezzad/Downloader/blob/master/LICENSE");
        }

        private void JN_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://www.nuget.org/packages/Javi.FFmpeg");
        }

        private void JG_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/jacovis/Javi.FFmpeg");
        }

        private void JL_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/jacovis/Javi.FFmpeg/blob/master/LICENSE.md");
        }

        private void NN_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://www.nuget.org/packages/Newtonsoft.Json");
        }

        private void NG_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/JamesNK/Newtonsoft.Json");
        }

        private void NL_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md");
        }

        private void EN_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://www.nuget.org/packages/DotNetEnv/");
        }

        private void EG_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/tonerdo/dotnet-env");
        }

        private void EL_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/tonerdo/dotnet-env/blob/master/LICENSE");
        }

        private void GN_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://www.nuget.org/packages/Google.Apis");
        }

        private void GG_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/googleapis/google-api-dotnet-client");
        }

        private void GL_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/googleapis/google-api-dotnet-client/blob/main/LICENSE");
        }
        #endregion
    }
}
