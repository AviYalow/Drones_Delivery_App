using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

using DalApi;
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

            switch (packageInTransfer.WeightCatgory)
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
        /// send drone to charge
        /// </summary>
        /// <param name="droneNumber">serial number of drone</param>
        public void DroneToCharge(uint droneNumber )
        {


            var drone = SpecificDrone(droneNumber);
            BaseStation baseStation = ClosestBase(drone.Location);
            if (drone.DroneStatus != DroneStatus.Free)
            {
                throw new DroneStillAtWorkException();
            }
           // var baseStation = CllosetBase(drone.location);
            double buttry = buttryDownWithNoPackege(drone.Location, baseStation.location);
            if (drone.ButrryStatus - buttry < 0)
            {
                throw new NoButrryToTripException(buttry);
            }

            try
            {

                if (baseStation.FreeState <= 0)
                    throw (new NoPlaceForChargeException(baseStation.SerialNum));
                dalObj.DroneToCharge(droneNumber, baseStation.SerialNum);
                drone.DroneStatus = DroneStatus.Maintenance;
                drone.Location = baseStation.location;
                dronesListInBl[dronesListInBl.FindIndex(x => x.SerialNumber == droneNumber)] = drone;

            
            }
            catch (DO.ItemNotFoundException ex)
            {
                throw (new ItemNotFoundException(ex));
            }
            catch (DO.ItemFoundException ex)
            {
                throw new ItemFoundExeption(ex);
            }


        }

        /// <summary>
        ///Charging drone function 
        /// </summary>
        /// <param name="droneNumber">serial number of the drone</param>
        /// <param name="timeInCharge"> the time that the drone in charge </param>
        /// <returns> butrry Status of the  drone</returns>
        public double FreeDroneFromCharging(uint droneNumber, int number = -1)
        {
            //locking for drone
            var drone = dronesListInBl.Find(x => x.SerialNumber == droneNumber);
            if (drone == null)
                throw new ItemNotFoundException("Drone", droneNumber);
            //locking the drone in charge
            var information = dalObj.ChargingDroneList(). FirstOrDefault(x => x.IdDrone == droneNumber);
           
            if (information.Equals(new DO.BatteryLoad()))
                throw new ItemNotFoundException("Drone", droneNumber);
            //calcoulet how mach he chraging alredy
            double buttry = DroneChrgingAlredy(DateTime.Now-information.EntringDrone);

            drone.ButrryStatus = buttry > 100 ? 100 : buttry+drone.ButrryStatus;
            drone.DroneStatus = DroneStatus.Free;
            var baseStation = dalObj.BaseStationByNumber(information.idBaseStation);
            baseStation.NumberOfChargingStations++;
            dalObj.UpdateBase(baseStation);
            dalObj.FreeDroneFromCharge(drone.SerialNumber);
            dronesListInBl[dronesListInBl.FindIndex(x => x.SerialNumber == drone.SerialNumber)] = drone;
            

            return drone.ButrryStatus;

        }

        /// <summary>
        /// Release a drone from a charger at a particular base station
        /// </summary>
        /// <param name="baseNumber"> serial number of the base station</param>
        /// <param name="number"> amount of drone to release</param>
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
            catch (DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }
           
            
            int i = 0;
            var returnDrone = new DroneInCharge();
            List<DroneInCharge> list = new List<DroneInCharge>();
            foreach (var droneChrging in dalObj.ChargingDroneList().Where(x => x.idBaseStation == baseNumber))
            {
                if (number != -1)
                {
                    if (i <= number)
                    {
                        //FreeDroneFromCharging(droneChrging.IdDrone, droneChrging.EntringDrone - DateTime.Now);
                        FreeDroneFromCharging(droneChrging.IdDrone);
                        i++;

                    }
                }
                else
                    //FreeDroneFromCharging(droneChrging.IdDrone, droneChrging.EntringDrone - DateTime.Now);
                    FreeDroneFromCharging(droneChrging.IdDrone);            }

            }

            /// <summary>
            /// Calculates the percentage of battery of the drone based on the charging time it was
            /// </summary>
            /// <param name="span">charging time the drone was</param>
            /// <returns> percentage of battery</returns>
            double DroneChrgingAlredy(TimeSpan span)
        {
            var a = ((double)(span).TotalMinutes); var b= (chargingPerMinote );
            a *= b;
            return a;
        }


    }
}
