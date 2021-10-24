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

        //Creating entities with initial initialization
        public DalObject()
        {
            DataSource.Initialize();
        }

        //Adding a new base station
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

        //Adding a new drone
        public static void Add_drone(int siralNumber, string model, int category, double butrry, int statos)
        {
            DataSource.drones[DataSource.Config.index_drones_empty] = new Drone() { siralNumber = siralNumber, Model = model, weightCategory = (Weight_categories)category, butrryStatus = butrry, drownStatus = (Drone_status)statos };
            DataSource.Config.index_drones_empty++;
        }

        //Adding a new client
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

        //Adding a new package
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

            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {
                if (DataSource.drones[i].weightCategory == DataSource.packages[DataSource.Config.package_num].weightCatgory &&
                    DataSource.drones[i].drownStatus == Drone_status.Free)
                {
                    DataSource.packages[DataSource.Config.package_num].operator_skimmer_ID = DataSource.drones[i].siralNumber;
                    DataSource.drones[i].drownStatus = Drone_status.Work;
                    DataSource.packages[DataSource.Config.package_num].package_association = DateTime.Now;
                }
                DataSource.packages[DataSource.Config.package_num].operator_skimmer_ID = 0;
            }
            DataSource.Config.index_Packages_empty++;
            DataSource.Config.package_num++;
            return DataSource.Config.package_num - 1;
        }

        //connect package to drone
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

        //Updated package collected
        public static void Package_collected(int packageNumber)
        {


            DataSource.packages[packageNumber - 1].collect_package_for_shipment = DateTime.Now;
        }

        //Updating a package that has reached its destination
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


        //sent drone to a free charging station
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
        public static string packege_by_number(int packageNumbe)
        {

           return(DataSource.packages[packageNumbe - 1].ToString());
        }

        public static void Base_station_list(ArrayList array)
        {

            for (int i = 0; i < DataSource.Config.index_base_stations_empty; i++)
            {

                array.Add(DataSource.base_Stations[i].ToString());


            }
        }
        public static void Drone_list(ArrayList array)
        {

            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {


                array.Add(DataSource.drones[i].ToString());

            }
        }
        public static void cilent_list(ArrayList array)
        {

            for (int i = 0; i < DataSource.Config.index_clients_empty; i++)
            {


                array.Add(DataSource.clients[i].ToString());

            }
        }
        public static void packege_list(ArrayList array)
        {

            for (int i = 0; i < DataSource.Config.package_num - 1; i++)
            {

                array.Add(DataSource.packages[i].ToString());

            }

        }

        public static void packege_list_with_no_drone(ArrayList array)
        {
            for (int i = 0; i < DataSource.Config.package_num; i++)
            {
                
                if (DataSource.packages[i].operator_skimmer_ID == 0)
                    array.Add(DataSource.packages[i].ToString());

            }
        }
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
        public static string Distance(double Longitude1, double Latitude1, double Longitude2, double Latitude2)
        {
           
           
                
            
           return($"the distans is: {Point.Distance( Longitude1,Latitude1, Longitude2, Latitude2)}KM");

        }
    }
}
