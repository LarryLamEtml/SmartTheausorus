﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace SmarthThesaurusLibrary
{
    public static class XML
    {

        static XmlWriterSettings settings = new XmlWriterSettings();
        static XmlWriter writer;

        public static void setDateXML(string actualisation, string day, string hour, string manualDate)
        {
            writer = XmlWriter.Create("actualisationDate.xml", settings);
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
            XmlWriter writer = XmlWriter.Create("tempData.xml", settings);

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
                writer.WriteElementString("idDateToActualise", f.idDateToActualise.ToString());

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Dispose();
            writer.Close();
        }
        public static void etmlDataToXML()
        {

        }
        public static void eduDataToXML()
        {

        }

        public static List<File> actualiseData(String[] _allTempFiles, List<File> _fileListTemp, int index)
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
                File file = new File(idCount, fi.Name, Library.BytesToString(fi.Length), fi.LastWriteTime, fi.Directory.ToString(), index);
                _fileListTemp.Add(file);
                idCount++;
            }
            tempDataToXML(_fileListTemp);
            etmlDataToXML();
            eduDataToXML();
            return _fileListTemp;
        }
    }
}