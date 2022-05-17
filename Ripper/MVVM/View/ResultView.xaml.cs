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
            // Get online image in WPF compatible form...

            try
            {
                BitmapImage bitmapimage = new BitmapImage();

                if (video[1].Split('.').Last().ToString() == "webp")
                {
                    Bitmap bitmap;

                    byte[] image = new WebClient().DownloadData(video[1]);
                    using (WebP webp = new WebP())
                    {
                        bitmap = webp.Decode(image);
                    }

                    using (MemoryStream stream = new MemoryStream())
                    {
                        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        stream.Position = 0;

                        bitmapimage.BeginInit();
                        bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapimage.StreamSource = stream;
                        bitmapimage.EndInit();
                    }
                }
                else
                {
                    bitmapimage.BeginInit();
                    bitmapimage.UriSource = new Uri(video[1], UriKind.Absolute);
                    bitmapimage.EndInit();
                }

                Thumbnail.ImageSource = bitmapimage;
            }
            catch (Exception ex)
            {
                Utilities.Error("Could not display thumbnail image...", "Executable Error", "038", false, ex);
            }
            #endregion

            // Set video information...

            Title.Text = video[2];
            Description.Text = video[3];
            Author.Text = video[4];
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Download video on selection...

            Globals.Main.Request("https://youtu.be/" + video[0]);

            search.Close();
        }
    }
}
