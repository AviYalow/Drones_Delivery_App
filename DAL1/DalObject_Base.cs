using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject  
    {
      
           
            /// <summary>
            ///Adding a new base station
            /// </summary>
            /// <param name="base_num">The base station number </param>
            /// <param name="name"> The name ot the station </param>
            /// <param name="numOfCharging">The amount of charging stations at the station </param>
            /// <param name="latitude">Latitude of the station</param>
            /// <param name="longitude">Longitude of the station</param>
            public void Add_station(int base_num, string name, int numOfCharging, double latitude, double longitude)
            {

                //  if (sustainability_test(base_num))
                // throw ("Error: The base already exist in the system");

                DataSource.base_Stations.Add(new Base_Station
                {
                    baseNumber = base_num,
                    NameBase = name,
                    Number_of_charging_stations = numOfCharging,
                    latitude = latitude,
                    longitude = longitude

                });



            }


            /// <summary>
            /// Display base station data desired   
            /// </summary>
            /// <param name="baseNum">Desired base station number</param>
            /// <returns> String of data </returns>
            public Base_Station Base_station_by_number(int baseNum)
            {

                //  if (!sustainability_test(baseNum))
                // throw ("Error: The base does not exist in the system");
                foreach (Base_Station number in DataSource.base_Stations)
                {
                    if (number.baseNumber == baseNum)
                    {
                        return number;

                    }

                }
                return DataSource.base_Stations[0];
            }


            /// <summary>
            /// Print all the base stations
            /// </summary>
            /// <param name="array">A array list that will contain 
            /// the values ​​of all the base stations so we can print them</param>
            public IEnumerable<Base_Station> Base_station_list()
        { 
        
            
               // return DataSource.base_Stations.ToList<Base_Station>();

                //  foreach (Base_Station base_ in DataSource.base_Stations)
                //    yield return base_;
                return DataSource.base_Stations.ToList<Base_Station>();
            }


            /// <summary>
            /// Display of base stations with available charging stations
            /// </summary>
            /// <param name="array">A array list that will contain 
            /// the values so we can print them</param>
            public IEnumerable<Base_Station> Base_station_list_with_free_charge_states()
            {
                List<Base_Station> temp = new List<Base_Station>();
                foreach (Base_Station item in DataSource.base_Stations)
                    if (item.Number_of_charging_stations > 0)
                        temp.Add(item);
                return temp.ToList<Base_Station>();

            }


            /// <summary>
            /// delete a spsific base for list
            /// </summary>
            /// <param name="sirial"></param>
            public void DeleteBase(int sirial)
            {
                //  if (!sustainability_test(baseNum))
                // throw ("Error: The base does not exist in the system");
                DataSource.base_Stations.Remove(DataSource.base_Stations[DataSource.base_Stations.FindIndex(x => x.baseNumber == sirial)]);


            }
        }
  
}
