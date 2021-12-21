using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

using DalApi;
namespace BlApi
{
    partial class BL : IBL
    {
        /// <summary>
        /// list of packages
        /// </summary>
        /// <returns>list of packages</returns>
        public IEnumerable<PackageToList> PackageToLists()
        {
            
            if (dalObj.PackegeList(x => true).Count() == 0)
                throw new TheListIsEmptyException();


            return from packege in dalObj.PackegeList(x => true)
                   select packege.convertPackegeDalToPackegeToList(dalObj);





        }
        /// <summary>
        /// packeges thir only crate
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PackageToList> PackageWithNoDroneToLists()
        {

            if (dalObj.PackegeList(x => x.OperatorSkimmerId!=0).Count() == 0)
                throw new TheListIsEmptyException();



            return from packege in dalObj.PackegeList(x => x.OperatorSkimmerId != 0)
                   select packege.convertPackegeDalToPackegeToList(dalObj);
           

            
        }
        /// <summary>
        /// packege they get to the resive client
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PackageToList> PackageArriveLists()
        {

            if (dalObj.PackegeList(x => x.PackageArrived!=null).Count() == 0)
                throw new TheListIsEmptyException();

            return from packege in dalObj.PackegeList(x => x.PackageArrived != null)
                   select packege.convertPackegeDalToPackegeToList(dalObj);
        }
        /// <summary>
        /// packege thier collected but not arrive
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PackageToList> PackageCollectedButNotArriveLists()
        {

            if (dalObj.PackegeList(x => x.CollectPackageForShipment != null&&x.PackageArrived is null).Count() == 0)
                throw new TheListIsEmptyException();

            return from packege in dalObj.PackegeList(x => x.CollectPackageForShipment != null && x.PackageArrived is null)
                   select packege.convertPackegeDalToPackegeToList(dalObj);
        }
       /// <summary>
       /// packege thet connect to drone but not collected
       /// </summary>
       /// <returns></returns>
        public IEnumerable<PackageToList> PackageConnectedButNutCollectedLists()
        {

            if (dalObj.PackegeList(x => x.CollectPackageForShipment is null && x.PackageAssociation != null).Count() == 0)
                throw new TheListIsEmptyException();

            return from packege in dalObj.PackegeList(x => x.CollectPackageForShipment is null && x.PackageAssociation != null)
                   select packege.convertPackegeDalToPackegeToList(dalObj);
        }


        public IEnumerable<PackageToList> PackageWeightLists(WeightCategories weight)
        {

            if (dalObj.PackegeList(x => x.WeightCatgory==(DO.WeightCategories)weight).Count() == 0)
                throw new TheListIsEmptyException();

            return from packege in dalObj.PackegeList(x => x.WeightCatgory == (DO.WeightCategories)weight)
                   select packege.convertPackegeDalToPackegeToList(dalObj);
        }
        public IEnumerable<PackageToList> PackagePriorityLists(Priority priority)
        {

            if (dalObj.PackegeList(x => x.Priority == (DO.Priority)priority).Count() == 0)
                throw new TheListIsEmptyException();

            return from packege in dalObj.PackegeList(x => x.Priority == (DO.Priority)priority)
                   select packege.convertPackegeDalToPackegeToList(dalObj);
        }
     

    }
}
