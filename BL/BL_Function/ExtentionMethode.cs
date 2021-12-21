using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
using BO;
using DalApi;
using DO;


namespace BlApi
{
   
       internal static class ExtentionMethode
        {

            /// <summary>
            /// Auxiliary function that converts a base station object from
            /// the data layer to a base station object on the logical layer
            /// </summary>
            /// <param name="baseStation">serial number of the base station</param>
            /// <returns> base station on logical layer </returns>
          public  static BaseStation convertBaseDalToBaseBl(this DO.Base_Station baseStation)
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
            /// <summary>
            /// convert base from dal to base in list
            /// </summary>
            /// <param name="base_Station"></param>
            /// <returns></returns>
          public  static BaseStationToList convertBaseInDalToBaseStationList(this DO.Base_Station base_Station, IDal dalObj )
            {
                var base_ = new BaseStationToList
                {
                    SerialNum = base_Station.baseNumber,
                    FreeState = base_Station.NumberOfChargingStations,
                    Name = base_Station.NameBase
                };
                base_.BusyState = (uint)dalObj.ChargingDroneList().Count(x => x.idBaseStation == base_Station.baseNumber);
                return base_;
            }
        
    }
}
