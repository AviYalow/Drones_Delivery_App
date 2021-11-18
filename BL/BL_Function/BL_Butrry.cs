using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    partial class BL : IBL
    {


        /// <summary>
        /// clculate how match butrry drone need for dleviry point to point
        /// </summary>
        /// <param name="packegeNumber"></param>
        /// <returns></returns>
        double buttryDownPackegeDelivery(uint packegeNumber)
        {
            double[] elctricity = dalObj.Elctrtricity();
            double battery_drop = 0, distans = 0;

            IDAL.DO.Package package = dalObj.packegeByNumber(packegeNumber);
            Location sending_location = ClientLocation(package.SendClient), geting_location = ClientLocation(package.GetingClient);

            distans = Distans(geting_location, sending_location);

            switch ((WeightCategories)package.WeightCatgory)//להשלים פונקציה ע"י חישוב לכל משקל
            {
                case WeightCategories.Easy:
                    battery_drop = ((distans / (double)SpeedDrone.Easy) / (double)ButrryPer.Minute);
                    battery_drop *= elctricity[1];
                    break;
                case WeightCategories.Medium:
                    battery_drop = ((distans / (double)SpeedDrone.Medium) / (double)ButrryPer.Minute);
                    battery_drop *= elctricity[2];
                    break;
                case WeightCategories.Heavy:
                    battery_drop = ((distans / (double)SpeedDrone.Heavy) / (double)ButrryPer.Minute);
                    battery_drop *= elctricity[3];
                    break;
                default:
                    break;

            }
            return battery_drop;
        }

        /// <summary>
        /// clculate how match butrry drone need for to get from base to client or client to base
        /// </summary>
        /// <param name="fromLocation"></param>
        /// <param name="toLocation"></param>
        /// <returns></returns>
        double buttryDownWithNoPackege(Location fromLocation, Location toLocation)
        {
            double[] elctricity = dalObj.Elctrtricity();
            double distans = Distans(fromLocation, toLocation);
            double buttry = ((distans / (double)SpeedDrone.Free) / (double)ButrryPer.Minute) * elctricity[0];
            return buttry;
        }
        /// <summary>
        /// send drone to charge
        /// </summary>
        /// <param name="dronenumber"></param>
        /// <param name="base_"></param>
        public void DroneToCharge(uint dronenumber )
        {


            var drone = SpecificDrone(dronenumber);
            BaseStation baseStation = ClosestBase(drone.location);
            if (drone.droneStatus != DroneStatus.Free)
            {
                throw new DroneCantSendToChargeException();
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

                dalObj.UpdateBase(new IDAL.DO.Base_Station
                {
                    baseNumber = baseStation.SerialNum,
                    latitude = baseStation.location.Latitude,
                    longitude = baseStation.location.Longitude,
                    NameBase = baseStation.Name,
                    NumberOfChargingStations = baseStation.FreeState - 1
                });
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
        public double FreeDroneFromCharging(uint droneNumber, uint timeInCharge)
        {
            //מחפש את הרחפן
            var drone = dronesListInBl.Find(x => x.SerialNum == droneNumber);
            if (drone == null)
                throw new ItemNotFoundException("Drone", droneNumber);
            //מחפש את התא במערך המוטענים
            var information = (dalObj.ChargingDroneList().ToList().Find(x => x.IdDrone == droneNumber));
            if (information.Equals(new IDAL.DO.BatteryLoad()))
                throw new ItemNotFoundException("Drone", droneNumber);
            //מחשב כמה הסוללה נטענה
            double buttry = DroneChrgingAlredy(information.EntringDrone, information.EntringDrone.AddMinutes(timeInCharge));

            drone.butrryStatus = buttry > 100 ? 100 : buttry+drone.butrryStatus;
            drone.droneStatus = DroneStatus.Free;
            var baseStation = dalObj.BaseStationByNumber(information.idBaseStation);
            baseStation.NumberOfChargingStations++;
            dalObj.UpdateBase(baseStation);
            dalObj.FreeDroneFromCharge(drone.SerialNum);
            dronesListInBl[dronesListInBl.FindIndex(x => x.SerialNum == drone.SerialNum)] = drone;
            

            return drone.butrryStatus;

        }

        /// <summary>
        /// free all drone in charge in spsifice base
        /// </summary>
        /// <param name="baseNumber"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public IEnumerable<DroneInCharge> FreeBaseFromDrone(uint baseNumber, int number)
        {
            try
            {
                if (dalObj.BaseStationByNumber(baseNumber).NumberOfChargingStations - number < 0)
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
            foreach (var droneChrging in dalObj.ChargingDroneList().ToList().FindAll(x => x.idBaseStation == baseNumber))
            {
                if (i <= number)
                {
                    returnDrone.SerialNum = droneChrging.IdDrone;
                    returnDrone.butrryStatus = DroneChrgingAlredy(droneChrging.EntringDrone, DateTime.Now);
                    i++;
                    dalObj.FreeDroneFromCharge(droneChrging.IdDrone);
                    list.Add(returnDrone);
                }
            }
            return list;
        }

        /// <summary>
        /// Some drone has already been charged 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="newdateTime"></param>
        /// <returns></returns>
        public double DroneChrgingAlredy(DateTime dateTime, DateTime newdateTime=default)
        {
            return ((newdateTime - dateTime).Seconds) * ((dalObj.Elctrtricity().ToList()[4]) / 60.0);
        }


    }
}
