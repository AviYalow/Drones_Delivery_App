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
        /// <param name="siralNumber">Serial number of the drone</param>
        /// <param name="model">The name of the modek </param>
        /// <param name="category"> Easy / Medium / Heavy</param>
        /// <param name="butrry">Battery status</param>
        /// <param name="statos"> Free/ Maintenance/ Work</param>
        public void AddDrone(uint siralNumber, string model, uint category)
        {
              if (DataSource.drones.Any(x=>x.SerialNumber==siralNumber))
             throw (new ItemFoundException("drone", siralNumber));
            DataSource.drones.Add(new Drone()
            {
                SerialNumber = siralNumber,
                Model = model,
                WeightCategory = (Weight_categories)category
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
            DataSource.droneInCharge.Add(new BatteryLoad { IdDrone = drone, idBaseStation = base_, EntringDrone = DateTime.Now });
            
        }

        /// <summary>
        /// delete a spsific drone
        /// </summary>
        /// <param name="sirial"></param>
        public void DeleteDrone(uint sirial)
        {
            if (DataSource.drones.Any(x=>x.SerialNumber==sirial))
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

        public double[] Elctrtricity()
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
