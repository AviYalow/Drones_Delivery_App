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
        public Location ClientLocation(uint id)
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
        /// <summary>
        /// add client
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Client client)
        {
            //chcinkg id
            if(client.Id<1000000000)
            { throw new NumberNotEnoughException(9); }
            //chcing phon number
            chkingFon(client.Phone);
            
            try
            {
                dalObj.AddClient(new IDAL.DO.Client {Id=client.Id,Latitude= client.Location.Latitude,Longitude= client.Location.Longitude,
                    Name= client.Name,PhoneNumber= client.Phone});
            }
            catch(IDAL.DO.ItemFoundException ex)
            {
                throw (new ItemFoundExeption(ex));
            }
        }
        /// <summary>
        /// help mathod to chack phonumber
        /// </summary>
        /// <param name="fon"></param>
        void chkingFon( string fon)
        {
            if(fon.Count()<10)
            { throw new NumberNotEnoughException(10); }
            if (fon[0]!='0'||fon[1]!='5')
            { throw new StartingException("0,5"); }
            if (fon.Any(c=>c<'0'||c>'9'))
            { throw new IllegalDigitsException(); }
            fon = fon.Insert(3, "-");
            fon = fon.Insert(7, "-");

        }

    }
}
