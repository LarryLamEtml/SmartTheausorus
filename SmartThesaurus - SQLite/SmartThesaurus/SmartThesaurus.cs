using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data.SQLite;

namespace SmartThesaurus
{
    public partial class formSearch : Form
    {
        public SQLiteConnection connectionToDb;
        string fileName = "./tempFiles.db";//File name
        const string sqlScript = "./scriptDb.sql";
        List<string> listComboBoxElements = new List<string>();
        //DirectoryInfo PATH = new DirectoryInfo(@"K:\INF\eleves\temp", SearchOption.AllDirectories);
        const string PATH = @"K:\INF\eleves\temp";
        String[] allfiles;
        List<FileInfo> listFileinfo;
        List<FileInfo> sortedListFileInfo;
        const string ETML = "ETML";
        const string EDUCANET = "Educanet2";
        const string TEMP = "Temp";

        DateTime dateToActualiseEtml;
        DateTime dateToActualiseEducanet;
        DateTime dateToActualiseTemp;

        Regex regexCondition;
        /// <summary> 
        /// Permet de pouvoir deplacer l'application malgré que le formBorderStyle soit à "none" !! mais permet de l'agrandir
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HT_CLIENT = 0x1;
            const int HT_CAPTION = 0x2;
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);

        }

        public formSearch()
        {
            InitializeComponent();
            this.AcceptButton = btnSearchEtml;//Bouton avec le focus (enter pour cliquer)
            //change les bordures des boutons
            btnSearchEtml.FlatAppearance.BorderColor = Color.FromArgb(25, 250, 65);
            btnSearchEducanet.FlatAppearance.BorderColor = Color.FromArgb(25, 250, 65);
            btnSearchTemp.FlatAppearance.BorderColor = Color.FromArgb(25, 250, 65);
            connectDatabase();
            actualiseData();


            // readXML();
        }
        public void connectDatabase()
        {

            if (!File.Exists(fileName))//Si le fichier score.txt n'existe pas
            {
                SQLiteConnection.CreateFile(fileName);//Créer un fichier tempFiles
                connectionToDb = new SQLiteConnection(@"Data Source=" + fileName);
                executeQuery(File.ReadAllText(sqlScript));//Crée la bdd et execute le script sql
            }
            else
            {
                connectionToDb = new SQLiteConnection(@"Data Source=" + fileName);
            }

        }

        public DataTable executeQuery(string _script)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                connectionToDb.Open();  //Initiate connection to the db
                cmd = connectionToDb.CreateCommand();
                cmd.CommandText = _script;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Impossible de créer la base de donnée...");
            }
            connectionToDb.Close();
            return dt;

        }

        public void insertFile()
        {
            string table = "t_etmlFile";
            string prefix = "etml";
            switch (TbCMain.SelectedIndex)
            {
                case 0:
                    table = "t_etmlFile";
                    prefix = "etml";
                    break;
                case 1:
                    table = "t_educanetFile";
                    prefix = "edu";
                    break;
                case 2:
                    table = "t_tempFile";
                    prefix = "temp";
                    break;
                default:
                    break;
            }

            connectionToDb.Open();
            SQLiteCommand clearTable = new SQLiteCommand("TRUNCATE db_theausorus." + table, connectionToDb);
            int id = 0;
            foreach (FileInfo fi in listFileinfo)
            {

                SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO " + table + " (idFile, " + prefix + "Name, " + prefix + "Size, " + prefix + "LastModified, " + prefix + "Directory, idDateToActualise) VALUES (@id,@name,@size,@lastModified,@directory,@dateToActualise)", connectionToDb);
                insertSQL.Parameters.Add(new SQLiteParameter("@id", id));
                insertSQL.Parameters.Add(new SQLiteParameter("@name", fi.Name));
                insertSQL.Parameters.Add(new SQLiteParameter("@size", fi.Length));
                insertSQL.Parameters.Add(new SQLiteParameter("@lastModified", fi.LastWriteTime));
                insertSQL.Parameters.Add(new SQLiteParameter("@directory", fi.Directory.ToString()));
                insertSQL.Parameters.Add(new SQLiteParameter("@dateToActualise", TbCMain.SelectedIndex));
                id++;

                try
                {
                    insertSQL.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            connectionToDb.Close();
        }

        /* public void createDb()
         {
             // This is the query which will create a new table in our database file with three columns. An auto increment column called "ID", and two NVARCHAR type columns with the names "Key" and "Value"
             string createTableQuery = @"CREATE TABLE IF NOT EXISTS [MyTable] (
                           [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                           [Key] NVARCHAR(2048)  NULL,
                           [Value] VARCHAR(2048)  NULL
                           )";

             SQLiteConnection.CreateFile("tempFiles.db3");
             using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=databaseFile.db3"))
             {
                 using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                 {
                     con.Open();                             // Open the connection to the database

                     com.CommandText = createTableQuery;     // Set CommandText to our query that will create the table
                     com.ExecuteNonQuery();                  // Execute the query

                     com.CommandText = "INSERT INTO MyTable (Key,Value) Values ('key one','value one')";     // Add the first entry into our database 
                     com.ExecuteNonQuery();      // Execute the query
                     com.CommandText = "INSERT INTO MyTable (Key,Value) Values ('key two','value value')";   // Add another entry into our database 
                     com.ExecuteNonQuery();      // Execute the query

                     com.CommandText = "Select * FROM MyTable";      // Select all rows from our database table

                     using (SQLiteDataReader reader = com.ExecuteReader())
                     {
                         while (reader.Read())
                         {
                             MessageBox.Show(reader["Key"] + " : " + reader["Value"]);     // Display the value of the key and value column for every row
                         }
                     }
                     con.Close();        // Close the connection to the database
                 }
             }
         }*/


        /*   public void readXML()
           {

           }

           public void writeInXml()
           {
               XmlWriterSettings settings = new XmlWriterSettings();

               settings.Indent = true;
               settings.IndentChars = ("\t");
               settings.OmitXmlDeclaration = true;


               XmlWriter writer = XmlWriter.Create("tempData.xml",settings);

               writer.WriteStartDocument();
               writer.WriteStartElement("Files");

               foreach (FileInfo fi in listFileinfo)
               {
                   writer.WriteStartElement("File");

                   writer.WriteElementString("name", fi.Name);  
                   writer.WriteElementString("size", BytesToString(fi.Length));
                   writer.WriteElementString("lastModified", fi.LastWriteTime.ToString());
                   writer.WriteElementString("directory", fi.Directory.ToString());

                   writer.WriteEndElement();
               }

               writer.WriteEndElement();
               writer.WriteEndDocument();
           }

    */
        public void actualiseData()
        {
            try
            {
                allfiles = Directory.GetFiles(PATH, "*.*", SearchOption.AllDirectories);
            }
            catch
            {
                MessageBox.Show("Le chemin " + PATH + " est introuvable");
            }

            listFileinfo = new List<FileInfo>();

            foreach (var file in allfiles)
            {
                listFileinfo.Add(new FileInfo(file));
            }

        }

        public void searchTemp()
        {
            try
            {

                sortedListFileInfo = new List<FileInfo>();
                listViewResultTemp.Items.Clear();

                if (txbInputTemp.Text != "")
                {
                    regexCondition = new Regex(@".*(" + Regex.Escape(txbInputTemp.Text) + @").*");
                }
                foreach (FileInfo fi in listFileinfo)
                {

                    if (txbInputTemp.Text != "")
                    {
                        Match match = regexCondition.Match(fi.Name);
                        if (match.Success)
                        {
                            ListViewItem lvi = new ListViewItem(fi.Name);
                            lvi.SubItems.Add(BytesToString(fi.Length));
                            lvi.SubItems.Add(fi.LastWriteTime.ToString());
                            lvi.SubItems.Add(fi.Directory.ToString());
                            listViewResultTemp.Items.Add(lvi);
                            sortedListFileInfo.Add(fi);
                        }
                    }
                    else
                    {
                        ListViewItem lvi = new ListViewItem(fi.Name);
                        lvi.SubItems.Add(BytesToString(fi.Length));
                        lvi.SubItems.Add(fi.LastWriteTime.ToString());
                        lvi.SubItems.Add(fi.Directory.ToString());
                        listViewResultTemp.Items.Add(lvi);
                        sortedListFileInfo.Add(fi);
                    }

                }
                listViewResultTemp.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);//Ajuste la taille de la colonne en fonction des données
                listViewResultTemp.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//Définis la taille minimum = taille du header
            }
            catch
            {
                MessageBox.Show("Avertissement, le répertoire du chemin " + PATH + " n'à pas été trouvé");
            }
        }

        public void searchEtml()
        {

        }

        /// <summary>
        /// Permet de retourner une taille de fichier lisible
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        static String BytesToString(long byteCount)
        {
            string[] suf = { " B", " KB", " MB", " GB", " TB", " PB", " EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        private void formSearch_Load(object sender, EventArgs e)
        {

        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearchEtml_Click(object sender, EventArgs e)
        {
            searchEtml();
            insertFile();
        }

        private void btnSearchTemp_Click(object sender, EventArgs e)
        {
            searchTemp();

            insertFile();
        }

        private void listViewResultTemp_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void listViewResultTemp_ItemActivate(object sender, EventArgs e)
        {
            int selectedFileIndex = listViewResultTemp.SelectedItems[0].Index;
            //MessageBox.Show(sortedListFileInfo[selectedFileIndex].DirectoryName + @"\" + sortedListFileInfo[selectedFileIndex].Name);
            try
            {
                Process.Start(sortedListFileInfo[selectedFileIndex].DirectoryName + @"\" + sortedListFileInfo[selectedFileIndex].Name);
            }
            catch
            {
                MessageBox.Show("Erreur lors de l'ouverture du fichier");
            }

        }

        private void btnSearchEducanet_Click(object sender, EventArgs e)
        {
            insertFile();
        }
    }
}
