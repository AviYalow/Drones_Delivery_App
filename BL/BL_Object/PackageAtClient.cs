using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{

    /// <summary>
    /// Package At Client
    /// </summary>
    public class PackageAtClient
    {
        public uint SerialNum { get; set; }
        public WeightCategories WeightCatgory { get; set; }
        public Priority Priority { get; set; }
        public PackageStatus packageStatus { get; set; }
       
        //The other client in the package.
        //The receiver for the sender and sender for the receiver
        public ClientInPackage client2 { get; set; }

        public override string ToString()
        {
            String print = "";
            print += $"Serial Number: {SerialNum},\n";
            print += $"Weight Category: {WeightCatgory},\n";
            print += $"priority: {Priority},\n";
            print += $"Package Status: {packageStatus},\n";
            print += $"The other client in the package: {client2.Name}\n";
            return print;
        }
        }
}
