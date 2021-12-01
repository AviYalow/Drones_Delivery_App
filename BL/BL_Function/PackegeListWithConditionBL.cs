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
        public IEnumerable<PackageToList> PackageToLists(Predicate<PackageToList> predicate)
        {
            if (dalObj.PackegeList(x => true).Count() == 0)
                throw new TheListIsEmptyException();

            List<PackageToList> packageToLists = new List<PackageToList>();

            foreach (var packege in dalObj.PackegeList(x => true))
            {
                packageToLists.Add(convertPackegeDalToList(packege));

            }
            packageToLists = packageToLists.FindAll(predicate);
            return packageToLists;
        }

    }
}
