using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL;
using IDAL.DO;
namespace DalObject
{

    public class DalObject
    {

        /// <summary>
        ///Creating entities with initial initialization
        /// </summary>
        public DalObject()
        {
            DataSource.Initialize();
        }

        /// <summary>
        ///Adding a new base station
        /// </summary>
        /// <param name="base_num">The base station number </param>
        /// <param name="name"> The name ot the station </param>
        /// <param name="numOfCharging">The amount of charging stations at the station </param>
        /// <param name="latitude">Latitude of the station</param>
        /// <param name="longitude">Longitude of the station</param>
        public void Add_station(int base_num, string name, int numOfCharging, double latitude, double longitude)
        {



            DataSource.base_Stations.Add(new Base_Station
            {
                baseNumber = base_num,
                NameBase = name,
                Number_of_charging_stations = numOfCharging,
                latitude = latitude,
                longitude = longitude

            });



        }

        /// <summary>
        /// Adding a new drone
        /// </summary>
        /// <param name="siralNumber">Serial number of the drone</param>
        /// <param name="model">The name of the modek </param>
        /// <param name="category"> Easy / Medium / Heavy</param>
        /// <param name="butrry">Battery status</param>
        /// <param name="statos"> Free/ Maintenance/ Work</param>
        public void Add_drone(int siralNumber, string model, int category, double butrry, int statos)
        {
            DataSource.drones.Add(new Drone()
            {
                serialNumber = siralNumber,
                Model = model,
                weightCategory = (Weight_categories)category
            });

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
        /// Adding a new package
        /// </summary>
        /// <param name="idsend">Sending customer ID</param>
        /// <param name="idget">Receiving customer ID</param>
        /// <param name="kg">Weight categories</param>
        /// <param name="priorityByUser">Priority: Immediate/ quick/ Regular </param>
        /// <returns>Returns the serial number of the created package</returns>
        public int Add_package(int idsend, int idget, int kg, int priorityByUser)
        {

            DataSource.Config.package_num++;
            Package package = new Package
            {
                serialNumber = DataSource.Config.package_num,
                receiving_delivery = DateTime.Now,
                sendClient = idsend,
                getingClient = idget,
                weightCatgory = (Weight_categories)kg,
                priority = (Priority)priorityByUser
            };

            //  package.operator_skimmer_ID = found_drone_for_package(DataSource.packages[DataSource.Config.package_num].weightCatgory);

            DataSource.packages.Add(package);
            return DataSource.Config.package_num;
        }

        /// <summary>
        /// connect package to drone
        /// </summary>
        /// <param name="packageNumber">serial number of the package that needs 
        /// to connect to drone </param>
        public void connect_package_to_drone(int packageNumber, int drone_sirial_number)
        {
            int i = 0;
            for (; i < DataSource.packages.Count(); i++)
            {
                if (DataSource.packages[i].serialNumber == packageNumber)
                    break;
            }
            Package package = DataSource.packages[i];
            package.operator_skimmer_ID = drone_sirial_number;

            package.package_association = DateTime.Now;
            DataSource.packages[i] = new Package
            {
                serialNumber = package.serialNumber,
                sendClient = package.sendClient,
                getingClient = package.getingClient,
                operator_skimmer_ID = package.operator_skimmer_ID,
                weightCatgory = package.weightCatgory,
                package_arrived = package.package_arrived,
                priority = package.priority,
                receiving_delivery = package.receiving_delivery,
                collect_package_for_shipment = package.collect_package_for_shipment,
                package_association = package.package_association

            };



        }
        /// <summary>
        /// loking for free drone
        /// </summary>
        /// <param name="weight_"></param>
        /// <returns></returns>
        /*  public static int found_drone_for_package(Weight_categories weight_)
          {
              for (int i = 0; i < DataSource.drones.Count(); i++)
              {

                  if (DataSource.drones[i].weightCategory == weight_)                    )
                  {

                      DataSource.drones[i].drownStatus = Drone_status.Work;
                      return DataSource.drones[i].serialNumber;
                  }

              }
              return 0;
          }*/

        /// <summary>
        /// Updated package collected
        /// </summary>
        /// <param name="packageNumber">serial number of the package</param>
        public void Package_collected(int packageNumber)
        {
            int i = 0;
            for (; i < DataSource.packages.Count(); i++)
            {
                if (DataSource.packages[i].serialNumber == packageNumber)
                    break;
            }
            Package package = DataSource.packages[i];
            package.collect_package_for_shipment = DateTime.Now;
            DataSource.packages[i] = new Package
            {
                serialNumber = package.serialNumber,
                sendClient = package.sendClient,
                getingClient = package.getingClient,
                operator_skimmer_ID = package.operator_skimmer_ID,
                weightCatgory = package.weightCatgory,
                package_arrived = package.package_arrived,
                priority = package.priority,
                receiving_delivery = package.receiving_delivery,
                collect_package_for_shipment = package.collect_package_for_shipment,
                package_association = package.package_association

            };
        }

        /// <summary>
        /// Update that package has arrived at destination
        /// </summary>
        /// <param name="packageNumber">serial number of the package</param>
        public void Package_arrived(int packageNumber)
        {
            int i = 0;
            for (; i < DataSource.packages.Count(); i++)
            {
                if (DataSource.packages[i].serialNumber == packageNumber)
                {
                    break;
                }

            }
            Package package = DataSource.packages[i];
            package.package_arrived = DateTime.Now;
            DataSource.packages[i] = new Package
            {
                serialNumber = package.serialNumber,
                sendClient = package.sendClient,
                getingClient = package.getingClient,
                operator_skimmer_ID = package.operator_skimmer_ID,
                weightCatgory = package.weightCatgory,
                package_arrived = package.package_arrived,
                priority = package.priority,
                receiving_delivery = package.receiving_delivery,
                collect_package_for_shipment = package.collect_package_for_shipment,
                package_association = package.package_association

            };
            // drone_after_work(DataSource.packages[packageNumber - 1].operator_skimmer_ID);
        }
        /// <summary>
        /// free drone to by free to take other job
        /// </summary>
        /// <param name="sirialNum"></param>
        /*public   void drone_after_work(int sirialNum)
        {
            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {
                if (DataSource.drones[i].serialNumber == sirialNum)
                {
                    DataSource.drones[i].drownStatus = Drone_status.Free;
                    DataSource.drones[i].butrryStatus -= 20;
                }
            }
        }*/


        /// <summary>
        /// sent drone to a free charging station
        /// </summary>
        /// <param name="base_station">Desired base station number</param>
        /// <param name="droneNmber">Desirable drone number</param>
        /*   public   void send_drone_to_charge(int base_station, int droneNmber)
           {
               for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
               {
                   if (DataSource.drones[i].serialNumber == droneNmber)
                   {

                       DataSource.drones[i].drownStatus = Drone_status.Maintenance;
                       for (int j = 0; j < DataSource.Config.index_base_stations_empty; j++)
                       {
                           if (DataSource.base_Stations[j].baseNumber == base_station)
                           {
                               update_charge_station_in_base(base_station, -1);
                               update_drone_charge_in_base(DataSource.base_Stations[j].baseNumber, Drone_in_charge.Add, DataSource.drones[i].serialNumber);
                               break;
                           }
                       }
                       break;
                   }
               }




           }*/
        /// <summary>
        /// Update of drone release from charging station
        /// </summary>
        /// <param name="base_station"></param>
        /// <param name="dronSirialNum"></param>
        /*    public   void update_drone_charge_in_base( int dronSirialNum,Drone_in_charge chose, int base_station=0)
            {
                switch (chose)
                {
                    case Drone_in_charge.Add:
                        DataSource.droneInCharge[DataSource.Config.index_battery_charge].idBaseStation = base_station;
                        DataSource.droneInCharge[DataSource.Config.index_battery_charge].id_drone = dronSirialNum;
                        break;
                    case Drone_in_charge.Delete:
                        for (int i = 0; i < DataSource.Config.index_battery_charge; i++)
                        {
                            if (DataSource.droneInCharge[i].id_drone == dronSirialNum)
                            {
                                DataSource.Config.index_battery_charge--;
                                for (int j = i; j < DataSource.Config.index_battery_charge; j++)
                                {
                                    DataSource.droneInCharge[j] = DataSource.droneInCharge[j + 1];
                                }
                                break;
                            }
                        }
                        break;
                    default:
                        break;
                }

            }*/
        /// <summary>
        /// update charge station in base
        /// </summary>
        /// <param name="base_station"></param>
        /// <param name="newChargeStationFree"></param>
        /*     public   void update_charge_station_in_base(int base_station, int newChargeStationFree)
             {
                 for (int i = 0; i < DataSource.Config.index_base_stations_empty; i++)
                 {
                     if (DataSource.base_Stations[i].baseNumber == base_station)
                         DataSource.base_Stations[i].Number_of_charging_stations += newChargeStationFree;
                 }

             }
        */
        /*   public   void free_drone_from_charge(int sirialNumber)
           {


               for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
               {
                   if (DataSource.drones[i].serialNumber == sirialNumber)
                   {
                       DataSource.drones[i].drownStatus = Drone_status.Free;
                       DataSource.drones[i].butrryStatus = 100;
                       update_charge_station_in_base(DataSource.drones[i].base_station, 1);
                       update_drone_charge_in_base(DataSource.drones[i].serialNumber,Drone_in_charge.Delete);
                       break;
                   }
               }



           }*/

        /// <summary>
        /// Display base station data desired   
        /// </summary>
        /// <param name="baseNum">Desired base station number</param>
        /// <returns> String of data </returns>
        public Base_Station Base_station_by_number(int baseNum)
        {


            foreach (Base_Station number in DataSource.base_Stations)
            {
                if (number.baseNumber == baseNum)
                {
                    return number;

                }

            }
            return DataSource.base_Stations[0];
        }

        /// <summary>
        /// Display drone data desired   
        /// </summary>
        /// <param name="droneNum">Desired drone number</param>
        /// <returns> String of data</returns>
        public Drone Drone_by_number(int droneNum)
        {

            foreach (Drone item in DataSource.drones)
            {
                if (item.serialNumber == droneNum)
                {
                    return item;

                }
            }
            return DataSource.drones[0];
        }

        /// <summary>
        /// Display client data desired 
        /// </summary>
        /// <param name="id">ID of desired client </param>
        /// <returns> string of data </returns>
        public Client cilent_by_number(int id)
        {

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
        /// Display packege data desired
        /// </summary>
        /// <param name="packageNumbe">Serial number of a particular package</param>
        /// <returns> string of data</returns>
        public Package packege_by_number(int packageNumbe)
        {
            foreach (Package package in DataSource.packages)
                if (package.serialNumber == packageNumbe)
                    return (package);
            return DataSource.packages[0];

        }

        /// <summary>
        /// Print all the base stations
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values ​​of all the base stations so we can print them</param>
        public IEnumerable<Base_Station> Base_station_list()
        {
            foreach(Base_Station base_ in DataSource.base_Stations)
            yield return base_;

        }

        /// <summary>
        /// Print all the drones
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values ​​of all the drones so we can print them</param>
        public IEnumerable<Drone> Drone_list()
        {
            foreach(Drone drone in DataSource.drones)
            yield return drone;
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
        /// Print all the packages
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values ​​of all the packages so we can print them</param>
        public IEnumerable<Package> packege_list()
        {
            foreach (Package item in DataSource.packages)
                yield return item;

        }

        /// <summary>
        /// Displays a list of packages that
        /// have not been assigned to a drone yet 
        /// </summary>

        public IEnumerable<Package> packege_list_with_no_drone()
        {

            foreach (Package item in DataSource.packages)
            {
                if (item.operator_skimmer_ID == 0)
                    yield return item;
            }

            

        }

        /// <summary>
        /// Display of base stations with available charging stations
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values so we can print them</param>
        public IEnumerable<Base_Station> Base_station_list_with_free_charge_states()
        {

            
            foreach (Base_Station item in DataSource.base_Stations)
                if (item.Number_of_charging_stations > 0)
                    yield return item;




        }

        /// <summary>  
        /// The function takes two-point coordinates and
        /// calculates the distance between them
        /// </summary>
        /// <param name="Longitude1">Longitude of the first point </param>
        /// <param name="Latitude1">Latitude of the first point </param>
        /// <param name="Longitude2">Longitude of the second point</param>
        /// <param name="Latitude2">Latitude of the second point</param>
        /// <returns>The distance between the two points at sexagesimal base</returns>


        public string Distance(double Longitude1, double Latitude1, double Longitude2, double Latitude2)
        {
            return ($"the distance is: {Point.Distance(Longitude1, Latitude1, Longitude2, Latitude2)}KM");
        }
        /// <summary>
        /// Returns a point in the form of degrees
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public string Point_to_degree(double point)
        {
            return Point.Degree(point);
        }


        /// <summary>
        /// delete a spsific base for list
        /// </summary>
        /// <param name="sirial"></param>
        public void DeleteBase(int sirial)
        {
            for (int i = 0; i < DataSource.base_Stations.Count(); i++)
            {
                if(DataSource.base_Stations[i].baseNumber==sirial)
                {
                    DataSource.base_Stations.Remove(DataSource.base_Stations[i]);
                    return;
                }
            }
        }
        /// <summary>
        /// delete a spsific client 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteClient(int id)
        {
            for (int i = 0; i < DataSource.clients.Count(); i++)
            {
                if (DataSource.clients[i].ID == id)
                {
                    DataSource.clients.Remove(DataSource.clients[i]);
                    return;
                }
            }
        }
        /// <summary>
        /// delete a spsific drone
        /// </summary>
        /// <param name="sirial"></param>
        public void DeleteDrone(int sirial)
        {
            for (int i = 0; i < DataSource.drones.Count(); i++)
            {
                if (DataSource.drones[i].serialNumber == sirial)
                {
                    DataSource.drones.Remove(DataSource.drones[i]);
                    return;
                }
            }
        }
        /// <summary>
        /// delete a spsific packege
        /// </summary>
        /// <param name="sirial"></param>
        public void Deletepackege(int sirial)
        {
            for (int i = 0; i < DataSource.packages.Count(); i++)
            {
                if (DataSource.packages[i].serialNumber == sirial)
                {
                    DataSource.packages.Remove(DataSource.packages[i]);
                    return;
                }
            }
        }
    }
}
