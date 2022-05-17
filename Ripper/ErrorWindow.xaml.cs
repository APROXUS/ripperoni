using System;
using System.Windows;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Ripper
{
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(string m, string t, string n, bool f)
        {
            InitializeComponent();

            // Set error title, number, message, and image (fatal or warning)...

            Titled.Text = n + ": " + t;
            Message.Text = m;

            if (f)
            {
                Fatal.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Error.png"));
            }
            else
            {
                Fatal.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Warning.png"));
            }
        }

        #region Input UI...
        private void DragBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/kpncio/ripperoni/issues");
        }
        #endregion
    }
}
