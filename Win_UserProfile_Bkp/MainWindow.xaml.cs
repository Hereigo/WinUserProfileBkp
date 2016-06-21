using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
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
        public static string bkpPath = "";

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                dataGrid.ItemsSource = GetFoldersList(dirsForScan());
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        // 1. Get UsersProfiles list

        // 2. If UPL not found => try to set manual

        // 3. Get Dirs From AD & LS_AD 

        private string[] dirsForScan() //string userForBkp)
        {
            // TODO:
            // MUST BE SELECTED FROM USERS LIST !!!
            // MUST BE SELECTED FROM USERS LIST !!!
            // MUST BE SELECTED FROM USERS LIST !!!

            return new string[] {
                // Environment.GetEnvironmentVariable("ALLUSERSPROFILE"),
                Environment.GetEnvironmentVariable("APPDATA"),
                
#if !DEBUG
                //Environment.GetEnvironmentVariable("USERPROFILE") + "\\Мои документы",
                // Environment.GetEnvironmentVariable("USERPROFILE") + "\\Рабочий стол",
                Environment.GetEnvironmentVariable("USERPROFILE") +
                    "\\Local Settings\\Application Data"
#endif
            };
        }


        private List<Folder> GetFoldersList(string[] startDirs)
        {
            List<Folder> foldersList = new List<Folder>();

            foreach (string currentDir in startDirs)
            {
                if (!CanRead(currentDir))
                {
                    MessageBox.Show("Not found : " + currentDir);
                }
                else
                {
                    string[] subDirs = Directory.GetDirectories(currentDir);

                    foreach (string subDir in subDirs)
                    {
                        // TODO:
                        // CREATE EXCLUDE LIST OUTSIZE !!!
                        // CREATE EXCLUDE LIST OUTSIZE !!!
                        // CREATE EXCLUDE LIST OUTSIZE !!!

                        if (!subDir.ToLower().Contains("mail.ru") && !subDir.ToLower().Contains("mailru") && !subDir.ToLower().Contains("daemon tools"))
                        {
                            foldersList.Add(new Folder(subDir));
                        }
                    }
                }
            }
            foldersList.Add(new Folder(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)));

            return foldersList;
        }


        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (Folder item in dataGrid.ItemsSource)
            {
                item.Selected = true;
                dataGrid.Items.Refresh();
            }
        }


        // 4. Use Exclusion-List for previous Step + ability to ADD

        // 5. Write List (3) into Grid

        // 6. Show in Grid CreateDate, ModifDate, DirSize

        // 7. Ability toSelect some dirs for next ZIP

        // 8. Set Exclude-List For ZIP

        // 9. ZIP

        // 10. Report of ZIP sizes.


        public static bool CanRead(string path)
        {
            var readAllow = false;
            var readDeny = false;
            var accessControlList = Directory.GetAccessControl(path);
            if (accessControlList == null)
                return false;
            var accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            if (accessRules == null)
                return false;

            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Read & rule.FileSystemRights) != FileSystemRights.Read) continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    readAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    readDeny = true;
            }

            return readAllow && !readDeny;
        }


        private void btnSelectPath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var foldBrowsDlg = new System.Windows.Forms.FolderBrowserDialog();

                System.Windows.Forms.DialogResult FBDResult = foldBrowsDlg.ShowDialog(this.GetIWin32Window());

                if (FBDResult == System.Windows.Forms.DialogResult.OK)
                {
                    string path = foldBrowsDlg.SelectedPath;

                    lblSelectedPath.Content = path;

                    bkpPath = path;

                    btnStartBkp.Visibility = Visibility.Visible;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        private void btnStartBkp_Click(object sender, RoutedEventArgs e)
        {
            foreach (Folder item in dataGrid.ItemsSource)
            {
                if (item.Selected)
                {
                    if (BkpThis(item.Name, item.Path))
                    {
                        item.Selected = false;
                        //MessageBox.Show(item.Name + " saved from " + item.Path);
                        dataGrid.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show(item.Name + " NOT saved from " + item.Path);
                    }
                    dataGrid.Items.Refresh();
                }
            }
            Process.Start(bkpPath);
        }


        private bool BkpThis(string name, string path)
        {
            bool successful = false;
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(@"C:\Progra~1\WinRAR\Rar.exe");

                // -m0: compressLevel, -rr5p: recovery5%, -t: testingAfter, -dh: compressSharedFiles
#if DEBUG
                psi.Arguments = " u -m0 -rr5p -r -t -dh \"" +
#else
                psi.Arguments = " m[f] -m0 -rr5p -r -t -dh \"" +
#endif
                bkpPath + "\\" + name.Replace(' ', '_') + ".RAR\" " + "\"" + path + "\\*.*\"";

                Process.Start(psi).WaitForExit();

                successful = true;

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            return successful;
        }
    }
}
