using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarDealer.Services;
using CarDealer.Web.Models.Cars;

namespace CarDealer.Web.Controllers
{
    [Route("cars")]
    public class CarsController : Controller
    {
        private ICarService cars;
        public CarsController(ICarService carService)
        {
            this.cars = carService;
        }
        [Route("{make}")]
        public IActionResult ByMake(string make)
        {
            var result = cars.ByMake(make);
            return View(new CarsByMakeModel()
            {
                Make = make,
                Cars = result
            });
        }

        [Route("parts")]
        public IActionResult WithParts()
            => View(this.cars.WithParts());
    }
}