using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bugfix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            searchUrlMatching("sdfswdf");
        }
        public void searchUrlMatching(string _text)
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
            Regex regexCondition = new Regex(@".*(" + Regex.Escape(word) + @").*");
            Match match = regexCondition.Match(content);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
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
    }
}
