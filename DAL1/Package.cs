using System;

namespace IDAL
{
    namespace DO
    {
        public struct Package
        {
            public int sirialNumber { get; init; }
            public int sendClient { get; set; }
            public int getingClient { get; set; }
            public Weight_categories weightCatgory { get; set; }
            public Priority priority { get; set; }
            public int operator_skimmer_ID { get; set; }
            public DateTime receiving_delivery { get; set; }
            public DateTime package_association { get; set; }

            public DateTime collect_package_for_shipment { get; set; }
            public DateTime package_arrived { get; set; }




            public override string ToString()
            {
                String printPackage = "";
                printPackage += $"Sirial Number is {sirialNumber},\n";
                printPackage += $"Send Client is {sendClient},\n";
                printPackage += $"Getting Client is {getingClient},\n";
                printPackage += $"weight Catgory is {weightCatgory},\n";
                printPackage += $"Priority is {priority},\n";
                printPackage += $"operator skimmer ID is {operator_skimmer_ID},\n";
                printPackage += $"Receiving Delivery is {receiving_delivery},\n";
                printPackage += $"Package Association is {package_association},\n";
                printPackage += $"collect package for shipment is {collect_package_for_shipment},\n";
                printPackage += $"package_arrived is {package_arrived}\n";
             
                return printPackage;
            }

        }

    }
}