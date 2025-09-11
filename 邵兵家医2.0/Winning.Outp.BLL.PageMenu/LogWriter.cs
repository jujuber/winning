using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Winning.Outp.BLL.PageMenu
{
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

                File.AppendAllText(filePath, "【进程名称】" + Process.GetCurrentProcess().ProcessName + Environment.NewLine);

                File.AppendAllText(filePath, "【界面标题】" + Process.GetCurrentProcess().MainWindowTitle + Environment.NewLine);

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

                Console.WriteLine("【进程名称】" + Process.GetCurrentProcess().ProcessName);

                Console.WriteLine("【界面标题】" + Process.GetCurrentProcess().MainWindowTitle);

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


        #region 数据库写入日志 注释 暂不启用
        //        /// <summary>
        //        /// 写入数据库日志
        //        /// </summary>
        //        /// <param name="strMsg"></param>
        //        public  void writeDBExceptLog(IDbConnection dbConn, string strMsg, EnumLogLevel lev = EnumLogLevel.INFO)
        //        {
        //            writeDBExceptLog(dbConn, new Exception(strMsg), lev);
        //        }

        //        /// <summary>
        //        /// 把异常信息写入数据库日志
        //        /// </summary>
        //        /// <param name="ex"></param>
        //        public  void writeDBExceptLog(IDbConnection dbConn, Exception ex, EnumLogLevel lev = EnumLogLevel.INFO)
        //        {
        //            IDbCommand logCmd = dbConn.CreateCommand();

        //            logCmd.CommandText = string.Format(@"INSERT INTO LOG(ID,THREAD,LOG_LEV,MESSAGE,STACK_TRACE,CREATED_TIME) 
        //                VALUE('{0}','{1}','{2}','{3}','{4}','{5}'",
        //                System.Guid.NewGuid().ToString(),
        //                Thread.CurrentThread.ManagedThreadId,
        //                lev.ToString(),
        //                ex.Message.Replace("\'", "''"),
        //                ex.StackTrace.Replace("\'", "''"),
        //                DateTime.Now.ToString());
        //        }

        #endregion

        #endregion
        private string createLogFile()
        {
            //默认保留最新的7个日志文件
            deleteOlderLogFile();

            string strFileName = DateTime.Now.ToString("yyyy-MM-dd");

            strFileName = string.IsNullOrWhiteSpace(_prefix) ? strFileName : _prefix + "_" + strFileName;

            if (!Directory.Exists(LogWriter.LogDirectory + @"\log\wdkf"))
            {
                Directory.CreateDirectory(LogWriter.LogDirectory + @"\log\wdkf");
            }

            string filePath = LogWriter.LogDirectory + @"\log\wdkf\" + strFileName + ".txt";
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
            if (Directory.Exists(LogWriter.LogDirectory + "\\log\\wdkf") && maxLogFiles > 0)
            {
                DirectoryInfo logDir = new DirectoryInfo(LogWriter.LogDirectory + "\\log\\wdkf");
                FileInfo[] logFiles = logDir.GetFiles("*.txt", SearchOption.TopDirectoryOnly);

                string regex = string.IsNullOrWhiteSpace(_prefix) ? @"\d{4}-\d{2}-\d{2}" : _prefix + @"_\d{4}-\d{2}-\d{2}";
                Regex regexExpress = new Regex(regex);
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
