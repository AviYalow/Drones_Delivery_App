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
        }

    }
}
