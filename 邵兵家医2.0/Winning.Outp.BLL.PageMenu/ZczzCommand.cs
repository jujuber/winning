using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.Outp.DAL.PatInfo.DataObject;
using Winning.Model.Common;
using Winning.FrameWork.IDAL;
using Winning.FrameWork.DAL.Kernel;
using System.Data;
using Winning.FrameWork.Kernel.Enum;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace Winning.Outp.BLL.PageMenu
{
    public class ZczzCommand: ICommand
    {

        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            Winning.FrameWork.Core.Common.RequestResult result = new Winning.FrameWork.Core.Common.RequestResult();
            //PatBasicInfo pat = ContextValueHelper.GetPatientObj();
            PatBasicInfo pat = GlobalVariable.PatInfoObj.CurrPatinfo;

            StringBuilder sb = new StringBuilder();
            string xml= "<Request><List>"+ToYbMessage(sb, pat)+ "</List></Request>";

            string url = "http://192.168.2.25:8888/jdyyzz/service/webService/UpCbxxInfoService";

            // GlobalFunction.InvokeWebService(url, "UpCbxxInfoService", new object [] {xml});

            PostMoths(url, xml);

            //object msg = null;
            //GlobalVariable.HisSys.Reflected.RunNetApp("Winning.Outp.UI.Zydjd.dll", "Winning.Outp.UI.Zydjd.StartUp", "Run", out msg, pat);




            return result;
        }

        private string PostMoths(string url, string param)
        {
            byte[] postData = Encoding.UTF8.GetBytes(param);//编码，尤其是汉字，事先要看下抓取网页的编码方式    
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可    
            byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流    

            string msg = System.Text.Encoding.UTF8.GetString(responseData).Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"");//
            WriteLog("执行结果：" + msg);
            return msg;
        }

        private void WriteLog(string log)
        {

            if (!Directory.Exists(System.IO.Path.Combine(Application.StartupPath, "log")))
                Directory.CreateDirectory("log");
            try
            {
                string logfile = System.IO.Path.Combine(Application.StartupPath, "log\\fjh" + DateTime.Now.ToString("yyyyMMdd") + ".log");
                FileStream fs = new FileStream(logfile, FileMode.Append);
                try
                {
                    StreamWriter sw = new StreamWriter(fs);
                    try
                    {
                        sw.Write(string.Format("{0}\r\n{1}\r\n{2}\r\n",
                                                   "--------------------------------------------------",
                                                   DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                   log
                                                   )
                                     );
                    }
                    finally
                    {
                        sw.Close();
                    }
                }
                finally
                {
                    fs.Close();
                }
            }
            catch
            { }
        }

        private string GetHospitalCode()
        {
            try
            {
                List<SYS_HOSPITAL> RoleList = DataHelper.DataObj.QueryTable<SYS_HOSPITAL>(SystemType.H0);
                return  RoleList[0].YLJGDM.ToString().Trim();
            }
            catch
            {
                return  "";
            }
        }

        public string ToYbMessage(StringBuilder sb,PatBasicInfo pat)
        {
            string ret = string.Empty;
            sb.Clear();
            //sb.AppendFormat("<DPYBZ>{0}</DPYBZ>", RxPurpose);
            sb.AppendFormat("<yljgdm>{0}</yljgdm> \r\n", GetHospitalCode());
            sb.AppendFormat("<zjlx>{0}</zjlx>\r\n", "1");
            sb.AppendFormat("<zjhm>{0}</zjhm>\r\n", pat.Sfzh);
            sb.AppendFormat("<jzh>{0}</jzh>\r\n", pat.Ghxh);
            sb.AppendFormat("<jzlx>{0}</jzlx>\r\n", "1");
            sb.AppendFormat("<jzrq>{0}</jzrq>\r\n", pat.Czrq.ToString().Trim().Substring(0,8).Insert(4, "-").Insert(7, "-"));
            sb.AppendFormat("<xm>{0}</xm>\r\n", pat.Hzxm);
            sb.AppendFormat("<xb>{0}</xb>\r\n", pat.Sex);
            sb.AppendFormat("<tel>{0}</tel>\r\n", pat.Lxdh);
            sb.AppendFormat("<csrq>{0}</csrq>\r\n", pat.Birth.Trim().Insert(4, "-").Insert(7, "-"));


            string sql = @"select WBNR,WJJGDM from CISDB_DATA..EMR_BLWJJGNRK where WJJGDM in('63950bdc-9952-4641-98dc-05bc68b2e071',
      '23e665d8-dbe5-4356-8edd-d37688124b11',
      '1ce56d4d-f776-4f3c-843d-3b4d729ee6cf',
      '64c82ffa-ddaa-4320-9dbc-c70debb75822',
      'de4e7d9e-29df-4e3e-8e2d-bc31ce445e95'    
      ) and SYXH =" + pat.nEmrXh;

            string zd = "";
            string cbzd = "";
            string xbs = "";
            string jws = "";
            string zljg = "";
            DataTable dt=ISql5.GetDataTable(sql);
            if(dt!=null  && dt.Rows.Count>0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["1"].ToString() == "63950bdc-9952-4641-98dc-05bc68b2e071")
                    {
                        zd = dr["0"].ToString();
                    }
                    if (dr["1"].ToString() == "23e665d8-dbe5-4356-8edd-d37688124b11")
                    {
                        cbzd = dr["0"].ToString();
                    }
                    if (dr["1"].ToString() == "1ce56d4d-f776-4f3c-843d-3b4d729ee6cf")
                    {
                        xbs = dr["0"].ToString();
                    }
                    if (dr["1"].ToString() == "64c82ffa-ddaa-4320-9dbc-c70debb75822")
                    {
                        jws = dr["0"].ToString();
                    }
                    if (dr["1"].ToString() == "de4e7d9e-29df-4e3e-8e2d-bc31ce445e95")
                    {
                        zljg = dr["0"].ToString();
                    }

                }
            }
            sb.AppendFormat("<zd>{0}</zd>\r\n", zd);
            sb.AppendFormat("<cbyx>{0}</cbyx>\r\n", cbzd);
            sb.AppendFormat("<xbs>{0}</xbs>\r\n", xbs);
            sb.AppendFormat("<jws>{0}</jws>\r\n", jws);
            sb.AppendFormat("<zljg>{0}</zljg>\r\n", zljg);
            ret = sb.ToString();
            return ret;
        }

        public string ID
        {
            get { return "08569244-35E0-4441-857E-9706C4CA59D6"; }
        }


        static IAdoDb _ISql5;
        /// <summary>
        /// 5.0数据库访问工具
        /// </summary>
        public static IAdoDb ISql5 //---------------------wss
        {
            get
            {
                if (_ISql5 == null)
                {
                    _ISql5 = SqlHelper.GetAdoDb();
                    _ISql5.SqlConnect(FrameWork.Kernel.Enum.SystemType.HT);

                }
                return _ISql5;
            }
        }
    }
}
