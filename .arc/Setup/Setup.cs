using System;
using System.IO;
using Microsoft.Win32;
using Setup.Properties;
using System.Reflection;
using IWshRuntimeLibrary;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Setup
{
    public partial class Setup : Form
    {
        private readonly bool silent;

        private string startmenu;
        private string path;
        private string zip;

        public Setup(string[] a)
        {
            if (a.Length > 0) if (a[0] == "-silent") silent = true;

            InitializeComponent();

            if (silent)
            {
                Status.Visible = false;
                Progress.Visible = false;
            }

            Task.Factory.StartNew(() => Installation());
        }

        private void Installation()
        {
            startmenu = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs\APROX Project\";
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\APROX Ripperoni\";
            zip = Path.GetTempPath() + @"APROX TEMP\Ripperoni.zip";

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
            System.Threading.Thread.Sleep(1000);
            Prog(0);

            try
            {
                Stat("Creating temporary directory...");
                Directory.CreateDirectory(Path.GetTempPath() + "APROX TEMP");
                Prog(4);
            }
            catch
            {
                Error("Could not create temporary directory...", true);
            }

            try
            {
                Stat("Extracting compressed media...");
                System.IO.File.WriteAllBytes(zip, Resources.Ripperoni);
                Prog(16);
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
                Prog(28);
            }
            catch
            {
                Error("Could not delete current installation...", true);
            }

            try
            {
                Stat("Extracting installation media...");
                ZipFile.ExtractToDirectory(zip, path);
                Prog(40);
            }
            catch
            {
                Error("Could not extract media...", true);
            }

            try
            {
                Stat("Extracting compressed executable...");
                System.IO.File.WriteAllBytes(path + "Uninstall.exe", Resources.Uninstall);
                Prog(52);
            }
            catch
            {
                Error("Could not extract uninstaller...", false);
            }

            WshShell shell = new WshShell();
            Directory.CreateDirectory(startmenu);

            try
            {
                Stat("Adding shortcut to start menu...");
                IWshShortcut shortstart = shell.CreateShortcut(startmenu + "Ripperoni.lnk");
                shortstart.Description = "APROX Ripperoni";
                shortstart.IconLocation = path + "Ripperoni.exe";
                shortstart.TargetPath = path + "Ripperoni.exe";
                shortstart.Save();
                Prog(64);
            }
            catch
            {
                Error("Could not add start menu icon...", false);
            }

            try
            {
                Stat("Adding uninstall shortcut to start menu...");
                IWshShortcut shortuninstall = shell.CreateShortcut(startmenu + "Uninstall.lnk");
                shortuninstall.Description = "APROX Ripperoni Uninstaller";
                shortuninstall.IconLocation = path + "Uninstall.exe";
                shortuninstall.TargetPath = path + "Uninstall.exe";
                shortuninstall.Save();
                Prog(76);
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
                Prog(88);
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
            Prog(100);
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
                Invoke((MethodInvoker)delegate
                {
                    TopMost = false;
                });

                MessageBoxIcon i;

                if (f) i = MessageBoxIcon.Error;
                else i = MessageBoxIcon.Warning;

                if (!silent) MessageBox.Show(m, "Error", MessageBoxButtons.OK, i);

                if (f) Application.Exit();

                Invoke((MethodInvoker)delegate
                {
                    TopMost = true;
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

                Status.Invoke((MethodInvoker)delegate {
                    Status.Text = stat;
                });
            }
            catch
            {
                Error("Could not invoke status text...", false);
            }
        }

        private void Prog(int p)
        {
            try
            {
                Progress.Invoke((MethodInvoker)delegate {
                    Progress.Style = ProgressBarStyle.Blocks;
                    Progress.Value = p;
                });
            }
            catch
            {
                Error("Could not invoke progress bar...", false);
            }
        }
        #endregion
    }
}
