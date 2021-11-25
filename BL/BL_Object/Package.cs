using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    /// <summary>
    /// Package
    /// </summary>
    public class Package
    {
        public uint SerialNumber { get; init; }
        public ClientInPackage SendClient { get; set; }
        
        public ClientInPackage RecivedClient { get; set; }
        public WeightCategories weightCatgory { get; set; }
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
        public override string ToString()
        {
            String print = "";
            print += $"Serial Number: {SerialNumber},\n";
            print += $"Send Client: {SendClient.Name},\n";
            print += $"Recived Client: {RecivedClient.Name},\n";
            print += $"Weight Category: {weightCatgory},\n";
            print += $"priority: {priority},\n";
            print +=drone is null?"drone: no drone\n": $"drone: {drone},\n";
            print += $"Delivery time create a package: {create_package},\n";
            print += $"Time to assign:";
            if (package_association != DateTime.MinValue)
                print += $"{package_association},\n";
            else
            {
                print += "packege not assoction yet\n";
                return print;
            }
                print += $"Time Package collection:";
            if (collect_package != DateTime.MinValue)
                print += $"{collect_package},\n";
            else
            {print+= "packege not collect yet\n";return print; }

            
            print += $"Time of arrival:";
            if (package_arrived != DateTime.MinValue)
                print += $"{ package_arrived}\n";
            else
            {
                print += "packege not arrive yet\n";
                return print;
            }
           
            return print;
        }
    }
}
