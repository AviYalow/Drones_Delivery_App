﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

using DalApi;
namespace BlApi
{
   partial class BL:IBL
    {

        /// <summary>
        /// convert droneToList object to drone object
        /// </summary>
        /// <param name="droneToList"> droneToList object</param>
        /// <returns> drone object </returns>
        Drone convertDroneToListToDrone( DroneToList droneToList)
        {

            return new Drone { SerialNum = droneToList.SerialNumber, butrryStatus = droneToList.ButrryStatus, droneStatus = droneToList.DroneStatus, location = droneToList.Location, Model = droneToList.Model, weightCategory = droneToList.WeightCategory, packageInTransfer = convertPackegeDalToPackegeInTrnansfer(dalObj.packegeByNumber(droneToList.NumPackage)) };
        }
        /// <summary>
        /// find specific drone in the list of the drones
        /// </summary>
        /// <param name="siralNuber"> serial number of the drone</param>
        /// <returns> drone founded </returns>
        public DroneToList SpecificDrone(uint siralNuber)
        {


            var drone = dronesListInBl.Find(x => x.SerialNumber == siralNuber && x.DroneStatus != DroneStatus.Delete);
            if (drone is null)
                throw new ItemNotFoundException("drone", siralNuber);
            return drone;

        }
    }
}
