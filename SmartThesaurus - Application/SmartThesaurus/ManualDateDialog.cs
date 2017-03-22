using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartThesaurus
{
    public partial class ManualDateDialog : Form
    {
        private string date = "";
        formSearch form;
        public ManualDateDialog(formSearch form)
        {
            InitializeComponent();
            dtp.MinDate = DateTime.Today;
            this.form = form;
        }

        private void btnSendManualDate_Click(object sender, EventArgs e)
        {
            date = dtp.Value.ToShortDateString();
            form.setDateXML("Manuel");
            this.Close();
            
        }

        public string getDate()
        {
            return date;
        }


        private void dtp_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
