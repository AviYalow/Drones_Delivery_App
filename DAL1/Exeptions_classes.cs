using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
       public class Item_found_exception : Exception
        {
            public string type { get; set; }
            public int key { get; set; }
            public Item_found_exception(string type, int unic_key)
            {
                this.type = type;
                key = unic_key;
            }

            public override string ToString()
            {
                string Error_mashge = "";
                Error_mashge += $"this " + type + "\n";
                Error_mashge += $"number: {key}\n";
                Error_mashge += "\nalrdy found";
                return Error_mashge;

            }
        }

       public class Item_not_found_exception : Exception
        {
            public string type { get; set; }
            public int key { get; set; }
            public Item_not_found_exception(string type, int unic_key)
            {
                this.type = type;
                key = unic_key;
            }

            public override string ToString()
            {
                string Error_mashge = "";
                Error_mashge += $"this " + type + "\n";
                Error_mashge += $"number: {key}\n";
                Error_mashge += "\nnot found";
                Error_mashge += $"Please check if {type} number: {key} existing\n";
                Error_mashge += $"You can check {type} by issuing complete lists\n";
                return Error_mashge;

            }
        }

        public class List_empty_exception : Exception
        {
            string ExceptionMesseg { get; set; }
            public List_empty_exception(string list)
            {
                ExceptionMesseg = $"ERROR: this {list} empty! ";
            }
            public override string ToString()
            {
                return ExceptionMesseg;
            }
        }

        public class No_Item_Whit_this_condition_exception : Exception
        {
            string ExceptionMassege { get => " no items whit your condition!"; }
        }

    }
}
