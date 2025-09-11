using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Winning.Outp.External.wdpatsign.Model
{
    internal class OutputDTO
    {
        public string code { get; set; }
        public string msg { get; set; }
        public OutputContent data { get; set; }
    }
    internal class OutputContent
    {
        public string total { get; set; }
        public string pageIndex { get; set; }
        public string msg { get; set; }
        public string pages { get; set; }
        public List<data> dataList { get; set; }
    }
    internal class data
    {
        public string id { get; set; }
        public string personcard { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string birth { get; set; }
        public string measureType { get; set; }
        public string measureTime { get; set; }
        public string measureSourceId { get; set; }
        public string measureLocation { get; set; }
        public string measureOrgId { get; set; }
        public string measureMode { get; set; }
        public string deviceId { get; set; }
        public string deviceType { get; set; }
        public string measureDoc { get; set; }
        public string networkStatus { get; set; }
        public string measureData { get; set; }
    }
}
