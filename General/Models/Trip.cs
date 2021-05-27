using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Models
{
    public class Trip
    {
        public Trip()
        {
        }

        public int? IdTrip { get; set; }

        public int Route_IdRoute { get; set; }

        public string Bus_LicensePlateBus { get; set; }

        public string Driver_DriversLicense { get; set; }

        public int CurrentOccupationTrip { get; set; }

        public int TimeZoneTrip { get; set; }

        public string StatusTrip { get; set; }

        public int TotalPassengersTrip {get; set; }

        public int MaximumCapacityTrip {get; set; }

    }
}
