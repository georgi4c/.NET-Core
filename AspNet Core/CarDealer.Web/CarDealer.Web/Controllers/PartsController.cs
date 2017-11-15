using CarDealer.Services;
using CarDealer.Web.Models.Parts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealer.Web.Controllers
{
    public class PartsController : Controller
    {
        private const int PageSize = 25;

        private readonly IPartService parts;

        public PartsController(IPartService parts)
        {
            this.parts = parts;
        }

        public IActionResult Create() => View();

        public IActionResult All(int page = 1)
            => View(new PartPageListingModel()
            {
                Parts = this.parts.All(page, PageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.parts.Total() / (double)PageSize)
            });
    }
}
