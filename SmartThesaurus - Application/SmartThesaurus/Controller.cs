using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SmartThesaurus
{
    public class Controller
    {
        private formSearch view;
        List<SmartThesaurusLibrary.File> sortedListFile;

        public Controller(formSearch _view)
        {
            view = _view;
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

        public void actualiseData()
        {

        }
    }
}
