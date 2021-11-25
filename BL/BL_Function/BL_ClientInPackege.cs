using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL:IBL
    {
        /// <summary>
        /// Request a client object from the data layer
        /// </summary>
        /// <param name="id">ID of the client</param>
        /// <returns>client object on the logical layer</returns>
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
