using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services.Models.Parts
{
    public class PartListingModel : PartModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public string SupplierName { get; set; }
    }
}
