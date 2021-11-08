using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class BaseStationToList
    {
        public int SerialNum { get; init; }
        public string Name { get; set; }
        public int FreeState { get; set; }
        public int BusyState { get; set; }
    }
}
