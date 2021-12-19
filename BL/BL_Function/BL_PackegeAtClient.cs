using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

using DalApi;
namespace IBL
{
    public partial class BL : IBL
    {
        /// <summary>
        /// convret Packege in the data layer to PackegeAtClient object in the logical layer
        /// </summary>
        /// <param name="package">Packege in the data layer </param>
        /// <param name="client1"> id of the client at the pacage</param>
        /// <returns> PackegeAtClient object</returns>
        PackageAtClient convretPackegeDalToPackegeAtClient(DO.Package package ,uint client1)
        {
            var convert = new PackageAtClient
            {
                SerialNum = package.SerialNumber,
                Priority = (Priority)package.Priority,
                WeightCatgory = (WeightCategories)package.WeightCatgory,
                
            };
            if (client1 == package.SendClient)
                convert.client2 = clientInPackageFromDal(package.GetingClient);
            else
                convert.client2 = clientInPackageFromDal(package.SendClient);

            if (package.PackageArrived != null)
                convert.packageStatus = PackageStatus.Arrived;
            else if(package.CollectPackageForShipment != null)
                convert.packageStatus = PackageStatus.Collected;
            else if (package.PackageAssociation !=null)
                convert.packageStatus = PackageStatus.Assign;
            else
                convert.packageStatus = PackageStatus.Create;
            return convert;

        }
        
    }
}
