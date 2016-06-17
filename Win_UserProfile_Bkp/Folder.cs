using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Win_UserProfile_Bkp
{
    class Folder
    {
        //public int ID { get; }
        public string Name { get; }
        public string Path { get; }
        public DateTime Created { get; }
        public DateTime Modified { get; }
        public bool Selected { get; set; }
        public double SizeMB { get; }

        public Folder(string dirPath)
        {
            DirectoryInfo d = new DirectoryInfo(dirPath);
            Created = d.CreationTime;
            Modified = d.LastWriteTime;
            Name = d.Name;
            Path = d.FullName;
            Selected = false;
            SizeMB = Math.Round((new DirSizeCalculate(d.FullName).Rezult / 1024 / 1024), 2);
        }
    }
}
