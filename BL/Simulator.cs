﻿using System;
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
    /// <summary>
    /// Simulator of drone travel
    /// </summary>
    internal class Simulator
    {

        BlApi.BL BL;
        private Thread myThread;
        Stopwatch sw;
        double chargePerMiliSecond;
        private const double SPEED = 1.0;
        private const int DELAY = 1000;
        
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
                                        //If there if not enough battery to take a package
                                        if ((drone.ButrryStatus < 20 || sendToCharge) && drone.ButrryStatus < 100)
                                        {
                                            var base_ = bl.ClosestBase(drone.Location, true);
                                            var path = distanseToButtry(m, SpeedDrone.Free);
                                            var a = bl.Distans(drone.Location, base_.Location);
                                            if (path <= a)
                                            {
                                                drone.ButrryStatus -= bl.buttryDownWithNoPackege(endTravel(m, a));
                                                stopWatch();
                                                bl.DroneToCharge(drone.SerialNumber);
                                            }
                                            else
                                            {
                                                changebuttry(bl, drone);
                                            }
                                            m = sw.ElapsedMilliseconds;
                                        }
                                        // else , take a package
                                        else
                                        {
                                            stopWatch();
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
                                        stopWatch();
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
                                                past = clculetPast(SpeedDrone.Easy);
                                                break;
                                            case WeightCategories.Medium:
                                                past = clculetPast(SpeedDrone.Medium);
                                                break;
                                            case WeightCategories.Heavy:
                                                past = clculetPast(SpeedDrone.Heavy);
                                                break;
                                            default:
                                                break;
                                        }

                                        if (past >= drone.PackageInTransfer.Distance)
                                        {
                                            stopWatch();
                                            bl.PackegArrive(droneNumber);
                                        }
                                        else
                                            drone.ButrryStatus -= bl.buttryDownPackegeDelivery(drone.PackageInTransfer, distanseToButtry(m, SpeedDrone.Free));

                                    }
                                    else
                                    {

                                        past = clculetPast(SpeedDrone.Free);

                                        var a = bl.Distans(drone.Location, drone.PackageInTransfer.Source);
                                        if (past >= bl.Distans(drone.Location, drone.PackageInTransfer.Source))
                                        {
                                            stopWatch();
                                            bl.CollectPackegForDelivery(droneNumber, endTravel(m, a));

                                        }
                                        else
                                        {
                                            drone.ButrryStatus -= bl.buttryDownWithNoPackege(distanseToButtry(m, SpeedDrone.Free));
                                            changebuttry(bl, drone);
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

        private static double endTravel(double m, double a)
        {
            return a - (m) * ((double)SpeedDrone.Free / 60.0 / 60.0 / 100);
        }

        private static void changebuttry(BlApi.BL bl, Drone drone)
        {
            bl.dronesListInBl.Find(x => x.SerialNumber == drone.SerialNumber).ButrryStatus = drone.ButrryStatus;
        }

        private double clculetPast(SpeedDrone speed)
        {
            return (sw.ElapsedMilliseconds * 100) * ((double)speed / 60.0 / 60.0 / 100);
        }

        private double distanseToButtry(double m, SpeedDrone speed)
        {
            return ((sw.ElapsedMilliseconds * 100) - m) * ((double)speed / 60.0 / 60.0 / 100);
        }

        private void stopWatch()
        {
            sw.Stop();
            sw.Reset();
        }
    }

}