using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {


        /// <summary>
        ///Adding a new base station
        /// </summary>
        /// <param name="base_num">The base station number </param>
        /// <param name="name"> The name ot the station </param>
        /// <param name="numOfCharging">The amount of charging stations at the station </param>
        /// <param name="latitude">Latitude of the station</param>
        /// <param name="longitude">Longitude of the station</param>
        public void AddStation(uint base_num, string name, uint numOfCharging, double latitude, double longitude)
        {

            if (DataSource.base_Stations.Any(x => x.baseNumber == base_num))
                throw (new ItemFoundException("Base station", base_num));

            DataSource.base_Stations.Add(new Base_Station
            {
                baseNumber = base_num,
                NameBase = name,
                NumberOfChargingStations = numOfCharging,
                latitude = latitude,
                longitude = longitude

            });



        }
        public void AddStation(Base_Station base_Station)
        {
            if (DataSource.base_Stations.Any(x => x.baseNumber == base_Station.baseNumber))
                throw (new ItemFoundException("base station", base_Station.baseNumber));
            DataSource.base_Stations.Add(base_Station);
        }


        /// <summary>
        /// Display base station data desired   
        /// </summary>
        /// <param name="baseNum">Desired base station number</param>
        /// <returns> String of data </returns>
        public Base_Station BaseStationByNumber(uint baseNum)
        {

            if (!DataSource.base_Stations.Any(x => x.baseNumber == baseNum))
                throw (new ItemNotFoundException("Base station", baseNum));
            return DataSource.base_Stations[DataSource.base_Stations.FindIndex(x => x.baseNumber == baseNum)];

        }


        /// <summary>
        /// Print all the base stations
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values ​​of all the base stations so we can print them</param>
        public IEnumerable<Base_Station> BaseStationList()
        {
            if(DataSource.base_Stations.Count==0)
            {
                throw (new ListEmptyException("Base Station"));
            }
            return DataSource.base_Stations.ToList<Base_Station>();
        }


        /// <summary>
        /// Display of base stations with available charging stations
        /// </summary>
        /// <param name="array">A array list that will contain 
        /// the values so we can print them</param>
        public IEnumerable<Base_Station> BaseStationListWithChargeStates()
        {
            if (DataSource.base_Stations.Count == 0)
                throw (new NoItemWhitThisConditionException());
            return DataSource.base_Stations.FindAll(x => x.NumberOfChargingStations > 0);

        }


        /// <summary>
        /// delete a spsific base for list
        /// </summary>
        /// <param name="sirial"></param>
        public void DeleteBase(uint sirial)
        {
            if (!DataSource.base_Stations.Any(x => x.baseNumber == sirial))
                throw (new ItemNotFoundException("Base station", sirial));
            DataSource.base_Stations.Remove(DataSource.base_Stations[DataSource.base_Stations.FindIndex(x => x.baseNumber == sirial)]);


        }

        public void UpdateBase(Base_Station base_)
        {
            int i = DataSource.base_Stations.FindIndex(x => x.baseNumber == base_.baseNumber);
            if (i == -1)
                throw (new IDAL.DO.ItemNotFoundException("Base Station", base_.baseNumber));
            else
                DataSource.base_Stations[i] = base_;
        }
    }

}
