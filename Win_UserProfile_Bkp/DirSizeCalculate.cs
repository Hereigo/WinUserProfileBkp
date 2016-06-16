using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Win_UserProfile_Bkp
{
    class DirSizeCalculate
    {
        private float fullSize = 0;

        public float Rezult { get; }

        public DirSizeCalculate(string dir)
        {
            GetDirectorySize(dir);

            Rezult = fullSize;
        }

        private float GetDirectorySize(string dir)
        {
            string[] files = Directory.GetFiles(dir, "*.*");

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                fullSize += fi.Length;
            }

            string[] subdirs = Directory.GetDirectories(dir);

            foreach (string subdir in subdirs)
            {
                GetDirectorySize(subdir);
            }

            return fullSize;
        }
    }
}
