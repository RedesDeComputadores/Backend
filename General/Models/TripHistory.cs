using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Models
{
    public class TripHistory
    {
        public TripHistory()
        {
        }

        public int IdTripHistory { get; set; }

        public int Route_IdRoute { get; set; }

        public string WeekdayTripHistory { get; set; }

        public DateTime DateTripHistory { get; set; }

        public int TimeZoneTripHistory { get; set; }

        public int PassengersPickedUpTripHistory { get; set; }

        public int PassengerCapacityTripHistory { get; set; }

        public double SupplyFactorTripHistory {get; set; }

        public int NumberOfBusesOfferedTripHistory {get; set; }

    }
}