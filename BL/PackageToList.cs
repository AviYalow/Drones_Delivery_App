using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class PackageToList
    {
        public int SerialNumber { get; set; }
        public string SendClient { get; set; }
        public string RecivedClient { get; set; }
        public Package_status packageStatus { get; set; }
    }
}
