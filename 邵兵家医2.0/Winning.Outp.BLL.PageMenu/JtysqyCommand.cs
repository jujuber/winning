using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using System.Windows.Forms;
using System.Drawing;
using Winning.Outp.DAL.PatInfo.DataObject;
using System.Web;
using System.Diagnostics;
using Winning.FrameWork.IDAL;
using Winning.FrameWork.Kernel.Enum;
using Winning.Model.Common;
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 1320088 家庭医生站中1+1+1签约，转诊，延伸处方接口改造
    /// 家庭医生签约
    /// </summary>
    public class JtysqyCommand : ICommand
    {
//        INSERT[dbo].[PUB_MENU_ITEMINFO]
//        ([MENUITEMID], [MENUITEMNAME], [PY], [WB], [COMMANDID], [CATEGORY], [CATEGORYNAME], [MEMO], [COLOR], [ICON], [DEFAULTICON], [XTDLDM], [JLZT], [PAGEID], [TIPINFO])
//VALUES(CAST(1001 AS Decimal(12, 0)), N'家庭医生签约', N'jtysqy', N'sybgk', N'3CBAC191-6534-4DCC-93E8-4B0E17DDDEA6', 0, N'按钮', N'家庭医生签约', N' ', N'MenuIcon\单据.png', N'MenuIcon\单据.png', N'HT', 1, NULL, NULL)


        public string ID
        {
            get { return "3CBAC191-6534-4DCC-93E8-4B0E17DDDEA6"; }
        }

        /// <summary>
        /// 随机数
        /// </summary>
        public int RandomNumber { get; set; }

        /// <summary>
        /// 签约url
        /// </summary>
        public string QyUrl { get; set; }

        /// <summary>
        /// 认证服务地址
        /// </summary>
        public string RzfwUrl { get; set; }
        /// <summary>
        /// 转诊url
        /// </summary>
        public string ZzUrl { get; set; }

        /// <summary>
        /// 延伸处方url
        /// </summary>
        public string YscfUrl { get; set; }
        /// <summary>
        /// 续约url
        /// </summary>
        public string XyUrl { get; set; }        
        /// <summary>
        /// 签约状态查询url
        /// </summary>
        public string QyztcxUrl { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 医疗机构名称
        /// </summary>
        public string Yljgmc { get; set; }

        /// <summary>
        /// 医疗机构代码
        /// </summary>
        public string Yljgdm { get; set; }

        public string Appid { get; set; }
        public string AppSecret { get; set; }

        /// <summary>
        /// 医生身份证号
        /// </summary>
        public string Yssfzh { get; set; }


        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            // 创建一个Random实例
            Random random = new Random();
            // 生成一个0到maxRandomNumber之间的随机数
            RandomNumber = random.Next(999999999);

            bool ret = false;
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                SYS_ZGDMK _Zginfo = DataHelper.DataObj.QueryTable<SYS_ZGDMK>(SystemType.H0, p => p.ID.Trim() == GlobalVariable.DrInfoObj.sYsdm.Trim() && p.JLZT == true).FirstOrDefault();
                if (_Zginfo != null)
                {
                    Yssfzh = _Zginfo.SFZH;
                }


                //; 1320088 家庭医生站中1 + 1 + 1签约，转诊，延伸处方接口改造
                //[JTYSQY]
                //; 家庭医生签约地址
                //QyUrl = http://10.85.69.194:9080/jtysqy/access.action?access=
                //; 预约转诊url地址
                //ZzUrl = http://10.85.69.195:9080/yyzz_dws/access.action?access=
                //; 延伸处方url地址
                //YscfUrl = http://10.85.69.194:9080/cfys/access.action?access=
                //; 家庭医生续约url地址
                //XyUrl = http://10.85.69.194:9080/fjzlqyDist/ieqy?
                //; 签约状态查询url
                //QyztcxUrl = http://xxx.xxx.xxx.xxx:xxxx/sqzgservice/v3/service
                //Appid = 4321
                //Appsecret = 876543
                //Account = 111
                //Password = 111
                //Yljgmc = 医疗机构名称
                //Yljgdm = 医疗机构代码
                //; 认证服务地址
                //Rzfwurl = http://10.85.69.194:9080/sqzgservice/v3/encryption/requestAccess?
                IniFile inifile = new IniFile(GlobalVariable.HisApp.Path.ApplicationPath + @"\Config\UrlConfig.ini");
                QyUrl = inifile.IniReadValue("JTYSQY", "QyUrl");
                ZzUrl = inifile.IniReadValue("JTYSQY", "ZzUrl");
                Appid = inifile.IniReadValue("JTYSQY", "Appid");
                AppSecret = inifile.IniReadValue("JTYSQY", "Appsecret");
                Account = inifile.IniReadValue("JTYSQY", "Account");
                Password = inifile.IniReadValue("JTYSQY", "Password");
                Yljgmc = inifile.IniReadValue("JTYSQY", "Yljgmc");
                Yljgdm = inifile.IniReadValue("JTYSQY", "Yljgdm");
                RzfwUrl = inifile.IniReadValue("JTYSQY", "Rzfwurl");
                YscfUrl = inifile.IniReadValue("JTYSQY", "YscfUrl");
                XyUrl = inifile.IniReadValue("JTYSQY", "XyUrl");
                QyztcxUrl = inifile.IniReadValue("JTYSQY", "QyztcxUrl");

                WriteLog("获取UrlConfig-JTYSQY-QyUrl=" + QyUrl);
                WriteLog("获取UrlConfig-JTYSQY-ZzUrl=" + ZzUrl);
                WriteLog("获取UrlConfig-JTYSQY-Appid=" + Appid);
                WriteLog("获取UrlConfig-JTYSQY-Appsecret=" + AppSecret);
                WriteLog("获取UrlConfig-JTYSQY-Account=" + Account);
                WriteLog("获取UrlConfig-JTYSQY-Password=" + Password);
                WriteLog("获取UrlConfig-JTYSQY-Yljgmc=" + Yljgmc);
                WriteLog("获取UrlConfig-JTYSQY-Yljgdm=" + Yljgdm);
                WriteLog("获取UrlConfig-JTYSQY-Rzfwurl=" + RzfwUrl);
                WriteLog("获取UrlConfig-JTYSQY-YscfUrl=" + YscfUrl);
                WriteLog("获取UrlConfig-JTYSQY-XyUrl=" + XyUrl);
                WriteLog("获取UrlConfig-JTYSQY-QyztcxUrl=" + QyztcxUrl);
                GetMenu();
            }
            return new FrameWork.Core.Common.RequestResult { Success = ret };
        }

        public void GetMenu()
        {
            List<MenuInfo> sMenu = new List<MenuInfo> {
                new MenuInfo{ Name = "家庭医生签约", Command = 1 },
                new MenuInfo{ Name = "延伸处方", Command = 2},
                new MenuInfo{ Name = "预约转诊", Command = 3 },
                new MenuInfo{ Name = "家庭医生续约", Command = 4},
                new MenuInfo{ Name = "签约状态查询", Command = 5}
            };
            ContextMenuStrip myMenu = new ContextMenuStrip();
            myMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            myMenu.Size = new System.Drawing.Size(61, 4);
            myMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(ItemClicked);
            for (int i = 0; i < sMenu.Count; i++)
            {
                myMenu.Items.Add(sMenu[i].Name);
                myMenu.Items[i].Tag = sMenu[i].Command;
            }
            Point position = new Point(System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y);
            myMenu.Show(position);
        }

        private void ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                var pat = ContextValueHelper.GetPatientObj();
                int button = Convert.ToInt32(e.ClickedItem.Tag);
                if (button == 1)
                    JtysqyMenu(pat);
                else if (button == 2)
                    YscfMenu(pat);
                else if (button == 3)
                    YyzzMenu(pat);
                else if (button == 4)
                    JtysxyMenu(pat);
                else if (button == 5)
                    QyztcxMenu(pat);
            }
            catch (Exception ex)
            {
                MessageBox.Show("点击家庭医生签约菜单按钮异常；" + ex.Message+ex.StackTrace);
            }

        }

        /// <summary>
        /// 家庭医生签约
        /// </summary>
        /// <param name="pat"></param>
        private void JtysqyMenu(PatBasicInfo pat)
        {
            WriteLog("------------------------------------家庭医生签约按钮开始：------------------------------------");
            UrlParams urlParas = SetParams(pat);
            StringBuilder sbl = new StringBuilder();
            sbl.Append("yljg=" + urlParas.yljg); 
            sbl.Append("&idcard=" + urlParas.idcard);
            sbl.Append("&sfzh=" + urlParas.sfzh);
            sbl.Append("&sbkh=" + urlParas.sbkh);
            sbl.Append("&ybkh=" + urlParas.ybkh);
            sbl.Append("&zfkh=" + urlParas.zfkh);
            sbl.Append("&hzxm=" + urlParas.hzxm);
            sbl.Append("&hzsex=" + urlParas.hzsex);
            sbl.Append("&hzcsrq=" + urlParas.hzcsrq);
            sbl.Append("&yszyzh=" + urlParas.yszyzh);
            sbl.Append("&gpid=" + urlParas.gpid);
            sbl.Append("&gpmc=" + urlParas.gpmc);
            sbl.Append("&czrybm=" + urlParas.czrybm);
            sbl.Append("&czryxm=" + urlParas.czryxm);
            sbl.Append("&ksbm=" + urlParas.ksbm);
            sbl.Append("&ksmc=" + urlParas.ksmc);
            sbl.Append("&agentip=" + urlParas.agentip);
            sbl.Append("&agentmac=" + urlParas.agentmac);
            sbl.Append("&jsessionid=" + urlParas.jsessionid);
            string _params = sbl.ToString();
            WriteLog("家庭医生签约按钮Url原参数：" + _params);
            string encodedParams = HttpUtility.UrlEncode(_params);
            WriteLog("家庭医生签约按钮UrlEncode参数：" + encodedParams);


            RzfwParams rzfw = new RzfwParams();
            rzfw.url = encodedParams;
            rzfw.userid = Account;
            rzfw.dymm = Password;
            rzfw.apptype = "01";
            string input_params = Newtonsoft.Json.JsonConvert.SerializeObject(rzfw);
            WriteLog("家庭医生签约按钮-post请求入参：" + input_params);
            //string res = HttpPost(RzfwUrl, input_params);
            string res = Post(RzfwUrl, Account, Password,"01",encodedParams);
            WriteLog("家庭医生签约按钮-post请求结束，返回内容：" + res);
            ResultContent rec = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultContent>(res);
            if (rec != null)
            {
                if (rec.code == "200")
                {
                    string url_Params = QyUrl + rec.msg;
                    WriteLog("家庭医生签约按钮-拼接url和参数：" + url_Params);
                    WriteLog("默认浏览器打开");
                    Process.Start("IEXPLORE.EXE", url_Params);
                }
            }
            else
            {
                WriteLog("家庭医生签约无返回数据！");
            }


            WriteLog("家庭医生签约按钮结束");
        }

        /// <summary>
        /// 延伸处方
        /// </summary>
        private void YscfMenu(PatBasicInfo pat)
        {
            WriteLog("------------------------------------延伸处方按钮开始：---------------------------------");
            UrlParams urlParas = SetParams(pat);
            StringBuilder sbl = new StringBuilder();
            sbl.Append("yljg=" + urlParas.yljg);
            sbl.Append("&idcard=" + urlParas.idcard);
            sbl.Append("&sfzh=" + urlParas.sfzh);
            sbl.Append("&sbkh=" + urlParas.sbkh);
            sbl.Append("&ybkh=" + urlParas.ybkh);
            sbl.Append("&zfkh=" + urlParas.zfkh);
            sbl.Append("&hzxm=" + urlParas.hzxm);
            sbl.Append("&hzsex=" + urlParas.hzsex);
            sbl.Append("&hzcsrq=" + urlParas.hzcsrq);
            sbl.Append("&yszyzh=" + urlParas.yszyzh);
            sbl.Append("&gpid=" + urlParas.gpid);
            sbl.Append("&gpmc=" + urlParas.gpmc);
            sbl.Append("&czrybm=" + urlParas.czrybm);
            sbl.Append("&czryxm=" + urlParas.czryxm);
            sbl.Append("&ksbm=" + urlParas.ksbm);
            sbl.Append("&ksmc=" + urlParas.ksmc);
            sbl.Append("&agentip=" + urlParas.agentip);
            sbl.Append("&agentmac=" + urlParas.agentmac);
            sbl.Append("&jsessionid=" + urlParas.jsessionid);
            string _params = sbl.ToString();
            WriteLog("延伸处方按钮Url原参数：" + _params);
            string encodedParams = HttpUtility.UrlEncode(_params);
            WriteLog("延伸处方按钮UrlEncode参数：" + encodedParams);


            RzfwParams rzfw = new RzfwParams();
            rzfw.url = encodedParams;
            rzfw.userid = Account;
            rzfw.dymm = Password;
            rzfw.apptype = "01";
            string input_params = Newtonsoft.Json.JsonConvert.SerializeObject(rzfw);
            WriteLog("延伸处方按钮-post请求入参：" + input_params);
            //string res = HttpPost(RzfwUrl, input_params);
            string res = Post(RzfwUrl, Account, Password, "01", encodedParams);
            WriteLog("延伸处方按钮-post请求结束，返回内容：" + res);
            ResultContent rec = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultContent>(res);


            if (rec != null)
            {
                if (rec.code == "200")
                {
                    string url_Params = YscfUrl + rec.msg;
                    WriteLog("延伸处方按钮-拼接url和参数：" + url_Params);
                    WriteLog("延伸处方默认浏览器打开");
                    Process.Start(url_Params);
                }
            }
            else
            {
                WriteLog("延伸处方无返回数据！");
            }

            WriteLog("延伸处方按钮结束");
        }

        /// <summary>
        /// 预约转诊
        /// </summary>
        private void YyzzMenu(PatBasicInfo pat)
        {
            WriteLog("------------------------------------预约转诊按钮开始：---------------------------------");
            UrlParams urlParas = SetParams(pat);
            StringBuilder sbl = new StringBuilder();
            sbl.Append("yljg=" + urlParas.yljg);
            sbl.Append("&idcard=" + urlParas.idcard);
            sbl.Append("&sfzh=" + urlParas.sfzh);
            sbl.Append("&sbkh=" + urlParas.sbkh);
            sbl.Append("&ybkh=" + urlParas.ybkh);
            sbl.Append("&zfkh=" + urlParas.zfkh);
            sbl.Append("&hzxm=" + urlParas.hzxm);
            sbl.Append("&hzsex=" + urlParas.hzsex);
            sbl.Append("&hzcsrq=" + urlParas.hzcsrq);
            sbl.Append("&yszyzh=" + urlParas.yszyzh);
            sbl.Append("&gpid=" + urlParas.gpid);
            sbl.Append("&gpmc=" + urlParas.gpmc);
            sbl.Append("&czrybm=" + urlParas.czrybm);
            sbl.Append("&czryxm=" + urlParas.czryxm);
            sbl.Append("&ksbm=" + urlParas.ksbm);
            sbl.Append("&ksmc=" + urlParas.ksmc);
            sbl.Append("&agentip=" + urlParas.agentip);
            sbl.Append("&agentmac=" + urlParas.agentmac);
            sbl.Append("&jsessionid=" + urlParas.jsessionid);
            string _params = sbl.ToString();
            WriteLog("预约转诊按钮Url原参数：" + _params);
            string encodedParams = HttpUtility.UrlEncode(_params);
            WriteLog("预约转诊按钮UrlEncode参数：" + encodedParams);


            RzfwParams rzfw = new RzfwParams();
            rzfw.url = encodedParams;
            rzfw.userid = Account;
            rzfw.dymm = Password;
            rzfw.apptype = "01";
            string input_params = Newtonsoft.Json.JsonConvert.SerializeObject(rzfw);
            WriteLog("预约转诊按钮-post请求入参：" + input_params);
            //string res = HttpPost(RzfwUrl, input_params);
            string res = Post(RzfwUrl, Account, Password, "01", encodedParams);
            WriteLog("预约转诊按钮-post请求结束，返回内容：" + res);
            ResultContent rec = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultContent>(res);

            if (rec != null)
            {
                if (rec.code == "200")
                {
                    string url_Params = ZzUrl + rec.msg;
                    WriteLog("预约转诊按钮-拼接url和参数：" + url_Params);
                    WriteLog("预约转诊默认浏览器打开");
                    Process.Start(url_Params);
                }
            }
            else
            {
                WriteLog("预约转诊无返回数据！");
            }

            WriteLog("预约转诊按钮结束");
        }


        /// <summary>
        /// 家庭医生续约
        /// </summary>
        private void JtysxyMenu(PatBasicInfo pat)
        {
            WriteLog("------------------------------------家庭医生续约按钮开始：---------------------------------");

            long timestamp = DateTimeToLongTimeStamp(DateTime.UtcNow);
            WriteLog("家庭医生续约按钮-appid：" + Appid);
            WriteLog("家庭医生续约按钮-appSecret：" + AppSecret);
            WriteLog("家庭医生续约按钮-timestamp：" + timestamp);
     
            string input = AppSecret + timestamp;
            WriteLog("家庭医生续约按钮-signature加密前：" + input);
            string hash = ComputeSha256Hash(input);
            WriteLog("家庭医生续约按钮-signature加密后：" + hash);

            StringBuilder sbl = new StringBuilder();
            sbl.Append("jgdm=" + Yljgdm);
            sbl.Append("&gpid=" + GlobalVariable.DrInfoObj.sYsdm);
            sbl.Append("&sfzh=" + pat.Sfzh);
            sbl.Append("&ywid=" + "renew");
            sbl.Append("&appid=" + Appid);
            sbl.Append("&signature=" + hash);
            sbl.Append("&timestamp=" + timestamp);
            string _params = sbl.ToString();
            WriteLog("家庭医生续约按钮-拼接入参：" + _params);
            string getUrl = XyUrl + _params;
            WriteLog("家庭医生续约按钮-请求GetUrl：" + getUrl);
            string gerResult = HttpGet(getUrl);
            WriteLog("家庭医生续约按钮-请求GetUrl返回：" + gerResult);

            WriteLog("家庭医生续约按钮结束");

        }


        /// <summary>
        /// 签约状态查询
        /// </summary>
        private void QyztcxMenu(PatBasicInfo pat)
        {
            WriteLog("------------------------------------签约状态查询按钮开始：---------------------------------");

            StringBuilder sbl = new StringBuilder();
            sbl.AppendFormat("<ws:getQyState>");
            sbl.AppendFormat("<idcard>{0}</idcard>",pat.Sfzh);
            sbl.AppendFormat("</ws:getQyState>");
            string xmlParams = sbl.ToString();
            WriteLog("签约状态查询按钮拼接xml入参：" + xmlParams);
            WriteLog("签约状态查询按钮webservice调用getQyState：" );
            string result = InvokeWebService(QyztcxUrl, "getQyState", xmlParams);
            WriteLog("签约状态查询按钮webservice调用getQyState返回：" + result);
            ResultContent rec = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultContent>(result);
            string showMsg = string.Empty;
            if (rec != null)
            {
                switch (rec.result)
                {
                    case "-2":
                        showMsg = "接口调用出错！";
                        break;
                    case "-1":
                        showMsg = "当前患者未签约！";
                        break;
                    case "0":
                        showMsg = "当前患者签约尚未生效！";
                        break;
                    case "1":
                        showMsg = "当前患者签约生效！";
                        break;
                    case "3":
                        showMsg = "当前患者已解约！";
                        break;
                }
                MessageBox.Show(showMsg);
            }

            WriteLog("签约状态查询按钮结束");
        }

        private string InvokeWebService(string url, string method, string inputdata, int TimeOut = 5)
        {
            string result = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //超时时间
                request.Timeout = TimeOut * 1000;
                request.ReadWriteTimeout = TimeOut * 1000;

                request.Headers.Add(@"SOAPAction:http://tempuri.org/" + method);
                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";
                string soapBody = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ws=""http://ws.qy.wondersgroup.com/"">
									<soapenv:Header/>
									   <soapenv:Body>{0}</soapenv:Body>
									</soapenv:Envelope>";
                soapBody = string.Format(soapBody, inputdata);

                WriteLog(method + " SOAP调用入参：" + soapBody);
                using (Stream stream = request.GetRequestStream())
                {
                    using (StreamWriter streamWriter = new StreamWriter(stream))
                    {
                        streamWriter.Write(soapBody);
                    }
                }
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        result = System.Web.HttpUtility.HtmlDecode(rd.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog("SOAP调用服务异常：" + ex.Message + ex.StackTrace);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// SHA256散列算法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ComputeSha256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="dateTime">当前时间 DateTime.UtcNow</param>
        /// <returns></returns>
        private long DateTimeToLongTimeStamp(DateTime dateTime)
        {
            DateTime timeStampStartTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(dateTime.ToUniversalTime() - timeStampStartTime).TotalMilliseconds;
        }




        public void WriteLog(string msg)
        {
            string logPath = AppDomain.CurrentDomain.BaseDirectory + "Log\\家庭医生签约_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            try
            {
                using (System.IO.StreamWriter sw = System.IO.File.AppendText(logPath))
                {
                    sw.WriteLine("消息：" + msg);
                    sw.WriteLine("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    sw.WriteLine("**************************************************");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (System.IO.IOException e)
            {
                using (System.IO.StreamWriter sw = System.IO.File.AppendText(logPath))
                {
                    sw.WriteLine("异常：" + e.Message);
                    sw.WriteLine("时间：" + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss.fff"));
                    sw.WriteLine("**************************************************");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
        }

        public UrlParams SetParams(PatBasicInfo pat)
        {
            UrlParams _parmss = new UrlParams();
            _parmss.yljg = Yljgdm;
            _parmss.idcard = pat.Sfzh;
            _parmss.sfzh = pat.Sfzh;
            _parmss.sbkh = pat.Cardno;
            _parmss.ybkh = pat.Cardno;
            _parmss.zfkh = pat.Cardno;
            _parmss.hzxm = pat.Hzxm;
            _parmss.hzsex = pat.Sex;
            _parmss.hzcsrq = pat.Birth;
            _parmss.yszyzh = Yssfzh;
            _parmss.gpid = GlobalVariable.DrInfoObj.sYsdm;
            _parmss.gpmc = GlobalVariable.DrInfoObj.sYsmc;
            _parmss.czrybm = GlobalVariable.DrInfoObj.sYsdm;
            _parmss.czryxm = GlobalVariable.DrInfoObj.sYsmc;
            _parmss.ksbm = GlobalVariable.DrInfoObj.sDbKsdm;
            _parmss.ksmc = GlobalVariable.DrInfoObj.sDbKsmc;
            _parmss.agentip = GlobalVariable.HisApp.Utils.uComputer.IpAddress;
            _parmss.agentmac = GlobalVariable.HisApp.Utils.uComputer.NetAddress; ;
            _parmss.jsessionid = RandomNumber.ToString();

            return _parmss;
        }

        public string Post(string url,string userid,string dymm,string apptype,string urlParams)
        {
            string tempMessage = "";
            try
            {
                System.Net.WebClient WebClientObj = new System.Net.WebClient();
                Dictionary<string, string> Params = new Dictionary<string, string>();
                Params.Add("userid", userid);
                Params.Add("dymm", dymm);
                Params.Add("apptype", apptype);
                Params.Add("url", urlParams);
                System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
                foreach (var item in Params)
                {
                    PostVars.Add(item.Key, item.Value);
                }
                byte[] byRemoteInfo = WebClientObj.UploadValues(url, "POST", PostVars);
                tempMessage = System.Text.Encoding.UTF8.GetString(byRemoteInfo);
     
            }
            catch (Exception ex)
            {
                WriteLog($"家庭医生签约接口调用错误：{ex.Message}{ex.StackTrace}");
            }
            return tempMessage;
        }

        public string HttpPost(string Url, string paraUrlCoded, int timeOut = 5)
        {
            string retString = "";
            if (string.IsNullOrWhiteSpace(Url))
            {
                MessageBox.Show("服务接口地址未配置！");
                return retString;
            }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                if (timeOut > 0)
                    request.Timeout = timeOut * 1000;
                request.Method = "POST";
                request.ContentType = "application/json";
                //request.Headers.Add("hsbToken", token);
                //request.Headers.Add("Content-Type", "application/json");
                byte[] payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);

                request.ContentLength = payload.Length;
                Stream myRequestStream = request.GetRequestStream();
                myRequestStream.Write(payload, 0, payload.Length);
                myRequestStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                WriteLog($"家庭医生签约接口调用错误：{ex.Message}{ex.StackTrace}");
            }

            return retString;
        }

        private  string HttpGet(string url)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Method = "GET";
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sReader = new StreamReader(responseStream))
                    {
                        result = sReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
                WriteLog($"家庭医生签约Get方式调用错误：{ex.Message}{ex.StackTrace}");
            }
            return result;
        }
    }


    public class ResultContent
    {
        public string msg { get; set; }
        public string code { get; set; }

        public string result { get; set; }

        public string message { get; set; }
    }

    /// <summary>
    /// 认证服务参数
    /// </summary>
    public class RzfwParams
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 认证密码
        /// </summary>
        public string dymm { get; set; }

        public string apptype { get; set; }

        public string url { get; set; }
    }
    public class UrlParams
    {
        /// <summary>
        /// 医疗机构代码
        /// </summary>
        public string yljg { get; set; }

        /// <summary>
        /// NFC卡号
        /// </summary>
        public string idcard { get; set; }


        /// <summary>
        /// 身份证
        /// </summary>
        public string sfzh { get; set; }
        /// <summary>
        /// 社保卡号
        /// </summary>
        public string sbkh { get; set; }

        /// <summary>
        /// 医保卡号
        /// </summary>
        public string ybkh { get; set; }

        /// <summary>
        /// 统一自费卡
        /// </summary>
        public string zfkh { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string hzxm { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        public string hzsex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string hzcsrq { get; set; }

        /// <summary>
        /// 医生身份证号
        /// </summary>
        public string yszyzh { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        public string gpid { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string gpmc { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        public string czrybm { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string czryxm { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string ksbm { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 操作电脑IP
        /// </summary>
        public string agentip { get; set; }

        /// <summary>
        /// 操作电脑MAC
        /// </summary>
        public string agentmac { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string jsessionid { get; set; }

    }
}
