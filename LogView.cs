using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class LogView : Form
    {
        public LogView()
        {
            InitializeComponent();
        }

        private void ButClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LogView_Shown(object sender, EventArgs e)
        {
            TB.SelectionStart = TB.TextLength;
            TB.ScrollToCaret();
            _ = ButClose.Focus();
        }

        private void BTFilter_Click(object sender, EventArgs e)
        {
            Filter(TBFilter.Text);
        }

        private void Filter(string fil)
        {
            if (string.IsNullOrEmpty(fil))
            {
                return;
            }

            TB.Lines = TB.Lines.Where(line => line.ToUpperInvariant().Contains(fil.ToUpperInvariant())).ToArray();
        }
    }
}
