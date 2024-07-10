using System;
using System.IO;
using System.Windows;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Threading;

using Microsoft.Win32;
using IWshRuntimeLibrary;

namespace Ripperoni.Install
{
    public partial class MainWindow : Window
    {
        private readonly string start;
        private readonly string path;
        private readonly string zip;
        private readonly bool silent;

        public MainWindow()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1) if (args[1] == "silent") silent = true;

            InitializeComponent();

            if (silent) Opacity = 0;

            start = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs\KPNCIO\";
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\KPNCIO\Ripperoni\";
            zip = Path.GetTempPath() + @"KPNCIO\Ripperoni.zip";

            Task.Factory.StartNew(() => Installation());
        }

        private void Installation()
        {
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
                Stat("Uninstalling current installation...");
                if (System.IO.File.Exists(path + "Uninstall.exe"))
                {
                    Process process = Process.Start(path + "Uninstall.exe", "silent");
                    process.WaitForExit();
                }

                Progression(8);
            }
            catch
            {
                Error("Could not uninstall current installation...", true);
            }

            try
            {
                Stat("Creating temporary directory...");
                Directory.CreateDirectory(System.IO.Path.GetTempPath() + "KPNCIO");

                Progression(16);
            }
            catch
            {
                Error("Could not create temporary directory...", true);
            }

            try
            {
                Stat("Extracting compressed media...");
                System.IO.File.WriteAllBytes(zip, Properties.Resources.Release);

                Progression(22);
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

                Progression(28);
            }
            catch
            {
                Error("Could not delete current installation...", true);
            }

            try
            {
                Stat("Extracting installation media...");
                ZipFile.ExtractToDirectory(zip, path);

                Progression(40);
            }
            catch
            {
                Error("Could not extract media...", true);
            }

            try
            {
                Stat("Extracting compressed executable...");
                System.IO.File.WriteAllBytes(path + "Uninstall.exe", Properties.Resources.Ripperoni_Uninstall);

                Progression(52);
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
                shortstart.Description = "KPNC Ripperoni";
                shortstart.IconLocation = path + "Ripperoni.exe";
                shortstart.TargetPath = path + "Ripperoni.exe";
                shortstart.Save();
                Progression(64);
            }
            catch
            {
                Error("Could not add start menu icon...", false);
            }

            //try
            //{
            //    Stat("Adding uninstall shortcut to start menu...");
            //    IWshShortcut shortuninstall = shell.CreateShortcut(start + "Uninstall.lnk");
            //    shortuninstall.Description = "KPNC Ripperoni Uninstaller";
            //    shortuninstall.IconLocation = path + "Uninstall.exe";
            //    shortuninstall.TargetPath = path + "Uninstall.exe";
            //    shortuninstall.Save();
            //    Progression(76);
            //}
            //catch
            //{
            //    Error("Could not add start menu uninstall icon...", false);
            //}

            try
            {
                Stat("Adding shortcut to desktop...");
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Ripperoni.lnk";
                IWshShortcut shortdesktop = shell.CreateShortcut(desktop);
                shortdesktop.Description = "KPNC Ripperoni";
                shortdesktop.IconLocation = path + "Ripperoni.exe";
                shortdesktop.TargetPath = path + "Ripperoni.exe";
                shortdesktop.Save();
                Progression(88);
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
                key.SetValue("DisplayIcon", path + "Uninstall.exe,0");
                key.SetValue("DisplayName", "KPNC Ripperoni");
                key.SetValue("DisplayVersion", "1.4.1");
                key.SetValue("EstimatedSize", size, RegistryValueKind.DWord);
                key.SetValue("HelpLink", "https://github.com/kpncio/ripperoni");
                key.SetValue("InstallLocation", path);
                key.SetValue("NoModify", 1, RegistryValueKind.DWord);
                key.SetValue("NoRepair", 1, RegistryValueKind.DWord);
                key.SetValue("Publisher", "KPNC Technology");
                key.SetValue("QuietUninstallString", "\"" + path + "Uninstall.exe" + "\" -silent");
                key.SetValue("UninstallString", "\"" + path + "Uninstall.exe" + "\"");
                key.SetValue("URLInfoAbout", "https://github.com/kpncio/ripperoni");
                key.SetValue("URLUpdateInfo", "https://github.com/kpncio/ripperoni/releases");
                key.Close();
            }
            catch
            {
                Error("Could not add uninstaller to registry...", false);
            }

            Stat("Installed...");
            Progression(100);
            System.Threading.Thread.Sleep(2000);

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
            Close();
        }

        private void DragBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion
    }
}
