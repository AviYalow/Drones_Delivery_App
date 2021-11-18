﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    partial class BL : IBL
    {

        PackageAtClient convretPackegeDalToPackegeAtClient(IDAL.DO.Package package)
        {
            var convert = new PackageAtClient
            {
                SerialNum = package.SerialNumber,
                Priority = (Priority)package.Priority,
                WeightCatgory = (WeightCategories)package.WeightCatgory,
                client2 = clientInPackageFromDal(package.SerialNumber)
            };
            if (package.PackageArrived != new DateTime())
                convert.packageStatus = PackageStatus.Arrived;
            else if(package.CollectPackageForShipment != new DateTime())
                convert.packageStatus = PackageStatus.Collected;
            else if (package.PackageAssociation != new DateTime())
                convert.packageStatus = PackageStatus.Assign;
            else
                convert.packageStatus = PackageStatus.Create;
            return convert;

        }
    }
}
