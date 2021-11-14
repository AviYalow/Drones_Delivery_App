using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
  public  class BaseStation
    {

        public uint SerialNum { get; init; }
        public string Name { get; set; }
        public Location location { get; set; }
        public uint FreeState { get; set; }
        public List<DroneInCharge> dronesInCharge { get; set; }

    }
}
