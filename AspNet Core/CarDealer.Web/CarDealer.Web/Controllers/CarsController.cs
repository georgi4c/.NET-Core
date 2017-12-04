using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarDealer.Services;
using CarDealer.Web.Models.Cars;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace CarDealer.Web.Controllers
{
    [Route("cars")]
    public class CarsController : Controller
    {
        private ICarService cars;
        private IPartService parts;

        public CarsController(ICarService carService, IPartService parts)
        {
            this.cars = carService;
            this.parts = parts;
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
        
        [Authorize]
        [Route(nameof(Create))]
        public IActionResult Create()
            => View(new CarFormModel
            {
                AllParts = GetPartsSelectItems()
            });

        [Authorize]
        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CarFormModel carModel)
        {
            if (!ModelState.IsValid)
            {
                carModel.AllParts = GetPartsSelectItems();
                return View(carModel);
            }

            this.cars.Create(
                carModel.Make,
                carModel.Model,
                carModel.TravelledDistance,
                carModel.SelectedParts);

            return RedirectToAction(nameof(WithParts));
        }

        [Route("parts")]
        public IActionResult WithParts()
            => View(this.cars.WithParts());

        private IEnumerable<SelectListItem> GetPartsSelectItems()
            => this.parts
                .All()
                .Select(p => new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                });
    }
}