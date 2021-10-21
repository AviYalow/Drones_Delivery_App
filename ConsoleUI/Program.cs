using System;
using IDAL;
using DalObject;

namespace ConsoleUI
{
    class Program
    {
        enum Options { Exit, Add, Update, ShowDetails, ShowList }
        enum Entities { Exit, Client, Base_station, Drone, Package }
        enum UpdatesOptions { Exit, Associate, Collect, Delivery ,Charge, UnCharge}
        enum Show { Exit, Client, Base_station, Drone, Package, ShowDistance }
        enum ShowList { Exit, Base_station, Drones, Clients, Package, FreePackage, FreeBaseStation }
        private static void Menu()
        {
            Options option;
            Entities entity;
            UpdatesOptions updatesOption;
            Show show;
            ShowList showList;

            do
            {

                int num;
                string str;
                str = "Choose one of the following:\n" +
                    " 1-Add,\n 2-Update,\n 3-Show Details,\n 4-Show List,\n 0-Exit";
                
                num=getChoose(str);
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
                                DalObject.DalObject.Add_client();
                                break;
                            case Entities.Base_station:
                                DalObject.DalObject.Add_station();
                                break;
                            case Entities.Drone:
                                DalObject.DalObject.Add_drone();
                                break;
                            case Entities.Package:
                                DalObject.DalObject.Add_package();
                                break;
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
                            
                            case UpdatesOptions.Associate:
                                DalObject.DalObject.connect_package_to_drone();
                                break;
                            case UpdatesOptions.Collect:
                                DalObject.DalObject.Package_collected();
                                break;
                            case UpdatesOptions.Delivery:
                                DalObject.DalObject.Package_arrived();
                                break;
                            case UpdatesOptions.Charge:
                                Console.WriteLine("Choose a base number from the following Base station:");
                                DalObject.DalObject.Base_station_list_with_free_charge_states();
                                int baseStation;
                                bool success;
                                do
                                {
                                    success = int.TryParse(Console.ReadLine(), out baseStation);
                                    if(!success)
                                        Console.WriteLine("Error! Chooose agine");
                                } while (!success);
                                DalObject.DalObject.send_drone_to_charge(baseStation);
                                break;
                            case UpdatesOptions.UnCharge:
                                DalObject.DalObject.free_drone_from_charge();
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
                                DalObject.DalObject.cilent_by_number();
                                break;
                            case Show.Base_station:
                                Console.WriteLine( DalObject.DalObject.Base_station_by_number());
                                break;
                            case Show.Drone:
                                DalObject.DalObject.Drone_by_number();
                                break;
                            case Show.Package:
                                DalObject.DalObject.packege_by_number();
                                break;
                            case Show.ShowDistance:
                               DalObject.DalObject.Distance();
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
                                DalObject.DalObject.Base_station_list();
                                break;
                            case ShowList.Drones:
                                DalObject.DalObject.Drone_list();
                                break;
                            case ShowList.Clients:
                                DalObject.DalObject.cilent_list();
                                break;
                            case ShowList.Package:
                                DalObject.DalObject.packege_list();
                                break;
                            case ShowList.FreePackage:
                                DalObject.DalObject.packege_list_with_no_drone();
                                break;
                            case ShowList.FreeBaseStation:
                                DalObject.DalObject.Base_station_list_with_free_charge_states();
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

        static void Main(string[] args)
        {

            new DalObject.DalObject();
            Menu();
        }
    }
}
