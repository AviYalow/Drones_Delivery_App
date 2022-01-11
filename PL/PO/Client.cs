using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    /// <summary>
    ///client 
    /// </summary>
    public class Client:BO.Client, INotifyPropertyChanged
    {
        public uint Id
        {
            get
            {
                return base.Id;
            }
            init
            {
                base.Id = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));
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
        public string Phone {
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

        public ObservableCollection<PackageAtClient> FromClient {
            get
            {
                ObservableCollection<PackageAtClient> tmp = null;
                foreach (var item in base.FromClient)
                    tmp.Add((PackageAtClient)item);
                return tmp;
            }
            set
            {
                ObservableCollection<BO.PackageAtClient> tmp = null;
                foreach (var item in value)
                    tmp.Add((BO.PackageAtClient)item);
                base.FromClient = tmp;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("FromClient"));
                }
            }
        }
        public ObservableCollection<PackageAtClient> ToClient {
            get
            {
                ObservableCollection<PackageAtClient> tmp = null;
                foreach (var item in base.ToClient)
                    tmp.Add((PackageAtClient)item);
                return tmp;
            }
            set
            {
                ObservableCollection<BO.PackageAtClient> tmp = null;
                foreach (var item in value)
                    tmp.Add((BO.PackageAtClient)item);
                base.ToClient = tmp;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ToClient"));
                }
            }
        }
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

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            String print = "";
            print += $"ID: {Id},\n";
            print += $"Name is {Name},\n";
            print += $"Phone: {Phone},\n";
            print += $"Location: {Location}\n";
            print += $"Statos client: ";
            print += Active ? "Active\n" : "Not active\n";
            print += "Packege from this client:\n";
            if (FromClient != null)
                foreach (var packege in FromClient)
                { print += $"{packege}"; }
            else
                print += "0\n";
            print += "Packege to this client:\n";
            if (ToClient != null)
                foreach (var packege in ToClient)
                { print += $"{packege}"; }
            else
                print += "0\n";

            return print;
        }


    }
}
