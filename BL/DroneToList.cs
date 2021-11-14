using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneToList
    {
        public uint SerialNumber { get; init; }
        public string Model { get; set; }
        public Weight_categories weightCategory { get; set; }

        public double butrryStatus { get; set; }
        public Drone_status droneStatus { get; set; }
        public Location location { get; set; }
        public uint numPackage { get; set; }

    }
}
