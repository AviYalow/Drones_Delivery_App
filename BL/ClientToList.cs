using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ClientToList
    {

        public uint ID { get; init; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public uint Arrived { get; set; } //the number of package that send and arrived

        public uint NotArrived { get; set; }//the number of package that send and hasn't arrived yet
        public uint received { get; set; }//the number of package that recived
        public uint OnTheWay { get; set; }//the number of package that on the way



}
}
