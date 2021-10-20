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
        internal Drone[] drowns = new Drone[10];
        internal Base_Station[] base_Stations = new Base_Station[5];
        internal Client[] clients = new Client[100];
        internal Package[] packages = new Package[1000];

        internal class Config
        {
            static int index_drowns_empty=0;
            static int index_base_stations_empty=0;
            static int index_clients_empty=0;
            static int index_Packages_empty=0;
            static int package_num;



        }







    }
}
