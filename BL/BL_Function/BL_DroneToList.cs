using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

using DalApi;
namespace IBL
{
   public partial class BL:IBL
    {

        /// <summary>
        /// convert droneToList object to drone object
        /// </summary>
        /// <param name="droneToList"> droneToList object</param>
        /// <returns> drone object </returns>
        Drone convertDroneToListToDrone(DroneToList droneToList)
        {
            return new Drone { SerialNum = droneToList.SerialNumber, butrryStatus = droneToList.ButrryStatus, droneStatus = droneToList.DroneStatus, location = droneToList.Location, Model = droneToList.Model, weightCategory = droneToList.WeightCategory, packageInTransfer = convertPackegeDalToPackegeInTrnansfer(dalObj.packegeByNumber(droneToList.NumPackage)) };
        }
    }
}
