using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace SmartThesaurusLibrary
{
    public static class XML
    {

        static XmlWriterSettings settings = new XmlWriterSettings();
        static XmlWriter writer;
        const string fileNameEtml = "etmlData.xml";
        const string fileNameEducanet = "educanetData.xml";
        const string fileNameTemp = "tempData.xml";
        const string fileNameDate = "actualisationDate.xml";

        public static void setDateXML(string actualisation, string day, string hour, string manualDate)
        {
            writer = XmlWriter.Create(fileNameDate, settings);
            settings.Indent = true;
            settings.IndentChars = ("\t");
            settings.OmitXmlDeclaration = true;

            writer.WriteStartDocument();
            writer.WriteStartElement("Date");
            writer.WriteElementString("actualisationMode", actualisation);
            writer.WriteElementString("day", day);
            writer.WriteElementString("hour", hour);
            writer.WriteElementString("manualDate", manualDate);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Dispose();
            writer.Close();
        }

        public static void tempDataToXML(List<File> _fileListTemp)
        {
            XmlWriter writer = XmlWriter.Create(fileNameTemp, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Files");

            foreach (File f in _fileListTemp)
            {
                writer.WriteStartElement("File");

                writer.WriteElementString("id", f.idFile.ToString());
                writer.WriteElementString("name", f.Name);
                writer.WriteElementString("size", f.Size);
                writer.WriteElementString("lastModified", f.LastWriteTime.ToString());
                writer.WriteElementString("directory", f.Directory);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Dispose();
            writer.Close();
        }
        public static List<File> loadTempData(string path)
        {
            try
            {
                List<File> _fileListTemp = new List<File>();
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
                    File newFile = new File(Convert.ToInt32(file.id), file.name, file.size, Convert.ToDateTime(file.lastModified), file.directory);
                    _fileListTemp.Add(newFile);
                }

                return _fileListTemp;
            }
            catch (Exception ex) //Si le fichier n'a pas pu être ouvert
            {
                return null;
            }
        }
        public static Dictionary<string, string> readEtmlData()
        {
            Dictionary<string, string> listUrls = new Dictionary<string, string>();

            try
            {
                //Vide la liste

                //Lit le fichier fileToRead
                XDocument xmlDoc = XDocument.Load(fileNameEtml);
                //Lit et stocke les données
                var Pages = from Page in xmlDoc.Descendants("Page")
                            select new
                            {
                                url = Page.Element("url").Value,
                                content = Page.Element("content").Value,
                            };
                //Lis chaque fichier dans le fichier XML et lajoute dans la liste des fichiers (local)
                foreach (var Page in Pages)
                {
                    listUrls.Add(Page.url, Page.content);
                }
                return listUrls;
                //checkSearchEtml(listUrls, listContent);
            }
            catch (Exception ex) //Si le fichier n'a pas pu être ouvert
            {
                //Afficher un message d'erreur
                //MessageBox.Show("Il n'y aucun documents enregistré dans la base de donnée, veuillez la mettre à jour");
                //throw new InvalidOperationException();
                return null;
            }
        }

        public static void etmlDataToXML(Dictionary<string, string> listUrls)
        {
            XmlWriter writer = XmlWriter.Create(fileNameEtml, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Pages");

            foreach (var url in listUrls)
            {
                writer.WriteStartElement("Page");

                writer.WriteElementString("url", url.Key);
                writer.WriteElementString("content", url.Value);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Dispose();
            writer.Close();
        }

        public static void searchUrlMatching(string _text, List<string> _fileListEtml, string _url)
        {
            foreach (string s in WebPage.getAllUrls())
            {
                if (WebPage.searchOnWeb(_text, s) != "")
                {

                    /* ListViewItem lvi = new ListViewItem(s);
                     _fileListEtml.Add(s);
                     view.addListViewItem(lvi, 0);*/
                }
            }
        }
        public static void eduDataToXML()
        {

        }

        public static void actualiseDataTemp(String[] _allTempFiles, List<File> _fileListTemp)
        {

            //---------------------actualise temp info -----------------------------
            List<FileInfo> listFileinfoTemp = new List<FileInfo>();

            foreach (var file in _allTempFiles)
            {
                listFileinfoTemp.Add(new FileInfo(file));
            }
            int idCount = 0;
            _fileListTemp.Clear();

            foreach (FileInfo fi in listFileinfoTemp)
            {
                try {
                    File file = new File(idCount, fi.Name, Library.BytesToString(fi.Length), fi.LastWriteTime, fi.Directory.ToString());
                    _fileListTemp.Add(file);
                    idCount++;
                }catch
                {

                }
            }
            tempDataToXML(_fileListTemp);
        }

        public static void actualiseDataEtml()
        {
            Dictionary<string, string> listUrls = new Dictionary<string, string>();

            foreach (string url in WebPage.getAllUrls())
            {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                    listUrls.Add(url, webClient.DownloadString(url));
            }
            etmlDataToXML(listUrls);
        }

        public static void actualiseDataEducanet()
        {

        }
    }
}
