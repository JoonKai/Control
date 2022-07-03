using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTreeView
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Contructor
        /// <summary>
        /// Default Contructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion
        #region On Loaded
        /// <summary>
        /// When the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(var drive in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem() 
                {
                    Header = drive,
                    Tag = drive
                };
                item.Items.Add(null);
                item.Expanded += Folder_Expended;
                FolderView.Items.Add(item);
            }
        }

        #endregion
        #region Folder Expanded
        private void Folder_Expended(object sender, RoutedEventArgs e)
        {
            #region Initial Checks
            var item = (TreeViewItem)sender;
            //만약 item이 더미데이터를 포함하고 있다면
            if (item.Items.Count != 1 && item.Items[0] != null)
                return;

            //더미데이터 제거
            item.Items.Clear();

            //fullpath를 얻어온다.
            var fullPath = (string)item.Tag;
            #endregion
            #region Get Folders
            var directories = new List<string>();
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch
            {
            }
            directories.ForEach(directoryPath =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(directoryPath),
                    Tag = directoryPath
                };

                subItem.Items.Add(null);
                subItem.Expanded += Folder_Expended;
                item.Items.Add(subItem);
            });
            #endregion
            #region Get Files
            var files = new List<string>();
            try
            {
                var fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch
            {
            }
            files.ForEach(filepath =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filepath),
                    Tag = filepath
                };
                item.Items.Add(subItem);
            });
            #endregion
        }
        #endregion
        /// <summary>
        /// Find the file  or folder name form a full path
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            //만약 패스를 가지고 있지 않다면, 공백을 리턴한다.
            if(string.IsNullOrEmpty(path))
                return string.Empty;
            //모든 슬래쉬를 백슬래쉬로 변경한다.
            var nomalizedPath = path.Replace('/', '\\');
            //패스의 마지막 슬래쉬를 찾는다.
            var lastIndex = nomalizedPath.LastIndexOf('\\');

            //만약 우리가 백슬래쉬를 찾지 못한다면 패스 그대로를 리턴한다.
            if(lastIndex <= 0)
            {
                return path;
            }
            //마지막 백슬래쉬 후에 이름을 리턴한다.
            return path.Substring(lastIndex + 1);
        }
    }
}
