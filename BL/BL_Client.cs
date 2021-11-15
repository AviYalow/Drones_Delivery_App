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
                client = dalObj.CilentByNumber(id);
            }
            catch(IDAL.DO.ItemNotFoundException ex)
            {
                throw (new ItemNotFoundException(ex));
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
                dalObj.AddClient(client.ID, client.Name, client.Phone, client.location.Latitude, client.location.Longitude);
            }
            catch(IDAL.DO.ItemFoundException ex)
            {
                throw (new ItemFoundExeption(ex));
            }
        }

    }
}
