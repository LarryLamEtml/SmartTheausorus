using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.ServiceProcess;
using System.Windows.Forms;

namespace SmartThesaurus
{
    public partial class formSearch : Form
    {
        Controller controller;
        ManualDateDialog manualActualisation;
        EducanetLogin login;
        ServiceBase service;

        const string PATH = @"K:\INF\eleves\temp";

        List<string> listCmb = new List<string>();
        List<SmartThesaurusLibrary.File> fileListEducanet = new List<SmartThesaurusLibrary.File>();
        List<SmartThesaurusLibrary.File> fileListTemp = new List<SmartThesaurusLibrary.File>();
        List<SmartThesaurusLibrary.File> sortedListFileTemp = new List<SmartThesaurusLibrary.File>();

        public formSearch()
        {
            InitializeComponent();
            InitialiseForm();
            
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.AcceptButton = btnClose;//Bouton avec le focus (enter pour cliquer)
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        //Global variables;
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);


        private void formSearch_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;  // _dragging is your variable flag
            _start_point = new Point(e.X, e.Y);
        }

        private void formSearch_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void formSearch_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
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
            checkbTemp.BringToFront();
            checkbEtml.BringToFront();
            controller.LoadTempData(ref fileListTemp, PATH);
            initialiseComboBox();
            //actualiseData();
            //Affiche tout les fichiers stockés


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
            controller.actualiseData(PATH, fileListTemp, ref sortedListFileTemp);
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
            clearListViewEtml();
            clearListViewTemp();

            if (txbInputEtml.Text != "")
            {

                if (checkbTemp.Checked)
                {
                    //Ajout d'une ligne de titre 
                    ListViewItem lviTitleTemp = new ListViewItem("Fichiers du temp");
                    lviTitleTemp.Font = new Font(lviTitleTemp.Font, FontStyle.Bold); //met la ligne en gras
                    addListViewItem(lviTitleTemp, 0);

                    //Ajout d'une ligne de titre pour l'onglet detail
                    ListViewItem lviTitleTempDetails = new ListViewItem("Details des fichiers du temp");
                    lviTitleTempDetails.Font = new Font(lviTitleTempDetails.Font, FontStyle.Bold); //met la ligne en gras
                    addListViewItem(lviTitleTempDetails, 2);

                    controller.checkSearchTemp(txbInputEtml.Text, fileListTemp, ref sortedListFileTemp, PATH);
                }
                if (checkbEtml.Checked)
                {

                    //Ajout d'une ligne de titre 
                    ListViewItem lviTitleEtml = new ListViewItem("Etml");
                    lviTitleEtml.Font = new Font(lviTitleEtml.Font, FontStyle.Bold); //met la ligne en gras
                    addListViewItem(lviTitleEtml, 0);

                    controller.searchUrlMatching(txbInputEtml.Text);
                }
            }

        }

        private void btnSearchTemp_Click(object sender, EventArgs e)
        {
            listViewResultTemp.Items.Clear();

        }

        public void clearListViewTemp()
        {
            listViewResultTemp.Items.Clear();
        }
        public void clearListViewEtml()
        {
            listViewResultEtml.Items.Clear();
        }
        public void clearListViewEducanet()
        {
            listViewResultEducanet.Items.Clear();
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
                        manualActualisation.Hide();
                        MessageBox.Show("Veuillez choisir une date");
                    }
                    else
                    {
                        setDateXML(listCmb[cmbActualisation.SelectedIndex]);
                        startService();

                    }
                    //Lance ou relance le service
                    //service = new actualisationService(this);
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
            //Démarre un service
            service = new serviceActualisation(this);
            ServiceBase.Run(service);
            /*
            ServiceController sc = new ServiceController(SERVICENAME);

            if (sc.Status == ServiceControllerStatus.Running)
                sc.Refresh();

            if (sc.Status == ServiceControllerStatus.Stopped)
                sc.Start();*/
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

        private void listViewResultEtml_DoubleClick(object sender, EventArgs e)
        {
            if (listViewResultEtml.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewResultEtml.SelectedItems[0];
                try
                {
                    //MessageBox.Show(item.SubItems[1].Text + "\\" + item.SubItems[0].Text);
                    Process.Start(sortedListFileTemp[listViewResultEtml.SelectedItems[0].Index].Directory + @"\" + sortedListFileTemp[listViewResultEtml.SelectedItems[0].Index].Name);
                }
                catch
                {
                    try
                    {
                        Process.Start(item.SubItems[1].Text);
                    }
                    catch
                    {
                        MessageBox.Show("Echec de l'ouverture du fichier, il à peut être été supprimé ou modifié");
                    }
                }
            }
        }
        
    }

}
