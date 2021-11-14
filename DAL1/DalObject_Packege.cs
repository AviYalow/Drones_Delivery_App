using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;

namespace DalObject
{
    partial class DalObject : IDAL.IDal
    {
     

        /// <summary>
        /// Adding a new package
        /// </summary>
        /// <param name="idsend">Sending customer ID</param>
        /// <param name="idget">Receiving customer ID</param>
        /// <param name="kg">Weight categories</param>
        /// <param name="priorityByUser">Priority: Immediate/ quick/ Regular </param>
        /// <returns>Returns the serial number of the created package</returns>
        public uint Add_package(uint idsend, uint idget, uint kg, uint priorityByUser)
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
            package.collect_package_for_shipment = new DateTime();
            package.package_arrived = new DateTime();
            package.package_association = new DateTime();
            DataSource.packages.Add(package);
            return DataSource.Config.package_num;
        }

        /// <summary>
        /// connect package to drone
        /// </summary>
        /// <param name="packageNumber">serial number of the package that needs 
        /// to connect to drone </param>
        public void connect_package_to_drone(uint packageNumber, uint drone_sirial_number)
        {
            int i = DataSource.packages.FindIndex(x=>x.serialNumber==packageNumber);
            if (i==-1)
                 throw (new Item_not_found_exception("packege",packageNumber));
             if (DataSource.drones.Any(x => x.serialNumber == drone_sirial_number))
                 throw (new Item_not_found_exception("drone",drone_sirial_number));

            
          
            Package package = DataSource.packages[i];
            package.operator_skimmer_ID = drone_sirial_number;

            package.package_association = DateTime.Now;
            DataSource.packages[i] = package;
         


        }

        /// <summary>
        /// Updated package collected
        /// </summary>
        /// <param name="packageNumber">serial number of the package</param>
        public void Package_collected(uint packageNumber)
        {
            int i = DataSource.packages.FindIndex(x=>x.serialNumber==packageNumber);
            if (i == -1)
                throw (new Item_not_found_exception("package", packageNumber));

            
            Package package = DataSource.packages[i];
            package.collect_package_for_shipment = DateTime.Now;
            DataSource.packages[i] = package;
            
        }

        /// <summary>
        /// Update that package has arrived at destination
        /// </summary>
        /// <param name="packageNumber">serial number of the package</param>
        public void Package_arrived(uint packageNumber)
        {
            int i = DataSource.packages.FindIndex(x => x.serialNumber == packageNumber);
            if (i == -1)
                throw (new Item_not_found_exception("package", packageNumber));

            Package package = DataSource.packages[i];
            package.package_arrived = DateTime.Now;
            DataSource.packages[i] = package;
        }


        /// <summary>
        /// Display packege data desired
        /// </summary>
        /// <param name="packageNumbe">Serial number of a particular package</param>
        /// <returns> string of data</returns>
        public Package packege_by_number(uint packageNumber)
        {
            int i = DataSource.packages.FindIndex(x => x.serialNumber == packageNumber);
            if (i == -1)
                throw (new Item_not_found_exception("package", packageNumber));
            return DataSource.packages[i];

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

        public IEnumerable<Package> Packages_with_drone()
        {
            return DataSource.packages.FindAll(x => x.operator_skimmer_ID != 0);
        }

        public IEnumerable<Package> Packages_arrive_list()
        {
            return DataSource.packages.FindAll(x => x.package_arrived != new DateTime());
        }

        /// <summary>
        /// delete a spsific packege
        /// </summary>
        /// <param name="sirial"></param>
        public void Deletepackege(uint sirial)
        {
            int i = DataSource.packages.FindIndex(x => x.serialNumber == sirial);
            if (i == -1)
                throw (new Item_not_found_exception("package", sirial));
            DataSource.packages.Remove(DataSource.packages[i]);
        }

        public void UpdatePackege(Package package)
        {
            int i = DataSource.packages.FindIndex(x => x.serialNumber == package.serialNumber);
            if (i == -1)
                throw (new IDAL.DO.Item_not_found_exception("Packege", package.serialNumber));
            else
                DataSource.packages[i] = package;
        }

    }
}
