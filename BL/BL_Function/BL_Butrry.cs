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
        /// calculate how match butrry drone need for delivery point to point
        /// </summary>
        /// <param name="packegeNumber"></param>
        /// <returns></returns>
        double buttryDownPackegeDelivery(PackageInTransfer packageInTransfer)
        {
            double[] elctricity = dalObj.Elctrtricity();
            double battery_drop = 0;

            switch (packageInTransfer.WeightCatgory)//להשלים פונקציה ע"י חישוב לכל משקל
            {
                case WeightCategories.Easy:
                    battery_drop = ((packageInTransfer.Distance / (double)SpeedDrone.Easy) * (double)ButrryPer.Minute);
                    battery_drop *= elctricity[1];
                    break;
                case WeightCategories.Medium:
                    battery_drop = ((packageInTransfer.Distance / (double)SpeedDrone.Medium) * (double)ButrryPer.Minute);
                    battery_drop *= elctricity[2];
                    break;
                case WeightCategories.Heavy:
                    battery_drop = ((packageInTransfer.Distance / (double)SpeedDrone.Heavy) * (double)ButrryPer.Minute);
                    battery_drop *= elctricity[3];
                    break;
                default:
                    break;

            }
            return battery_drop;
        }

        /// <summary>
        /// calculate how match butrry drone need for to get from base to client or client to base
        /// </summary>
        /// <param name="fromLocation"></param>
        /// <param name="toLocation"></param>
        /// <returns></returns>
        double buttryDownWithNoPackege(Location fromLocation, Location toLocation)
        {
            double[] elctricity = dalObj.Elctrtricity();
            double distans = Distans(fromLocation, toLocation);
            double buttry = ((distans / (double)SpeedDrone.Free) * (double)ButrryPer.Minute) * elctricity[0];
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
            //מחפש את הרחפן
            var drone = dronesListInBl.Find(x => x.SerialNum == droneNumber);
            if (drone == null)
                throw new ItemNotFoundException("Drone", droneNumber);
            //מחפש את התא במערך המוטענים
            var information = (dalObj.ChargingDroneList().ToList().Find(x => x.IdDrone == droneNumber));
            if (information.Equals(new IDAL.DO.BatteryLoad()))
                throw new ItemNotFoundException("Drone", droneNumber);
            //מחשב כמה הסוללה נטענה
            double buttry = DroneChrgingAlredy(information.EntringDrone,DateTime.Now);

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
        public void FreeBaseFromDrone(uint baseNumber, int number)
        {
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
                if (i <= number)
                {
                    FreeDroneFromCharging(droneChrging.IdDrone, droneChrging.EntringDrone - DateTime.Now);
                    i++;
                             
                }
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
            return ((newdateTime - dateTime).Seconds) * ((dalObj.Elctrtricity().ToList()[4]) / 60.0);
        }


    }
}
