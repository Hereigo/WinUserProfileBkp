using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Win_UserProfile_Bkp
{
    class Folder
    {
        public int ID { get; }
        public bool Selected { get; set; }
        //public float Size { get; }
        public string Size { get; }
        public string Name { get; }
        public DateTime Created { get; }
        public DateTime Modified { get; }
        public string Path { get; }

        public Folder(string dirPath)
        {
            DirectoryInfo d = new DirectoryInfo(dirPath);
            Created = d.CreationTime;
            Modified = d.LastWriteTime;
            Name = d.Name;
            Path = d.FullName;
            Selected = false;
            Size = Math.Round((new DirSizeCalculate(d.FullName).Rezult / 1024 / 1024), 2).ToString() + " MB.";
        }
    }
}
