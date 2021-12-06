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
        /// list of clients activ
        /// </summary>
        /// <returns> list of clients</returns>
        public IEnumerable<ClientToList> ClientActiveToLists()
        {
            List<ClientToList> clientToLists = new List<ClientToList>();
            foreach (var clientInDal in dalObj.cilentList().Where(x => x.Active))
            {
                clientToLists.Add(new ClientToList
                {
                    ID = clientInDal.Id,
                    Name = clientInDal.Name,
                    Phone = clientInDal.PhoneNumber,
                    Arrived = (uint)dalObj.PackegeList(x => x.SendClient == clientInDal.Id && x.PackageArrived != null).Count(),
                    NotArrived = (uint)dalObj.PackegeList(x => x.SendClient == clientInDal.Id && x.PackageArrived == null).Count(),
                    OnTheWay = (uint)dalObj.PackegeList(x => x.GetingClient == clientInDal.Id && x.PackageArrived == null).Count(),
                    received = (uint)dalObj.PackegeList(x => x.GetingClient == clientInDal.Id && x.PackageArrived != null).Count()
                });

            }
           
            return clientToLists;
        }

        /// <summary>
        /// list of clients activ
        /// </summary>
        /// <returns> list of clients</returns>
        public IEnumerable<ClientToList> ClientToLists()
        {
            List<ClientToList> clientToLists = new List<ClientToList>();
            foreach (var clientInDal in dalObj.cilentList().Where(x => true))
            {
                clientToLists.Add(new ClientToList
                {
                    ID = clientInDal.Id,
                    Name = clientInDal.Name,
                    Phone = clientInDal.PhoneNumber,
                    Arrived = (uint)dalObj.PackegeList(x => x.SendClient == clientInDal.Id && x.PackageArrived != null).Count(),
                    NotArrived = (uint)dalObj.PackegeList(x => x.SendClient == clientInDal.Id && x.PackageArrived == null).Count(),
                    OnTheWay = (uint)dalObj.PackegeList(x => x.GetingClient == clientInDal.Id && x.PackageArrived == null).Count(),
                    received = (uint)dalObj.PackegeList(x => x.GetingClient == clientInDal.Id && x.PackageArrived != null).Count()
                });

            }
            
            return clientToLists;
        }

        /// <summary>
        /// IEnumerable of client how send packege
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowSendPackegesToLists()
        {
         
            return ClientActiveToLists().Where(x=>x.Arrived>0||x.NotArrived>0);
        }

        /// <summary>
        /// IEnumerable of client how send packege and arrive
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowSendAndArrivePackegesToLists()
        {
         
            return ClientActiveToLists().Where(x => x.Arrived > 0 );
        }
        /// <summary>
        /// IEnumerable of client how send packege and not arrive
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowSendPackegesAndNotArriveToLists()
        {

            return ClientActiveToLists().Where(x =>  x.NotArrived > 0);
        }
        /// <summary>
        /// IEnumerable of client how need to get packege 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowGetingPackegesAndNotArriveToLists()
        {

            return ClientActiveToLists().Where(x => x.OnTheWay > 0);
        }
        /// <summary>
        /// IEnumerable of client how need to get packege and they get 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowGetingPackegesAndArriveToLists()
        {

            return ClientActiveToLists().Where(x => x.received > 0);
        }
        /// <summary>
        /// IEnumerable of client how need to get packege and they not get 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientToList> ClientActiveHowGetingPackegesToLists()
        {

            return ClientActiveToLists().Where(x => x.received > 0||x.OnTheWay>0);
        }
    }
}
