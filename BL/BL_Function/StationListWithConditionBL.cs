using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {

        /* /// <summary>
         /// show base station list 
         /// </summary>
         /// <returns> base station list </returns>
         public IEnumerable<BaseStationToList> BaseStationToLists(Predicate<BaseStationToList> predicate)
         {
             List<BaseStationToList> baseStationToLists = new List<BaseStationToList>();
             if (dalObj.BaseStationList(x => true).Count() == 0)
                 throw new TheListIsEmptyException();
             uint i = 0;
             foreach (var baseStationFromDal in dalObj.BaseStationList(x => true))
             {
                 i = 0;
                 foreach (var chargePerBase in dalObj.ChargingDroneList())
                 {
                     if (chargePerBase.idBaseStation == baseStationFromDal.baseNumber)
                         i++;
                 }
                 baseStationToLists.Add(new BaseStationToList
                 {
                     SerialNum = baseStationFromDal.baseNumber,
                     Name = baseStationFromDal.NameBase,
                     BusyState = i,
                     FreeState = baseStationFromDal.NumberOfChargingStations
                 });
             }
             baseStationToLists = baseStationToLists.FindAll(predicate);
             return baseStationToLists;
         }*/

        public IEnumerable<BaseStationToList> BaseStationToLists()
        {
            return dalObj.BaseStationList(x => true).ToList().ConvertAll(convertBaseInDalToBaseStationList);
        }






    }
}
