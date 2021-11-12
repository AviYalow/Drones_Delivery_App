using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        public void Add_drone(int siralNumber, string model, int category, double butrry, int statos);
        public int Add_package(int idsend, int idget, int kg, int priorityByUser);
        public void Add_station(int base_num, string name, int numOfCharging, double latitude, double longitude);
        public void Add_client(int id, string name, string phone, double latitude, double londitude);


        public void Package_collected(int packageNumber);
        public void connect_package_to_drone(int packageNumber, int drone_sirial_number);
        public void Package_arrived(int packageNumber);
        public Package packege_by_number(int packageNumber);
        public IEnumerable<Package> packege_list();
        public IEnumerable<Package> packege_list_with_no_drone();
        public IEnumerable<Package> Packages_with_drone();
        public IEnumerable<Package> Packages_arrive_list();

        public Drone Drone_by_number(int droneNum);
        public double[] Elctrtricity();
        public void Update_drone(Drone drone);
        public void DeleteDrone(int sirial);
        public IEnumerable<Drone> Drone_list();
        public void Deletepackege(int sirial);



        public IEnumerable<Base_Station> Base_station_list();
        public IEnumerable<Base_Station> Base_station_list_with_free_charge_states();
        public Base_Station Base_station_by_number(int baseNum);
        public void DeleteBase(int sirial);


        public IEnumerable<Client> cilent_list();
        public Client cilent_by_number(int id);
        public void DeleteClient(int id);


        public double Distance(double Longitude1, double Latitude1, 
            double Longitude2, double Latitude2);
        public string Point_to_degree(double point);

        
       
       
        

       
    }
}
