using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    /// <summary>
    /// Package In Transfer
    /// </summary>
    public class PackageInTransfer:BO.PackageInTransfer, INotifyPropertyChanged
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
        public WeightCategories WeightCatgory
        {
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
        public bool InTheWay
        {
            get
            {
                return base.InTheWay;
            }
            set
            {
                base.InTheWay = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("InTheWay"));
                }
            }
        }//true-in the way,false-waiting to be collected
        public ClientInPackage SendClient
        {
            get
            {
                return (ClientInPackage)base.SendClient;
            }
            set
            {
                base.SendClient = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SendClient"));
                }
            }
        }
        public ClientInPackage RecivedClient
        {
            get
            {
                return (ClientInPackage)base.RecivedClient;
            }
            set
            {
                base.RecivedClient = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("RecivedClient"));
                }
            }
        }
        public Location Source
        {
            get
            {
                return (Location)base.Source;
            }
            set
            {
                base.Source = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Source"));
                }
            }
        }
        public Location Destination
        {
            get
            {
                return (Location)base.Destination;
            }
            set
            {
                base.Destination = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Destination"));
                }
            }
        }
        public double Distance {
            get
            {
                return base.Distance;
            }
            set
            {
                base.Distance = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Distance"));
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
            print += "in the way:";
            print += InTheWay ? "yes\n" : "no\n";
            print += $"Send Client: {SendClient.Name},\n";
            print += $"Recived Client: {RecivedClient.Name},\n";
            print += $"Source:\n {Source,5}\n" ;
            print += $"Destination:\n {Destination}\n";
            print += $"Distance: {Distance} KM\n";
            return print;
        }

    } 
}
