using System;
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Uninstall
{
    public partial class Uninstall : Form
    {
        private readonly bool silent;

        private string real;
        private string path;
        private string temp;
        private string startmenu;

        public Uninstall(string[] a)
        {
            if (a.Length > 0) if (a[0] == "-silent") silent = true;

            InitializeComponent();

            if (silent)
            {
                Status.Visible = false;
                Progress.Visible = false;
            }

            Task.Factory.StartNew(() => Uninstallation());
        }

        private void Uninstallation()
        {
            real = AppDomain.CurrentDomain.BaseDirectory;
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\APROX Ripperoni\";
            temp = Path.GetTempPath() + @"APROX TEMP\";
            startmenu = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs\APROX Project\";

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
            Prog(0);

            try
            {
                Stat("Deleting installation directory...");
                if (Directory.Exists(path)) Directory.Delete(path, true);
                Prog(20);
            }
            catch
            {
                Error("Could not remove installation directory...", false);
            }

            try
            {
                Stat("Removing shortcut from start menu...");
                File.Delete(startmenu + "Ripperoni.lnk");
                Prog(40);
            }
            catch
            {
                Error("Could not remove start menu icon...", false);
            }

            try
            {
                Stat("Removing uninstall shortcut from start menu...");
                File.Delete(startmenu + "Uninstall.lnk");
                Prog(60);
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
                Prog(80);
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
            Prog(100);
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
