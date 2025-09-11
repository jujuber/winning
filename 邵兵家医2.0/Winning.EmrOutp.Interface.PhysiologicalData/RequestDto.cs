using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Winning.EmrOutp.Interface.PhysiologicalData
{
    public class RequestDto
    {
        public string personcard { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public List<measure> measures { get; set; }
    }
    public class measure
    {
        public string measureType { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }

    }

    public class ResponseDto
    {
        public string code { get; set; }

        public string msg { get; set; }

        public List<PageMeasure> page { get; set; }


    }
    public class PageMeasure : measure
    {
        public int total { get; set; }
        public List<data> dataList { get; set; }
    }
    public class data
    {
            public string id { get; set; }
            public string personcard { get; set; }
            public string name { get; set; }
            public string gender { get; set; }
            public string birth { get; set; }
            public string measureType { get; set; }
            public string measureData { get; set; }
            public string measureTime { get; set; }
            public string measureSourceId { get; set; }
            public string measureLocation { get; set; }
            public string measureOrgId { get; set; }
            public string measureMode { get; set; }
            public string deviceId { get; set; }
            public string deviceType { get; set; }
            public string measureDoc { get; set; }
            public string networkStatus { get; set; }
    }
}
