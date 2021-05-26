using System;
using General.Maps;
using Microsoft.EntityFrameworkCore;
using General.Models;

namespace General.Models
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusStop> BusStops { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<RouteStops> RouteStops_s { get; set; }
        public DbSet<TripHistory> TripHistorys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new BusMap(modelBuilder.Entity<Bus>());
            new BusStopMap(modelBuilder.Entity<BusStop>());
            new DriverMap(modelBuilder.Entity<Driver>());
            new RouteMap(modelBuilder.Entity<Route>());
            new TripMap(modelBuilder.Entity<Trip>());
            new RouteStopsMap(modelBuilder.Entity<RouteStops>());
            new TripHistoryMap(modelBuilder.Entity<TripHistory>());
        }
    }
}
