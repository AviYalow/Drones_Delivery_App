using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class PackageInTransfer
    {
        public int SerialNum { get; set; }
        public WeightCategories WeightCatgory { get; set; }
        public Priority Priority { get; set; }
        public bool InTheWay { get; set; }//true-in the way,false-waiting to be collected
        public ClientInPackage SendClient { get; set; }
        public ClientInPackage RecivedClient { get; set; }
        public Location Source { get; set; }
        public Location Destination { get; set; }
        public double Distance { get; set; }
        public override string ToString()
        {
            String print = "";
            print += $"Serial Number: {SerialNum},\n";
            print += $"Weight Category: {WeightCatgory},\n";
            print += $"priority: {Priority},\n";
            print += InTheWay ? "in the way\n" : "waiting to be collected\n";
            print += $"Send Client: {SendClient.Name},\n";
            print += $"Recived Client: {RecivedClient.Name},\n";
            print += $"Source Location: Latitude-{Source.Latitude} Longitude-{Source.Longitude}\n";
            print += $"Destination Location: Latitude-{Destination.Latitude} Longitude-{Destination.Longitude}\n";
            print += $"Distance: {Distance}\n";
            return print;
        }

    } 
}
