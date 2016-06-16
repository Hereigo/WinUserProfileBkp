using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Win_UserProfile_Bkp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string dirForCalc = "C:\\temp";

            float rezSize = new DirSizeCalculate(dirForCalc).Rezult;

            MessageBox.Show(Math.Round((rezSize/1024/1024), 2).ToString() + " MB. (" + rezSize.ToString() + ")");
        }


        // 1. Get UsersProfiles list



        // 2. If UPL not found => try to set manual



        // 3. Get Dirs From AD & LS_AD 

        //private List<Folder> GetFoldersList(string profileRoot)
        //{

        //}

        // 4. Use Exclusion-List for previous Step + ability to ADD

        // 5. Write List (3) into Grid

        // 6. Show in Grid CreateDate, ModifDate, DirSize

        // 7. Ability toSelect some dirs for next ZIP

        // 8. ZIP

        // 9. Report of ZIP sizes.
    }
}
