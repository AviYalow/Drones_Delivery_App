using System;
using System.Collections;
using IBL.BO;
using IBL;

namespace ConsoleUI_BL
{
    class Program
    {
        // enumes for the menu options
        #region
        enum Options { Exit, Add, Update, ShowDetails, ShowList }
        enum Entities { Exit, Client, Base_station, Drone, Package }
        enum UpdatesOptions { Exit, DroneName, Base_station, Client, Associate, Collect, Delivery, Charge, UnCharge }
        enum Show { Exit, Client, Base_station, Drone, Package, ShowDistance, ShoeDegree }
        enum ShowList { Exit, Base_station, Drones, Clients, Package, FreePackage, FreeBaseStation }
        enum Distans_2_point { base_station = 1, client }
        #endregion

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
        private static void Menu(IBL.IBL bl)
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
                int amount;
                double doubleNum1, doubleNum2;
                string str, name, phone;


                str = "Choose one of the following:\n" +
                    " 1-Add,\n 2-Update,\n 3-Show Details,\n 4-Show List,\n 0-Exit";

                num = getChoose(str);
                option = (Options)num;

                switch (option)
                {
                    case Options.Add:

                        #region
                        str = "Choose an entity:\n " +
                             "1-Client,\n 2-Base station,\n 3- Drone,\n 4- Package";
                        num = getChoose(str);
                        entity = (Entities)num;
                        try
                        {
                            switch (entity)
                            {

                                case Entities.Base_station:
                                    {
                                        //received the details from the user

                                        AddBase(bl, out check, out id, out num1, out doubleNum1, out doubleNum2, out name);
                                        break;
                                    }
                                case Entities.Drone:
                                    {
                                        //received the details from the user
                                        AddDrone(bl, out check, out num, out id, out num1, out name);
                                        break;
                                    }
                                case Entities.Client:
                                    {
                                        //received the details from the user

                                        AddClient(bl, out check, out num, out doubleNum1, out doubleNum2, out name, out phone);


                                        break;
                                    }

                                case Entities.Package:
                                    {

                                        AddPackage(bl, out check, out num, out id, out num1, out num2);


                                        break;
                                    }
                                case Entities.Exit:
                                    break;
                            }


                        }
                        catch (ItemNotFoundException ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (ItemFoundExeption ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (NumberMoreException ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (NumberNotEnoughException ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (ClientOutOfRangeException ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (MoreDistasThenMaximomException ex)
                        { Console.WriteLine(ex); }
                        #endregion
                        break;
                    #region
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
                                case UpdatesOptions.DroneName:

                                    UpdateDroneName(bl, out check, out num, out name);
                                    break;

                                case UpdatesOptions.Base_station:

                                    UpdateBase(bl, out check, out id, out name, out amount);
                                    //option to connect package to drone
                                    break;

                                case UpdatesOptions.Client:
                                    UpdateClient(bl, out check, out id, out name, out phone);
                                    break;

                                //sent drone to a free charging station
                                case UpdatesOptions.Charge:

                                    updateCharge(bl, out check, out num);
                                    break;

                                // Release drone from charging position
                                case UpdatesOptions.UnCharge:

                                    UpdateUnCharge(bl, out check, out num);
                                    break;

                                case UpdatesOptions.Associate:

                                    UpdateAssociate(bl, out check, out num);
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


                                case UpdatesOptions.Exit:
                                    break;

                            }
                        }
                        catch (IBL.BO.UpdateChargingPositionsException ex)
                        {
                            Console.WriteLine(ex);
                            string chose = "";
                            do
                            {
                                Console.WriteLine("do you want to relse number of drone in this base?\n plese enter yes\\no");
                                chose = Console.ReadLine();
                                if (chose == "yes")
                                {
                                    Console.WriteLine("How match drone you want to pull out?");
                                    do
                                    {
                                        check = uint.TryParse(Console.ReadLine(), out num);
                                    } while (!check);
                                    bl.FreeBaseFromDrone(ex.BaseNumber, (int)num);
                                }
                                else if (chose == "no")
                                {
                                    break;
                                }
                                else
                                    chose = "";
                            } while (chose == "");
                        }
                        catch (IBL.BO.StartingException ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (IBL.BO.IllegalDigitsException ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (IBL.BO.NumberMoreException ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (IBL.BO.NumberNotEnoughException ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (IBL.BO.TryToPullOutMoreDrone ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (IBL.BO.ItemNotFoundException exception)
                        {

                            Console.WriteLine(exception);
                        }

                        break;
                    #endregion
                    #region
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

                                    clientById(bl, out check, out id);
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
                                    Distans(bl, out check, out doubleNum1);
                                    break;

                                case Show.ShoeDegree:
                                    //  pointToDegree(bl, out check, out point2);
                                    break;
                                case Show.Exit:
                                    break;
                            }
                        }

                        catch (ItemNotFoundException exception)
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
                                  ShowBaseList();
                                    break;
                                case ShowList.Drones:
                                    ShowDroneList();
                                    break;
                                case ShowList.Clients:
                                    ShowClientList();
                                    break;
                                case ShowList.Package:
                                    ShowPackageList();
                                    break;
                                case ShowList.FreePackage:
                                    ShowFreePackage();
                                    break;
                                case ShowList.FreeBaseStation:
                                   ShowFreeBaseStation();
                                    break;
                                case ShowList.Exit:
                                    break;
                            }
                        }
                        catch (TheListIsEmptyException exception)
                        {

                            Console.WriteLine(exception);
                        }
                        catch (ItemNotFoundException exception)
                        {

                            Console.WriteLine(exception);
                        }
                        break;


                    case Options.Exit:
                        break;

                }
                #endregion
            } while (option != Options.Exit);


            void packageByNumber(IBL.IBL bl, out bool check, out uint num)
            {
                Console.Write("Enter packege number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num);
                } while (!check);

                bl.ShowPackage(num);
            }

            void clientById(IBL.IBL bl, out bool check, out uint id)
            {
                //received the details from the user
                Console.Write("Enter ID:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out id);
                } while (!check);
                bl.GetingClient(id);
            }

            void droneBySirialNumber(IBL.IBL bl, out bool check, out uint num)
            {
                Console.Write("Enter drone number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num);
                } while (!check);
                bl.SpecificDrone(num);
            }

            void baseByNumber(IBL.IBL bl, out bool check, out uint num)
            {
                //received the details from the user
                Console.Write("Enter base number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num);
                } while (!check);
                bl.BaseByNumber(num);
            }

            void AddPackage(IBL.IBL bl, out bool check, out uint num, out uint id, out uint num1, out uint num2)
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
                bl.AddPackege(new Package { SendClient = new ClientInPackage { Id = id }, RecivedClient = new ClientInPackage { Id = num1 }, weightCatgory = (WeightCategories)num2, priority = (Priority)num });
            }

            void AddDrone(IBL.IBL bl, out bool check, out uint num, out uint id, out uint num1, out string name)
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
                    weightCategory = (WeightCategories)num,

                };

                //add new drone
                bl.AddDrone(drone, num1);
            }

            void AddBase(IBL.IBL bL, out bool check, out uint id, out uint num1, out double doubleNum1, out double doubleNum2, out string name)
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

            void AddClient(IBL.IBL bl, out bool check, out uint myId, out double doubleNum1, out double doubleNum2, out string name, out string phone)
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
                    Location = new Location { Longitude = doubleNum1, Latitude = doubleNum2 },
                    FromClient = null,
                    ToClient = null
                };
                bl.AddClient(client);
            }

            void UpdateBase(IBL.IBL bl, out bool check, out uint id, out string name, out int amount)
            {
                Console.Write("Enter base number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out id);
                } while (!check);

                Console.Write("Enter base name:");
                name = Console.ReadLine();

                Console.Write("Enter Number of charging stations:");
                string temp;
                do
                {
                    temp = Console.ReadLine();
                    if (temp == "")
                    {
                        amount = -1;
                        break;
                    }
                    check = int.TryParse(temp, out amount);
                } while (!check);


                if (name != "" && amount == -1)
                    bl.UpdateBase(id, name);
                else if (name == "" && amount != -1)
                    bl.UpdateBase(id, "", amount);
                else
                    bl.UpdateBase(id, name, amount);

            }

            void updateDelivery(IBL.IBL bl, out bool check, out uint num)
            {
                //received the details from the user
                Console.Write("Enter drone number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num);
                } while (!check);
                bl.PackegArrive(num);
            }

            void UpdateClient(IBL.IBL bl, out bool check, out uint id, out string name, out string phone)
            {
                Console.Write("Enter ID:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out id);
                } while (!check);
                Console.Write("Enter name:");
                name = Console.ReadLine();
                Console.Write("Enter phone number:");
                phone = Console.ReadLine();

                Client client;
                if (name != "" && phone != "")
                {
                    client = new Client
                    {
                        Id = id,
                        Name = name,
                        Phone = phone

                    };
                }
                else if (name != "" && phone == "")
                {
                    client = new Client
                    {
                        Id = id,
                        Name = name

                    };

                }
                else
                {
                    client = new Client
                    {
                        Id = id,
                        Phone = phone

                    };

                }

                bl.UpdateClient(ref client);
            }

            void updateCollect(IBL.IBL bl, out bool check, out uint num)
            {
                Console.Write("Enter drone number: ");

                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num);
                } while (!check);
                bl.CollectPackegForDelivery(num);
            }

            void UpdateAssociate(IBL.IBL bl, out bool check, out uint num)
            {
                //received the details from the user

                Console.Write("Enter drone number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num);
                } while (!check);

                bl.ConnectPackegeToDrone(num);
            }

            void UpdateDroneName(IBL.IBL bl, out bool check, out uint num, out string name)
            {
                //received the details from the user
                Console.Write("Enter serial number: ");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out num);
                } while (!check);
                Console.Write("Enter model: ");
                name = Console.ReadLine();
                bl.UpdateDroneName(num, name);
            }

            void updateCharge(IBL.IBL bl, out bool check, out uint serial)
            {
                Console.Write("Enter sireal number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out serial);
                } while (!check);

                bl.DroneToCharge(serial);
            }

            void UpdateUnCharge(IBL.IBL bl, out bool check, out uint serial)
            {
                TimeSpan timeInCharge;
                Console.Write("Enter sireal number:");
                do
                {
                    check = uint.TryParse(Console.ReadLine(), out serial);
                } while (!check);

                Console.Write("Enter how long it is in charge:");
                do
                {
                    check = TimeSpan.TryParse(Console.ReadLine(), out timeInCharge);
                } while (!check);

                bl.FreeDroneFromCharging(serial, timeInCharge);
            }

            void Distans(IBL.IBL bl, out bool check, out double doubleNum1)
            {
                check = true;
                doubleNum1 = 0;
                Location location1 = new Location(), location2 = new Location();
                for (int i = 1; i < 3; i++)
                {
                    Console.WriteLine("plese enter point for location{0}", i);
                    do
                    {

                        Console.Write("plese enter latitude point");
                        check = double.TryParse(Console.ReadLine(), out doubleNum1);
                    }
                    while (!check);
                    location1.Latitude = doubleNum1;
                    do
                    {

                        Console.Write("plese enter longitude point");
                        check = double.TryParse(Console.ReadLine(), out doubleNum1);
                    }
                    while (!check);
                    location1.Longitude = doubleNum1;

                }
                Console.WriteLine("The distans is:{0}KM", bl.Distans(location1, location2));
            }
          
            void ShowBaseList()
            {
                foreach (var _base in bl.BaseStationToLists())
                {
                    Console.WriteLine(_base); 
                }
            }
            void ShowFreeBaseStation()
            {
                foreach (var _base in bl.BaseStationToLists())
                {
                    if(_base.FreeState!=0)
                    Console.WriteLine(_base);
                }

            }

            void ShowFreePackage()
            {
                foreach (var pack in bl.PackageWithNoDroneToLists())
                {
                    Console.WriteLine(pack);
                }
            }

           void ShowPackageList()
            {
                foreach (var pack in bl.PackageToLists())
                {
                    Console.WriteLine(pack);
                }
            }

             void ShowClientList()
            {
                foreach (var client in bl.ClientToLists())
                {
                    Console.WriteLine(client);
                }
            }

           void ShowDroneList()
            {
                foreach (var drone in bl.DroneToLists())
                {
                    Console.WriteLine(drone);
                }
            }

        }


        static void Main(string[] args)
        {
            IBL.IBL bl = new BL();
            Menu(bl);
        }
    }
}



