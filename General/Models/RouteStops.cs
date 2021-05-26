using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Models
{
    public class RouteStops
    {
        public RouteStops()
        {
        }

        public int Route_IdRoute { get; set; }

        public int BusStop_IdBusStop { get; set; }

        public int positionRouteStops { get; set; }
        
    }
}
