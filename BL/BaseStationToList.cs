﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BaseStationToList
    {
        public uint SerialNum { get; init; }
        public string Name { get; set; }
        public uint FreeState { get; set; }
        public uint BusyState { get; set; }
    }
}
