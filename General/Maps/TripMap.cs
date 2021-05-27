using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using General.Models;

namespace General.Maps
{
    public class TripMap
    {
        public TripMap(EntityTypeBuilder<Trip> entityBuilder)
        {
            entityBuilder.HasKey(x => x.IdTrip);
            entityBuilder.ToTable("Trip");

            entityBuilder.Property(x => x.IdTrip).HasColumnName("IdTrip");
            entityBuilder.Property(x => x.Route_IdRoute).HasColumnName("Route_IdRoute");
            entityBuilder.Property(x => x.Bus_LicensePlateBus).HasColumnName("Bus_LicensePlateBus");
            entityBuilder.Property(x => x.Driver_DriversLicense).HasColumnName("Driver_DriversLicense");
            entityBuilder.Property(x => x.CurrentOccupationTrip).HasColumnName("CurrentOccupationTrip");
            entityBuilder.Property(x => x.TimeZoneTrip).HasColumnName("TimeZoneTrip");
            entityBuilder.Property(x => x.StatusTrip).HasColumnName("StatusTrip");
            entityBuilder.Property(x => x.TotalPassengersTrip).HasColumnName("TotalPassengersTrip");
            entityBuilder.Property(x => x.MaximumCapacityTrip).HasColumnName("MaximumCapacityTrip");
        }
    }
}
