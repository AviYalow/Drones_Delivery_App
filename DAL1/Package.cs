using System;

namespace IDAL
{
    namespace DO
    {
        /// <Package>
        /// Entity representing a package for delivery
        /// </Package>
        public struct Package
        {
            public int serialNumber { get; init; }
            public int sendClient { get; set; }
            public int getingClient { get; set; }
            public Weight_categories weightCatgory { get; set; }
            public Priority priority { get; set; }
            public int operator_skimmer_ID { get; set; }

            //Delivery time create a package
            public DateTime receiving_delivery { get; set; }

            //Time to assign the package to a drone
            public DateTime package_association { get; set; }

            //Package collection time from the sender
            public DateTime collect_package_for_shipment { get; set; }

            //Time of arrival of the package to the recipient
            public DateTime package_arrived { get; set; }



         
            public override string ToString()
            {
                String printPackage = "";
                printPackage += $"Sirial Number is {serialNumber},\n";
                printPackage += $"Send Client is {sendClient},\n";
                printPackage += $"Getting Client is {getingClient},\n";
                printPackage += $"weight Catgory is {weightCatgory},\n";
                printPackage += $"Priority is {priority},\n";
                printPackage += $"operator skimmer ID is {operator_skimmer_ID},\n";
                printPackage += $"Receiving Delivery is {receiving_delivery},\n";

                //If the package was associated with a drone
                if (operator_skimmer_ID != 0)
                {
                    printPackage += $"Package Association is {package_association},\n";
                    //if the package have been collected
                    if (collect_package_for_shipment != new DateTime())
                    {
                        printPackage += $"collect package for shipment is {collect_package_for_shipment},\n";
                        // if the package arrived
                        if (package_arrived != new DateTime())
                        {
                            printPackage += $"package_arrived is {package_arrived}\n";
                        }
                        //if the package not arrived
                        else
                            printPackage += $"Shipping on the way \n";

                    }
                    //if the package haven't been collected
                    else
                        printPackage += "The shipment has not been collected yet\n";


                }
                else //If the package wasn't associated with a drone
                    printPackage += $"Package is not Association yet ,\n";
              
              
               

              


                return printPackage;
            }

        }

    }
}