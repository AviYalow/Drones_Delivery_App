using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
   static class DalConfig
    {
        internal static string DalName;
        internal static Dictionary<string, string> DalPackeges;
        static DalConfig()
        {
            XElement dalConfig = XElement.Load(@"xml\dal-config.xml");
            DalName = dalConfig.Element("dal").Value;
            DalPackeges = (from pkg in dalConfig.Element("dal-packeges").Elements()
                           select pkg).ToDictionary(p => "" + p.Name, p => p.Value);
        }
    }
    public class DalConfigException:Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg , Exception ex) : base(msg, ex) { }
    }


}
