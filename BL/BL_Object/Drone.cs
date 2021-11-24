﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
   public class Drone
    {
        public uint SerialNum { get; init; }
        public string Model { get; set; }

        public WeightCategories weightCategory { get; set; }
        public PackageInTransfer packageInTransfer { get; set; }

        public double butrryStatus { get; set; }
        public DroneStatus droneStatus { get; set; }
        public Location location { get; set; }
        public override string ToString()
        {
            String print = "";
            print += $"Siral Number: {SerialNum},\n";
            print += $"model: {Model},\n";
            print+= $"Weight Category: {weightCategory},\n";
            print += $"Package in transfer:\n";
            print +=(packageInTransfer != null)? $" {packageInTransfer}":'0';
            print += $"\n Butrry status: {butrryStatus},\n";
            print += $" Drone status: {droneStatus},\n";
            print += $"{location}\n";
           
            return print;
        }

    }
}