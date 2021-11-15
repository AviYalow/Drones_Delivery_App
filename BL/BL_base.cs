using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;
using IBL.BO;

namespace IBL
{
   partial class BL:IBL
    {
        /// <summary>
        /// clculet the most collset base to the cilent
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public Location Clloset_base(uint client)
        {
            Location new_location = new Location();
            try
            {
                Location client_location = Client_location(client);
                Location base_location = new Location();

                double distans = 0, distans2;
                foreach (IDAL.DO.Base_Station base_ in dalObj.BaseStationList())
                {
                    base_location.Latitude = base_.latitude;
                    base_location.Longitude = base_.longitude;
                    distans2 = Distans(client_location, base_location);
                    if (distans < distans2 || distans == 0)
                    {
                        distans = distans2;
                        new_location = base_location;
                    }
                }
            }
            catch(IDAL.DO.ItemNotFoundException ex)
            {
                throw (new ItemNotFoundException(ex));
            }
            return new_location;
        }
        /// <summary>
        /// geting location for spsific base
        /// </summary>
        /// <param name="base_number"></param>
        /// <returns></returns>
        public Location Base_location(uint base_number)
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
                dalObj.AddStation(baseStation.SerialNum, baseStation.Name, baseStation.FreeState, baseStation.location.Latitude, baseStation.location.Longitude);
            }
            catch (IDAL.DO.ItemFoundException ex)
            {
                throw (new ItemFoundExeption(ex));
            }
            baseStation.dronesInCharge.Clear();
        }
    }
}
