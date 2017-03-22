using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SmartThesaurus
{
    public class Controller
    {
        private formSearch view;
        private ManualDateDialog manualActualisation;

        const string fileNameEtml = "etmlData.xml";
        const string fileNameEducanet = "educanetData.xml";
        const string fileNameTemp = "tempData.xml";

        public Controller(formSearch _view, ManualDateDialog _manualActualisation)
        {
            view = _view;
            manualActualisation = _manualActualisation;
        }

        public void checkSearchTemp(string _input, List<SmartThesaurusLibrary.File> _fileListTemp, List<SmartThesaurusLibrary.File> _sortedListFileTemp, string path)
        {
            try
            {
                _sortedListFileTemp.Clear();
                Regex regexCondition = new Regex(@"");

                if (_input != "")
                {
                    regexCondition = new Regex(@".*(" + Regex.Escape(_input) + @").*");
                }

                foreach (SmartThesaurusLibrary.File fi in _fileListTemp)
                {
                    if (_input != "")
                    {
                        Match match = regexCondition.Match(fi.Name);
                        if (match.Success)
                        {
                            ListViewItem lvi = new ListViewItem(fi.Name);
                            lvi.SubItems.Add((fi.Size));
                            lvi.SubItems.Add(fi.LastWriteTime.ToString());
                            lvi.SubItems.Add(fi.Directory.ToString());
                            view.addListViewItem(lvi);
                            _sortedListFileTemp.Add(fi);
                        }
                    }
                    else
                    {
                        ListViewItem lvi = new ListViewItem(fi.Name);
                        lvi.SubItems.Add((fi.Size));
                        lvi.SubItems.Add(fi.LastWriteTime.ToString());
                        lvi.SubItems.Add(fi.Directory.ToString());
                        view.addListViewItem(lvi);
                        _sortedListFileTemp.Add(fi);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Avertissement, le répertoire du chemin " + path + " n'à pas été trouvé " + ex);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void actualiseData(string _PATH, List<SmartThesaurusLibrary.File> _fileListTemp)
        {
            String[] allTempFiles = Directory.GetFiles(_PATH, "*.*", SearchOption.AllDirectories);
            String[] allEtmlFiles = new String[1]; 
            String[] allEducanetFiles = new String[1];

            //Dit au modèle (librairie) d'actualiser les données
            SmartThesaurusLibrary.XML.actualiseDataTemp(allTempFiles, _fileListTemp);
            readXMLTemp(_fileListTemp);

            //SmartThesaurusLibrary.XML.actualiseDataEtml();
            //SmartThesaurusLibrary.XML.actualiseDataEducanet();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void readXMLTemp(List<SmartThesaurusLibrary.File> _fileListTemp)
        {
            try
            {
                //Vide la liste
                _fileListTemp.Clear();

                //Lit le fichier fileToRead
                XDocument xmlDoc = XDocument.Load(fileNameTemp);
                //Lit et stocke les données
                var files = from file in xmlDoc.Descendants("File")
                            select new
                            {
                                id = file.Element("id").Value,
                                name = file.Element("name").Value,
                                size = file.Element("size").Value,
                                lastModified = file.Element("lastModified").Value,
                                directory = file.Element("directory").Value,
                            };
                //Lis chaque fichier dans le fichier XML et lajoute dans la liste des fichiers (local)
                foreach (var file in files)
                {
                    SmartThesaurusLibrary.File newFile = new SmartThesaurusLibrary.File(Convert.ToInt32(file.id), file.name, file.size, Convert.ToDateTime(file.lastModified), file.directory);
                    _fileListTemp.Add(newFile);
                }
            }
            catch (Exception ex) //Si le fichier n'a pas pu être ouvert
            {
                //Afficher un message d'erreur
                MessageBox.Show("Il n'y aucun documents enregistré dans la base de donnée, veuillez la mettre à jour");
            }
        }
        /// <summary>
        /// Exécute la fonction setDateXML de la librairie, elle permet de définir le mode et la date d'actualisation dans un fichier XML 
        /// </summary>
        /// <param name="text"></param>
        public void setDateXML(string text)
        {
            SmartThesaurusLibrary.XML.setDateXML(text, DateTime.Today.DayOfYear.ToString(), DateTime.Now.Hour.ToString(), manualActualisation.getDate());
        }
    }
}
