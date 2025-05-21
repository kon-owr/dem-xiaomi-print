using System.Collections.Generic;
using System.Windows;

namespace MyWpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 动态加载域名列表
            LoadDomainList();
        }

        private void LoadDomainList()
        {
            // 将GetAllFolders获取到的文件夹路径列表转为domains列表
            var folders = GetAllFolders(@"\\10.70.51.221\xiaomi\P\物流标贴");

            // 将域名列表绑定到 ListBox
            DomainListBox.ItemsSource = folders;
        }

        public void DomainListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ConfirmButton_Click(ConfirmButton, new RoutedEventArgs());
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            // 获取选中的域名
            var selectedDomain = DomainListBox.SelectedItem as FolderInfo;

            if (selectedDomain != null)
            {
                // 创建新窗口并传递选中的域名
                DetailWindow detailWindow = new DetailWindow(selectedDomain);


                // 显示新窗口
                detailWindow.Show();

                // 可选：隐藏当前窗口
                // this.Hide();

                // 可选：如果希望当前窗口关闭
                this.Close();
            }
            else
            {
                // 如果未选择任何项，显示提示信息
                ResultTextBlock.Text = "请先选择一个模板！";
            }
        }

        public static List<FolderInfo> GetAllFolders(string path)
        {
            // 获取文件夹路径和文件夹名称
            List<FolderInfo> folders = new List<FolderInfo>();
            try
            {
                string[] subFolders = System.IO.Directory.GetDirectories(path);
                foreach (string folder in subFolders)
                {
                    // 获取文件夹名称
                    string folderName = System.IO.Path.GetFileName(folder);
                    folders.Add(new FolderInfo { FolderPath = folder, FolderName = folderName });
                }
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show($"无法访问路径: {path}\n错误信息: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return folders;
        }
    }

    // 文件信息类
    public class FolderInfo
    {
        public required string FolderPath { get; set; }
        public required string FolderName { get; set; }
    }

}
