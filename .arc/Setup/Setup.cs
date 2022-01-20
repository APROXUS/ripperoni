using System;
using System.IO;
using Microsoft.Win32;
using Setup.Properties;
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
            System.Threading.Thread.Sleep(1000);

            Prog(4);
            Stat("Creating temporary directory...");
            Directory.CreateDirectory(Path.GetTempPath() + "APROX TEMP");

            Prog(16);
            Stat("Extracting compressed media...");
            string zip = Path.GetTempPath() + "APROX TEMP/Ripperoni.zip";
            System.IO.File.WriteAllBytes(zip, Resources.Ripperoni);

            Prog(28);
            Stat("Deleting current installation...");
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "APROX Ripperoni/";
            if (Directory.Exists(path)) Directory.Delete(path, true);
            Directory.CreateDirectory(path);

            Prog(40);
            Stat("Extracting installation media...");
            ZipFile.ExtractToDirectory(zip, path);

            Prog(52);
            Stat("Adding shortcut to start menu...");
            string start = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"Microsoft\Windows\Start Menu\Programs\APROX Project\";
            WshShell shellstart = new WshShell();
            IWshShortcut shortstart = (IWshShortcut)shellstart.CreateShortcut(start + "Ripperoni.lnk");
            shortstart.Description = "APROX Ripperoni";
            shortstart.IconLocation = path + "Ripperoni.ico";
            shortstart.TargetPath = path + "Ripperoni.exe";
            shortstart.Save();

            Prog(64);
            Stat("Adding uninstall shortcut to start menu...");
            IWshShortcut unshortstart = (IWshShortcut)shellstart.CreateShortcut(start + "Uninstall.lnk");
            unshortstart.Description = "APROX Ripperoni Uninstaller";
            unshortstart.IconLocation = path + "Uninstall.ico";
            unshortstart.TargetPath = path + "Uninstall.exe";
            unshortstart.Save();

            Prog(76);
            Stat("Adding shortcut to desktop...");
            string desk = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"Microsoft\Windows\Start Menu\Programs\APROX Project\Ripperoni.lnk";
            WshShell shelldesk = new WshShell();
            IWshShortcut shortdesk = (IWshShortcut)shelldesk.CreateShortcut(desk);
            shortdesk.Description = "APROX Ripperoni";
            shortdesk.IconLocation = path + "Ripperoni.ico";
            shortdesk.TargetPath = path + "Ripperoni.exe";
            shortdesk.Save();

            Prog(88);
            Stat("Adding application to registry...");
            long size = new FileInfo(zip).Length / 1024;
            RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Ripperoni");
            key.SetValue("DisplayIcon", path + "Uninstall.ico");
            key.SetValue("DisplayName", "APROX Ripperoni");
            key.SetValue("EstimatedSize", size.ToString(), RegistryValueKind.DWord);
            key.SetValue("NoModify", "1", RegistryValueKind.DWord);
            key.SetValue("NoRepair", "1", RegistryValueKind.DWord);
            key.SetValue("Publisher", "APROX Project");
            key.SetValue("QuietUninstallString", "\"" + path + "Uninstall.exe" + "\" -silent");
            key.SetValue("UninstallString", "\"" + path + "Uninstall.exe" + "\"");
            key.Close();

            Prog(100);
            if (!silent)
            {
                Process.Start(path + "Ripperoni.exe");
            }

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
