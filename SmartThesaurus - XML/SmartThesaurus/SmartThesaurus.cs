using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SmartThesaurus
{
    public partial class formSearch : Form
    {
        List<string> listComboBoxElements = new List<string>();
        //DirectoryInfo PATH = new DirectoryInfo(@"K:\INF\eleves\temp", SearchOption.AllDirectories);
        const string PATH = @"K:\INF\eleves\temp";
        String[] allfiles;
        List<FileInfo> listFileinfo;
        List<FileInfo> sortedListFileInfo;
        const string ETML = "ETML";
        const string EDUCANET = "Educanet2";
        const string TEMP = "Temp";
        Regex regexCondition;
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
            this.AcceptButton = btnSearchEtml;//Bouton avec le focus (enter pour cliquer)

            //change les bordures des boutons
            btnSearchEtml.FlatAppearance.BorderColor = Color.FromArgb(25, 250, 65);
            btnSearchEducanet.FlatAppearance.BorderColor = Color.FromArgb(25, 250, 65);
            btnSearchTemp.FlatAppearance.BorderColor = Color.FromArgb(25, 250, 65);
            actualiseData();
            readXML();
        }

        public void readXML()
        {

        }

        public void writeInXml()
        {
            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.IndentChars = ("\t");
            settings.OmitXmlDeclaration = true;


            XmlWriter writer = XmlWriter.Create("tempData.xml",settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Files");

            foreach (FileInfo fi in listFileinfo)
            {
                writer.WriteStartElement("File");

                writer.WriteElementString("name", fi.Name);  
                writer.WriteElementString("size", BytesToString(fi.Length));
                writer.WriteElementString("lastModified", fi.LastWriteTime.ToString());
                writer.WriteElementString("directory", fi.Directory.ToString());

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }


        public void actualiseData()
        {
            allfiles = Directory.GetFiles(PATH, "*.*", SearchOption.AllDirectories);

            listFileinfo = new List<FileInfo>();

            foreach (var file in allfiles)
            {
                listFileinfo.Add(new FileInfo(file));
            }
            writeInXml();

        }

        public void searchTemp()
        {
            try
            {

                sortedListFileInfo = new List<FileInfo>();
                listViewResultTemp.Items.Clear();

                if (txbInputTemp.Text != "")
                {
                    regexCondition = new Regex(@".*(" + Regex.Escape(txbInputTemp.Text) + @").*");
                }
                foreach (FileInfo fi in listFileinfo)
                {

                    if (txbInputTemp.Text != "")
                    {
                        Match match = regexCondition.Match(fi.Name);
                        if (match.Success)
                        {
                            ListViewItem lvi = new ListViewItem(fi.Name);
                            lvi.SubItems.Add(BytesToString(fi.Length));
                            lvi.SubItems.Add(fi.LastWriteTime.ToString());
                            lvi.SubItems.Add(fi.Directory.ToString());
                            listViewResultTemp.Items.Add(lvi);
                            sortedListFileInfo.Add(fi);
                        }
                    }
                    else
                    {
                        ListViewItem lvi = new ListViewItem(fi.Name);
                        lvi.SubItems.Add(BytesToString(fi.Length));
                        lvi.SubItems.Add(fi.LastWriteTime.ToString());
                        lvi.SubItems.Add(fi.Directory.ToString());
                        listViewResultTemp.Items.Add(lvi);
                        sortedListFileInfo.Add(fi);
                    }

                }
                listViewResultTemp.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);//Ajuste la taille de la colonne en fonction des données
                listViewResultTemp.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//Définis la taille minimum = taille du header
            }
            catch
            {
                MessageBox.Show("Avertissement, le répertoire du chemin " + PATH + " n'à pas été trouvé");
            }
        }

        public void searchEtml()
        {

        }

        /// <summary>
        /// Permet de retourner une taille de fichier lisible
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        static String BytesToString(long byteCount)
        {
            string[] suf = { " B", " KB", " MB", " GB", " TB", " PB", " EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        private void formSearch_Load(object sender, EventArgs e)
        {

        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearchEtml_Click(object sender, EventArgs e)
        {
            searchEtml();
        }

        private void btnSearchTemp_Click(object sender, EventArgs e)
        {
            searchTemp();
        }

        private void listViewResultTemp_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void listViewResultTemp_ItemActivate(object sender, EventArgs e)
        {
            int selectedFileIndex = listViewResultTemp.SelectedItems[0].Index;
            //MessageBox.Show(sortedListFileInfo[selectedFileIndex].DirectoryName + @"\" + sortedListFileInfo[selectedFileIndex].Name);
            try
            {
                Process.Start(sortedListFileInfo[selectedFileIndex].DirectoryName + @"\" + sortedListFileInfo[selectedFileIndex].Name);
            }
            catch
            {
                MessageBox.Show("Erreur lors de l'ouverture du fichier");
            }

        }
    }
}
