using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarthThesaurusLibrary
{
    public class File
    {
        public int idFile;
        public string Name;
        public string Size;
        public DateTime LastWriteTime;
        public string Directory;
        public int idDateToActualise;

        public File(int idFile, string Name, string Size, DateTime LastWriteTime, string Directory, int idDateToActualise)
        {
            this.idFile = idFile;
            this.Name = Name;
            this.Size = Size;
            this.LastWriteTime = LastWriteTime;
            this.Directory = Directory;
            this.idDateToActualise = idDateToActualise;
        }
    }
}
