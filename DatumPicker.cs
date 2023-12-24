using System;
using System.Windows.Forms;

namespace Overbrugging
{
    public partial class DatumPicker : UserControl
    {
        public string Datum
        {
            get
            {
                if (TB.Text == " --/--/----")
                    return "";
                return TB.Text;
            }

            set
            {
                if (value == "")
                {
                    TB.Text = " --/--/----";
                }
                else
                {
                    TB.Text = value;
                }
            }
        }

        public DatumPicker()
        {
            InitializeComponent();
        }

        //private DateTime ZetOmNaarDateTime(string Datum) // is van format "19-11-2023 00:00:00" of "9-4-2001 00:00:00"
        //{
        //    // verwijder tijd
        //    int pos = Datum.IndexOf(" ");
        //    if (pos > -1)
        //    {
        //        Datum = Datum.Substring(0, pos);
        //    }

        //    string[] temp = Datum.Split('-');

        //    int Dag = int.Parse(temp[0]);
        //    int Maand = int.Parse(temp[1]);
        //    int Jaar = int.Parse(temp[2]);

        //    DateTime ret = new DateTime(Jaar, Maand, Dag);
        //    return ret;
        //}

        private void DT_ValueChanged(object sender, EventArgs e)
        {
            TB.Text = DT.Value.ToString("dd-MM-yyyy");
        }
    }
}
