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
        /// list of clients
        /// </summary>
        /// <returns> list of clients</returns>
        public IEnumerable<ClientToList> ClientToLists(Predicate<ClientToList> predicate)
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
            clientToLists = clientToLists.FindAll(predicate);
            return clientToLists;
        }
    }
}
