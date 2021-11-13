/*
 we add the bonus function
 */

using System;
using System.Collections;
using System.Collections.Generic;
using IDAL;
using DalObject;

namespace ConsoleUI
{
    partial class Program
    {
        // enumes for the menu options
        enum Options { Exit, Add, Update, ShowDetails, ShowList }
        enum Entities { Exit, Client, Base_station, Drone, Package }
        enum UpdatesOptions { Exit, Associate, Collect, Delivery, Charge, UnCharge }
        enum Show { Exit, Client, Base_station, Drone, Package, ShowDistance, ShoeDegree }
        enum ShowList { Exit, Base_station, Drones, Clients, Package, FreePackage, FreeBaseStation }
        enum Distans_2_point { base_station = 1, client }

        /// <summary>
        ///  function which allows us to receive 
        ///  a number from the user safely
        /// </summary>
        private static int getChoose(string val)
        {
            bool success;
            int number;
            do
            {
                Console.WriteLine(val);
                success = int.TryParse(Console.ReadLine(), out number);
                if (!success)
                    Console.WriteLine("Error!\n");
            }
            while (!success);
            return number;
        }

        /// <Menu>
        /// Selection menu that which show to the customer 
        /// when opening the program 
        /// </Menu>
        private static void Menu(DalObject.DalObject dalObject)
        {
            Options option;
            Entities entity;
            UpdatesOptions updatesOption;
            Show show;
            ShowList showList;

            do
            {
                bool check;
                int num, id, num1, num2;
                double doubleNum1, doubleNum2, point1, point2;
                string str, name, phone;
                ArrayList backList = new ArrayList();

                str = "Choose one of the following:\n" +
                    " 1-Add,\n 2-Update,\n 3-Show Details,\n 4-Show List,\n 0-Exit";

                num = getChoose(str);
                option = (Options)num;

                switch (option)
                {
                    case Options.Add:

                        str = "Choose an entity:\n " +
                             "1-Client,\n 2-Base station,\n 3- Drone,\n 4- Package";
                        num = getChoose(str);
                        entity = (Entities)num;

                        switch (entity)
                        {
                            case Entities.Client:
                                {
                                    //received the details from the user
                                    try
                                    {
                                        addClient(dalObject, out check, out num, out doubleNum1, out doubleNum2, out name, out phone);
                                    }
                                    catch (IDAL.DO.Item_found_exception exception)
                                    {
                                        Console.WriteLine(exception);
                                    }
                                    break;
                                }
                            case Entities.Base_station:
                                {
                                    //received the details from the user
                                    try
                                    {
                                        addBase(dalObject, out check, out id, out num1, out doubleNum1, out doubleNum2, out name);
                                    }
                                    catch (IDAL.DO.Item_found_exception exception)
                                    {
                                        Console.WriteLine(exception);
                                    }
                                    break;
                                }
                            case Entities.Drone:
                                {
                                    //received the details from the user
                                    try
                                    {
                                        addDrone(dalObject, out check, out num, out id, out num1, out doubleNum1, out name);
                                    }
                                    catch (IDAL.DO.Item_found_exception exception)
                                    {
                                        Console.WriteLine(exception);
                                    }
                                    break;
                                }
                            case Entities.Package:
                                {
                                    try
                                    {
                                        addPackage(dalObject, out check, out num, out id, out num1, out num2);
                                    }
                                    catch (IDAL.DO.Item_found_exception exception)
                                    {
                                        Console.WriteLine(exception);
                                    }
                                    break;
                                }
                            case Entities.Exit:
                                break;
                        }
                        break;


                    case Options.Update:
                        str = "Choose one of the following updates:\n " +
                            "1-Associate a package,\n 2-collect a package by drone,\n" +
                            " 3- Delivering a package to the customer,\n" +
                            " 4- Send drone to Charge\n 5- free drone from charge";
                        num = getChoose(str);
                        updatesOption = (UpdatesOptions)num;
                        try
                        {
                            switch (updatesOption)
                            {


                                //option to connect package to drone
                                case UpdatesOptions.Associate:

                                    updateAssociate(dalObject, out check, out num, out num1);
                                    break;

                                // update that the package is collected
                                case UpdatesOptions.Collect:

                                    //received the details from the user
                                    updateCollect(dalObject, out check, out num);
                                    break;

                                // update that the package is arrived to the target
                                case UpdatesOptions.Delivery:

                                    updateDelivery(dalObject, out check, out num);
                                    break;
                                /*
                                                            //sent drone to a free charging station
                                                            case UpdatesOptions.Charge:

                                                                updateCharge( dalObject, out check, out num, backList);
                                                                break;

                                                            // Release drone from charging position
                                                            case UpdatesOptions.UnCharge:

                                                                releaseDrone( dalObject, out check, out num);
                                                                break;
                                                                */
                                case UpdatesOptions.Exit:
                                    break;


                            }
                        }
                        catch(IDAL.DO.Item_not_found_exception exception)
                        {
                            
                            Console.WriteLine(exception);
                        }
                        break;

                    case Options.ShowDetails:
                        str = "Choose one of the following view option:\n " +
                             "1-Client,\n 2-Base station,\n 3- Drone,\n 4- Package\n 5-Distance";
                        num = getChoose(str);
                        show = (Show)num;
                        try
                        {
                            switch (show)
                            {
                                case Show.Client:

                                    clientById(dalObject, out check, out num);
                                    break;
                                case Show.Base_station:

                                    baseByNumber(dalObject, out check, out num);
                                    break;
                                case Show.Drone:
                                    droneBySirialNumber(dalObject, out check, out num);
                                    break;
                                case Show.Package:
                                    packageByNumber(dalObject, out check, out num);
                                    break;

                                //option to show distance between two points
                                case Show.ShowDistance:
                                    {
                                        distanceBetween2points(dalObject, out check, out num1, out doubleNum1, out doubleNum2, out point1, out point2);

                                        break;
                                    }
                                case Show.ShoeDegree:
                                    pointToDegree(dalObject, out check, out point2);
                                    break;
                                case Show.Exit:
                                    break;
                            }
                        }
                        catch (IDAL.DO.Item_not_found_exception exception)
                        {

                            Console.WriteLine(exception);
                        }
                        break;
                    case Options.ShowList:
                        str = "Choose one of the following option:\n" +
                             " 1-Base stations,\n 2- Drones,\n 3-Clients,\n" +
                             " 4- Packages\n 5-packege with no drone,\n 6- Base station with free charge states";
                        num = getChoose(str);
                        showList = (ShowList)num;
                        try
                        {
                            switch (showList)
                            {
                                case ShowList.Base_station:
                                    listOfBass(dalObject);
                                    break;
                                case ShowList.Drones:
                                    listOfDrone(dalObject);
                                    break;
                                case ShowList.Clients:
                                    listOfClinet(dalObject);
                                    break;
                                case ShowList.Package:
                                    listOfPackage(dalObject);
                                    break;
                                case ShowList.FreePackage:
                                    packegeWhitNoDrone(dalObject);
                                    break;
                                case ShowList.FreeBaseStation:
                                    baseWhitFreeChargeStation(dalObject);
                                    break;
                                case ShowList.Exit:
                                    break;
                            }
                        }
                        catch (IDAL.DO.Item_not_found_exception exception)
                        {

                            Console.WriteLine(exception);
                        }
                        break;


                    case Options.Exit:
                        break;

                }
            } while (option != Options.Exit);



            void baseWhitFreeChargeStation(DalObject.DalObject dalObject)
            {

                foreach (IDAL.DO.Base_Station print in dalObject.Base_station_list_with_charge_states())
                    Console.WriteLine(print);
            }

             void packegeWhitNoDrone(DalObject.DalObject dalObject)
            {

                foreach (IDAL.DO.Package print in dalObject.packege_list_with_no_drone())
                    Console.WriteLine(print);
            }

             void listOfPackage(DalObject.DalObject dalObject)
            {

                foreach (IDAL.DO.Package print in dalObject.packege_list())
                    Console.WriteLine(print);
            }

             void listOfClinet(DalObject.DalObject dalObject)
            {

                foreach (IDAL.DO.Client print in dalObject.cilent_list())
                    Console.WriteLine(print);
            }

            void listOfDrone(DalObject.DalObject dalObject)
            {

                List<IDAL.DO.Drone> list = (List<IDAL.DO.Drone>)dalObject.Drone_list();
                foreach (IDAL.DO.Drone print in list)
                    Console.WriteLine(print);
            }

            void listOfBass(DalObject.DalObject dalObject)
            {


                foreach (IDAL.DO.Base_Station print in dalObject.Base_station_list())
                    Console.WriteLine(print);


            }

           void pointToDegree(DalObject.DalObject dalObject, out bool check, out double point2)
            {
                Console.Write("Enter a longitude or latitude to ge it in degree :");
                do
                {
                    check = double.TryParse(Console.ReadLine(), out point2);
                } while (!check);
                Console.WriteLine(dalObject.Point_to_degree(point2));
            }

            void distanceBetween2points(DalObject.DalObject dalObject, out bool check, out int num1, out double doubleNum1, out double doubleNum2, out double point1, out double point2)
            {
                num1 = 0;
                double[] points = new double[4];

                //received the details from the user
                Console.Write("Insert the latitude of the first point:");
                do
                {
                    check = double.TryParse(Console.ReadLine(), out doubleNum1);
                } while (!check);
                Console.Write("Enter a longitude of the first point:");
                do
                {
                    check = double.TryParse(Console.ReadLine(), out doubleNum2);
                } while (!check);
                Console.Write("Enter a latitude of the second point:");
                do
                {
                    check = double.TryParse(Console.ReadLine(), out point1);
                } while (!check);
                Console.Write("Enter a longitude of the second point:");
                do
                {
                    check = double.TryParse(Console.ReadLine(), out point2);
                } while (!check);

                Console.WriteLine($"the distance is: {0}KM",dalObject.Distance(doubleNum2, doubleNum1, point2, point1));
            }

            void packageByNumber(DalObject.DalObject dalObject, out bool check, out int num)
            {
                Console.Write("Enter packege number:");
                check = int.TryParse(Console.ReadLine(), out num);
                Console.WriteLine(dalObject.packege_by_number(num));
            }

             void droneBySirialNumber(DalObject.DalObject dalObject, out bool check, out int num)
            {
                Console.Write("Enter drone number:");
                check = int.TryParse(Console.ReadLine(), out num);
                Console.WriteLine(dalObject.Drone_by_number(num));
            }

           void baseByNumber(DalObject.DalObject dalObject, out bool check, out int num)
            {
                //received the details from the user
                Console.Write("Enter base number:");
                check = int.TryParse(Console.ReadLine(), out num);
                Console.WriteLine(dalObject.Base_station_by_number(num));
            }

            void clientById(DalObject.DalObject dalObject, out bool check, out int num)
            {
                //received the details from the user
                Console.Write("Enter ID:");
                check = int.TryParse(Console.ReadLine(), out num);
                Console.WriteLine(dalObject.cilent_by_number(num));
            }

          
            void updateDelivery(DalObject.DalObject dalObject, out bool check, out int num)
            {
                //received the details from the user
                Console.Write("Enter package number:");
                do
                {
                    check = int.TryParse(Console.ReadLine(), out num);
                } while (!check);
                dalObject.Package_arrived(num);
            }

             void updateCollect(DalObject.DalObject dalObject, out bool check, out int num)
            {
                Console.Write("Enter package number:");

                do
                {
                    check = int.TryParse(Console.ReadLine(), out num);
                } while (!check);
                dalObject.Package_collected(num);
            }

            void updateAssociate(DalObject.DalObject dalObject, out bool check, out int num, out int num1)
            {
                //received the details from the user
                Console.Write("Enter package number:");
                do
                {
                    check = int.TryParse(Console.ReadLine(), out num);
                } while (!check);
                Console.Write("Enter drone number:");
                do
                {
                    check = int.TryParse(Console.ReadLine(), out num1);
                } while (!check);
                dalObject.connect_package_to_drone(num, num1);
            }

           void addPackage(DalObject.DalObject dalObject, out bool check, out int num, out int id, out int num1, out int num2)
            {
                //received the details from the user

                Console.Write("Enter ID of the sender:");
                do
                {
                    check = int.TryParse(Console.ReadLine(), out id);
                } while (!check);
                Console.Write("Enter ID of the recipient:");
                do
                {
                    check = int.TryParse(Console.ReadLine(), out num1);
                } while (!check);
                Console.Write("Enter Weight categories 0 for easy,1 for mediom,2 for haevy:");
                do
                {
                    check = int.TryParse(Console.ReadLine(), out num2);
                } while (!check);
                Console.Write("Enter priority 0 for Immediate ,1 for quick ,2 for Regular:");
                do
                {
                    check = int.TryParse(Console.ReadLine(), out num);
                } while (!check);
                // add new package
                dalObject.Add_package(id, num1, num2, num);
            }

            void addDrone(DalObject.DalObject dalObject, out bool check, out int num, out int id, out int num1, out double doubleNum1, out string name)
            {
                Console.Write("Enter sireal number:");
                do
                {
                    check = int.TryParse(Console.ReadLine(), out id);
                } while (!check);
                Console.Write("Enter name:");
                name = Console.ReadLine();
                Console.Write("Enter weight Category:0 for easy,1 for mediom,2 for heavy:");
                do
                {
                    check = int.TryParse(Console.ReadLine(), out num);
                } while (!check);
                Console.Write("Enter amount of butrry:");
                do
                {
                    check = double.TryParse(Console.ReadLine(), out doubleNum1);
                } while (!check);
                Console.Write("Enter a statos:0 for free,1 for maintenance:");
                do
                {
                    check = int.TryParse(Console.ReadLine(), out num1);
                } while (!check);

                //add new drone
                dalObject.Add_drone(id, name, num, doubleNum1, num1);
            }

            void addBase(DalObject.DalObject dalObject, out bool check, out int id, out int num1, out double doubleNum1, out double doubleNum2, out string name)
            {
                Console.Write("Enter base number:");
                do
                {
                    check = int.TryParse(Console.ReadLine(), out id);
                } while (!check);
                Console.Write("Enter base name:");
                name = Console.ReadLine();
                Console.Write("Enter Number of charging stations:");
                do
                {
                    check = int.TryParse(Console.ReadLine(), out num1);
                } while (!check);
                Console.Write("Enter latitude:");
                do
                {
                    check = double.TryParse(Console.ReadLine(), out doubleNum1);
                } while (!check);
                Console.Write("Enter longitude:");
                do
                {

                    check = double.TryParse(Console.ReadLine(), out doubleNum2);
                } while (!check);

                // add new Base station
                dalObject.Add_station(id, name, num1, doubleNum1, doubleNum2);
            }

             void addClient(DalObject.DalObject dalObject, out bool check, out int num, out double doubleNum1, out double doubleNum2, out string name, out string phone)
            {
                Console.Write("Enter ID:");
                do
                {
                    check = int.TryParse(Console.ReadLine(), out num);
                } while (!check);
                Console.Write("Enter name:");
                name = Console.ReadLine();
                Console.Write("Enter phone number:");
                phone = Console.ReadLine();
                Console.Write("Enter latitude:");
                do
                {
                    check = double.TryParse(Console.ReadLine(), out doubleNum1);
                } while (!check);
                Console.Write("Enter londitude:");
                do
                {
                    check = double.TryParse(Console.ReadLine(), out doubleNum2);
                } while (!check);

                // add new client
                dalObject.Add_client(num, name, phone, doubleNum1, doubleNum2);
            }



            static void Main(string[] args)
            {

                DalObject.DalObject dalObject = new DalObject.DalObject();

                Menu(dalObject);
            }
        }
    }
}
