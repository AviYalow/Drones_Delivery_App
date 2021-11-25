

using System;

namespace IDAL
{
    namespace DO
    {
        /// <Client>
        /// An entity that will represent 
        /// a client
        /// </Client>
        public struct Client 
        {
            public uint Id { get; init; }
            public String Name { get; set; }
            public String PhoneNumber { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public bool Active { get; set; }

           

            public override string ToString()
            {

                String printClient = "";
                printClient += $"ID is {Id},\n";
                printClient += $"Name is {Name},\n";
                printClient += $"Phone Number is {PhoneNumber},\n";

                //A view based on a sexagesimal of the coordinate values
                printClient += $"Longitude is {(Longitude)},\n";
                printClient += $"Latitude is {(Latitude)} \n";

                return printClient;
            }



        }

    }
}