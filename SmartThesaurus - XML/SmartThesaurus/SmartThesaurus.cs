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
using System.Xml.Linq;

namespace SmartThesaurus
{
    public partial class formSearch : Form
    {
        List<string> listComboBoxElements = new List<string>();
        List<File> fileListEtml = new List<File>();
        List<File> fileListEducanet = new List<File>();
        List<File> fileListTemp = new List<File>();
        string dateToActualiseEtml = "12.12.2017";
        string dateToActualiseEducanet = "25.05.2017";
        string dateToActualiseTemp = "15.02.2017";
        //DirectoryInfo PATH = new DirectoryInfo(@"K:\INF\eleves\temp", SearchOption.AllDirectories);
        const string PATH = @"K:\INF\eleves\temp";
        String[] allTempFiles;
        List<FileInfo> listFileinfoEtml;
        List<FileInfo> listFileinfoEducanet;
        List<FileInfo> listFileinfoTemp;
        List<File> sortedListFile;
        const string ETML = "ETML";
        const string EDUCANET = "Educanet2";
        const string TEMP = "Temp";
        Regex regexCondition;
        /// <summary> 
        /// Permet de pouvoir deplacer l'application malgré que le formBorderStyle soit à "none" !! mais permet de l'agrandir
        /// </summary>
        /// <param name="m"></param>
      /*  protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HT_CLIENT = 0x1;
            const int HT_CAPTION = 0x2;
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);

        }*/

        public formSearch()
        {
            InitializeComponent();
            this.AcceptButton = btnSearchEtml;//Bouton avec le focus (enter pour cliquer)

            //change les bordures des boutons
            btnSearchEtml.FlatAppearance.BorderColor = Color.FromArgb(25, 250, 65);
            btnSearchEducanet.FlatAppearance.BorderColor = Color.FromArgb(25, 250, 65);
            btnSearchTemp.FlatAppearance.BorderColor = Color.FromArgb(25, 250, 65);

            //Vérification de la date d'actualisation des fichiers
            checkDate(0);
            checkDate(1);
            checkDate(2);

            actualiseViewTemp();



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void checkDate(int index)
        {
            string fileToRead = "";
            int idDateToActualise = 0;

            switch (index)
            {
                case 0:
                    fileToRead = ("etmlData.xml");
                    idDateToActualise = 0;
                    break;
                case 1:
                    fileToRead = ("eduData.xml");
                    idDateToActualise = 1;
                    break;
                case 2:
                    fileToRead = ("tempData.xml");
                    idDateToActualise = 2;
                    break;
            }
            
           /* ;

            var files = from file in xmlDoc.Descendants("File")
                        select idDateToActualise = Convert.ToInt32(file.Element("idDateToActualise").Value);
                        */
            //Vérifie si la date d'actualisation des fichiers correspond à aujourdhui 
            switch (idDateToActualise)
            {
                case 0:
                    if (dateToActualiseEtml == DateTime.Now.ToShortDateString())
                    {
                        actualiseData(idDateToActualise);//Actualise les données
                    }
                    break;
                case 1:
                    if (dateToActualiseEducanet == DateTime.Now.ToShortDateString())
                    {
                        actualiseData(idDateToActualise);//Actualise les données
                    }
                    break;
                case 2:
                    if (dateToActualiseTemp == DateTime.Now.ToShortDateString())
                    {
                        actualiseData(idDateToActualise);//Actualise les données
                    }
                    break;
            }
            //Lis le ficher xml contenants les données
            readXML(idDateToActualise);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void actualiseData(int index)
        {
            allTempFiles = Directory.GetFiles(PATH, "*.*", SearchOption.AllDirectories);
            /*
            listFileinfoEducanet.Add(new FileInfo(file));
            listFileinfoTemp.Add(new FileInfo(file));
            */

            switch (index)
            {
                case 0:

                    break;
                case 1:

                    break;
                case 2:
                    listFileinfoTemp = new List<FileInfo>();

                    foreach (var file in allTempFiles)
                    {
                        listFileinfoTemp.Add(new FileInfo(file));
                    }
                    int idCount = 0;
                    fileListTemp.Clear();

                    foreach (FileInfo fi in listFileinfoTemp)
                    {
                        File file = new File(idCount, fi.Name, BytesToString(fi.Length), fi.LastWriteTime, fi.Directory.ToString(), TbCMain.SelectedIndex);
                        fileListTemp.Add(file);
                        idCount++;
                    }
                    break;

            }
            dataToXML();
        }
        /// <summary>
        /// 
        /// </summary>
        public void dataToXML()
        {
            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.IndentChars = ("\t");
            settings.OmitXmlDeclaration = true;

            XmlWriter writer = XmlWriter.Create("tempData.xml", settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Files");

            foreach (File f in fileListTemp)
            {
                writer.WriteStartElement("File");

                writer.WriteElementString("id", f.idFile.ToString());
                writer.WriteElementString("name", f.Name);
                writer.WriteElementString("size", f.Size);
                writer.WriteElementString("lastModified", f.LastWriteTime.ToString());
                writer.WriteElementString("directory", f.Directory);
                writer.WriteElementString("idDateToActualise", f.idDateToActualise.ToString());

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void readXML(int index)
        {
            string fileToRead = "";
            switch (index)
            {
                case 0:
                    fileToRead = ("etmlData.xml");
                    break;
                case 1:
                    fileToRead = ("eduData.xml");
                    break;
                case 2:
                    fileToRead = ("tempData.xml");
                    break;
            }
            try
            {
                fileListTemp.Clear();
                XDocument xmlDoc = XDocument.Load(fileToRead);
                var files = from file in xmlDoc.Descendants("File")
                            select new
                            {
                                id = file.Element("id").Value,
                                name = file.Element("name").Value,
                                size = file.Element("size").Value,
                                lastModified = file.Element("lastModified").Value,
                                directory = file.Element("directory").Value,
                                idDateToActualise = file.Element("idDateToActualise").Value,
                            };
                // string text = "";

                //Lis chaque fichier dans le fichier XML et lajoute dans la liste des fichiers (local)
                foreach (var file in files)
                {
                   /* text = text + "Id: " + file.id + "\n";
                    text = text + "Name: " + file.name + "\n";
                    text = text + "size: " + file.size + "\n";
                    text = text + "lastModified: " + file.lastModified + "\n";
                    text = text + "directory: " + file.directory + "\n";
                    text = text + "idDateToActualise: " + file.idDateToActualise + "\n\n";*/

                    File newFile = new File(Convert.ToInt32(file.id), file.name, file.size, Convert.ToDateTime(file.lastModified), file.directory, Convert.ToInt32(file.idDateToActualise));
                    fileListTemp.Add(newFile);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Il n'y aucun documents enregistré dans la base de donnée, veuillez la mettre à jour");

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void actualiseViewTemp()
        {
            try
            {
                sortedListFile = new List<File>();
                listViewResultTemp.Items.Clear();

                if (txbInputTemp.Text != "")
                {
                    regexCondition = new Regex(@".*(" + Regex.Escape(txbInputTemp.Text) + @").*");
                }

                foreach (File fi in fileListTemp)
                {
                    if (txbInputTemp.Text != "")
                    {
                        Match match = regexCondition.Match(fi.Name);
                        if (match.Success)
                        {
                            ListViewItem lvi = new ListViewItem(fi.Name);
                            lvi.SubItems.Add((fi.Size));
                            lvi.SubItems.Add(fi.LastWriteTime.ToString());
                            lvi.SubItems.Add(fi.Directory.ToString());
                            listViewResultTemp.Items.Add(lvi);
                            sortedListFile.Add(fi);
                        }
                    }
                    else
                    {
                        ListViewItem lvi = new ListViewItem(fi.Name);
                        lvi.SubItems.Add((fi.Size));
                        lvi.SubItems.Add(fi.LastWriteTime.ToString());
                        lvi.SubItems.Add(fi.Directory.ToString());
                        listViewResultTemp.Items.Add(lvi);
                        sortedListFile.Add(fi);
                    }

                }
                listViewResultTemp.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);//Ajuste la taille de la colonne en fonction des données
                listViewResultTemp.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//Définis la taille minimum = taille du header
            }
            catch (Exception ex)
            {
                MessageBox.Show("Avertissement, le répertoire du chemin " + PATH + " n'à pas été trouvé " + ex);
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
            actualiseViewTemp();
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
                Process.Start(sortedListFile[selectedFileIndex].Directory + @"\" + sortedListFile[selectedFileIndex].Name);
            }
            catch
            {
                MessageBox.Show("Erreur lors de l'ouverture du fichier, il à peut être été supprimé ou modifié");
            }

        }
    }
}
