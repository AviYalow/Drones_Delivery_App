using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;

namespace DalObject
{
    partial class DalObject
    {

        bool sustainability_test_c(int number)
        {

            foreach (Client item in DataSource.clients)
            {
                if (item.ID == number)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adding a new client
        /// </summary>
        /// <param name="id">ID of the new client</param>
        /// <param name="name">Name of the new cliet</param>
        /// <param name="phone">Phone number</param>
        /// <param name="latitude">Latitude of the client</param>
        /// <param name="londitude">Londitude of the client</param>

        public void Add_client(int id, string name, string phone, double latitude, double londitude)
        {

          
            if (sustainability_test_c( id))
                throw (new DAL.Item_found_exception("Client", id));
            DataSource.clients.Add(new Client
            {
                ID = id,
                Name = name,
                PhoneNumber = phone,
                Latitude = latitude,
                Longitude = londitude
            });


        }

        /// <summary>
        /// Display client data desired 
        /// </summary>
        /// <param name="id">ID of desired client </param>
        /// <returns> string of data </returns>
        public Client cilent_by_number(int id)
        {
              if (sustainability_test_c(id))
             throw (new DAL.Item_not_found_exception("client",id));
            return DataSource.clients[DataSource.clients.FindIndex(x => x.ID == id)];

        }


        /// <summary>
        /// Print thr all clients
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values ​​of all the clients so we can print them</param>
        public IEnumerable<Client> cilent_list()
        {
            return DataSource.clients.ToList<Client>();
        }

        /// <summary>
        /// delete a spsific client 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteClient(int id)
        {
            if (sustainability_test_c(id))
                throw (new DAL.Item_not_found_exception("client", id));

            DataSource.clients.Remove(DataSource.clients[DataSource.clients.FindIndex(x => x.ID == id)]);


        }
    }
}
