using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// exception for item alrdy exist
        /// </summary>
        public class ItemFoundExeption : Exception
        {
            IDAL.DO.ItemFoundException exeption { get; set; }
            public ItemFoundExeption(IDAL.DO.ItemFoundException ex) { exeption = ex; }
            protected ItemFoundExeption(SerializationInfo serializableAttribute, StreamingContext context) : base(serializableAttribute, context) { }
            public override string ToString()
            {
                string exeptionString = "";
                DateTime time = DateTime.Now;
                exeptionString += $"\aTime:{time.ToLongTimeString()}\n";
                exeptionString += exeption.ToString();
                return exeptionString;
            }
        }

        /// <summary>
        /// exception for item not exist
        /// </summary>
        public class ItemNotFoundException : Exception
        {
            IDAL.DO.ItemNotFoundException exeption { get; set; }
            public string type { get; set; }
            public uint key { get; set; }

            public ItemNotFoundException(string type, uint unic_key):base(type)
            {
                this.type = type;
                key = unic_key;
            }
            public ItemNotFoundException(IDAL.DO.ItemNotFoundException ex):base(ex.Message,ex) { exeption = ex; }
            protected ItemNotFoundException(SerializationInfo serializableAttribute, StreamingContext context) : base(serializableAttribute, context) { }
            public override string ToString()
            {
                string exeptionString = "";
                DateTime time = DateTime.Now;
                exeptionString += $"\aTime:{time.ToLongTimeString()}\n";
                exeptionString += exeption.ToString();
                return exeptionString;
            }
        }

        /// <summary>
        /// no place for drone in base
        /// </summary>
        public class NoPlaceForChargeException:Exception
        {
            uint base_ { get; set; }
            public NoPlaceForChargeException(uint base_)
            { this.base_ = base_; }
            protected NoPlaceForChargeException(SerializationInfo serializableAttribute, StreamingContext context) : base(serializableAttribute, context) { }
            public override string ToString()
            {
                return $"Time:{DateTime.Now} \nIn this base number:{base_}\nno place for drone! \n" +
                    $"plase chack the chrging drone list , and relese drone whit full buttry. ";
            }
        }

        /// <summary>
        /// exception for try down number of charging station whan still have more drone in charge
        /// </summary>
        public class UpdateChargingPositionsException:Exception
        {
            public int DroneInCharge { get; set; }
            public UpdateChargingPositionsException(int number) : base() { DroneInCharge = number; }
            protected UpdateChargingPositionsException(SerializationInfo serializableAttribute, StreamingContext context) : base(serializableAttribute, context) { }
            public override string ToString()
            {
                return $"There are {DroneInCharge} skimmers in charging than the new amount of positions.\n" +
                    "Please release skimmers from charging.";
            }
        }

        public class TryToPullOutMoreDrone:Exception
        {
            public TryToPullOutMoreDrone() : base() { }
            protected TryToPullOutMoreDrone(SerializationInfo serializableAttribute, StreamingContext context) : base(serializableAttribute, context) { }
            public override string ToString()
            {
                return "Your try to pull out more drone then statins charge! ";
            }
        }

        public class DroneCantSendToChargeException : Exception
        {
            public DroneCantSendToChargeException() : base() { }
            protected DroneCantSendToChargeException(SerializationInfo serializableAttribute, StreamingContext context) : base(serializableAttribute, context) { }
            public override string ToString()
            {
                return "The drone not in statos to get charge";
            }
        }

        public class NoButrryToTripException:Exception
        {
           public double buttry { get; set; }
          public  NoButrryToTripException(double butrry) : base() { this.buttry = butrry; }
            protected NoButrryToTripException(SerializationInfo serializableAttribute, StreamingContext context) : base(serializableAttribute, context) { }
            public override string ToString()
            {
                return $"This Drone hes{buttry}. he cant go to this point!" +
                    $"You should send the skimmer for charging or transfer location " ;
            }
        }

        public class NumberNotEnoughException:Exception
        {
            int amount { get; set; }
            public NumberNotEnoughException(int num) : base() { amount = num; }
            protected NumberNotEnoughException(SerializationInfo serializableAttribute, StreamingContext context) : base(serializableAttribute, context) { }
            public override string ToString()
            {
                return $"ther his last from {amount} digit number" ;
            }
        }

        public class NumberMoreException : Exception
        {

            public NumberMoreException() : base() { }
            protected NumberMoreException(SerializationInfo serializableAttribute, StreamingContext context) : base(serializableAttribute, context) { }
            public override string ToString()
            {
                return "ther his more then 10 digit number";
            }
        }

        public class IllegalDigitsException : Exception
        {
           public IllegalDigitsException() : base() { }
            protected IllegalDigitsException(SerializationInfo serializableAttribute, StreamingContext context) : base(serializableAttribute, context) { }
        public override string ToString()
            {
                return "Digits only without signs and letters";
            }
        }

        public class StartingException:Exception
        {
           public string Start { get; set; }
          public  StartingException(string masegg) : base(masegg) { Start = masegg; }
            protected StartingException(SerializationInfo serializableAttribute, StreamingContext context) : base(serializableAttribute, context) { }
        public override string ToString()
            {
                return "You have to start whit"+Start+"only";
            }
        }

        public class TheListIsEmptyException:Exception
        {
            public TheListIsEmptyException(string masseg="\a ERROR: This list his empty") : base(masseg) { }
           protected TheListIsEmptyException(SerializationInfo serializableAttribute, StreamingContext context) : base(serializableAttribute,context) { }
        }

        public class DroneCantMakeDliveryException:Exception
        {
            public DroneCantMakeDliveryException(string masseg = "\a ERROR: Drone not free for  delivery") : base(masseg) { }
            protected DroneCantMakeDliveryException(SerializationInfo serializableAttribute, StreamingContext context) : base(serializableAttribute, context) { }
        }

        public class FunctionErrorException:Exception
        {
           
           public FunctionErrorException(string message) : base(message) {  }
            protected FunctionErrorException(SerializationInfo serializableAttribute, StreamingContext context) : base(serializableAttribute, context) { }
            public override string ToString()
            {
                return $"Error in"+ Message+" function";
            }
        }
    }
}
