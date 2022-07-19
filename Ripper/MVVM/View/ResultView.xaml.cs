using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using WebPWrapper;
using YouTubeApiSharp;

namespace Ripper.MVVM.View
{
    public partial class ResultView : UserControl
    {
        private readonly VideoSearchComponents video;
        private readonly SearchWindow search;

        public ResultView(SearchWindow s, VideoSearchComponents v)
        {
            InitializeComponent();

            video = v;
            search = s;

            #region Image Processing...
            // Get online image in WPF compatible form...
            BitmapImage bitmapimage = new BitmapImage();
            try
            {
                if (video.getThumbnail().Split('.').Last().ToString() == "webp")
                {
                    Bitmap bitmap;

                    byte[] image = new WebClient().DownloadData(video.getThumbnail());
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
                    bitmapimage.UriSource = new Uri(video.getThumbnail(), UriKind.Absolute);
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

            Title.Text = video.getTitle();
            Author.Text = video.getAuthor();
            Duration.Text = video.getDuration();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Download video on selection...

            Globals.Main.Request(video);

            search.Close();
        }
    }
}