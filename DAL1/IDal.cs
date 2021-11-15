﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        public void AddDrone(uint siralNumber, string model,uint category);
        public uint AddPackage(uint idsend, uint idget, uint kg, uint priorityByUser);
        public void AddStation(uint base_num, string name, uint numOfCharging, double latitude, double longitude);
        public void AddClient(uint id, string name, string phone, double latitude, double londitude);


        

        public void PackageCollected(uint packageNumber);
        public void ConnectPackageToDrone(uint packageNumber, uint drone_sirial_number);
        public void PackageArrived(uint packageNumber);
        public Package packegeByNumber(uint packageNumber);
        public IEnumerable<Package> PackegeList();
        public IEnumerable<Package> PackegeListWithNoDrone();
        public IEnumerable<Package> PackagesWithDrone();
        public IEnumerable<Package> PackagesArriveList();
        public void UpdatePackege(Package package);
        public void DeletePackege(uint sirial);


        public Drone DroneByNumber(uint droneNum);
        public double[] Elctrtricity();
        public void UpdateDrone(Drone drone);
        public void DeleteDrone(uint sirial);
        public IEnumerable<Drone> DroneList();
        public void DroneToCharge(uint drone, uint base_);

        public void AddStation(Base_Station base_Station);
        public IEnumerable<Base_Station> BaseStationList();
        public IEnumerable<Base_Station> BaseStationListWithChargeStates();
        public Base_Station BaseStationByNumber(uint baseNum);
        public void DeleteBase(uint sirial);
        public void UpdateBase(Base_Station base_);

        public IEnumerable<Client> cilentList();
        public Client CilentByNumber(uint id);
        public void DeleteClient(uint id);
        public void UpdateClient(Client client);



        public double Distance(double Longitude1, double Latitude1,
            double Longitude2, double Latitude2);
        public string PointToDegree(double point);
        public IEnumerable<BatteryLoad> ChargingDroneList();



        
        
       


    }
}
