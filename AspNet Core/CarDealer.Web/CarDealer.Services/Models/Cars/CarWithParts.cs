using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services.Models
{
    public class CarWithParts : CarModel
    {
        public List<PartModel> Parts { get; set; }
    }
}
