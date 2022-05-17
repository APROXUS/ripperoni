using System;
using System.Windows;
using System.Diagnostics;
using System.Windows.Input;
using System.Collections.Generic;

using Ripper.MVVM.View;

namespace Ripper
{
    public partial class SearchWindow : Window
    {
        private readonly List<string[]> videos;
        private readonly string query;

        public SearchWindow(string q, List<string[]> v)
        {
            InitializeComponent();

            query = q;
            videos = v;

            List();
        }

        private void List()
        {
            // Create a result view control for each result...

            try
            {
                foreach (var video in videos)
                {
                    var ResultView = new ResultView(this, video);

                    Results.Children.Add(ResultView);
                }
            }
            catch (Exception ex)
            {
                Utilities.Error("Could not list all available videos...", "Executable Error", "012", false, ex);
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
            Process.Start("https://www.youtube.com/results?search_query=" + query);
        }
        #endregion
    }
}
