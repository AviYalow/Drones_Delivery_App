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
        public uint Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                base.Id = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));
                }
            }
        }
        public string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

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
