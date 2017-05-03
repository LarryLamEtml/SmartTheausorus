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

        public void checkSearchTemp(string _input, List<SmartThesaurusLibrary.File> _fileListTemp, ref List<SmartThesaurusLibrary.File> _sortedListFileTemp, string path)
        {
            List<ListViewItem> listLviDetails = new List<ListViewItem>();
            List<ListViewItem> listLvi = new List<ListViewItem>();

            try
            {
                _sortedListFileTemp.Clear();
                Regex regexCondition = new Regex(@"");


                foreach (SmartThesaurusLibrary.File fi in _fileListTemp)
                {
                    if (_input != "")
                    {
                        regexCondition = new Regex(@".*(" + Regex.Escape(_input) + @").*");
                        Match match = regexCondition.Match(fi.Name);
                        if (match.Success)
                        {
                            ListViewItem lviDetails = new ListViewItem(fi.Name);
                            lviDetails.SubItems.Add((fi.Size));
                            lviDetails.SubItems.Add(fi.LastWriteTime.ToString());
                            lviDetails.SubItems.Add(fi.Directory.ToString());
                            listLviDetails.Add(lviDetails);

                            ListViewItem lvi = new ListViewItem(fi.Name);
                            lvi.SubItems.Add((fi.Size));
                            lvi.SubItems.Add(fi.Directory.ToString());
                            listLvi.Add(lvi);
                            _sortedListFileTemp.Add(fi);
                        }
                    }
                    else
                    {
                        ListViewItem lviDetails = new ListViewItem(fi.Name);
                        lviDetails.SubItems.Add((fi.Size));
                        lviDetails.SubItems.Add(fi.LastWriteTime.ToString());
                        lviDetails.SubItems.Add(fi.Directory.ToString());
                        listLviDetails.Add(lviDetails);

                        ListViewItem lvi = new ListViewItem(fi.Name);
                        lvi.SubItems.Add((fi.Size));
                        lvi.SubItems.Add(fi.Directory.ToString());
                        listLvi.Add(lvi);
                        _sortedListFileTemp.Add(fi);


                    }

                }
                view.setProgressBar(listLvi.Count);
                addListViewItem(listLviDetails, 2);
                view.resetProgressBarValue();
                addListViewItem(listLvi, 0);
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
        public void actualiseData(string _PATH, List<SmartThesaurusLibrary.File> _fileListTemp, ref List<SmartThesaurusLibrary.File> _sortedListFileTemp)
        {
            String[] allTempFiles = Directory.GetFiles(_PATH, "*.*", SearchOption.AllDirectories);
            String[] allEtmlFiles = new String[1];
            String[] allEducanetFiles = new String[1];

            //Dit au modèle (librairie) d'actualiser les données
            SmartThesaurusLibrary.XML.actualiseDataTemp(allTempFiles, _fileListTemp);
            LoadTempData(ref _fileListTemp, _PATH);
            SmartThesaurusLibrary.XML.actualiseDataEtml();
            view.searchEtml();

            //SmartThesaurusLibrary.XML.actualiseDataEducanet();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void LoadTempData(ref List<SmartThesaurusLibrary.File> _fileListTemp, string path)
        {
            _fileListTemp = SmartThesaurusLibrary.XML.loadTempData(path);

            if (_fileListTemp == null)//Si le fichier n'a pas pu être ouvert
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

        //public void WriteEtmlData()
        //{
        //    List<string> listUrls = new List<string>();
        //    List<string> listContent = new List<string>();

        //    foreach (string url in web.getAllUrls())
        //    {
        //        listUrls.Add(url);
        //        using (System.Net.WebClient webClient = new System.Net.WebClient())
        //            listContent.Add(webClient.DownloadString(url));
        //    }
        //    SmartThesaurusLibrary.XML.etmlDataToXML(listUrls, listContent);
        //}

        public void                                         searchUrlMatching(string _text)
        {

            Dictionary<string, string> listUrls = new Dictionary<string, string>();
            List<ListViewItem> listLvi = new List<ListViewItem>();
            listUrls = readEtmlData();
            int count = 0;
            if (listUrls != null)
            {

                foreach (var url in listUrls)
                {
                    //string url = listUrls.Keys.Take(1);
                    if (containsWordEtml(url.Value, _text))
                    {
                        string[] name = url.Key.Split('/');
                        if (name[name.Count() - 1] == "")
                        {
                            name[name.Count() - 1] = "etml.ch";
                        }
                        ListViewItem lvi = new ListViewItem(name[name.Count() - 1]);
                        lvi.SubItems.Add(url.Key);
                        lvi.SubItems.Add("-");
                        //_fileListEtml.Add(new SmartThesaurusLibrary.Url(url.Key,url.Value));
                        listLvi.Add(lvi);
                    }
                    count++;
                }
                view.setProgressBar(listLvi.Count);
                addListViewItem(listLvi, 0);
            }
            else
            {
                MessageBox.Show("Aucun résultat trouvé");
            }
        }

        public bool containsWordEtml(string content, string word)
        {
            /*string[] tableWords = content.Split(' ');
            bool containsWord = tableWords.Contains(word);*/
            //Regex regexCondition = new Regex(@".*(" + Regex.Escape(word) + @").*");
            if(content.ToLower().Contains(word.ToLower()))
            {
                return true;
            }else
            {
                return false;
            }/*
            Match match = regexCondition.Match(content);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }*/
        }
        public void addListViewItem(List<ListViewItem> listLvi, int index)
        {
            view.addListViewItem(listLvi, index);
        }
        public Dictionary<string, string> readEtmlData()
        {
            Dictionary<string, string> listUrls = new Dictionary<string, string>();
            listUrls = SmartThesaurusLibrary.XML.readEtmlData();
            if (listUrls != null)
            {
                return listUrls;
            }
            else
            {
                //Afficher un message d'erreur
                MessageBox.Show("Il n'y aucun documents enregistré dans la base de donnée, veuillez la mettre à jour");
                return null;
            }
        }
        //public void checkSearchEtml(List<string> listUrls, List<string> listContent)
        //{
        //    try
        //    {
        //        view.clearListViewEtml();
        //        _sortedListFileTemp.Clear();
        //        Regex regexCondition = new Regex(@"");

        //        if (_input != "")
        //        {
        //            regexCondition = new Regex(@".*(" + Regex.Escape(_input) + @").*");
        //        }

        //        foreach (SmartThesaurusLibrary.File fi in _fileListTemp)
        //        {
        //            if (_input != "")
        //            {
        //                Match match = regexCondition.Match(fi.Name);
        //                if (match.Success)
        //                {
        //                    ListViewItem lvi = new ListViewItem(fi.Name);
        //                    lvi.SubItems.Add((fi.Size));
        //                    lvi.SubItems.Add(fi.LastWriteTime.ToString());
        //                    lvi.SubItems.Add(fi.Directory.ToString());
        //                    addListViewItem(lvi, 2);
        //                    _sortedListFileTemp.Add(fi);
        //                }
        //            }
        //            else
        //            {
        //                ListViewItem lvi = new ListViewItem(fi.Name);
        //                lvi.SubItems.Add((fi.Size));
        //                lvi.SubItems.Add(fi.LastWriteTime.ToString());
        //                lvi.SubItems.Add(fi.Directory.ToString());
        //                addListViewItem(lvi, 2);
        //                _sortedListFileTemp.Add(fi);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Avertissement, le répertoire du chemin " + path + " n'à pas été trouvé " + ex);
        //    }
        //}
    }
}
