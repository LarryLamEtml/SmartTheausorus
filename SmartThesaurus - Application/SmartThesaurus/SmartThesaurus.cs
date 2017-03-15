using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
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
        List<string> listCmb = new List<string>();
        List<SmarthThesaurusLibrary.File> fileListEtml = new List<SmarthThesaurusLibrary.File>();
        List<SmarthThesaurusLibrary.File> fileListEducanet = new List<SmarthThesaurusLibrary.File>();
        List<SmarthThesaurusLibrary.File> fileListTemp = new List<SmarthThesaurusLibrary.File>();
        string dateToActualiseEtml = "12.12.2017";
        string dateToActualiseEducanet = "25.05.2017";
        string dateToActualiseTemp = "15.02.2017";
        //DirectoryInfo PATH = new DirectoryInfo(@"K:\INF\eleves\temp", SearchOption.AllDirectories);
        const string PATH = @"K:\INF\eleves\temp";
        List<FileInfo> listFileinfoEtml;
        List<FileInfo> listFileinfoEducanet;
        List<SmarthThesaurusLibrary.File> sortedListFile;
        const string ETML = "ETML";
        const string EDUCANET = "Educanet2";
        const string TEMP = "Temp";
        Regex regexCondition;
        manualModeActualisation manualActualisation;
        EducanetLogin login;


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
            initialiseComboBox();
            manualActualisation = new manualModeActualisation(this);
            login = new EducanetLogin(this);
            this.AcceptButton = btnSearchEtml;//Bouton avec le focus (enter pour cliquer)
            //ramene le combobox au dessus  (visuel)
            lblBackColor.BringToFront();
            cmbActualisation.BringToFront();
            actualiseViewTemp();
            readXML(2);
        }

        public void logEducanet()
        {
            var client = new CookieAwareWebClient();
            client.BaseAddress = @"https://www.educanet2.ch";
            var loginData = new NameValueCollection();
            loginData.Add("login", "YourLogin");
            loginData.Add("password", "YourPassword");
            client.UploadValues("login.php", "POST", loginData);

            //Now you are logged in and can request pages    
            string htmlSource = client.DownloadString("index.php");
        }

        public void initialiseComboBox()
        {
            listCmb.Clear();
            listCmb.Add("Actualisation");
            listCmb.Add("Chaque jour");
            listCmb.Add("Chaque heure");
            listCmb.Add("Personnalisé");
            listCmb.Add("Manuel");
            foreach (string choice in listCmb)
            {
                cmbActualisation.Items.Add(choice);
            }
        }
        /////// <summary>
        /////// 
        /////// </summary>
        /////// <param name="index"></param>
        ////public void checkDate(int index)
        ////{
        ////    string fileToRead = "";

        ////    switch (index)
        ////    {
        ////        case 0:
        ////            fileToRead = ("etmlData.xml");
        ////            break;
        ////        case 1:
        ////            fileToRead = ("eduData.xml");
        ////            break;
        ////        case 2:
        ////            fileToRead = ("tempData.xml");
        ////            break;
        ////    }

        ////     ;
        ////    XDocument xmlDoc = XDocument.Load(fileToRead);
        ////    var files = from file in xmlDoc.Descendants("File")
        ////                select new
        ////                {
        ////                    idDateToActualise = Convert.ToInt32(file.Element("idDateToActualise").Value)
        ////                };
        ////    //Vérifie si la date d'actualisation des fichiers correspond à aujourdhui 
        ////    switch (index)
        ////    {
        ////        case 0:
        ////            if (dateToActualiseEtml == DateTime.Now.ToShortDateString())
        ////            {
        ////                actualiseData(index);//Actualise les données
        ////            }
        ////            break;
        ////        case 1:
        ////            if (dateToActualiseEducanet == DateTime.Now.ToShortDateString())
        ////            {
        ////                actualiseData(index);//Actualise les données
        ////            }
        ////            break;
        ////        case 2:
        ////            if (dateToActualiseTemp == DateTime.Now.ToShortDateString())
        ////            {
        ////                actualiseData(index);//Actualise les données
        ////            }
        ////            break;
        ////    }
        ////    //Lis le ficher xml contenants les données
        ////    readXML(index);
        ////}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void actualiseData()
        {
            String[] allTempFiles = Directory.GetFiles(PATH, "*.*", SearchOption.AllDirectories);
            /*//---------------------actualise temp info -----------------------------

            List<FileInfo> listFileinfoTemp = new List<FileInfo>();

            foreach (var file in allTempFiles)
            {
                listFileinfoTemp.Add(new FileInfo(file));
            }
            int idCount = 0;
            fileListTemp.Clear();

            foreach (FileInfo fi in listFileinfoTemp)
            {
                SmarthThesaurusLibrary.File file = new SmarthThesaurusLibrary.File(idCount, fi.Name, BytesToString(fi.Length), fi.LastWriteTime, fi.Directory.ToString(), TbCMain.SelectedIndex);
                fileListTemp.Add(file);
                idCount++;
            }
            tempDataToXML();
            etmlDataToXML();
            eduDataToXML();*/
            SmarthThesaurusLibrary.XML.actualiseData(allTempFiles, fileListTemp, TbCMain.SelectedIndex);
        }

        public void etmlDataToXML()
        {

        }
        public void eduDataToXML()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void tempDataToXML()
        {
            //XmlWriter writer = XmlWriter.Create("tempData.xml", settings);

            //writer.WriteStartDocument();
            //writer.WriteStartElement("Files");

            //foreach (SmarthThesaurusLibrary.File f in fileListTemp)
            //{
            //    writer.WriteStartElement("File");

            //    writer.WriteElementString("id", f.idFile.ToString());
            //    writer.WriteElementString("name", f.Name);
            //    writer.WriteElementString("size", f.Size);
            //    writer.WriteElementString("lastModified", f.LastWriteTime.ToString());
            //    writer.WriteElementString("directory", f.Directory);
            //    writer.WriteElementString("idDateToActualise", f.idDateToActualise.ToString());

            //    writer.WriteEndElement();
            //}

            //writer.WriteEndElement();
            //writer.WriteEndDocument();
            //writer.Dispose();
            //writer.Close();
            SmarthThesaurusLibrary.XML.tempDataToXML(fileListTemp);
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

                    SmarthThesaurusLibrary.File newFile = new SmarthThesaurusLibrary.File(Convert.ToInt32(file.id), file.name, file.size, Convert.ToDateTime(file.lastModified), file.directory, Convert.ToInt32(file.idDateToActualise));
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
                sortedListFile = new List<SmarthThesaurusLibrary.File>();
                listViewResultTemp.Items.Clear();

                if (txbInputTemp.Text != "")
                {
                    regexCondition = new Regex(@".*(" + Regex.Escape(txbInputTemp.Text) + @").*");
                }

                foreach (SmarthThesaurusLibrary.File fi in fileListTemp)
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
            //checkDate(2);
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

        private void cmbActualisation_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualisationService service;
            if (cmbActualisation.Text == "Actualisation")
            {
                cmbActualisation.SelectedIndex = 0;
            }
            switch (cmbActualisation.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    setDateXML(listCmb[cmbActualisation.SelectedIndex]);
                    //Lance ou relance le service
                    //service = new actualisationService(this);
                    startService();
                    break;
                case 2:
                    setDateXML(listCmb[cmbActualisation.SelectedIndex]);
                    //Lance ou relance le service
                    //service = new actualisationService(this);
                    startService();
                    break;
                case 3:
                    setDateXML(listCmb[cmbActualisation.SelectedIndex]);
                    useCron();
                    break;
                case 4:
                    manualActualisation.ShowDialog();
                    if (manualActualisation.getDate() == "")
                    {
                        manualActualisation.Close();
                        setDateXML(listCmb[cmbActualisation.SelectedIndex]);

                    }
                    //Lance ou relance le service
                    //service = new actualisationService(this);
                    startService();
                    break;
            }

        }
        private void startService()
        {
            string serviceName = "ActualisationServiceSmartThesaurus";
            ServiceController controller = new ServiceController(serviceName);
            if (controller.Status == ServiceControllerStatus.Running)
                controller.Refresh();

            if (controller.Status == ServiceControllerStatus.Stopped)
                controller.Start();
        }
        public void useCron()
        {

        }

        public void setDateXML(string text)
        {
            /*
            writer = XmlWriter.Create("actualisationDate.xml", settings);
            settings.Indent = true;
            settings.IndentChars = ("\t");
            settings.OmitXmlDeclaration = true;

            writer.WriteStartDocument();
            writer.WriteStartElement("Date");
            writer.WriteElementString("actualisationMode", text);
            writer.WriteElementString("day", DateTime.Today.DayOfYear.ToString());
            writer.WriteElementString("hour", DateTime.Now.Hour.ToString());
            writer.WriteElementString("manualDate", manualActualisation.getDate());
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Dispose();
            writer.Close();*/
            SmarthThesaurusLibrary.XML.setDateXML(text, DateTime.Today.DayOfYear.ToString(), DateTime.Now.Hour.ToString(), manualActualisation.getDate());

        }
        private void button2_Click(object sender, EventArgs e)
        {
            login.ShowDialog();
        }
    }

}
