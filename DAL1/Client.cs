
using System;

namespace IDAL
{
    namespace DO
    {
        public struct Client
        {
            public int ID { get; private set; }
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
                printClient += $"Longitude is {Longitude},\n";
                printClient += $"Latitude is {Latitude} \n";
                return printClient;
            }



        }

    }
}