using System.Windows.Controls;

namespace Ripper.MVVM.View
{
    public partial class DownloadsView : UserControl
    {
        public DownloadsView()
        {
            InitializeComponent();

            Json.Write();
        }

        public void DownloadProcess()
        {
            Records.Children.Add(new RecordView());
        }
    }
}
