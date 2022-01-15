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
    public class ClientToListModel :BO.ClientToList, INotifyPropertyChanged
    {

        public new uint ID
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
        public new string Phone
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

        public new uint Arrived {
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
        public new bool Active {
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
        public new uint NotArrived {
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
        public new uint received
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
        public new uint OnTheWay {
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




    }
}
