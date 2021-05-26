using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using General.Models;

namespace General.Maps
{
    public class DriverMap
    {
        public DriverMap(EntityTypeBuilder<Driver> entityBuilder)
        {
            entityBuilder.HasKey(x => x.DriversLicense);
            entityBuilder.ToTable("Driver");

            entityBuilder.Property(x => x.DriversLicense).HasColumnName("DriversLicense");
            entityBuilder.Property(x => x.Name).HasColumnName("Name");
            entityBuilder.Property(x => x.DriverExperience).HasColumnName("DriverExperience");
            entityBuilder.Property(x => x.AverageDriverRating).HasColumnName("AverageDriverRating");
        }
    }
}
