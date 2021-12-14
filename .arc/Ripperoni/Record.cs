using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ripperoni
{
    public partial class Record : UserControl
    {
        public Record()
        {
            InitializeComponent();
            

        }

        public void LoadImage()
        {

            controlThumbnail.LoadAsync(ControlImage);
        }

        public String ControlImage { get; set; }

        public String ControlTitle 
        {
            set
            {
                controlTitle.Text = value;
            }
        }

       
    }
}
