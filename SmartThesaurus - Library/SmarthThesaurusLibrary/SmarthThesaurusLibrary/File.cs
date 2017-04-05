using System;

namespace SmartThesaurusLibrary
{
    public class File
    {
        public int idFile;
        public string Name;
        public string Size;
        public DateTime LastWriteTime;
        public string Directory;

        public File(int idFile, string Name, string Size, DateTime LastWriteTime, string Directory)
        {
            this.idFile = idFile;
            this.Name = Name;
            this.Size = Size;
            this.LastWriteTime = LastWriteTime;
            this.Directory = Directory;
        }
    }
}
