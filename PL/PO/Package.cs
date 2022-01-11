using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PO
{
    /// <summary>
    /// Package
    /// </summary>
    public class Package: BO.Package, INotifyPropertyChanged
    {
        
        
       public ClientInPackage SendClient
        {
            get
            {
                return (ClientInPackage) base.SendClient;
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
        
        public Priority Priority
        {
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
      
        public DroneInPackage Drone
        {
            get
            {
                return (DroneInPackage) base.Drone;
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

        //Delivery time create a package
        public DateTime? Create_package {
            get
            {
                return base.Create_package;
            }
            set
            {
                base.Create_package = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Create_package"));
                }
            }
        }

        //Time to assign the package to a drone
        public DateTime? PackageAssociation
        {
            get
            {
                return base.PackageAssociation;
            }
            set
            {
                base.PackageAssociation = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PackageAssociation"));
                }
            }
        }

        //Package collection time from the sender
        public DateTime? CollectPackage {
            get
            {
                return base.CollectPackage;
            }
            set
            {
                base.CollectPackage = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CollectPackage"));
                }
            }
        }

        //Time of arrival of the package to the recipient
        public DateTime? PackageArrived {
            get
            {
                return base.PackageArrived;
            }
            set
            {
                base.PackageArrived = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PackageArrived"));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
   
    }
}
