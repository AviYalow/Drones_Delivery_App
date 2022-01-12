using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace PO 
{
    /// <summary>
    /// Drone To List
    /// </summary>
    public class DroneToListModel : BO.DroneToList, INotifyPropertyChanged
    {
        
        public new uint SerialNumber {
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
        public new DroneModel Model
        {
            get
            {
                return (DroneModel)base.Model;
            }
            set
            {
                base.Model = (BO.DroneModel)value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Model"));
                }
            }
        }
        public new WeightCategoriesView WeightCategory {
            get
            {
                return (WeightCategoriesView)base.WeightCategory;
            }
            set
            {
                base.WeightCategory = (BO.WeightCategories)value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("WeightCategory"));
                }
            }
        }

        public new double? ButrryStatus
        {
            get
            {
                return base.ButrryStatus;
            }
            set
            {
                base.ButrryStatus = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ButrryStatus"));
                }
            }
        }
        public new DroneStatus DroneStatus
        {
            get
            {
                return (DroneStatus)base.DroneStatus;
            }
            set
            {
                base.DroneStatus = (BO.DroneStatus)value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DroneStatus"));
                }
            }
        }
        public new LocationModel  Location
        {
            get
            {
                return (LocationModel )base.Location;
            }
            set
            {
                base.Location = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Location"));
                }
            }
        }
        public new uint NumPackage
        {
            get
            {
                return base.NumPackage;
            }
            set
            {
                base.NumPackage = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("NumPackage"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            String print = "";
            print += $"Siral Number: {SerialNumber},\n";
            print += $"model: {Model},\n";
            print += $"Weight Category: {WeightCategory},\n";
            print += $" Butrry status: {ButrryStatus},\n";
            print += $" Drone status: {DroneStatus},\n";
            print += Location;
            print += $"Number Package: {NumPackage}\n";

            return print;
        }


    }
}
