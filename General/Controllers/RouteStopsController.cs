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
    public class RouteStopsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public RouteStopsController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public object GetRouteStops()
        {
            return _context.RouteStops_s.Where(b => b.Route_IdRoute >= 0).Select((c) => new
            {
                Route_IdRoute = c.Route_IdRoute,
                BusStop_IdBusStop = c.BusStop_IdBusStop,
                positionRouteStops = c.positionRouteStops
            }).ToList();
        }

        [HttpGet("RouteId/{Route_IdRoute}")]
        public object GetBusStopsByIdRoute(int Route_IdRoute)
        {
            var res = _context.RouteStops_s.Join(_context.Routes,
                          a => a.Route_IdRoute,
                          b => b.IdRoute,
                          (a, b) => new { A = a, B = b }).Where(c => c.A.Route_IdRoute == c.B.IdRoute && c.A.Route_IdRoute == Route_IdRoute)
                          .Join(_context.BusStops,
                          ab => ab.A.BusStop_IdBusStop,
                          c => c.IdBusStop,
                          (ab, c) => new
                          {
                              routeStops = ab.A,
                              route = ab.B,
                              busStop = c
                          }).Where(d => d.routeStops.BusStop_IdBusStop == d.busStop.IdBusStop)
                                        .Select((e) => new
                                        {
                                            positionRouteStops = e.routeStops.positionRouteStops,
                                            nameOrAddressBusStop = e.busStop.NameOrAddressBusStop
                                        });

            return res;
        }

        [HttpGet("Route/{Route_IdRoute}/BusStop/{BusStop_IdBusStop}/positionRouteStops/{positionRouteStops}")]
        public async Task<ActionResult<RouteStops>> GetRouteStopsAccoidingToIdRouteAndIdStopPosition(int Route_IdRoute, int BusStop_IdBusStop, int positionRouteStops)
        {
            var routeStops = await _context.RouteStops_s.FindAsync(Route_IdRoute, BusStop_IdBusStop, positionRouteStops);

            if (routeStops == null)
            {
                return NotFound();
            }

            return routeStops_ToDTO(routeStops);
        }

        [HttpGet("Route/{Route_IdRoute}/BusStop/{BusStop_IdBusStop}")]
        public async Task<ActionResult<object>> GetRouteStopsAccoidingToIdRouteAndIdStop(int Route_IdRoute, int BusStop_IdBusStop)
        {
            return _context.RouteStops_s.Where(b => b.Route_IdRoute == Route_IdRoute && b.BusStop_IdBusStop == BusStop_IdBusStop).Select((c) => new
            {
                Route_IdRoute = c.Route_IdRoute,
                BusStop_IdBusStop = c.BusStop_IdBusStop,
                positionRouteStops = c.positionRouteStops
            }).ToList();

        }

        [HttpPost]
        public async Task<ActionResult<Message>> CreateRouteStops(RouteStops routeStops)
        {
            var routeStopsAux = new RouteStops
            {
                Route_IdRoute = routeStops.Route_IdRoute,
                BusStop_IdBusStop = routeStops.BusStop_IdBusStop,
                positionRouteStops = routeStops.positionRouteStops
            };

            _context.RouteStops_s.Add(routeStopsAux);
            await _context.SaveChangesAsync();

            var message = new Message("Route Stop successfully added");

            return message;
        }

        [HttpDelete("Route/{Route_IdRoute}/BusStop/{BusStop_IdBusStop}/positionRouteStops/{positionRouteStops}")]
        public async Task<ActionResult<RouteStops>> DeleteRouteStopsAccordingToIdRouteAndIdStopAndPosition(int route_IdRoute, int busStop_IdBusStop, int PositionRouteStops)
        {
            RouteStops routeStops = new RouteStops
            {
                Route_IdRoute = route_IdRoute,
                BusStop_IdBusStop = busStop_IdBusStop,
                positionRouteStops = PositionRouteStops
            };

            if (routeStops == null)
            {
                return NotFound();
            }

            _context.RouteStops_s.Remove(routeStops);
            await _context.SaveChangesAsync();

            return routeStops_ToDTO(routeStops);
        }

        private bool routesStopsExists(int Route_IdRoute) =>
             _context.RouteStops_s.Any(e => e.Route_IdRoute == Route_IdRoute);

        private static RouteStops routeStops_ToDTO(RouteStops routeStops) =>
            new RouteStops
            {
                Route_IdRoute = routeStops.Route_IdRoute,
                BusStop_IdBusStop = routeStops.BusStop_IdBusStop,
                positionRouteStops = routeStops.positionRouteStops
            };
    }
}
