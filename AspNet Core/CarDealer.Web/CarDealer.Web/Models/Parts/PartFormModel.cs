using CarDealer.Services.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealer.Web.Models.Parts
{
    public class PartFormModel
    {
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        public IEnumerable<SupplierModel> Suppliers { get; set; }
    }
}
