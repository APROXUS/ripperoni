using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

using Downloader;
using WebPWrapper;
using YoutubeDLSharp;
using YoutubeDLSharp.Metadata;

namespace Ripperoni
{
    public partial class Main : Form
    {
        private Point mouselocation;

        public Main()
        {
            InitializeComponent();

            Json.Read();

            if (Directory.Exists(Globals.Temp))
            {
                Directory.Delete(Globals.Temp, true);
            }

            Directory.CreateDirectory(Globals.Temp);
        }
        
        private void Main_Load(object sender, EventArgs e)
        {
            Format.SelectedItem = ".MP4";
            Format_SelectedIndexChanged(sender, e);

            Input.Text = "https://youtu.be/dQw4w9WgXcQ";
            //Input.Text = "https://www.youtube.com/watch?v=tPEE9ZwTmy0";
            //Input.Text = "https://vimeo.com/660521773";
            //Input.Text = "https://vimeo.com/666109154";

            Output.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        }

        #region Handle Bar...
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

        #region Upper UI...
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
                    Resolution.Items.Add("4320p (8K)");
                    Resolution.Items.Add("2160p (4K)");
                    Resolution.Items.Add("1440p (QHD)");
                    Resolution.Items.Add("1080p (FHD)");
                    Resolution.Items.Add("720p (HD)");
                    Resolution.Items.Add("480p (SD)");
                    Resolution.Items.Add("360p");
                    Resolution.Items.Add("240p");
                    Resolution.Items.Add("144p");
                    Resolution.SelectedItem = "1080p (FHD)";
                    break;
                default:
                    Elements.Items.Clear();
                    Elements.Items.Add("Audio Only");
                    Elements.SelectedItem = "Audio Only";

                    Resolution.Items.Clear();
                    Resolution.Items.Add("Highest");
                    Resolution.SelectedItem = "Highest";
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
        #endregion

        #region Lower UI...
        private void Support_Click(object sender, EventArgs e)
        {
            Process.Start("https://ytdl-org.github.io/youtube-dl/supportedsites.html");
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void Metadata_Click(object sender, EventArgs e)
        {
            Metadata metadata = new Metadata(Input.Text);
            metadata.ShowDialog();
        }

        private void OpenFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                InitialDirectory = Output.Text,
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Output.Text = dialog.FileName;
            }
        }

        private void Convert_Click(object sender, EventArgs e)
        {
            if (Path.IsPathRooted(Output.Text))
            {
                Directory.CreateDirectory(Output.Text);

                Records.VerticalScroll.Visible = true;
                Records.HorizontalScroll.Maximum = 0;
                Records.AutoScroll = true;
                Records.ResumeLayout();

                Processor p = new Processor();
                p.Process(Input.Text, Output.Text, (string)Format.SelectedItem, (string)Resolution.SelectedItem, (string)Elements.SelectedItem);
            }
        }
        #endregion

        #region Footer UI...
        private void FooterIcon_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.aprox.us/");
        }

        private void Website_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.aprox.us/service/ripperoni");
        }

        private void Repository_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.github.com/aproxus/ripperoni");
        }

        private void Folder_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Output.Text);

            ProcessStartInfo info = new ProcessStartInfo
            {
                Arguments = Output.Text,
                FileName = "explorer.exe"
            };

            Process.Start(info);
        }

        #region Hover Effect...
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

        private void Repository_MouseHover(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f, FontStyle.Underline);
            Repository.Font = font;
        }

        private void Repository_MouseLeave(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f);
            Repository.Font = font;
        }

        private void Settings_MouseHover(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f, FontStyle.Underline);
            Folder.Font = font;
        }

        private void Settings_MouseLeave(object sender, EventArgs e)
        {
            Font font = new Font("Segoe UI", 8.25f);
            Folder.Font = font;
        }
        #endregion
        #endregion

        #region Auxiliary...
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

    public class Processor
    {
        private Main m;

        private string epoch_primary;
        private string epoch_secondary;
        private string epoch_tertiary;

        private string input;
        private string output;
        private string format;
        private string resolution;
        private string elements;

        private string title;
        private FormatData[] formats;

        private string real_format = "mp4";
        private string down_format = "mp4";

        public void Process(string i, string o, string f, string r, string e)
        {
            m = new Main();

            input = i;
            output = o;
            format = f;
            resolution = r;
            elements = e;

            epoch_primary = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            UseRecord();

            epoch_secondary = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            UseAdditional();

            epoch_tertiary = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            UseProcessing();
        }

        private void UseRecord()
        {
            Record r = new Record(format, resolution, elements, input, epoch_primary);
            m.Records.Controls.Add(r);

            title = r.title;
            formats = r.formats;
            real_format = r.real_format;
            down_format = r.down_format;
        }

        private void UseAdditional()
        {
            Additional a = new Additional(formats, title, epoch_secondary);
            m.Records.Controls.Add(a);
        }

        private void UseProcessing()
        {
            Processing p = new Processing(formats, title, epoch_tertiary);
            m.Records.Controls.Add(p);
        }
    }
}
