using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL;
using IDAL.DO;

namespace DAL
{
    namespace IDAL
    {
       interface  IDal
        {


            /// <summary>
            ///Adding a new base station
            /// </summary>
            /// <param name="base_num">The base station number </param>
            /// <param name="name"> The name ot the station </param>
            /// <param name="numOfCharging">The amount of charging stations at the station </param>
            /// <param name="latitude">Latitude of the station</param>
            /// <param name="longitude">Longitude of the station</param>
             void Add_station(int base_num, string name, int numOfCharging, double latitude, double longitude);


            /// <summary>
            /// Adding a new drone
            /// </summary>
            /// <param name="siralNumber">Serial number of the drone</param>
            /// <param name="model">The name of the modek </param>
            /// <param name="category"> Easy / Medium / Heavy</param>
            /// <param name="butrry">Battery status</param>
            /// <param name="statos"> Free/ Maintenance/ Work</param>
            void Add_drone(int siralNumber, string model, int category, double butrry, int statos);



            /// <summary>
            /// Adding a new client
            /// </summary>
            /// <param name="id">ID of the new client</param>
            /// <param name="name">Name of the new cliet</param>
            /// <param name="phone">Phone number</param>
            /// <param name="latitude">Latitude of the client</param>
            /// <param name="londitude">Londitude of the client</param>

            void Add_client(int id, string name, string phone, double latitude, double londitude);


            /// <summary>
            /// Adding a new package
            /// </summary>
            /// <param name="idsend">Sending customer ID</param>
            /// <param name="idget">Receiving customer ID</param>
            /// <param name="kg">Weight categories</param>
            /// <param name="priorityByUser">Priority: Immediate/ quick/ Regular </param>
            /// <returns>Returns the serial number of the created package</returns>
            int Add_package(int idsend, int idget, int kg, int priorityByUser);


            /// <summary>
            /// connect package to drone
            /// </summary>
            /// <param name="packageNumber">serial number of the package that needs 
            /// to connect to drone </param>
            void connect_package_to_drone(int packageNumber, int drone_sirial_number);

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
            void Package_collected(int packageNumber);


            /// <summary>
            /// Update that package has arrived at destination
            /// </summary>
            /// <param name="packageNumber">serial number of the package</param>
            void Package_arrived(int packageNumber);

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
            Base_Station Base_station_by_number(int baseNum);


            /// <summary>
            /// Display drone data desired   
            /// </summary>
            /// <param name="droneNum">Desired drone number</param>
            /// <returns> String of data</returns>
            Drone Drone_by_number(int droneNum);


            /// <summary>
            /// Display client data desired 
            /// </summary>
            /// <param name="id">ID of desired client </param>
            /// <returns> string of data </returns>
            Client cilent_by_number(int id);


            /// <summary>
            /// Display packege data desired
            /// </summary>
            /// <param name="packageNumbe">Serial number of a particular package</param>
            /// <returns> string of data</returns>
            Package packege_by_number(int packageNumbe);


            /// <summary>
            /// Print all the base stations
            /// </summary>
            /// <param name="array">A array list that will contain 
            /// the values ​​of all the base stations so we can print them</param>
            IEnumerable<Base_Station> Base_station_list();


            /// <summary>
            /// Print all the drones
            /// </summary>
            /// <param name="array">A array list that will contain 
            /// the values ​​of all the drones so we can print them</param>
            IEnumerable<Drone> Drone_list();



            /// <summary>
            /// Print thr all clients
            /// </summary>
            /// <param name="array">A array list that will contain 
            /// the values ​​of all the clients so we can print them</param>
            IEnumerable<Client> cilent_list();


            /// <summary>
            /// Print all the packages
            /// </summary>
            /// <param name="array">A array list that will contain 
            /// the values ​​of all the packages so we can print them</param>
            IEnumerable<Package> packege_list();


            /// <summary>
            /// Displays a list of packages that
            /// have not been assigned to a drone yet 
            /// </summary>

            IEnumerable<Package> packege_list_with_no_drone();


            /// <summary>
            /// Display of base stations with available charging stations
            /// </summary>
            /// <param name="array">A array list that will contain 
            /// the values so we can print them</param>
            IEnumerable<Base_Station> Base_station_list_with_free_charge_states();


            /// <summary>  
            /// The function takes two-point coordinates and
            /// calculates the distance between them
            /// </summary>
            /// <param name="Longitude1">Longitude of the first point </param>
            /// <param name="Latitude1">Latitude of the first point </param>
            /// <param name="Longitude2">Longitude of the second point</param>
            /// <param name="Latitude2">Latitude of the second point</param>
            /// <returns>The distance between the two points at sexagesimal base</returns>


            string Distance(double Longitude1, double Latitude1, double Longitude2, double Latitude2);

            /// <summary>
            /// Returns a point in the form of degrees
            /// </summary>
            /// <param name="point"></param>
            /// <returns></returns>
            string Point_to_degree(double point);



            /// <summary>
            /// delete a spsific base for list
            /// </summary>
            /// <param name="sirial"></param>
            void DeleteBase(int sirial);

            /// <summary>
            /// delete a spsific client 
            /// </summary>
            /// <param name="id"></param>
            void DeleteClient(int id);

            /// <summary>
            /// delete a spsific drone
            /// </summary>
            /// <param name="sirial"></param>
            void DeleteDrone(int sirial);

            /// <summary>
            /// delete a spsific packege
            /// </summary>
            /// <param name="sirial"></param>
            void Deletepackege(int sirial);


           

        }
    }
}
