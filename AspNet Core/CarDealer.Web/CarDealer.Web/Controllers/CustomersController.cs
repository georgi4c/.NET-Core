using CarDealer.Services;
using CarDealer.Services.Models;
using CarDealer.Web.Extensions;
using CarDealer.Web.Models.Customers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealer.Web.Controllers
{
    [Route("customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Route("all/{order}")]
        public IActionResult All(string order)
        {
            var orderDirection = order.ToLower() == "ascending"
                ? OrderDirection.Ascending
                : OrderDirection.Descending;

            var result = this.customers.Ordered(orderDirection);
            return View(new AllCustomersModel
            {
                Customers = result,
                OrderDirection = orderDirection
            });
        }

        [Route("{id}")]
        public IActionResult Expenses(int id)
            => this.ViewOrNotFound(this.Expenses(id));

        [Route(nameof(Create))]
        public IActionResult Create()
            => View();

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CreateCustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.customers.Create(
                model.Name,
                model.BirthDate,
                model.IsYoungDriver);

            return RedirectToAction(nameof(All), new { oreder = OrderDirection.Ascending});
        }

    }
}
