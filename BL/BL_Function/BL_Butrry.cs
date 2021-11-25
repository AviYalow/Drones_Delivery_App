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
        /// Calculate how much percentage of battery is needed to delivery a package
        /// </summary>
        /// <param name="packageInTransfer"> package</param>
        /// <returns> percentage of battery needed</returns>
        double buttryDownPackegeDelivery(PackageInTransfer packageInTransfer)
        {
            
            double battery_drop = 0;

            switch (packageInTransfer.WeightCatgory
            {
                case WeightCategories.Easy:
                    battery_drop = ((packageInTransfer.Distance / (double)SpeedDrone.Easy) * (double)ButrryPer.Minute);
                    battery_drop *= easyElctric;
                    break;
                case WeightCategories.Medium:
                    battery_drop = ((packageInTransfer.Distance / (double)SpeedDrone.Medium) * (double)ButrryPer.Minute);
                    battery_drop *= mediomElctric;
                    break;
                case WeightCategories.Heavy:
                    battery_drop = ((packageInTransfer.Distance / (double)SpeedDrone.Heavy) * (double)ButrryPer.Minute);
                    battery_drop *= heaviElctric;
                    break;
                default:
                    break;

            }
            return battery_drop;
        }

        /// <summary>
        /// calculate how match butrry drone need to get from base to client or client to base
        /// </summary>
        /// <param name="fromLocation"> source location</param>
        /// <param name="toLocation"> destination location</param>
        /// <returns> percentage of battery needed</returns>
        double buttryDownWithNoPackege(Location fromLocation, Location toLocation)
        {
            
            double distans = Distans(fromLocation, toLocation);
            double buttry = ((distans / (double)SpeedDrone.Free) * (double)ButrryPer.Minute) * freeElctric;
            return buttry;
        }

        /// <summary>
        ///  send drone to charge
        /// </summary>
        /// <param name="dronenumber">serial number of drone</param>
       
        public void DroneToCharge(uint dronenumber )
        {


            var drone = SpecificDrone(dronenumber);
            BaseStation baseStation = ClosestBase(drone.location);
            if (drone.droneStatus != DroneStatus.Free)
            {
                throw new DroneStillAtWorkException();
            }
           // var baseStation = CllosetBase(drone.location);
            double buttry = buttryDownWithNoPackege(drone.location, baseStation.location);
            if (drone.butrryStatus - buttry < 0)
            {
                throw new NoButrryToTripException(buttry);
            }

            try
            {

                if (baseStation.FreeState <= 0)
                    throw (new NoPlaceForChargeException(baseStation.SerialNum));
                dalObj.DroneToCharge(dronenumber, baseStation.SerialNum);

            
            }
            catch (IDAL.DO.ItemNotFoundException ex)
            {
                throw (new ItemNotFoundException(ex));
            }
            catch (IDAL.DO.ItemFoundException ex)
            {
                throw new ItemFoundExeption(ex);
            }


        }
        /// <summary>
        /// free drone from charge
        /// </summary>
        /// <param name="droneNumber"></param>
        /// <param name="timeInCharge"></param>
        /// <returns></returns>
        public double FreeDroneFromCharging(uint droneNumber, TimeSpan timeInCharge)
        {
            //locking for drone
            var drone = dronesListInBl.Find(x => x.SerialNumber == droneNumber);
            if (drone == null)
                throw new ItemNotFoundException("Drone", droneNumber);
            //locking the drone in charge
            var information = dalObj.ChargingDroneList().First(x => x.IdDrone == droneNumber);
            
            if (information.Equals(new IDAL.DO.BatteryLoad()))
                throw new ItemNotFoundException("Drone", droneNumber);
            //calcoulet how mach he chraging alredy
            double buttry = DroneChrgingAlredy(information.EntringDrone,DateTime.Now);

            drone.butrryStatus = buttry > 100 ? 100 : buttry+drone.butrryStatus;
            drone.droneStatus = DroneStatus.Free;
            var baseStation = dalObj.BaseStationByNumber(information.idBaseStation);
            baseStation.NumberOfChargingStations++;
            dalObj.UpdateBase(baseStation);
            dalObj.FreeDroneFromCharge(drone.SerialNumber);
            dronesListInBl[dronesListInBl.FindIndex(x => x.SerialNumber == drone.SerialNumber)] = drone;
            

            return drone.butrryStatus;

        }

        /// <summary>
        /// free all drone in charge in spsifice base
        /// </summary>
        /// <param name="baseNumber"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public void FreeBaseFromDrone(uint baseNumber, int number=-1)
        {
            if(number!=-1)
            
            try
            {
                if (dalObj.ChargingDroneList().Count(x=>x.idBaseStation==baseNumber) - number < 0)
                {
                    throw (new TryToPullOutMoreDrone());
                }
            }
            catch (IDAL.DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }
           
            
            int i = 0;
            var returnDrone = new DroneInCharge();
            List<DroneInCharge> list = new List<DroneInCharge>();
            foreach (var droneChrging in dalObj.ChargingDroneList().Where(x => x.idBaseStation == baseNumber))
            {
                if(number!=-1)
                if (i <= number)
                {
                    FreeDroneFromCharging(droneChrging.IdDrone, droneChrging.EntringDrone - DateTime.Now);
                    i++;
                             
                }
                else
                        FreeDroneFromCharging(droneChrging.IdDrone, droneChrging.EntringDrone - DateTime.Now);
            }
           
        }

        /// <summary>
        /// Some drone has already been charged 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="newdateTime"></param>
        /// <returns></returns>
        public double DroneChrgingAlredy(DateTime dateTime, DateTime newdateTime=default)
        {
            return ((newdateTime - dateTime).Seconds) * (chargingPerMinote / 60.0);
        }


    }
}
