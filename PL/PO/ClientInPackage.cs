using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    /// <summary>
    /// Client In Package
    /// </summary>
    public class ClientInPackage: BO.ClientInPackage, INotifyPropertyChanged
    {
        public uint Id { get; set; }
        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            string print = "";
            print += $"ID: {Id},\n";
            print += $"Name: {Name}\n";
    
            return print;
        }
    }
}
