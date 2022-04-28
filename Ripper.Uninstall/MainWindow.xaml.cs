using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

namespace Ripper.Uninstall
{
    public partial class MainWindow : Window
    {
        private readonly bool silent;

        private readonly string start;
        private readonly string real;
        private readonly string path;
        private readonly string temp;

        public MainWindow(string[] a)
        {
            if (a.Length > 0) if (a[0] == "-silent") silent = true;

            InitializeComponent();

            if (silent)
            {
                Opacity = 0;
            }

            Task.Factory.StartNew(() => Uninstallation());
        }

        private void Uninstallation()
        {
            real = AppDomain.CurrentDomain.BaseDirectory;
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\APROX Ripperoni\";
            temp = System.IO.Path.GetTempPath() + @"APROX TEMP\";
            start = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs\APROX Project\";

            try
            {
                if (real != temp)
                {
                    Directory.CreateDirectory(temp);
                    File.Copy(real + "Uninstall.exe", temp + "Uninstall.exe", true);
                    //Process.Start(temp + "Uninstall.exe");

                    //Invoke((MethodInvoker)delegate
                    //{
                    //    Close();
                    //});
                }
            }
            catch
            {
                Error("Could not mirror uninstall executable...", true);
            }

            try
            {
                while (Process.GetProcessesByName("Ripperoni").Length > 0)
                {
                    Error("You must close all instances of Ripperoni before installing...", false);
                }
            }
            catch
            {
                Error("Could not determine if Ripperoni is running...", false);
            }

            Stat("Build: " + Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString());
            System.Threading.Thread.Sleep(5000);
            Progress(0);

            try
            {
                Stat("Deleting installation directory...");
                if (Directory.Exists(path)) Directory.Delete(path, true);
                Progress(20);
            }
            catch
            {
                Error("Could not remove installation directory...", false);
            }

            try
            {
                Stat("Removing shortcut from start menu...");
                File.Delete(start + "Ripperoni.lnk");
                Progress(40);
            }
            catch
            {
                Error("Could not remove start menu icon...", false);
            }

            try
            {
                Stat("Removing uninstall shortcut from start menu...");
                File.Delete(start + "Uninstall.lnk");
                Progress(60);
            }
            catch
            {
                Error("Could not remove start menu unistall icon...", false);
            }

            try
            {
                Stat("Removing shortcut from desktop...");
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Ripperoni.lnk";
                File.Delete(desktop);
                Progress(80);
            }
            catch
            {
                Error("Could not remove desktop icon...", false);
            }

            try
            {
                Stat("Removing application from registry...");
                Registry.CurrentUser.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Ripperoni");

                try
                {
                    Stat("Removing application from 64-Bit registry...");
                    Registry.CurrentUser.DeleteSubKeyTree(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Ripperoni");
                }
                catch
                {
                    Stat("No 64-Bit installation detected...");
                }
            }
            catch
            {
                Error("Could not remove uninstaller from registry...", false);
            }

            Stat("Cleaning up...");
            Progress(100);
            System.Threading.Thread.Sleep(1000);

            if (!silent)
            {
                try
                {
                    //Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
                }
                catch
                {

                }
            }

            Invoke((MethodInvoker)delegate
            {
                Close();
            });
        }

        #region Auxiliary
        private void Error(string m, bool f)
        {
            try
            {
                Dispatcher.Invoke(delegate ()
                {
                    Topmost = false;
                });

                MessageBoxIcon i;

                if (f) i = MessageBoxIcon.Error;
                else i = MessageBoxIcon.Warning;

                System.Windows.Forms.MessageBox.Show(m, $"{n}: {t}", MessageBoxButtons.OK, i);

                if (f) Close();

                Dispatcher.Invoke(delegate ()
                {
                    Topmost = true;
                });
            }
            catch
            {
                //You're seriously fucked!
            }
        }

        private void Stat(string s)
        {
            try
            {
                string stat = s?.Length > 36
                ? s.Substring(0, 36) + "..."
                : s;

                Dispatcher.Invoke(delegate ()
                {
                    Status.Text = stat;
                });
            }
            catch
            {
                Error("Could not invoke status text...", false);
            }
        }

        private void Progress(int p)
        {
            try
            {
                Dispatcher.Invoke((Action)delegate ()
                {
                    this.Progress.IsIndeterminate = false;
                    this.Progress.Value = p;
                });
            }
            catch
            {
                Error("Could not invoke progress bar...", false);
            }
        }
        #endregion

        #region Handle Bar UI...
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var a = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
            a.Completed += (s, _) => Close();
            BeginAnimation(OpacityProperty, a);
        }

        private void DragBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion
    }
}
