using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO    
{

    /// <summary>
    /// Package At Client
    /// </summary>
    public class PackageAtClient: BO.PackageAtClient, INotifyPropertyChanged
    {
        public uint SerialNum {
            get
            {
                return base.SerialNum;
            }
            set
            {
                base.SerialNum = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SerialNum"));
                }
            }
        }
        public WeightCategories WeightCatgory {
            get
            {
                return (WeightCategories)base.WeightCatgory;
            }
            set
            {
                base.WeightCatgory = (BO.WeightCategories)value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("WeightCatgory"));
                }
            }
        }
        public Priority Priority {
            get
            {
                return (Priority)base.Priority;
            }


            set
            {
                base.Priority = (BO.Priority)value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Priority"));
                }
            }
        }
        public PackageStatus PackageStatus
        {
            get
            {
                return (PackageStatus)base.PackageStatus;
            }


            set
            {
                base.PackageStatus = (BO.PackageStatus)value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PackageStatus"));
                }
            }
        }
       
        //The other client in the package.
        //The receiver for the sender and sender for the receiver
        public ClientInPackage Client2 {
            get
            {
                return (ClientInPackage)base.Client2;
            }
            set
            {
                base.Client2 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Client2"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            String print = "";
            print += $"Serial Number: {SerialNum},\n";
            print += $"Weight Category: {WeightCatgory},\n";
            print += $"priority: {Priority},\n";
            print += $"Package Status: {PackageStatus},\n";
            print += $"The other client in the package: {Client2.Name}\n";
            return print;
        }
        }
}
