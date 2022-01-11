using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

using YoutubeDLSharp;
using YoutubeDLSharp.Options;
using YoutubeDLSharp.Metadata;

namespace Ripperoni
{
    public partial class Metadata : Form
    {
        private Point mouselocation;

        public Metadata(string input)
        {
            InitializeComponent();

            GetMetadata(input);
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

        private async void GetMetadata(string input)
        {
            YoutubeDL youtube = new YoutubeDL
            {
                YoutubeDLPath = "ytdlp.exe",
                FFmpegPath = "ffmpeg.exe"
            };

            var res = await youtube.RunVideoDataFetch(input);
            VideoData video = res.Data;
            string title = video.Title;
            string desc = video.Description;
            string uploader = video.Uploader;
            long views = video.ViewCount ?? default;
            DateTime date = video.UploadDate ?? default;
            float length = video.Duration ?? default;

            VideoTitle.Text = title;
            VideoDesc.Text = desc;
            VideoUploader.Text = uploader;
            VideoViews.Text = String.Format("{0:n0}", views);
            VideoLength.Text = TimeSpan.FromSeconds(length).ToString(@"hh\:mm\:ss");
            VideoDate.Text = date.ToString("MM/dd/yyyy");
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

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
