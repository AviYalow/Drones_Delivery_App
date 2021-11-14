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
        internal static List<BatteryLoad> droneInCharge = new List<BatteryLoad>();


        /// <Config>
        /// Inner class that contain a static int
        /// for indexes of the first free element
        /// in each of the arrays
        /// </Config>
        internal class Config
        {
            internal static double free { get { return 0.5; } }//per minute
            internal static double easyWeight  { get { return 1; } }//per minute
            internal static double mediomWeight { get { return 1.5; } }//per minute
            internal static double heavyWeight { get { return 2; } }//per minute
            internal static double Charging_speed { get { return 1.8; } }//per minute
            internal static uint package_num = 10000;
            
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

            uint idSnding(List<Client> client, uint sending)
            {
                int i;

                for (i = rand.Next(10); client[i].ID == sending; i = rand.Next(10))
                {

                }
                return client[i].ID;
            }

            //initializing the base station's array
            base_Stations.Add(new Base_Station
            {
                baseNumber =(uint)rand.Next(1000, 10000),
                NameBase = randomName(rand),
                Number_of_charging_stations = (uint)rand.Next(3, 6),
                latitude = 31.790133,
                longitude = 34.627143

            });


            base_Stations.Add(new Base_Station
            {
                baseNumber = (uint)rand.Next(1000, 10000),
                NameBase = randomName(rand),
                Number_of_charging_stations = (uint)rand.Next(3, 6),
                latitude = 32.009490,
                longitude = 34.736002
            });


            //initializing the drones's array in a randone values
            for (int i = 0; i < 5; i++)
            {
                drones.Add(new Drone
                {
                    serialNumber = (uint)rand.Next(10000),
                    Model = randomName(rand, rand.Next(3, 7)),
                    weightCategory = (Weight_categories)rand.Next(0, 3),
                   
                });


            }

            //initializing the client's array in a random values
            for (int i = 0; i < 10; i++)
            {
                clients.Add(new Client
                {
                    ID = (uint)rand.Next(100000000, 999999999),

                    PhoneNumber = ($"05{rand.Next(0, 6)}-{rand.Next(100, 999)}-{rand.Next(1000, 9999)}"),
                    Name = (personalRandomName(rand, rand.Next(3, 7))),
                    Latitude = rand.Next(31, 33) + (double)rand.Next(60000, 80000) / 1000000,
                    Longitude = 34 + (double)rand.Next(60000, 80000) / 1000000
                });


            }

            //initializing the package's array in a random values
            for (int i = 0; i < 10; i++)
            {
                int j = rand.Next(0, 10);
                Package package = new Package() { serialNumber = Config.package_num };



                package.sendClient = clients[j].ID;
                package.getingClient = idSnding(clients, clients[j].ID);
                package.weightCatgory = (Weight_categories)rand.Next(0, 3);
                package.priority = (Priority)rand.Next(0, 3);
                package.operator_skimmer_ID = (rand.Next(2) != 0) ? drones[rand.Next(5)].serialNumber : 0;
                package.receiving_delivery = DateTime.Now.AddMinutes(rand.Next(-300, -150));

                package.package_association = (package.operator_skimmer_ID != 0) ? package.receiving_delivery.AddMinutes(2) : new DateTime();
                if (package.package_association != new DateTime())
                {
                    package.package_arrived = (rand.Next(2) != 0) ? package.package_association.AddMinutes(rand.Next(60)) : new DateTime();
                    if (package.package_arrived != new DateTime())
                        package.collect_package_for_shipment = (rand.Next(2) != 0) ? package.package_arrived.AddMinutes(rand.Next(60)) : new DateTime();
                }

                packages.Add(new Package
                {
                    serialNumber = package.serialNumber,
                    sendClient = package.sendClient,
                    getingClient = package.getingClient,
                    package_arrived = package.package_arrived,
                    collect_package_for_shipment = package.collect_package_for_shipment,
                    operator_skimmer_ID = package.operator_skimmer_ID,
                    package_association = package.package_association,
                    priority = package.priority,
                    receiving_delivery = package.receiving_delivery,
                    weightCatgory = package.weightCatgory


                });

                Config.package_num++;

            };



        }

    }


}

