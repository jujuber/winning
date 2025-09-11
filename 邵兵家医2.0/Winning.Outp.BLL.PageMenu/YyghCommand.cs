using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.Model.Outp;
using Winning.Outp.Core;
using Winning.Outp.Core.Common;
using Winning.FrameWork.Core.Common;
using Winning.FrameWork.Kernel.Enum;
using System.Diagnostics;
using Winning.Model.Common;
using Winning.FrameWork.IDAL;
using System.Security.Cryptography;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    ///  预约挂号（网上预约）
    /// </summary>
    public class YyghCommand:ICommand
    {
        public FrameWork.Core.Common.RequestResult Execute(object sender, EventArgs e)
        {
            var pat = ContextValueHelper.GetPatientObj();
            if (pat == null)
            {
                GlobalVariable.HisApp.Prompt.Show("请选择一个病人！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            else
            {
                string _HT601 = GlobalVariable.HisApp.Config.Get("HT601").Trim();
                string url = _HT601;
                string ysxb="";          
                 var entityList= DataHelper.DataObj.QueryTable<SYS_ZGDMK>(SystemType.H0, p => p.ID == GlobalVariable.DrInfoObj.sYsdm);
                    if (entityList.Count>0) 
                        ysxb = entityList[0].SEX.ToString()=="1"?"0":"1";
                ///md5加密
                string czy_md5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(GlobalVariable.DrInfoObj.sYsdm.Trim(), "MD5");
                string HospitalCode = GetHospitalCode();                
                byte[] buffer = Encoding.GetEncoding("utf-8").GetBytes(pat.Hzxm.Trim());
                string hzxm = "";

                foreach (byte b in buffer)
                    hzxm += string.Format("%{0:X}", b);
                byte[] buffer1 = Encoding.GetEncoding("utf-8").GetBytes(GlobalVariable.DrInfoObj.sYsmc.Trim());
                string ysxm = "";

                foreach (byte b in buffer1)
                    ysxm += string.Format("%{0:X}", b);
                if (!string.IsNullOrWhiteSpace(_HT601))
                {
                    if (!_HT601.EndsWith("?"))
                    {
                        url = _HT601 + "?";
                    }                       
                    url = string.Format("{0}operNo={1}&operName={2}&operSex={3}&patientIdCard={4}&patientName={5}&patientMobile={6}&captcha={7}&orgCode={8}&topType=1",
                        url, GlobalVariable.DrInfoObj.sYsdm,ysxm, ysxb, pat.Sfzh,hzxm, pat.Lxdh, czy_md5, HospitalCode);
                    Process.Start(url);
                }
                else
                    GlobalVariable.HisApp.Prompt.Show("预约挂号url地址[HT601]未设置，请联系系统管理员！", System.Windows.Forms.MessageBoxButtons.OK);
            }
            return new FrameWork.Core.Common.RequestResult { Success = true };
        }

        public string ID
        {
            get { return MenuCommandId.YyghCommandID; }
        }       
        /// <summary>
        /// 获取医院代码
        /// </summary>
        private string GetHospitalCode()
        {
            string ret = string.Empty;
            try
            {
                List<SYS_HOSPITAL> RoleList = DataHelper.DataObj.QueryTable<SYS_HOSPITAL>(Winning.FrameWork.Kernel.Enum.SystemType.H0);
                ret = RoleList[0].YLJGDM.ToString().Trim();
            }
            catch
            {
                ret = string.Empty;
            }
            return ret;
        }
    }
}
