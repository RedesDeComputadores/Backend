using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using General.Models;

namespace General.Maps
{
    public class BusStopMap
    {
        public BusStopMap(EntityTypeBuilder<BusStop> entityBuilder)
        {
            entityBuilder.HasKey(x => x.IdBusStop);
            entityBuilder.ToTable("BusStop");

            entityBuilder.Property(x => x.IdBusStop).HasColumnName("IdBusStop");
            entityBuilder.Property(x => x.NameOrAddressBusStop).HasColumnName("NameOrAddressBusStop");
        }
    }
}