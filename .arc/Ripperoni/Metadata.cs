using System;
using System.Drawing;
using System.Windows.Forms;

using YoutubeDLSharp;
using YoutubeDLSharp.Metadata;

namespace Ripperoni
{
    public partial class Metadata : Form
    {
        private Point mouselocation;
        private readonly string input;

        public Metadata(string i)
        {
            input = i;

            InitializeComponent();
        }

        private async void Metadata_Load(object sender, EventArgs e)
        {
            Size = new Size(300, 450);

            Utilities.ForceInternet();

            try
            {
                YoutubeDL y = new YoutubeDL
                {
                    YoutubeDLPath = Globals.Real + "DownloaderP.exe"
                };

                var r = await y.RunVideoDataFetch(input);
                VideoData v = r.Data;
                string title = v.Title;
                string description = v.Description;
                string uploader = v.Uploader;
                long views = v.ViewCount ?? default;
                DateTime date = v.UploadDate ?? default;
                float length = v.Duration ?? default;

                VideoTitle.Text = title;
                VideoDesc.Text = description;
                VideoUploader.Text = uploader;
                VideoViews.Text = String.Format("{0:n0}", views);
                VideoLength.Text = TimeSpan.FromSeconds(length).ToString(@"hh\:mm\:ss");
                VideoDate.Text = date.ToString("MM/dd/yyyy");
            }
            catch
            {
                Utilities.Error($"Could not retrieve information about the inputted URL ({input})...", "Error", false);
            }
        }

        #region Handle Bar
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
                Location = new Point(Location.X + dx, Location.Y + dy);
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion
    }
}
