using System;
using System.Collections;
using IDAL;
using DalObject;

namespace ConsoleUI
{

    class Program
    {
        // enumes for the menu options
        enum Options { Exit, Add, Update, ShowDetails, ShowList }
        enum Entities { Exit, Client, Base_station, Drone, Package }
        enum UpdatesOptions { Exit, Associate, Collect, Delivery, Charge, UnCharge }
        enum Show { Exit, Client, Base_station, Drone, Package, ShowDistance,ShoeDegree }
        enum ShowList { Exit, Base_station, Drones, Clients, Package, FreePackage, FreeBaseStation }
        enum Distans_2_point { base_station = 1, client }

        // function that which allows us to receive a number from the user safely
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
        /// 
        /// </Menu>
        private static void Menu()
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
                string str, name, phon;
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
                                    Console.Write("Enter ID:");
                                    do
                                    {
                                        check = int.TryParse(Console.ReadLine(), out num);
                                    } while (!check);
                                    Console.Write("Enter name:");
                                    name = Console.ReadLine();
                                    Console.Write("Enter phone number:");
                                    phon = Console.ReadLine();
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
                                    DalObject.DalObject.Add_client(num, name, phon, doubleNum1, doubleNum2); // add new client
                                    break;
                                }
                            case Entities.Base_station:
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
                                    DalObject.DalObject.Add_station(id, name, num1, doubleNum1, doubleNum2); // add new Base station
                                    break;
                                }
                            case Entities.Drone:
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
                                    DalObject.DalObject.Add_drone(id, name, num, doubleNum1, num1); //add new drone
                                    break;
                                }
                            case Entities.Package:
                                {
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
                                    Console.Write("Enter Weight categories 0 for easy,1 for mediom,3 for haevy:");
                                    do
                                    {
                                        check = int.TryParse(Console.ReadLine(), out num2);
                                    } while (!check);
                                    Console.Write("Enter priority 0 for Immediate ,1 for quick ,2 for Regular:");
                                    do
                                    {
                                        check = int.TryParse(Console.ReadLine(), out num);
                                    } while (!check);
                                    DalObject.DalObject.Add_package(id, num1, num2, num); // add new package
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

                        switch (updatesOption)
                        {
                            //option to connect package to drone
                            case UpdatesOptions.Associate:
                                Console.Write("Enter package number:");
                                do
                                {
                                    check = int.TryParse(Console.ReadLine(), out num);
                                } while (!check);
                                DalObject.DalObject.connect_package_to_drone(num);
                                break;

                            // update that the package is collected
                            case UpdatesOptions.Collect:
                                Console.Write("Enter package number:");
                                do
                                {
                                    check = int.TryParse(Console.ReadLine(), out num);
                                } while (!check);
                                DalObject.DalObject.Package_collected(num);
                                break;

                            // update that the package is arrived to the target
                            case UpdatesOptions.Delivery:
                                Console.Write("Enter package number:");
                                do
                                {
                                    check = int.TryParse(Console.ReadLine(), out num);
                                } while (!check);
                                DalObject.DalObject.Package_arrived(num);
                                break;

                            //sent drone to a free charging station
                            case UpdatesOptions.Charge:
                                Console.WriteLine("Choose a base number from the following Base station:");
                                DalObject.DalObject.Base_station_list_with_free_charge_states(backList);
                                for (int i = 0; i < backList.Count; i++)
                                {
                                    Console.WriteLine(backList[i]);
                                }
                                int baseStation;
                                bool success;

                                // recived a number safely
                                do
                                {
                                    success = int.TryParse(Console.ReadLine(), out baseStation);
                                    if (!success)
                                        Console.WriteLine("Error! Choose again");
                                } while (!success);
                                Console.Write("Enter drone number:");
                                check = int.TryParse(Console.ReadLine(), out num);
                                DalObject.DalObject.send_drone_to_charge(baseStation, num);
                                break;

                            // Release drone from charging position
                            case UpdatesOptions.UnCharge:
                                Console.Write("Enter drone number:");
                                check = int.TryParse(Console.ReadLine(), out num);
                                DalObject.DalObject.free_drone_from_charge(num);
                                break;
                            case UpdatesOptions.Exit:
                                break;
                        }
                        break;

                    case Options.ShowDetails:
                        str = "Choose one of the following view option:\n " +
                             "1-Client,\n 2-Base station,\n 3- Drone,\n 4- Package\n 5-Distance";
                        num = getChoose(str);
                        show = (Show)num;

                        switch (show)
                        {
                            case Show.Client:
                                Console.Write("Enter ID:");
                                check = int.TryParse(Console.ReadLine(), out num);
                                Console.WriteLine( DalObject.DalObject.cilent_by_number(num));
                                break;
                            case Show.Base_station:
                                Console.Write("Enter base number:");
                                check = int.TryParse(Console.ReadLine(), out num);
                                Console.WriteLine(DalObject.DalObject.Base_station_by_number(num));
                                break;
                            case Show.Drone:
                                Console.Write("Enter drone number:");
                                check = int.TryParse(Console.ReadLine(), out num);
                                Console.WriteLine( DalObject.DalObject.Drone_by_number(num));
                                break;
                            case Show.Package:
                                Console.Write("Enter packege number:");
                                check = int.TryParse(Console.ReadLine(), out num);
                                Console.WriteLine(DalObject.DalObject.packege_by_number(num));
                                break;
                            case Show.ShowDistance:
                                {
                                    num1 = 0;
                                    double[] points = new double[4];
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

                                    Console.WriteLine( DalObject.DalObject.Distance(doubleNum2, doubleNum1, point2, point1));

                                    break;
                                }
                            case Show.ShoeDegree:
                                Console.Write("Enter a longitude or latitude to ge it in degree :");
                                do
                                {
                                    check = double.TryParse(Console.ReadLine(), out point2);
                                } while (!check);
                                Console.WriteLine( DalObject.DalObject.Point_to_degree(point2));
                                break;
                            case Show.Exit:
                                break;
                        }
                        break;
                    case Options.ShowList:
                        str = "Choose one of the following option:\n" +
                             " 1-Base stations,\n 2- Drones,\n 3-Clients,\n" +
                             " 4- Packages\n 5-packege with no drone,\n 6- Base station with free charge states";
                        num = getChoose(str);
                        showList = (ShowList)num;
                        switch (showList)
                        {
                            case ShowList.Base_station:
                                backList.Clear();
                                DalObject.DalObject.Base_station_list(backList);
                                for (int i = 0; i < backList.Count; i++)
                                {
                                    Console.WriteLine(backList[i]);
                                }
                                break;
                            case ShowList.Drones:
                                backList.Clear();
                                DalObject.DalObject.Drone_list(backList);
                                for (int i = 0; i < backList.Count; i++)
                                {
                                    Console.WriteLine(backList[i]);
                                }
                                break;
                            case ShowList.Clients:
                                backList.Clear();
                                DalObject.DalObject.cilent_list(backList);
                                for (int i = 0; i < backList.Count; i++)
                                {
                                    Console.WriteLine(backList[i]);
                                }
                                break;
                            case ShowList.Package:
                                backList.Clear();
                                DalObject.DalObject.packege_list(backList);
                                for (int i = 0; i < backList.Count; i++)
                                {
                                    Console.WriteLine(backList[i]);
                                }
                                break;
                            case ShowList.FreePackage:
                                backList.Clear();
                                DalObject.DalObject.packege_list_with_no_drone(backList);
                                for (int i = 0; i < backList.Count; i++)
                                {
                                    Console.WriteLine(backList[i]);
                                }
                                break;
                            case ShowList.FreeBaseStation:
                                backList.Clear();
                                DalObject.DalObject.Base_station_list_with_free_charge_states(backList);
                                for (int i = 0; i < backList.Count; i++)
                                {
                                    Console.WriteLine(backList[i]);
                                }
                                break;
                            case ShowList.Exit:
                                break;
                        }
                        break;
                    case Options.Exit:
                        break;
                }

            } while (option != Options.Exit);

        }




        static void Main(string[] args)
        {

            new DalObject.DalObject();
            Menu();
        }
    }
}
