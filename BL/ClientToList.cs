using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ClientToList
    {

        public int ID { get; init; }
        public string Name { get; set; }
        public int Phone { get; set; }

        public int Arrived { get; set; } //the number of package that send and arrived

        public int NotArrived { get; set; }//the number of package that send and hasn't arrived yet
        public int received { get; set; }//the number of package that recived
        public int OnTheWay { get; set; }//the number of package that on the way



}
}
