using System;

namespace IDAL
{
    namespace DO
    {
        public struct BtarryLoad
        {
            public int idBaseStation { get; set; }

            public int id_drown { get; set; }

            public override string ToString()
            {
                String printBtarryLoad = "";
                printBtarryLoad += $"ID BaseStation is {idBaseStation},\n";
                printBtarryLoad += $"ID drown is {id_drown}\n";
                return printBtarryLoad;
            }
        }

    }
}