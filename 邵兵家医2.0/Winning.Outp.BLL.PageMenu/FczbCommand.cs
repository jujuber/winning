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
    public  class FczbCommand : ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
          
           
            bool ret = false;
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                

                GlobalVariable.HisApp.Prompt.Show("请先选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                //loger.writeLogMessage("执行建档操作");

                IniFile inifile = new IniFile(GlobalVariable.HisApp.Path.ApplicationPath + @"\Config\UrlConfig.ini");
                string registPatUrl = inifile.IniReadValue("Fczb", "FczbUrl");

             
                if (string.IsNullOrWhiteSpace(registPatUrl))
                {
                   
                    GlobalVariable.HisApp.Prompt.Show("房颤专病接口地址未配置，请检查UrlConfig内,Fczb下的子节点FczbUrl！", System.Windows.Forms.MessageBoxButtons.OK);
                    return new RequestResult { Success = ret };
                }
                string url = "";
                url = registPatUrl + string.Format("?hospitalId={0}&patientId={1}&hospitalName={2}&patientName={3}&age={4}&gender={5}&belongCity={6}&physicianName={7}&physicianId={8}&outerSerialNo={9}&hospitalDepartment={10}&_k={11}", 
                       GlobalVariable.HisSys.HospitalCode.Trim(), pat.Patid, GlobalVariable.HisSys.HospitalName.Trim(), pat.Hzxm.Trim(),
                       GlobalFunction.GetAge(pat.Birth, "0", "", "0").Replace("岁","").Replace("月", "").Replace("日", ""), pat.Sex.Trim(), "上海" ,
                       GlobalVariable.DrInfoObj.sYsmc.Trim(), GlobalVariable.DrInfoObj.sYsdm.Trim(), pat.Ghxh,  GlobalVariable.HisApp.User.Dept.DeptName.Trim(), "ho3qcb");


                //string temp = string.Format(@"a=Winning&b={0}&c={1}&d=1&e={2}&f={3}&g={4}&h={5}&i={6}", zgdm, hzxm, ghxh, patid, ksdm, wkzd, zdxx);
                //string url = strurl + EncodeBase64(temp);

               // GlobalVariable.HisApp.Prompt.Show(url, System.Windows.Forms.MessageBoxButtons.OK);

                try
                {
                    System.Diagnostics.Process.Start("chrome", url);

                }
                catch
                {
                    System.Diagnostics.Process.Start( url);
                }

                
            }
            return new RequestResult { Success = ret };
        }

        public string ID
        {
            get { return "1AF8C280-4B34-48E2-8B20-A92FE5B385AA"; }
        }


        private string EncodeBase64(Encoding encode, string source)
        {
            string endstr = source;
            byte[] bytes = encode.GetBytes(source);
            try
            {
                endstr = Convert.ToBase64String(bytes);
            }
            catch
            {
                endstr = source;
            }
            return endstr;
        }
        private string EncodeBase64(string source)
        {
            return EncodeBase64(Encoding.UTF8, source);
        }


    }

  
}
