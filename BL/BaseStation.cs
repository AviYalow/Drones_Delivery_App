using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class BaseStation
    {

        public int SerialNum { get; init; }
        public string Name { get; set; }
        public Location location { get; set; }
        public int FreeState { get; set; }
        public List<DroneInCharge> dronesInCharge { get; set; }

    }
}
