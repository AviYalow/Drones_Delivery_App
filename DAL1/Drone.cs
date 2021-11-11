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
            public int serialNumber { get; init; }
            public string Model { get; set; }
            public Weight_categories weightCategory { get; set; }
            
            




            public override string ToString()
            {
                String printDrown = "";
                printDrown += $"Siral Number is {serialNumber},\n";
                printDrown += $"Weight Category is {weightCategory},\n";
                /* printDrown += $"Butrry Statos is {butrryStatus},\n";
                 printDrown += $"Drown Statos is {drownStatus}\n";*/
                return printDrown;
            }

        }

    }
}