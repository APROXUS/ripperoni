using System;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Uninstall
{
    public partial class Uninstall : Form
    {
        private readonly bool silent;

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
            System.Threading.Thread.Sleep(1000);

            Prog(0);
            Stat("Deleting installation directory...");
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "APROX Ripperoni/";
            if (Directory.Exists(path)) Directory.Delete(path, true);

            Prog(20);
            Stat("Removing shortcut from start menu...");
            string start = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"Microsoft\Windows\Start Menu\Programs\APROX Project\";
            File.Delete(start + "Ripperoni.lnk");

            Prog(40);
            Stat("Removing uninstall shortcut from start menu...");
            File.Delete(start + "Uninstall.lnk");

            Prog(60);
            Stat("Removing shortcut from desktop...");
            string desk = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"Microsoft\Windows\Start Menu\Programs\APROX Project\Ripperoni.lnk";
            File.Delete(desk);

            Prog(80);
            Stat("Removing application from registry...");
            Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Ripperoni");
            Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Ripperoni");

            Prog(100);
            if (!silent)
            {
                
            }

            Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");

            Close();
        }

        private void Stat(string s)
        {
            string stat = s?.Length > 36
                ? s.Substring(0, 36) + "..."
                : s;

            Status.Invoke((MethodInvoker)delegate {
                Status.Text = "Task: " + stat;
            });
        }

        private void Prog(int p)
        {
            Progress.Invoke((MethodInvoker)delegate {
                Progress.Style = ProgressBarStyle.Blocks;
                Progress.Value = p;
            });
        }
    }
}
