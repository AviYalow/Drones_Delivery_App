using System;


namespace IDAL
{
    namespace DO
    {
        public struct Clint
        {
            readonly int id;
            String name;
            int fonNumber;
            double longitude;
            double latitude;
            private int myVar;


        }
        public struct Drown
        {
            readonly int siralNumber;
            string baseStation;
            string whiteCategory;
            int butrryStatos;
            string drownStatos;
        }
        public struct Base_Station
        {
            readonly int baseNumber;
            string NameBase;
            int Number_of_charging_stations;
            double longitude;
            double latitude;
        }
        public struct Pacege
        {
            readonly int sirialNumber;
            int sendClint;
            int getingClint;
            string whightCatgory;
            string priority;
            int operator_skimmer_ID;
            DateTime receiving_delivery;
            DateTime package_association;
            DateTime collect_package_for_shipment;
            DateTime package_arrived;
        }

        public struct BtarryLoad
        {
            int idBaseStation;
            int id_drown;
        }
    }
}
