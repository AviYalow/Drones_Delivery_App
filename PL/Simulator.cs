using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using System.Threading;


namespace PL
{
    class Simulator
    {
        IBL bl;
        uint droneNumber;
        Action action;
        Func<bool> cehcking;

        public Simulator(IBL bl, uint droneNumber, Action action, Func<bool> cehcking)
        {
            this.action = action;
            this.bl = bl;
            this.cehcking = cehcking;
            this.droneNumber = droneNumber;
        }
    }
}
