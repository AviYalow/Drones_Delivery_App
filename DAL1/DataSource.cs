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
        internal static List<Drone> drones = new List<Drone>();
        internal static List<Base_Station> base_Stations = new List<Base_Station>();
        internal static List<Client> clients = new List<Client>();
        internal static List<Package> packages = new List<Package>();
       // internal static List<BtarryLoad> droneInCharge = new List<BtarryLoad>();


        /// <Config>
        /// Inner class that contain a static int
        /// for indexes of the first free element
        /// in each of the arrays
        /// </Config>
        internal class Config
        {

            internal static int package_num = 10000;

        }

        /// <Initialize>
        /// A function that starts with a quick boot of all arrays with
        /// Data and update fields in the Config class accordingly
        /// </Initialize>
        public static void Initialize()
        {

            Random rand = new Random();

            //A function that responsible for initializing names randomly in upercase
            string randomName(Random rand, int num = 3)
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
            string personalRandomName(Random rand, int num = 3)
            {
                string name = "";
                name += (char)rand.Next(65, 91);
                for (int j = 0; j < num - 1; j++)
                {
                    name += (char)rand.Next(97, 123);
                }
                return name;
            }

            int idSnding(List<Client> client ,int sending)
            {
                int i;
                
                for ( i = rand.Next(10); client[i].ID!=sending; i = rand.Next(10))
                {

                }
                return client[i].ID;
            }

            //initializing the base station's array
            base_Stations.Add(new Base_Station
            {
                baseNumber = rand.Next(1000, 10000),
                NameBase = randomName(rand),
                Number_of_charging_stations = rand.Next(3, 6),
                latitude = 31.790133,
                  longitude = 34.627143
                
            });


            base_Stations.Add(new Base_Station
            {
                baseNumber = rand.Next(1000, 10000),
                NameBase = randomName(rand),
                Number_of_charging_stations = rand.Next(3, 6),
                latitude = 32.009490,
                longitude = 34.736002
            });


            //initializing the drones's array in a randone values
            for (int i = 0; i < 5; i++)
            {
                drones.Add(new Drone
                {
                    siralNumber = rand.Next(10000),
                    Model = randomName(rand, rand.Next(3, 7)),
                    weightCategory = (Weight_categories)rand.Next(0, 3),
                    base_station = base_Stations[rand.Next(2)].baseNumber
                });


            }

            //initializing the client's array in a random values
            for (int i = 0; i < 10; i++)
            {
                clients.Add(new Client
                {
                    ID = rand.Next(100000000, 999999999),

                    PhoneNumber = ($"05{rand.Next(0, 6)}-{rand.Next(100, 999)}-{rand.Next(1000, 9999)}"),
                    Name = (prsonalRandomName(rand, rand.Next(3, 7))),
                    Latitude = rand.Next(31, 33) + (double)rand.Next(60000, 80000) / 1000000,
                    Longitude = 34 + (double)rand.Next(60000, 80000) / 1000000
                });


            }

            //initializing the package's array in a random values
            for (int i = 0; i < 10; i++)
            {
                int j= rand.Next(0, 10);
                Package package = new Package
                {
                    sirialNumber = Config.package_num,
                    sendClient = clients[j].ID,
                    getingClient = idSnding(clients, clients[j].ID),
                    weightCatgory = (Weight_categories)rand.Next(0, 3),
                    priority = (Priority)rand.Next(0, 3),
                    operator_skimmer_ID = (rand.Next(2) != 0) ? drones[rand.Next(10)].siralNumber : 0,
                    receiving_delivery = DateTime.Now.AddMinutes(rand.Next(-300, -150)),
                };
                package.package_association = (package.operator_skimmer_ID != 0) ? package.receiving_delivery.AddMinutes(2) : new DateTime();

                packages.Add(new Package
                {
                  
                }) ;
               
                

            };


            Config.package_num++;
        }

    }


}

