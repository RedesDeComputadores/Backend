using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using General.Models;

namespace General.Maps
{
    public class RouteStopsMap
    {
        public RouteStopsMap(EntityTypeBuilder<RouteStops> entityBuilder)
        {
            entityBuilder.HasKey(x => new { x.Route_IdRoute, x.BusStop_IdBusStop, x.positionRouteStops });
            entityBuilder.ToTable("RouteStops");

            entityBuilder.Property(x => x.Route_IdRoute).HasColumnName("Route_IdRoute");
            entityBuilder.Property(x => x.BusStop_IdBusStop).HasColumnName("BusStop_IdBusStop");
            entityBuilder.Property(x => x.positionRouteStops).HasColumnName("positionRouteStops");
        }
    }
}