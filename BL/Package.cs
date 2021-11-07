using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Package
    {
        public int SerialNumber { get; init; }
        public ClientInPackage SendClient { get; set; }
        public ClientInPackage RecivedClient { get; set; }
        public Weight_categories weightCatgory { get; set; }
        public Priority priority { get; set; }
        public DroneInPackage drone { get; set; }

        //Delivery time create a package
        public DateTime create_package { get; set; }

        //Time to assign the package to a drone
        public DateTime package_association { get; set; }

        //Package collection time from the sender
        public DateTime collect_package { get; set; }

        //Time of arrival of the package to the recipient
        public DateTime package_arrived { get; set; }
    }
}
