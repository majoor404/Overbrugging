using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
