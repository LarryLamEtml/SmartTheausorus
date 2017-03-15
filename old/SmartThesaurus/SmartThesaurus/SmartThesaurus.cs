using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartThesaurus
{
    public partial class formSearch : Form
    {

        /// <summary> 
        /// Permet de pouvoir deplacer l'application malgré que le formBorderStyle soit à "none" !! mais permet de l'agrandir
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HT_CLIENT = 0x1;
            const int HT_CAPTION = 0x2;
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);

        }

        public formSearch()
        {
            InitializeComponent();
            this.AcceptButton = btnSearch;
            string path = @"K:\INF\eleves\temp";
            string[] filePaths = Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories);
        }

        

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbIndex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
        
    }
}
