using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;
using IBL.BO;

namespace IBL
{
    public interface IBL
    {

        //Drone function
        double DroneChrgingAlredy(DateTime dateTime, DateTime newdateTime = default);
        public void DroneToCharge(uint dronenumber);
        public void AddDrone(DroneToList drone, uint base_);
        public void UpdateDronelocation(uint drone, Location location);
        public void UpdateDroneName(uint droneId, string newName);
        public DroneToList SpecificDrone(uint siralNuber);
        public IEnumerable<DroneToList> DroneToLists();
        public Drone GetDrone(uint droneNum);

        //Charging drone function 
        public double FreeDroneFromCharging(uint droneNumber, TimeSpan timeInCharge);
        public void FreeBaseFromDrone(uint baseNumber, int number);

        //Base function
        public BaseStation ClosestBase(Location location);
        public Location BaseLocation(uint base_number);
        public void AddBase(BaseStation baseStation);
        public void UpdateBase(uint base_, string newName , string newNumber );
        public IEnumerable<BaseStationToList> BaseStationWhitChargeStationsToLists();
        public IEnumerable<BaseStationToList> BaseStationToLists();
        public BaseStation BaseByNumber(uint baseNume);

        //Client function
        public void AddClient(Client client);
        public void UpdateClient(ref Client client);
        public Location ClientLocation(uint id);
        public IEnumerable<ClientToList> ClientToLists();
        public Client GetingClient(uint id);

        //Packege function
        public uint AddPackege(Package package);
        public void ConnectPackegeToDrone(uint droneNumber);
        public IEnumerable<PackageToList> PackageWithNoDroneToLists();
        public IEnumerable<PackageToList> PackageToLists();
        public void PackegArrive(uint droneNumber);
        public void CollectPackegForDelivery(uint droneNumber);
        public Package ShowPackage(uint number);
        public void UpdatePackegInDal(Package package);

        //Function
        public double Distans(Location location1, Location location2);
        public string PointToDegree(double point);


















    }
}
