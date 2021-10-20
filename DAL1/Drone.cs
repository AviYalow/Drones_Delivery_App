using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int siralNumber { get; init; }
            public string Model { get; set; }
            public Weight_categories weightCategory { get; set; }
            public double butrryStatos { get; set; }
            public Drone_statos drownStatos { get; set; }
            public int base_station  { get; set; }
            public double base_station_longitude { get; set; }
            public double base_station_latitude { get; set; }


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