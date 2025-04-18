using BarTender;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWpfApp
{

    public class BarTenderPrint
    {
        /// <summary>
        /// 打开标签文件
        /// </summary>
        public static BarTender.Application btApp = new BarTender.Application();

        /// <summary>
        /// BarTender运行软件
        /// </summary>
        public static BarTender.Format btFormat = new BarTender.Format();


        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="printerName">打印机名称</param>
        /// <param name="btFileName">打印文件</param>
        /// <param name="dict">内容字典</param>
        /// <param name="CopiesOfLabel">打印数量</param>
        public static bool Print(string printerName, string btFileName, Dictionary<string, string> dict, int CopiesOfLabel)
        {
            try
            {
                btFormat = btApp.Formats.Open(btFileName);
                btFormat.PrintSetup.Printer = printerName;
                btFormat.IdenticalCopiesOfLabel = CopiesOfLabel;
                foreach (var item in dict)
                {
                    btFormat.SetNamedSubStringValue(item.Key, item.Value);
                }
                btFormat.PrintOut(false, false);
                //不保存标签退出
                btFormat.Close(BarTender.BtSaveOptions.btDoNotSaveChanges);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }
    }
}
