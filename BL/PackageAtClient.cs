using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class PackageAtClient
    {
        public int SerialNum { get; set; }
        public WeightCategories WeightCatgory { get; set; }
        public Priority Priority { get; set; }
        public PackageStatus packageStatus { get; set; }
       
        //The other client in the package.
        //The receiver for the sender and sender for the receiver
        public ClientInPackage client2 { get; set; }


    }
}
