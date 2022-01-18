using System;
using System.Net;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Ripperoni
{
    public partial class Processing : UserControl
    {
        private readonly string first_epoch;
        private readonly string second_epoch;
        private readonly string second_format;

        private readonly string elements;
        private readonly string real_format;
        private readonly string down_format;
        private readonly string title;
        private readonly string epoch;
        private readonly Processor process;

        private string temp_multiplex;
        private string temp_convert;
        private int progress = 0;
        private readonly int steps = 0;
        private readonly int step = 0;

        public Processing(Processor p, string e1, string e2, string l, string r, string d, string t, string e)
        {
            first_epoch = e1;
            second_epoch = e2;

            if (!string.IsNullOrEmpty(e2))
            {
                second_format = "m4a";
                steps++;
            }

            if (r != d)
            {
                steps++;
            }

            elements = l;
            real_format = r;
            down_format = d;
            title = t;
            epoch = e;
            process = p;

            process.done_tertiary = false;

            InitializeComponent();

            ProcessMedia();
        }

        #region Main Thread
        private void ProcessMedia()
        {
            temp_multiplex = Globals.Temp + "\\" + title + "." + epoch + "." + down_format;
            temp_convert = Globals.Temp + "\\" + title + "." + epoch + "." + real_format;

            Title.Invoke((MethodInvoker)delegate {
                Title.Text = title + " (Audio)";
            });

            Json.Read();

            if (!string.IsNullOrEmpty(second_epoch))
            {
                Multiplex();
            }

            if (real_format != down_format)
            {
                Convert();
            }

            process.done_tertiary = true;
        }

        private void Multiplex()
        {

        }

        private void Convert()
        {

        }
        #endregion

        #region Auxiliary
        private void Progression(int e)
        {
            Status.Invoke((MethodInvoker)delegate
            {
                Status.Text = "[Step " + step + "/" + steps + "]:";
            });

            progress = e;

            int total = (progress + ((step - 1) * 100)) / steps;

            Progress.Invoke((MethodInvoker)delegate
            {
                Progress.Style = ProgressBarStyle.Blocks;
                Progress.Value = total;
            });
        }
        #endregion
    }
}
