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
    public class BusStopController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public BusStopController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public object GetBusStop()
        {
            return _context.BusStops.Where(b => b.IdBusStop >= 0).Select((c) => new
            {
                IdBusStop = c.IdBusStop,
                NameOrAddressBusStop = c.NameOrAddressBusStop
            }).ToList();
        }

        [HttpGet("{IdBusStop}")]
        public async Task<ActionResult<BusStop>> GetBusStop(int IdBusStop)
        {
            var busStop = await _context.BusStops.FindAsync(IdBusStop);

            if (busStop == null)
            {
                return NotFound();
            }

            return busStop_ToDTO(busStop);
        }

        [HttpPut("{IdBusStop}")]
        public async Task<ActionResult<BusStop>> UpdateBusStop(int IdBusStop, BusStop busStop)
        {
            if (IdBusStop != busStop.IdBusStop)
            {
                return BadRequest();
            }

            var StopAux = await _context.BusStops.FindAsync(IdBusStop);
            if (StopAux == null)
            {
                return NotFound();
            }

            StopAux.IdBusStop = busStop.IdBusStop;
            StopAux.NameOrAddressBusStop = busStop.NameOrAddressBusStop;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception) when (!busStopExists(IdBusStop))
            {
                return NotFound();
            }

            return busStop_ToDTO(StopAux);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> CreateBusStop(BusStop busStop)
        {
            var busStopAux = new BusStop
            {
                NameOrAddressBusStop = busStop.NameOrAddressBusStop
            };

            _context.BusStops.Add(busStopAux);
            await _context.SaveChangesAsync();

            var message = new Message("Bus Stop successfully added");

            return message;
        }

        [HttpDelete("{IdBusStop}")]
        public async Task<ActionResult<BusStop>> DeleteBusStop(int IdBusStop)
        {
            var busStop = await _context.BusStops.FindAsync(IdBusStop);

            if (busStop == null)
            {
                return NotFound();
            }

            _context.BusStops.Remove(busStop);
            await _context.SaveChangesAsync();

            return busStop_ToDTO(busStop);
        }

        private bool busStopExists(int IdBusStop) =>
             _context.BusStops.Any(e => e.IdBusStop == IdBusStop);

        private static BusStop busStop_ToDTO(BusStop busStop) =>
            new BusStop
            {
                IdBusStop = busStop.IdBusStop,
                NameOrAddressBusStop = busStop.NameOrAddressBusStop
            };
    }
}
