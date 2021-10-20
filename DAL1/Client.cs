
using System;

namespace IDAL
{
    namespace DO
    {
        public struct Client
        {
            public int ID { get; init; }
            public String Name { get; set; }
            public String FonNumber { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            

            public override string ToString()
            {
                
                String printClient = "";
                printClient += $"ID is {ID},\n";
                printClient += $"Name is {Name},\n";
                printClient += $"Phone Number is {FonNumber},\n";
                printClient += $"Longitude is {Point.Degree(Longitude)},\n";
                printClient += $"Latitude is {Point.Degree(Latitude)} \n";
                return printClient;
            }



        }

    }
}