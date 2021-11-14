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
        public void Add_drone(uint siralNumber, string model,uint category);
        public uint Add_package(uint idsend, uint idget, uint kg, uint priorityByUser);
        public void Add_station(uint base_num, string name, uint numOfCharging, double latitude, double longitude);
        public void Add_client(uint id, string name, string phone, double latitude, double londitude);


        public void Package_collected(uint packageNumber);
        public void connect_package_to_drone(uint packageNumber, uint drone_sirial_number);
        public void Package_arrived(uint packageNumber);
        public Package packege_by_number(uint packageNumber);
        public IEnumerable<Package> packege_list();
        public IEnumerable<Package> packege_list_with_no_drone();
        public IEnumerable<Package> Packages_with_drone();
        public IEnumerable<Package> Packages_arrive_list();
        public void UpdatePackege(Package package);
        public void Deletepackege(uint sirial);


        public Drone Drone_by_number(uint droneNum);
        public double[] Elctrtricity();
        public void Update_drone(Drone drone);
        public void DeleteDrone(uint sirial);
        public IEnumerable<Drone> Drone_list();
        public void Drone_To_charge(uint drone, uint base_);

        public void Add_station(Base_Station base_Station);
        public IEnumerable<Base_Station> Base_station_list();
        public IEnumerable<Base_Station> Base_station_list_with_charge_states();
        public Base_Station Base_station_by_number(uint baseNum);
        public void DeleteBase(uint sirial);
        public void UpdateBase(Base_Station base_);

        public IEnumerable<Client> cilent_list();
        public Client cilent_by_number(uint id);
        public void DeleteClient(uint id);
        public void Update_client(Client client);



        public double Distance(double Longitude1, double Latitude1,
            double Longitude2, double Latitude2);
        public string Point_to_degree(double point);
        public IEnumerable<BatteryLoad> Charging_Drone_List();






    }
}
