///ETML
///Auteur : lamho
///Date : 11.01.2017
///Description: Vue du programme. S'occupe de toute les requêtes interface-utilisateur
///
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartThesaurus
{
    /// <summary>
    /// Vue du programme. S'occupe de toute les requêtes interface-utilisateur
    /// </summary>
    public partial class formSearch : Form
    {
        //Classes
        Controller controller;
        ManualDateDialog manualActualisation;
        EducanetLogin login;

        //Constantes
        const string PATH = @"K:\INF\eleves\temp";
        const string modeActualisation = "Actualisation";
        const string modeDay = "Chaque jour";
        const string modeHour = "Chaque heure";
        const string modeCustom = "Cron";
        const string modeManual = "Manuel";
        const string modeNow = "Maintenant";

        //Pour la souris
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);

        //Listes
        List<string> listCmb = new List<string>();
        List<SmartThesaurusLibrary.File> fileListEducanet = new List<SmartThesaurusLibrary.File>();
        List<SmartThesaurusLibrary.File> fileListTemp = new List<SmartThesaurusLibrary.File>();
        List<SmartThesaurusLibrary.File> sortedListFileTemp = new List<SmartThesaurusLibrary.File>();

        /// <summary>
        /// Constructeur
        /// </summary>
        public formSearch()
        {
            IsProcessOpen("SmartThesaurus");
        }

        /// <summary>
        /// Vérifie si l'application est pas déjà lancée. Démarre une nouvelle instance
        /// </summary>
        /// <param name="name"></param>
        public void IsProcessOpen(string name)
        {
            List<Process> listProcess = new List<Process>();//Liste des processus
            foreach (Process clsProcess in Process.GetProcesses())//Tous les processus en execution
            {
                if (clsProcess.ProcessName.Contains(name))//S'ils contiennent le mot "SmartThesaurus"
                {
                    listProcess.Add(clsProcess);//Les ajouter dans la liste

                }
            }

            foreach (Process process in listProcess)//Parcours la liste
            {
                if (process.Id != Process.GetCurrentProcess().Id)//Supprime tous les processus sauf l'actuel
                {
                    process.Kill();//Supprime le processus
                }
            }
            InitializeComponent();//Initalise les composants
            InitialiseForm();//Initalise les controls
            startService();//Démarre un service en arrière plan

        }
        /// <summary>
        /// Permet le focus sur le bouton exit lors du clique sur la touche escape
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.AcceptButton = btnClose;//Bouton avec le focus (enter pour cliquer)
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Lors d'un click de souris
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formSearch_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;  // _dragging variable flag
            _start_point = new Point(e.X, e.Y);//change les coordonnées
        }

        /// <summary>
        /// Lorsque la souris n'est plus pressée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formSearch_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false; // _dragging variable flag
        }

        /// <summary>
        /// Lors de mouvement de souris. (Obligé de faire tout cela car en mettant le style sans border la form de peut être déplacée)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formSearch_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)//Si la souris est pressée
            {
                //Déplace le form
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        /// <summary>
        /// Initialise les controls
        /// </summary>
        public void InitialiseForm()
        {
            manualActualisation = new ManualDateDialog(this);
            controller = new Controller(this, manualActualisation);
            login = new EducanetLogin(this);
            initialiseComboBox();// Initialise la liste déroulante
            this.AcceptButton = btnSearchEtml;//Bouton avec le focus (enter pour cliquer)
            //ramene le combobox au dessus (visuel)
            listViewResultTemp.BringToFront();
            cmbActualisation.BringToFront();
            checkbTemp.BringToFront();
            checkbEtml.BringToFront();
            controller.LoadTempData(ref fileListTemp, PATH);
        }
        /// <summary>
        /// Se connecte a educanet2
        /// </summary>
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
            listCmb.Add(modeActualisation);
            listCmb.Add(modeDay);
            listCmb.Add(modeHour);
            listCmb.Add(modeCustom);
            listCmb.Add(modeManual);
            listCmb.Add(modeNow);
            //Pour chaque élément de la liste, l'ajouter à la ComboBox
            foreach (string choice in listCmb)
            {
                cmbActualisation.Items.Add(choice);
            }
            // cmbActualisation.SelectedIndex = 1;

        }


        /// <summary>
        /// Actualise les données du fichier XML
        /// </summary>
        /// <param name="index"></param>
        public void actualiseData()
        {
            controller.actualiseData(PATH, fileListTemp, ref sortedListFileTemp);
            MessageBox.Show("Les données ont été mises à jour");
            /*controller.actualiseData(PATH, fileListEducanet);
            controller.actualiseData(PATH, fileListEtml);*/
        }

        /// <summary>
        /// Ajoute l'item à la listView et redimensionne celle-ci
        /// </summary>
        /// <param name="lvi"></param>
        public void addListViewItem(List<ListViewItem> listLvi, int index)
        {
            switch (index)//Switch avec le chiffre donné
            {
                case 0:

                    this.listViewResultEtml.Invoke((MethodInvoker)delegate
                    {
                        suspendListView();
                        foreach (ListViewItem lvi in listLvi)
                        {
                            listViewResultEtml.Items.Add(lvi);//Ajoute l'item à la listView
                            incrementProgressBar();
                        }
                        listViewResultEtml.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);//Ajuste la taille de la colonne en fonction des données
                        listViewResultEtml.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//Définis la taille minimum = taille du header
                        enableListView();
                    });
                    break;
                case 1:
                    this.listViewResultEducanet.Invoke((MethodInvoker)delegate
                    {
                        suspendListView();
                        foreach (ListViewItem lvi in listLvi)
                        {
                            listViewResultEducanet.Items.Add(lvi);//Ajoute l'item à la listView
                            incrementProgressBar();
                        }
                        listViewResultEducanet.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);//Ajuste la taille de la colonne en fonction des données
                        listViewResultEducanet.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//Définis la taille minimum = taille du header
                        enableListView();
                    });
                    break;
                case 2:

                    this.listViewResultTemp.Invoke((MethodInvoker)delegate
                    {
                        suspendListView();
                        foreach (ListViewItem lvi in listLvi)
                        {
                            listViewResultTemp.Items.Add(lvi);//Ajoute l'item à la listView
                            incrementProgressBar();
                        }
                        listViewResultTemp.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);//Ajuste la taille de la colonne en fonction des données
                        listViewResultTemp.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//Définis la taille minimum = taille du header
                        enableListView();
                    });
                    break;
            }
        }
        /// <summary>
        /// Ajoute l'item à la listView et redimensionne celle-ci
        /// </summary>
        /// <param name="lvi"></param>
        public void addSingleListViewItem(ListViewItem lvi, int index)
        {
            switch (index)
            {
                case 0:

                    this.listViewResultEtml.Invoke((MethodInvoker)delegate
                    {
                        listViewResultEtml.Items.Add(lvi);//Ajoute l'item à la listView
                        listViewResultEtml.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);//Ajuste la taille de la colonne en fonction des données
                        listViewResultEtml.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//Définis la taille minimum = taille du header

                    });
                    break;
                case 1:
                    this.listViewResultEducanet.Invoke((MethodInvoker)delegate
                    {
                        listViewResultEducanet.Items.Add(lvi);//Ajoute l'item à la listView
                        listViewResultEducanet.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);//Ajuste la taille de la colonne en fonction des données
                        listViewResultEducanet.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//Définis la taille minimum = taille du header

                    });
                    break;
                case 2:

                    this.listViewResultTemp.Invoke((MethodInvoker)delegate
                    {
                        listViewResultTemp.Items.Add(lvi);//Ajoute l'item à la listView
                        listViewResultTemp.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);//Ajuste la taille de la colonne en fonction des données
                        listViewResultTemp.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//Définis la taille minimum = taille du header

                    });
                    break;
                default:
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
            this.Hide();

            //Close();
        }

        /// <summary>
        /// Lors du clique sur le bouton btnSearchEtml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchEtml_Click(object sender, EventArgs e)
        {
            searchEtml();//Effectue la recherche
        }

        /// <summary>
        /// Effectue la recherche avec le/s mot/s donné/s dans la barre de recherche
        /// </summary>
        public void searchEtml()
        {
            //Vide les listviews
            clearListViewEtml();
            clearListViewTemp();

            if (txbInputEtml.Text != "")//Si la barre de recherche n'est pas vide
            {

                if (checkbTemp.Checked)//Si la case temp est cochée
                {
                    resetProgressBarValue();//Reset la barre de progression

                    //Ajout d'une ligne de titre 
                    ListViewItem lviTitleTemp = new ListViewItem("Fichiers du temp");
                    lviTitleTemp.Font = new Font(lviTitleTemp.Font, FontStyle.Bold); //met la ligne en gras
                    addSingleListViewItem(lviTitleTemp, 0);//L'ajoute dans la LV

                    //Ajout d'une ligne de titre pour l'onglet detail
                    ListViewItem lviTitleTempDetails = new ListViewItem("Details des fichiers du temp");
                    lviTitleTempDetails.Font = new Font(lviTitleTempDetails.Font, FontStyle.Bold); //met la ligne en gras
                    addSingleListViewItem(lviTitleTempDetails, 2);//L'ajoute dans la LV

                    controller.checkSearchTemp(txbInputEtml.Text, fileListTemp, ref sortedListFileTemp, PATH);//Effectue la recherche avec le mot donné (-> controller)
                }
                if (checkbEtml.Checked)//Si la case temp est cochée
                {
                    resetProgressBarValue();//Reset la barre de progression

                    //Ajout d'une ligne de titre 
                    ListViewItem lviTitleEtml = new ListViewItem("Etml");
                    lviTitleEtml.Font = new Font(lviTitleEtml.Font, FontStyle.Bold); //met la ligne en gras
                    addSingleListViewItem(lviTitleEtml, 0);//L'ajoute dans la LV

                    controller.searchUrlMatching(txbInputEtml.Text);//Regarde dans les données s'ils contiennent le mot clé
                }
            }
        }
        /// <summary>
        /// Reset la barre de progression
        /// </summary>
        public void resetProgressBarValue()
        {
            this.pbLoadingTime.Invoke((MethodInvoker)delegate
            {
                pbLoadingTime.Value = 0;// Reset la barre de progression
            });

        }

        //Définit la taille de la barre de progression
        public void setProgressBar(int size)
        {
            this.pbLoadingTime.Invoke((MethodInvoker)delegate
            {
                pbLoadingTime.Maximum = size;//Taille max
                pbLoadingTime.Minimum = 0;//taille min
                pbLoadingTime.Step = 1;
            });

        }
        /// <summary>
        /// Incrémente la barre de progression
        /// </summary>
        public void incrementProgressBar()
        {
            pbLoadingTime.Value += 1;
        }
        /// <summary>
        /// Désactive la listview (utile lors de changement de données en multi-thread)
        /// </summary>
        public void suspendListView()
        {
            //Permet la modification de l'UI avec un autre thread
            this.listViewResultTemp.Invoke((MethodInvoker)delegate
            {
                listViewResultTemp.Enabled = false;//Désactive la listview 
            });
            this.listViewResultEtml.Invoke((MethodInvoker)delegate
            {
                listViewResultEtml.Enabled = false;//Désactive la listview 
            });


        }

        /// <summary>
        /// Réactive la listview
        /// </summary>
        public void enableListView()
        {
            this.listViewResultTemp.Invoke((MethodInvoker)delegate
            {
                listViewResultTemp.Enabled = true;
            });
            this.listViewResultEtml.Invoke((MethodInvoker)delegate
            {
                listViewResultEtml.Enabled = true;
            });
        }
        /// <summary>
        /// Vide la listviewTemp
        /// </summary>
        public void clearListViewTemp()
        {
            this.listViewResultTemp.Invoke((MethodInvoker)delegate
            {
                listViewResultTemp.Items.Clear();//Vide la listview
            });
        }
        /// <summary>        
        /// Vide la listviewEtml
        /// </summary>
        public void clearListViewEtml()
        {
            this.listViewResultEtml.Invoke((MethodInvoker)delegate
            {
                listViewResultEtml.Items.Clear();//Vide la listview

            });
        }
        /// <summary>
        /// Vide la listviewEducanet
        /// </summary>
        public void clearListViewEducanet()
        {
            this.listViewResultEducanet.Invoke((MethodInvoker)delegate
            {
                listViewResultEducanet.Items.Clear();//Vide la listview
            });
        }

        /// <summary>
        /// Lorsqu'on double clique sur le nom d'un fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewResultTemp_ItemActivate(object sender, EventArgs e)
        {
            int selectedFileIndex = listViewResultTemp.SelectedItems[0].Index;//stocke l'index de l'item selectionné
            //MessageBox.Show(sortedListFileInfo[selectedFileIndex].DirectoryName + @"\" + sortedListFileInfo[selectedFileIndex].Name); //Test
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
                cmbActualisation.SelectedIndex = 1;//Change l'index à 0
                return;
            }
            //Switch en fonction de l'option du combobox choisie
            switch (cmbActualisation.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    setDateXML(listCmb[cmbActualisation.SelectedIndex]);//Définit la date avec l'index de l'option selectionnée
                    startService();//Relance le service d'actualisation en arrière plan
                    break;
                case 2:
                    setDateXML(listCmb[cmbActualisation.SelectedIndex]);//Définit la date avec l'index de l'option selectionnée
                    startService();//Relance le service d'actualisation en arrière plan
                    break;
                case 3:
                    setDateXML(listCmb[cmbActualisation.SelectedIndex]);//Définit la date avec l'index de l'option selectionnée
                    useCron();//Utilise la methode cron
                    break;
                case 4:
                    manualActualisation.ShowDialog();//Affiche le popup de selection de date
                    if (manualActualisation.getDate() == "")//Si la date choisie n'est pas ""
                    {
                        manualActualisation.Hide();//Cache le popup
                        MessageBox.Show("Veuillez choisir une date");//Affiche un message d'erreur
                    }
                    else//Sinon
                    {
                        setDateXML(listCmb[cmbActualisation.SelectedIndex]);//Définit la date avec l'index de l'option selectionnée
                        startService();//Relance le service d'actualisation en arrière plan
                    }
                    break;
                case 5:
                    setDateXML(listCmb[cmbActualisation.SelectedIndex]);//Définit la date avec l'index de l'option selectionnée
                    DialogResult dialogResult = MessageBox.Show("Voulez-vous actualiser les données ?", "Mise à jour de la base de données", MessageBoxButtons.YesNo);//Popup avec choix
                    if (dialogResult == DialogResult.Yes)//Si l'utilisateur accepte
                    {
                        Task.Factory.StartNew(() => actualiseData());//Actualise les données en arrière-plan
                    }
                    cmbActualisation.SelectedIndex = 1;//Change l'option choisie de la liste déroulante à 1
                    break;
            }

        }

        /// <summary>
        /// Définit le mode et la date d'actualisation dans un fichier XML 
        /// </summary>
        /// <param name="text"></param>
        public void setDateXML(string text)
        {
            controller.setDateXML(text);//Execution sur le controleur
        }

        /// <summary>
        /// Démarre ou redémarre le "service" d'actualisation en arrière plan
        /// </summary>
        private void startService()
        {
            BackgroundManager service;
            Task.Factory.StartNew(() => service = new BackgroundManager(this));
        }

        /// <summary>
        /// Définit le mode d'actualisation suivant le string donné
        /// </summary>
        /// <param name="mode"></param>
        public void setActualisationMode(string mode)
        {
            switch (mode)//Switch avec les options 
            {
                case modeDay:
                    this.cmbActualisation.Invoke((MethodInvoker)delegate
                    {
                        cmbActualisation.SelectedIndex = 1;//Change l'option selectionné
                    });
                    break;
                case modeHour:
                    this.cmbActualisation.Invoke((MethodInvoker)delegate
                    {
                        cmbActualisation.SelectedIndex = 2;//Change l'option selectionné
                    });
                    break;
                case modeCustom:
                    this.cmbActualisation.Invoke((MethodInvoker)delegate
                    {
                        cmbActualisation.SelectedIndex = 3;//Change l'option selectionné
                    });
                    break;
                case modeManual:
                    this.cmbActualisation.Invoke((MethodInvoker)delegate
                    {
                        cmbActualisation.SelectedIndex = 4;//Change l'option selectionné
                    });
                    break;
                default:
                   // MessageBox.Show("Defaut");
                    break;
            }

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

        /// <summary>
        /// Lors d'un double click sur un item, l'ouvrir ou aller sur son url
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewResultEtml_DoubleClick(object sender, EventArgs e)
        {
            if (listViewResultEtml.SelectedItems.Count > 0)//S'il y a au moins 1 item cliqué
            {
                ListViewItem item = listViewResultEtml.SelectedItems[0];//Stocke l'item cliqué
                try
                {
                    Process.Start(sortedListFileTemp[listViewResultEtml.SelectedItems[0].Index-1].Directory + @"\" + sortedListFileTemp[listViewResultEtml.SelectedItems[0].Index-1].Name);//Essaie d'ouvrir le fichier
                }
                catch (Exception ex)
                {
                    try
                    {
                        Process.Start(item.SubItems[1].Text);//Sinon, essai d'ouvrir sa page web
                    }
                    catch
                    {
                        MessageBox.Show("Echec de l'ouverture du fichier, il à peut être été supprimé ou modifié");//Sinon affiche un message d'erreur
                    }
                }
            }
        }

        /// <summary>
        /// Lors d'un changement de tab, afficher ou pas la comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbCMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TbCMain.SelectedIndex == 2)//Si le tab est sur la page détail
            {
                //Cacher la combobox
                cmbActualisation.Hide();
                lblBackColor.Hide();
            }
            else//Sinon
            {
                //L'afficher
                cmbActualisation.Show();
                lblBackColor.Show();
            }
        }
    }

}
