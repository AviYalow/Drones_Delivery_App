using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    interface IDal
    {
        public IEnumerable<Base_Station> Base_station_list();
        public IEnumerable<Drone> Drone_list();
        public IEnumerable<Client> cilent_list();
        public IEnumerable<Package> packege_list();
        public IEnumerable<Package> packege_list_with_no_drone();
        public IEnumerable<Base_Station> Base_station_list_with_free_charge_states();
        public string Distance(double Longitude1, double Latitude1, 
            double Longitude2, double Latitude2);
        public string Point_to_degree(double point);

        public void DeleteBase(int sirial);
        public void DeleteClient(int id);
        public void DeleteDrone(int sirial);
        public void Deletepackege(int sirial);
    }
}
