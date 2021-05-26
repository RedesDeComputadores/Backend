using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Models
{
    public class Bus
    {
        public Bus()
        {
        }

        public string LicensePlateBus { get; set; }

        public string Model { get; set; }

        public int SeatedPassengerCapacity { get; set; }

        public int StandingPassengerCapacity { get; set; }

    }
}
