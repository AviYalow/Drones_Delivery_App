using System;

namespace IDAL
{
    namespace DO
    {
        public struct Base_Station
        {
            public int baseNumber { get; private set; }
            public String NameBase { get; set; }
            public int Number_of_charging_stations { get; set; }
            public double longitude { get; set; }
            public double latitude { get; set; }


        }


    }
}