using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;
using IBL.BO;

namespace IBL
{
    partial class BL : IBL
    {
        /// <summary>
        /// clculet the most collset base to the cilent
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public BaseStation CllosetBase(Location location)
        {
            BaseStation baseStation = new BaseStation();
            baseStation.location = new Location();
            try
            {

                Location base_location = new Location();

                double distans = -5, distans2;
                foreach (IDAL.DO.Base_Station base_ in dalObj.BaseStationList())
                {
                    base_location.Latitude = base_.latitude;
                    base_location.Longitude = base_.longitude;
                    distans2 = Distans(location, base_location);
                    if (distans > distans2 || distans == -5)
                    {

                        distans = distans2;
                        baseStation = new BaseStation
                        {
                            location = base_location,
                            Name = base_.NameBase,
                            SerialNum = base_.baseNumber,
                            FreeState = base_.NumberOfChargingStations,
                            dronesInCharge = null
                        };

                    }
                }
            }
            catch (IDAL.DO.ItemNotFoundException ex)
            {
                throw (new ItemNotFoundException(ex));
            }
            return baseStation;
        }
        /// <summary>
        /// geting location for spsific base
        /// </summary>
        /// <param name="base_number"></param>
        /// <returns></returns>
        public Location BaseLocation(uint base_number)
        {
            IDAL.DO.Base_Station base_Station = new IDAL.DO.Base_Station();
            try
            {
                base_Station = dalObj.BaseStationByNumber(base_number);
            }
            catch (IDAL.DO.ItemNotFoundException ex)
            {
                throw (new ItemNotFoundException(ex));
            }
            Location base_location = new Location();

            base_location.Latitude = base_Station.latitude;
            base_location.Longitude = base_Station.longitude;
            return base_location;
        }
        /// <summary>
        /// add base station
        /// </summary>
        /// <param name="baseStation"></param>
        public void AddBase(BaseStation baseStation)
        {
            try
            {
                dalObj.AddStation(new IDAL.DO.Base_Station
                {
                    baseNumber = baseStation.SerialNum,
                    NameBase = baseStation.Name,
                    NumberOfChargingStations = baseStation.FreeState,
                    latitude = baseStation.location.Latitude,
                    longitude = baseStation.location.Longitude
                });
            }
            catch (IDAL.DO.ItemFoundException ex)
            {
                throw (new ItemFoundExeption(ex));
            }
            baseStation.dronesInCharge.Clear();
        }

        public void UpdateBase(uint base_,string newName = "", int newNumber = -1)
        {
            var baseUpdat = new IDAL.DO.Base_Station();
            try
            {
                baseUpdat = dalObj.BaseStationByNumber(base_);
            }
            catch (IDAL.DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }
            if (newName != "")
                baseUpdat.NameBase = newName;
            if (newNumber != -1)
            {
                int droneInCharge = dalObj.ChargingDroneList().Count(x => x.idBaseStation == (uint)newNumber);
                baseUpdat.NumberOfChargingStations = (droneInCharge <= newNumber) ?
                    (uint)newNumber : throw new UpdateChargingPositionsException(droneInCharge);

            }
            dalObj.UpdateBase(baseUpdat);
        }

        BaseStation convertBaseDalToBaseBl(IDAL.DO.Base_Station baseStation)
        {
            return new BaseStation
            {
                SerialNum = baseStation.baseNumber,
                Name = baseStation.NameBase,
                FreeState = baseStation.NumberOfChargingStations,
                location = new Location { Latitude = baseStation.latitude, Longitude = baseStation.longitude },
                dronesInCharge = null
            };
        }

        public BaseStation BaseByNumber(uint baseNume)
        {
            try
            {
                var baseStation = dalObj.BaseStationByNumber(baseNume);
                var baseReturn = new BaseStation { SerialNum = baseNume, location = new Location { Latitude = baseStation.latitude, Longitude = baseStation.longitude }, Name = baseStation.NameBase };
                var droneInCharge = dalObj.ChargingDroneList().ToList().FindAll(x => x.idBaseStation == baseNume);
                baseReturn.dronesInCharge = new List<DroneInCharge>();
                foreach (var drone in droneInCharge)
                {
                    var butrry = (SpecificDrone(drone.IdDrone).butrryStatus + DroneChrgingAlredy(drone.EntringDrone, DateTime.Now));
                    butrry = (butrry > 100) ? 100 : butrry;
                    baseReturn.dronesInCharge.Add(new DroneInCharge { SerialNum = drone.IdDrone, butrryStatus =butrry  });
                }
                return baseReturn;
            }
            catch (IDAL.DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }
        }

        public IEnumerable<BaseStationToList> BaseStationToLists()
        {
            List<BaseStationToList> baseStationToLists = new List<BaseStationToList>();
            if (dalObj.BaseStationList().Count() == 0)
                throw new TheListIsEmptyException();
            uint i = 0;
            foreach(var baseStationFromDal in dalObj.BaseStationList())
            {
                i = 0;
                foreach(var chargePerBase in dalObj.ChargingDroneList())
                {
                    if (chargePerBase.idBaseStation == baseStationFromDal.baseNumber)
                        i++;
                }
                baseStationToLists.Add(new BaseStationToList
                {
                    SerialNum = baseStationFromDal.baseNumber,
                    Name = baseStationFromDal.NameBase,
                    BusyState = i,
                    FreeState = baseStationFromDal.NumberOfChargingStations
                });
            }
            return baseStationToLists;
        }

        public IEnumerable<BaseStationToList> BaseStationWhitChargeStationsToLists()
        {
            List<BaseStationToList> baseStationToLists = new List<BaseStationToList>();
            if (dalObj.BaseStationList().Count() == 0)
                throw new TheListIsEmptyException();
            uint i = 0;
            foreach (var baseStationFromDal in dalObj.BaseStationListWithChargeStates())
            {
                i = 0;
                foreach (var chargePerBase in dalObj.ChargingDroneList())
                {
                    if (chargePerBase.idBaseStation == baseStationFromDal.baseNumber)
                        i++;
                }
                baseStationToLists.Add(new BaseStationToList
                {
                    SerialNum = baseStationFromDal.baseNumber,
                    Name = baseStationFromDal.NameBase,
                    BusyState = i,
                    FreeState = baseStationFromDal.NumberOfChargingStations
                });
            }
            return baseStationToLists;
        }

    }
}
