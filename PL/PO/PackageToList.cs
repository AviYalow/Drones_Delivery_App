using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    /// <summary>
    /// Package To List
    /// </summary>
    public class PackageToList:BO.PackageToList, INotifyPropertyChanged
    {
        public uint SerialNumber
        {
            get
            {
                return base.SerialNumber;
            }
            set
            {
                base.SerialNumber = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SerialNumber"));
                }
            }
        }
        public string SendClient
        {
            get
            {
                return base.SendClient;
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
        public string RecivedClient {
            get
            {
                return base.RecivedClient;
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
        public Priority priority
        {
            get
            {
                return (Priority)base.priority;
            }


            set
            {
                base.priority = (BO.Priority)value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("priority"));
                }
            }
        }
        public WeightCategories WeightCategories {
            get
            {
                return (WeightCategories)base.WeightCategories;
            }
            set
            {
                base.WeightCategories = (BO.WeightCategories)value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("WeightCategories"));
                }
            }
        }
        public PackageStatus packageStatus
        {
            get
            {
                return (PackageStatus)base.packageStatus;
            }
            set
            {
                base.packageStatus = (BO.PackageStatus)value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("packageStatus"));
                }
            }
        }
        public bool Drone
        {
            get
            {
                return base.Drone;
            }
            set
            {
                base.Drone = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Drone"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            String print = "";
            print += $"Serial Number: {SerialNumber},\n";
            print += $"Send Client: {SendClient},\n";
            print += $"Recived Client: {RecivedClient},\n";
            print += $"Weight Category: {WeightCategories},\n";
            print += $"Priority: {priority},\n";
            print += $"Package Status: {packageStatus},\n";
            return print;
        }

    }
}