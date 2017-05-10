///ETML
///Auteur : lamho
///Date : 05.04.2017
///Description: Form pour entrer la date manuellement
///
using System;
using System.Windows.Forms;

namespace SmartThesaurus
{
    /// <summary>
    /// Popup pour entrer la date manuellement
    /// </summary>
    public partial class ManualDateDialog : Form
    {
        private string date = "";//date pour la maj
        formSearch form;//Reférence sur la view
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="form"></param>
        public ManualDateDialog(formSearch form)
        {
            InitializeComponent();
            dtp.MinDate = DateTime.Today;//Interdit de choisir une date ultérieure à ajd
            this.form = form;//reference
        }

        /// <summary>
        /// Définit la date de mise à jour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendManualDate_Click(object sender, EventArgs e)
        {
            date = dtp.Value.ToShortDateString();//Converti en string dans le bon format
            form.setDateXML("Manuel");//Ecrit la date dans le fichier xml
            this.Close();//Ferme la form
        }
        /// <summary>
        /// Donne la date
        /// </summary>
        /// <returns>date</returns>
        public string getDate()
        {
            return date;
        }
    }
}
