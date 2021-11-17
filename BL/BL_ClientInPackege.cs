using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
   partial class BL:IBL
    {
        ClientInPackage clientInPackageFromDal(uint id)
        {
            IDAL.DO.Client client;
            try
            {
                 client = dalObj.CilentByNumber(id);
            }
            catch(IDAL.DO. ItemNotFoundException ex)
            {
                throw new ItemNotFoundException(ex);
            }
            return new ClientInPackage { Id = client.Id, Name = client.Name };
        }

    }
}
