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

            public override string ToString()
            {
                String printBtarryLoad = "";
                printBtarryLoad += $"ID BaseStation is {idBaseStation},\n";
                printBtarryLoad += $"ID drown is {id_drone}\n";
                return printBtarryLoad;
            }
        }

    }
}