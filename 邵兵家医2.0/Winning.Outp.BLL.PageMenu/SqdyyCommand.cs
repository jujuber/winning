using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Winning.Framework;
using Newtonsoft.Json;
using Winning.Outp.Core;
using Winning.Outp.DAL.PatInfo.DataObject;
//using Winning.Outp.BaseCore;
//using Winning.Framework.Presentation;
using Winning.FrameWork.Core.Common;
using System.Net;
using System.IO;
using System.Web.Services.Description;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Data;
using Winning.FrameWork.DAL.Kernel;
using Winning.FrameWork.Kernel.Enum;


namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 外院申请单
    /// </summary>
    public class SqdyyCommand : ICommand
    {
        public RequestResult Execute(object sender, EventArgs e)
        {
            LogWriter loger = LogWriter.CreateLoger("外院申请单调用日志");

            loger.writeLogMessage("开始执行");
            bool ret = false;
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                loger.writeLogMessage("未选择病人,返回");

                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                loger.writeLogMessage("执行建档操作");

                IniFile inifile = new IniFile(GlobalVariable.HisApp.Path.ApplicationPath + @"/UrlConfig.ini");
                string registPatUrl = inifile.IniReadValue("OutHospySqd", "RegistPat");

                loger.writeLogMessage("建档服务接口地址：" + registPatUrl);

                if (string.IsNullOrWhiteSpace(registPatUrl))
                {
                    loger.writeLogMessage("建档服务接口地址未配置，请检查UrlConfig内,OutHospySqd下的子节点RegistPat");
                    GlobalVariable.HisApp.Prompt.Show("建档服务接口地址未配置，请检查UrlConfig内,OutHospySqd下的子节点RegistPat！", System.Windows.Forms.MessageBoxButtons.OK);
                    return new RequestResult { Success = ret };
                }


                Prescription prescription = new Prescription(registPatUrl);

                string nXml = "{\"hzxm\":\"" + pat.Hzxm +
                    "\",\"cardtype\":\"" + pat.Cardtype +
                    "\",\"cardno\":\"" + pat.Cardno +
                    "\",\"ybdm\":\"" + pat.Ybdm +
                    "\",\"sex\":\"" + pat.Sex +
                    "\",\"sfzh\":\"" + pat.Sfzh +
                    "\",\"birth\":\"" + pat.Birth +
                    "\",\"lxdz\":\"" + pat.Lxdz +
                    "\",\"lxdh\":\"" + pat.Lxdh +
                    "\",\"memo\":\"" + pat.Memo +
                    "\",\"jhrsfzh\":\"\"}";

                string InXml = "{\"request\":{\"timestamp\":\"\",\"params\":[" + nXml + "]}}";

                loger.writeLogMessage("入参：" + "RY002");
                loger.writeLogMessage("入参：" + InXml);
                object obj = prescription.PrescriptionBusiness("RY002", InXml);
                loger.writeLogMessage("建档返回：" + (obj ?? "").ToString());

                if (string.IsNullOrWhiteSpace((obj ?? "").ToString()))
                {
                    loger.writeLogMessage("建档失败,返回空");
                    System.Windows.Forms.MessageBox.Show("建档失败,返回空");
                    return new RequestResult { Success = ret };
                }

                loger.writeLogMessage("解析返回结果");
                Root root = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(obj.ToString());
                loger.writeLogMessage("解析返回结果完成");

                if (root.Response.resultCode == "0")
                {
                    loger.writeLogMessage("建档完成,调用开单界面");

                    string sqdUrl = inifile.IniReadValue("OutHospySqd", "WebSqdUrl");

                    if (string.IsNullOrWhiteSpace((sqdUrl ?? "").ToString()))
                    {
                        loger.writeLogMessage("外院申请单开单接口地址未配置，请检查UrlConfig内,OutHospySqd下的子节点WebSqdUrl");
                        GlobalVariable.HisApp.Prompt.Show("外院申请单开单接口地址未配置，请检查UrlConfig内,OutHospySqd下的子节点WebSqdUrl！", System.Windows.Forms.MessageBoxButtons.OK);
                        return new RequestResult { Success = ret };
                    }


                    sqdUrl = sqdUrl.Replace("[patid]", root.Response.resultMessage.patid).Replace("[PATID]", root.Response.resultMessage.patid);
                    sqdUrl = sqdUrl.Replace("[ysdm]", GlobalVariable.DrInfoObj.sYsdm).Replace("[YSDM]", GlobalVariable.DrInfoObj.sYsdm);
                    loger.writeLogMessage("网页地址：" + sqdUrl);

                    System.Diagnostics.Process.Start("iexplore.exe",sqdUrl);
                }
                else
                {
                    loger.writeLogMessage("建档失败,错误信息：" + (obj ?? ""));
                    System.Windows.Forms.MessageBox.Show("建档失败,错误信息：" + (obj ?? ""));
                }
            }
            return new RequestResult { Success = ret };
        }

        public string ID
        {
            get { return "0CE7E617-DD03-4136-8C7D-6EA3D189AA8D"; }
        }
    }

    public class ResultMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public string Column1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string patid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }
    }

    public class Response
    {
        /// <summary>
        /// 
        /// </summary>
        public string resultCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ResultMessage resultMessage { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public Response Response { get; set; }
    }

}
