using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winning.FrameWork.Core.Common;
using Winning.Outp.Core.Common;
using Winning.Outp.Core;
using Winning.FrameWork.Core.PageMenu;
using Winning.Outp.DAL.PatInfo.DataObject;

namespace Winning.Outp.BLL.PageMenu
{
    /// <summary>
    /// 此类主要供常用的获取上下文变量值的通用方法
    /// </summary>
    public class ContextValueHelper
    {
        /// <summary>
        /// 获取病人对象，门诊目前只有病人列表和其他录入界面有差别
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static PatBasicInfo GetPatientObj()
        {
            PatBasicInfo pat = null;
            ContextItemInfo obj = GlobalVariable.MenuContextManagerObj.GetContext(GlobalCacheKeys.Page_Context_PatientInfoKey) as ContextItemInfo;
            if (obj != null)
            {
                if (obj.ValueSourceType == SourceOfContextValueType.PublicVariable)
                    pat = GlobalVariable.PatInfoObj.CurrPatinfo;
                else if (obj.ValueSourceType == SourceOfContextValueType.KeyValue)
                    pat = obj.Value as PatBasicInfo;
            }
            return pat;
        }
    }
}
