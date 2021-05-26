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
    public class DriverController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public DriverController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public object GetDriver()
        {
            return _context.Drivers.Where(b => b.DriversLicense.Contains("")).Select((c) => new
            {
                DriversLicense = c.DriversLicense,
                Name = c.Name,
                DriverExperience = c.DriverExperience,
                AverageDriverRating = c.AverageDriverRating
            }).ToList();
        }

        [HttpGet("{DriversLicense}")]
        public async Task<ActionResult<Driver>> GetDriver(string DriversLicense)
        {
            var driver = await _context.Drivers.FindAsync(DriversLicense);

            if (driver == null)
            {
                return NotFound();
            }

            return driver_ToDTO(driver);
        }

        [HttpPut("{DriversLicense}")]
        public async Task<ActionResult<Driver>> UpdateDriver(string DriversLicense, Driver driver)
        {
            if (DriversLicense != driver.DriversLicense)
            {
                return BadRequest();
            }

            var driverAux = await _context.Drivers.FindAsync(DriversLicense);
            if (driverAux == null)
            {
                return NotFound();
            }


            driverAux.DriversLicense = driver.DriversLicense;
            driverAux.Name = driver.Name;
            driverAux.DriverExperience = driver.DriverExperience;
            driverAux.AverageDriverRating = driver.AverageDriverRating;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception) when (!driverExists(DriversLicense))
            {
                return NotFound();
            }

            return driver_ToDTO(driverAux);
        }

        [HttpPost]
        public async Task<ActionResult<Driver>> Createbus(Driver driver)
        {
            var driverAux = new Driver
            {
                DriversLicense = driver.DriversLicense,
                Name = driver.Name,
                DriverExperience = driver.DriverExperience,
                AverageDriverRating = driver.AverageDriverRating
            };

            _context.Drivers.Add(driverAux);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDriver),
                new { LicensePlateBus = driver.DriversLicense },
                driver_ToDTO(driver));
        }

        [HttpDelete("{DriversLicense}")]
        public async Task<ActionResult<Driver>> Deletebus(string DriversLicense)
        {
            var driver = await _context.Drivers.FindAsync(DriversLicense);

            if (driver == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();

            return driver_ToDTO(driver);
        }

        private bool driverExists(string DriversLicense) =>
             _context.Drivers.Any(e => e.DriversLicense == DriversLicense);

        private static Driver driver_ToDTO(Driver driver) =>
            new Driver
            {
                DriversLicense = driver.DriversLicense,
                Name = driver.Name,
                DriverExperience = driver.DriverExperience,
                AverageDriverRating = driver.AverageDriverRating
            };
    }
}
