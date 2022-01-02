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
        Func<ClientToList, bool> activeClient = x => x.Active;
        Func<ClientToList, bool> clientHowSendPackege = client => (client.Arrived > 0 || client.NotArrived > 0);
        Func<ClientToList, bool> clientHowSendPackegeAndThePackegeArrive = client => client.Arrived > 0;
        Func<ClientToList, bool> clientHowSendPackegeAndThePackegeNotArrive = client => client.NotArrived > 0;
        Func<ClientToList, bool> clientHowGetingPackegesAndNotArrive = client => client.OnTheWay > 0;
        Func<ClientToList, bool> clientHowGetingPackegesAndArrive = client => client.received > 0;
        Func<ClientToList, bool> clientActiveHowGetingPackeges = client => client.received > 0 || client.OnTheWay > 0;
        /// <summary>
        /// list of clients activ
        /// </summary>
        /// <returns> list of clients</returns>
        public IEnumerable<ClientToList> ClientActiveToLists(bool filter=true)
        {
            clientToListFilter -= activeClient;
            if(filter)
            clientToListFilter += activeClient;
            return FilterClientList();
        }

        /// <summary>
        /// list of clients activ
        /// </summary>
        /// <returns> list of clients</returns>
        public IEnumerable<ClientToList> ClientToLists()
        {

            return from client in dalObj.CilentList(x => true)
                   select client.convertClientDalToClientToList(dalObj);

        }

        /// <summary>
        /// IEnumerable of client how send packege
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowSendPackegesToLists(bool filter = true)
        {
            clientToListFilter -= clientHowSendPackege;
            clientToListFilter -= clientHowSendPackegeAndThePackegeArrive;
            clientToListFilter -= clientHowSendPackegeAndThePackegeNotArrive;
            if (filter)
                clientToListFilter += clientHowSendPackege;
            return FilterClientList();

        }

        /// <summary>
        /// IEnumerable of client how send packege and arrive
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowSendAndArrivePackegesToLists(bool filter = true)
        {
            clientToListFilter -= clientHowSendPackege;
            clientToListFilter -= clientHowSendPackegeAndThePackegeArrive;
            clientToListFilter -= clientHowSendPackegeAndThePackegeNotArrive;
            if (filter)
                clientToListFilter += clientHowSendPackegeAndThePackegeArrive;
            return FilterClientList();

        }
        /// <summary>
        /// IEnumerable of client how send packege and not arrive
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowSendPackegesAndNotArriveToLists(bool filter = true)
        {
            clientToListFilter -= clientHowSendPackege;
            clientToListFilter -= clientHowSendPackegeAndThePackegeArrive;
            clientToListFilter -= clientHowSendPackegeAndThePackegeNotArrive;
            if (filter)
                clientToListFilter += clientHowSendPackegeAndThePackegeNotArrive;
            return FilterClientList();

        }
        /// <summary>
        /// IEnumerable of client how need to get packege 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowGetingPackegesAndNotArriveToLists(bool filter = true)
        {

            clientToListFilter -= clientHowGetingPackegesAndNotArrive;
            clientToListFilter -= clientHowGetingPackegesAndArrive;
            clientToListFilter -= clientActiveHowGetingPackeges;
            if (filter)
                clientToListFilter += clientHowGetingPackegesAndNotArrive;
            return FilterClientList();
        }
        /// <summary>
        /// IEnumerable of client how need to get packege and they get 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowGetingPackegesAndArriveToLists(bool filter = true)
        {
            clientToListFilter -= clientHowGetingPackegesAndNotArrive;
            clientToListFilter -= clientHowGetingPackegesAndArrive;
            clientToListFilter -= clientActiveHowGetingPackeges;
            if (filter)
                clientToListFilter += clientHowGetingPackegesAndArrive;
            return FilterClientList();

        }
        /// <summary>
        /// IEnumerable of client how need to get packege and they not get 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowGetingPackegesToLists(bool filter = true)
        {
            clientToListFilter -= clientHowGetingPackegesAndNotArrive;
            clientToListFilter -= clientHowGetingPackegesAndArrive;
            clientToListFilter -= clientActiveHowGetingPackeges;
            if (filter)
                clientToListFilter += clientActiveHowGetingPackeges;
            return FilterClientList();

        }

        public IEnumerable<ClientInPackage> ClientInPackagesList(bool filter = true)
        {
            return from client in dalObj.CilentList(x => true)
                   select new ClientInPackage { Id = client.Id, Name = client.Name };
        }

        public IEnumerable<ClientToList> FilterClientList()
        {

            return from client in filerList(ClientToLists(), clientToListFilter)
                   select client.Clone();
        }
    }
}
