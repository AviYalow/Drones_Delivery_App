using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;

namespace DAL
{
   partial class DalObject_Client:DalObject.DalObject
    {
        public static bool sustainability_test(int number)
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
            //  if (sustainability_test(id))
            // throw ("Error: The client already exist in the system");
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
          //  if (!sustainability_test(id))
          // throw ("Error: The client does not exist in the system");
            foreach (Client client in DataSource.clients)
            {
                if (client.ID == id)
                {
                    return (client);

                }
            }
            return DataSource.clients[0];
        }


        /// <summary>
        /// Print thr all clients
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values ​​of all the clients so we can print them</param>
        public IEnumerable<Client> cilent_list()
        {
            foreach (Client item in DataSource.clients)
                yield return item;
        }

        /// <summary>
        /// delete a spsific client 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteClient(int id)
        {
            //  if (!sustainability_test(id))
            // throw ("Error: The client does not exist in the system");
            for (int i = 0; i < DataSource.clients.Count(); i++)
            {
                if (DataSource.clients[i].ID == id)
                {
                    DataSource.clients.Remove(DataSource.clients[i]);
                    return;
                }
            }
        }
    }
}
