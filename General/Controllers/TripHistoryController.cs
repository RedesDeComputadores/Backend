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
    public class TripHistoryController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public TripHistoryController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("weekDay/{weekDay}/idRoute/{idRoute}/supplyFactorTrip/{supplyFactorTrip}/timeZone/{timeZone}")]
        [EnableCors]
        public object GetNumberBuses(string weekDay, int timeZone, int idRoute,int supplyFactorTrip)
        {
            var tripHistoryObject = _context.TripHistorys.Where(b => b.TimeZoneTripHistory.Equals(timeZone)
                                                                    && b.Route_IdRoute.Equals(idRoute)
                                                                    && weekDay.Equals(b.WeekdayTripHistory))
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

            if (tripHistoryObject.Count() > 0 && supplyFactorTrip < 100)
            {
                double factor = 0.0, numberBuses = 0.0;
                for (int i = 0; i < tripHistoryObject.Count(); i++)
                {
                    factor += Convert.ToDouble(tripHistoryObject.ElementAt(i).SupplyFactorTripHistory);
                    numberBuses += tripHistoryObject.ElementAt(i).NumberOfBusesOfferedTripHistory;
                }

                factor /= tripHistoryObject.Count();
                numberBuses /= tripHistoryObject.Count();

                var message = new Message((factor > (Convert.ToDouble(supplyFactorTrip) / 100)) ? ("Enviar " + (numberBuses + (numberBuses * 0.1))) : ("Enviar " + (numberBuses - (numberBuses * 0.1))));

                return message;
            }

            return (10);
        }
    }
}
