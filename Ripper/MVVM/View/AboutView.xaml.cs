using System.Windows.Controls;

namespace Ripper.MVVM.View
{
    public partial class AboutView : UserControl
    {
        public AboutView()
        {
            InitializeComponent();

            Json.Write();
        }
    }
}
