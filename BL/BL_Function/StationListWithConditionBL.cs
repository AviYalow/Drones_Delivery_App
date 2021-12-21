using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

using DalApi;

namespace BlApi
{
    partial class BL : IBL
    {


        /// <summary>
        /// show base station list 
        /// </summary>
        /// <returns> base station list </returns>
        public IEnumerable<BaseStationToList> BaseStationToLists()
        {

            var base_ = from x in dalObj.BaseStationList(x => true)
                        select x.convertBaseInDalToBaseStationList(dalObj);

            if (base_.Count() == 0)
                throw new TheListIsEmptyException();
            
            return base_;
        }
        /// <summary>
        /// return base station with free place for drone
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseStationToList> BaseStationWhitFreeChargingStationToLists()
        {
            var base_ = from x in dalObj.BaseStationList(x => x.NumberOfChargingStations > 0)
                        select x.convertBaseInDalToBaseStationList(dalObj);
           
            return base_;
        }

      


    }
}
