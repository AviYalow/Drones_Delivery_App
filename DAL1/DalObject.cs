using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL;
using IDAL.DO;


namespace DalObject
{

    public partial class DalObject
    {

        /// <summary>
        ///Creating entities with initial initialization
        /// </summary>
        public DalObject()
        {
            DataSource.Initialize();
        }


         /* bool sustainability_test(IEnumerable<DAL.IKey> keys, int number)
        {
            
            foreach (DAL.IKey item in keys)
            {
                if (item.key == number)
                {
                    return true;
                }
            }

            return false;
        }*/




        /// <summary>
        /// loking for free drone
        /// </summary>
        /// <param name="weight_"></param>
        /// <returns></returns>
        /*  public static int found_drone_for_package(Weight_categories weight_)
          {
              for (int i = 0; i < DataSource.drones.Count(); i++)
              {

                  if (DataSource.drones[i].weightCategory == weight_)                    )
                  {

                      DataSource.drones[i].drownStatus = Drone_status.Work;
                      return DataSource.drones[i].serialNumber;
                  }

              }
              return 0;
          }*/


        /// <summary>
        /// free drone to by free to take other job
        /// </summary>
        /// <param name="sirialNum"></param>
        /*public   void drone_after_work(int sirialNum)
        {
            for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
            {
                if (DataSource.drones[i].serialNumber == sirialNum)
                {
                    DataSource.drones[i].drownStatus = Drone_status.Free;
                    DataSource.drones[i].butrryStatus -= 20;
                }
            }
        }*/


        /// <summary>
        /// sent drone to a free charging station
        /// </summary>
        /// <param name="base_station">Desired base station number</param>
        /// <param name="droneNmber">Desirable drone number</param>
        /*   public   void send_drone_to_charge(int base_station, int droneNmber)
           {
               for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
               {
                   if (DataSource.drones[i].serialNumber == droneNmber)
                   {

                       DataSource.drones[i].drownStatus = Drone_status.Maintenance;
                       for (int j = 0; j < DataSource.Config.index_base_stations_empty; j++)
                       {
                           if (DataSource.base_Stations[j].baseNumber == base_station)
                           {
                               update_charge_station_in_base(base_station, -1);
                               update_drone_charge_in_base(DataSource.base_Stations[j].baseNumber, Drone_in_charge.Add, DataSource.drones[i].serialNumber);
                               break;
                           }
                       }
                       break;
                   }
               }




           }*/
        /// <summary>
        /// Update of drone release from charging station
        /// </summary>
        /// <param name="base_station"></param>
        /// <param name="dronSirialNum"></param>
        /*    public   void update_drone_charge_in_base( int dronSirialNum,Drone_in_charge chose, int base_station=0)
            {
                switch (chose)
                {
                    case Drone_in_charge.Add:
                        DataSource.droneInCharge[DataSource.Config.index_battery_charge].idBaseStation = base_station;
                        DataSource.droneInCharge[DataSource.Config.index_battery_charge].id_drone = dronSirialNum;
                        break;
                    case Drone_in_charge.Delete:
                        for (int i = 0; i < DataSource.Config.index_battery_charge; i++)
                        {
                            if (DataSource.droneInCharge[i].id_drone == dronSirialNum)
                            {
                                DataSource.Config.index_battery_charge--;
                                for (int j = i; j < DataSource.Config.index_battery_charge; j++)
                                {
                                    DataSource.droneInCharge[j] = DataSource.droneInCharge[j + 1];
                                }
                                break;
                            }
                        }
                        break;
                    default:
                        break;
                }

            }*/
        /// <summary>
        /// update charge station in base
        /// </summary>
        /// <param name="base_station"></param>
        /// <param name="newChargeStationFree"></param>
        /*     public   void update_charge_station_in_base(int base_station, int newChargeStationFree)
             {
                 for (int i = 0; i < DataSource.Config.index_base_stations_empty; i++)
                 {
                     if (DataSource.base_Stations[i].baseNumber == base_station)
                         DataSource.base_Stations[i].Number_of_charging_stations += newChargeStationFree;
                 }

             }
        */
        /*   public   void free_drone_from_charge(int sirialNumber)
           {


               for (int i = 0; i < DataSource.Config.index_drones_empty; i++)
               {
                   if (DataSource.drones[i].serialNumber == sirialNumber)
                   {
                       DataSource.drones[i].drownStatus = Drone_status.Free;
                       DataSource.drones[i].butrryStatus = 100;
                       update_charge_station_in_base(DataSource.drones[i].base_station, 1);
                       update_drone_charge_in_base(DataSource.drones[i].serialNumber,Drone_in_charge.Delete);
                       break;
                   }
               }



           }*/


        /// <summary>  
        /// The function takes two-point coordinates and
        /// calculates the distance between them
        /// </summary>
        /// <param name="Longitude1">Longitude of the first point </param>
        /// <param name="Latitude1">Latitude of the first point </param>
        /// <param name="Longitude2">Longitude of the second point</param>
        /// <param name="Latitude2">Latitude of the second point</param>
        /// <returns>The distance between the two points at sexagesimal base</returns>


        public string Distance(double Longitude1, double Latitude1, double Longitude2, double Latitude2)
        {
            return ($"the distance is: {Point.Distance(Longitude1, Latitude1, Longitude2, Latitude2)}KM");
        }
        /// <summary>
        /// Returns a point in the form of degrees
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public string Point_to_degree(double point)
        {
            return Point.Degree(point);
        }







    }
}
