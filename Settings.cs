using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            DateTime buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
            LabelBuildData.Text = buildDate.ToString();
        }
    }
}
