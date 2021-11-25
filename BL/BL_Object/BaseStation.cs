using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    /// <summary>
    /// base station
    /// </summary>
  public  class BaseStation
    {

        public uint SerialNum { get; init; }
        public string Name { get; set; }
        public Location location { get; set; }
        public uint FreeState { get; set; }
        public List<DroneInCharge> dronesInCharge { get; set; }
        public override string ToString()
        {
            String print= "";
            print += $"Siral Number is {SerialNum},\n";
            print += $"The Name is {Name},\n";
            print += $"Location: Latitude:{location.Latitude} Longitude:{location.Longitude},\n";
            print += $"Number of free state: {FreeState},\n";
            print += $"Drone in Charge: {dronesInCharge.Count},\n";
            foreach(DroneInCharge drone in dronesInCharge)
            {
               print += $"Serial number: {drone.SerialNum}, butrry Status: {drone.butrryStatus}\n";
            }
            return print;
        }

    }
}
