using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    partial class BL : IBL
    {/// <summary>
     /// add packege to list in data source 
     /// </summary>
     /// <param name="package"></param>
     /// <returns></returns>
        public uint AddPackege(Package package)
        {
            uint packegeNum = 0;
            try
            {
                packegeNum = dalObj.AddPackage(new IDAL.DO.Package
                {
                    SendClient = dalObj.CilentByNumber(package.SendClient).Id,
                    GetingClient = dalObj.CilentByNumber(package.RecivedClient).Id,
                    Priority = (IDAL.DO.Priority)package.priority,
                    WeightCatgory = (IDAL.DO.Weight_categories)package.weightCatgory,
                    PackageArrived = package.create_package

                });
            }
            catch (IDAL.DO.ItemFoundException ex)
            {
                throw (new ItemFoundExeption(ex));
            }



            return packegeNum;

        }

        public void ConnctionPackegeToDrone(uint droneNumber)
        {
            var drone = dronesListInBl.Find(x => x.SerialNum == droneNumber);
            if (drone == null)
            { }
            if (drone.droneStatus != Drone_status.Free)
            { }
            IEnumerable<IDAL.DO.Package> packege,temp;
            packege = dalObj.PackegeListWithNoDrone().ToList().FindAll(x => x.Priority == IDAL.DO.Priority.Immediate);
            if (packege == null)
                packege = dalObj.PackegeListWithNoDrone().ToList().FindAll(x => x.Priority == IDAL.DO.Priority.quick);
            if (packege == null)
                packege = dalObj.PackegeListWithNoDrone().ToList().FindAll(x => x.Priority == IDAL.DO.Priority.Regular);

            switch (drone.weightCategory)
            {
                case Weight_categories.Heavy:
                    temp = dalObj.PackegeListWithNoDrone().ToList().FindAll(x => x.WeightCatgory == IDAL.DO.Weight_categories.Heavy);
                    if (temp == null)
                        temp = dalObj.PackegeListWithNoDrone().ToList().FindAll(x => x.WeightCatgory == IDAL.DO.Weight_categories.Medium);
                    if (temp == null)
                        temp = dalObj.PackegeListWithNoDrone().ToList().FindAll(x => x.WeightCatgory == IDAL.DO.Weight_categories.Easy);
                    packege = temp;
                    break;
                case Weight_categories.Medium:
                    temp = dalObj.PackegeListWithNoDrone().ToList().FindAll(x => x.WeightCatgory == IDAL.DO.Weight_categories.Medium);
                    if (temp == null)
                        temp = dalObj.PackegeListWithNoDrone().ToList().FindAll(x => x.WeightCatgory == IDAL.DO.Weight_categories.Easy);
                    packege = temp;
                    break;
                case Weight_categories.Easy:
                    packege = dalObj.PackegeListWithNoDrone().ToList().FindAll(x => x.WeightCatgory == IDAL.DO.Weight_categories.Easy);
                    break;
                default:
                    break;


            }

            Package cloosetPackege(Location location,IEnumerable<IDAL.DO.Package> packages=null)
            {
                Package package = new Package();
                if (packages!=null)
                {
                    Location location1 = ClientLocation( packages.ToList() [0].SendClient);
                    

                    for (int i = 1; i < packages.ToList(). Count(); i++)
                    {
                        uint sendig = packages.ToList()[i].SendClient;
                        Location location2 = ClientLocation(sendig);
                        if (Distans(location, location1) > Distans(location, location2))
                        {
                            location1 = location2;
                            package.SendClient = sendig;
                        }

                    }
                    
                }
                return package;

            }
        }
    }
}
