using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DriverViewer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var drives in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem();
                item.Tag = drives;
                item.Header = drives;
                item.Items.Add(null);
                item.Expanded += Folder_Expanded;   //为展开操作订阅事件
                FolderViewer.Items.Add(item);
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            if (item.Items.Count != 1 || item.Items[0] != null) return;
            item.Items.Clear();

            #region 尝试获取Drive下的文件夹
            var dictionaries = new List<string>();
            try
            {
                var dirs = Directory.GetDirectories((string)item.Tag);
                if (dirs.Length > 0) dictionaries.AddRange(dirs);
            }
            catch
            {
                // ignored
            }


            dictionaries.ForEach(dic =>
            {
                var subitem = new TreeViewItem()
                {
                    Header = GetFileFolderName(dic),
                    Tag = dic
                };

                subitem.Items.Add(null);
                subitem.Expanded += Folder_Expanded;

                item.Items.Add(subitem);
            });
            #endregion


            #region 获取文件
            var files = new List<string>();
            try
            {
                var fs = Directory.GetFiles((string)item.Tag);
                if (fs.Length > 0) files.AddRange(fs);
            }
            catch
            {
                // ignored
            }


            files.ForEach(filepath =>
            {
                var subitem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filepath),
                    Tag = filepath
                };

                item.Items.Add(subitem);
            });
            #endregion
        }


        #region 获取文件的名字GetFileFolderName()
        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            var normalizedPath = path.Replace('/', '\\');
            var lastIndex = normalizedPath.LastIndexOf('\\');
            if (lastIndex <= 0) return path;

            return path.Substring(lastIndex + 1);
        }
        #endregion
    }
}
