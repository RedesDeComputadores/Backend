using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Models
{
    public class Driver
    {
        public Driver()
        {
        }

        public string DriversLicense { get; set; }

        public string Name { get; set; }

        public int DriverExperience { get; set; }

        public string AverageDriverRating { get; set; }
    }
}
