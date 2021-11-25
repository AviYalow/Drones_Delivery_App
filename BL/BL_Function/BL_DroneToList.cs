using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

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
            return new Drone { SerialNum = droneToList.SerialNumber, butrryStatus = droneToList.butrryStatus, droneStatus = droneToList.droneStatus, location = droneToList.location, Model = droneToList.Model, weightCategory = droneToList.weightCategory, packageInTransfer = convertPackegeDalToPackegeInTrnansfer(dalObj.packegeByNumber(droneToList.numPackage)) };
        }
    }
}
