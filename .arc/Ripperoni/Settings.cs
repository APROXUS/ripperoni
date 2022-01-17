using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ripperoni
{
    public partial class Settings : Form
    {
        private Point mouselocation;

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            Json.Read();

            Buffer.Text = Globals.Buffer.ToString();
            Chunks.Text = Globals.Chunks.ToString();
            Bytes.Text = Globals.Bytes.ToString();
            Tries.Text = Globals.Tries.ToString();
            Timeout.Text = Globals.Timeout.ToString();
            Temp.Text = Globals.Temp;
            OnFly.Checked = Globals.OnFly;
        }

        private void Save()
        {
            Globals.Buffer = Int32.Parse(Buffer.Text);
            Globals.Chunks = Int32.Parse(Chunks.Text);
            Globals.Bytes = Int64.Parse(Bytes.Text);
            Globals.Tries = Int32.Parse(Tries.Text);
            Globals.Timeout = Int32.Parse(Timeout.Text);
            Globals.Temp = Temp.Text;
            Globals.OnFly = OnFly.Checked;

            Json.Write();
        }

        #region Handle Bar...
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void Title_MouseDown(object sender, MouseEventArgs e)
        {
            mouselocation = e.Location;
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - mouselocation.X;
                int dy = e.Location.Y - mouselocation.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Save();

            Close();
        }
        #endregion
    }
}
