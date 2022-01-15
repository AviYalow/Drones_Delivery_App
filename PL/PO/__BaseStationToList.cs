using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    /// <summary>
    /// base station to list
    /// </summary>
    public class BaseStationToListModel : BO.BaseStationToList, INotifyPropertyChanged
    {
        public new uint SerialNum
        {
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
        public new string Name
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
        public new uint FreeState {
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
        public new uint BusyState {
            get
            {
                return base.BusyState;
            }
            set
            {
                base.BusyState = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("BusyState"));
                }
            }
        }
        public new string Active
        {
            get
            {
                return base.Active;
            }
            set
            {
                base.Active = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Active"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            String print = "";
            print += $"Siral Number is {SerialNum},\n";
            print += $"The Name is {Name},\n";
            print += $"Number of free state: {FreeState},\n";
            print += $"Number of busy state: {BusyState}\n ";
            return print;
        }
    }
}
