using System;
using IDAL;
using DalObject;

namespace ConsoleUI
{
    class Program
    {
        enum Options { Exit, Add, Update, ShowObject, ShowList }
        enum Entities { Exit, Client, Base_station, Drone, Package }
        enum UpdatesOptions { Exit, Associate, Collect, Delivery ,Charge, UnCharge}

        private static void Menu()
        {
            Options option;
            Entities entity;
            UpdatesOptions updatesOption;

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
                                break;
                            case UpdatesOptions.Collect:
                                break;
                            case UpdatesOptions.Delivery:
                                break;
                            case UpdatesOptions.Charge:
                                break;
                            case UpdatesOptions.UnCharge:
                                break;
                            case UpdatesOptions.Exit:
                                break;
                        }
                        break;
                    case Options.ShowObject:
                        break;
                    case Options.ShowList:
                        break;

                    case Options.Exit:
                        break;
                    default:
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
