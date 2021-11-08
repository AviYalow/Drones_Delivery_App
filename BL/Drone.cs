using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Drone
    {
        public int SerialNum { get; init; }
        public string Model { get; set; }

        public Weight_categories weightCategory { get; set; }
        public PackageInTransfer packageInTransfer { get; set; }

        public double butrryStatus { get; set; }
        public Drone_status droneStatus { get; set; }
        public Location location { get; set; }


    }
}
