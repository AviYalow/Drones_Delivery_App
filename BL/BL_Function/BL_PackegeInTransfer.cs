using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        /// <summary>
        ///  convert packege in data layer to packegeInTrnansfer object in the logical layer
        /// </summary>
        /// <param name="package"> packege in data layer</param>
        /// <returns> packegeInTrnansfer object in the logical layer</returns>
        PackageInTransfer convertPackegeDalToPackegeInTrnansfer(IDAL.DO.Package package)
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
            returnPackege.InTheWay = (package.PackageArrived == new DateTime()) ? true : false;
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
        //drone start delivery

        /// <summary>
        /// A package is collected by a drone
        /// </summary>
        /// <param name="droneNumber">A drone number that collects the package</param>
        public void CollectPackegForDelivery(uint droneNumber)
        {
            var drone = dronesListInBl.Find(x => x.SerialNumber == droneNumber && x.droneStatus != DroneStatus.Delete);
            if (drone == null)
                throw new ItemNotFoundException("Drone", droneNumber);
            try
            {
                var pacege = convertPackegeDalToPackegeInTrnansfer(dalObj.packegeByNumber(drone.numPackage));
                if (pacege.InTheWay != true)
                { new FunctionErrorException("ShowPackage||AddPackege"); }

                Location location = ClientLocation(pacege.SendClient.Id);
                drone.butrryStatus -= buttryDownWithNoPackege(drone.location, location);

                if (drone.butrryStatus < 0)
                { new FunctionErrorException("BatteryCalculationForFullShipping"); }

                drone.location = location;

                dalObj.PackageCollected(pacege.SerialNum);
                dronesListInBl[dronesListInBl.FindIndex(x => x.SerialNumber == droneNumber)] = drone;
            }
            catch(IDAL.DO.ItemNotFoundException ex)
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
            var drone = dronesListInBl.Find(x => x.SerialNumber == droneNumber && x.droneStatus != DroneStatus.Delete);
            if (drone == null)
                throw new ItemNotFoundException("Drone", droneNumber);
            try
            {
                var packege = convertPackegeDalToPackegeInTrnansfer(dalObj.packegeByNumber(drone.numPackage));
                if (packege.InTheWay == false)
                    throw new PackegeNotAssctionOrCollectedException();
                drone.butrryStatus -= buttryDownPackegeDelivery(packege);

                drone.location = ClientLocation(packege.RecivedClient.Id);
                drone.droneStatus = DroneStatus.Free;
                drone.numPackage = 0;
                packege.InTheWay = false;
                dalObj.PackageArrived(packege.SerialNum);
                dronesListInBl[dronesListInBl.FindIndex(x => x.SerialNumber == droneNumber)] = drone;
            }
            catch(IDAL.DO.ItemNotFoundException ex)
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
            var drone = dronesListInBl.Find(x => x.SerialNumber == droneNumber&&x.droneStatus!=DroneStatus.Delete);
            if (drone is null)
            { throw new ItemNotFoundException("Drone", droneNumber); }
            if (drone.droneStatus != DroneStatus.Free)
            { throw new DroneCantMakeDliveryException(); }

          
            IDAL.DO.Package returnPackege = new IDAL.DO.Package { SerialNumber=0};
            foreach (var packege in dalObj.PackegeList(x=>x.OperatorSkimmerId!=0))
            {
                if (batteryCalculationForFullShipping(drone.location, convertPackegeDalToBl(packege)) < drone.butrryStatus && (WeightCategories)packege.WeightCatgory <= drone.weightCategory)
                    if (returnPackege.Priority < packege.Priority)
                        returnPackege = packege;
                    else if (returnPackege.Priority == packege.Priority)
                        if (returnPackege.WeightCatgory < packege.WeightCatgory)
                            returnPackege = packege;
                        else if (returnPackege.WeightCatgory == packege.WeightCatgory)
                            if (Distans(drone.location, ClientLocation(packege.SendClient)) < Distans(drone.location, ClientLocation(returnPackege.SendClient)))
                                returnPackege = packege;

            }
            if(returnPackege.SerialNumber==0)
            throw new DroneCantMakeDliveryException();
            drone.numPackage = returnPackege.SerialNumber;
            drone.droneStatus = DroneStatus.Work;
            dalObj.ConnectPackageToDrone(returnPackege.SerialNumber, droneNumber);
            for (int i = 0; i < dronesListInBl.Count; i++)
            {
                if (dronesListInBl[i].SerialNumber == drone.SerialNumber)
                    dronesListInBl[i] = drone;
            }

        }

    }
}
