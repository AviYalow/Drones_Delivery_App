using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
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
            catch (IDAL.DO.ItemNotFoundException ex)
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
            if (client.Id < 100000000)
            { throw new NumberNotEnoughException(9); }
            if (client.Id > 999999999)
            { throw new NumberMoreException(); }
            var distas = Distans(client.Location, ClosestBase(client.Location).location);
            if (distas > 30)
                throw new ClientOutOfRangeException();
            //chcing phon number
            chekingFon(client.Phone);

            try
            {
                dalObj.AddClient(new IDAL.DO.Client
                {
                    Id = client.Id,
                    Latitude = client.Location.Latitude,
                    Longitude = client.Location.Longitude,
                    Name = client.Name,
                    PhoneNumber = client.Phone
                });
            }
            catch (IDAL.DO.ItemFoundException ex)
            {
                throw (new ItemFoundExeption(ex));
            }
        }
        /// <summary>
        /// help mathod to chack phonumber
        /// </summary>
        /// <param name="fon"></param>
        void chekingFon(string fon)
        {
            if (fon.Count() < 10)
            { throw new NumberNotEnoughException(10); }
            if (fon[0] != '0' || fon[1] != '5')
            { throw new StartingException("0,5"); }
            if (fon.Any(c => c < '0' || c > '9'))
            { throw new IllegalDigitsException(); }
            fon = fon.Insert(3, "-");
            fon = fon.Insert(7, "-");

        }
        public void UpdateClient(ref Client client)
        {
            //chcinkg id
            if (client.Id < 100000000)
            { throw new NumberNotEnoughException(9); }
            if (client.Id > 999999999)
            { throw new NumberMoreException(); }
            //chcing phon number
            chekingFon(client.Phone);
            dalObj.UpdateClient(new IDAL.DO.Client { Id = client.Id, Name = client.Name, PhoneNumber = client.Phone, Latitude = client.Location.Latitude, Longitude = client.Location.Longitude });
        }
        public Client GetingClient(uint id)
        {
            try
            {
                var client = dalObj.CilentByNumber(id);
                var returnClient = new Client { Id = client.Id, Name = client.Name, Phone = client.PhoneNumber };
                returnClient.ToClient = new List<PackageAtClient>();
                var packege = dalObj.PackegeList().ToList().FindAll(x => x.SendClient == id);
                foreach (var packegeInList in packege)
                {
                    returnClient.ToClient.Add(convretPackegeDalToPackegeAtClient(packegeInList));
                }
                returnClient.FromClient = new List<PackageAtClient>();
                 packege = dalObj.PackegeList().ToList().FindAll(x => x.GetingClient == id);
                foreach (var packegeInList in packege)
                {
                    returnClient.ToClient.Add(convretPackegeDalToPackegeAtClient(packegeInList));
                }
                return returnClient;
            }
            catch (IDAL.DO.ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }


        }
        public IEnumerable<ClientToList> ClientToLists()
        {
            List<ClientToList> clientToLists = new List<ClientToList>();
            foreach (var clientInDal in dalObj.cilentList())
            {
                clientToLists.Add(new ClientToList
                {
                    ID = clientInDal.Id,
                    Name = clientInDal.Name,
                    Phone = clientInDal.PhoneNumber,
                    Arrived = (uint)dalObj.PackagesArriveList().Count(x => x.SendClient == clientInDal.Id && x.PackageArrived != new DateTime()),
                    NotArrived = (uint)dalObj.PackagesArriveList().Count(x => x.SendClient == clientInDal.Id && x.PackageArrived == new DateTime() && x.CollectPackageForShipment != new DateTime()),
                    OnTheWay = (uint)dalObj.PackagesArriveList().Count(x => x.GetingClient == clientInDal.Id && x.PackageArrived == new DateTime() && x.CollectPackageForShipment != new DateTime()),
                    received = (uint)dalObj.PackagesArriveList().Count(x => x.GetingClient == clientInDal.Id && x.PackageArrived != new DateTime())
                });

            }
            return clientToLists;
        }



    }
}
