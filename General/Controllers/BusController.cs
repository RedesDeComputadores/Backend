using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using General.Controllers;
using General.Models;

namespace General.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BusController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public BusController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public object GetBus()
        {
            return _context.Buses.Where(b => b.LicensePlateBus.Contains("")).Select((c) => new
            {
                LicensePlateBus = c.LicensePlateBus,
                Model = c.Model,
                SeatedPassengerCapacity = c.SeatedPassengerCapacity,
                StandingPassengerCapacity = c.StandingPassengerCapacity
            }).ToList();
        }

        [HttpGet("{licensePlateBus}")]
        public async Task<ActionResult<Bus>> Getbus(string licensePlateBus)
        {
            var bus = await _context.Buses.FindAsync(licensePlateBus);

            if (bus == null)
            {
                return NotFound();
            }

            return bus_ToDTO(bus);
        }

        [HttpPut("{licensePlateBus}")]
        public async Task<ActionResult<Bus>> Updatebus(string licensePlateBus, Bus Bus)
        {
            if (licensePlateBus != Bus.LicensePlateBus)
            {
                return BadRequest();
            }

            var bus = await _context.Buses.FindAsync(licensePlateBus);
            if (bus == null)
            {
                return NotFound();
            }

            
            bus.LicensePlateBus = Bus.LicensePlateBus;
            bus.Model = Bus.Model;
            bus.SeatedPassengerCapacity = Bus.SeatedPassengerCapacity;
            bus.StandingPassengerCapacity = Bus.StandingPassengerCapacity;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception) when (!busExists(licensePlateBus))
            {
                return NotFound();
            }
            
            return bus_ToDTO(bus);
        }

        [HttpPost]
        public async Task<ActionResult<Bus>> Createbus(Bus bus)
        {
            var busAux = new Bus
            {
                LicensePlateBus = bus.LicensePlateBus,
                Model = bus.Model,
                SeatedPassengerCapacity = bus.SeatedPassengerCapacity,
                StandingPassengerCapacity = bus.StandingPassengerCapacity
            };

            _context.Buses.Add(busAux);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Getbus),
                new { LicensePlateBus = bus.LicensePlateBus },
                bus_ToDTO(bus));
        }

        [HttpDelete("{licensePlateBus}")]
        public async Task<ActionResult<Bus>> Deletebus(string licensePlateBus)
        {
            var bus = await _context.Buses.FindAsync(licensePlateBus);

            if (bus == null)
            {
                return NotFound();
            }

            _context.Buses.Remove(bus);
            await _context.SaveChangesAsync();

            return bus_ToDTO(bus);
        }

        private bool busExists(string licensePlateBus) =>
             _context.Buses.Any(e => e.LicensePlateBus == licensePlateBus);

        private static Bus bus_ToDTO(Bus bus) =>
            new Bus
            {
                LicensePlateBus = bus.LicensePlateBus,
                Model = bus.Model,
                SeatedPassengerCapacity = bus.SeatedPassengerCapacity,
                StandingPassengerCapacity = bus.StandingPassengerCapacity
            };
    }
}
