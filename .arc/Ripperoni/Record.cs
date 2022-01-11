using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;

using WebPWrapper;

namespace Ripperoni
{
    public partial class Record : UserControl
    {
        public Record(string th, string ti, string au, string le, string da)
        {
            InitializeComponent();

            DownloadFileAsync(th).GetAwaiter();

            Thread.Sleep(100);

            byte[] image = File.ReadAllBytes(Path.GetTempPath() + "thumbnail.webp");
            using (WebP webp = new WebP())
                Thumbnail.Image = webp.Decode(image);

            Title.Text = ti;
            Author.Text = au;
            Length.Text = le;
            Date.Text = da;
        }

        private static async Task DownloadFileAsync(string th)
        {
            WebClient client = new WebClient();
            await client.DownloadFileTaskAsync(new Uri(th), Path.GetTempPath() + "thumbnail.webp");
        }
    }
}
