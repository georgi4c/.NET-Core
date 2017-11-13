using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services.Models
{
    public class SaleDetailsModel : SaleListModel
    {
        public CarModel Car { get; set; }
    }
}
