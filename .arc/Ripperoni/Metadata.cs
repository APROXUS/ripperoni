using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ripperoni
{
    public partial class Metadata : Form
    {
        private Point mouselocation;

        public Metadata()
        {
            InitializeComponent();
        }

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
            Close();
        }
    }
}
