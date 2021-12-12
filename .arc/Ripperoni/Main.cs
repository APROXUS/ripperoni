using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.WindowsAPICodePack.Dialogs;

using YoutubeDLSharp;
using Newtonsoft.Json;

namespace Ripperoni
{
    public partial class Main : Form
    {
        private Point mouselocation;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Format.SelectedItem = ".MP4";

            Format_SelectedIndexChanged(sender, e);

            Link.Text = "https://youtu.be/dQw4w9WgXcQ";
            Output.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        #region Handle Bar
        private void Exit_Click(object sender, EventArgs e)
        {
            ExitProcess();
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            MinimizeProcess();
        }

        private void HandleLayout_MouseDown(object sender, MouseEventArgs e)
        {
            mouselocation = e.Location;
        }

        private void HandleLayout_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - mouselocation.X;
                int dy = e.Location.Y - mouselocation.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        private void Icon_MouseDown(object sender, MouseEventArgs e)
        {
            mouselocation = e.Location;
        }

        private void Icon_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - mouselocation.X;
                int dy = e.Location.Y - mouselocation.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        private void Title_MouseDown(object sender, MouseEventArgs e)
        {
            mouselocation = e.Location;
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - mouselocation.X;
                int dy = e.Location.Y - mouselocation.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }
        #endregion

        #region Main UI
        private void Format_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Format.SelectedItem)
            {
                case ".MP4":
                case ".WebM":
                case ".FLV":
                case ".3GP":
                case ".MOV":
                case ".AVI":
                    Elements.Items.Clear();
                    Elements.Items.Add("Audio/Video");
                    Elements.Items.Add("Video Only");
                    Elements.SelectedItem = "Audio/Video";

                    Resolution.Items.Clear();
                    Resolution.Items.Add("4320p");
                    Resolution.Items.Add("2160p");
                    Resolution.Items.Add("1440p");
                    Resolution.Items.Add("1080p");
                    Resolution.Items.Add("720p");
                    Resolution.Items.Add("480p");
                    Resolution.Items.Add("360p");
                    Resolution.Items.Add("240p");
                    Resolution.Items.Add("144p");
                    Resolution.SelectedItem = "1080p";
                    break;
                default:
                    Elements.Items.Clear();
                    Elements.Items.Add("Audio Only");
                    Elements.SelectedItem = "Audio Only";

                    Resolution.Items.Clear();
                    Resolution.Items.Add("None");
                    Resolution.SelectedItem = "None";
                    break;
            }

            switch (Format.SelectedItem)
            {
                case "-- Video --":
                    Format.SelectedItem = ".MP4";
                    break;
                case "-- Audio --":
                    Format.SelectedItem = ".MP3";
                    break;
            }
        }

        private void OpenFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Output.Text;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Output.Text = dialog.FileName;
            }
        }

        private void Convert_Click(object sender, EventArgs e)
        {
            if (Path.IsPathRooted(Output.Text))
            {
                try
                {
                    Path.GetFullPath(Output.Text);

                    try
                    {
                        Directory.CreateDirectory(Output.Text);

                        // Start Ripping Process
                    }
                    catch
                    {
                        ErrorProcess("Could not create directory...", "Error");
                    }
                }
                catch
                {
                    ErrorProcess("Please enter a valid path...", "Error");
                }
            }
            else
            {
                ErrorProcess("Please enter a global path...", "Error");
            }
        }

        private void Support_Click(object sender, EventArgs e)
        {
            Process.Start("https://ytdl-org.github.io/youtube-dl/supportedsites.html");
        }

        private void Metadata_Click(object sender, EventArgs e)
        {
            Metadata metadata = new Metadata();
            metadata.ShowDialog();
        }
        #endregion

        #region Footer
        private void Settings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void Repository_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.github.com/aproxus/ripperoni");
        }

        private void Website_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.aprox.us/service/ripperoni");
        }

        private void FooterIcon_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.aprox.us/");
        }

        private void Settings_MouseHover(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f, FontStyle.Underline);
            Settings.Font = font;
        }

        private void Repository_MouseHover(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f, FontStyle.Underline);
            Repository.Font = font;
        }

        private void Website_MouseHover(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f, FontStyle.Underline);
            Website.Font = font;
        }

        private void Website_MouseLeave(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f);
            Website.Font = font;
        }

        private void Repository_MouseLeave(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f);
            Repository.Font = font;
        }

        private void Settings_MouseLeave(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f);
            Settings.Font = font;
        }
        #endregion

        #region Auxiliary
        private void ErrorProcess(string m, string t)
        {
            MessageBox.Show(m, t, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        
        private void MinimizeProcess()
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ExitProcess()
        {
            Close();
        }
        #endregion
    }
}
