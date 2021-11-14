using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    partial class BL:IBL
    {
        /// <summary>
        /// poll out the client location
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Location Client_location(uint id)
        {
            IDAL.DO.Client client = new IDAL.DO.Client();
            try
            {
                client = dalObj.cilent_by_number(id);
            }
            catch(IDAL.DO.Item_not_found_exception ex)
            {
                throw (new Item_not_found_exception(ex));
            }
            Location location_client = new Location();
            location_client.Latitude = client.Latitude;
            location_client.Longitude = client.Longitude;
            return location_client;
        }

        public void AddClient(Client client)
        {
            try
            {
                dalObj.Add_client(client.ID, client.Name, client.Phone, client.location.Latitude, client.location.Longitude);
            }
            catch(IDAL.DO.Item_found_exception ex)
            {
                throw (new Item_found_exeption(ex));
            }
        }

    }
}
