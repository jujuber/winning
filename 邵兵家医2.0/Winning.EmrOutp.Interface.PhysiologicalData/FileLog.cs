using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Winning.EmrOutp.Interface.PhysiologicalData
{
    internal class FileLog
    {
        private static object lockObj = new object();
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="log"></param>
        public static void WriteLog(string log)
        {
            lock (lockObj)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "//Log//Tzsj";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyy-MM-ddHH");
                string fullFileName = path + "//" + fileName + ".log";
                try
                {
                    using (FileStream fs = new FileStream(fullFileName, FileMode.Append, FileAccess.Write))
                    {
                        StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                        sw.WriteLine("\r\n-----------------------------------------\r\n\r\n" + DateTime.Now.ToString() + "\r\n\r\n" + log.Replace("\r\n", ""));
                        sw.Close();
                    }
                }
                catch
                {
                   
                }
            }
        }
    }
}
