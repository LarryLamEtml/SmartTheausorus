using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.ServiceProcess;
using System.Windows.Forms;

namespace SmartThesaurus
{
    public partial class formSearch : Form
    {
        Controller controller;
        List<string> listCmb = new List<string>();
        List<string> fileListEtml = new List<string>();
        List<SmartThesaurusLibrary.File> fileListEducanet = new List<SmartThesaurusLibrary.File>();
        List<SmartThesaurusLibrary.File> fileListTemp = new List<SmartThesaurusLibrary.File>();
        //DirectoryInfo PATH = new DirectoryInfo(@"K:\INF\eleves\temp", SearchOption.AllDirectories);
        const string PATH = @"K:\INF\eleves\temp";
        List<SmartThesaurusLibrary.File> sortedListFileEtml = new List<SmartThesaurusLibrary.File>();
        List<SmartThesaurusLibrary.File> sortedListFileEducanet = new List<SmartThesaurusLibrary.File>();
        List<SmartThesaurusLibrary.File> sortedListFileTemp = new List<SmartThesaurusLibrary.File>();
        const string ETML = "ETML";
        const string EDUCANET = "Educanet2";
        const string TEMP = "Temp";

        ManualDateDialog manualActualisation;
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
            InitialiseForm();
        }

        public void InitialiseForm()
        {
            manualActualisation = new ManualDateDialog(this);
            controller = new Controller(this, manualActualisation);
            login = new EducanetLogin(this);

            this.AcceptButton = btnSearchEtml;//Bouton avec le focus (enter pour cliquer)
            //ramene le combobox au dessus (visuel)
            lblBackColor.BringToFront();
            cmbActualisation.BringToFront();

            initialiseComboBox();
            actualiseData();

            //Affiche tout les fichiers stockés
            controller.checkSearchTemp(txbInputTemp.Text, fileListTemp, sortedListFileTemp, PATH);
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

        /// <summary>
        /// Remplis les options de la ComboBox
        /// </summary>
        public void initialiseComboBox()
        {
            listCmb.Clear();//Vide la liste

            //Ajoute les options dans une liste
            listCmb.Add("Actualisation");
            listCmb.Add("Chaque jour");
            listCmb.Add("Chaque heure");
            listCmb.Add("Personnalisé");
            listCmb.Add("Manuel");

            //Pour chaque élément de la liste, l'ajouter à la ComboBox
            foreach (string choice in listCmb)
            {
                cmbActualisation.Items.Add(choice);
            }
        }


        /// <summary>
        /// Actualise les données du fichier XML
        /// </summary>
        /// <param name="index"></param>
        public void actualiseData()
        {
            controller.actualiseData(PATH, fileListTemp);
            /*controller.actualiseData(PATH, fileListEducanet);
            controller.actualiseData(PATH, fileListEtml);*/
        }

        public void etmlDataToXML()
        {

        }
        public void eduDataToXML()
        {

        }




        /// <summary>
        /// Ajoute l'item à la listView et redimensionne celle-ci
        /// </summary>
        /// <param name="lvi"></param>
        public void addListViewItem(ListViewItem lvi, int index)
        {
            switch (index)
            {
                case 0:
                    listViewResultEtml.Items.Add(lvi);//Ajoute l'item à la listView
                    listViewResultEtml.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);//Ajuste la taille de la colonne en fonction des données
                    listViewResultEtml.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//Définis la taille minimum = taille du header
                    break;
                case 1:
                    listViewResultEducanet.Items.Add(lvi);//Ajoute l'item à la listView
                    listViewResultEducanet.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);//Ajuste la taille de la colonne en fonction des données
                    listViewResultEducanet.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//Définis la taille minimum = taille du header
                    break;
                case 2:
                    listViewResultTemp.Items.Add(lvi);//Ajoute l'item à la listView
                    listViewResultTemp.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);//Ajuste la taille de la colonne en fonction des données
                    listViewResultTemp.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//Définis la taille minimum = taille du header
                    break;
            }
        }



        /// <summary>
        /// Cache la Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Ferme la Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearchEtml_Click(object sender, EventArgs e)
        {
            listViewResultEtml.Items.Clear();
            controller.searchUrlMatching(txbInputEtml.Text,fileListEtml);
            if(fileListEtml.Count==0)
            {
                MessageBox.Show("Aucun résultat trouvé");
            }

        }

        private void btnSearchTemp_Click(object sender, EventArgs e)
        {
            //checkDate(2);
            listViewResultTemp.Items.Clear();
            controller.checkSearchTemp(txbInputTemp.Text, fileListTemp, sortedListFileTemp, PATH);

        }

        /// <summary>
        /// Lorsqu'on double clique sur le nom d'un fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewResultTemp_ItemActivate(object sender, EventArgs e)
        {
            int selectedFileIndex = listViewResultTemp.SelectedItems[0].Index;
            //MessageBox.Show(sortedListFileInfo[selectedFileIndex].DirectoryName + @"\" + sortedListFileInfo[selectedFileIndex].Name);//Test
            try
            {
                //Execute le fichier associé
                Process.Start(sortedListFileTemp[selectedFileIndex].Directory + @"\" + sortedListFileTemp[selectedFileIndex].Name);
            }
            catch
            {
                //Affiche un message d'erreur
                MessageBox.Show("Echec de l'ouverture du fichier, il à peut être été supprimé ou modifié");
            }
        }

        /// <summary>
        /// Changement du service en fonction de l'option d'actualisation choisie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbActualisation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Permet de rendre l'option "Actualisation" inchoisissable
            if (cmbActualisation.Text == "Actualisation")
            {
                cmbActualisation.SelectedIndex = 0;//Change l'index à 0
            }
            //Switch en fonction de l'option du combobox choisie
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

        /// <summary>
        /// Définit le mode et la date d'actualisation dans un fichier XML 
        /// </summary>
        /// <param name="text"></param>
        public void setDateXML(string text)
        {
            controller.setDateXML(text);
        }

        /// <summary>
        /// 
        /// </summary>
        private void startService()
        {
            string serviceName = "ActualisationServiceSmartThesaurus";
            ServiceController controller = new ServiceController(serviceName);
            if (controller.Status == ServiceControllerStatus.Running)
                controller.Refresh();

            if (controller.Status == ServiceControllerStatus.Stopped)
                controller.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        public void useCron()
        {

        }


        /// <summary>
        /// Affiche un popup pour se log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            login.ShowDialog();
        }

        private void listViewResultEtml_ItemActivate(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start(fileListEtml[listViewResultEtml.SelectedItems[0].Index]);

        }
    }

}
