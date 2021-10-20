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
            DataSource.drons[DataSource.Config.index_drowns_empty] = new Drone() { siralNumber = num  };
            Console.Write("Enter name:");
            DataSource.drons[DataSource.Config.index_drowns_empty].Model = Console.ReadLine();
            Console.Write("Enter weight Category:0 for easy,1 for mediom,2 for heavy:");
            res = int.TryParse(Console.ReadLine(), out num);
            DataSource.drons[DataSource.Config.index_drowns_empty].weightCategory = (Weight_categories)num;
            Console.Write("Enter amount of butrry:");
            res =  double.TryParse(Console.ReadLine(), out butrry);
            DataSource.drons[DataSource.Config.index_drowns_empty].butrryStatos = butrry;
            Console.Write("Enter a statos:0 for free,1 for maintenance,2 for in charge:");
            res = int.TryParse(Console.ReadLine(), out num);
            DataSource.drons[DataSource.Config.index_drowns_empty].drownStatos = (Drone_statos)num;
            DataSource.Config.index_drowns_empty++;
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
            Console.Write("Enter fon number:");
            DataSource.clients[DataSource.Config.index_clients_empty].FonNumber = Console.ReadLine();
            Console.Write("Enter latitude:");
            cheak = double.TryParse(Console.ReadLine(), out pointLine);
            DataSource.clients[DataSource.Config.index_clients_empty].Latitude = pointLine;
            Console.Write("Enter londitude:");
            cheak = double.TryParse(Console.ReadLine(), out pointLine);
            DataSource.clients[DataSource.Config.index_clients_empty].Longitude = pointLine;

        }
        public static void Add_package()
        {
            bool cheak;
            int intNum;
            int send,get;
            Console.Write("Enter ID:");
            DataSource.packages[DataSource.Config.package_num] = new Package { sirialNumber = DataSource.Config.package_num++ };
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            DataSource.packages[DataSource.Config.package_num].sendClient = intNum;
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            DataSource.packages[DataSource.Config.package_num].getingClient = intNum;
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            DataSource.packages[DataSource.Config.package_num].weightCatgory =(Weight_categories) intNum;
            cheak = int.TryParse(Console.ReadLine(), out intNum);
            DataSource.packages[DataSource.Config.package_num].priority = (Priority)intNum;
            for (int i = 0; i < DataSource.Config.index_drowns_empty; i++)
            {
                if (DataSource.drons[i].weightCategory == DataSource.packages[DataSource.Config.package_num].weightCatgory
                    && DataSource.drons[i].drownStatos==Drone_statos.Free)
                {
                    DataSource.packages[DataSource.Config.package_num].operator_skimmer_ID = DataSource.drons[i].siralNumber;
                    intNum = i;
                    break;
                }
                DataSource.packages[DataSource.Config.package_num].operator_skimmer_ID = 0;
            }
            DataSource.packages[DataSource.Config.package_num].receiving_delivery = DateTime.Now;
            DataSource.packages[DataSource.Config.package_num].package_association = DateTime.Now.AddMinutes(2);
            DataSource.packages[DataSource.Config.package_num].package_arrived = DateTime.Now.AddMinutes(52);
            DataSource.packages[DataSource.Config.package_num].collect_package_for_shipment = DateTime.Now.AddMinutes(30);


        }

    }
}
