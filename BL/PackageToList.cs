using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class PackageToList
    {
        public uint SerialNumber { get; set; }
        public string SendClient { get; set; }
        public string RecivedClient { get; set; }
        public Priority priority { get; set; }
        public WeightCategories WeightCategories { get; set; }
        public PackageStatus packageStatus { get; set; }
    }
}
