using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Win_UserProfile_Bkp
{
    class Folder
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public float Size { get; set; }
        public bool Selected { get; set; }
    }
}
