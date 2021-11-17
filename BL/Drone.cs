using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
   public class Drone
    {
        public uint SerialNum { get; init; }
        public string Model { get; set; }

        public WeightCategories weightCategory { get; set; }
        public Package packageInTransfer { get; set; }

        public double butrryStatus { get; set; }
        public DroneStatus droneStatus { get; set; }
        public Location location { get; set; }
        

    }
}
