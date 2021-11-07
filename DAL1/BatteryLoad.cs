using System;

namespace IDAL
{
    namespace DO
    {
        /// <BtarryLoad>
        /// An entity that will represent a position
        /// for loading drones
        /// </BtarryLoad>
        public struct BatteryLoad
        {
            public int idBaseStation { get; set; }

            public int id_drone { get; set; }

            public DateTime entringDrone { get; init; }
            public DateTime freeDrone { get; init; }

            public override string ToString()
            {
                String printBtarryLoad = "";
                printBtarryLoad += $"ID BaseStation is {idBaseStation},\n";
                printBtarryLoad += $"ID drown is {id_drone}\n";
                printBtarryLoad += $"Time of geting drone {entringDrone}\n";
                printBtarryLoad += $"Time of free drone {freeDrone}\n";
                return printBtarryLoad;
            }
        }

    }
}