using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class DeliveryInTransfer
    {
        public int SerialNum { get; set; }
        public Weight_categories WeightCatgory { get; set; }
        public Priority Priority { get; set; }
        public bool InTheWay { get; set; }//true-in the way,false-waiting to be collected
        public Location Source { get; set; }
        public Location Destination { get; set; }
        public double Distance { get; set; }


    } 
}
