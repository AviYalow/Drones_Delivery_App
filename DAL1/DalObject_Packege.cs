using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;

namespace DalObject
{
    partial class DalObject
    {
       public static bool sustainability_test_packege(int number)
        {

            foreach (Package item in DataSource.packages)
            {
                if (item.serialNumber == number)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adding a new package
        /// </summary>
        /// <param name="idsend">Sending customer ID</param>
        /// <param name="idget">Receiving customer ID</param>
        /// <param name="kg">Weight categories</param>
        /// <param name="priorityByUser">Priority: Immediate/ quick/ Regular </param>
        /// <returns>Returns the serial number of the created package</returns>
        public int Add_package(int idsend, int idget, int kg, int priorityByUser)
        {


            DataSource.Config.package_num++;
            Package package = new Package
            {
                serialNumber = DataSource.Config.package_num,
                receiving_delivery = DateTime.Now,
                sendClient = idsend,
                getingClient = idget,
                weightCatgory = (Weight_categories)kg,
                priority = (Priority)priorityByUser
            };

            //  package.operator_skimmer_ID = found_drone_for_package(DataSource.packages[DataSource.Config.package_num].weightCatgory);

            DataSource.packages.Add(package);
            return DataSource.Config.package_num;
        }

        /// <summary>
        /// connect package to drone
        /// </summary>
        /// <param name="packageNumber">serial number of the package that needs 
        /// to connect to drone </param>
        public void connect_package_to_drone(int packageNumber, int drone_sirial_number)
        {
            /* if (!sustainability_test(packageNumber))
                 throw ("Error: The package does not exist in the system");*/
            // if (!DalObject_Drone.sustainability_test(drone_sirial_number))
                // throw ("Error: The package does not exist in the system");*/

            int i = 0;
            for (; i < DataSource.packages.Count(); i++)
            {
                if (DataSource.packages[i].serialNumber == packageNumber)
                    break;
            }
            Package package = DataSource.packages[i];
            package.operator_skimmer_ID = drone_sirial_number;

            package.package_association = DateTime.Now;
            DataSource.packages[i] = new Package
            {
                serialNumber = package.serialNumber,
                sendClient = package.sendClient,
                getingClient = package.getingClient,
                operator_skimmer_ID = package.operator_skimmer_ID,
                weightCatgory = package.weightCatgory,
                package_arrived = package.package_arrived,
                priority = package.priority,
                receiving_delivery = package.receiving_delivery,
                collect_package_for_shipment = package.collect_package_for_shipment,
                package_association = package.package_association

            };



        }

        /// <summary>
        /// Updated package collected
        /// </summary>
        /// <param name="packageNumber">serial number of the package</param>
        public void Package_collected(int packageNumber)
        {
            /* if (!sustainability_test(packageNumber))
               throw ("Error: The package does not exist in the system");*/
            int i = 0;
            for (; i < DataSource.packages.Count(); i++)
            {
                if (DataSource.packages[i].serialNumber == packageNumber)
                    break;
            }
            Package package = DataSource.packages[i];
            package.collect_package_for_shipment = DateTime.Now;
            DataSource.packages[i] = new Package
            {
                serialNumber = package.serialNumber,
                sendClient = package.sendClient,
                getingClient = package.getingClient,
                operator_skimmer_ID = package.operator_skimmer_ID,
                weightCatgory = package.weightCatgory,
                package_arrived = package.package_arrived,
                priority = package.priority,
                receiving_delivery = package.receiving_delivery,
                collect_package_for_shipment = package.collect_package_for_shipment,
                package_association = package.package_association

            };
        }

        /// <summary>
        /// Update that package has arrived at destination
        /// </summary>
        /// <param name="packageNumber">serial number of the package</param>
        public void Package_arrived(int packageNumber)
        {
            /* if (!sustainability_test(packageNumber))
               throw ("Error: The package does not exist in the system");*/
            int i = 0;
            for (; i < DataSource.packages.Count(); i++)
            {
                if (DataSource.packages[i].serialNumber == packageNumber)
                {
                    break;
                }

            }
            Package package = DataSource.packages[i];
            package.package_arrived = DateTime.Now;
            DataSource.packages[i] = new Package
            {
                serialNumber = package.serialNumber,
                sendClient = package.sendClient,
                getingClient = package.getingClient,
                operator_skimmer_ID = package.operator_skimmer_ID,
                weightCatgory = package.weightCatgory,
                package_arrived = package.package_arrived,
                priority = package.priority,
                receiving_delivery = package.receiving_delivery,
                collect_package_for_shipment = package.collect_package_for_shipment,
                package_association = package.package_association

            };
            // drone_after_work(DataSource.packages[packageNumber - 1].operator_skimmer_ID);
        }


        /// <summary>
        /// Display packege data desired
        /// </summary>
        /// <param name="packageNumbe">Serial number of a particular package</param>
        /// <returns> string of data</returns>
        public Package packege_by_number(int packageNumbe)
        {
            /* if (!sustainability_test(packageNumber))
               throw ("Error: The package does not exist in the system");*/
            return DataSource.packages[DataSource.packages.FindIndex(x => x.serialNumber == packageNumbe)];

        }

        /// <summary>
        /// Print all the packages
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values ​​of all the packages so we can print them</param>
        public IEnumerable<Package> packege_list()
        {
            return DataSource.packages.ToList<Package>();

        }

        /// <summary>
        /// Displays a list of packages that
        /// have not been assigned to a drone yet 
        /// </summary>

        public IEnumerable<Package> packege_list_with_no_drone()
        {

            return DataSource.packages.FindAll(x => x.operator_skimmer_ID == 0);



        }

        /// <summary>
        /// delete a spsific packege
        /// </summary>
        /// <param name="sirial"></param>
        public void Deletepackege(int sirial)
        {
            /* if (!sustainability_test(packageNumber))
               throw ("Error: The package does not exist in the system");*/
            for (int i = 0; i < DataSource.packages.Count(); i++)
            {
                if (DataSource.packages[i].serialNumber == sirial)
                {
                    DataSource.packages.Remove(DataSource.packages[i]);
                    return;
                }
            }
        }

    }
}
