using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Models
{
    public class Route
    {
        public Route()
        {
        }

        public int IdRoute { get; set; }

        public string NameRoute { get; set; }

        public string InitialBusStop { get; set; }

        public string FinalBusStop { get; set; }

        public string ApproximateDuration { get; set; }

    }
}
