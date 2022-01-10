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
            BL = bl;
            
                sw = new Stopwatch();
                Drone drone ;
                uint? packageSerialNum = null;
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

                                    /*  var packlist = from item in bl.PackageWithNoDroneToLists()
                                                     where ((BO.WeightCategories)item.WeightCategories <= drone.WeightCategory)
                                                         && (bl.buttryDownPackegeDelivery(bl.convertPackegeBlToPackegeInTrnansfer(bl.ShowPackage(item.SerialNumber))) < drone.ButrryStatus)
                                                     orderby item.priority descending, item.WeightCategories
                                                     select item;
                                      if (packlist.FirstOrDefault() != null)
                                      {
                                          packageSerialNum = packlist.FirstOrDefault().SerialNumber;
                                      }*/

                                    if (drone.ButrryStatus < 20)
                                    {
                                        var base_ = bl.ClosestBase(drone.Location, true);
                                        var path = sw.ElapsedMilliseconds * (double)SpeedDrone.Free * (60.0 * 60.0 * 1000);
                                        if (path <= bl.Distans(drone.Location, base_.Location))
                                        {
                                            drone.ButrryStatus -= bl.buttryDownWithNoPackege(drone.Location, base_.Location);
                                            bl.DroneToCharge(drone.SerialNumber);
                                        }
                                        else
                                        {

                                            bl.dronesListInBl.Find(x => x.SerialNumber == drone.SerialNumber).ButrryStatus = drone.ButrryStatus;
                                        }
                                    }
                                    else
                                        try
                                        {
                                           
                                            bl.ConnectPackegeToDrone(droneNumber);
                                        }
                                        catch(Exception)
                                        {

                                        }




                                    break;
                                case BO.DroneStatus.Maintenance:

                                    var buttry = bl.droneChrgingAlredy(sw.ElapsedMilliseconds) + drone.ButrryStatus;
                                    if (buttry >= 100)
                                    {
                                        sw.Stop();
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
                                                past = (sw.ElapsedMilliseconds ) * (double)SpeedDrone.Easy / (60.0 / 60.0/1000);
                                                break;
                                            case WeightCategories.Medium:
                                                past = (sw.ElapsedMilliseconds) * (double)SpeedDrone.Medium / (60.0 * 60.0 * 500);
                                                break;
                                            case WeightCategories.Heavy:
                                                past = (sw.ElapsedMilliseconds ) * (double)SpeedDrone.Heavy / (60.0 * 60.0 * 500);
                                                break;
                                            default:
                                                break;
                                        }
                                        drone.ButrryStatus -= bl.buttryDownPackegeDelivery(drone.PackageInTransfer, past);
                                        if (past >= drone.PackageInTransfer.Distance)
                                        {
                                            sw.Stop();
                                            bl.PackegArrive(droneNumber);
                                        }
                                        
                                    }
                                    else
                                    {
                                        
                                        past = (sw.ElapsedMilliseconds) * (double)SpeedDrone.Free / 60.0/ 60.0/ 1000.0;
                                        
                                        
                                        if (past >= bl.Distans(drone.Location, drone.PackageInTransfer.Source))
                                        {
                                            sw.Stop();
                                            bl.CollectPackegForDelivery(droneNumber);
                                        }
                                        else
                                            drone.ButrryStatus -= bl.buttryDownWithNoPackege( past);
                                    }
                                    bl.dronesListInBl.Find(x => x.SerialNumber == drone.SerialNumber).ButrryStatus = drone.ButrryStatus;
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
    }

    /* internal class SimulatorBackGround
     {
         Stopwatch sw;
         BackgroundWorker bw;
         Drone drone;
         BlApi.BL bl;
         Action action;
         Func<bool> func;
         double chargePerMiliSec;

         public SimulatorBackGround(BlApi.BL bL, uint sirial, Action action, Func<bool> func)
         {
             bl = bL;
             sw = new Stopwatch();
             bw = new BackgroundWorker();
             drone = bl.GetDrone(sirial);
             bw.WorkerReportsProgress = true;
             bw.ProgressChanged += Bw_ProgressChanged;
             bw.DoWork += Bw_DoWork;

         }

         private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
         {

         }

         private void Bw_DoWork(object sender, DoWorkEventArgs e)
         {

             switch (drone.DroneStatus)
             {
                 case DroneStatus.Free:
                     if (drone.ButrryStatus < 20)
                     { droneToCharge(); }
                     else
                     { droneToDelvery(); }
                     break;
                 case DroneStatus.Maintenance:
                     break;
                 case DroneStatus.Work:
                     break;

                 default:
                     break;
             }
         }

         private void droneToCharge()
         {
             try
             {
                 bl.DroneToCharge(drone.SerialNumber);
                 sw.Start();
             }
             catch (Exception)
             { }
         }

         private void droneToDelvery()
         {
             try
             {
                 bl.ConnectPackegeToDrone(drone.SerialNumber);
             }
             catch (Exception)
             { }
         }
     }*/
}
