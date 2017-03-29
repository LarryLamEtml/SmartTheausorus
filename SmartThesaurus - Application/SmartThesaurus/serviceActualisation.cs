using System;
using System.Data;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Xml.Linq;

namespace SmartThesaurus
{
    public partial class serviceActualisation : ServiceBase
    {
        const string dayMode = ("Chaque jour");
        const string hourMode = ("Chaque heure");
        const string costumizedMode = ("Personnalisé");
        const string manualMode = ("Manuel");
        const string actualisationMode = ("actualisationMode");
        const string day = ("day");
        const string hour = ("hour");
        const string manualDate = ("manualDate");

        string xmlMode = "";
        string xmlDay = "";
        string xmlHour = "";
        string xmlManual = "";
        formSearch formsearch;

        public serviceActualisation(formSearch form)
        {
            InitializeComponent();
            formsearch = form;
            //OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            // TODO: ajoutez ici le code pour démarrer votre service.

            switch (readActualisationXML())
            {
                case dayMode:
                    actualiseDayMode();
                    break;
                case hourMode:
                    actualisehourMode();
                    break;
                case costumizedMode:
                    actualisecostumizedMode();
                    break;
                case manualMode:
                    actualiseManualMode();
                    break;
            }
        }

        public void actualiseDayMode()
        {
            while (true)
            {
                //Si nous somme le jours suivant
                if (Convert.ToInt32(DateTime.Now.DayOfYear) > Convert.ToInt32(xmlDay))
                {
                    formsearch.setDateXML(xmlMode);
                    formsearch.actualiseData();
                    Thread.Sleep(Convert.ToInt32(86390000));//Met en pause pendant 23h59min et 50 secondes
                }
                Thread.Sleep(60000);//Effectue la vérification chaque minutes
            }
        }
        public void actualisehourMode()
        {
            while (true)
            {
                if (Convert.ToInt32(DateTime.Now.Hour) > Convert.ToInt32(xmlHour))
                {
                    formsearch.setDateXML(xmlMode);
                    formsearch.actualiseData();
                    Thread.Sleep(Convert.ToInt32(3590000));//Met en pause pendant 59min et 50 secondes
                }
                Thread.Sleep(60000);//Effectue la vérification chaque minutes
            }

        }
        public void actualisecostumizedMode()
        {
            while (true)
            {

            }

        }
        public void actualiseManualMode()
        {
            while (true)
            {
                if ((DateTime.Now.ToShortDateString()) == (xmlManual))
                {
                    formsearch.setDateXML(xmlMode);
                    formsearch.actualiseData();
                    Thread.Sleep(Convert.ToInt32(3590000));//Met en pause pendant 59min et 50 secondes
                }
                Thread.Sleep(60000);//Effectue la vérification chaque minutes
            }

        }

        public string readActualisationXML()
        {
            try
            {
                XDocument xmlDoc = XDocument.Load("actualisationDate.xml");
                //Lis et stocke les infos du fichier xml
                var xmlInfos = from info in xmlDoc.Descendants("Date")
                               select new
                               {
                                   xmlMode = info.Element("actualisationMode").Value,
                                   xmlDay = info.Element("day").Value,
                                   xmlHour = info.Element("hour").Value,
                                   xmlManual = info.Element("manualDate").Value,
                               };
                //Parcours et récupères les informations lues dans le fichier xml
                foreach (var info in xmlInfos)
                {

                    xmlMode = info.xmlMode;
                    xmlDay = info.xmlDay;
                    xmlHour = info.xmlHour;
                    xmlManual = info.xmlManual;
                }
                return xmlMode;
                //XmlTextReader reader = new XmlTextReader("actualisationDate.xml");
                //while (reader.Read())
                //{
                //    switch (reader.NodeType)
                //    {
                //        case XmlNodeType.Element:
                //            if (reader.Name == actualisationMode)
                //            {
                //                xmlMode = (reader.Value + "\n");
                //            }
                //            else if (reader.Name == day)
                //            {
                //                xmlDay = (reader.Value + "\n");
                //            }
                //            else if (reader.Name == hour)
                //            {
                //                xmlHour = (reader.Value + "\n");
                //            }
                //            else if (reader.Name == manualDate)
                //            {
                //                xmlManual = (reader.Value + "\n");
                //            }
                //            break;
                //    }
                //}
                //reader.Dispose();
                //reader.Close();

                //MessageBox.Show(result);
                //return "a";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                return dayMode;
            }
        }

        protected override void OnStop()
        {
            // TODO: ajoutez ici le code pour effectuer les destructions nécessaires à l'arrêt de votre service.
        }


    }
}
