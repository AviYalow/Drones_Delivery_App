using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

using DalApi;
namespace BlApi
{
    partial class BL : IBL
    {
        /// <summary>
        /// add packege
        /// </summary>
        /// <param name="package"> packege to add</param>
        /// <returns> serial number of the packege</returns>
        public uint AddPackege(Package package)
        {
            if (package.Priority > Priority.Regular || package.WeightCatgory > WeightCategories.Heavy)
                throw new InputErrorException();
            uint packegeNum = 0;
            var send = dalObj.CilentByNumber(package.SendClient.Id);
            if (!send.Active)
                throw new ItemNotFoundException("Client", package.SendClient.Id);
            Location locationsend = new Location { Latitude = send.Latitude, Longitude = send.Longitude };
            Location locationGet = ClientLocation(package.RecivedClient.Id);
            var butrryWithDelvery = buttryDownPackegeDelivery(convertPackegeBlToPackegeInTrnansfer(package));
            var butrryFree = buttryDownWithNoPackege(ClosestBase(locationsend).location, locationsend) + buttryDownWithNoPackege(ClosestBase(locationGet).location, locationGet);
            if (butrryWithDelvery + butrryFree > 100)
                throw new MoreDistasThenMaximomException(package.SendClient.Id, package.RecivedClient.Id);


            try
            {

                packegeNum = dalObj.AddPackage(package.convertPackageBltopackegeDal());
            }
            catch (DO.ItemFoundException ex)
            {
                throw (new ItemFoundExeption(ex));
            }



            return packegeNum;

        }

        /// <summary>
        /// Updating fields of a particular package in the data layer
        /// </summary>
        /// <param name="package"> particular package</param>
        public void UpdatePackegInDal(Package package)
        {
            dalObj.UpdatePackege(package.convertPackageBltopackegeDal());
        }

        /// <summary>
        ///  view a package
        /// </summary>
        /// <param name="number">serial number of package</param>
        /// <returns> package in the logical layer</returns>
        public Package ShowPackage(uint number)
        {

            try
            {
                var dataPackege = dalObj.packegeByNumber(number);
                return convertPackegeDalToBl(dataPackege);
            }
            catch (DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }

        }

        /// <summary>
        /// delete packege 
        /// </summary>
        /// <param name="number"> serial nummber of package</param>
        public void DeletePackege(uint number)
        {
            try
            {
                var packege = dalObj.packegeByNumber(number);
                //cheack the packege not send yet
                if (packege.PackageAssociation != null)
                { throw new ThePackegeAlredySendException(); }
                if (packege.OperatorSkimmerId != 0)
                {
                    var drone = SpecificDrone(packege.OperatorSkimmerId);
                    drone.DroneStatus = DroneStatus.Free;
                    
                    drone.NumPackage = 0;

                    for (int i = 0; i < dronesListInBl.Count; i++)
                    {
                        if (dronesListInBl[i].SerialNumber == drone.SerialNumber)
                            dronesListInBl[i] = drone;
                    }
                }
                dalObj.DeletePackege(number);
            }
            catch (DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }
        }

        /// <summary>
        /// convert packege from the data layer to the logical layer
        /// </summary>
        /// <param name="dataPackege"> package in the data layer </param>
        /// <returns>  package in the logical layer</returns>
        Package convertPackegeDalToBl(DO.Package dataPackege)
        {
            return new Package
            {
                SerialNumber = dataPackege.SerialNumber,
                SendClient = dalObj.CilentByNumber(dataPackege.SendClient).clientInPackageFromDal(),
                CollectPackage = dataPackege.CollectPackageForShipment,
                Create_package = dataPackege.ReceivingDelivery,
                Drone = dataPackege.OperatorSkimmerId != 0 ? SpecificDrone(dataPackege.OperatorSkimmerId).droneToDroneInPackage() : null,
                PackageArrived = dataPackege.PackageArrived,
                PackageAssociation = dataPackege.PackageAssociation,
                Priority = (Priority)dataPackege.Priority,
                RecivedClient = dalObj.CilentByNumber(dataPackege.GetingClient).clientInPackageFromDal(),
                WeightCatgory = (WeightCategories)dataPackege.WeightCatgory
            };

        }
    }
}
