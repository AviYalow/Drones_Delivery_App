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
        public static void Add_station(int base_num, string name, int numOfCharging, double latitude, double longitude)
        {

            DataSource.base_Stations[DataSource.Config.index_base_stations_empty] = new Base_Station
            {
                baseNumber = base_num,
                NameBase = name,
                Number_of_charging_stations = numOfCharging,
                latitude = latitude,
                longitude = longitude

            };


            DataSource.Config.index_base_stations_empty++;
        }

        /// <summary>
        /// Adding a new drone
        /// </summary>
        /// <param name="siralNumber">Serial number of the drone</param>
        /// <param name="model">The name of the modek </param>
        /// <param name="category"> Easy / Medium / Heavy</param>
        /// <param name="butrry">Battery status</param>
        /// <param name="statos"> Free/ Maintenance/ Work</param>
        public static void Add_drone(int siralNumber, string model, int category, double butrry, int statos)
        {
            DataSource.drones[DataSource.Config.index_drones_empty] = new Drone() { siralNumber = siralNumber, Model = model, weightCategory = (Weight_categories)category, butrryStatus = butrry, drownStatus = (Drone_status)statos };
            DataSource.Config.index_drones_empty++;
        }


        /// <summary>
        /// Adding a new client
        /// </summary>
        /// <param name="id">ID of the new client</param>
        /// <param name="name">Name of the new cliet</param>
        /// <param name="phone">Phone number</param>
        /// <param name="latitude">Latitude of the client</param>
        /// <param name="londitude">Londitude of the client</param>

        public static void Add_client(int id, string name, string phone, double latitude, double londitude)
        {

            DataSource.clients[DataSource.Config.index_clients_empty] = new Client
            {
                ID = id
            ,
                Name = name,
                PhoneNumber = phone,
                Latitude = latitude,
                Longitude = londitude
            };

            DataSource.Config.index_clients_empty++;
        }

        /// <summary>
        /// Adding a new package
        /// </summary>
        /// <param name="idsend">Sending customer ID</param>
        /// <param name="idget">Receiving customer ID</param>
        /// <param name="kg">Weight categories</param>
        /// <param name="priorityByUser">Priority: Immediate/ quick/ Regular </param>
        /// <returns>Returns the serial number of the created package</returns>
        public static int Add_package(int idsend, int idget, int kg, int priorityByUser)
        {

            DataSource.packages[DataSource.Config.package_num] = new Package
            {
                sirialNumber = DataSource.Config.package_num,
                receiving_delivery = DateTime.Now,
                sendClient = idsend,
                getingClient = idget,
                weightCatgory = (Weight_categories)kg,
                priority = (Priority)priorityByUser
            };

            connect_package_to_drone(DataSource.Config.package_num);
            DataSource.Config.index_Packages_empty++;
            DataSource.Config.package_num++;
            return DataSource.Config.package_num - 1;
        }

        /// <summary>
        /// connect package to drone
        /// </summary>
        /// <param name="packageNumber">serial number of the package that needs 
        /// to connect to drone </param>
        public static void connect_package_to_drone(int packageNumber)
        {


            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {
                if (DataSource.drones[i].weightCategory == DataSource.packages[packageNumber - 1].weightCatgory
                    && DataSource.drones[i].drownStatus == Drone_status.Free)
                {
                    DataSource.packages[packageNumber - 1].operator_skimmer_ID = DataSource.drones[i].siralNumber;
                    DataSource.drones[i].drownStatus = Drone_status.Work;
                    DataSource.packages[packageNumber - 1].package_association = DateTime.Now;
                    return;
                }
                DataSource.packages[DataSource.Config.package_num].operator_skimmer_ID = 0;
            }
            Console.WriteLine("No suitable drone found");
        }

        /// <summary>
        /// Updated package collected
        /// </summary>
        /// <param name="packageNumber">serial number of the package</param>
        public static void Package_collected(int packageNumber)
        {
            DataSource.packages[packageNumber - 1].collect_package_for_shipment = DateTime.Now;
        }

        /// <summary>
        /// Update that package has arrived at destination
        /// </summary>
        /// <param name="packageNumber">serial number of the package</param>
        public static void Package_arrived(int packageNumber)
        {

            DataSource.packages[packageNumber - 1].package_arrived = DateTime.Now;
            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {
                if (DataSource.drones[i].siralNumber == DataSource.packages[packageNumber - 1].operator_skimmer_ID)
                {
                    DataSource.drones[i].drownStatus = Drone_status.Free;
                    DataSource.drones[i].butrryStatus -= 20;
                }
            }
        }


        /// <summary>
        /// sent drone to a free charging station
        /// </summary>
        /// <param name="base_station">Desired base station number</param>
        /// <param name="droneNmber">Desirable drone number</param>
        public static void send_drone_to_charge(int base_station, int droneNmber)
        {


            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {
                if (DataSource.drones[i].siralNumber == droneNmber)
                {
                    if (DataSource.drones[i].drownStatus == Drone_status.Work)
                    {
                        Console.WriteLine("The drone in shipment, please wait until it arrives!");
                        return;
                    }
                    DataSource.drones[i].drownStatus = Drone_status.Maintenance;
                    for (int j = 0; j < DataSource.Config.index_base_stations_empty; j++)
                    {
                        if (DataSource.base_Stations[j].baseNumber == base_station)
                        {
                            DataSource.base_Stations[j].Number_of_charging_stations--;
                            DataSource.drones[i].base_station_latitude = DataSource.base_Stations[j].latitude;
                            DataSource.drones[i].base_station_longitude = DataSource.base_Stations[j].longitude;
                            DataSource.droneInCharge[DataSource.Config.index_butrry_chrge].idBaseStation = DataSource.base_Stations[j].baseNumber;
                            DataSource.droneInCharge[DataSource.Config.index_butrry_chrge].id_drown = DataSource.drones[i].siralNumber;
                            break;
                        }
                    }
                    break;
                }
            }

        }

        /// <summary>
        /// Update of drone release from charging station
        /// </summary>
        /// <param name="sirialNumber">serial number of the drone</</param>
        public static void free_drone_from_charge(int sirialNumber)
        {
            
          
            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {
                if (DataSource.drones[i].siralNumber == sirialNumber)
                {
                    DataSource.drones[i].drownStatus = Drone_status.Free;
                    DataSource.drones[i].butrryStatus = 100;
                    break;
                }
            }
            for (int i = 0; i < DataSource.Config.index_butrry_chrge; i++)
            {
                if (DataSource.droneInCharge[i].id_drown == sirialNumber)
                {
                    DataSource.Config.index_butrry_chrge--;
                    for (int j = i; j < DataSource.Config.index_butrry_chrge; j++)
                    {
                        DataSource.droneInCharge[j] = DataSource.droneInCharge[j + 1];
                    }
                }
            }
        }

        /// <summary>
        /// Display base station data desired   
        /// </summary>
        /// <param name="baseNum">Desired base station number</param>
        /// <returns> String of data </returns>
        public static string Base_station_by_number(int baseNum)
        {
            
           
            for (int i = 0; i < DataSource.Config.index_base_stations_empty; i++)
            {
                if (DataSource.base_Stations[i].baseNumber == baseNum)
                {
                    return DataSource.base_Stations[i].ToString();

                }

            }
            return "base station not exitst";
        }

        /// <summary>
        /// Display drone data desired   
        /// </summary>
        /// <param name="droneNum">Desired drone number</param>
        /// <returns> String of data</returns>
        public static string Drone_by_number(int droneNum)
        {
          
            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {
                if (DataSource.drones[i].siralNumber == droneNum)
                {
                    return DataSource.drones[i].ToString();
                    
                }
            }
            return "drone not found!";
        }

        /// <summary>
        /// Display client data desired 
        /// </summary>
        /// <param name="id">ID of desired client </param>
        /// <returns> string of data </returns>
        public static string cilent_by_number(int id)
        {
           
            for (int i = 0; i < DataSource.Config.index_clients_empty; i++)
            {
                if (DataSource.clients[i].ID == id)
                {
                    return(DataSource.clients[i].ToString());
                    
                }
            }
            return "client not found!";
        }

        /// <summary>
        /// Display packege data desired
        /// </summary>
        /// <param name="packageNumbe">Serial number of a particular package</param>
        /// <returns> string of data</returns>
        public static string packege_by_number(int packageNumbe)
        {

           return(DataSource.packages[packageNumbe - 1].ToString());
        }

        /// <summary>
        /// Print all the base stations
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values ​​of all the base stations so we can print them</param>
        public static void Base_station_list(ArrayList array)
        {

            for (int i = 0; i < DataSource.Config.index_base_stations_empty; i++)
            { 
                array.Add(DataSource.base_Stations[i].ToString());

            }
        }

        /// <summary>
        /// Print all the drones
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values ​​of all the drones so we can print them</param>
        public static void Drone_list(ArrayList array)
        {

            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {
                array.Add(DataSource.drones[i].ToString());

            }
        }


        /// <summary>
        /// Print thr all clients
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values ​​of all the clients so we can print them</param>
        public static void cilent_list(ArrayList array)
        {

            for (int i = 0; i < DataSource.Config.index_clients_empty; i++)
            {

                array.Add(DataSource.clients[i].ToString());

            }
        }

        /// <summary>
        /// Print all the packages
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values ​​of all the packages so we can print them</param>
        public static void packege_list(ArrayList array)
        {

            for (int i = 0; i < DataSource.Config.package_num - 1; i++)
            {

                array.Add(DataSource.packages[i].ToString());

            }

        }

        /// <summary>
        /// Displays a list of packages that
        /// have not been assigned to a drone yet 
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values so we can print them</param>
        public static void packege_list_with_no_drone(ArrayList array)
        {
            for (int i = 0; i < DataSource.Config.package_num; i++)
            {
                
                if (DataSource.packages[i].operator_skimmer_ID == 0)
                    array.Add(DataSource.packages[i].ToString());

            }
        }

        /// <summary>
        /// Display of base stations with available charging stations
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values so we can print them</param>
        public static void Base_station_list_with_free_charge_states(ArrayList array)
        {
            

            for (int i = 0; i < DataSource.Config.index_base_stations_empty; i++)
            {
                if (DataSource.base_Stations[i].Number_of_charging_stations > 0)
                {
                    array.Add(DataSource.base_Stations[i]);
                    
                }
            }
            
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


        public static string Distance(double Longitude1, double Latitude1, double Longitude2, double Latitude2)
        {
            return ($"the distance is: {Point.Distance(Longitude1, Latitude1, Longitude2, Latitude2)}KM");
        }
    }
}
