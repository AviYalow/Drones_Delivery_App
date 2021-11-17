using System;

namespace IDAL
{
    namespace DO
    {
        /// <Drone>
        /// An entity that will represent 
        /// a Drone for deliverly 
        /// </Drone>
        public struct Drone 
        {
            public uint SerialNumber { get; init; }
            public string Model { get; set; }
            public WeightCategories WeightCategory { get; set; }
            
            




            public override string ToString()
            {
                String printDrown = "";
                printDrown += $"Siral Number is {SerialNumber},\n";
                printDrown += $"Weight Category is {WeightCategory},\n";
                /* printDrown += $"Butrry Statos is {butrryStatus},\n";
                 printDrown += $"Drown Statos is {drownStatus}\n";*/
                return printDrown;
            }

        }

    }
}