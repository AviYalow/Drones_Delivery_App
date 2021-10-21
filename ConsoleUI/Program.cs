using System;
using IDAL;
using DalObject;

namespace ConsoleUI
{
    class Program
    {
        enum Options { Exit, Add, Update, ShowObject, ShowList }
        enum Entities { Exit, Client, Base_station, Drone, Package }
        private static void Menu()
        {
            Options option;
            Entities entity;

            do
            {
                bool success;
                int number;

                do
                {
                    Console.WriteLine("Choose one of the following:\n" +
                        " 1-Add,\n 2-Update,\n 3-Show Details,\n 4-Show List,\n 0-Exit");


                    success = int.TryParse(Console.ReadLine(), out number);
                    if (!success)
                        Console.WriteLine("Error!\n");
                }
                while (!success);
                option = (Options)number;

                switch (option)
                {
                    case Options.Add:
                        
                        do
                        {
                            Console.WriteLine("Choose an entity:\n " +
                             "1-Client,\n 2-Base station,\n 3- Drone,\n 4- Package");
                            success = int.TryParse(Console.ReadLine(), out number);
                            if (!success)
                                Console.WriteLine("Error!\n");
                        }
                        while (!success);
                        
                        entity = (Entities)number;
                        
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
        static void Main(string[] args)
        {

            new DalObject.DalObject();
            Menu();
        }
    }
}
