using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using General.Controllers;
using General.Models;
using Microsoft.AspNetCore.Cors;

namespace General.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public TripController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [EnableCors]
        public object GetTrip()
        {
            return _context.Trips.Where(b => 1 > 0).Select((c) => new
            {
                IdTrip = c.IdTrip,
                Route_IdRoute = c.Route_IdRoute,
                Bus_LicensePlateBus = c.Bus_LicensePlateBus,
                Driver_DriversLicense = c.Driver_DriversLicense,
                CurrentOccupationTrip = c.CurrentOccupationTrip,
                TimeZoneTrip = c.TimeZoneTrip,
                StatusTrip = c.StatusTrip,
                TotalPassengersTrip = c.TotalPassengersTrip,
                MaximumCapacityTrip = c.MaximumCapacityTrip
            }).ToList();
        }

        [HttpGet("Route/{Route_IdRoute}/LicensePlateBus/{Bus_LicensePlateBus}/DriversLicense/{Driver_DriversLicense}")]
        [EnableCors]
        public object GetTripAccordingToRouteBusDriver(int Route_IdRoute, string Bus_LicensePlateBus, string Driver_DriversLicense)
        {
            return _context.Trips.Where(b => b.Route_IdRoute.Equals(Route_IdRoute) 
            & b.Bus_LicensePlateBus.Equals(Bus_LicensePlateBus) 
            & b.Driver_DriversLicense.Equals(Driver_DriversLicense)).Select((c) => new
            {
                IdTrip = c.IdTrip,
                Route_IdRoute = c.Route_IdRoute,
                Bus_LicensePlateBus = c.Bus_LicensePlateBus,
                Driver_DriversLicense = c.Driver_DriversLicense,
                CurrentOccupationTrip = c.CurrentOccupationTrip,
                TimeZoneTrip = c.TimeZoneTrip,
                StatusTrip = c.StatusTrip,
                TotalPassengersTrip = c.TotalPassengersTrip,
                MaximumCapacityTrip = c.MaximumCapacityTrip
            }).ToList();
        }

        [HttpGet("{IdTrip}")]
        [EnableCors]
        public async Task<ActionResult<Trip>> GetTrip(int IdTrip)
        {
            var trip = await _context.Trips.FindAsync(IdTrip);

            if (trip == null)
            {
                return NotFound();
            }

            return trip_ToDTO(trip);
        }

        [HttpPut("{IdTrip}")]
        [EnableCors]
        public async Task<ActionResult<Trip>> Updatebus(int IdTrip, Trip trip)
        {
            if (IdTrip != trip.IdTrip)
            {
                return BadRequest();
            }

            var tripAux = await _context.Trips.FindAsync(IdTrip);
            if (tripAux == null)
            {
                return NotFound();
            }

            
                tripAux.IdTrip = trip.IdTrip;
                tripAux.Route_IdRoute = trip.Route_IdRoute;
                tripAux.Bus_LicensePlateBus = trip.Bus_LicensePlateBus;
                tripAux.Driver_DriversLicense = trip.Driver_DriversLicense;
                tripAux.CurrentOccupationTrip = trip.CurrentOccupationTrip;
                tripAux.TimeZoneTrip = trip.TimeZoneTrip;
                tripAux.StatusTrip = trip.StatusTrip;
                tripAux.TotalPassengersTrip = trip.TotalPassengersTrip;
                tripAux.MaximumCapacityTrip = trip.MaximumCapacityTrip;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception) when (!tripExists(IdTrip))
            {
                return NotFound();
            }

            return trip_ToDTO(tripAux);
        }

        [HttpPost]
        [EnableCors]
        public async Task<ActionResult<Message>> Createbus(Trip trip)
        {
            var tripAux = new Trip
            {
                Route_IdRoute = trip.Route_IdRoute,
                Bus_LicensePlateBus = trip.Bus_LicensePlateBus,
                Driver_DriversLicense = trip.Driver_DriversLicense,
                CurrentOccupationTrip = trip.CurrentOccupationTrip,
                TimeZoneTrip = trip.TimeZoneTrip,
                StatusTrip = trip.StatusTrip,
                TotalPassengersTrip = trip.TotalPassengersTrip,
                MaximumCapacityTrip = trip.MaximumCapacityTrip
            };

            _context.Trips.Add(tripAux);
            await _context.SaveChangesAsync();

            DateTime midnight = DateTime.Now.Date;
            string dateNowWeekday = DateTime.Now.ToString("dddd");
            string dateNow = DateTime.Now.ToString();
            try
            {
                var tripHistoryObject = _context.TripHistorys.Where(b => b.TimeZoneTripHistory.Equals(trip.TimeZoneTrip)
                                                                    && b.Route_IdRoute.Equals(trip.Route_IdRoute)
                                                                    && b.DateTripHistory >= midnight
                                                                    && dateNowWeekday == b.WeekdayTripHistory)
                .Select((c) => new {
                    IdTripHistory = c.IdTripHistory,
                    Route_IdRoute = c.Route_IdRoute,
                    WeekdayTripHistory = c.WeekdayTripHistory,
                    DateTripHistory = c.DateTripHistory,
                    TimeZoneTripHistory = c.TimeZoneTripHistory,
                    PassengersPickedUpTripHistory = c.PassengersPickedUpTripHistory,
                    PassengerCapacityTripHistory = c.PassengerCapacityTripHistory,
                    SupplyFactorTripHistory = c.SupplyFactorTripHistory,
                    NumberOfBusesOfferedTripHistory = c.NumberOfBusesOfferedTripHistory
                }).ToList();

                if (tripHistoryObject.Count() == 0)
                {
                    TripHistory tripHistory = new TripHistory();
                    tripHistory.Route_IdRoute = trip.Route_IdRoute;
                    tripHistory.WeekdayTripHistory = DateTime.Now.ToString("dddd");
                    tripHistory.DateTripHistory = DateTime.Now;
                    tripHistory.TimeZoneTripHistory = trip.TimeZoneTrip;
                    tripHistory.PassengersPickedUpTripHistory = trip.TotalPassengersTrip;
                    tripHistory.PassengerCapacityTripHistory = trip.MaximumCapacityTrip;
                    tripHistory.SupplyFactorTripHistory = Convert.ToDouble(trip.TotalPassengersTrip) / Convert.ToDouble(trip.MaximumCapacityTrip);
                    tripHistory.NumberOfBusesOfferedTripHistory = 1;
                    _context.TripHistorys.Add(tripHistory);
                }
                else
                {
                    var tripHistoryAuxiliar = await _context.TripHistorys.FindAsync(tripHistoryObject.ElementAt(0).IdTripHistory);
                    tripHistoryAuxiliar.PassengerCapacityTripHistory += tripAux.MaximumCapacityTrip;
                    tripHistoryAuxiliar.PassengersPickedUpTripHistory += tripAux.TotalPassengersTrip;
                    tripHistoryAuxiliar.SupplyFactorTripHistory = Convert.ToDouble(tripHistoryAuxiliar.PassengersPickedUpTripHistory) / Convert.ToDouble(tripHistoryAuxiliar.PassengerCapacityTripHistory);
                    tripHistoryAuxiliar.NumberOfBusesOfferedTripHistory++;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception ==========>>>> " + e);                
            }

            await _context.SaveChangesAsync();

            var message = new Message("Trip successfully added");

            return message;
        }

        [HttpDelete("{IdTrip}")]
        [EnableCors]
        public async Task<ActionResult<Trip>> Deletebus(int IdTrip)
        {
            var trip = await _context.Trips.FindAsync(IdTrip);

            if (trip == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();

            return trip_ToDTO(trip);
        }

        private bool tripExists(int IdTrip) =>
             _context.Trips.Any(e => e.IdTrip == IdTrip);

        private static Trip trip_ToDTO(Trip trip) =>
            new Trip
            {
                Route_IdRoute = trip.Route_IdRoute,
                Bus_LicensePlateBus = trip.Bus_LicensePlateBus,
                Driver_DriversLicense = trip.Driver_DriversLicense,
                CurrentOccupationTrip = trip.CurrentOccupationTrip,
                TimeZoneTrip = trip.TimeZoneTrip,
                StatusTrip = trip.StatusTrip,
                TotalPassengersTrip = trip.TotalPassengersTrip,
                MaximumCapacityTrip = trip.MaximumCapacityTrip
            };
    }
}
