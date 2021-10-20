using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int siralNumber { get; private set; }
            public string Model { get; set; }
            public string weightCategory { get; set; }
            public int butrryStatos { get; set; }
            public string drownStatos { get; set; }


            public override string ToString()
            {
                String printDrown = "";
                printDrown += $"Siral Number is {siralNumber},\n";
                printDrown += $"Weight Category is {weightCategory},\n";
                printDrown += $"Butrry Statos is {butrryStatos},\n";
                printDrown += $"Drown Statos is {drownStatos}\n";
                return printDrown;
            }

        }

    }
}