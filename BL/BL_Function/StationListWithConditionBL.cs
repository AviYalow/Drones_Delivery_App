using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {


        /// <summary>
        /// show base station list 
        /// </summary>
        /// <returns> base station list </returns>
        public IEnumerable<BaseStationToList> BaseStationToLists()
        {
            var base_= dalObj.BaseStationList(x => true).ToList().ConvertAll(convertBaseInDalToBaseStationList);
            if (base_.Count == 0)
                throw new TheListIsEmptyException();
            return base_;
        }
        /// <summary>
        /// return base station with free place for drone
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseStationToList> BaseStationWhitFreeChargingStationToLists()
        {
            var base_ = dalObj.BaseStationList(x => x.NumberOfChargingStations>0).ToList().ConvertAll(convertBaseInDalToBaseStationList);
            if (base_.Count == 0)
                throw new TheListIsEmptyException();
            return base_;
        }

      


    }
}
