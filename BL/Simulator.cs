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
    /// <summary>
    /// Simulator of drone travel
    /// </summary>
    internal class Simulator
    {

        BlApi.BL BL;
        private Thread myThread;
        Stopwatch sw;
        double chargePerMiliSecond;
        private const double SPEED = 500;
        private const double HOUER_TO_MILISECOUND = 60.0 * 60.0 * 1000;
        static Dictionary<uint, uint> keys = new();
        //private const double TIME_STEP = DELAY / 1000.0;
        //private const double STEP = VELOCITY / TIME_STEP;

        public Simulator(BlApi.BL bl, uint droneNumber, Action action, Func<bool> StopChecking)
        {
            sw = new Stopwatch();
            Drone drone;
            bool sendToCharge = false;
            double m = 0,past=0;
            SpeedDrone speed=SpeedDrone.Free;

            try
            {
                BL = bl;


                chargePerMiliSecond = 100 / (bl.chargingPerMinute / 60 * 1000);

                new Thread(() =>
                {
                  try  {
                        if (keys.Any(x => x.Key == droneNumber))
                            throw new DroneTryToStartSecondeSimolatorException(droneNumber);
                        keys.Add(droneNumber, droneNumber);
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
                                           
                                            if ((drone.ButrryStatus <= 20 || sendToCharge) && drone.ButrryStatus < 100)
                                            {
                                                BaseStation base_ = new();
                                                if (drone.LocationNext==LocationNext.None)
                                                base_ = bl.ClosestBase(drone.Location, true);
                                                drone.LocationNext = LocationNext.Base;
                                                
                                                 past = distanse(m, SpeedDrone.Free);
                                                var a = bl.Distans(drone.Location, base_.Location);
                                                if (drone.DistanseToNextLocation -past<=0)
                                                {
                                                    sw.Stop();
                                                    drone.ButrryStatus -= bl.buttryDownWithNoPackege(drone.DistanseToNextLocation);
                                                    sw.Reset();
                                                    
                                                    bl.DroneToCharge(drone.SerialNumber, drone.DistanseToNextLocation);
                                                    drone.DistanseToNextLocation = 0;
                                                }
                                                else
                                                {
                                                    drone.DistanseToNextLocation -= distanse(m, SpeedDrone.Free);
                                                    changeDroneList(bl, drone);
                                                   
                                                }
                                                m = sw.ElapsedMilliseconds;
                                            }
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

                                        var buttry = bl.droneChrgingAlredy(sw.ElapsedMilliseconds - m) + drone.ButrryStatus;
                                        if (buttry >= 100)
                                        {
                                            stopWatch();
                                            bl.FreeDroneFromCharging(drone.SerialNumber, buttry);
                                        }
                                        else
                                        {
                                            drone.ButrryStatus = buttry;
                                            changeDroneList(bl, drone);
                                        }
                                        break;
                                    case BO.DroneStatus.Work:
                                     
                                        if (drone.PackageInTransfer.InTheWay)
                                        {

                                            switch (drone.PackageInTransfer.WeightCatgory)
                                            {
                                                case WeightCategories.Easy:
                                              
                                                    past= distanse(m, SpeedDrone.Easy);
                                                    speed = SpeedDrone.Easy;
                                                    break;
                                                case WeightCategories.Medium:
                                           
                                                   past= distanse(m, SpeedDrone.Medium);
                                                    speed = SpeedDrone.Medium;
                                                    break;
                                                case WeightCategories.Heavy:
                                        
                                                   past= distanse(m, SpeedDrone.Heavy);
                                                    speed = SpeedDrone.Heavy;
                                                    break;
                                                default:
                                                    break;
                                            }

                                            if (drone.DistanseToNextLocation-past <= 0)
                                            {
                                                stopWatch();
                                                bl.PackegArrive(droneNumber, drone.DistanseToNextLocation);
                                                drone.DistanseToNextLocation = 0;
                                            }
                                            else
                                            {
                                                drone.ButrryStatus -= bl.buttryDownPackegeDelivery(drone.PackageInTransfer, distanse(m, speed));
                                                drone.DistanseToNextLocation -= past;
                                                changeDroneList(bl, drone);
                                            }
                                        }
                                        else
                                        {

                                            past = distanse(m, SpeedDrone.Free);

                                            var a = bl.Distans(drone.Location, drone.PackageInTransfer.Source);
                                            if (drone.DistanseToNextLocation-past<=0)
                                            {
                                                stopWatch();
                                             
                                                bl.CollectPackegForDelivery(droneNumber, drone.DistanseToNextLocation);
                                                drone.DistanseToNextLocation = 0;
                                            }
                                            else
                                            {
                                                drone.ButrryStatus -= bl.buttryDownWithNoPackege(distanse(m, SpeedDrone.Free));
                                                drone.DistanseToNextLocation -= past;
                                               changeDroneList(bl, drone);
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
                            Thread.Sleep(500);
                        }
                        keys.Remove(droneNumber);
                        for (int i = 0; i < 10; i++)
                        {
                            Thread.Sleep(500);
                        }
                    }
                    catch (ItemNotFoundException)
                    { keys.Remove(droneNumber); }
                    catch (DroneTryToStartSecondeSimolatorException)
                    {  }
                    catch (Exception) { }
                }).Start();

            }
            catch (ItemNotFoundException)
            { keys.Remove(droneNumber); }
            catch (DroneTryToStartSecondeSimolatorException)
            { }
            catch (Exception) { }
        }

       
        /// <summary>
        /// update drone list
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="drone"></param>
        private  void changeDroneList(BlApi.BL bl, Drone drone)
        {
            var droneInList = bl.dronesListInBl.Find(x => x.SerialNumber == drone.SerialNumber);
            droneInList.ButrryStatus = drone.ButrryStatus;
            droneInList.DistanseToNextLocation = drone.DistanseToNextLocation;
            droneInList.LocationNext = drone.LocationNext;
            droneInList.LocationName = drone.LocationName;
        }

       

          /// <summary>
        /// Calculate the distance the drone did to the charge
        /// </summary>
        /// <param name="m"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        private double distanse(double m, SpeedDrone speed)
        {
            return (sw.ElapsedMilliseconds - m)*SPEED * (((double)speed / HOUER_TO_MILISECOUND));
        }

        /// <summary>
        /// Stop the timer, and reset him
        /// </summary>
        private void stopWatch()
        {
            sw.Stop();
            sw.Reset();
        }
    }

}