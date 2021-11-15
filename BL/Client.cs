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





    }
}
