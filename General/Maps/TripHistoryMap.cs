using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using General.Models;

namespace General.Maps
{
    public class TripHistoryMap
    {
        public TripHistoryMap(EntityTypeBuilder<TripHistory> entityBuilder)
        {
            entityBuilder.HasKey(x => new { x.IdTripHistory});
            entityBuilder.ToTable("TripHistory");

            entityBuilder.Property(x => x.IdTripHistory).HasColumnName("IdTripHistory");
            entityBuilder.Property(x => x.Route_IdRoute).HasColumnName("Route_IdRoute");
            entityBuilder.Property(x => x.WeekdayTripHistory).HasColumnName("WeekdayTripHistory");
            entityBuilder.Property(x => x.DateTripHistory).HasColumnName("DateTripHistory");
            entityBuilder.Property(x => x.TimeZoneTripHistory).HasColumnName("TimeZoneTripHistory");
            entityBuilder.Property(x => x.PassengersPickedUpTripHistory).HasColumnName("PassengersPickedUpTripHistory");
            entityBuilder.Property(x => x.PassengerCapacityTripHistory).HasColumnName("PassengerCapacityTripHistory");
            entityBuilder.Property(x => x.SupplyFactorTripHistory).HasColumnName("SupplyFactorTripHistory");
            entityBuilder.Property(x => x.NumberOfBusesOfferedTripHistory).HasColumnName("NumberOfBusesOfferedTripHistory");
        }
    }
}
