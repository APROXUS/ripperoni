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
using Ripper.Install.Properties;
using System.Reflection;
using IWshRuntimeLibrary;
using System.Diagnostics;
using System.IO.Compression;
using System.Windows.Forms;

namespace Ripper.Install
{
    public partial class MainWindow : Window
    {
        private readonly bool silent;

        private string start;
        private string path;
        private string zip;

        public MainWindow(string[] a)
        {
            if (a.Length > 0) if (a[0] == "-silent") silent = true;

            InitializeComponent();

            if (silent)
            {
                Opacity = 0;
            }

            Task.Factory.StartNew(() => Installation());
        }

        private void Installation()
        {
            start = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs\APROX Project\";
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\APROX Ripperoni\";
            zip = System.IO.Path.GetTempPath() + @"APROX TEMP\Ripperoni.zip";

            try
            {
                int expected = 0;

                if (silent) expected = 1;

                while (Process.GetProcessesByName("Ripperoni").Length > expected)
                {
                    Error("You must close all instances of Ripperoni before installing...", false);

                    System.Threading.Thread.Sleep(100);
                }
            }
            catch
            {
                Error("Could not determine if Ripperoni is running...", false);
            }

            Stat("Build: " + Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString());
            System.Threading.Thread.Sleep(1000);
            Progress(0);

            try
            {
                Stat("Creating temporary directory...");
                Directory.CreateDirectory(System.IO.Path.GetTempPath() + "APROX TEMP");
                Progress(4);
            }
            catch
            {
                Error("Could not create temporary directory...", true);
            }

            try
            {
                Stat("Extracting compressed media...");
                System.IO.File.WriteAllBytes(zip, Resources.Release);
                Progress(16);
            }
            catch
            {
                Error("Could not extract compressed...", true);
            }

            try
            {
                Stat("Deleting current installation...");
                if (Directory.Exists(path)) Directory.Delete(path, true);
                Directory.CreateDirectory(path);
                Progress(28);
            }
            catch
            {
                Error("Could not delete current installation...", true);
            }

            try
            {
                Stat("Extracting installation media...");
                ZipFile.ExtractToDirectory(zip, path);
                Progress(40);
            }
            catch
            {
                Error("Could not extract media...", true);
            }

            try
            {
                Stat("Extracting compressed executable...");
                System.IO.File.WriteAllBytes(path + "Uninstall.exe", Resources.Uninstall);
                Progress(52);
            }
            catch
            {
                Error("Could not extract uninstaller...", false);
            }

            WshShell shell = new WshShell();
            Directory.CreateDirectory(start);

            try
            {
                Stat("Adding shortcut to start menu...");
                IWshShortcut shortstart = shell.CreateShortcut(start + "Ripperoni.lnk");
                shortstart.Description = "APROX Ripperoni";
                shortstart.IconLocation = path + "Ripperoni.exe";
                shortstart.TargetPath = path + "Ripperoni.exe";
                shortstart.Save();
                Progress(64);
            }
            catch
            {
                Error("Could not add start menu icon...", false);
            }

            try
            {
                Stat("Adding uninstall shortcut to start menu...");
                IWshShortcut shortuninstall = shell.CreateShortcut(start + "Uninstall.lnk");
                shortuninstall.Description = "APROX Ripperoni Uninstaller";
                shortuninstall.IconLocation = path + "Uninstall.exe";
                shortuninstall.TargetPath = path + "Uninstall.exe";
                shortuninstall.Save();
                Progress(76);
            }
            catch
            {
                Error("Could not add start menu uninstall icon...", false);
            }

            try
            {
                Stat("Adding shortcut to desktop...");
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Ripperoni.lnk";
                IWshShortcut shortdesktop = shell.CreateShortcut(desktop);
                shortdesktop.Description = "APROX Ripperoni";
                shortdesktop.IconLocation = path + "Ripperoni.exe";
                shortdesktop.TargetPath = path + "Ripperoni.exe";
                shortdesktop.Save();
                Progress(88);
            }
            catch
            {
                Error("Could not add desktop icon...", false);
            }

            try
            {
                Stat("Adding application to registry...");
                long size = new FileInfo(zip).Length / 1024;
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Ripperoni");
                key.SetValue("DisplayIcon", path + "Uninstall.ico");
                key.SetValue("DisplayName", "APROX Ripperoni");
                key.SetValue("EstimatedSize", size.ToString(), RegistryValueKind.DWord);
                key.SetValue("NoModify", "1", RegistryValueKind.DWord);
                key.SetValue("NoRepair", "1", RegistryValueKind.DWord);
                key.SetValue("Publisher", "APROX Project");
                key.SetValue("QuietUninstallString", "\"" + path + "Uninstall.exe" + "\" -silent");
                key.SetValue("UninstallString", "\"" + path + "Uninstall.exe" + "\"");
                key.Close();
            }
            catch
            {
                Error("Could not add uninstaller to registry...", false);
            }

            Stat("Cleaning up...");
            Progress(100);
            System.Threading.Thread.Sleep(1000);

            if (!silent)
            {
                try
                {
                    Process.Start(path + "Ripperoni.exe");
                }
                catch
                {
                    Error("Could not open Ripperoni...", false);
                }

            }

            Close();
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

                if (!silent) System.Windows.Forms.MessageBox.Show(m, "Error", MessageBoxButtons.OK, i);

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
