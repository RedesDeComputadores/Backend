using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using General.Models;

namespace General.Maps
{
    public class BusMap
    {
        public BusMap(EntityTypeBuilder<Bus> entityBuilder)
        {
            entityBuilder.HasKey(x => x.LicensePlateBus);
            entityBuilder.ToTable("Bus");

            entityBuilder.Property(x => x.LicensePlateBus).HasColumnName("LicensePlateBus");
            entityBuilder.Property(x => x.Model).HasColumnName("Model");
            entityBuilder.Property(x => x.SeatedPassengerCapacity).HasColumnName("SeatedPassengerCapacity");
            entityBuilder.Property(x => x.StandingPassengerCapacity).HasColumnName("StandingPassengerCapacity");
        }
    }
}
