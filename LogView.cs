using System;
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
    }
}
