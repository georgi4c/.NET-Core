using CarDealer.Services;
using CarDealer.Web.Models.Suppliers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealer.Web.Controllers
{
    public class SuppliersController : Controller
    {
        private ISupplierService suppliers;

        public SuppliersController(ISupplierService suppliers)
        {
            this.suppliers = suppliers;
        }

        [Route("suppliers/local")]
        public IActionResult Local()
            => View("Suppliers", this.GetSuppliers(false));


        [Route("suppliers/importers")]
        public IActionResult Importers()
            => View("Suppliers", this.GetSuppliers(true));

        private SuppliersModel GetSuppliers(bool importers)
        {
            var type = importers ? "Importer" : "Local";

            var suppliers = this.suppliers.AllListings(importers);

            return new SuppliersModel
            {
                Type = type,
                Suppliers = suppliers
            };
        }
    }
}
