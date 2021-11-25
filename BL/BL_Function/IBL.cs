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


     //   double DroneChrgingAlredy(TimeSpan timeSpan);
        public void DroneToCharge(uint dronenumber);
        /// <summary>
        /// add drone to list
        /// </summary>
        /// <param name="drone"> drone to add</param>
        /// <param name="base_"> serial number of base station for first chraging</param>
        public void AddDrone(DroneToList drone, uint base_);
        /// <summary>
        /// update new location for drone
        /// </summary>
        /// <param name="drone"> serial number of drone</param>
        /// <param name="location"> new location</param>
        public void UpdateDronelocation(uint drone, Location location);
        /// <summary>
        /// update new model for drone
        /// </summary>
        /// <param name="droneId"> serial number of the drone</param>
        /// <param name="newName"> new name to change</param>
        public void UpdateDroneName(uint droneId, string newName);
        /// <summary>
        /// find specific drone in the list of the drones
        /// </summary>
        /// <param name="siralNuber"> serial number of the drone</param>
        /// <returns> drone founded </returns>
        public DroneToList SpecificDrone(uint siralNuber);
       /// <summary>
        /// return list of drones
        /// </summary>
        /// <returns> return list of drones</returns>
        public IEnumerable<DroneToList> DroneToLists();

        /// <summary>
        /// search a drone by serial number
        /// </summary>
        /// <param name="droneNum"> serial number of the drone</param>
        /// <returns> drone founded</returns>
        public Drone GetDrone(uint droneNum);


        /// <summary>
        /// delete drone 
        /// </summary>
        /// <param name="droneNum"> serial number of the drone</param>
        public void DeleteDrone(uint droneNum);

        /// <summary>
        ///Charging drone function 
        /// </summary>
        /// <param name="droneNumber">serial number of the drone</param>
        /// <param name="timeInCharge"> the time that the drone in charge </param>
        /// <returns> butrry Status of the  drone</returns>
        public double FreeDroneFromCharging(uint droneNumber, TimeSpan timeInCharge);

        /// <summary>
        /// Release a drone from a charger at a particular base station
        /// </summary>
        /// <param name="baseNumber"> serial number of the base station</param>
        /// <param name="number"> amount of drone to release</param>
        public void FreeBaseFromDrone(uint baseNumber, int number);



        //Base function

        /// <summary>
        /// calculation the most collset base station to a particular location
        /// </summary>
        /// <param name="location"> particular location</param>
        /// <returns> the most collset base station </returns>
        public BaseStation ClosestBase(Location location);

        /// <summary>
        /// geting location for specific base station
        /// </summary>
        /// <param name="base_number"> serial number of base station</param>
        /// <returns> Location of the base station</returns>
        public Location BaseLocation(uint base_number);

        /// <summary>
        /// add base station
        /// </summary>
        /// <param name="baseStation"> serial number of the base station</param>
        public void AddBase(BaseStation baseStation);
        /// <summary>
        /// update base station
        /// </summary>
        /// <param name="base_">erial number of the base station</param>
        /// <param name="newName"> new name</param>
        /// <param name="newNumber"> charging states</param>
        public void UpdateBase(uint base_, string newName , string newNumber );
        /// <summary>
        /// List of base staions with free states
        /// </summary>
        /// <returns> List of base staions with free states</returns>

        public IEnumerable<BaseStationToList> BaseStationWhitChargeStationsToLists();
        /// <summary>
        /// show base station list 
        /// </summary>
        /// <returns> base station list </returns>
        public IEnumerable<BaseStationToList> BaseStationToLists();
        
        /// <summary>
        /// search a specific station
        /// </summary>
        /// <param name="baseNume"> serial number</param>
        /// <returns> base station </returns>
        public BaseStation BaseByNumber(uint baseNume);

        /// <summary>
        /// delete base station
        /// </summary>
        /// <param name="base_"> serial number</param>
        public void DeleteBase(uint base_);



        //Client function


        /// <summary>
        /// add client
        /// </summary>
        /// <param name="client"> client to add</param>
        public void AddClient(Client client);
        /// <summary>
        /// Update fields at a client
        /// </summary>
        /// <param name="client"> client </param>
        public void UpdateClient(ref Client client);
        /// <summary>
        /// return the client location
        /// </summary>
        /// <param name="id"> id client</param>
        /// <returns> client location</returns>
        public Location ClientLocation(uint id);

        /// <summary>
        /// list of clients
        /// </summary>
        /// <returns> list of clients</returns>
        public IEnumerable<ClientToList> ClientToLists();
        /// <summary>
        /// Receiving a client by ID
        /// </summary>
        /// <param name="id"> client ID</param>
        /// <returns> client</returns>
        public Client GetingClient(uint id);

        /// <summary>
        /// delete client
        /// </summary>
        /// <param name="id"> client id</param>
        public void DeleteClient(uint id);
        
        
        //Packege function
        public uint AddPackege(Package package);
        public void ConnectPackegeToDrone(uint droneNumber);
        public IEnumerable<PackageToList> PackageWithNoDroneToLists();
        public IEnumerable<PackageToList> PackageToLists();
        public void PackegArrive(uint droneNumber);
        public void CollectPackegForDelivery(uint droneNumber);
        public Package ShowPackage(uint number);
        public void UpdatePackegInDal(Package package);
        /// <summary>
        /// delete packege 
        /// </summary>
        /// <param name="number"></param>
        public void DeletePackege(uint number);

        //Function
        public double Distans(Location location1, Location location2);
        public string PointToDegree(double point);


















    }
}
