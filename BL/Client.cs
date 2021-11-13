using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Client
    {
        public int ID { get; init; }
        public string Name { get; set; }
        public String Phone { get; set; }
        public Location location { get; set; }

        public List<Package> fromClient { get; set; }
        public List<Package> toClient { get; set; }





    }
}
