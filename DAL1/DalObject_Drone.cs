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
        public void Add_drone(int siralNumber, string model, int category)
        {
              if (DataSource.drones.Any(x=>x.serialNumber==siralNumber))
             throw (new Item_found_exception("drone", siralNumber));
            DataSource.drones.Add(new Drone()
            {
                serialNumber = siralNumber,
                Model = model,
                weightCategory = (Weight_categories)category
            });

        }

        /// <summary>
        /// Display drone data desired   
        /// </summary>
        /// <param name="droneNum">Desired drone number</param>
        /// <returns> String of data</returns>
        public Drone Drone_by_number(int droneNum)
        {
            if (!DataSource.drones.Any(x=>x.serialNumber==droneNum))
                throw (new Item_not_found_exception("drone", droneNum));

            foreach (Drone item in DataSource.drones)
            {
                if (item.serialNumber == droneNum)
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
        public IEnumerable<Drone> Drone_list()
        {
            return DataSource.drones.ToList<Drone>();
        }

        public void Drone_To_charge(int drone,int base_)
        {
            if(DataSource.drones.All(x=>x.serialNumber!=drone))
            {
                throw (new Item_not_found_exception("drone", drone));
            }
            if (DataSource.base_Stations.All(x => x.baseNumber !=base_))
            {
                throw (new Item_not_found_exception("base station", base_));
            }
            DataSource.droneInCharge.Add(new BatteryLoad { id_drone = drone, idBaseStation = base_, entringDrone = DateTime.Now });
            
        }

        /// <summary>
        /// delete a spsific drone
        /// </summary>
        /// <param name="sirial"></param>
        public void DeleteDrone(int sirial)
        {
            if (DataSource.drones.Any(x=>x.serialNumber==sirial))
                throw (new Item_not_found_exception("drone", sirial));
            for (int i = 0; i < DataSource.drones.Count(); i++)
            {
                if (DataSource.drones[i].serialNumber == sirial)
                {
                    DataSource.drones.Remove(DataSource.drones[i]);
                    return;
                }
            }
        }

        public double[] Elctrtricity()
        {
            double[] elctricity = new double[5];
            elctricity[(int)butturyLoad.Free] = DataSource.Config.free;
            elctricity[(int)butturyLoad.Easy] = DataSource.Config.easyWeight;
            elctricity[(int)butturyLoad.Medium] = DataSource.Config.mediomWeight;
            elctricity[(int)butturyLoad.Heavy] = DataSource.Config.heavyWeight;
            elctricity[(int)butturyLoad.Charging] = DataSource.Config.Charging_speed;
            return elctricity;
        }

        public void Update_drone(Drone drone)
        {
            int index = DataSource.drones.FindIndex(x => x.serialNumber == drone.serialNumber);
            if (index != -1)
                DataSource.drones[index] = drone;
            else
                throw (new IDAL.DO.Item_not_found_exception("drone", drone.serialNumber));
        }
        
        public TimeSpan TimeInCharge(int drone)
        {
            int i = DataSource.droneInCharge.FindIndex(x => x.id_drone == drone);
            if (i==-1)
            {
                throw (new Item_not_found_exception("drone", drone));
            }
            return (DateTime.Now - DataSource.droneInCharge[i].entringDrone);

        }

        public void FreeDroneFromCharge(int drone)
        {
            int i = DataSource.droneInCharge.FindIndex(x => x.id_drone == drone);
                if (i == -1)
                throw (new Item_not_found_exception("drone", drone));
            DataSource.droneInCharge.RemoveAt(i);
           
        }


    }
}
