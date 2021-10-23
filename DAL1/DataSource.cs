using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DalObject
{
    internal class DataSource
    {
        //an arrays that contain the various entity
        internal static Drone[] drones = new Drone[10];
        internal static Base_Station[] base_Stations = new Base_Station[5];
        internal static Client[] clients = new Client[100];
        internal static Package[] packages = new Package[1000];
        internal static BtarryLoad[] droneInCharge = new BtarryLoad[10];

        /// <Config>
        /// Inner class that contain a static int
        /// for indexes of the first free element
        /// in each of the arrays
        /// </Config>
        internal class Config
        {
            internal static int index_drones_empty=0;
            internal static int index_base_stations_empty=0;
            internal static int index_clients_empty=0;
            internal static int index_Packages_empty=0;
            internal static int package_num=1;
            internal static int index_butrry_chrge = 0;
        }

        /// <Initialize>
        /// A function that starts with a quick boot of all arrays with
        /// Data and update fields in the Config class accordingly
        /// </Initialize>
        public static void Initialize()
        {
            //A function that responsible for initializing names randomly in upercase 
            Random rand = new Random();
            string randomName(Random rand,int num=3)
            {
                string name = "";
                for (int j = 0; j < num; j++)
                {
                    name += (char)rand.Next(65, 91);
                }
                return name;
            }

            //A function that responsible for initializing names randomly-upercase
            //letter for the firt letter and lowercase for the other letters
            string prsonalRandomName(Random rand, int num = 3)
            {
                string name = "";
                name += (char)rand.Next(65, 91);
                for (int j = 0; j < num-1; j++)
                {
                    name += (char)rand.Next(97, 123);
                }
                return name;
            }

            //initializing the base station's array
            base_Stations[0] = new Base_Station
            {
                baseNumber = Config.index_base_stations_empty + 1,
                NameBase = randomName(rand),
                Number_of_charging_stations = rand.Next(1, 6),
                latitude = 31.790133,
                longitude = 34.627143
            };
            Config.index_base_stations_empty++;

            base_Stations[1] = new Base_Station
            {
                baseNumber = Config.index_base_stations_empty + 1,
                NameBase = randomName(rand),
                Number_of_charging_stations = rand.Next(1, 6),
                latitude = 32.009490,
                longitude = 34.736002
            };
            Config.index_base_stations_empty++;

            //initializing the drones's array
            for (int i = 0; i < 5; i++)
            {
                drones[i] = new Drone{ siralNumber = rand.Next(10000) };
                  drones[i].  Model = randomName(rand, rand.Next(3, 7));
                drones[i].  weightCategory = (Weight_categories)rand.Next(0, 3);
                drones[i]. butrryStatus =  rand.Next(25, 100) +(double) rand.Next(0,100)/100;
                drones[i].drownStatus =  (Drone_status)rand.Next(0, 2) ;
                int j = rand.Next(0, Config.index_base_stations_empty);
                drones[i].base_station = base_Stations[j].baseNumber;
                drones[i].base_station_latitude = base_Stations[j].latitude;
                drones[i].base_station_longitude = base_Stations[j].longitude;
                if(drones[i].drownStatus==Drone_status.Maintenance)
                {
                    droneInCharge[Config.index_butrry_chrge].id_drown = drones[i].siralNumber;
                    droneInCharge[Config.index_butrry_chrge].idBaseStation = drones[i].base_station;
                    Config.index_butrry_chrge++;
                }
                Config.index_drones_empty++;
            }

            //initializing the client's array
            for (int i = 0; i < 10; i++)
            {
                clients[i] = new Client { ID = rand.Next(100000000,999999999) };
                clients[i].PhoneNumber += $"05{rand.Next(0, 6)}-{rand.Next(100, 999)}-{rand.Next(1000, 9999)}";
                clients[i].Name += prsonalRandomName(rand, rand.Next(3, 7));
                clients[i].Latitude = rand.Next(31, 33) +(double) rand.Next(60000,80000)/1000000;
                clients[i].Longitude = 34 + (double)rand.Next(60000, 80000) / 1000000;
                Config.index_clients_empty++;
            }

            //initializing the package's array
            for (int i = 0; i < 10; i++)
            {
                packages[i] = new Package { sirialNumber = Config.package_num };
                packages[i].sendClient = clients[rand.Next(0, 10)].ID;
                do
                {
                    packages[i].getingClient = clients[rand.Next(0, 10)].ID;
                } while (packages[i].getingClient== packages[i].sendClient);
                packages[i].weightCatgory = (Weight_categories)rand.Next(0, 3);
                packages[i].priority=(Priority)rand.Next(0, 3);
                for (int j = 0; j <Config.index_drones_empty; j++)
                {
                    if (drones[j].weightCategory == packages[i].weightCatgory && drones[j].drownStatus == Drone_status.Free)
                    {
                        packages[i].operator_skimmer_ID = drones[j].siralNumber;
                        drones[j].drownStatus = Drone_status.Work;
                        break;
                    }
                    packages[i].operator_skimmer_ID = 0;
                }
                packages[i].receiving_delivery = (DateTime.Now.AddMinutes(rand.Next(-150, 0)));
                if (packages[i].operator_skimmer_ID != 0)
                {
                    packages[i].package_association = packages[i].receiving_delivery.AddMinutes(5);
                    if (rand.Next(0, 2) != 0)
                        packages[i].collect_package_for_shipment = packages[i].package_association.AddMinutes(30);
                }
               
               
                Config.index_Packages_empty++;
                Config.package_num++;
            }

        }






    }
}
