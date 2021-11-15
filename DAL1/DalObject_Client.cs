using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;

namespace DalObject
{
    partial class DalObject : IDAL.IDal
    {

        

        /// <summary>
        /// Adding a new client
        /// </summary>
        /// <param name="id">ID of the new client</param>
        /// <param name="name">Name of the new cliet</param>
        /// <param name="phone">Phone number</param>
        /// <param name="latitude">Latitude of the client</param>
        /// <param name="londitude">Londitude of the client</param>

        public void AddClient(Client client)
        {

          
            if (DataSource.clients.Any(x=>x.Id==client.Id))
                throw (new ItemFoundException("Client", client.Id));
            DataSource.clients.Add(client);


        }

        /// <summary>
        /// Display client data desired 
        /// </summary>
        /// <param name="id">ID of desired client </param>
        /// <returns> string of data </returns>
        public Client CilentByNumber(uint id)
        {
              if (DataSource.clients.Any(x=>x.Id==id))
             throw (new ItemNotFoundException("client",id));
            return DataSource.clients[DataSource.clients.FindIndex(x => x.Id == id)];

        }


        /// <summary>
        /// Print thr all clients
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values ​​of all the clients so we can print them</param>
        public IEnumerable<Client> cilentList()
        {
            return DataSource.clients.ToList<Client>();
        }

        /// <summary>
        /// delete a spsific client 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteClient(uint id)
        {
            if (!DataSource.clients.Any(x=>x.Id==id))
                throw (new ItemNotFoundException("client", id));

            DataSource.clients.Remove(DataSource.clients[DataSource.clients.FindIndex(x => x.Id == id)]);


        }

        public void UpdateClient(Client client)
        {
            int index = DataSource.clients.FindIndex(x => x.Id == client.Id);
            if (index != -1)
                DataSource.clients[index] = client;
            else
                throw (new IDAL.DO.ItemNotFoundException("client", client.Id));
        }
    }
}
