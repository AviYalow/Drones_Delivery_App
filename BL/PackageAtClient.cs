using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class PackageAtClient
    {
        public int SerialNum { get; set; }
        public Weight_categories WeightCatgory { get; set; }
        public Priority Priority { get; set; }
        public Package_status packageStatus { get; set; }
       
        //The other client in the package.
        //The receiver for the sender and sender for the receiver
        public ClientInPackage client2 { get; set; }


    }
}
