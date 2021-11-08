using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;

namespace DalObject
{
     partial class DalObject
    {
        

        /// <summary>
        /// Adding a new drone
        /// </summary>
        /// <param name="siralNumber">Serial number of the drone</param>
        /// <param name="model">The name of the modek </param>
        /// <param name="category"> Easy / Medium / Heavy</param>
        /// <param name="butrry">Battery status</param>
        /// <param name="statos"> Free/ Maintenance/ Work</param>
        public void Add_drone(int siralNumber, string model, int category, double butrry, int statos)
        {
            //  if (sustainability_test(droneNum))
            // throw ("Error: The drone already exist in the system");
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
            //  if (!sustainability_test(droneNum))
            // throw ("Error: The drone does not exist in the system");

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
            foreach (Drone drone in DataSource.drones)
                yield return drone;
        }

        /// <summary>
        /// delete a spsific drone
        /// </summary>
        /// <param name="sirial"></param>
        public void DeleteDrone(int sirial)
        {
            //  if (!sustainability_test(droneNum))
            // throw ("Error: The drone does not exist in the system");
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

    }
}
