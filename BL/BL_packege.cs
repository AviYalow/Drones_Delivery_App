using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    partial class BL : IBL
    {
        public int AddPackege(Package package)
        {
            int packegeNum = 0;
            try
            {
                packegeNum = dalObj.Add_package(package.SendClient, package.RecivedClient, (int)package.weightCatgory, (uint)package.priority);
            }
            catch (IDAL.DO.Item_found_exception ex)
            {
                throw (new Item_found_exeption(ex));
            }

           
            
            return packegeNum;

        }
    }
}
