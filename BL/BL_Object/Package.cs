using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Package
    {
        public uint SerialNumber { get; init; }
        public ClientInPackage SendClient { get; set; }
        
        public ClientInPackage RecivedClient { get; set; }
        public WeightCategories weightCatgory { get; set; }
        public Priority priority { get; set; }
        public Drone drone { get; set; }

        //Delivery time create a package
        public DateTime create_package { get; set; }

        //Time to assign the package to a drone
        public DateTime package_association { get; set; }

        //Package collection time from the sender
        public DateTime collect_package { get; set; }

        //Time of arrival of the package to the recipient
        public DateTime package_arrived { get; set; }
        public override string ToString()
        {
            String print = "";
            print += $"Serial Number: {SerialNumber},\n";
            print += $"Send Client: {SendClient.Name},\n";
            print += $"Recived Client: {RecivedClient.Name},\n";
            print += $"Weight Category: {weightCatgory},\n";
            print += $"priority: {priority},\n";
            print += $"drone: {drone.SerialNum},\n";
            print += $"Delivery time create a package: {create_package},\n";
            print += $"Time to assign: {package_association},\n";
            print += $"Time Package collection: {collect_package},\n";
            print += $"Time of arrival: {package_arrived}\n";
           
            return print;
        }
    }
}
