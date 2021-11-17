using System;
using System.Collections;
using BL;
using IBL.BO;
using IBL;

namespace ConsoleUI_BL
{
    class Program
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
        private static uint getChoose(string str)
        {
            bool success;
            uint number;
            do
            {
                Console.WriteLine(str);
                success = uint.TryParse(Console.ReadLine(), out number);
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
        private static void Menu(BL.BL bl)
        {
            if (bl is null)
            {
                throw new ArgumentNullException(nameof(bl));
            }

            Options option;
            Entities entity;
            UpdatesOptions updatesOption;
            Show show;
            ShowList showList;

            do
            {
                bool check;
                uint num, id, num1, num2;
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
                            case Entities.Base_station:
                                {
                                    //received the details from the user
                                    try
                                    {
                                        Add_Base(bl, out check, out id, out num1, out doubleNum1, out doubleNum2, out name);
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
                                        Add_Drone(bl, out check, out num, out id, out num1, out doubleNum1, out name);
                                    }
                                    catch (IDAL.DO.Item_found_exception exception)
                                    {
                                        Console.WriteLine(exception);
                                    }
                                    break;
                                }
                            case Entities.Client:
                                {
                                    //received the details from the user
                                    try
                                    {
                                        Add_Client(bl, out check, out num, out doubleNum1, out doubleNum2, out name, out phone);
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
                                        addPackage(bl, out check, out num, out id, out num1, out num2);
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

                                    updateAssociate(bl, out check, out num, out num1);
                                    break;

                                // update that the package is collected
                                case UpdatesOptions.Collect:

                                    //received the details from the user
                                    updateCollect(bl, out check, out num);
                                    break;

                                // update that the package is arrived to the target
                                case UpdatesOptions.Delivery:

                                    updateDelivery(bl, out check, out num);
                                    break;

                                //sent drone to a free charging station
                                case UpdatesOptions.Charge:

                                    updateCharge(bl, out check, out num, backList);
                                    break;

                                // Release drone from charging position
                                case UpdatesOptions.UnCharge:

                                    releaseDrone(bl, out check, out num);
                                    break;

                                case UpdatesOptions.Exit:
                                    break;


                            }
                        }
                        catch (IDAL.DO.Item_not_found_exception exception)
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

                                    clientById(bl, out check, out num);
                                    break;
                                case Show.Base_station:

                                    baseByNumber(bl, out check, out num);
                                    break;
                                case Show.Drone:
                                    droneBySirialNumber(bl, out check, out num);
                                    break;
                                case Show.Package:
                                    packageByNumber(bl, out check, out num);
                                    break;

                                //option to show distance between two points
                                case Show.ShowDistance:
                                    {
                                        distanceBetween2points(bl, out check, out num1, out doubleNum1, out doubleNum2, out point1, out point2);

                                        break;
                                    }
                                case Show.ShoeDegree:
                                    pointToDegree(bl, out check, out point2);
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
                                    listOfBass(bl);
                                    break;
                                case ShowList.Drones:
                                    listOfDrone(bl);
                                    break;
                                case ShowList.Clients:
                                    listOfClinet(bl);
                                    break;
                                case ShowList.Package:
                                    listOfPackage(bl);
                                    break;
                                case ShowList.FreePackage:
                                    packegeWhitNoDrone(bl);
                                    break;
                                case ShowList.FreeBaseStation:
                                    baseWhitFreeChargeStation(bl);
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



            void baseWhitFreeChargeStation(BL.BL bl)
            {

                foreach (IDAL.DO.Base_Station print in bl.Base_station_list_with_charge_states())
                    Console.WriteLine(print);
            }

            void packegeWhitNoDrone(BL.BL bl)
            {

                foreach (IDAL.DO.Package print in bl.packege_list_with_no_drone())
                    Console.WriteLine(print);
            }

            void listOfPackage(BL.BL bl)
            {

                foreach (IDAL.DO.Package print in bl.packege_list())
                    Console.WriteLine(print);
            }

            void listOfClinet(BL.BL bl)
            {

                foreach (IDAL.DO.Client print in bl.cilent_list())
                    Console.WriteLine(print);
            }

            void listOfDrone(BL.BL bl)
            {

                List<IDAL.DO.Drone> list = (List<IDAL.DO.Drone>)bl.Drone_list();
                foreach (IDAL.DO.Drone print in list)
                    Console.WriteLine(print);
            }

            void listOfBass(BL.BL bl)
            {


                foreach (IDAL.DO.Base_Station print in bl.Base_station_list())
                    Console.WriteLine(print);


            }

            void pointToDegree(BL.BL bl, out bool check, out double point2)
            {
                Console.Write("Enter a longitude or latitude to ge it in degree :");
                do
                {
                    check = double.TryParse(Console.ReadLine(), out point2);
                } while (!check);
                Console.WriteLine(bl.Point_to_degree(point2));
            }

            void distanceBetween2points(BL.BL bl, out bool check, out uint num1, out double doubleNum1, out double doubleNum2, out double point1, out double point2)
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

                Console.WriteLine($"the distance is: {0}KM", bl.Distance(doubleNum2, doubleNum1, point2, point1));
            }

            void packageByNumber(BL.BL bl, out bool check, out uint num)
            {
                Console.Write("Enter packege number:");
                check = uint.TryParse(Console.ReadLine(), out num);
                Console.WriteLine(bl.packege_by_number(num));
            }

            void droneBySirialNumber(BL.BL bl, out bool check, out uint num)
            {
                Console.Write("Enter drone number:");
                check = uint.TryParse(Console.ReadLine(), out num);
                Console.WriteLine(bl.Drone_by_number(num));
            }

            void baseByNumber(BL.BL bl, out bool check, out uint num)
            {
                //received the details from the user
                Console.Write("Enter base number:");
                check = uint.TryParse(Console.ReadLine(), out num);
                Console.WriteLine(bl.Base_station_by_number(num));
            }

            void clientById(BL.BL bl, out bool check, out uint num)
            {
                //received the details from the user
                Console.Write("Enter ID:");
                check = uint.TryParse(Console.ReadLine(), out num);
                Console.WriteLine(bl.cilent_by_number(num));
            }


            void updateDelivery(BL.BL bl, out bool check, out uint num)
            {
                //received the details from the user
                Console.Write("Enter package number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num);
                } while (!check);
                bl.Package_arrived(num);
            }

            void updateCollect(BL.BL bl, out bool check, out uint num)
            {
                Console.Write("Enter package number:");

                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num);
                } while (!check);
                bl.Package_collected(num);
            }

            void updateAssociate(BL.BL bl, out bool check, out uint num, out uint num1)
            {
                //received the details from the user
                Console.Write("Enter package number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num);
                } while (!check);
                Console.Write("Enter drone number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num1);
                } while (!check);
                bl.ConnectPackageToDrone(num, num1);
            }

            void addPackage(BL.BL bl, out bool check, out uint num, out uint id, out uint num1, out uint num2)
            {
                //received the details from the user

                Console.Write("Enter ID of the sender:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out id);
                } while (!check);
                Console.Write("Enter ID of the recipient:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num1);
                } while (!check);
                Console.Write("Enter Weight categories 0 for easy,1 for mediom,2 for haevy:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num2);
                } while (!check);
                Console.Write("Enter priority 0 for Immediate ,1 for quick ,2 for Regular:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num);
                } while (!check);
                // add new package
                bl.AddPackage(id, num1, num2, num);
            }

            void Add_Drone(BL.BL bl, out bool check, out uint num, out uint id, out uint num1, out double doubleNum1, out string name)
            {
                Console.Write("Enter sireal number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out id);
                } while (!check);
                Console.Write("Enter model:");
                name = Console.ReadLine();
                Console.Write("Enter weight Category:0 for easy,1 for mediom,2 for heavy:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num);
                } while (!check);

                Console.Write("Enter number of base station for the first charging: ");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num1);
                } while (!check);

                Drone drone = new Drone
                {
                    SerialNum = id,
                    Model = name,
                    weightCategory = (Weight_categories)num,
             
                };

                //add new drone
                bl.AddDrone(drone, num1);
            }

            void Add_Base(BL.BL bL, out bool check, out uint id, out uint num1, out double doubleNum1, out double doubleNum2, out string name)
            {
                Console.Write("Enter base number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out id);
                } while (!check);
                Console.Write("Enter base name:");
                name = Console.ReadLine();
                Console.Write("Enter Number of charging stations:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num1);
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

                BaseStation baseStation = new BaseStation
                {
                    SerialNum = id,
                    Name = name,
                    location = new Location { Longitude = doubleNum1, Latitude = doubleNum2 },
                    dronesInCharge = null
                };
                // add new Base station
               bl.AddBase(baseStation);
            }

            void Add_Client(BL.BL bl, out bool check, out uint myId, out double doubleNum1, out double doubleNum2, out string name, out string phone)
            {
                Console.Write("Enter ID:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out myId);
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
                Client client = new Client
                {
                    Id = myId,
                    Name = name,
                    Phone = phone,
                    location = new Location { Longitude = doubleNum1, Latitude = doubleNum2 },
                    FromClient = null,
                    ToClient=null
                };
                bl.AddClient(client);
            }

            void releaseDrone(BL.BL bl, out bool check, out uint serial,out uint timeInCharge)
           {
                Console.Write("Enter sireal number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out serial);
                } while (!check);

                Console.Write("Enter how long it is in charge:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out timeInCharge);
                } while (!check);

                bl.ReleaseDroneFromCharge(serial,timeInCharge);
            }
            void updateCharge(BL.BL bl, out bool check, out uint serial)
                {
                Console.Write("Enter sireal number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out serial);
                } while (!check);

                bl.DroneToCharge(serial);
            }
            
            static void Main(string[] args)

            {

                IBL.IBL bl = new BL.BL();
                Menu(bl);
            }


        }
    }
}
