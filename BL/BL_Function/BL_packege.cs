using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {/// <summary>
     /// add packege to list in data source 
     /// </summary>
     /// <param name="package"></param>
     /// <returns></returns>
        public uint AddPackege(Package package)
        {
            uint packegeNum = 0;
            var distans = Distans(ClientLocation(package.SendClient.Id), ClientLocation(package.RecivedClient.Id));
            if (package.weightCatgory == WeightCategories.Heavy && distans > 10)
                throw new MoreDistasThenMaximomException("10");
            else if (package.weightCatgory == WeightCategories.Medium && distans > 16.6)
                throw new MoreDistasThenMaximomException("16.6");
            else if (package.weightCatgory == WeightCategories.Easy && distans > 33.3)
                throw new MoreDistasThenMaximomException("33.3");

            try
            {

                packegeNum = dalObj.AddPackage(new IDAL.DO.Package
                {
                    SendClient = package.SendClient.Id,
                    GetingClient = package.RecivedClient.Id,
                    Priority = (IDAL.DO.Priority)package.priority,
                    WeightCatgory = (IDAL.DO.WeightCategories)package.weightCatgory,
                    PackageArrived = package.create_package

                });
            }
            catch (IDAL.DO.ItemFoundException ex)
            {
                throw (new ItemFoundExeption(ex));
            }



            return packegeNum;

        }

      

        double batteryCalculationForFullShipping(Location drone, Package package)
        {
            return buttryDownWithNoPackege(drone, ClientLocation(package.SendClient.Id)) + buttryDownPackegeDelivery(convertPackegeBlToPackegeInTrnansfer(package)) +
                buttryDownWithNoPackege(drone, ClientLocation(package.RecivedClient.Id));
        }

        public void UpdatePackegInDal(Package package)
        {
            dalObj.UpdatePackege(new IDAL.DO.Package
            {
                SerialNumber = package.SerialNumber,
                SendClient = package.SendClient.Id,
                CollectPackageForShipment = package.collect_package,
                ReceivingDelivery = package.create_package,
                OperatorSkimmerId = package.drone.SerialNum,
                PackageArrived = package.package_arrived,
                PackageAssociation = package.package_association,
                Priority = (IDAL.DO.Priority)package.priority,
                GetingClient = package.RecivedClient.Id,
                WeightCatgory = (IDAL.DO.WeightCategories)package.weightCatgory
            });
        }

        Package cloosetPackege(Location location, IEnumerable<IDAL.DO.Package> packages = null)
        {
            Package package = new Package();
            if (packages == null)
                throw new TheListIsEmptyException();
            {
                Location location1 = ClientLocation(packages.ToList()[0].SendClient);


                foreach (var packege1 in packages)
                {
                    uint sendig = packege1.SendClient;
                    Location location2 = ClientLocation(sendig);
                    if (Distans(location, location1) > Distans(location, location2))
                    {
                        location1 = location2;
                        package = convertToPackegeBl(packege1);
                    }

                }

            }
            return package;

        }

        public Package ShowPackage(uint number)
        {

            try
            {
                var dataPackege = dalObj.packegeByNumber(number);
                return convertPackegeDalToBl(dataPackege);
            }
            catch (IDAL.DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }

        }

        Package convertPackegeDalToBl(IDAL.DO.Package dataPackege)
        {
            return new Package
            {
                SerialNumber = dataPackege.SerialNumber,
                SendClient = clientInPackageFromDal(dataPackege.SendClient),
                collect_package = dataPackege.CollectPackageForShipment,
                create_package = dataPackege.ReceivingDelivery,
                drone = SpecificDrone(dataPackege.OperatorSkimmerId),
                package_arrived = dataPackege.PackageArrived,
                package_association = dataPackege.PackageAssociation,
                priority = (Priority)dataPackege.Priority,
                RecivedClient = clientInPackageFromDal(dataPackege.GetingClient),
                weightCatgory = (WeightCategories)dataPackege.WeightCatgory
            };

        }

        Package convertToPackegeBl(IDAL.DO.Package dataPackege)
        {
            return new Package
            {
                SerialNumber = dataPackege.SerialNumber,
                SendClient = clientInPackageFromDal(dataPackege.SendClient),
                collect_package = dataPackege.CollectPackageForShipment,
                create_package = dataPackege.ReceivingDelivery,
                drone = SpecificDrone(dataPackege.OperatorSkimmerId),
                package_arrived = dataPackege.PackageArrived,
                package_association = dataPackege.PackageAssociation,
                priority = (Priority)dataPackege.Priority,
                RecivedClient = clientInPackageFromDal(dataPackege.GetingClient),
                weightCatgory = (WeightCategories)dataPackege.WeightCatgory
            };
        }

        

        public IEnumerable<PackageToList> PackageToLists()
        {
            if (dalObj.PackegeList().ToList().Count() == 0)
                throw new TheListIsEmptyException();
            List<PackageToList> packageToLists = new List<PackageToList>();
            PackageStatus packageStatus;
            foreach (var packege in dalObj.PackegeList())
            {
                if (packege.PackageArrived != new DateTime())
                    packageStatus = PackageStatus.Arrived;
                else if (packege.CollectPackageForShipment != new DateTime())
                    packageStatus = PackageStatus.Collected;
                else if (packege.PackageAssociation != new DateTime())
                    packageStatus = PackageStatus.Assign;
                else
                    packageStatus = PackageStatus.Create;
                packageToLists.Add(new PackageToList
                {
                    packageStatus = packageStatus,
                    RecivedClient = dalObj.CilentByNumber(packege.GetingClient).Name,
                    SendClient = dalObj.CilentByNumber(packege.SendClient).Name,
                    SerialNumber = packege.SerialNumber,
                    priority = (Priority)packege.Priority,
                    WeightCategories = (WeightCategories)packege.WeightCatgory


                });

            }
            return packageToLists;
        }

        public IEnumerable<PackageToList> PackageWithNoDroneToLists()
        {
            if (dalObj.PackegeListWithNoDrone().ToList().Count() == 0)
                throw new TheListIsEmptyException();
            List<PackageToList> packageToLists = new List<PackageToList>();
            PackageStatus packageStatus;
            foreach (var packege in dalObj.PackegeListWithNoDrone())
            {

                packageStatus = PackageStatus.Create;
                packageToLists.Add(new PackageToList
                {
                    packageStatus = packageStatus,
                    RecivedClient = dalObj.CilentByNumber(packege.GetingClient).Name,
                    SendClient = dalObj.CilentByNumber(packege.SendClient).Name,
                    SerialNumber = packege.SerialNumber,
                    priority = (Priority)packege.Priority,
                    WeightCategories = (WeightCategories)packege.WeightCatgory


                });

            }
            return packageToLists;
        }



    }
}
