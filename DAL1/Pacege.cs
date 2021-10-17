using System;

namespace IDAL
{
    namespace DO
    {
        public struct Pacege
        {
            public int sirialNumber { get; private set; }
            public int sendClint { get; set; }
            public int getingClint { get; set; }
            public String whightCatgory { get; set; }
            public String priority { get; set; }
            public int operator_skimmer_ID { get; set; }
            public DateTime receiving_delivery { get; set; }
            public DateTime package_association { get; set; }

            public DateTime collect_package_for_shipment { get; set; }
            public DateTime package_arrived { get; set; }

        }

    }
}