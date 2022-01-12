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
    public class PackageToListModel:BO.PackageToList, INotifyPropertyChanged
    {
        public new uint SerialNumber
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
        public new string SendClient
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
        public new string RecivedClient {
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
        public new PriorityView priority
        {
            get
            {
                return (PriorityView)base.priority;
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
        public new WeightCategoriesView WeightCategories {
            get
            {
                return (WeightCategoriesView)base.WeightCategories;
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
        public new PackageStatusView packageStatus
        {
            get
            {
                return (PackageStatusView)base.packageStatus;
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
        public new bool Drone
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

       

    }
}