using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using General.Models;

namespace General.Maps
{
    public class RouteMap
    {
        public RouteMap(EntityTypeBuilder<Route> entityBuilder)
        {
            entityBuilder.HasKey(x => x.IdRoute);
            entityBuilder.ToTable("Route");

            entityBuilder.Property(x => x.IdRoute).HasColumnName("IdRoute");
            entityBuilder.Property(x => x.NameRoute).HasColumnName("NameRoute");
            entityBuilder.Property(x => x.InitialBusStop).HasColumnName("InitialBusStop");
            entityBuilder.Property(x => x.FinalBusStop).HasColumnName("FinalBusStop");
            entityBuilder.Property(x => x.ApproximateDuration).HasColumnName("ApproximateDuration");
        }
    }
}
