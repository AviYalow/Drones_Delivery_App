﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
       public class ItemFoundException : Exception
        {
            public string type { get; set; }
            public uint key { get; set; }
            public ItemFoundException(string type, uint unic_key)
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

       public class ItemNotFoundException : Exception
        {
            public string type { get; set; }
            public uint key { get; set; }
            public ItemNotFoundException(string type, uint unic_key)
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

        public class ListEmptyException : Exception
        {
            string ExceptionMesseg { get; set; }
            public ListEmptyException(string list)
            {
                ExceptionMesseg = $"ERROR: this {list} empty! ";
            }
            public override string ToString()
            {
                return ExceptionMesseg;
            }
        }

        public class NoItemWhitThisConditionException : Exception
        {
            string ExceptionMassege { get => " no items whit your condition!"; }
        }

    }
}
