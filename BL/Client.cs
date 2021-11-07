using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Client
    {
        public int ID { get; init; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public Location location { get; set; }

        public List<Package> fromClient { get; set; }
        public List<Package> toClient { get; set; }





    }
}
