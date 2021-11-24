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
        public uint AddPackage(Package package)
        {


           
            
            package.ReceivingDelivery = DateTime.Now;
            package.CollectPackageForShipment = new DateTime();
            package.PackageArrived = new DateTime();
            package.PackageAssociation = new DateTime();
            DataSource.packages.Add(new Package { SerialNumber= DataSource.Config.package_num,SendClient=package.SendClient,GetingClient= package.GetingClient,
                 Priority= package.Priority,ReceivingDelivery=package.ReceivingDelivery,WeightCatgory=package.WeightCatgory,OperatorSkimmerId=0,
                CollectPackageForShipment= package.CollectPackageForShipment,PackageArrived= package.PackageArrived,PackageAssociation= package.PackageAssociation
            });
            DataSource.Config.package_num++;
            return DataSource.Config.package_num-1;
        }

        /// <summary>
        /// connect package to drone
        /// </summary>
        /// <param name="packageNumber">serial number of the package that needs 
        /// to connect to drone </param>
        public void ConnectPackageToDrone(uint packageNumber, uint drone_sirial_number)
        {
            int i = DataSource.packages.FindIndex(x=>x.SerialNumber==packageNumber);
            if (i==-1)
                 throw (new ItemNotFoundException("packege",packageNumber));
             if (!DataSource.drones.Any(x => x.SerialNumber == drone_sirial_number))
                 throw (new ItemNotFoundException("drone",drone_sirial_number));

            
          
            Package package = DataSource.packages[i];
            package.OperatorSkimmerId = drone_sirial_number;

            package.PackageAssociation = DateTime.Now;
            DataSource.packages[i] = package;
         


        }

        /// <summary>
        /// Updated package collected
        /// </summary>
        /// <param name="packageNumber">serial number of the package</param>
        public void PackageCollected(uint packageNumber)
        {
            int i = DataSource.packages.FindIndex(x=>x.SerialNumber==packageNumber);
            if (i == -1)
                throw (new ItemNotFoundException("package", packageNumber));

            
            Package package = DataSource.packages[i];
            package.CollectPackageForShipment = DateTime.Now;
            DataSource.packages[i] = package;
            
        }

        /// <summary>
        /// Update that package has arrived at destination
        /// </summary>
        /// <param name="packageNumber">serial number of the package</param>
        public void PackageArrived(uint packageNumber)
        {
            int i = DataSource.packages.FindIndex(x => x.SerialNumber == packageNumber);
            if (i == -1)
                throw (new ItemNotFoundException("package", packageNumber));

            Package package = DataSource.packages[i];
            package.PackageArrived = DateTime.Now;
            DataSource.packages[i] = package;
        }


        /// <summary>
        /// Display packege data desired
        /// </summary>
        /// <param name="packageNumbe">Serial number of a particular package</param>
        /// <returns> string of data</returns>
        public Package packegeByNumber(uint packageNumber)
        {
            int i = DataSource.packages.FindIndex(x => x.SerialNumber == packageNumber);
            if (i == -1)
                throw (new ItemNotFoundException("package", packageNumber));
            return DataSource.packages[i];

        }

        /// <summary>
        /// Print all the packages
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values ​​of all the packages so we can print them</param>
        public IEnumerable<Package> PackegeList()
        {
            return DataSource.packages.ToList<Package>();

        }

        /// <summary>
        /// Displays a list of packages that
        /// have not been assigned to a drone yet 
        /// </summary>

        public IEnumerable<Package> PackegeListWithNoDrone()
        {

            return DataSource.packages.FindAll(x => x.OperatorSkimmerId == 0);



        }

        public IEnumerable<Package> PackagesWithDrone()
        {
            return DataSource.packages.FindAll(x => x.OperatorSkimmerId != 0);
        }

        public IEnumerable<Package> PackagesArriveList()
        {
            return DataSource.packages.FindAll(x => x.PackageArrived != DateTime.MinValue).ToList();
        }

        /// <summary>
        /// delete a spsific packege
        /// </summary>
        /// <param name="sirial"></param>
        public void DeletePackege(uint sirial)
        {
            int i = DataSource.packages.FindIndex(x => x.SerialNumber == sirial);
            if (i == -1)
                throw (new ItemNotFoundException("package", sirial));
            DataSource.packages.Remove(DataSource.packages[i]);
        }

        public void UpdatePackege(Package package)
        {
            int i = DataSource.packages.FindIndex(x => x.SerialNumber == package.SerialNumber);
            if (i == -1)
                throw (new IDAL.DO.ItemNotFoundException("Packege", package.SerialNumber));
            else
                DataSource.packages[i] = package;
        }

    }
}
