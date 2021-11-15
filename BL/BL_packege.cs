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
        public uint AddPackege(Package package)
        {
            uint packegeNum = 0;
            try
            {
                packegeNum = dalObj.AddPackage(package.SendClient, package.RecivedClient, (uint)package.weightCatgory, (uint)package.priority);
            }
            catch (IDAL.DO.ItemFoundException ex)
            {
                throw (new ItemFoundExeption(ex));
            }

           
            
            return packegeNum;

        }
    }
}
