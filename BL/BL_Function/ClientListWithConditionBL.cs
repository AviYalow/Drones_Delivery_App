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
        /// list of clients activ
        /// </summary>
        /// <returns> list of clients</returns>
        public IEnumerable<ClientToList> ClientActiveToLists()
        {
            return from clientInDal in dalObj.CilentList(x => x.Active)
                   select clientInDal.convertClientDalToClientToList(dalObj);
        }

        /// <summary>
        /// list of clients activ
        /// </summary>
        /// <returns> list of clients</returns>
        public IEnumerable<ClientToList> ClientToLists()
        {
            return from clientInDal in dalObj.CilentList(x => true)
                   select clientInDal.convertClientDalToClientToList(dalObj);
        }

        /// <summary>
        /// IEnumerable of client how send packege
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowSendPackegesToLists()
        {

            return from client in ClientActiveToLists()
                   where client.Arrived > 0 || client.NotArrived > 0
                   select client.Clone();

        }

        /// <summary>
        /// IEnumerable of client how send packege and arrive
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowSendAndArrivePackegesToLists()
        {
            return from client in ClientActiveToLists()
                   where client.Arrived > 0
                   select client.Clone();

        }
        /// <summary>
        /// IEnumerable of client how send packege and not arrive
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowSendPackegesAndNotArriveToLists()
        {
            return from client in ClientActiveToLists()
                   where client.NotArrived > 0
                   select client.Clone();

        }
        /// <summary>
        /// IEnumerable of client how need to get packege 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowGetingPackegesAndNotArriveToLists()
        {
            return from client in ClientActiveToLists()
                   where client.OnTheWay > 0
                   select client.Clone();

        }
        /// <summary>
        /// IEnumerable of client how need to get packege and they get 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowGetingPackegesAndArriveToLists()
        {
            return from client in ClientActiveToLists()
                   where client.received > 0
                   select client.Clone();

        }
        /// <summary>
        /// IEnumerable of client how need to get packege and they not get 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowGetingPackegesToLists()
        {
            return from client in ClientActiveToLists()
                   where client.received > 0 || client.OnTheWay > 0
                   select client.Clone();

        }

        public IEnumerable<ClientInPackage> ClientInPackagesList()
        {
            return from client in dalObj.CilentList(x => true)
                   select new ClientInPackage { Id = client.Id, Name = client.Name };
        }
    }
}
