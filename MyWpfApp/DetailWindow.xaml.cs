using System.Windows;
using System.Windows.Input;
using System.Management;
using System.IO;

namespace MyWpfApp
{
    public partial class DetailWindow : Window
    {
        readonly FolderInfo folderInfo1;
        public DetailWindow(FolderInfo folderInfo)
        {
            InitializeComponent();
            folderInfo1 = folderInfo;
            // 设置文本框显示选中的域名
            DomainTextBlock.Text = this.folderInfo1.FolderName;
        }

        // 按下回车键时，将内容插入到 TextBlock 的下一行
        private void DomainTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                InsertTextToTextBlock(DomainTextBox.Text);
                Print(DomainTextBox.Text);
            }
        }

        // 点击按钮时，将内容插入到 TextBlock 的下一行
        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            InsertTextToTextBlock(DomainTextBox.Text);
            Print(DomainTextBox.Text);
        }

        // 插入内容到 TextBlock 的方法
        private void InsertTextToTextBlock(string inputText)
        {

            if (!string.IsNullOrWhiteSpace(inputText))
            {
                // 将新内容追加到 TextBlock 的下一行
                DomainTextBlock.Text += "\n" + inputText;

                // 清空 TextBox
                DomainTextBox.Clear();
            }
        }

        public void Print(string text)
        {
            // 发送信息到TextBlock
            InsertTextToTextBlock("正在打印......");
            // 获取系统默认打印机名称
            string printName = "";
            string query = "SELECT * FROM Win32_Printer WHERE Default = true";
            bool Flag = false;

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject printer in searcher.Get())
                {
                    printName = (string)printer["Name"];
                }
            }

            // 关联打印模板字段的值
            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                { "binData", text},
            };

            //表示有一个名称为SN的打印机，然后打印模板的路径，打印内容，打印数量
            Flag = BarTenderPrint.Print(printName, this.folderInfo1.FolderPath + "\\物流标贴.btw", dict, 1);

            if (Flag)
            {
                // 将新内容追加到 TextBlock 的下一行
                InsertTextToTextBlock("打印成功");
            }
            else
            {
                // 将新内容追加到 TextBlock 的下一行
                InsertTextToTextBlock("打印失败");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // 创建 MainWindow 实例
            MainWindow mainWindow = new MainWindow();

            // 显示 MainWindow
            mainWindow.Show();

            // 关闭当前窗口
            this.Close();
        }

    }
}
