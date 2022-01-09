using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  BlApi;



namespace BL
{
    internal class Simulator
    {


        //enum Maintenance { Starting, Going, Charging};
        private const double SPEED = 1.0;
        private const int DELAY = 1000;
        //private const double TIME_STEP = DELAY / 1000.0;
        //private const double STEP = VELOCITY / TIME_STEP;

        public  Simulator(BlApi.BL bl, uint droneNumber, Action action, Func<bool> StopChecking)
        {
            var drone = bl.GetDrone(droneNumber);
            uint? packageSerialNum = null; 

           
            do
            {
                switch (drone.DroneStatus)
                {
                    case BO.DroneStatus.Free:
                        var packlist = from item in bl.PackageWithNoDroneToLists()
                                 where ((BO.WeightCategories)item.WeightCategories <= drone.WeightCategory)
                                 && (bl.buttryDownPackegeDelivery(bl.convertPackegeBlToPackegeInTrnansfer(bl.ShowPackage(item.SerialNumber))) < drone.ButrryStatus)
                                 orderby item.priority descending, item.WeightCategories
                                 select item;
                        packageSerialNum = packlist.FirstOrDefault().SerialNumber;           

                        break;
                    case BO.DroneStatus.Maintenance:
                        break;
                    case BO.DroneStatus.Work:
                        break;
                    case BO.DroneStatus.Delete:
                        break;
                    default:
                        break;
                }

            } while (!StopChecking());

        }
    }
}
