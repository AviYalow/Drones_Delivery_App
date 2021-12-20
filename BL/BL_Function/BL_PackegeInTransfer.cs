using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi.BO;

using DalApi;
namespace BlApi
{
    public partial class BL : IBL
    {
        /// <summary>
        ///  convert packege in data layer to packegeInTrnansfer object in the logical layer
        /// </summary>
        /// <param name="package"> packege in data layer</param>
        /// <returns> packegeInTrnansfer object in the logical layer</returns>
        PackageInTransfer convertPackegeDalToPackegeInTrnansfer(DO.Package package)
        {
            var returnPackege = new PackageInTransfer
            {
                SerialNum = package.SerialNumber,
                WeightCatgory = (WeightCategories)package.WeightCatgory,
                Priority = (Priority)package.Priority,
                Source = ClientLocation(package.SendClient),
                Destination = ClientLocation(package.GetingClient),
                SendClient = clientInPackageFromDal(package.SendClient),
                RecivedClient = clientInPackageFromDal(package.GetingClient)
            };
            returnPackege.Distance = Distans(returnPackege.Source, returnPackege.Destination);
            returnPackege.InTheWay = (package.PackageArrived is null&&package.OperatorSkimmerId!=0) ? true : false;
            return returnPackege;
        }
        /// <summary>
        /// convert Packege object to PackegeInTrnansfer object
        /// </summary>
        /// <param name="package">Packege object </param>
        /// <returns> PackegeInTrnansfer object</returns>
        PackageInTransfer convertPackegeBlToPackegeInTrnansfer(Package package)
        {
            var returnPackege = new PackageInTransfer { Priority = package.priority, SendClient = package.SendClient, RecivedClient = package.RecivedClient, SerialNum = package.SerialNumber, WeightCatgory = package.weightCatgory, Source = ClientLocation(package.SendClient.Id), Destination = ClientLocation(package.RecivedClient.Id) };
            returnPackege.Distance = Distans(returnPackege.Source, returnPackege.Destination);
            returnPackege.InTheWay = (package.package_arrived != new DateTime()) ? true : false;
            return returnPackege;
        }
    

        /// <summary>
        /// A package is collected by a drone
        /// </summary>
        /// <param name="droneNumber">A drone number that collects the package</param>
        public void CollectPackegForDelivery(uint droneNumber)
        {
            var drone = dronesListInBl.Find(x => x.SerialNumber == droneNumber && x.DroneStatus != DroneStatus.Delete);
            if (drone == null)
                throw new ItemNotFoundException("Drone", droneNumber);
            try
            {
                var pacege = convertPackegeDalToPackegeInTrnansfer(dalObj.packegeByNumber(drone.NumPackage));
                if (pacege.InTheWay != true)
                { new FunctionErrorException("ShowPackage||AddPackege"); }

                Location location = ClientLocation(pacege.SendClient.Id);
                drone.ButrryStatus -= buttryDownWithNoPackege(drone.Location, location);

                if (drone.ButrryStatus < 0)
                { new FunctionErrorException("BatteryCalculationForFullShipping"); }

                drone.Location = location;

                dalObj.PackageCollected(pacege.SerialNum);
                dronesListInBl[dronesListInBl.FindIndex(x => x.SerialNumber == droneNumber)] = drone;
            }
            catch(DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }
        }

        /// <summary>
        /// A package that arrived at the destination
        /// </summary>
        /// <param name="droneNumber">A drone number that takes the package</param>
        public void PackegArrive(uint droneNumber)
        {
            var drone = dronesListInBl.Find(x => x.SerialNumber == droneNumber && x.DroneStatus != DroneStatus.Delete);
            if (drone == null)
                throw new ItemNotFoundException("Drone", droneNumber);
            try
            {
                var packege = convertPackegeDalToPackegeInTrnansfer(dalObj.packegeByNumber(drone.NumPackage));
                if (packege.InTheWay == false)
                    throw new PackegeNotAssctionOrCollectedException();
                drone.ButrryStatus -= buttryDownPackegeDelivery(packege);

                drone.Location = ClientLocation(packege.RecivedClient.Id);
                drone.DroneStatus = DroneStatus.Free;
                drone.NumPackage = 0;
                packege.InTheWay = false;
                dalObj.PackageArrived(packege.SerialNum);
                dronesListInBl[dronesListInBl.FindIndex(x => x.SerialNumber == droneNumber)] = drone;
            }
            catch(DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }

        }

        /// <summary>
        /// Assignment between a package and a drone
        /// </summary>
        /// <param name="droneNumber"> serial number of a drone</param>
        public void ConnectPackegeToDrone(uint droneNumber)
        {
            var drone = dronesListInBl.Find(x => x.SerialNumber == droneNumber&&x.DroneStatus!=DroneStatus.Delete);
            if (drone is null)
            { throw new ItemNotFoundException("Drone", droneNumber); }
            if (drone.DroneStatus != DroneStatus.Free)
            { throw new DroneCantMakeDliveryException(); }

          
           DO.Package returnPackege = new DO.Package { SerialNumber=0};
            foreach (var packege in dalObj.PackegeList(x=>x.OperatorSkimmerId==0))
            {
                if (batteryCalculationForFullShipping(drone.Location, convertPackegeDalToBl(packege)) < drone.ButrryStatus && (WeightCategories)packege.WeightCatgory <= drone.WeightCategory)
                    if (returnPackege.Priority < packege.Priority)
                        returnPackege = packege;
                    else if (returnPackege.Priority == packege.Priority)
                        if (returnPackege.WeightCatgory < packege.WeightCatgory)
                            returnPackege = packege;
                        else if (returnPackege.WeightCatgory == packege.WeightCatgory)
                            if (Distans(drone.Location, ClientLocation(packege.SendClient)) < Distans(drone.Location, ClientLocation(returnPackege.SendClient)))
                                returnPackege = packege;

            }
            if(returnPackege.SerialNumber==0)
            throw new DroneCantMakeDliveryException();
            drone.NumPackage = returnPackege.SerialNumber;
            drone.DroneStatus = DroneStatus.Work;
            dalObj.ConnectPackageToDrone(returnPackege.SerialNumber, droneNumber);
            for (int i = 0; i < dronesListInBl.Count; i++)
            {
                if (dronesListInBl[i].SerialNumber == drone.SerialNumber)
                {
                    dronesListInBl[i] = drone;
                    break;
                }
            }
            

        }

    }
}
