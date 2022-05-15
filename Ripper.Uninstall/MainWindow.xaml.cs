using System;
using System.IO;
using System.Windows;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

using Microsoft.Win32;

namespace Ripper.Uninstall
{
    public partial class MainWindow : Window
    {
        private readonly bool silent;

        private readonly string start;
        private readonly string real;
        private readonly string path;
        private readonly string temp;

        public MainWindow()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1) if (args[1] == "-silent") silent = true;

            InitializeComponent();

            if (silent) Opacity = 0;

            real = AppDomain.CurrentDomain.BaseDirectory;
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\KPNC Ripperoni\";
            temp = Path.GetTempPath() + @"KPNC TEMP\";
            start = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs\KPNC Project\";

            Task.Factory.StartNew(() => Uninstallation());
        }

        private void Uninstallation()
        {
            try
            {
                if (real == path)
                {
                    Directory.CreateDirectory(temp);
                    File.Copy(real + "Uninstall.exe", temp + "Uninstall.exe", true);
                    Process.Start(temp + "Uninstall.exe");

                    Dispatcher.Invoke(delegate ()
                    {
                        Close();
                    });
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

            Progression(0);

            try
            {
                Stat("Deleting installation directory...");
                if (Directory.Exists(path)) Directory.Delete(path, true);
                Progression(20);
            }
            catch
            {
                Error("Could not remove installation directory...", false);
            }

            try
            {
                Stat("Removing shortcut from start menu...");
                File.Delete(start + "Ripperoni.lnk");
                Progression(40);
            }
            catch
            {
                Error("Could not remove start menu icon...", false);
            }

            try
            {
                Stat("Removing uninstall shortcut from start menu...");
                File.Delete(start + "Uninstall.lnk");
                Progression(60);
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
                Progression(80);
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
            Progression(100);

            Dispatcher.Invoke(delegate ()
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

                System.Windows.Forms.MessageBox.Show(m, "Error!", MessageBoxButtons.OK, i);

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
                Dispatcher.Invoke(delegate ()
                {
                    Status.Text = s;
                });
            }
            catch
            {
                Error("Could not invoke status text...", false);
            }
        }

        private void Progression(int p)
        {
            try
            {
                Dispatcher.Invoke(delegate ()
                {
                    Progress.IsIndeterminate = false;
                    Progress.Value = p;
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
