using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Winning.Outp.External.wdpatsign.Model
{
    public class InputDTO
    {
        public string personcard { get; set; }
        public string measureType { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string pageIndex { get; set; }
        public string pageSize { get; set; }
    }
}
