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
            public uint idBaseStation { get; set; }

            public uint id_drone { get; set; }

            public DateTime entringDrone { get; init; }
            

            public override string ToString()
            {
                String printBtarryLoad = "";
                printBtarryLoad += $"ID BaseStation is {idBaseStation},\n";
                printBtarryLoad += $"ID drown is {id_drone}\n";
                printBtarryLoad += $"Time of geting drone {entringDrone}\n";
                
                return printBtarryLoad;
            }
        }

    }
}