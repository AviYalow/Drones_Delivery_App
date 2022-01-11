using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    /// <summary>
    /// Client To List
    /// </summary>
    public class ClientToList:BO.ClientToList, INotifyPropertyChanged
    {

        public uint ID
        {
            get
            {
                return base.ID;
            }
            init
            {
                base.ID = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ID"));
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
        public string Phone
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

        public uint Arrived {
            get
            {
                return base.Arrived;
            }
            set
            {
                base.Arrived = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Arrived"));
                }
            }
        } //the number of package that send and arrived
        public bool Active {
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
        public uint NotArrived {
            get
            {
                return base.NotArrived;
            }
            set
            {
                base.NotArrived = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("NotArrived"));
                }
            }
        }//the number of package that send and hasn't arrived yet
        public uint received
        {
            get
            {
                return base.received;
            }
            set
            {
                base.received = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("received"));
                }
            }
        }//the number of package that recived
        public uint OnTheWay {
            get
            {
                return base.OnTheWay;
            }
            set
            {
                base.OnTheWay = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OnTheWay"));
                }
            }
        }//the number of package that on the way for this client

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            String print = "";
            print += $"ID: {ID},\n";
            print += $"The Name is {Name},\n";
            print += $"Phone {Phone},\n";
            print += $"Amount of package that send and arrived: {Arrived},\n";
            print += $"Amount of package that send but not arrived yet: {NotArrived},\n";
            print += $"Amount of package that recived : {received},\n";
            print += $"Amount of package that on the way : {OnTheWay},\n";

            return print;
        }


    }
}
