using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using WebPWrapper;

namespace Ripper.MVVM.View
{
    public partial class ResultView : UserControl
    {
        private readonly SearchWindow search;
        private readonly string[] video;

        public ResultView(SearchWindow s, string[] v)
        {
            InitializeComponent();

            video = v;
            search = s;

            #region Image Processing...
            try
            {
                BitmapImage bi = new BitmapImage();

                if (video[1].Split('.').Last().ToString() == "webp")
                {
                    Bitmap bm;

                    byte[] i = new WebClient().DownloadData(video[1]);
                    using (WebP w = new WebP())
                    {
                        bm = w.Decode(i);
                    }

                    using (var m = new MemoryStream())
                    {
                        bm.Save(m, System.Drawing.Imaging.ImageFormat.Png);
                        m.Position = 0;

                        bi.BeginInit();
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.StreamSource = m;
                        bi.EndInit();
                    }
                }
                else
                {
                    bi.BeginInit();
                    bi.UriSource = new Uri(video[1], UriKind.Absolute);
                    bi.EndInit();
                }

                Thumbnail.ImageSource = bi;
            }
            catch (Exception ex)
            {
                Utilities.Error("Could not display thumbnail image...", "Executable Error", "038", false, ex);
            }
            #endregion

            Title.Text = video[2];
            Description.Text = video[3];
            Author.Text = video[4];
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Globals.Main.Request("https://youtu.be/" + video[0]);

            search.Close();
        }
    }
}
