using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SmartThesaurus
{
    public class Controller
    {
        private formSearch view;
        private ManualDateDialog manualActualisation;


        public Controller(formSearch _view, ManualDateDialog _manualActualisation)
        {
            view = _view;
            manualActualisation = _manualActualisation;
        }

        public void checkSearch(string _input, List<SmartThesaurusLibrary.File> _fileListTemp, List<SmartThesaurusLibrary.File> _sortedListFileTemp, string path)
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
        public void actualiseData(string _PATH, List<SmartThesaurusLibrary.File> _fileList, int index)
        {
            String[] allTempFiles = Directory.GetFiles(_PATH, "*.*", SearchOption.AllDirectories);
            String[] allEtmlFiles = new String[1];
            String[] allEducanetFiles = new String[1];
            //Dit au model de changer les données
            SmartThesaurusLibrary.XML.tempDataToXML(SmartThesaurusLibrary.XML.actualiseData(allTempFiles, _fileList, index));
            /*SmartThesaurusLibrary.XML.tempDataToXML(SmartThesaurusLibrary.XML.actualiseData(allEtmlFiles, _fileList, index));
            SmartThesaurusLibrary.XML.tempDataToXML(SmartThesaurusLibrary.XML.actualiseData(allEducanetFiles, _fileList, index));*/
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
