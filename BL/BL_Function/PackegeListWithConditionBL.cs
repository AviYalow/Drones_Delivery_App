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
        Func<DO.Package, bool> noDronePackegeFilter = x => x.OperatorSkimmerId==0;
        Func<DO.Package, bool> arrivePackegeFilter = x => x.PackageArrived != null;
        Func<DO.Package, bool> collectedPackegeFilter = x => x.CollectPackageForShipment !=null;
        Func<DO.Package, bool> CollectedandNotArrivePackegeFilter = x => x.CollectPackageForShipment != null && x.PackageArrived is null; 
        Func<DO.Package, bool> connectedButNutCollectedPackegeFilter = x => x.CollectPackageForShipment is null && x.PackageAssociation != null;
        Func<DO.Package, bool> weightPackegeFilter = null;
        Func<DO.Package, bool> priorityPackegeFilter = null;
        /// <summary>
        /// list of packages
        /// </summary>
        /// <returns>list of packages</returns>
        public IEnumerable<PackageToList> PackageToLists()
        {
            
            if (dalObj.PackegeList(x => true).Count() == 0)
                throw new TheListIsEmptyException();


            //return from x in filerList(dalObj.PackegeList(x => true), packegeToListFilter)
            //       select x.convertPackegeDalToPackegeToList(dalObj);
            return from x in filerList(dalObj.PackegeList(x => true), x=>true)
                   select x.convertPackegeDalToPackegeToList(dalObj);


        }
        /// <summary>
        /// packeges thir only crate
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PackageToList> PackageWithNoDroneToLists(bool filterPackege=true)
        {
           
         
            packegeToListFilter -= noDronePackegeFilter;
            if (PackageToLists().Count() == 0)
                throw new TheListIsEmptyException();
            if (filterPackege)
                packegeToListFilter += noDronePackegeFilter;

            return from x in filerList(dalObj.PackegeList(x => true), packegeToListFilter)
                   select x.convertPackegeDalToPackegeToList(dalObj);

        }
        /// <summary>
        /// packege they get to the resive client
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PackageToList> PackageArriveLists(bool filterPackege = true)
        {
            
            packegeToListFilter -= arrivePackegeFilter;
            if (PackageToLists().Count() == 0)
                throw new TheListIsEmptyException();
            if (filterPackege)
                packegeToListFilter += arrivePackegeFilter;

            return from x in filerList(dalObj.PackegeList(x => true), packegeToListFilter)
                   select x.convertPackegeDalToPackegeToList(dalObj);
        }
        /// <summary>
        /// packege thier collected but not arrive
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PackageToList> PackageCollectedButNotArriveLists(bool filterPackege = true)
        {

            packegeToListFilter -= CollectedandNotArrivePackegeFilter;
            if (PackageToLists().Count() == 0)
                throw new TheListIsEmptyException();
            if (filterPackege)
                packegeToListFilter += CollectedandNotArrivePackegeFilter;

            return from x in filerList(dalObj.PackegeList(x => true), packegeToListFilter)
                   select x.convertPackegeDalToPackegeToList(dalObj);
        }
       /// <summary>
       /// packege thet connect to drone but not collected
       /// </summary>
       /// <returns></returns>
        public IEnumerable<PackageToList> PackageConnectedButNutCollectedLists(bool filterPackege = true)
        {

            packegeToListFilter -= connectedButNutCollectedPackegeFilter;
            if (PackageToLists().Count() == 0)
                throw new TheListIsEmptyException();
            if (filterPackege)
                packegeToListFilter += connectedButNutCollectedPackegeFilter;

            return from x in filerList(dalObj.PackegeList(x => true), packegeToListFilter)
                   select x.convertPackegeDalToPackegeToList(dalObj);
        }
        /// <summary>
        /// filter packeg list by weight
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public IEnumerable<PackageToList> PackageWeightLists(WeightCategories? weight=null)
        {

           

            packegeToListFilter -= weightPackegeFilter;
            if (PackageToLists().Count() == 0)
                throw new TheListIsEmptyException();
            if (weight != null)
            {
                weightPackegeFilter = x => x.WeightCatgory == (DO.WeightCategories)weight;
                packegeToListFilter += weightPackegeFilter;
               
            }
            return from x in filerList(dalObj.PackegeList(x => true), packegeToListFilter)
                   select x.convertPackegeDalToPackegeToList(dalObj);
           
        }
        /// <summary>
        /// filter list by priority
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        public IEnumerable<PackageToList> PackagePriorityLists(Priority? priority=null)
        {

          

            packegeToListFilter -= priorityPackegeFilter;
            if (PackageToLists().Count() == 0)
                throw new TheListIsEmptyException();
            if (priority != null)
            {
                priorityPackegeFilter = x => x.Priority == (DO.Priority)priority;
                packegeToListFilter += priorityPackegeFilter;
            }
            return from x in filerList(dalObj.PackegeList(x => true), packegeToListFilter)
                   select x.convertPackegeDalToPackegeToList(dalObj);
           
        }

        
    }
}
