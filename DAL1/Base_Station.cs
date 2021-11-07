using System;

namespace IDAL
{
    namespace DO
    {
        /// <Base_Station>
        /// An entity that will represent 
        /// a base station for the drones
        /// </Base_Station>
        public struct Base_Station
        {
            public int baseNumber { get; init; }
            public String NameBase { get; set; }
            public int Number_of_charging_stations { get; set; }
            public double longitude { get; set; }
            public double latitude { get; set; }
           

           

            public override string ToString()
            {
                String print_Base_Station = "";
                print_Base_Station += $"Base Number: {baseNumber},\n";
                print_Base_Station += $"Name Base: {NameBase},\n";
                print_Base_Station += $"Number of charging stations: {Number_of_charging_stations},\n";

                //A view based on a sexagesimal of the coordinate values
                print_Base_Station += $"Longitude Status: {(longitude)},\n";
                print_Base_Station += $"Latitude Status: {(latitude)}\n";
                return print_Base_Station;
            }



        }


    }
}