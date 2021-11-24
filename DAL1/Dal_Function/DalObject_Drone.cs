using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;

namespace DalObject
{
     partial class DalObject : IDAL.IDal
    {
       

        /// <summary>
        /// Adding a new drone
        /// </summary>
        /// <param name="drone">Serial number of the drone</param>
        /// <param name="model">The name of the modek </param>
        /// <param name="category"> Easy / Medium / Heavy</param>
        /// <param name="butrry">Battery status</param>
        /// <param name="statos"> Free/ Maintenance/ Work</param>
        public void AddDrone( Drone drone)
        {
              if (DataSource.drones.Any(x=>x.SerialNumber==drone.SerialNumber))
             throw (new ItemFoundException("drone", drone.SerialNumber));
            DataSource.drones.Add(new Drone()
            {
                SerialNumber = drone.SerialNumber,
                Model =drone.Model,
                WeightCategory = (WeightCategories)drone.WeightCategory
            });

        }

        /// <summary>
        /// Display drone data desired   
        /// </summary>
        /// <param name="droneNum">Desired drone number</param>
        /// <returns> String of data</returns>
        public Drone DroneByNumber(uint droneNum)
        {
            if (!DataSource.drones.Any(x=>x.SerialNumber==droneNum))
                throw (new ItemNotFoundException("drone", droneNum));

            foreach (Drone item in DataSource.drones)
            {
                if (item.SerialNumber == droneNum)
                {
                    return item;

                }
            }
            return DataSource.drones[0];
        }

        /// <summary>
        /// Print all the drones
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values ​​of all the drones so we can print them</param>
        public IEnumerable<Drone> DroneList()
        {
            return DataSource.drones.ToList<Drone>();
        }

        public void DroneToCharge(uint drone,uint base_)
        {
            if(DataSource.drones.All(x=>x.SerialNumber!=drone))
            {
                throw (new ItemNotFoundException("drone", drone));
            }
            if (DataSource.base_Stations.All(x => x.baseNumber !=base_))
            {
                throw (new ItemNotFoundException("base station", base_));
            }
            if (DataSource.droneInCharge.Any(x => x.IdDrone == drone))
                throw new ItemFoundException("drone", drone);
           

            DataSource.droneInCharge.Add(new BatteryLoad { IdDrone = drone, idBaseStation = base_, EntringDrone = DateTime.Now });
            for (int i = 0; i < DataSource.base_Stations.Count; i++)
            {
                if(DataSource.base_Stations[i].baseNumber==base_)
                {
                    var baseNew = DataSource.base_Stations[i];
                    baseNew.NumberOfChargingStations--;
                    DataSource.base_Stations[i] = baseNew;
                }
            }
            
        }

        /// <summary>
        /// delete a spsific drone
        /// </summary>
        /// <param name="sirial"></param>
        public void DeleteDrone(uint sirial)
        {
            if (!DataSource.drones.Any(x=>x.SerialNumber==sirial))
                throw (new ItemNotFoundException("drone", sirial));
            for (int i = 0; i < DataSource.drones.Count(); i++)
            {
                if (DataSource.drones[i].SerialNumber == sirial)
                {
                    DataSource.drones.Remove(DataSource.drones[i]);
                    return;
                }
            }
        }
        /// <summary>
        /// Returns how much electricity the drone needs:
        /// 0. Available, 1. Light weight 2. Medium weight 3. Heavy 4. Charging per minute
        /// </summary>
        /// <returns></returns>
        public IEnumerable<double> Elctrtricity()
        {
            double[] elctricity = new double[5];
            elctricity[(int)ButturyLoad.Free] = DataSource.Config.free;
            elctricity[(int)ButturyLoad.Easy] = DataSource.Config.easyWeight;
            elctricity[(int)ButturyLoad.Medium] = DataSource.Config.mediomWeight;
            elctricity[(int)ButturyLoad.Heavy] = DataSource.Config.heavyWeight;
            elctricity[(int)ButturyLoad.Charging] = DataSource.Config.Charging_speed;
            return elctricity;
        }

        public void UpdateDrone(Drone drone)
        {
            int index = DataSource.drones.FindIndex(x => x.SerialNumber == drone.SerialNumber);
            if (index != -1)
                DataSource.drones[index] = drone;
            else
                throw (new IDAL.DO.ItemNotFoundException("drone", drone.SerialNumber));
        }
        
        public TimeSpan TimeInCharge(uint drone)
        {
            int i = DataSource.droneInCharge.FindIndex(x => x.IdDrone == drone);
            if (i==-1)
            {
                throw (new ItemNotFoundException("drone", drone));
            }
            return (DateTime.Now - DataSource.droneInCharge[i].EntringDrone);

        }

        public void FreeDroneFromCharge(uint drone)
        {
            int i = DataSource.droneInCharge.FindIndex(x => x.IdDrone == drone);
                if (i == -1)
                throw (new ItemNotFoundException("drone", drone));
            DataSource.droneInCharge.RemoveAt(i);
           
        }


    }
}
