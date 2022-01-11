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
    public class Package: BO.Package,INotifyPropertyChanged
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

        public ClientInPackage RecivedClient { get; set; }
        public WeightCategories WeightCatgory { get; set; }
        public Priority Priority { get; set; }
        public DroneInPackage Drone { get; set; }

        //Delivery time create a package
        public DateTime? Create_package { get; set; }

        //Time to assign the package to a drone
        public DateTime? PackageAssociation { get; set; }

        //Package collection time from the sender
        public DateTime? CollectPackage { get; set; }

        //Time of arrival of the package to the recipient
        public DateTime? PackageArrived { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
   
    }
}
