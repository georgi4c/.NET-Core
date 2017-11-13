using CarDealer.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services
{
    public interface ICarService
    {
        List<CarModel> ByMake(string make);
        List<CarWithParts> WithParts();
    }
}
