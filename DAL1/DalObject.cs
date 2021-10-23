using System;
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
        public DalObject()
        {
            DataSource.Initialize();
        }
        public static void Add_station()
        {
            Console.Write("Enter base number:");
            bool cheak;
            int intNum;
            double pointLine;
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            DataSource.base_Stations[DataSource.Config.index_base_stations_empty] = new Base_Station
            {
                baseNumber = intNum
            };
            Console.Write("Enter base name:");
            DataSource.base_Stations[DataSource.Config.index_base_stations_empty].NameBase = Console.ReadLine();
            Console.Write("Enter Number of charging stations:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            DataSource.base_Stations[DataSource.Config.index_base_stations_empty].Number_of_charging_stations = intNum;
            Console.Write("Enter latitude:");
            cheak = double.TryParse(Console.ReadLine(), out pointLine);
            DataSource.base_Stations[DataSource.Config.index_base_stations_empty].latitude = pointLine;
            Console.Write("Enter longitude:");
            cheak = double.TryParse(Console.ReadLine(), out pointLine);
            DataSource.base_Stations[DataSource.Config.index_base_stations_empty].longitude = pointLine;
            DataSource.Config.index_base_stations_empty++;
        }
        public static void Add_drone()
        {
            bool res;
            int num;
            double butrry;
            Console.Write("Enter sireal number:");
            res = int.TryParse(Console.ReadLine(), out num);
            DataSource.drones[DataSource.Config.index_drones_empty] = new Drone() { siralNumber = num };
            Console.Write("Enter name:");
            DataSource.drones[DataSource.Config.index_drones_empty].Model = Console.ReadLine();
            Console.Write("Enter weight Category:0 for easy,1 for mediom,2 for heavy:");
            res = int.TryParse(Console.ReadLine(), out num);
            DataSource.drones[DataSource.Config.index_drones_empty].weightCategory = (Weight_categories)num;
            Console.Write("Enter amount of butrry:");
            res = double.TryParse(Console.ReadLine(), out butrry);
            DataSource.drones[DataSource.Config.index_drones_empty].butrryStatus = butrry;
            Console.Write("Enter a statos:0 for free,1 for maintenance,2 for in charge:");
            res = int.TryParse(Console.ReadLine(), out num);
            DataSource.drones[DataSource.Config.index_drones_empty].drownStatus = (Drone_status)num;
            DataSource.Config.index_drones_empty++;
        }
        public static void Add_client()
        {
            bool cheak;
            int intNum;
            double pointLine;
            Console.Write("Enter ID:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            DataSource.clients[DataSource.Config.index_clients_empty] = new Client { ID = intNum };
            Console.Write("Enter name:");
            DataSource.clients[DataSource.Config.index_clients_empty].Name = Console.ReadLine();
            Console.Write("Enter phone number:");
            DataSource.clients[DataSource.Config.index_clients_empty].PhoneNumber = Console.ReadLine();
            Console.Write("Enter latitude:");
            cheak = double.TryParse(Console.ReadLine(), out pointLine);
            DataSource.clients[DataSource.Config.index_clients_empty].Latitude = pointLine;
            Console.Write("Enter londitude:");
            cheak = double.TryParse(Console.ReadLine(), out pointLine);
            DataSource.clients[DataSource.Config.index_clients_empty].Longitude = pointLine;
            DataSource.Config.index_clients_empty++;
        }
        public static int Add_package()
        {
            bool cheak;
            int intNum;

            DataSource.packages[DataSource.Config.package_num].receiving_delivery = DateTime.Now;
            DataSource.packages[DataSource.Config.package_num] = new Package { sirialNumber = DataSource.Config.package_num++ };
            Console.Write("Enter ID of the sender:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            DataSource.packages[DataSource.Config.package_num].sendClient = intNum;
            Console.Write("Enter ID of the recipient:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            DataSource.packages[DataSource.Config.package_num].getingClient = intNum;
            Console.Write("Enter Weight categories 0 for easy,1 for mediom,3 for haevy:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            DataSource.packages[DataSource.Config.package_num].weightCatgory = (Weight_categories)intNum;
            Console.Write("Enter priority 0 for Immediate ,1 for quick ,2 for Regular:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            DataSource.packages[DataSource.Config.package_num].priority = (Priority)intNum;
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


        public static void connect_package_to_drone()
        {
            bool cheak;
            int intNum;
            Console.Write("Enter package number:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {
                if (DataSource.drones[i].weightCategory == DataSource.packages[intNum - 1].weightCatgory
                    && DataSource.drones[i].drownStatus == Drone_status.Free)
                {
                    DataSource.packages[intNum - 1].operator_skimmer_ID = DataSource.drones[i].siralNumber;
                    DataSource.drones[i].drownStatus = Drone_status.Work;
                    DataSource.packages[intNum - 1].package_association = DateTime.Now;
                    break;
                }
                DataSource.packages[DataSource.Config.package_num].operator_skimmer_ID = 0;
            }
        }
        public static void Package_collected()
        {
            bool cheak;
            int intNum;
            Console.Write("Enter package number:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            DataSource.packages[intNum - 1].collect_package_for_shipment = DateTime.Now;




        }
        public static void Package_arrived()
        {
            bool cheak;
            int intNum;
            Console.Write("Enter package number:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            DataSource.packages[intNum - 1].package_arrived = DateTime.Now;
            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {
                if (DataSource.drones[i].siralNumber == DataSource.packages[intNum - 1].operator_skimmer_ID)
                {
                    DataSource.drones[i].drownStatus = Drone_status.Free;
                    DataSource.drones[i].butrryStatus -= 20;
                }
            }
        }
        public static void send_drone_to_charge(int base_station)
        {
            bool cheak;
            int intNum;
            Console.Write("Enter drone number:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {
                if (DataSource.drones[i].siralNumber == intNum)
                {
                    if (DataSource.drones[i].drownStatus == Drone_status.Work)
                    {
                        Console.WriteLine("The drone in shipment, plese with until is finish!");
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
                            break;
                        }
                    }
                    break;
                }
            }

        }
        public static void free_drone_from_charge()
        {
            bool cheak;
            int intNum;
            Console.Write("Enter drone number:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {
                if (DataSource.drones[i].siralNumber == intNum)
                {
                    DataSource.drones[i].drownStatus = Drone_status.Free;
                    DataSource.drones[i].butrryStatus = 100;
                    break;
                }
            }
        }

        public static string Base_station_by_number()
        {
            bool cheak;
            int intNum;
            Console.Write("Enter base number:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            for (int i = 0; i < DataSource.Config.index_base_stations_empty; i++)
            {
                if (DataSource.base_Stations[i].baseNumber == intNum)
                {
                    return DataSource.base_Stations[i].ToString();

                }

            }
            return "base station not exitst";
        }
        public static void Drone_by_number()
        {
            bool cheak;
            int intNum;
            Console.Write("Enter drone number:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {
                if (DataSource.drones[i].siralNumber == intNum)
                {
                    Console.WriteLine(DataSource.drones[i].ToString());
                    break;
                }
            }
        }
        public static void cilent_by_number()
        {
            bool cheak;
            int intNum;
            Console.Write("Enter ID:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            for (int i = 0; i < DataSource.Config.index_clients_empty; i++)
            {
                if (DataSource.clients[i].ID == intNum)
                {
                    Console.WriteLine(DataSource.clients[i].ToString());
                    break;
                }
            }
        }
        public static void packege_by_number()
        {
            bool cheak;
            int intNum;
            Console.Write("Enter packege number:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            Console.WriteLine(DataSource.packages[intNum - 1].ToString());



        }

        public static void Base_station_list()
        {

            for (int i = 0; i < DataSource.Config.index_base_stations_empty; i++)
            {

                Console.WriteLine(DataSource.base_Stations[i].ToString());


            }
        }
        public static void Drone_list()
        {

            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {


                Console.WriteLine(DataSource.drones[i].ToString());

            }
        }
        public static void cilent_list()
        {

            for (int i = 0; i < DataSource.Config.index_clients_empty; i++)
            {


                Console.WriteLine(DataSource.clients[i].ToString());

            }
        }
        public static void packege_list()
        {

            for (int i = 0; i < DataSource.Config.package_num - 1; i++)
            {

                Console.WriteLine(DataSource.packages[i].ToString());

            }

        }

        public static void packege_list_with_no_drone()
        {
            for (int i = 0; i < DataSource.Config.package_num; i++)
            {
                if (DataSource.packages[i].operator_skimmer_ID == 0)
                    Console.WriteLine(DataSource.packages[i].ToString());

            }
        }
        public static void Base_station_list_with_free_charge_states()
        {

            for (int i = 0; i < DataSource.Config.index_base_stations_empty; i++)
            {
                if (DataSource.base_Stations[i].Number_of_charging_stations > 0)
                    Console.WriteLine(DataSource.base_Stations[i]);
            }
        }
        public static void Distance()
        {
            bool cheak;
            int intNum, count = 0;
            double[] points = new double[4];
            Console.Write("Enter 1 for base point,2 for client point for the first point:");
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            for (int i = 1; i < 2; i++)
            {
                switch ((Distans_2_point)intNum)
                {
                    case Distans_2_point.base_station:
                        Console.Write("Enter base number:");
                        cheak = int.TryParse(Console.ReadLine(), out intNum);
                        for (int j = 0; j < DataSource.Config.index_base_stations_empty; j++)
                        {
                            if (DataSource.base_Stations[j].baseNumber == intNum)
                            {
                                points[count] = DataSource.base_Stations[j].longitude;
                                count++;
                                points[count] = DataSource.base_Stations[j].latitude;
                                count++;
                            }

                        }

                        break;
                    case Distans_2_point.client:
                        Console.Write("Enter client number:");
                        cheak = int.TryParse(Console.ReadLine(), out intNum);
                        for (int j = 0; j < DataSource.Config.index_drones_empty; j++)
                        {
                            if (DataSource.drones[j].siralNumber == intNum)
                            {
                                points[count] = DataSource.clients[j].Longitude;
                                count++;
                                points[count] = DataSource.clients[j].Latitude;
                                count++;
                            }

                        }
                        break;
                    default:
                        Console.WriteLine("no point");
                        return;

                }
                Console.Write("Enter 1 for base point, 2 for client point for the second point:");
                cheak = int.TryParse(Console.ReadLine(), out intNum);
            }
            Console.WriteLine($"the distans is: {Point.Distans(points[0], points[2], points[1], points[3])}KM");

        }
    }
}
