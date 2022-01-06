using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using System.Runtime.CompilerServices;
using DalApi;

namespace BlApi
{
    partial class BL : IBL
    {

        /// <summary>
        /// show base station list 
        /// </summary>
        /// <returns> base station list </returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationToList> BaseStationToLists()
        {

            var base_ = from x in dalObj.BaseStationList(x => x.Active)
                        select x.convertBaseInDalToBaseStationList(dalObj);

            if (base_.Count() == 0)
                return null;
            
            return base_;
        }
        /// <summary>
        /// return base station with free place for drone
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationToList> BaseStationWhitFreeChargingStationToLists()
        {
            var base_ = from x in dalObj.BaseStationList(x => x.NumberOfChargingStations > 0&&x.Active)
                        select x.convertBaseInDalToBaseStationList(dalObj);
           
            return base_;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationToList> AllBaseStation()
        {
            try
            {
                return from base_ in dalObj.BaseStationList(x => true)
                       select base_.convertBaseInDalToBaseStationList(dalObj);
            }
            catch(Exception)
            {
                return null;
            }
        }
        
    }
}
