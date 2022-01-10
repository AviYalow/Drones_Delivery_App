using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using static BlApi.BL;
using BO;
using BlApi;



namespace BL
{
    internal class Simulator
    {

        BlApi.BL BL;
        private Thread myThread;
        Stopwatch sw;
        double chargePerMiliSecond;
        //enum Maintenance { Starting, Going, Charging};
        private const double SPEED = 1.0;
        private const int DELAY = 1000;
        //private const double TIME_STEP = DELAY / 1000.0;
        //private const double STEP = VELOCITY / TIME_STEP;

        public Simulator(BlApi.BL bl, uint droneNumber, Action action, Func<bool> StopChecking)
        {
            try
            {
                BL = bl;

                sw = new Stopwatch();
                Drone drone;
                bool sendToCharge = false;
                double m = 0;
                chargePerMiliSecond = 100 / (bl.chargingPerMinute / 60 * 1000);

                new Thread(() =>
                {


                    myThread = Thread.CurrentThread;
                    while (StopChecking())
                    {
                        lock (bl)
                        {
                            if (!sw.IsRunning)
                                sw.Start();
                            drone = bl.GetDrone(droneNumber);
                            switch (drone.DroneStatus)
                            {
                                case BO.DroneStatus.Free:


                                    try
                                    {
                                        if ((drone.ButrryStatus < 20 || sendToCharge)&&drone.ButrryStatus<100)
                                        {
                                            var base_ = bl.ClosestBase(drone.Location, true);
                                            var path = (sw.ElapsedMilliseconds * 100) * ((double)SpeedDrone.Easy / 60.0 / 60.0 / 10);
                                            var a = bl.Distans(drone.Location, base_.Location);
                                            if (path <= a)
                                            {
                                                drone.ButrryStatus -= bl.buttryDownWithNoPackege(a - (m) * ((double)SpeedDrone.Free / 60.0 / 60.0 / 10));
                                                sw.Stop();
                                                sw.Reset();
                                                bl.DroneToCharge(drone.SerialNumber);
                                            }
                                            else
                                            {
                                                bl.dronesListInBl.Find(x => x.SerialNumber == drone.SerialNumber).ButrryStatus = drone.ButrryStatus;
                                            }
                                            m = sw.ElapsedMilliseconds;
                                        }
                                        else
                                        {
                                            sw.Stop();
                                            sw.Reset();
                                            bl.ConnectPackegeToDrone(droneNumber);
                                        }

                                    }
                                    catch (DroneCantMakeDliveryException)
                                    {
                                        lock (bl.dalObj)
                                        {
                                            if (bl.dalObj.PackegeList(x => true).All(x => x.OperatorSkimmerId != 0))
                                                sendToCharge = true;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        
                                    }




                                    break;
                                case BO.DroneStatus.Maintenance:

                                    var buttry = bl.droneChrgingAlredy(sw.ElapsedMilliseconds) + drone.ButrryStatus;
                                    if (buttry >= 100)
                                    {
                                        sw.Stop();
                                        sw.Reset();
                                        bl.FreeDroneFromCharging(drone.SerialNumber, buttry);

                                    }
                                    else
                                    {

                                        bl.dronesListInBl.Find(x => x.SerialNumber == drone.SerialNumber).ButrryStatus = buttry;
                                    }
                                    break;
                                case BO.DroneStatus.Work:
                                    double past = 0;
                                    if (drone.PackageInTransfer.InTheWay)
                                    {

                                        switch (drone.PackageInTransfer.WeightCatgory)
                                        {
                                            case WeightCategories.Easy:
                                                past = (sw.ElapsedMilliseconds * 100) * ((double)SpeedDrone.Easy / 60.0 / 60.0 / 100);
                                                break;
                                            case WeightCategories.Medium:
                                                past = (sw.ElapsedMilliseconds * 100) * ((double)SpeedDrone.Medium / 60.0 / 60.0 / 100);
                                                break;
                                            case WeightCategories.Heavy:
                                                past = (sw.ElapsedMilliseconds * 100) * ((double)SpeedDrone.Heavy / 60.0 / 60.0 / 100);
                                                break;
                                            default:
                                                break;
                                        }

                                        if (past >= drone.PackageInTransfer.Distance)
                                        {
                                            sw.Stop();
                                            sw.Reset();
                                            bl.PackegArrive(droneNumber);
                                        }
                                        else
                                            drone.ButrryStatus -= bl.buttryDownPackegeDelivery(drone.PackageInTransfer, ((sw.ElapsedMilliseconds * 100) - m) * ((double)SpeedDrone.Free / 60.0 / 60.0 / 100));

                                    }
                                    else
                                    {

                                        past = (sw.ElapsedMilliseconds * 100) * ((double)SpeedDrone.Free / 60.0 / 60.0 / 100);

                                        var a = bl.Distans(drone.Location, drone.PackageInTransfer.Source);
                                        if (past >= bl.Distans(drone.Location, drone.PackageInTransfer.Source))
                                        {

                                            sw.Stop();
                                            sw.Reset();
                                            bl.CollectPackegForDelivery(droneNumber, (a - m * ((double)SpeedDrone.Free / 60.0 / 60.0 / 1000)));

                                        }
                                        else
                                        {
                                            drone.ButrryStatus -= bl.buttryDownWithNoPackege(((sw.ElapsedMilliseconds) - m) * ((double)SpeedDrone.Free / 60.0 / 60.0 / 100));
                                            bl.dronesListInBl.Find(x => x.SerialNumber == drone.SerialNumber).ButrryStatus = drone.ButrryStatus;
                                        }
                                    }
                                    m = sw.ElapsedMilliseconds;

                                    break;
                                case BO.DroneStatus.Delete:
                                    break;
                                default:
                                    break;
                            }

                        }
                        action();
                        Thread.Sleep(1000);
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        Thread.Sleep(1000);
                    }

                }).Start();

            }
            catch (Exception) { }
        }
    }

}