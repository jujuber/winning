using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.Kernel.Enum;
using Winning.Outp.DAL.PatInfo.DataObject;
using Winning.FrameWork.DAL.Kernel;
using System.Data;
using System.IO;
using System.Web;
using Winning.EmrOutp.Core.Interface;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 转诊单
    /// </summary>
    public class SqdyyCommand2 : ICommand
    {
        public string ID
        {
            get { return "B32D2C44-EF6B-4221-B96E-E754CB26EA9E"; }
        }

        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            LogWriter loger = LogWriter.CreateLoger("外院申请单调用日志");
            loger.writeLogMessage("开始执行");
            PatBasicInfo pat = null;

            if (Winning.Outp.Core.GlobalVariable.RunAddin == Winning.Outp.Core.AddinEnum.None)
            {
                pat = Winning.Outp.Core.GlobalVariable.PatInfoObj.CurrSelectPatinfo;
            }
            else
            {
                pat = Winning.Outp.Core.GlobalVariable.PatInfoObj.CurrPatinfo;
            }

            if (pat == null)
            {
                loger.writeLogMessage("未选择病人,返回");
                Winning.Outp.Core.GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                loger.writeLogMessage("构造入参");

               

                string zhbz = "";
                string cardno = pat.Cardno.ToString();
                IAdoDb hisAdodb = SqlHelper.GetAdoDb();
                hisAdodb.SqlConnect(SystemType.THIS4);
                DataTable dtSfbrxxk = hisAdodb.GetDataTable("SELECT * FROM SF_BRXXK where patid= " + pat.Patid);
                DataTable dtGhzdk = hisAdodb.GetDataTable("SELECT * FROM GH_GHZDK where xh= " + pat.Ghxh);


                if (dtSfbrxxk.Rows.Count>0)
                {
                    zhbz = (dtSfbrxxk.Rows[0]["zhbz"] ?? "").ToString();
                }

                if (dtGhzdk.Rows.Count > 0)
                {
                    cardno = (dtGhzdk.Rows[0]["cardno"] ?? "").ToString();
                }

                string diagText = "";
                for (int i = 0; i < Winning.Outp.Core.GlobalVariable.DiagnInfo.Count; i++)
                {
                    diagText += "<diag><zddm>" + Winning.Outp.Core.GlobalVariable.DiagnInfo[i].Zddm + "</zddm><zdmc>"+ Winning.Outp.Core.GlobalVariable.DiagnInfo[i].Zdmc + "</zdmc></diag>";
                }

                if (diagText.Length>0)
                {
                    diagText = "<diagnosis>" + diagText + "</diagnosis>";
                }

                loger.writeLogMessage("获取主诉现病史");
                string strLcxx = GetEmrText(pat);
                loger.writeLogMessage("主诉现病史：" + strLcxx);


                string strXml = string.Format(@"
                <root>
                    <hzxm>{0}</hzxm>
                    <sex>{1}</sex>
                    <birth>{2}</birth>
                    <age>{3}</age>
                    <patid>{4}</patid>
                    <cardtype>{5}</cardtype>
                    <cardno>{6}</cardno>
                    <ybdm>{7}</ybdm>
                    <sfzh>{8}</sfzh>
                    <ysdm>{9}</ysdm>
                    <ysxm>{10}</ysxm>
                    <ksdm>{11}</ksdm>
                    <ksmc>{12}</ksmc>
                    <lcxx>{13}</lcxx>
                    <jcmd>{14}</jcmd>
                    <zhbz>{15}</zhbz>
                    {16}
                </root>",
                pat.Hzxm.Trim(),
                (pat.Sex ?? "").Trim(),
                (pat.Birth ?? "").Trim(),
                Winning.Outp.Core.GlobalFunction.GetAge(pat.Birth, "0", "0", "0"),
                "",
                (pat.Cardtype ?? "").Trim(),
                (cardno ?? "").Trim(),
                (pat.Ybdm ?? "").Trim(),
                (pat.Sfzh ?? "").Trim(),
                "",
                "",
                "",
                "",
                strLcxx,
                "",
                zhbz.Trim(),
                diagText);


                strXml = "\"" + strXml + "\"";
                loger.writeLogMessage("入参：" + strXml);

                string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\WinningSqdExe\Winning.Outp.UI.Sqd.exe";
                loger.writeLogMessage("接口文件路径：" + filePath);

                if (!System.IO.File.Exists(filePath))
                {

                    loger.writeLogMessage("接口文件未找到 返回");
                    Winning.Outp.Core.GlobalVariable.HisApp.Prompt.Show("接口文件: " + filePath + " 未找到！",MessageBoxButtons.OK);
                }
                else
                {
                    loger.writeLogMessage("调用接口，入参: " + strXml);

                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath,strXml);
                    process.Start();
                }

            }

            return new FrameWork.Core.Common.RequestResult { Success = true };
        }


        

        /// <summary>
        /// 获取电子病历文本
        /// </summary>
        public string GetEmrText(PatBasicInfo patBasicInfo)
        {
            IEmrQuery context = Winning.EmrOutp.Infrastructure.IOC.IOCFactory.GetIOCResolve<IEmrQuery>();

            List<string> paths = new List<string>();

            List<string> result = new List<string>();
            try
            {
                IAdoDb htAdodb = SqlHelper.GetAdoDb();
                htAdodb.SqlConnect(SystemType.HT);
                DataTable dtEmrLdxxNodeConfig = htAdodb.GetDataTable("SELECT * FROM OUTP_SQDCONFIGK WHERE ID='SQ009'");


                string emrConfig = dtEmrLdxxNodeConfig != null && dtEmrLdxxNodeConfig.Rows.Count > 0 ? dtEmrLdxxNodeConfig.Rows[0]["VALUE"].ToString() : "";

                if (!string.IsNullOrWhiteSpace(emrConfig))
                {
                    List<string> emrid = new List<string>();
                    List<string> items = emrConfig.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    items.ForEach(p =>
                    {
                        string[] array = p.Split('|');
                        if (!emrid.Contains(array[0]))
                        {
                            emrid.Add(array[0]);
                            if (array.Length == 2)
                                dict.Add(array[0], array[1]);
                        }
                    });
                    emrid.ForEach(p =>
                    {
                        string caption = dict.ContainsKey(p) ? dict[p] : "";
                        string data = context.GetEmrModel(p, patBasicInfo.nEmrXh.ToString("0.#####"));
                        if (!string.IsNullOrEmpty(data))
                        {
                            result.Add(caption + data);
                        }
                    });
                }

            }
            catch (Exception ex)
            {
                LogWriter.CreateLoger("外院申请单调用日志").writeLogMessage("获取主诉现病史报错：" + ex.Message);
                return "";
            }

            return result == null ? string.Empty : result.Count == 0 ? string.Empty : string.Join("\r\n", result);
        }

        public class LogWriter
        {
            public static LogWriter CreateLoger(string prefix)
            {
                LogWriter logWriter = new LogWriter(prefix);
                return logWriter;
            }

            public static LogWriter CreateLoger()
            {
                LogWriter logWriter = new LogWriter(string.Empty);
                return logWriter;
            }

            /// <summary>
            /// 日志文件保留的最大数目
            /// </summary>
            public static int maxLogFiles = 7;

            public static string LogDirectory
            {
                get
                {
                    if (HttpContext.Current != null)
                    {
                        return AppDomain.CurrentDomain.BaseDirectory + "bin";
                    }
                    else
                    {
                        return AppDomain.CurrentDomain.BaseDirectory;
                    }
                }
            }

            static object _locker = new object();

            private string _prefix = string.Empty;

            private LogWriter(string prefix)
            {
                _prefix = prefix;
            }


            #region 日志记录器


            public void writeLogMessage(string strMsg)
            {
                lock (_locker)
                {
                    string filePath = createLogFile();

                    File.AppendAllText(filePath, "【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】" + strMsg + Environment.NewLine);
                    //writeDBExceptLog("【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】" + strMsg );
                }
            }


            public void writeConsoleMessage(string strMsg)
            {
                lock (_locker)
                {
                    string filePath = createLogFile();

                    Console.Write("【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】" + strMsg + Environment.NewLine);

                }
            }


            /// <summary>
            /// 在记事本文件中写入异常信息，日志等级默认INFO
            /// </summary>
            /// <param name="ex"></param>
            /// <param name="lev"></param>
            public void writeLogExceptione(Exception ex, EnumLogLevel lev = EnumLogLevel.INFO)
            {
                lock (_locker)
                {
                    string filePath = createLogFile();

                    File.AppendAllText(filePath, "【记录时间】" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);

                    File.AppendAllText(filePath, "【进程名称】" + System.Diagnostics.Process.GetCurrentProcess().ProcessName + Environment.NewLine);

                    File.AppendAllText(filePath, "【界面标题】" + System.Diagnostics.Process.GetCurrentProcess().MainWindowTitle + Environment.NewLine);

                    File.AppendAllText(filePath, "【日志等级】" + lev.ToString() + Environment.NewLine);

                    File.AppendAllText(filePath, "【错误类型】" + ex.GetType().ToString() + Environment.NewLine);

                    File.AppendAllText(filePath, "【错误消息】" + ex.Message + Environment.NewLine);

                    File.AppendAllText(filePath, "【堆栈地址】" + ex.StackTrace + Environment.NewLine);

                    //写出内部异常信息
                    if (ex.InnerException != null)
                    {
                        File.AppendAllText(filePath, "      【内部异常错误消息】" + ex.InnerException.Message + Environment.NewLine);

                        File.AppendAllText(filePath, "      【内部异常堆栈地址】" + ex.InnerException.StackTrace + Environment.NewLine);

                    }


                    File.AppendAllText(filePath,
                        "===============================================================================" + Environment.NewLine);

                }
            }

            /// <summary>
            /// 在命令行窗体中写入异常信息，日志等级默认INFO
            /// </summary>
            /// <param name="ex"></param>
            /// <param name="lev"></param>
            public void writeConsoleException(Exception ex, EnumLogLevel lev = EnumLogLevel.INFO)
            {
                lock (_locker)
                {
                    Console.WriteLine("【记录时间】" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    Console.WriteLine("【进程名称】" + System.Diagnostics.Process.GetCurrentProcess().ProcessName);

                    Console.WriteLine("【界面标题】" + System.Diagnostics.Process.GetCurrentProcess().MainWindowTitle);

                    Console.WriteLine("【日志等级】" + lev);

                    Console.WriteLine("【错误类型】" + ex.GetType().ToString());

                    Console.WriteLine("【错误消息】" + ex.Message);

                    Console.WriteLine("【堆栈地址】" + ex.StackTrace);

                    //写出内部异常信息
                    if (ex.InnerException != null)
                    {

                        Console.WriteLine("     【内部异常错误消息】" + ex.InnerException.Message);

                        Console.WriteLine("     【内部异常堆栈地址】" + ex.InnerException.StackTrace);

                    }

                    Console.WriteLine();
                }
            }



            #endregion
            private string createLogFile()
            {
                //默认保留最新的7个日志文件
                deleteOlderLogFile();

                string strFileName = DateTime.Now.ToString("yyyy-MM-dd");

                strFileName = string.IsNullOrWhiteSpace(_prefix) ? strFileName : _prefix + "_" + strFileName;

                if (!Directory.Exists(LogWriter.LogDirectory + @"\log"))
                {
                    Directory.CreateDirectory(LogWriter.LogDirectory + @"\log");
                }

                string filePath = LogWriter.LogDirectory + @"\log\" + strFileName + ".txt";
                if (!File.Exists(filePath))
                {
                    try
                    {
                        File.Create(filePath).Close();
                    }
                    catch (Exception logEx)
                    {
                        throw logEx;
                    }
                }

                return filePath;
            }

            private void deleteOlderLogFile()
            {
                //只保留最新的几个日志文件
                if (Directory.Exists(LogWriter.LogDirectory + "\\log") && maxLogFiles > 0)
                {
                    DirectoryInfo logDir = new DirectoryInfo(LogWriter.LogDirectory + "\\log");
                    FileInfo[] logFiles = logDir.GetFiles("*.txt", SearchOption.TopDirectoryOnly);

                    string regex = string.IsNullOrWhiteSpace(_prefix) ? @"\d{4}-\d{2}-\d{2}" : _prefix + @"_\d{4}-\d{2}-\d{2}";
                    System.Text.RegularExpressions.Regex regexExpress = new System.Text.RegularExpressions.Regex(regex);
                    logFiles = logFiles.Where(
                        o => regexExpress.IsMatch(o.Name)).ToArray();

                    if (logFiles.Count() > maxLogFiles)
                    {
                        IList<FileInfo> enumerFileInfo = logFiles.OrderByDescending(o => o.CreationTime).ToList();

                        //当前正在使用的日志文件不能删除 
                        for (int i = maxLogFiles; i < enumerFileInfo.Count(); i++)
                        {
                            try
                            {
                                enumerFileInfo[i].Delete();
                            }
                            catch (Exception logex)
                            {
                                ;
                            }
                        }
                    }
                }
            }

           

        }

        /// <summary>
        ///日志等级 
        /// </summary>
        public enum EnumLogLevel
        {
            INFO = 0,
            DEBUG = 1,
            ERROR = 2,
            WARNING = 3,
            FATAL = 4
        }
    }
}

