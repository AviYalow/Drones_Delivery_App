﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Client
    {
        public uint Id { get; init; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }

        public List<PackageAtClient> FromClient { get; set; }
        public List<PackageAtClient> ToClient { get; set; }

        public override string ToString()
        {
            String print = "";
            print += $"ID: {Id},\n";
            print += $"Name is {Name},\n";
            print += $"Phone: {Phone},\n";
            print += Location;
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
