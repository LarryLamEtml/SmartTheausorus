using System;
using System.Windows.Forms;
///ETML
///Auteur : lamho
///Date : 03.05.2017
///Description: Programme par défaut
///
namespace SmartThesaurus
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new formSearch());

        }
    }
}
