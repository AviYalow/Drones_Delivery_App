using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Client
    {
        public uint Id { get; init; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }

        public List<Package> FromClient { get; set; }
        public List<Package> ToClient { get; set; }

        public override string ToString()
        {
            String print = "";
            print += $"ID: {Id},\n";
            print += $"Name is {Name},\n";
            print += $"Phone: {Phone},\n";
            print += $"Location: Latitude-{Location.Latitude} Longitude-{Location.Longitude},\n";
            
            return print;
        }


    }
}
