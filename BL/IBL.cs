using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public interface IBL
    {
        public void Add_station(uint base_num, string name, uint numOfCharging, double latitude, double longitude);
        public void Add_client(uint id, string name, string phone, double latitude, double londitude);
        public void Add_drone(uint siralNumber, string model, uint category);
        public uint Add_package(uint idsend, uint idget, uint kg, uint priorityByUser);
        public void connect_package_to_drone(uint packageNumber, uint drone_sirial_number);
        public void Package_collected(uint packageNumber);
        public void Package_arrived(uint packageNumber);
        public void ReleaseDroneFromCharge(uint serial, uint timeInCharge);
        public void DroneToCharge(uint serial);


    }
}
