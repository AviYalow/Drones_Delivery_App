using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PO
{
    /// <summary>
    /// base station
    /// </summary>
  public  class BaseStation: BO.BaseStation, INotifyPropertyChanged
    {

        public uint SerialNum {
            get
            {
                return base.SerialNum;
            }
            init
            {
                base.SerialNum = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SerialNum"));
                }
            }
        }

        public string Name {
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
        public Location Location
        {
            get
            {
                return (Location)base.Location;
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
        public uint FreeState
        {
            get
            {
                return base.FreeState;
            }
            set
            {
                base.FreeState = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("FreeState"));
                }
            }
        }
        public ObservableCollection<DroneInCharge> DronesInChargeList {
            get
            {
                ObservableCollection<DroneInCharge> tmp = null;
                foreach(var item in base.DronesInChargeList)
                         tmp.Add((DroneInCharge)item);
                return tmp; 
            }
            set
            {
                ObservableCollection<BO.DroneInCharge> tmp = null;
                foreach (var item in value)
                    tmp.Add((BO.DroneInCharge)item);
                base.DronesInChargeList = tmp;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DronesInChargeList"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            String print= "";
            print += $"Siral Number is {SerialNum},\n";
            print += $"The Name is {Name},\n";
            print += $"Location: Latitude:{Location.Latitude} Longitude:{Location.Longitude},\n";
            print += $"Number of free state: {FreeState},\n";
            print += $"Drone in Charge: {DronesInChargeList.Count()},\n";
            foreach(DroneInCharge drone in DronesInChargeList)
            {
               print += $"Serial number: {drone.SerialNum}, butrry Status: {drone.ButrryStatus}\n";
            }
            return print;
        }

    }
}
