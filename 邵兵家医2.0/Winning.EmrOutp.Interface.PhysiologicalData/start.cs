using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Winning.EmrOutp.Interface.PhysiologicalData
{
    class start
    {

        [STAThread]
        static void Main()
        {

          var ss=  PhysiologicalDataService.GetData("",null);
        }
    }
}
